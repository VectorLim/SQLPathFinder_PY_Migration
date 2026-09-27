from __future__ import annotations

import os
import subprocess
import sys
import tempfile
import time
from pathlib import Path

from scripthost_portable import PortableScriptHostRuntime

DELIM = "<---- New Query ---->"


def _script(output: Path) -> str:
    return (
        "<OPTIONS>\n"
        "/WRITE-FILE=Y\n"
        f"/CSV={output}\n"
        "</OPTIONS>\n"
        "benchmark"
    )


def _child_env(repo_root: Path) -> dict[str, str]:
    env = dict(os.environ)
    env["PYTHONPATH"] = os.pathsep.join(
        [str(repo_root / "src"), str(repo_root), env.get("PYTHONPATH", "")]
    )
    return env


def main() -> None:
    repo_root = Path(__file__).resolve().parents[2]
    iterations = 3
    with tempfile.TemporaryDirectory(prefix="scripthost-benchmark-") as temp:
        root = Path(temp)
        runtime = PortableScriptHostRuntime()
        runtime.run_text(_script(root / "warmup.txt"), root)

        started = time.perf_counter()
        for index in range(iterations):
            runtime.run_text(_script(root / f"inproc-{index}.txt"), root)
        inproc = time.perf_counter() - started

        child_code = (
            "from pathlib import Path; "
            "from scripthost_portable import PortableScriptHostRuntime; "
            "import sys; "
            "root=Path(sys.argv[1]); out=Path(sys.argv[2]); "
            "text='<OPTIONS>\\n/WRITE-FILE=Y\\n/CSV='+str(out)+'\\n</OPTIONS>\\nbenchmark'; "
            "raise SystemExit(0 if PortableScriptHostRuntime().run_text(text, root) else 1)"
        )
        started = time.perf_counter()
        for index in range(iterations):
            result = subprocess.run(
                [
                    sys.executable,
                    "-c",
                    child_code,
                    str(root),
                    str(root / f"process-{index}.txt"),
                ],
                cwd=repo_root,
                env=_child_env(repo_root),
                stdout=subprocess.DEVNULL,
                stderr=subprocess.PIPE,
                text=True,
                timeout=30,
                check=False,
            )
            if result.returncode:
                raise RuntimeError(result.stderr)
        isolated = time.perf_counter() - started

    inproc_ms = inproc * 1000 / iterations
    isolated_ms = isolated * 1000 / iterations
    print(f"same_process_ms_per_job={inproc_ms:.1f}")
    print(f"process_isolated_ms_per_job={isolated_ms:.1f}")
    print(f"estimated_process_boundary_overhead_ms={max(0.0, isolated_ms - inproc_ms):.1f}")


if __name__ == "__main__":
    main()
