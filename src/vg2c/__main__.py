import argparse
from pathlib import Path

from vg2c import CompileError, translate


def main() -> None:
    parser = argparse.ArgumentParser(description="Compile supported VG2 jobs to clean Python.")
    parser.add_argument("inputs", type=Path, nargs="+")
    parser.add_argument("--out-dir", type=Path)
    args = parser.parse_args()
    for source in args.inputs:
        try:
            output = translate(source, args.out_dir)
        except (CompileError, OSError, ValueError) as error:
            parser.exit(1, f"{error}\n")
        print(output)


if __name__ == "__main__":
    main()
