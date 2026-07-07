from __future__ import annotations

"""Temporary helper script to inspect Stage 1 -> Stage 4 intermediate outputs.

Usage examples:
  python tests/tmp_show_pipeline_progress.py --fixture tests/fixtures/actual_script.txt
  python tests/tmp_show_pipeline_progress.py --fixture tests/fixtures/sql_script.txt --oasys-schema OASYS_OWN
"""

import argparse
import sys
from collections import Counter
from pathlib import Path

# Allow running as a standalone script from repo root
ROOT = Path(__file__).resolve().parents[1]
SRC = ROOT / "src"
if str(SRC) not in sys.path:
    sys.path.insert(0, str(SRC))

from vg2c.dataflow import analyze
from vg2c.dispatch import dispatch
from vg2c.dispatch.models import DispatchConfig
from vg2c.frontend import classify, parse
from vg2c.resolver import resolve


def _preview(text: str, max_len: int = 100) -> str:
    one_line = " ".join(text.split())
    if len(one_line) <= max_len:
        return one_line
    return one_line[: max_len - 3] + "..."


def _print_diag_summary(title: str, diags) -> None:
    by_severity = Counter(d.severity for d in diags)
    by_code = Counter(d.code for d in diags)

    print(f"\n[{title}] diagnostics: {len(diags)} total")
    print(
        "  by severity: "
        + ", ".join(f"{k}={v}" for k, v in sorted(by_severity.items()))
        if by_severity
        else "  by severity: none"
    )

    if by_code:
        print("  top codes:")
        for code, count in by_code.most_common(10):
            print(f"    - {code}: {count}")


def _iter_scope(node, depth: int = 0):
    yield depth, node
    for child in node.children:
        yield from _iter_scope(child, depth + 1)


def main() -> None:
    parser = argparse.ArgumentParser(
        description="Show intermediate pipeline outputs for a fixture"
    )
    parser.add_argument(
        "--fixture",
        type=Path,
        default=Path("tests/fixtures/actual_script.txt"),
        help="Path to fixture script file",
    )
    parser.add_argument(
        "--oasys-schema",
        default="",
        help="Optional schema name used by Stage 4 for @OASYSSCHEMA@ substitution",
    )
    parser.add_argument(
        "--max-items",
        type=int,
        default=8,
        help="Max items printed per section",
    )
    args = parser.parse_args()

    fixture = args.fixture
    if not fixture.exists():
        raise SystemExit(f"Fixture not found: {fixture}")

    text = fixture.read_text(encoding="utf-8", errors="replace")

    print("=" * 88)
    print(f"Fixture: {fixture}")
    print(f"Chars: {len(text):,}")
    print("=" * 88)

    # Stage 1a: parse
    parsed, pdiag = parse(text, source=fixture)
    print(f"\n[Stage 1A: parse] blocks={len(parsed)}")
    for b in parsed[: args.max_items]:
        print(
            f"  - idx={b.index:>4} lines={b.span.start_line}-{b.span.end_line} "
            f"opts={len(b.options.pairs):>2} body='{_preview(b.body)}'"
        )
    _print_diag_summary("Stage 1A: parse", pdiag)

    # Stage 1b: classify
    classified, cdiag = classify(parsed)
    kind_counts = Counter(b.kind.value for b in classified)
    print(f"\n[Stage 1B: classify] blocks={len(classified)}")
    for kind, count in sorted(kind_counts.items()):
        print(f"  - {kind}: {count}")
    _print_diag_summary("Stage 1B: classify", cdiag)

    # Stage 2: resolve
    resolved = resolve(classified, diagnostics=[*pdiag, *cdiag])
    print(f"\n[Stage 2: resolve] blocks={len(resolved.blocks)}")

    scope_nodes = list(_iter_scope(resolved.scope_tree))
    print(f"  scope_nodes={len(scope_nodes)}")
    for depth, node in scope_nodes[: args.max_items]:
        indent = "  " * depth
        print(
            f"  {indent}- scope_id={node.scope_id} kind={node.kind} "
            f"range={node.start_index}-{node.end_index}"
        )

    all_sql_calls = sum(len(b.sql_macro_calls) for b in resolved.blocks)
    print(f"  sql_macro_calls={all_sql_calls}")

    # Stage 2 diagnostics are cumulative from parse+classify input
    stage2_new_diags = resolved.diagnostics[len(pdiag) + len(cdiag) :]
    _print_diag_summary("Stage 2: resolve (new-only)", stage2_new_diags)

    # Stage 3: analyze
    analyzed = analyze(resolved)
    print(
        f"\n[Stage 3: analyze] producers={len(analyzed.producers)} consumers={len(analyzed.consumers)} edges={len(analyzed.edges)}"
    )
    analyzed_sql_calls = sum(len(b.sql_macro_calls) for b in analyzed.resolved.blocks)
    print(f"  sql_macro_calls={analyzed_sql_calls}")
    for edge in analyzed.edges[: args.max_items]:
        p = edge.producer.block_index if edge.producer is not None else None
        print(
            f"  - path={edge.csv_path} producer_idx={p} consumer_idx={edge.consumer.block_index} "
            f"relation={edge.scope_relation} order_ok={edge.order_ok}"
        )

    stage3_new_diags = analyzed.diagnostics[len(resolved.diagnostics) :]
    _print_diag_summary("Stage 3: analyze (new-only)", stage3_new_diags)

    # Stage 4: dispatch
    cfg = DispatchConfig(oasys_schema=args.oasys_schema)
    dispatched = dispatch(analyzed, config=cfg)
    print(f"\n[Stage 4: dispatch] dispatched_blocks={len(dispatched.dispatched)}")
    for d in dispatched.dispatched[: args.max_items]:
        target = d.reader_target
        print(
            f"  - idx={d.block_index} dialect={d.dialect} db={target.database_arg} "
            f"record={target.record_name}@{target.record_version} "
            f"node='{target.node}' instance='{target.instance}'"
        )
        print(f"    sql='{_preview(d.rewritten_sql, max_len=140)}'")

    stage4_new_diags = dispatched.diagnostics[len(analyzed.diagnostics) :]
    _print_diag_summary("Stage 4: dispatch (new-only)", stage4_new_diags)

    print("\nDone.")


if __name__ == "__main__":
    main()
