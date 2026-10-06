"""Portable overrides for the archived XMLToCSVTask class."""

import SPFLib.SPFSQL3 as legacy


class XMLToCSVTask(legacy.XMLToCSVTask):
    """Extend the original implementation with portable methods."""

    def executeTaskCommand(self):
        r"""
        ' Overridden base method. Implementation of XMLTOCSV
        '================================
        'Converts XML File to CSV 
        '================================
        sample input line: /UTILITIES=@EXEDIR@\XMLToCSV.va "XMLToCSVTask_HT01_Input.xml" "XMLToCSVTask_HT01_ouput.csv"
        """
        #locals
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        NoArgs = 1 # number of Args expected to process this Task
        MyUtilityName = "XMLTOCSV"

        XLText= -4158 #'Create XL Tab dlm file
        XLCSV=6       #'Create XL , dlm file
        csvFileToWrite_DLM = XLCSV

        objExcel = None
        XLBook = None
        xmlFileToLoad = None # MyArgList(0)
        csvFileToWrite = None # MyArgList(1)
        try :
            self.Write_Prompt()
            #self.Console("Starting XML to CSV Applet, v1.2 ... {0}".format(self.DatetimeNow))

            xmlFileToLoad = self.MyUtilities[1]
            self.logger.debug("  XML File to Load   : {0}".format(xmlFileToLoad))

            csvFileToWrite = self.MyUtilities[2]
            self.logger.debug("  CSV File to Create : {0}".format(csvFileToWrite))

            """
            '*******
            'Arg Chk
            '*******
            """
            if self.IsEmptyOrNone(xmlFileToLoad) is True or self.IsEmptyOrNone(csvFileToWrite) is True :
                errMsg = "Some arguments are missing. Exiting ..."
                raise Exception(errMsg)

            #"""
            #'*******
            #'Set dlm
            #'*******
            #"""
            #l_DLM = self.GetFileDLM(csvFileToWrite)
            #if l_DLM == "\t" :
            #    csvFileToWrite_DLM = XLText
            #self.logger.debug("{0} - csvFileToWrite_DLM : {1}".format(calling_func, csvFileToWrite_DLM))
            #"""
            #'**********************************
            #'Get Path of CSV o/p & XML I/p File
            #'**********************************
            #"""

            xmlFileToLoad = legacy.os.path.abspath(xmlFileToLoad)
            self.logger.debug("{0} - xmlFileToLoad : {1}".format(calling_func, xmlFileToLoad))

            csvFileToWrite = legacy.os.path.abspath(csvFileToWrite)
            self.logger.debug("{0} - csvFileToWrite : {1}".format(calling_func, csvFileToWrite))

            """
            '*************************
            'Extract Folder from Dest.
            '*************************
            """
            csvFileToWrite_path, csvFileToWrite_name = legacy.os.path.split(csvFileToWrite)
            self.logger.debug("{0} - csvFileToWrite_path : {1}".format(calling_func, csvFileToWrite_path))
            self.logger.debug("{0} - csvFileToWrite_name : {1}".format(calling_func, csvFileToWrite_name))

            if self.IsEmptyOrNone(csvFileToWrite_path) is True :
                csvFileToWrite_path = ".\\"

            if legacy.os.path.exists(xmlFileToLoad) is False : # 'XML File Does Not Exist
                errMsg = "Sorry, but could not locate XML file: {0}".format(xmlFileToLoad)
                raise Exception(errMsg)

            if legacy.os.path.exists(csvFileToWrite_path) is False : # 'Destination folder does not exist
                errMsg = "Sorry, but destination directory for CSV file does not exist: {0}".format(csvFileToWrite)
                raise Exception(errMsg)

            if legacy.os.path.exists(csvFileToWrite) is True :
                try :
                    self.SPFDelete(csvFileToWrite, displayPrompt=False)
                except Exception as err:
                    self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
                    errMsg = ("{0}  occurred while accessing {1}\n"
                              "You do not have permissions to overwrite this file."
                              ).format(err.args[0], csvFileToWrite)
                    raise Exception(errMsg)

            #try :
            #    objExcel = Dispatch('Excel.Application') #'Invisible Xl
            #    #objExcel = win32com.client.gencache.EnsureDispatch('Excel.Application')
            #    self.logger.debug("{0} - objExcel : {1}".format(calling_func, objExcel))
            #except Exception as err:
            #    self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            #    errMsg = "The following error occurred while initiating Excel: \n{0}\n".format(err.args[0])
            #    raise Exception(errMsg)

            #objExcel.DisplayAlerts = 0 #'Do not display Excel messages

            #try :
            #    XLBook = objExcel.Workbooks.OpenXML(Filename=xmlFileToLoad,LoadOption=2)
            #except Exception as err:
            #    self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            #    errMsg = "The following error occurred while processing XML: \n{0}\n".format(err.args[0])
            #    raise Exception(errMsg)

            #try :
            #    XLBook.SaveAs(Filename=csvFileToWrite, FileFormat=csvFileToWrite_DLM, CreateBackup=False)
            #    #'ActiveWorkbook.SaveAs Filename:="C:\Recovery.tab", FileFormat:=xlText, CreateBackup:=False
            #except pythoncom.com_error as comErr:
            #    self.logger.exception("{0} - {1}".format(calling_func, comErr.strerror))
            #    errMsg = "The following error occurred while saving CSV from Excel: \n{0}\n".format(comErr.strerror)
            #    raise Exception(errMsg)
            #except Exception as err:
            #    self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            #    errMsg = "The following error occurred while saving CSV from Excel: \n{0}\n".format(err.args[0])
            #    raise Exception(errMsg)

            #try :
            #    XLBook.Close(False)
            #except Exception as err:
            #    self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            #    errMsg = "The following error occurred while closing the Excel Workbook: \n{0}\n".format(err.args[0])
            #    raise Exception(errMsg)

            MyExe0 = "spfExcelUtility.exe"
            MyExe0 = legacy.os.path.join(self.gSPFExe, MyExe0)
            cmdArgs = ['/mode="XMLTOCSV"',
                        '/XML="{0}"'.format(xmlFileToLoad),
                        '/out="{0}"'.format(csvFileToWrite)]
            # [Removed from current version] runStatus, runExitCode = self.Run(MyExe0, cmdArgs,usePopen=True)
            if legacy.os.name != "nt":
                from scripthost_portable.file_operations import xml_to_csv
                xml_to_csv(xmlFileToLoad, csvFileToWrite, self.GetFileDLM(csvFileToWrite))
            else:
                runStatus, runExitCode = self.Run(MyExe0, cmdArgs,usePopen=True)
            self.ConsoleDoneWithTimeStamp2()
        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise
        finally :
            self.CloseExcel(objExcel, XLBook)
