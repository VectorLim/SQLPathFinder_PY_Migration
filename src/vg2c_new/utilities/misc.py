from __future__ import annotations

import html
import re
import xml.etree.ElementTree as ET
from datetime import datetime
from pathlib import Path
from typing import TYPE_CHECKING

import pandas as pd

from vg2c_new.paths import resolve_path, working_directory_for
from vg2c_new.utilities.base import Utility
from vg2c_new.utilities.csv import CsvUtility

if TYPE_CHECKING:
    from vg2c_new.model import Command
    from vg2c_new.runtime import RuntimeState


class EchoUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        print(" ".join(state.substitute(v) for v in command.arguments))


class CsvToHtmlUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Direct portable port of ScriptHost CSVToHTMLTask current table rendering."""
        args = [state.substitute(v) for v in command.arguments]
        if not args:
            return
        source = _path(command, state, args[0])
        output = _path(
            command, state, args[1] if len(args) > 1 and args[1] else "SQLPathFinder.html"
        )
        caption = args[2] if len(args) > 2 else ""
        frame = CsvUtility.read_dataframe(source)
        output.parent.mkdir(parents=True, exist_ok=True)
        with output.open("w", encoding="utf-8") as h:
            h.write(
                "<html><head><title>" + html.escape(str(source)) + "</title></head><body><table>\n"
            )
            if caption:
                h.write(f"<caption><h3>{html.escape(caption)}</h3></caption>\n")
            h.write(
                "<tr>"
                + "".join(
                    f"<th>{html.escape(str(c).replace('_', ' '))}</th>" for c in frame.columns
                )
                + "</tr>\n"
            )
            for _, row in frame.iterrows():
                h.write(
                    "<tr>" + "".join(f"<td>{html.escape(str(v))}</td>" for v in row) + "</tr>\n"
                )
            h.write("</table></body></html>\n")


class CsvToXmlUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Direct portable port of ScriptHost CSVToXMLTask current DOM shape."""
        args = [state.substitute(v) for v in command.arguments]
        if not args:
            return
        source = _path(command, state, args[0])
        output = _path(
            command, state, args[1] if len(args) > 1 and args[1] else "SQLPathFinder.xml"
        )
        style = args[2] if len(args) > 2 else ""
        frame = CsvUtility.read_dataframe(source)
        root = ET.Element(
            "Main", Created=datetime.now().strftime("%Y-%m-%d %H:%M:%S"), Source=str(source)
        )
        names = [re.sub(r"[^\w]", "_", str(c)) for c in frame.columns]
        for _, row in frame.iterrows():
            item = ET.SubElement(root, "Item")
            for name, value in zip(names, row, strict=False):
                ET.SubElement(item, name).text = str(value).strip() or "."
        output.parent.mkdir(parents=True, exist_ok=True)
        ET.ElementTree(root).write(output, encoding="utf-8", xml_declaration=True)
        if style:
            text = output.read_text(encoding="utf-8")
            declaration, rest = text.split("?>", 1)
            output.write_text(
                declaration + "?>\n" + f'<?xml-stylesheet type="text/xsl" href="{style}"?>' + rest,
                encoding="utf-8",
            )


class XmlToCsvUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        args = [state.substitute(v) for v in command.arguments]
        if len(args) < 2:
            raise ValueError("XMLToCSV requires XML input and CSV output.")
        root = ET.parse(_path(command, state, args[0])).getroot()
        records = [{child.tag: child.text or "" for child in list(item)} for item in list(root)]
        CsvUtility.write_dataframe(pd.DataFrame(records), _path(command, state, args[1]))


class StackDataUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Amended port of ScriptHost StackDataTask file union/sort behavior.
        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: StackDataTask.execute/getAllColsAndDelimFromFilesList.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT. Preserved: case-insensitive union, source order and SORT.
        Amendments: pandas only. Intentionally discarded: MemTable/UI output.
        """
        tables = state.substitute(command.option("TABLE", "") or "")
        output = command.option("CSV")
        if not tables or not output:
            raise ValueError("StackDataTask requires /TABLE and /CSV.")
        frames = []
        canonical = {}
        order = []
        for item in [x.strip() for x in tables.split(",") if x.strip()]:
            frame = CsvUtility.read_dataframe(_path(command, state, item))
            rename = {}
            for column in frame.columns:
                key = str(column).casefold()
                if key not in canonical:
                    canonical[key] = str(column).upper()
                    order.append(canonical[key])
                rename[str(column)] = canonical[key]
            frames.append(frame.rename(columns=rename))
        result = (
            pd.concat(
                [f.reindex(columns=order, fill_value="") for f in frames],
                ignore_index=True,
                sort=False,
            ).fillna("")
            if frames
            else pd.DataFrame(columns=order)
        )
        sort = state.substitute(command.option("SORT", "") or "")
        if sort:
            cols = []
            asc = []
            for spec in [s.strip() for s in sort.split(",") if s.strip()]:
                m = re.match(r"\[?([^\]]+)\]?\s+(ASC|DESC)(-1)?", spec, re.I)
                if not m:
                    continue
                col = canonical.get(m.group(1).casefold(), m.group(1))
                if m.group(3):
                    result[col] = pd.to_numeric(result[col], errors="coerce")
                cols.append(col)
                asc.append(m.group(2).upper() == "ASC")
            if cols:
                result = result.sort_values(cols, ascending=asc, kind="stable")
        CsvUtility.write_dataframe(result, _path(command, state, state.substitute(output)))


def _path(command: Command, state: RuntimeState, value: str) -> Path:
    return resolve_path(value, state, base=working_directory_for(command.option("WORKDIR"), state))
