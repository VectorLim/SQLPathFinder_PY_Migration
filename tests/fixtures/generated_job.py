"""Observe per-run state without changing generated entrypoint contracts."""

import ast


def sql_keyword_names(source):
    return {
        keyword.arg
        for node in ast.walk(ast.parse(source))
        if isinstance(node, ast.Call)
        and isinstance(node.func, ast.Attribute)
        and isinstance(node.func.value, ast.Name)
        and node.func.value.id == "job"
        and node.func.attr == "sql"
        for keyword in node.keywords
    }


def capture_jobs(namespace):
    original = namespace["JobRuntime"]

    class CapturedJob(original):
        @classmethod
        def from_script(cls, *args, **kwargs):
            job = super().from_script(*args, **kwargs)
            namespace["_job"] = job
            return job

    namespace["JobRuntime"] = CapturedJob
