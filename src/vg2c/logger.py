from __future__ import annotations

import logging
from collections.abc import Mapping, Sequence
from typing import Any

# Re-export common logging constants for drop-in familiarity.
CRITICAL = logging.CRITICAL
ERROR = logging.ERROR
WARNING = logging.WARNING
INFO = logging.INFO
DEBUG = logging.DEBUG
NOTSET = logging.NOTSET


def _format_table(
	rows: Sequence[Mapping[str, Any]] | Sequence[Sequence[Any]],
	headers: Sequence[str] | None = None,
	title: str | None = None,
) -> str:
	if not rows:
		return f"{title}\n<empty table>" if title else "<empty table>"

	first = rows[0]
	body: list[list[str]] = []

	if isinstance(first, Mapping):
		cols = list(headers) if headers else []
		if not cols:
			for row in rows:
				if not isinstance(row, Mapping):
					raise TypeError("Mixed table row types are not supported.")
				for key in row:
					key_s = str(key)
					if key_s not in cols:
						cols.append(key_s)
		for row in rows:
			if not isinstance(row, Mapping):
				raise TypeError("Mixed table row types are not supported.")
			body.append([str(row.get(c, "")) for c in cols])
	else:
		cols = [str(h) for h in headers] if headers else [f"col_{i+1}" for i in range(max(len(r) for r in rows))]
		for row in rows:
			if isinstance(row, Mapping):
				raise TypeError("Mixed table row types are not supported.")
			vals = [str(v) for v in row]
			if len(vals) < len(cols):
				vals.extend([""] * (len(cols) - len(vals)))
			body.append(vals)

	widths = [len(c) for c in cols]
	for row in body:
		for i, value in enumerate(row):
			widths[i] = max(widths[i], len(value))

	border = "+" + "+".join("-" * (w + 2) for w in widths) + "+"
	header = "| " + " | ".join(cols[i].ljust(widths[i]) for i in range(len(cols))) + " |"
	lines = [
		"| " + " | ".join(row[i].ljust(widths[i]) for i in range(len(cols))) + " |"
		for row in body
	]
	out = [border, header, border, *lines, border]
	return (title + "\n" if title else "") + "\n".join(out)


class PrettyLogger(logging.Logger):
	def table(
		self,
		rows: Sequence[Mapping[str, Any]] | Sequence[Sequence[Any]],
		*,
		headers: Sequence[str] | None = None,
		title: str | None = None,
		level: int = INFO,
	) -> None:
		self.log(level, _format_table(rows, headers=headers, title=title))


logging.setLoggerClass(PrettyLogger)


def basicConfig(
	*,
	level: int | str = INFO,
	format: str = "%(asctime)s | %(levelname)-8s | %(name)s | %(message)s",
	datefmt: str = "%Y-%m-%d %H:%M:%S",
) -> None:
	logging.basicConfig(level=level, format=format, datefmt=datefmt)


def getLogger(name: str | None = None) -> PrettyLogger:
	return logging.getLogger(name)  # type: ignore[return-value]


def table(
	rows: Sequence[Mapping[str, Any]] | Sequence[Sequence[Any]],
	*,
	headers: Sequence[str] | None = None,
	title: str | None = None,
	level: int = INFO,
	name: str | None = None,
) -> None:
	getLogger(name).table(rows, headers=headers, title=title, level=level)


__all__ = [
	"CRITICAL",
	"ERROR",
	"WARNING",
	"INFO",
	"DEBUG",
	"NOTSET",
	"PrettyLogger",
	"basicConfig",
	"getLogger",
	"table",
]
