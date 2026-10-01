"""imserverless entry point: the base image calls handle() for each trigger."""

from __future__ import annotations

import json
import os
from datetime import UTC, datetime

from scripthost_portable.worker import ScriptHostJob, run_job


def _log(level: str, message: str, **fields: object) -> None:
    record = {
        "@timestamp": datetime.now(UTC).isoformat(),
        "ecs.version": "1.6.0",
        "log.level": level,
        "message": message,
        **fields,
    }
    print(json.dumps(record, default=str), flush=True)


def handle(job_id=None, context=None, header=None, body=None) -> tuple[bool, str, str]:
    script_path = os.getenv("SCRIPT_PATH")
    _log("info", "reaching inside the handle function", job_id=job_id, script_path=script_path)
    if not script_path:
        message = "SCRIPT_PATH environment variable is not set."
        _log("error", message, job_id=job_id)
        return False, message, message

    workdir = os.getenv("WORKDIR") or os.path.dirname(script_path) or "."
    timeout = os.getenv("JOB_TIMEOUT")
    result = run_job(
        ScriptHostJob(working_directory=workdir, script_path=script_path),
        timeout=float(timeout) if timeout else None,
    )
    _log(
        "info" if result.success else "error",
        result.message,
        job_id=job_id,
        env_mode=os.getenv("ENV_MODE"),
        script_path=script_path,
        workdir=workdir,
        success=result.success,
        error_category=result.error_category,
        exit_code=result.exit_code,
        generated_outputs=list(result.generated_outputs),
        stderr=result.stderr[-4000:],
    )
    return result.success, result.message, result.message


if __name__ == "__main__":
    handle()
