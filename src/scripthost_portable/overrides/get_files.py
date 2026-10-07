"""Adapt Windows glob spelling; retain the original file scan and metadata."""

import SPFLib.SPFSQL3 as legacy


class GetFilesTask(legacy.GetFilesTask):
    def _directory_glob(self, path):
        if legacy.os.name == "nt":
            return super()._directory_glob(path)
        return legacy.os.path.join(path, "*.*")

    def _glob_parts(self, parent_parts, wildcard_parts):
        if legacy.os.name == "nt":
            return super()._glob_parts(parent_parts, wildcard_parts)
        return str(legacy.Path(*parent_parts)), legacy.os.sep.join(wildcard_parts)
