# Stage 1 — Thin Script API Over the Original Runtime

Stage 1 lets ordinary Python drive the original ScriptHost backend. It includes no compiler, emitter, editor, embedded runtime, or historical utility expansion. The only compatibility targets are `ICMPCS.txt` and `output/aed-migration/CSR_IAM_v2.aed.txt`.

## Public API

```python
from scripthost_portable.script_api import aed, macros, query, reports, utilities


def run():
    if macros.load_csv("configsets.csv"):
        query.run(
            sql="SELECT lot AS LOT FROM measurements WHERE value > 0",
            tables=macros["SOURCE"],
            output="candidates.csv",
            quote_csv=True,
        )
        utilities.rows_in_file("candidates.csv", "SIGNAL")
        if macros.compare("SIGNAL", "GT", "0"):
            aed.process("candidates.csv")
```

Launch with `python -m scripthost_portable.launcher job.py --workdir <directory>`.

The launcher selects Python for `.py` files. `ScriptHostJob.python_path` is the explicit worker input; existing `script_path`/`script_text` remain VG2 inputs. Exactly one input is required. Python files must define a callable `run()`. They import only the five public facade objects. There is no public session API.

## Hidden ownership

`runtime._execution()` reuses existing manager construction, command-line options and cwd handling for both VG2 and Python. `run_python()` binds that manager privately during module loading and `run()`, calls original final cleanup and restores cwd. The binding is removed and its macro table dropped on exit, including exceptions. Nested Python execution is rejected before a second manager can disturb the current job.

All facade calls resolve that same original manager. `GetQuery()` still creates original task classes; those tasks share `SPFGlobals` state. The manager alone does not isolate globals or the environment. One fresh child per job remains required; in-process entrypoints are not thread-safe. Environment mutations retain original behavior and disappear when the worker exits.

## Macros and utilities

```python
macros.load_csv(path, continue_on_error=False)
macros["NAME"]
macros.get(name)
macros.set(name, value)
macros.substitute(text)
macros.compare(lhs, operator, rhs)
utilities.rows_in_file(path, variable)
```

- Loading executes original `StartMacroTask`, which uses `MemTable.LoadFromFile(..., EANImport=False)`. Only data row 1 supplies macros; rows 2..N are not iterations. One successful CSV scope per job is supported.
- A header-only CSV returns `False`. A missing CSV raises `FileNotFoundError`, or returns `False` with `continue_on_error=True`.
- **Discovery differing from the earlier plan:** a zero-byte file has no header; the original loader raises `StopIteration` and its error handler exposes `IndexError`. The facade preserves this failure.
- Ordinary names resolve through original `Substitute_Macro`, preserving case-insensitive lookup, empty values and missing-name errors. Original `Substitute_Global_Var` handles special/CLI tokens and environment substitution first, as in the original entrypoint.
- **Bracket behavior:** the original CSV importer normalizes `[SITE]` to `(SITE)`, so `SITE` lookup then fails. The facade preserves this behavior.
- CSV macros and environment variables remain separate original namespaces. `macros.set("VALUE", "7")` delegates to original `setEnv`; read it with `macros["%VALUE%"]` or `macros.substitute("<<<%VALUE%>>>")`. It does not override an ordinary CSV column of the same name.
- Comparison executes original `IfThenTask`, preserving environment-name and `VAR(...)`/`ENV(...)` argument resolution and original `CompareVars` semantics.
- Row counting executes original `RowsInFileTask` (original `getRowCountFromFile` and `setEnv`) and returns its environment-visible count. Original count errors set `-1` and continue.

## Queries

```python
query.run(sql=..., output=..., engine="SQLite", node=None, tables=None, **options)
```

The facade internally constructs one options/body block and calls the existing manager's `GetQuery()` and original `task.execute()`. Original tasks still reach portability overrides and existing query transport. No query preprocessing, SQLite engine, Oracle client or output writer is reimplemented.

Only `SQLite` and `VA` engines and `SQLite`/`SQLPlus` OLEDB values are supported. The scoped readable names are:

| Python name | Original option |
| --- | --- |
| `node`, `engine`, `tables`, `output` | NODE, ENGINE, TABLE, CSV |
| `oledb`, `username`, `password` | OLEDB, UN, PW |
| `headers`, `unique_headers`, `quote_csv` | HEADERS, HEADERS_UNIQUE, QUOTECSV |
| `ct_rows`, `ct_value`, `ct_header`, `ct_array` | CTROW, CTVALUE, CTHEADER, CTARRAY |
| `record`, `reset`, `show_result`, `timestamp` | RECORD, RESET, T, TS |
| `delete`, `sqlite_types`, `instance`, `prompt` | DELETE, SQLITE_DT, INSTANCE, PROMPT-TEXT |
| `workdir`, `hadoop_server` | WORKDIR, HADOOP_SERVER_DEFAULT |

Unknown options fail explicitly. Newlines/block tokens in option values and legacy task blocks in SQL/template bodies are rejected. `quote_csv=True` requests the original CSV writer; it does not promise quotation of every field.

## Reports and AED

```python
reports.run(template, **options)
reports.defer(template, report_id=..., **options)
reports.layout(template, **options)
reports.delete(instance=None)
aed.process(path)
```

Report templates retain the original report data/layout format, without an options block. Options are `instance`, `prompt`, `app_server`, `outlook`, `json_only`, and `chart_instance`. Calls reach original HTML tasks with the same hidden manager and shared original report state. Templates must satisfy the original backend's layout requirements; abbreviated layouts are not repaired automatically.

AED delegates to existing `aed_api.process_candidates`. Python jobs use prepared CSV/environment inputs; Stage 1 does not infer AED bootstrap needs by scanning Python source or translate `prepare_job` into another configuration path. Deployment bootstrap/cutover and full-job differential parity belong to later work. External AED side effects are mocked in API tests.

## Validation and boundary

`tests/scripthost_portable/test_script_api.py` characterizes plain `run()`, one manager, original macro edge cases, row counting/comparison, local SQLite, controlled Oracle transport, report lifecycle, AED delegation, worker isolation and cwd/binding cleanup.

On Windows, installed native drivers can bypass fake readers. Run offline tests with `SCRIPTHOST_FORCE_PORTABLE_QUERY_TRANSPORT=1`; production driver selection is unchanged. Existing baseline failures must be reported separately from regressions.

Stage 1 excludes nested macro scopes, RUN-LOOP, historical utility coverage, new runtime engines, compatibility frameworks and Stage 2 code. The direct VG2 runtime remains the later parity oracle.
