"""Compiler-only control payloads and main's source-indexed scope tree."""

from __future__ import annotations

import csv
import io
from dataclasses import dataclass
from typing import Literal

from vg2c.diagnostics import fail
from vg2c.frontend.models import ClassifiedBlock


def utility_arguments(block: ClassifiedBlock) -> list[str]:
    """Validate compiler syntax strictly; runtime utility parsing remains original."""
    try:
        values = next(
            csv.reader(
                io.StringIO(block.options.lookup["UTILITIES"].strip()),
                delimiter=" ",
                skipinitialspace=True,
                strict=True,
            )
        )
    except (csv.Error, StopIteration) as error:
        fail("utility-arguments", f"Malformed utility arguments: {error}", block)
    return values[1:]


@dataclass(frozen=True, slots=True)
class StartMacro:
    csv_path: str
    continue_on_error: bool

    @classmethod
    def from_block(cls, block: ClassifiedBlock) -> StartMacro:
        args = utility_arguments(block)
        if len(args) not in {1, 2} or not args[0]:
            fail("macro-arguments", "START-MACRO needs a CSV path and optional Y/N flag.", block)
        flag = args[1].upper() if len(args) == 2 else "N"
        if flag not in {"Y", "N"}:
            fail("macro-arguments", "ContinueOnError must be Y or N.", block)
        return cls(args[0], flag == "Y")


@dataclass(frozen=True, slots=True)
class IfThen:
    lhs: str
    op: str
    rhs: str

    @classmethod
    def from_block(cls, block: ClassifiedBlock) -> IfThen:
        args = utility_arguments(block)
        if len(args) not in {3, 7} or any(args[3:]):
            fail(
                "condition-arguments",
                "Only one three-argument IF-THEN comparison is supported.",
                block,
            )
        if not all(args[:3]) or args[1].upper() not in {
            "EQ",
            "EQS",
            "NE",
            "NES",
            "LE",
            "LES",
            "GE",
            "GES",
            "LT",
            "LTS",
            "GT",
            "GTS",
            "BT",
            "BTS",
            "NBT",
            "NBTS",
        }:
            fail(
                "condition-arguments", "Missing operand or unsupported comparison operator.", block
            )
        return cls(args[0], args[1].upper(), args[2])


MacroControlPayload = StartMacro | IfThen


@dataclass(frozen=True, slots=True)
class ScopeNode:
    scope_id: int
    kind: Literal["program", "macro", "if", "leaf"]
    start_index: int
    end_index: int
    children: tuple[ScopeNode, ...]
    block_index: int | None
    control_payload: MacroControlPayload | None
