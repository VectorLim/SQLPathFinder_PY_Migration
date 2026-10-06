"""Fallbacks for unavailable compiled legacy database drivers."""

class _UnavailableLegacyDBDriverBase:
    def __init__(self, *args, **kwargs):
        raise RuntimeError("Legacy ScriptHost database transport is unavailable on this platform")


class NodesInfo:
    """Portable fallback for the node metadata bundled with compiled dbDrivers."""
    def __init__(self, node):
        self.node = node.strip()
        self.un = ""
        self.pw = ""


