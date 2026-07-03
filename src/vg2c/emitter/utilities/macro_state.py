"""MacroState - runtime macro variable storage and substitution."""

from __future__ import annotations

import re
from contextlib import contextmanager
from pathlib import Path
from typing import Callable, Iterator, Protocol

from vg2c.emitter.utilities._base import UtilitySpec
from vg2c.emitter.utilities._registry import register_utility


class MacroLookup(Protocol):
    """Minimal interface for macro substitution."""

    def named(self, name: str) -> str: ...

    def positional(self) -> str: ...


@register_utility
class MacroState(UtilitySpec):
    """Stack of variable frames; lookups walk top-to-bottom."""

    utility_name = "macro"
    utility_imports = (
        "import re",
        "from contextlib import contextmanager",
        "from pathlib import Path",
        "from typing import Callable, Iterator, Protocol",
    )

    PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>|<<>>")
    NAMED_PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>")
    CROSSTAB_RE = re.compile(
        r"(?:,CrossTab->\[\[\s*([A-Za-z_][A-Za-z0-9_]*)\s*,\s*([^;\]]+)\s*;\s*:([YyNn])\s*\]\]|CrossTab->\[\[\s*([A-Za-z_][A-Za-z0-9_]*)\s*,\s*([^;\]]+)\s*;\s*:([YyNn])\s*\]\],)"
    )

    @staticmethod
    def _extract_selected_columns_by_alias(sql: str) -> dict[str, set[str]]:
        by_alias: dict[str, set[str]] = {}
        match = re.search(
            r"\bSELECT\b(?P<select_part>.*?)\bFROM\b",
            sql,
            flags=re.IGNORECASE | re.DOTALL,
        )
        if not match:
            return by_alias

        select_part = match.group("select_part")
        col_ref_re = re.compile(
            r"\b([A-Za-z_][A-Za-z0-9_]*)\s*\.\s*(?:\[([^\]]+)\]|\"([^\"]+)\"|([A-Za-z_][A-Za-z0-9_]*))"
        )
        for col_match in col_ref_re.finditer(select_part):
            alias = col_match.group(1).lower()
            col_name = col_match.group(2) or col_match.group(3) or col_match.group(4)
            if not col_name:
                continue
            by_alias.setdefault(alias, set()).add(col_name.lower())

        return by_alias

    @classmethod
    def normalize_macro_name(cls, raw: str) -> str:
        name = raw.strip()
        if name.startswith("<<<") and name.endswith(">>>"):
            name = name[3:-3]
        return name.strip().upper()

    @classmethod
    def substitute_crosstab(
        cls,
        sql: str,
        alias_columns_lookup: Callable[[str], list[str]] | None = None,
    ) -> str:
        if alias_columns_lookup is None or "CrossTab->[[" not in sql:
            return sql

        selected_by_alias = cls._extract_selected_columns_by_alias(sql)

        def _replace(match: re.Match[str]) -> str:
            alias = match.group(1)
            mode = match.group(3).upper()
            all_cols = alias_columns_lookup(alias)
            selected = selected_by_alias.get(alias.lower(), set())
            dynamic_cols = [c for c in all_cols if c.lower() not in selected]

            if not dynamic_cols:
                return ""

            if mode == "N":
                return ",".join(dynamic_cols)

            return "\n         ,".join(f"{alias}.[{c}] AS [{c}]" for c in dynamic_cols)

        return cls.CROSSTAB_RE.sub(_replace, sql)

    def __init__(self) -> None:
        self._stack: list[dict[str, str]] = [{}]

    def named(self, name: str) -> str:
        key = name.upper()
        for frame in reversed(self._stack):
            if key in frame:
                return frame[key]
        return ""

    def set_named(self, name: str, value: str) -> None:
        self._stack[-1][name.upper()] = value

    def positional(self) -> str:
        frame = self._stack[-1]
        cursor = frame.get("__cursor__", 0)
        pos_list: list[str] = frame.get("__positional__", [])  # type: ignore[assignment]
        if isinstance(pos_list, list) and cursor < len(pos_list):
            frame["__cursor__"] = cursor + 1
            return pos_list[cursor]
        return ""

    def substitute_sql(
        self,
        sql: str,
        crosstab_alias_columns: Callable[[str], list[str]] | None = None,
    ) -> str:
        if "<<<" in sql:
            sql = self.NAMED_PLACEHOLDER_RE.sub(
                lambda m: self.named(self.normalize_macro_name(m.group(1))),
                sql,
            )
        return self.substitute_crosstab(
            sql, alias_columns_lookup=crosstab_alias_columns
        )

    def write_file(
        self,
        path: str,
        template: str,
        vars: dict[str, str] | None = None,
    ) -> None:
        def _lookup(name: str) -> str:
            key = self.normalize_macro_name(name)
            if vars is not None:
                return vars.get(key, "")
            return self.named(key)

        def _replace(match: re.Match[str]) -> str:
            named = match.group(1)
            if named is not None:
                return _lookup(named)
            return self.positional()

        content = self.PLACEHOLDER_RE.sub(_replace, template)
        content = content.lstrip("\n")

        out = Path(path)
        out.parent.mkdir(parents=True, exist_ok=True)
        out.write_text(content, encoding="utf-8")

    def eval_condition(self, lhs: str, op: str, rhs: str) -> bool:
        lhs_val = self.named(lhs) if lhs.startswith("VAR(") else lhs
        rhs_val = self.named(rhs) if rhs.startswith("VAR(") else rhs
        return lhs_val == rhs_val

    def push_frame(self, named: dict[str, str] | None = None) -> None:
        frame: dict[str, str] = {}
        for k, v in (named or {}).items():
            if k is None:
                continue
            frame[k.upper()] = str(v)
        self._stack.append(frame)

    def pop_frame(self) -> None:
        if len(self._stack) > 1:
            self._stack.pop()

    @contextmanager
    def scope(self, row: dict[str, str] | None = None) -> Iterator[None]:
        self.push_frame(named=row)
        try:
            yield
        finally:
            self.pop_frame()
