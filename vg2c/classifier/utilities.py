from __future__ import annotations

from collections.abc import Callable
from dataclasses import dataclass

from vg2c.classifier.coerce import as_bool_yn
from vg2c.classifier.model import (
    CopySpec,
    DeleteSpec,
    EmailSpec,
    Kind,
    RenameSpec,
    RunPythonSpec,
    Spec,
)


@dataclass(frozen=True)
class UtilityHandler:
    """Registry entry for a known utility."""

    kind: Kind
    spec_builder: Callable[[str, list[str], dict[str, str], str], Spec]


def _build_run_python(
    executable: str, args: list[str], options: dict[str, str], body: str
) -> RunPythonSpec:
    """Build spec for Python script execution."""
    script_path = args[0] if args else ""
    extra_args = args[1:] if len(args) > 1 else []
    return RunPythonSpec(
        script_path=script_path,
        extra_args=extra_args,
        workdir=options.get("WORKDIR"),
        hadoop_server=options.get("HADOOP_SERVER_DEFAULT"),
        python_version=args[-1] if args and args[-1].startswith("Python") else None,
        prompt=options.get("PROMPT-TEXT"),
    )


def _build_copy(executable: str, args: list[str], options: dict[str, str], body: str) -> CopySpec:
    """Build spec for file copy."""
    src = args[0] if args else ""
    dst = args[1] if len(args) > 1 else ""
    continue_on_fail = as_bool_yn(args[2] if len(args) > 2 else "N")
    return CopySpec(
        src=src,
        dst=dst,
        continue_on_fail=continue_on_fail,
        prompt=options.get("PROMPT-TEXT"),
    )


def _build_rename(
    executable: str, args: list[str], options: dict[str, str], body: str
) -> RenameSpec:
    """Build spec for file rename."""
    src = args[0] if args else ""
    dst = args[1] if len(args) > 1 else ""
    return RenameSpec(
        src=src,
        dst=dst,
        prompt=options.get("PROMPT-TEXT"),
    )


def _build_delete(
    executable: str, args: list[str], options: dict[str, str], body: str
) -> DeleteSpec:
    """Build spec for file deletion."""
    target = args[0] if args else ""
    force = as_bool_yn(args[1] if len(args) > 1 else "N")
    return DeleteSpec(
        target=target,
        force=force,
        prompt=options.get("PROMPT-TEXT"),
    )


def _build_email(executable: str, args: list[str], options: dict[str, str], body: str) -> EmailSpec:
    """Build spec for email sending."""
    attachments_raw = args[0] if args else ""
    attachments = [a.strip() for a in attachments_raw.split(";") if a.strip()]

    recipients_token = args[1] if len(args) > 1 else ""
    subject = args[2] if len(args) > 2 else ""
    body_file = args[3] if len(args) > 3 else ""

    recipients_list = []
    if len(args) > 4:
        recipients_list = [r.strip() for r in args[4].split(";") if r.strip()]

    return EmailSpec(
        attachments=attachments,
        recipients_token=recipients_token,
        subject=subject,
        body_file=body_file,
        recipients_list=recipients_list,
        prompt=options.get("PROMPT-TEXT"),
    )


# Known utility registry
KNOWN_UTILITY_REGISTRY: dict[str, UtilityHandler] = {
    "run_python_script.va": UtilityHandler(Kind.RUN_PYTHON, _build_run_python),
    "copy.va": UtilityHandler(Kind.COPY, _build_copy),
    "rename.va": UtilityHandler(Kind.RENAME, _build_rename),
    "delete.va": UtilityHandler(Kind.DELETE_FILE, _build_delete),
    "email.va": UtilityHandler(Kind.EMAIL, _build_email),
}
