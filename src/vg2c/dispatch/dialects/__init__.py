from __future__ import annotations

# Import all dialect handlers to trigger registration
from vg2c.dispatch.dialects.aries import AriesDialect
from vg2c.dispatch.dialects.mars import MarsDialect
from vg2c.dispatch.dialects.oasys import OasysDialect
from vg2c.dispatch.dialects.sqlite import SqliteDialect

__all__ = ["AriesDialect", "MarsDialect", "OasysDialect", "SqliteDialect"]
