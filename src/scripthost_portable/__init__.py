"""Experimental portable facade over the decompiled ScriptHost runtime.

This package exists only for the architecture reassessment. It does not replace
or delete vg2c_new.
"""

from .runtime import PortableScriptHostRuntime

__all__ = ["PortableScriptHostRuntime"]
