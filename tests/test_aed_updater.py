"""Check the copied updater without contacting AED or changing lots."""

import logging
import sys
import unittest
from unittest.mock import Mock, patch

import aed_updater as updater

with patch.object(sys, "argv", ["aed-updater-test"]):
    import aed_client


class UpdaterTests(unittest.TestCase):
    def setUp(self):
        self.service = Mock()
        self.service.lot_status.return_value.json.return_value = {
            "Success": True,
            "Result": {"Attributes": [{"ID": "1064", "Value": None}]},
        }
        self.service.lot_set_attr.return_value.json.return_value = {"Success": True}
        self.factory = patch.object(aed_client, "ManufacturingService", return_value=self.service)
        self.factory.start()
        self.addCleanup(self.factory.stop)
        self.mode = patch.object(updater, "ENV_MODE", "prod")
        self.mode.start()
        self.addCleanup(self.mode.stop)

    def update(self, **kwargs):
        return updater.update_lot_attributes(
            "A15", "DUMMY", kwargs.get("attributes", "1064"),
            kwargs.get("value", {"2446"}), logging.getLogger("aed-test"),
        )

    def test_csr_call_normalizes_attribute_and_skip_value(self):
        self.assertEqual(self.update(), "Y")
        self.service.lot_set_attr.assert_called_once_with("DUMMY", "1064", "2446")

    def test_test_mode_never_writes(self):
        with patch.object(updater, "ENV_MODE", "test"):
            self.assertEqual(self.update(), "Y")
        self.service.lot_set_attr.assert_not_called()

    def test_already_correct_attribute_never_writes(self):
        self.service.lot_status.return_value.json.return_value["Result"]["Attributes"][0]["Value"] = "2446"
        self.assertEqual(self.update(), "N")
        self.service.lot_set_attr.assert_not_called()

    def test_failed_read_never_writes(self):
        self.service.lot_status.return_value.raise_for_status.side_effect = RuntimeError("read failed")
        with self.assertRaisesRegex(RuntimeError, "read failed"):
            self.update()
        self.service.lot_set_attr.assert_not_called()

    def test_failed_update_is_reported(self):
        self.service.lot_set_attr.return_value.json.return_value = {"Success": False}
        with self.assertRaisesRegex(Exception, "lot_set_attr"):
            self.update()

    def test_ambiguous_skip_value_is_rejected_before_requests(self):
        with self.assertRaises(ValueError):
            self.update(value={"2446", "2303"})
        self.service.lot_status.assert_not_called()


if __name__ == "__main__":
    unittest.main()
