from __future__ import annotations

import argparse
import os
from pathlib import Path


def main() -> None:
    parser = argparse.ArgumentParser(description="Run the local VG2 visual editor")
    parser.add_argument("--data-dir", default=os.getenv("VG2C_DATA_DIR", "/data"))
    parser.add_argument("--host", default="0.0.0.0")
    parser.add_argument("--port", type=int, default=8765)
    args = parser.parse_args()

    import uvicorn

    from vg2c_ui.app import create_app

    uvicorn.run(create_app(Path(args.data_dir)), host=args.host, port=args.port)


if __name__ == "__main__":
    main()
