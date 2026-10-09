"""Values for native generated loops; these helpers never execute their bodies."""

from __future__ import annotations

from vg2c.runtime.csv_io import _CsvIO
from vg2c.runtime.values import job_path


def for_values(start, end, step, suffix, reverse=None, version=None):
    # Adapted from scripthost-utilities-decompiled/SPSQL3_py/SPFLib/SPFSQL3.py:
    # ForLoopTask.executeTaskCommand (12701-12874), Substitute_Loop_Counter (12917-12926).
    # Keep the supplied-version quirk and signed step tokens; test_control_parity.py.
    if not start.isdigit() or not end.isdigit() or not suffix or int(start) > int(end):
        raise ValueError("Invalid FOR start/end/suffix")
    try:
        delta = int(step)
    except ValueError:
        delta = float(step)

    def row(first, last, stride, counter):
        return {
            f"SPF-START-{suffix}".upper(): str(first),
            f"SPF-END-{suffix}".upper(): str(last),
            f"SPF-STEP-{suffix}".upper(): str(stride),
            f"SPF-STEP-{suffix}-INT".upper(): str(int(stride)),
            f"SPF-LOOP-CTR-{suffix}".upper(): str(counter),
            f"SPF-LOOP-CTR-{suffix}-INT".upper(): str(int(counter)),
        }

    if version is not None:
        yield row(int(start), int(end), int(step), int(start))
        return
    backwards = reverse is None or reverse.upper() == "Y"
    first, last = (int(end), int(start)) if backwards else (int(start), int(end))
    increment = -float(delta) if backwards else float(delta)
    count = 0 if delta == 0 else int((int(end) - int(start)) / delta) + 1
    if delta and last > first + count * float(delta):
        count += 1
    counter = float(first)
    for _ in range(count):
        if ((not backwards and counter + increment > last and first != 0)
                or (backwards and counter + increment < last and last != 0)):
            increment = last - counter
        yield row(first, last, -increment, counter)
        counter += increment


def site_values(nodes):
    # Adapted from SPFLib/SPFSQL3.py:SiteLoopTask (12965-13039), and
    # SPFUtilities/utils.py:Replace_Special_Chars (5122-5128); keep comma-list order.
    if not nodes:
        raise ValueError("Missing Site Argument")
    for node in nodes.split(","):
        yield {"SPF-SITE": node,
               "SPF-SITE-FOR-FILE-NAME": "".join("_" if char in '?/|<>"*{};[].\\/' else char
                                                for char in node)}


def csv_chunks(input_file, chunk_file, chunk_size, *, workdir):
    # Adapted from SPFLib/SPFSQL3.py:RunLoopTask (12608-12669); missing input skips.
    source = job_path(input_file, workdir)
    if not source.exists():
        return
    if chunk_size <= 0:
        raise ValueError("RUN chunk size must be positive")
    output = job_path(chunk_file, workdir)
    if output.resolve() == source.resolve():
        raise ValueError("RUN chunk output cannot overwrite its input")
    try:
        yield from _CsvIO(workdir=workdir).iter_chunks(str(source), str(output), chunk_size)
    finally:
        output.unlink(missing_ok=True)
