"""Focused checks for the CSR CSV row counts and conditional flow."""

import logging
import os
import tempfile
import unittest
from pathlib import Path
from types import SimpleNamespace
from unittest.mock import Mock, patch

import pandas as pd

import CSR_IAM as csr


class ConditionalFlowTests(unittest.TestCase):
    def test_sqlite_with_complete_dummy_data(self):
        source = Path(__file__).parent / "fixtures" / "csr_iam" / "PARMI_IPM_RAW.csv"
        ctx = Mock()
        csr.step_0004_sqlite_query(ctx)
        query = ctx.run_query.call_args.kwargs
        result = query["reader"].execute(query["sql"], [(str(source), "PARMI_IPM_RAW")])
        self.assertEqual(result["lot"].tolist(), ["DUMMY"])
        self.assertEqual(result["FlagLot"].tolist(), ["1"])
        with tempfile.TemporaryDirectory() as directory:
            ipm_data = Path(directory) / "IPM_Data.csv"
            result.to_csv(ipm_data, index=False)
            csr.step_0005_sqlite_query(ctx)
            query = ctx.run_query.call_args.kwargs
            signals = query["reader"].execute(query["sql"], [str(ipm_data)])
            self.assertEqual(signals["Lot_NCORisk"].tolist(), ["DUMMY"])

    def test_sqlite_reports_first_error_for_incomplete_dummy_data(self):
        source = Path(__file__).parent / "fixtures" / "csr_iam" / "PARMI_IPM_RAW.csv"
        with tempfile.TemporaryDirectory() as directory:
            incomplete = Path(directory) / "PARMI_IPM_RAW.csv"
            pd.read_csv(source).drop(columns=["height"]).to_csv(incomplete, index=False)
            ctx = Mock()
            csr.step_0004_sqlite_query(ctx)
            query = ctx.run_query.call_args.kwargs
            with self.assertRaisesRegex(RuntimeError, r"no such column: a0.height") as error:
                query["reader"].execute(query["sql"], [str(incomplete)])
            self.assertIn("CREATE TABLE T_L0_Init AS", str(error.exception))

    def test_count_rows(self):
        with tempfile.TemporaryDirectory() as directory:
            source = Path(directory) / "input.csv"
            with patch.object(csr, "resolve_path", return_value=source):
                self.assertEqual(csr.CsvIO().count_rows("input.csv"), -1)
                for contents, expected in [
                    ("", 0),
                    ("LOT,VALUE\n", 0),
                    ('LOT,VALUE\nA,"two\nlines"\n\nB,value\n', 2),
                ]:
                    with self.subTest(contents=contents):
                        source.write_text(contents, encoding="utf-8")
                        self.assertEqual(csr.CsvIO().count_rows("input.csv"), expected)

    def test_conditional_flow(self):
        for raw_count, signal_count in [(-1, 1), (0, 1), (1, -1), (1, 0), (1, 1)]:
            with self.subTest(raw_count=raw_count, signal_count=signal_count):
                csv_io = Mock(spec=csr.CsvIO)
                csv_io.count_rows.side_effect = [raw_count, signal_count]
                signals = pd.DataFrame({"Lot_NCORisk": ["NEW"]})
                lots = pd.DataFrame({"facility": ["KM", "KM"], "lot": ["OLD", "NEW"]})
                with (
                    patch.object(csr, "logger", logging.getLogger(__name__), create=True),
                    patch.object(csr, "step_0004_sqlite_query") as step4,
                    patch.object(csr, "step_0005_sqlite_query") as step5,
                    patch.object(csr, "read_csv", side_effect=[signals, lots]) as read,
                ):
                    ctx = SimpleNamespace(csv_io=csv_io)
                    result = csr.get_facility_lot(ctx)
                    self.assertEqual(step4.call_count, int(raw_count > 0))
                    self.assertEqual(step5.call_count, int(raw_count > 0))
                    self.assertEqual(
                        [call.args[0] for call in csv_io.count_rows.call_args_list],
                        ["PARMI_IPM_RAW.csv", "DATA.csv"]
                        if raw_count > 0
                        else ["PARMI_IPM_RAW.csv"],
                    )
                    if raw_count > 0 and signal_count > 0:
                        self.assertEqual(result["LOT"].tolist(), ["NEW"])
                        self.assertEqual(read.call_count, 2)
                        self.assertEqual(
                            [Path(call.args[0]).name for call in read.call_args_list],
                            ["DATA.csv", "IPM_Data.csv"],
                        )
                    else:
                        self.assertTrue(result.empty)
                        read.assert_not_called()

    def test_history_filters_signals_and_deduplicates_candidates(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            ipm = pd.DataFrame({
                "facility": ["KM", "KM", "KM"],
                "lot": ["001", "NEW", "NEW"],
            })
            ipm.to_csv(root / "IPM_Data.csv", index=False)
            (root / "PARMI_IPM_RAW.csv").write_text("lot\n001\n", encoding="utf-8")
            history = root / "configured_history.csv"
            previous_directory = Path.cwd()
            try:
                os.chdir(root)
                with (
                    patch.object(csr, "__file__", str(root / "CSR_IAM.py")),
                    patch.object(csr, "HIST_PATH", str(history)),
                    patch.object(csr, "logger", logging.getLogger(__name__), create=True),
                    patch.object(csr, "step_0004_sqlite_query"),
                ):
                    ctx = csr.PipelineContext({
                        "csv_io": csr.CsvIO(), "macro": csr.MacroState(),
                    })
                    for contents, expected in [
                        (None, ["001", "NEW"]),
                        ("", ["001", "NEW"]),
                        ("\n", ["001", "NEW"]),
                        (" \n\t\n", ["001", "NEW"]),
                        ("LOT,OUT_DATE\n", ["001", "NEW"]),
                        ("LOT,OUT_DATE\n001,2026-10-02\n", ["NEW"]),
                        ("FACILITY,LOT\nKM,001\n", ["NEW"]),
                        ("LOT,OUT_DATE\n001,2026-10-02\nNEW,2026-10-02\n", []),
                    ]:
                        with self.subTest(history=contents):
                            if contents is not None:
                                history.write_text(contents, encoding="utf-8")
                            result = csr.get_facility_lot(ctx)
                            self.assertEqual(result["LOT"].tolist(), expected)
                            self.assertEqual(result.columns.tolist(), ["FACILITY", "LOT"])
                            self.assertEqual(csr.read_csv(root / "DATA.csv")["Lot_NCORisk"].tolist(), expected)
            finally:
                os.chdir(previous_directory)

    def test_history_save_accepts_empty_file_and_preserves_lot_ids(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            history = root / "HIST.csv"
            lots = pd.DataFrame({"FACILITY": ["KM"], "LOT": ["002"]})
            with (
                patch.object(csr, "HIST_PATH", str(history)),
                patch.object(csr, "logger", logging.getLogger(__name__), create=True),
                patch.object(csr, "get_facility_lot", return_value=lots),
                patch.object(csr.os, "getcwd", return_value=str(root)),
                patch("aed_updater.update_lot_attributes") as update,
            ):
                for contents, expected in [
                    ("", ["002"]),
                    ("\n", ["002"]),
                    ("FACILITY,LOT\nKM,001\n", ["001", "002"]),
                ]:
                    with self.subTest(history=contents):
                        history.write_text(contents, encoding="utf-8")
                        csr.process_facility_attribute_update.__wrapped__(Mock())
                        self.assertEqual(csr.read_csv(history)["LOT"].tolist(), expected)
                self.assertEqual(update.call_count, 3)

    def test_read_csv_does_not_hide_malformed_data(self):
        with tempfile.TemporaryDirectory() as directory:
            source = Path(directory) / "HIST.csv"
            source.write_text('LOT\n"unterminated\n', encoding="utf-8")
            with patch.object(csr, "logger", logging.getLogger(__name__), create=True):
                with self.assertRaises(pd.errors.ParserError):
                    csr.read_csv(source)


if __name__ == "__main__":
    unittest.main()
