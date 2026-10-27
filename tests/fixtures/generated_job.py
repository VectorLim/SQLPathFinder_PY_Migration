"""Observe per-run state without changing generated entrypoint contracts."""


def capture_jobs(namespace):
    original = namespace["JobRuntime"]

    class CapturedJob(original):
        @classmethod
        def from_script(cls, *args, **kwargs):
            job = super().from_script(*args, **kwargs)
            namespace["_job"] = job
            return job

    namespace["JobRuntime"] = CapturedJob
