"""Portable overrides for the archived GetFilesTask class."""

import SPFLib.SPFSQL3 as legacy


class GetFilesTask(legacy.GetFilesTask):
    """Extend the original implementation with portable methods."""

    def getFilesInfoFromFolderGlob(self, myPathToRead, includeSubFolder=False):
        """
        helper function to scan for files matching the given pattern -- using pathlib.Path
        """
        #region locals
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        self.logger.debug("{0} - myPathToRead : {1}".format(calling_func, myPathToRead))
        self.logger.debug("{0} - includeSubFolder : {1}".format(calling_func, includeSubFolder))
        output_FilesInfoArr = []
        found_wild_card = False
        #endregion locals
        try:
            try:
                if legacy.Path(myPathToRead).is_dir() is True:
                    #this is path to a directory without any wildcards...add the *.*
                    self.logger.debug("{0} - Path points to a folder: {1}".format(calling_func, myPathToRead))
                    # [Removed from current version] myPathToRead = rf"{myPathToRead}\*.*"
                    myPathToRead = legacy.os.path.join(myPathToRead, "*.*")
                    self.logger.debug("{0} - Updated Path : {1}".format(calling_func, myPathToRead))
            except Exception as err:
                #path is not plain directory...
                #continue
                pass

            if myPathToRead == ".\\":
                myPathToRead = r".\*.*"
            if myPathToRead.startswith(".\\") is True:
                myPathToRead = str(legacy.Path(myPathToRead).absolute())
                self.logger.debug("{0} - absolute myPathToRead : {1}".format(calling_func, myPathToRead))
            path_parts = legacy.Path(myPathToRead).parts
            path_parts_without_wildcard = [] #all parts of path till a wildcard is encountered
            path_parts_with_wildcard = [] #part of path with wildcard and rest parts of path

            for path_part in path_parts:
                if (path_part.find("*") > -1
                or path_part.find("?") > -1
                or  found_wild_card is True):
                    path_parts_with_wildcard.append(path_part)
                    found_wild_card= True
                else:
                    path_parts_without_wildcard.append(path_part)
            self.logger.debug("{0} - found_wild_card : {1}".format(calling_func, found_wild_card))

            #now construct the pathparts
            # [Removed from current version] path_parent = "\\".join(path_parts_without_wildcard)
            # [Removed from current version] path_glob_part = "\\".join(path_parts_with_wildcard)
            path_parent = str(legacy.Path(*path_parts_without_wildcard))
            path_glob_part = legacy.os.sep.join(path_parts_with_wildcard)

            if found_wild_card is False:
                self.logger.debug("{0} - single file : {1}".format(calling_func, path_parent))
                _tmp = legacy.Path(path_parent)
                self.logger.debug("{0} - file exists: {1}".format(calling_func, _tmp.exists()))
                filesIterator = [_tmp] if _tmp.exists() else []
            else:
                if includeSubFolder is True:
                    filesIterator = legacy.Path(path_parent).rglob(path_glob_part)
                else:
                    filesIterator = legacy.Path(path_parent).glob(path_glob_part)

            for filePathObj in filesIterator:
                output_FilesInfoArr.append(self.getFileInfo(filePathObj))
        except Exception as err:
            if self.ContinueOnError is True:
                self.ConsoleErrMsgContinue(err)
            else:
                self.logger.exception("{0} - {1}".format(calling_func, err))
                raise
        return output_FilesInfoArr
