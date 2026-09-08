from __future__ import annotations

import argparse
import sys
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
sys.path.insert(0, str(ROOT / "src"))

from vg2c_ui.api.contracts import render_typescript_contracts
TARGET = ROOT / "src/vg2c_ui/frontend/src/contracts.generated.ts"


def main() -> int:
    parser = argparse.ArgumentParser()
    parser.add_argument("--check", action="store_true")
    args = parser.parse_args()
    generated = render_typescript_contracts()
    if args.check:
        current = TARGET.read_text(encoding="utf-8") if TARGET.is_file() else ""
        if current != generated:
            print(f"Generated contracts are stale: {TARGET}")
            return 1
        return 0
    TARGET.write_text(generated, encoding="utf-8")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
