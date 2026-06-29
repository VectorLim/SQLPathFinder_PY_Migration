# Example: Adding a New Dialect Handler

This demonstrates how to add a new SQL dialect to the dispatch system.

## Step 1: Create a new dialect handler file

```python
# src/vg2c/dispatch/dialects/xeus.py
from __future__ import annotations

from vg2c.dispatch.base import DialectHandler
from vg2c.dispatch.models import DispatchConfig
from vg2c.dispatch.registry import register
from vg2c.frontend.models import Diagnostic, Kind, SourceSpan


@register  # <-- This decorator automatically registers the handler
class XeusDialect(DialectHandler):
    """Handler for Oracle XEUS dialect."""
    
    # Define the dialect configuration
    dialect = "oracle_xeus"
    kind = Kind.XEUS_READ  # Would need to add this to Kind enum
    reader_class_hint = "OracleReader"
    database_arg = "XEUS"
    schema_placeholder = None  # No schema placeholder for this dialect

    @classmethod
    def matches_signals(cls, node: str, engine: str, oledb: str) -> bool:
        """Check if option signals indicate XEUS dialect."""
        node_u = node.upper()
        engine_u = engine.upper()
        oledb_u = oledb.upper()
        return (engine_u == "VA" or oledb_u == "SQLPLUS") and "XEUS" in node_u

    @classmethod
    def substitute(
        cls,
        body: str,
        config: DispatchConfig | None,
        span: SourceSpan | None,
        block_index: int,
    ) -> tuple[str, list[Diagnostic]]:
        """Perform any dialect-specific SQL transformations."""
        # Example: XEUS might require different schema handling
        # For now, just return the body unchanged
        return body, []
```

## Step 2: Import the handler in dialects/__init__.py

```python
# src/vg2c/dispatch/dialects/__init__.py
from vg2c.dispatch.dialects.aries import AriesDialect
from vg2c.dispatch.dialects.mars import MarsDialect
from vg2c.dispatch.dialects.oasys import OasysDialect
from vg2c.dispatch.dialects.sqlite import SqliteDialect
from vg2c.dispatch.dialects.xeus import XeusDialect  # <-- Add this

__all__ = ["AriesDialect", "MarsDialect", "OasysDialect", "SqliteDialect", "XeusDialect"]
```

## Step 3: Add the Kind enum value (if needed)

```python
# src/vg2c/frontend/models.py
class Kind(str, Enum):
    MARS_READ = "MARS_READ"
    OASYS_READ = "OASYS_READ"
    ARIES_READ = "ARIES_READ"
    XEUS_READ = "XEUS_READ"  # <-- Add this
    SQLITE_QUERY = "SQLITE_QUERY"
    # ... other kinds
```

## Step 4: Add classification rule in classifier (if needed)

```python
# src/vg2c/frontend/classifier.py
def _rule_xeus(block: ParsedBlock, opts: Mapping[str, str]) -> Kind | None:
    """XEUS rule."""
    if opts.get("ENGINE", "").upper() == "VA":
        node = opts.get("NODE", "").upper()
        if "XEUS" in node or node.endswith(".XEUS"):
            return Kind.XEUS_READ
    return None
```

## That's it!

The new dialect is now fully integrated:
- The dispatch module automatically discovers it via the `@register` decorator
- It's available in the `HANDLERS` registry
- The dispatch() function will route XEUS blocks to the XeusDialect handler
- All dialect-specific logic is encapsulated in one file

## Key Benefits

1. **Single file per dialect** - All logic for a dialect lives in one place
2. **Name → Class mapping** - The registry provides automatic discovery
3. **Common interface** - All handlers implement the same DialectHandler ABC
4. **No central switch statements** - No need to edit dispatch/__init__.py
5. **Easy to test** - Each handler can be tested independently
