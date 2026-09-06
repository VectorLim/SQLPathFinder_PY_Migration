from vg2c.dataflow.models import AnalyzedProgram
from vg2c.dispatch import dialects as _dialects  # noqa: F401
from vg2c.dispatch.base import DialectHandler
from vg2c.dispatch.filter_detector import detect_filters
from vg2c.dispatch.models import (
    DispatchedBlock,
    DispatchedProgram,
    ReaderSpec,
    ReaderTarget,
)
from vg2c.kind import Kind

__all__ = [
    "dispatch",
    "DispatchedBlock",
    "DispatchedProgram",
    "DialectHandler",
    "ReaderSpec",
    "ReaderTarget",
]


def dispatch(
    analyzed: AnalyzedProgram,
) -> DispatchedProgram:
    """Stage 4 entry point: resolve dialects, substitute schemas, and bind readers."""
    dispatched: list[DispatchedBlock] = []

    for block in analyzed.resolved.blocks:
        opts = block.resolved_options.lookup

        if block.kind is Kind.SQL_QUERY:
            handler = DialectHandler.derive_handler_from_signals(
                node=opts.get("NODE", ""),
                engine=opts.get("ENGINE", ""),
                oledb=opts.get("OLEDB", ""),
            )
        else:
            handler = DialectHandler.for_kind(block.kind)

        if handler is None and block.kind is Kind.UNKNOWN:
            handler = DialectHandler.derive_handler_from_signals(
                node=opts.get("NODE", ""),
                engine=opts.get("ENGINE", ""),
                oledb=opts.get("OLEDB", ""),
            )

        if handler is None:
            continue

        rewritten_sql = handler.substitute(body=block.resolved_body)
        reader_target = handler.build_reader_target(block)

        sqlite = block.kind is Kind.SQLITE_QUERY
        suffix = "sqlite_query" if sqlite else "sql_query"
        step_name = f"step_{block.index:04d}_{suffix}"
        sql_filters = detect_filters(rewritten_sql, step_name)

        dispatched.append(
            DispatchedBlock(
                resolved=block,
                reader=handler.reader,
                reader_kwargs=handler.reader_kwargs,
                reader_target=reader_target,
                rewritten_sql=rewritten_sql,
                step_name=step_name,
                sql_filters=tuple(sql_filters),
            )
        )

    return DispatchedProgram(
        analyzed=analyzed,
        dispatched=tuple(dispatched),
    )
