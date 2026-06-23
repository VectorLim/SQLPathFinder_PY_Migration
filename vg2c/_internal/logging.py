from __future__ import annotations

import logging


def get_logger(name: str) -> logging.Logger:
    """Return a package-scoped logger without attaching handlers."""
    return logging.getLogger(name)
