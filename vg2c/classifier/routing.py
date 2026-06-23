from __future__ import annotations

from vg2c.classifier.model import EngineKind, EngineTarget, RecordRef


def route(options: dict[str, str], body: str, record: RecordRef | None) -> EngineTarget:
    """Determine the execution engine for a block."""
    engine_opt = options.get("ENGINE", "").strip().upper()
    oledb_opt = options.get("OLEDB", "").strip().upper()
    node_opt = options.get("NODE", "").strip()

    # SQLite
    if engine_opt == "SQLITE" or oledb_opt == "SQLITE":
        return EngineTarget(
            kind=EngineKind.SQLITE,
            node=node_opt or None,
            schema_hint=None,
            reason="ENGINE=SQLITE or OLEDB=SQLITE",
        )

    # Aries detection
    if node_opt and "ARIES" in node_opt.upper():
        return EngineTarget(
            kind=EngineKind.ARIES,
            node=node_opt,
            schema_hint=None,
            reason="NODE contains ARIES",
        )

    # Oracle MARS vs OASYS detection
    if engine_opt == "VA" or oledb_opt in {"SQLPLUS", "ORACLE"}:
        # Check if OASYS is in NODE
        if node_opt and "OASYS" in node_opt.upper():
            return EngineTarget(
                kind=EngineKind.ORACLE_OASYS,
                node=node_opt,
                schema_hint="@OASYSSCHEMA@",
                reason="NODE contains OASYS",
            )

        # Check body for OASYS schema hints
        if body and "@OASYSSCHEMA@" in body:
            return EngineTarget(
                kind=EngineKind.ORACLE_OASYS,
                node=node_opt,
                schema_hint="@OASYSSCHEMA@",
                reason="body contains @OASYSSCHEMA@",
            )

        # Check record name for OASYS prefix (P_*)
        if record and record.name.startswith("P_"):
            return EngineTarget(
                kind=EngineKind.ORACLE_OASYS,
                node=node_opt,
                schema_hint="@OASYSSCHEMA@",
                reason="RECORD starts with P_",
            )

        # Check body for MARS schema hints
        if body and "@[]@" in body:
            return EngineTarget(
                kind=EngineKind.ORACLE_MARS,
                node=node_opt,
                schema_hint="@[]@",
                reason="body contains @[]@",
            )

        # Check record name for MARS prefix (F_*)
        if record and record.name.startswith("F_"):
            return EngineTarget(
                kind=EngineKind.ORACLE_MARS,
                node=node_opt,
                schema_hint="@[]@",
                reason="RECORD starts with F_",
            )

        # Default Oracle to MARS
        return EngineTarget(
            kind=EngineKind.ORACLE_MARS,
            node=node_opt,
            schema_hint="@[]@",
            reason="ENGINE=VA or OLEDB=SQLPlus/Oracle (defaulting to MARS)",
        )

    # No engine detected
    return EngineTarget(
        kind=EngineKind.NONE,
        node=None,
        schema_hint=None,
        reason="no engine detected",
    )
