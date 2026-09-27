# ScriptHost utility portability matrix

Audit starting point: `fab2c5f9583177f706be41a9eb07881cbe0d25c3`.
Original ScriptHost owns parsing, task selection, arguments, globals and utility semantics.
One fresh worker child executes each job. No duplicate is deletion-approved without
original-path execution evidence on Linux. Source-only candidates are not Linux passes.

Baseline Windows/Python 3.14.6: 89 passed, 2 Linux-characterization failures.
Linux evidence and capability rows are populated as the audit executes.
