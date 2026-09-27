"""Retired migration entrypoint: jobs must use the original ScriptHost worker."""

import sys


def main() -> int:
    print(
        "vg2c_new is migration/reference code and cannot execute jobs. "
        "Use scripthost_portable.worker.run_job(ScriptHostJob(...)); "
        "each job runs original ScriptHost in a fresh child process.",
        file=sys.stderr,
    )
    return 2


if __name__ == "__main__":
    raise SystemExit(main())
