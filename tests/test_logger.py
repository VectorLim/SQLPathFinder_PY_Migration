import logging

from vg2c.logger import Logger


def test_condition_logs_and_returns_the_original_boolean(caplog) -> None:
    prompt = "/PROMPT-TEXT=Condition is 100% complete"

    with caplog.at_level(logging.INFO, logger="vg2c.workflow"):
        assert Logger.condition(prompt, True) is True
        assert Logger.condition(prompt, False) is False

    assert [record.getMessage() for record in caplog.records] == [
        f"{prompt} | IF evaluated to True",
        f"{prompt} | IF evaluated to False",
    ]
