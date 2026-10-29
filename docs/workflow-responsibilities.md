# Workflow projection responsibilities

`project_document()` remains the entry point for a single effective document.
It applies edits, projects operations, orders file states, and assembles resources,
diagnostics and reorder targets. Document contracts, workspace aggregation, and
reorder analysis are consolidated in `vg2c.workflow`.

| Responsibility | Authoritative owner |
| --- | --- |
| Operation bindings and file-effect declarations | `semantic/operations.py` and utility `@emittable` metadata |
| Binding declarations to file endpoints | `dataflow/file_effects.py::bind_effect_definitions` |
| SQL/report source-derived file effects and scope context | `dataflow/projection.py` |
| File-state ordering and dependency identities | `dataflow/file_effects.py::order_file_effects` |
| Shared document and workspace data contracts | `workflow.py` |
| Single-document assembly and resource inventory | `workflow.py` |
| Cross-document producers, links and conflicts | `workflow.py` |
| Shared conservative reorder analysis | `workflow.py` |

## Adding an operation

Declare its `FileEffectDefinition` values alongside its bindings. Utility operation
declarations are copied into `WorkflowOperation.file_effects`; native controls and
composite operations declare the same metadata when projected. No additional
operation-kind branch is needed in document assembly.

Each definition names input/output bindings, path bases and an effect key. The key
and binding names participate in stable endpoint IDs. `conditional` and `in_loop`
can conservatively strengthen the enclosing scope flags, as needed for CSR result
writes and chunk materialization. An empty declaration tuple means no file effects;
`None` means effects are unknown. Embedded Python explicitly declares unknown effects.

`bind_effect_definitions()` binds emitted parameters and native semantic bindings
using their effective values. SQL file-list expressions and
HTML templates still require their canonical source parsers. Those enrichments are
kept in dataflow projection; HTML parsing uses `HtmlReport.parse_options()` and
`HtmlReport.split_layout()` rather than copying their parsing rules.

## Redundancy removed and validation

- Removed the separate macro, chunk, row-count and CSR effect builders and their
  duplicate endpoint-building helper from document assembly.
- Consolidated reorder safety and scope traversal in `workflow.py`; the separate
  scope-permutation API is retired with the UI.
- Removed the unused first binding collection in semantic projection.
- Reused the SQL parser's identifier predicate in schema matching and removed the
  parser's no-op wrapper around `dataclasses.replace`.
- Corrected a separate pre-existing scope-flag lookup: flags are keyed by scope ID,
  not source block index. This changes derived conditional/loop availability where
  the old lookup selected a different scope; it does not change generated execution.

Before the scope correction, 20 captured fixture snapshots matched exactly after
the structural refactor, excluding only the new declaration fields. After the fix,
generated source/assets, operation identities, symbols and endpoint identities still
match all 20 snapshots. Derived workflow state changes in 5 inputs as expected.
Regression tests cover new operation kinds with declarations, scope flags and parsed
HTML output, alongside existing edits, workspace links, execution ordering and CSR tests.

The inventory covered owned workflow/compiler code, not generated jobs or decompiled
third-party implementations. Large modules are not split solely by line count;
unrelated editing, SQL parsing and report compilation behavior is outside this change.
The HTML directive readers were also compared: one normalizes line endings and
strips directive keys, while layout preparation preserves body line endings and
does not strip keys. Their differing contracts are retained rather than merging
them by appearance. No further proven dead code was found in editing, emission
helpers or operation registration during this bounded review.
