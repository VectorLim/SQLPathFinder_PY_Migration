from __future__ import annotations

import csv
import os
import re
from collections.abc import Iterable, Iterator, Mapping
from contextlib import contextmanager
from dataclasses import dataclass, field
from pathlib import Path

from vg2c_new.model import Command, CommandKind
from vg2c_new.paths import resolve_path, working_directory_for
from vg2c_new.utilities.base import Utility

_ENV_TOKEN_RE = re.compile(r"<<<%([^%]+)%>>>", re.IGNORECASE)
_MACRO_TOKEN_RE = re.compile(r"<<<(.*?)>>>", re.IGNORECASE | re.MULTILINE)
_VAR_RE = re.compile(r"^VAR\((.*?)\)$", re.IGNORECASE)
_ENV_VALUE_RE = re.compile(r"^ENV\((.*?)\)$", re.IGNORECASE)


@dataclass(slots=True)
class RuntimeState:
    """Minimal mutable execution state for one direct-runtime invocation."""

    working_directory: Path
    globals: dict[str, str] = field(default_factory=dict)
    environment: Mapping[str, str] = field(default_factory=lambda: dict(os.environ))
    _frames: list[dict[str, str]] = field(default_factory=list, init=False, repr=False)

    def __post_init__(self) -> None:
        self.working_directory = Path(self.working_directory).resolve(strict=False)
        self.globals = {key.upper(): str(value) for key, value in self.globals.items()}

    def set_global(self, name: str, value: object) -> None:
        self.globals[name.upper()] = str(value)

    def environment_value(self, name: str) -> str | None:
        if name in self.environment:
            return self.environment[name]
        upper = name.upper()
        for key, value in self.environment.items():
            if key.upper() == upper:
                return value
        return None

    def lookup(self, name: str, default: str | None = None) -> str | None:
        key = name.upper()
        for frame in reversed(self._frames):
            if key in frame:
                return frame[key]
        if key in self.globals:
            return self.globals[key]
        env = self.environment_value(name)
        return default if env is None else env

    def push_frame(self, values: Mapping[str, object]) -> None:
        self._frames.append({key.upper(): str(value) for key, value in values.items()})

    def pop_frame(self) -> None:
        if not self._frames:
            raise RuntimeError("RuntimeState frame stack is empty.")
        self._frames.pop()

    @contextmanager
    def frame(self, values: Mapping[str, object]) -> Iterator[None]:
        self.push_frame(values)
        try:
            yield
        finally:
            self.pop_frame()

    def substitute(self, value: str) -> str:
        """Amended port of ScriptHost Substitute_Env/Substitute_Macro.

        Original: SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Utilities.Substitute_Env
        and Utilities.Substitute_Macro.
        Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED_PORT.
        Preserved: <<<%ENV%>>> syntax, case-insensitive <<<macro>>> lookup,
        unresolved ordinary macro errors, and reserved spf token behavior.
        Amendments: resolves RuntimeState frames/globals instead of MemTable/global
        singleton state; loop/site tokens use the same frame lookup.
        Discarded: mutable MemTable traversal, SPFGlobals, Python-2 branches.
        """

        def replace_env(match: re.Match[str]) -> str:
            name = match.group(1)
            resolved = self.environment_value(name)
            if not resolved:
                raise RuntimeError(f"Environment variable {match.group(0)} not found")
            return resolved

        out = _ENV_TOKEN_RE.sub(replace_env, value)

        def replace_macro(match: re.Match[str]) -> str:
            token = match.group(1).strip()
            full = match.group(0)
            if token.lower() == "spf_datetime":
                return full
            resolved = self.lookup(token)
            if resolved is not None:
                return resolved
            upper = token.upper()
            if (
                upper.startswith("SPF-")
                or upper.startswith("SPF$")
                or upper.startswith("%")
                or upper.startswith("!")
            ):
                return full
            raise RuntimeError(f"Macro variable {full} not found")

        return _MACRO_TOKEN_RE.sub(replace_macro, out)


class Interpreter:
    """Executes immutable Commands directly with stateless Utility bindings."""

    def __init__(self, utilities: Mapping[str, Utility] | None = None):
        self._utilities = dict(utilities or {})

    def execute(self, commands: Iterable[Command], state: RuntimeState) -> None:
        for command in commands:
            self._execute(command, state)

    def _execute(self, command: Command, state: RuntimeState) -> None:
        if command.kind in {CommandKind.UTILITY, CommandKind.QUERY}:
            self._execute_utility(command, state)
            return
        if command.kind is CommandKind.IF:
            self._execute_if(command, state)
            return
        if command.kind is CommandKind.MACRO:
            self._execute_macro(command, state)
            return
        if command.kind is CommandKind.FOR_LOOP:
            self._execute_for_loop(command, state)
            return
        if command.kind is CommandKind.SITE_LOOP:
            self._execute_site_loop(command, state)
            return
        if command.kind is CommandKind.RUN_LOOP:
            self._execute_run_loop(command, state)
            return
        if command.kind is CommandKind.SCOPE:
            # BEGIN/END-HPC was a transport boundary. The direct runtime flattens it.
            self.execute(command.children, state)
            return
        raise _runtime_error(command, f"Unexpected executable command kind {command.kind.value!r}.")

    def _execute_utility(self, command: Command, state: RuntimeState) -> None:
        if command.utility_type is None:
            raise _runtime_error(command, "Resolved command has no utility type.")
        utility = self._utilities.get(command.utility_type)
        if utility is None:
            raise _runtime_error(command, f"No runtime utility bound for {command.utility_type!r}.")
        try:
            utility.apply(command, state)
        except Exception as exc:
            if isinstance(exc, RuntimeError) and str(exc).startswith(command.span.location):
                raise
            raise _runtime_error(command, str(exc)) from exc

    def _execute_if(self, command: Command, state: RuntimeState) -> None:
        """Amended port of ScriptHost IfThenTask.executeTaskCommand.

        Original: SPSQL3_py/SPFLib/SPFSQL3.py :: IfThenTask.executeTaskCommand,
        parseMyEVxIsConstOrEnvvar, parseMyVal; comparison from Utilities.CompareVars.
        Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED_PORT.
        Preserved: one/two-condition IF, VAR()/ENV(), AND/OR, numeric/string
        comparison operators and ELSE behavior.
        Amendments: explicit boolean composition instead of Python eval(); state
        lookup replaces process-global environment variables created by old tasks.
        Discarded: mutable child task extraction and global abort flags.
        """
        args = tuple(state.substitute(arg) for arg in command.arguments)
        if len(args) < 3:
            raise _runtime_error(command, "{IF-THEN} requires at least three arguments.")
        if len(args) > 7:
            raise _runtime_error(command, "{IF-THEN} supports at most two conditions.")

        first = self._condition(args[0], args[1], args[2], state, command)
        result = first

        extras = list(args[3:]) + [""] * max(0, 5 - len(args[3:]))
        logic, lhs2, op2, rhs2 = extras[:4]
        second_fields = (logic, lhs2, op2, rhs2)
        if any(field.strip() for field in second_fields):
            if not all(field.strip() for field in second_fields):
                raise _runtime_error(
                    command, "The second IF condition is only partially specified."
                )
            logic_key = logic.strip().lower()
            if logic_key not in {"and", "or"}:
                raise _runtime_error(command, f"Invalid IF logical operator {logic!r}.")
            second = self._condition(lhs2, op2, rhs2, state, command)
            result = (result and second) if logic_key == "and" else (result or second)

        self.execute(command.children if result else command.else_children, state)

    def _condition(
        self, lhs: str, operator: str, rhs: str, state: RuntimeState, command: Command
    ) -> bool:
        lhs_match = _VAR_RE.fullmatch(lhs.strip())
        left = lhs_match.group(1) if lhs_match else (state.lookup(lhs.strip(), "") or "")
        rhs_match = _ENV_VALUE_RE.fullmatch(rhs.strip())
        right = (state.environment_value(rhs_match.group(1)) or "") if rhs_match else rhs
        try:
            return compare_vars(left, right, operator)
        except Exception as exc:
            raise _runtime_error(command, str(exc)) from exc

    def _execute_macro(self, command: Command, state: RuntimeState) -> None:
        """Amended port of ScriptHost StartMacroTask.executeTaskCommand.

        Original: SPSQL3_py/SPFLib/SPFSQL3.py :: StartMacroTask.executeTaskCommand
        with Utilities.Substitute_Macro.
        Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED_PORT.
        Preserved: macro-file selection, default continue-on-error, first-row
        substitution and nested macro scoping.
        Amendments: csv module + RuntimeState frame; immutable Commands are never
        deep-copied or rewritten.
        Discarded: MemTable, SPFGlobals, AlreadyMacFile mutation, console state.
        """
        if not command.arguments:
            raise _runtime_error(command, "{START-MACRO} requires a macro file.")
        macro_name = state.substitute(command.arguments[0])
        continue_on_error = True
        if len(command.arguments) > 1 and command.arguments[1].strip():
            try:
                continue_on_error = _as_bool(state.substitute(command.arguments[1]))
            except ValueError:
                pass

        base = working_directory_for(command.option("WORKDIR"), state)
        path = resolve_path(macro_name, state, base=base)
        if not path.exists():
            if continue_on_error:
                return
            raise _runtime_error(command, f"Macro file not found: {path}")

        delimiter = _file_delimiter(path, mode="I")
        try:
            with path.open("r", encoding="utf-8-sig", newline="") as handle:
                reader = csv.reader(handle, delimiter=delimiter)
                header = next(reader, None)
                row = next(reader, None)
        except OSError as exc:
            if continue_on_error:
                return
            raise _runtime_error(command, str(exc)) from exc
        if not header or row is None:
            return

        values = {name: row[index] if index < len(row) else "" for index, name in enumerate(header)}
        with state.frame(values):
            self.execute(command.children, state)

    def _execute_for_loop(self, command: Command, state: RuntimeState) -> None:
        """Amended port of the current ScriptHost ForLoopTask Version-2 branch.

        Original: SPSQL3_py/SPFLib/SPFSQL3.py :: ForLoopTask.executeTaskCommand /
        Substitute_Loop_Counter.
        Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED_PORT.
        Preserved: non-negative integer start/end, numeric step, default reverse=Y,
        inclusive iteration count, current Version-2 token names/values.
        Amendments: loop values live in RuntimeState frames; no Command deepcopy.
        Discarded: historical Version-1 execution branch and mutable task rewriting.
        """
        args = tuple(state.substitute(arg) for arg in command.arguments)
        if len(args) < 4:
            raise _runtime_error(command, "{FOR-LOOP} requires start, end, step, and suffix.")
        if len(args) > 6:
            raise _runtime_error(command, "{FOR-LOOP} received too many arguments.")
        start_text, end_text, step_text, suffix = args[:4]
        if not start_text.isdigit() or not end_text.isdigit() or not suffix.strip():
            raise _runtime_error(command, "Invalid {FOR-LOOP} start/end/suffix.")
        start, end = int(start_text), int(end_text)
        if start > end:
            raise _runtime_error(command, "{FOR-LOOP} start cannot exceed end.")
        try:
            step = float(step_text)
        except ValueError as exc:
            raise _runtime_error(command, f"Invalid {{FOR-LOOP}} step {step_text!r}.") from exc
        if step < 0:
            raise _runtime_error(command, "{FOR-LOOP} step must be non-negative.")

        reverse = True
        if len(args) >= 5 and args[4].strip():
            try:
                reverse = _as_bool(args[4])
            except ValueError:
                pass
        if len(args) >= 6 and args[5].strip():
            raise _runtime_error(
                command,
                "Historical {FOR-LOOP} version selection is not supported; only current Version 2 semantics remain.",
            )
        if step == 0:
            return

        start_i, end_i = (end, start) if reverse else (start, end)
        increment = -step if reverse else step
        iterations = int((end - start) / step) + 1
        current = float(start_i)
        loop_name = f"spf-loop-ctr-{suffix}"

        for _ in range(iterations):
            adjusted = increment
            if not reverse and current + adjusted > end_i and start_i != 0:
                adjusted = end_i - current
            elif reverse and current + adjusted < end_i and end_i != 0:
                adjusted = end_i - current
            token_step = -adjusted
            values = {
                f"spf-start-{suffix}": str(start_i),
                f"spf-end-{suffix}": str(end_i),
                f"spf-step-{suffix}": str(float(token_step)),
                f"spf-step-{suffix}-int": str(int(token_step)),
                loop_name: str(float(current)),
                f"{loop_name}-int": str(int(current)),
            }
            with state.frame(values):
                self.execute(command.children, state)
            current += adjusted

    def _execute_site_loop(self, command: Command, state: RuntimeState) -> None:
        """Amended port of ScriptHost SiteLoopTask.

        Original: SPSQL3_py/SPFLib/SPFSQL3.py :: SiteLoopTask.executeTaskCommand /
        Substitute_Site_Value and Utilities.Replace_Special_Chars.
        Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED_PORT.
        Preserved: comma site list, spf-site tokens, filename-safe site token, and
        current stop-after-first-failing-site behavior.
        Amendments: RuntimeState frame instead of deep-copy/substitute child tasks.
        Discarded: global abort mutation and console behavior.
        """
        if not command.arguments:
            raise _runtime_error(command, "{SITE-LOOP} requires a site list.")
        sites = [site for site in state.substitute(command.arguments[0]).split(",") if site]
        if not sites:
            raise _runtime_error(command, "{SITE-LOOP} site list is empty.")
        for site in sites:
            values = {
                "spf-site": site,
                "spf-site-for-file-name": _replace_special_chars(site),
            }
            try:
                with state.frame(values):
                    self.execute(command.children, state)
            except Exception:
                # The current ScriptHost source prints "next iteration" but executes
                # break; preserve the actual code path rather than the message.
                break

    def _execute_run_loop(self, command: Command, state: RuntimeState) -> None:
        """Amended portable port of ScriptHost RunLoopTask.

        Original: SPSQL3_py/SPFLib/SPFSQL3.py :: RunLoopTask.executeTaskCommand.
        Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED_PORT.
        Preserved: input/output files, data-row chunking, output rewrite per chunk,
        optional continue-on-error, and missing-input no-op.
        Amendments: stdlib csv streaming instead of pandas/temp-copy/global flags.
        Discarded: gAnyLoop/gMyAbort bookkeeping and Windows cleanup helpers.
        """
        args = tuple(state.substitute(arg) for arg in command.arguments)
        if len(args) < 3:
            raise _runtime_error(
                command, "{RUN-LOOP} requires input file, output file, and row size."
            )
        input_name, output_name, rows_text = args[:3]
        if not rows_text.isdigit() or int(rows_text) <= 0:
            raise _runtime_error(command, f"Invalid {RUN_LOOP_LABEL} row size {rows_text!r}.")
        continue_on_error = False
        if len(args) >= 4 and args[3].strip():
            try:
                continue_on_error = _as_bool(args[3])
            except ValueError:
                pass

        base = working_directory_for(command.option("WORKDIR"), state)
        input_path = resolve_path(input_name, state, base=base)
        output_path = resolve_path(output_name, state, base=base)
        if not input_path.exists():
            return

        source_delimiter = _file_delimiter(input_path, mode="I")
        target_delimiter = _file_delimiter(output_path, mode="O")
        chunk_size = int(rows_text)
        with input_path.open("r", encoding="utf-8-sig", newline="") as handle:
            reader = csv.reader(handle, delimiter=source_delimiter)
            header = next(reader, None)
            if header is None:
                return
            chunk: list[list[str]] = []
            for row in reader:
                chunk.append(row)
                if len(chunk) == chunk_size:
                    self._run_chunk(
                        command,
                        state,
                        output_path,
                        target_delimiter,
                        header,
                        chunk,
                        bool(continue_on_error),
                    )
                    chunk = []
            if chunk:
                self._run_chunk(
                    command,
                    state,
                    output_path,
                    target_delimiter,
                    header,
                    chunk,
                    bool(continue_on_error),
                )

    def _run_chunk(
        self,
        command: Command,
        state: RuntimeState,
        output_path: Path,
        delimiter: str,
        header: list[str],
        rows: list[list[str]],
        continue_on_error: bool,
    ) -> None:
        try:
            with output_path.open("w", encoding="utf-8", newline="") as handle:
                writer = csv.writer(handle, delimiter=delimiter, lineterminator="\n")
                writer.writerow(header)
                writer.writerows(rows)
            self.execute(command.children, state)
        except Exception:
            if not continue_on_error:
                raise


RUN_LOOP_LABEL = "{RUN-LOOP}"


def compare_vars(var1: object, var2: object, operator: str = "EQS") -> bool:
    """Direct/amended port of Utilities.CompareVars without runtime-global side effects.

    Original: SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Utilities.CompareVars.
    Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
    Port mode: AMENDED_PORT.
    Preserved: EQ/NE/LE/GE/LT/GT, string-S forms, BT/NBT semantics, numeric floats,
    empty equality RHS normalization.
    Amendments: pure function, raises ValueError for numeric conversion failures.
    Discarded: gMyAbort/console/MyMode error-routing side effects.
    """
    op = (operator or "EQS").upper()
    left = "" if var1 is None else str(var1)
    right = "" if var2 is None else str(var2)

    if op in {"EQ", "EQS", "NE", "NES"} and not right:
        right = "&NBSP;"
        if not op.endswith("S"):
            op += "S"

    is_string = op.endswith("S")
    if is_string:
        left_value: str | float = left.upper()
    else:
        try:
            left_value = float(left)
        except ValueError as exc:
            raise ValueError(
                f"Invalid column for numeric conditional test (datatype mismatch). Exiting job: {left}"
            ) from exc

    if op in {"BT", "BTS", "NBT", "NBTS"}:
        try:
            low_text, high_text = next(csv.reader([right], delimiter=",", skipinitialspace=True))
        except (csv.Error, ValueError) as exc:
            raise ValueError("Between value must contain low,high.") from exc
        if is_string:
            low: str | float = low_text.upper()
            high: str | float = high_text.upper()
        else:
            try:
                low = float(low_text)
                high = float(high_text)
            except ValueError as exc:
                raise ValueError("Between bounds must be numeric.") from exc
        if op in {"BT", "BTS"}:
            return low <= left_value <= high
        return not (low < left_value < high)

    if is_string:
        right_value: str | float = right.upper()
    else:
        try:
            right_value = float(right)
        except ValueError as exc:
            raise ValueError(
                f"Invalid column for numeric conditional test (datatype mismatch). Exiting job: {right}"
            ) from exc

    if op in {"EQ", "EQS"}:
        return left_value == right_value
    if op in {"NE", "NES"}:
        return left_value != right_value
    if op in {"LE", "LES"}:
        return left_value <= right_value
    if op in {"GE", "GES"}:
        return left_value >= right_value
    if op in {"LT", "LTS"}:
        return left_value < right_value
    if op in {"GT", "GTS"}:
        return left_value > right_value
    return False


def _file_delimiter(path: Path, *, mode: str) -> str:
    suffix = path.suffix.lower()
    if suffix in {".tab", ".hive-tab", ".hive-sequence"}:
        return "\t"
    if suffix == ".asc":
        return "|"
    if suffix == ".plus":
        return "+"
    if suffix == ".txt" and mode == "O":
        return ","  # csv.writer requires a one-character delimiter; Session 1 uses CSV/TAB loops.
    return ","


def _replace_special_chars(value: str) -> str:
    # Direct port of Utilities.Replace_Special_Chars(MyMode=0).
    chars = r'?/|<>"*{};[].\/'
    return "".join("_" if char in chars else char for char in value)


def _as_bool(value: str) -> bool:
    normalized = value.strip().upper()
    if normalized in {"Y", "YES", "TRUE", "1", "G"}:
        return True
    if normalized in {"N", "NO", "FALSE", "0", ""}:
        return False
    raise ValueError(f"Invalid boolean value {value!r}.")


def _runtime_error(command: Command, message: str) -> RuntimeError:
    return RuntimeError(f"{command.span.location}: {message}")
