from __future__ import annotations

import logging
from typing import ClassVar

from vg2c.utilities._base import UtilitySpec


class Logger(UtilitySpec):
    """Shared logger utility used by translator code and generated scripts."""

    utility_name = "logger"
    CRITICAL: ClassVar[int] = logging.CRITICAL
    ERROR: ClassVar[int] = logging.ERROR
    WARNING: ClassVar[int] = logging.WARNING
    INFO: ClassVar[int] = logging.INFO
    DEBUG: ClassVar[int] = logging.DEBUG
    NOTSET: ClassVar[int] = logging.NOTSET

    @classmethod
    def basicConfig(
        cls,
        *,
        level: int | str = logging.INFO,
        format: str = "%(asctime)s | %(levelname)-8s | %(name)s | %(message)s",
        datefmt: str = "%Y-%m-%d %H:%M:%S",
    ) -> None:
        logging.basicConfig(level=level, format=format, datefmt=datefmt)

    @classmethod
    def getLogger(cls, name: str | None = None) -> logging.Logger:
        return logging.getLogger(name)


__all__ = ["Logger"]
