"""
-------------------------------------------------------------------------
Class: PyUtils.py      Date: 08/31/2019

Version History:
========================================================================================================================
| Date         |  Who        | Ver    |  Description                                                                   |
========================================================================================================================
| 08/31/2019   |  gcarmiol   |  1.0   |  Initial Version                                                               |
| 11/11/2019   |  gcarmiol   |  1.1   |  Added function CheckDateIdeal to remove MKL bug                               |
| 09/01/2020   |  gcarmiol   |  1.2   |  Added FixString functio                                                       |
| 08/05/2021   |  gcarmiol   |  1.3   |  Fixes to allow the utility to work with KitchenSink data correctly            |
| 07/12/2022   |  gcarmiol   |  1.4   |  Modified logging to work with Catalyst Team execution                         |
| 10/25/2022   |  gcarmiol   |  2.0   |  Improved integration with KitchenSink                                         |
| 03/14/2023   |  gcarmiol   |  3.0   |  Support for parquet files and added method to determine if a map is a         |
|              |             |        |  carrier or a wafer                                                            |
| 09/20/2024   |  gcarmiol   |  3.1   |  Capability to check Trace_Unit and Trace_Die in both servers for              |
|              |             |        |  drop_opers_after_signal                                                       |
| 08/12/2025   |  gcarmiol   |  3.2   |  Adding possibility of not requiring default values                            |
========================================================================================================================

Description:
============
Functions and classes containing useful utilities for Python

Classes:
            BuildArgsClass:   This class builds arguments based on command line, lists, dictionaries or config files

Functions:
            BuildArgs      :  This function just helps execusion of BuildArgsClass
            GetDataFileType:  Helper function that gets the data type of a file
            CleanHeaders   :  Clean headers of a dataframe
            NameDefaultArgs:  Names arguments given in command line
            CheckDateIdeal :  Checks if IDEAL version is too old and libiomp5md.dll needs to be updated
            FixString:     :  Cleans a string to remove non-desired characters
============

"""
__version__ = '3.2'

import os
import re
import sys
import time
import threading
import queue
import numpy as np
import pandas as pd
import configparser
from subprocess import Popen
from datetime import datetime as dt

isSH = os.path.expandvars('%SHServer%')
if isSH == '%SHServer%':  # Note that Env Var SHServer is set on ScriptHost
    myspf = os.path.dirname(os.path.realpath(__file__)).strip() + r'\SPFLib\SPFUtilities'
else:
    myspf = os.path.expandvars('%temp%')
sys.path.insert(0, myspf)
try:
    from ATTDMongoDB.ATTDMongoDBDriver import ATTDMongoDBHandler
except:
    pass


class BuildArgsClass(object):
    '''
    Description:
        Function that parses command line arguments based on configuration file.  The arguments can be in
        any order.  There should be no spaces between the tag, the '=' and the value.

    Arguments:
        arg_input:     This is a list with the command line arguments, typically sys.argv
                      This can also be a dictionary passed from another class with inputs
        arg_config:   This is the configuration file with the command arguments required and
                      the default values when applicable.
                      Only values in the dictionary will be returned and the type of the data will be
                      retained.  Currently it works for strings, int, float, lists or dictionaries.
                      In case of a list or dictionary since this contain spaces the whole list or
                      dictionary need to be enclosed in double quotes.
                      If you require a value to be passed that has no default, an empty value of the same
                      type should be added to the arg_cofig list.  If the value is numeric then None should
                      be passed.  The value will be converted first to int then float if that fails.
                      Example of arg_config:
                             arg_config = {
                                            /arg1='',
                                            /arg2=5.3,
                                            /arg3=3,
                                            /arg4=None,
                                            /arg5="['a', 'b', 3]"
                                            /arg6="{'a': 'res1', 'b': 5, 'c': 3.8}"
                                            /arg7=[]
                                            /arg8={}
                                          }
                      In the previous example, arg1 is required and needs to be text, arg2, needs to be float
                      and had a default value of 5.3, arg3 needs to be an integer and has a default value of
                      3 (if you need a float value you should write 3.0), arg4 is numeric and has no default
                      value, arg5 is a list and has a default value, arg6 is a dict and has a default value,
                      arg7 is a list and has no default, arg8 is a dict and has no default value.
                      Any arguments that require a sting with spaces need to be passed surrounded by double quotes.

    Output:           Dictionary similar to arg_config, but with data from command line or defaults when
                      no value found in command line.
    '''
    def __init__(self, inputs, defaults, ini_section, correct_keys, require_defaults, interactive_run=True, logger=None):
        self._logger = logger
        self._logger.debug('Running BuildArgsClass class init method')
        self._interactive_run = interactive_run
        self.arg_values = {}
        self._correct_keys = correct_keys
        self._empty_defaults = [None, [], '', (), {}]
        self._inputs = inputs
        self._require_defaults = require_defaults
        self._defaults = self._Clean_keys(defaults)
        self._input_type = self._Check_Input()
        if self._input_type == 'type_list':
            self._inputs = self._Convert_List_to_Dict(self._inputs)
        self._inputs = self._Clean_keys(self._inputs)
        if ('ini_file' in self._inputs) and os.path.isfile(self._inputs['ini_file']) and ini_section!='none':
            self._my_ini_file = self._Clean_keys(self._Read_ini_values(self._inputs['ini_file'], ini_section))
            self._inputs = {**self._inputs, **self._my_ini_file}
        self._Get_Arguments()

    def _Read_ini_values(self, ini_name, ini_section):
        self._logger.debug('Running _Read_ini_values method')
        myini = {}
        config = configparser.ConfigParser()
        try:
            config.read(ini_name)
            if ini_section in config:
                for mykey in config[ini_section]:
                    myini[mykey] = config[ini_section][mykey]
        except:
            myini = {}
        return myini

    def _Convert_List_to_Dict(self, list_input):
        self._logger.debug('Running _Convert_List_to_Dict method')
        dict_input = {}
        for i in list_input:
            if i.find('=') > 0:
                key, val = i.split('=')
                dict_input.update({key: val})
            elif i.find(':') > 0:
                key, val = i.split(':')
                dict_input.update({key: val})
            else:
                dict_input.update({key: None})
        return dict_input

    def _Clean_keys(self, mydict):
        self._logger.debug('Running _Clean_keys method')
        fixed_dict = {}
        if self._correct_keys == 'Y':
            for mykey in mydict.keys():
                if mykey[0] == '/':
                    mykey_new = mykey[1:].strip().lower()
                else:
                    mykey_new = mykey.strip().lower()
                    #new line of code here
                fixed_dict[mykey_new] = mydict[mykey]
            return fixed_dict
        else:
            return mydict

    def _Check_Input(self):
        self._logger.debug('Running _Check_Input method')
        if type(self._inputs) == list:
            self._Check_List_Spaces()
            return 'type_list'
        elif type(self._inputs) == dict:
            return 'type_dict'
        else:
            print('\n')
            self._logger.error("===============================================================================")
            self._logger.error("Unexpected Error:  Input to the BuildArgs function can only be list or dict")
            self._logger.error("===============================================================================")
            print("\n")
            print("\n")
            if self._interactive_run:
                sys.exit(1)

    def _Check_List_Spaces(self):
        self._logger.debug('Running _Read_ini_values method')
        if type(self._inputs) == list:
            check = [t for t in self._inputs if t[0] == '=' or t[-1] == '=']
            if len(check) > 0:
                print('\n')
                self._logger.error("===================================================================")
                self._logger.error("Command line arguments can't have spaces between the tag and")
                self._logger.error("the value, exiting ...")
                self._logger.error("===================================================================")
                print("\n")
                print("\n")
                if self._interactive_run:
                    sys.exit(1)

    def _Get_Arguments(self):
        self._logger.debug('Running _Get_Arguments method')
        for key in self._defaults.keys():
            if key in self._inputs.keys():
                results = self._inputs[key]
                self._Add_Result(results, key)
                del self._inputs[key]
            else:
                results = self._defaults[key]
                store = True
                if results in self._empty_defaults and self._require_defaults == 'Y':
                    store = False
                elif isinstance(results, pd.Series) or isinstance(results, pd.DataFrame):
                    if len(results) == 0:
                        store = False
                elif self._require_defaults == 'N':
                    store = True
                if store:
                    self.arg_values[key] = results
                else:
                    print('\n')
                    self._logger.error("=============================================================")
                    self._logger.error("Missing required argument: " + key + ". Exiting ...")
                    self._logger.error("=============================================================")
                    print("\n")
                    print("\n")
                    if self._interactive_run:
                        sys.exit(1)
        for key in self._inputs.keys():
            self.arg_values[key] = self._inputs[key]

    def _Add_Result(self, curr_result, key):
        self._logger.debug('Running _Add_Result method')
        default_val = self._defaults[key]
        if isinstance(default_val, bool):
            try:
                if type(curr_result) == bool:
                    self.arg_values[key] = curr_result
                else:
                    curr_result = curr_result.capitalize()
                    myvalue = eval(curr_result)
                    if type(myvalue) == bool:
                        self.arg_values[key] = myvalue
                    else:
                        raise Exception
            except:
                print("\n")
                self._logger.error("=========================================================================")
                self._logger.error("Value for argument: " + key + ". Needs to be a boolean")
                self._logger.error("Please correct it and retry, exiting ...")
                self._logger.error("=========================================================================")
                print("\n")
                print("\n")
                if self._interactive_run:
                    sys.exit(1)
        elif isinstance(default_val, int):
            try:
                self.arg_values[key] = int(curr_result)
            except:
                print("\n")
                self._logger.error("===================================================================")
                self._logger.error("Value for argument: " + key + ". Needs to be integer, exiting ...")
                self._logger.error("===================================================================")
                print("\n")
                print("\n")
                if self._interactive_run:
                    sys.exit(1)
        elif isinstance(default_val, float):
            try:
                self.arg_values[key] = float(curr_result)
            except:
                print("\n")
                self._logger.error("===================================================================")
                self._logger.error("Value for argument: " + key + ". Needs to be float, exiting ...")
                self._logger.error("===================================================================")
                print("\n")
                print("\n")
                if self._interactive_run:
                    sys.exit(1)
        elif isinstance(default_val, list):
            try:
                if type(curr_result) == list:
                    self.arg_values[key] = curr_result
                else:
                    myvalue = eval(curr_result)
                    if type(myvalue) == list:
                        self.arg_values[key] = myvalue
                    else:
                        raise Exception
            except:
                print("\n")
                self._logger.error("=========================================================================")
                self._logger.error("Value for argument: " + key + ". Needs to be a list: ")
                self._logger.error('    The list needs to be surrounded by "double quotes",')
                self._logger.error("    if you have strings as values they need to be in 'single quotes'.\n")
                self._logger.error("Please correct it and retry, exiting ...")
                self._logger.error("=========================================================================")
                print("\n")
                print("\n")
                if self._interactive_run:
                    sys.exit(1)
        elif isinstance(default_val, dict):
            try:
                if type(curr_result) == dict:
                    self.arg_values[key] = curr_result
                else:
                    myvalue = eval(curr_result)
                    if type(myvalue) == dict:
                        self.arg_values[key] = myvalue
                    else:
                        raise Exception
            except:
                print("\n")
                self._logger.error("=========================================================================")
                self._logger.error("Value for argument: " + key + ". Needs to be a dictionary: ")
                self._logger.error('    The dict needs to be surrounded by "double quotes",')
                self._logger.error("    the keys and string values need to be in 'single quotes'.\n")
                self._logger.error("Please correct it and retry, exiting ...")
                self._logger.error("=========================================================================")
                print("\n")
                print("\n")
                if self._interactive_run:
                    sys.exit(1)
        elif isinstance(default_val, tuple):
            try:
                if type(curr_result) == tuple:
                    self.arg_values[key] = curr_result
                else:
                    myvalue = eval(curr_result)
                    if type(myvalue) == tuple:
                        self.arg_values[key] = myvalue
                    else:
                        raise Exception
            except:
                print("\n")
                self._logger.error("=========================================================================")
                self._logger.error("Value for argument: " + key + ". Needs to be a tuple: ")
                self._logger.error('    The tuple needs to be surrounded by "double quotes",')
                self._logger.error("    the keys and string values need to be in 'single quotes'.\n")
                self._logger.error("Please correct it and retry, exiting ...")
                self._logger.error("=========================================================================")
                print("\n")
                print("\n")
                if self._interactive_run:
                    sys.exit(1)
        elif isinstance(default_val, pd.DataFrame):
            if isinstance(curr_result, pd.DataFrame):
                self.arg_values[key] = curr_result
            else:
                print("\n")
                self._logger.error("=========================================================================")
                self._logger.error("Value for argument: " + key + ". Needs to be a Pandas DataFrame")
                self._logger.error("Please correct it and retry, exiting ...")
                self._logger.error("=========================================================================")
                print("\n")
                print("\n")
                if self._interactive_run:
                    sys.exit(1)
        elif isinstance(default_val, pd.Series):
            if isinstance(curr_result, pd.Series):
                self.arg_values[key] = curr_result
            else:
                print("\n")
                self._logger.error("=========================================================================")
                self._logger.error("Value for argument: " + key + ". Needs to be a Pandas Series")
                self._logger.error("Please correct it and retry, exiting ...")
                self._logger.error("=========================================================================")
                print("\n")
                print("\n")
                if self._interactive_run:
                    sys.exit(1)
        elif default_val == None:
            try:
                self.arg_values[key] = int(curr_result)
            except:
                try:
                    self.arg_values[key] = float(curr_result)
                except:
                    print("\n")
                    self._logger.error("=======================================================================")
                    self._logger.error("Value for argument: " + key + " can only be int or float. Exiting ...")
                    self._logger.error("=======================================================================")
                    print("\n")
                    print("\n")
                    if self._interactive_run:
                        sys.exit(1)
        else:
            if len(curr_result) > 0 and ((curr_result[0] == "'" and curr_result[-1] == "'") or (curr_result[0] == '"' and curr_result[-1] == '"')):
                self.arg_values[key] = curr_result[1:-1]
            else:
                self.arg_values[key] = curr_result


def BuildArgs(arg_input, arg_config, ini_section='none', correct_keys='Y', require_default='Y', logger=None):
    '''
    Function just intended to simplify execution of class BuildClassArgs and immediately return arguments
    '''
    logger.debug('Running BuildArgs function')
    myargs = BuildArgsClass(arg_input, arg_config, ini_section, correct_keys, require_default, logger=logger)
    return myargs.arg_values


def GetDataFileType(data_file_name, check_file_exists='Y', interactive_run=True, logger=None):
    logger.debug('Running GetDataFileType function')
    if check_file_exists == 'Y':
        if not os.path.isfile(data_file_name):
            print('\n')
            logger.error('#########################################')
            logger.error('Error: Data File not Found....exiting')
            logger.error('#########################################')
            print('\n')
            if interactive_run:
                sys.exit(1)
    delim_dict = {'.csv': ',',
                  '.tab': '\t',
                  '.tsv': '\t',
                  '.parquet': 'parquet'}
    fileext = os.path.splitext(data_file_name)[1]
    if fileext.lower() in delim_dict.keys():
        return delim_dict[fileext]
    else:
        print('\n')
        logger.error('######################################################')
        logger.error('Error: Only CSV (,), TAB (\\t), TSV (\\t) or Parquet')
        logger.error('       files can be used....exiting')
        logger.error('######################################################')
        print('\n')
        if interactive_run:
            sys.exit(1)


def CleanHeaders(data_file_name, interactive_run=True, logger=None):
    logger.debug('Running CleanHeaders function')
    delim = GetDataFileType(data_file_name, logger=logger)
    df = pd.read_csv(data_file_name, nrows=1, sep=delim)
    mycols = list(df.columns)
    space_found = False
    for colcheck in mycols:
        if colcheck[0] == ' ' or colcheck[-1] == ' ':
            space_found = True
    if space_found:
        print('\n')
        logger.info("=====================================================================================")
        logger.info("Leading or trailing spaces found in the column headers of the data table.")
        logger.info("The spaces need to be removed for the utility to work properly.  A new data file")
        logger.info('ending in "_clean" will be created in the same location as the original data file.')
        logger.info("=====================================================================================")
        print('\n')
        data_file_name_clean = os.path.splitext(data_file_name)[0] + '_clean' + os.path.splitext(data_file_name)[1]
        if os.path.isfile(data_file_name_clean):
            os.remove(data_file_name_clean)
        counter = 1
        with open(data_file_name) as fr:
            with open(data_file_name_clean, 'a') as fw:
                for line in fr:
                    if counter == 1:
                        if line[0] == ' ' or line[-1] == ' ':
                            line = line.strip()
                            line = line + '\n'
                        errors_found = True
                        while errors_found:
                            if line.find(' ,') >= 0 or line.find(', ') >= 0:
                                line = line.replace(' ,', ',')
                                line = line.replace(', ', ',')
                            else:
                                errors_found = False
                        counter += 1
                    fw.write(line)
        print('\n')
        logger.info("Data file with clean headers created successfully...." )
        print('\n')
    else:
        data_file_name_clean = data_file_name
    return data_file_name_clean


def NameDefaultArgs(cmd_inputs, default_arg_names, interactive_run=True, logger=None):
    logger.debug('Running NameDefaultArgs function')
    if len(default_arg_names) > len(cmd_inputs):
        print("\n")
        logger.error("=============================================================")
        logger.error("Missing required arguments. Exiting ...")
        logger.error("=============================================================")
        print("\n")
        print("\n")
        if interactive_run:
            sys.exit(1)
    final_list = []
    for i in range(len(cmd_inputs)):
        if len(default_arg_names) > i:
            final_list.append(default_arg_names[i] + '=' + cmd_inputs[i])
        else:
            final_list.append(cmd_inputs[i])
    return final_list


def FindPattern(all_cols, patterns, interactive_run=True, logger=None):
    logger.debug('Running FindPattern function')
    list_final = []
    for i in patterns:
        list_final = list_final + [mycol for mycol in all_cols if len(re.findall(i.lower(), mycol.lower())) > 0]
    return list(set(list_final))

def IsRectangle(df, x_col, y_col, logger=None):
    logger.debug('Running IsWafer function')
    xmax = df[x_col].max()
    xmin = df[x_col].min()
    ymax = df[y_col].max()
    ymin = df[y_col].min()
    coord_list = [(xmax, ymax), (xmin, ymin), (xmax, ymin), (xmin, ymax),
                 (xmax - 1, ymax), (xmin + 1, ymax), (xmax - 1, ymin), (xmin + 1, ymin),
                 (xmax, ymax - 1), (xmax, ymin + 1), (xmin, ymax - 1), (xmin, ymin + 1)]
    for coord in coord_list:
        if len(df[(df[x_col] == coord[0]) & (df[y_col] == coord[1])]) > 0:
            return True
    return False

def get_input(message, channel, interactive_run=True, logger=None):
    logger.debug('Running get_input function')
    response = input(message)
    channel.put(response)


def input_with_timeout(message, timeout, interactive_run=True, logger=None):
    logger.debug('Running input_with_timeout function')
    channel = queue.Queue()
    # message = message + " [{} sec timeout] ".format(timeout)
    thread = threading.Thread(target=get_input, args=(message, channel))
    # by setting this as a daemon thread, python won't wait for it to complete
    thread.daemon = True
    thread.start()
    try:
        response = channel.get(True, timeout)
        return response
    except queue.Empty:
        pass
    return None


def CheckDateIdeal(interactive_run=True, logger=None):
    if os.name != "nt":
        raise RuntimeError("IDEAL registry discovery is only supported on Windows")
    import winreg

    logger.debug('Running CheckDateIdeal function')
    myregval1 = myregval2 = ''
    try:
        myreg = winreg.ConnectRegistry(None, winreg.HKEY_CLASSES_ROOT)
        myregval1 = winreg.QueryValue(myreg, "Ideal.ProjectFile\shell\open\command")
        myregval1 = myregval1.replace('"', '')
        myregval1 = myregval1[:myregval1.find('.exe') + 4]
        myregval1 = os.path.split(myregval1)[0]
    except:
        pass

    try:
        myreg = winreg.ConnectRegistry(None, winreg.HKEY_CLASSES_ROOT)
        myregval2 = winreg.QueryValue(myreg, "Ideal.ProjectFile\DefaultIcon")
        myregval1 = myregval1.replace('"', '')
        myregval2 = myregval2[:myregval2.find('.exe') + 4]
        myregval2 = os.path.split(myregval2)[0]
    except:
        pass

    if myregval1 != '':
        idealdir = myregval1
    elif myregval1 != '':
        idealdir = myregval2
    else:
        print("\n")
        logger.error("=================================================================")
        logger.error("IDEAL must be installed to use this utility,")
        logger.error("please install at http://ideal.intel.com/sitefiles/main.asp")
        logger.error("=================================================================")
        print("\n")
        print("\n")
        if interactive_run:
            sys.exit(1)

    file1 = os.path.join(idealdir, 'libiomp5md.dll')
    if os.path.isfile(file1):
        time1 = dt.fromtimestamp(os.stat(file1).st_mtime)
    else:
        print("\n")
        logger.error("=================================================================")
        logger.error("IDEAL doesn't seem to be installed correctly in this system,")
        logger.error("please re-install at http://ideal.intel.com/sitefiles/main.asp")
        logger.error("=================================================================")
        print("\n")
        print("\n")
        if interactive_run:
            sys.exit(1)

    if time1 < dt(year=2019, month=1, day=1):
        print("\n")
        print("\n")
        logger.warning("======================================== ATTENTION ========================================")
        logger.warning("\n")
        logger.warning("The IDEAL version installed in the system is too old to run this utility.")
        logger.warning("\n")
        logger.warning("This can be corrected in two ways:")
        logger.warning("  1) Install the latest version of IDEAL from:")
        logger.warning("          http://ideal.intel.com/sitefiles/main.asp")
        logger.warning("  2) Overwrite the file in current version of IDEAL that needs to be updated.")
        logger.warning("     This version of IDEAL is expected to keep working normally after the file is replaced.")
        logger.warning("     Window will open requiring you to approve the batch file run as administrator.")
        logger.warning("\n")
        myresult = input_with_timeout("Automatically Overwrite File (Y/N): ", 120)
        if myresult == None:
            logger.warning("\n\n\n=========================================================")
            logger.warning("No input received in 120 sec, exiting...")
            logger.warning("=========================================================")
            print("\n")
            print("\n")
            if interactive_run:
                sys.exit(1)
        print("\n")
        print("\n")
        if str(myresult).upper() == 'Y':
            script_path = os.path.split(os.path.realpath(__file__))[0]

            writeline = 'copy /Y "' + os.path.join(script_path, 'Python3', 'Library', 'bin', 'libiomp5md.dll') + '" "' + idealdir + '"'
            with open(os.path.join(script_path, 'execute_copy.bat'), 'w') as fw:
                fw.write(writeline)
            
            execution = os.path.join(script_path, "fix_mkl_error.bat")
            p = Popen(execution)
            stdout, stderr = p.communicate()
            time.sleep(5)
            try:
                os.remove(os.path.join(script_path, 'execute_copy.bat'))
            except:
                pass
        else:
            logger.warning("No file was replaced, exiting....")
            print("\n")
            print("\n")


def FixString(mystr, lower='Y', interactive_run=True, logger=None):
    logger.debug('Running FixString function')
    pattern = re.compile('[^0-9a-zA-Z~-]+')
    if lower == 'Y':
        return pattern.sub('_', str(mystr)).lower()
    else:
        return pattern.sub('_', str(mystr))


def Create_SPFSQL(losscode_list, response_col_list, input_vid_file, result_file, spf_sql_file_name='KS_Default_Extraction.spfsql',
                  instance='9999', spfsql_template='spfsql_template.txt', resp_col_name='response_flag', null_val_zero='N', interactive_run=True, logger=None):
    logger.debug('Running Create_SPFSQL function')
    if null_val_zero.upper()[0] == 'N':
        null_resp = "'$null'"
    else:
        null_resp = '0'
    file_path = os.path.split(os.path.realpath(__file__))[0]
    with open(os.path.join(file_path, spfsql_template), 'r') as file:
        spfsql_txt = file.read()
    spfsql_txt = spfsql_txt.replace('!!!vid_file!!!', input_vid_file)
    spfsql_txt = spfsql_txt.replace('!!!output_file!!!', result_file)
    spfsql_txt = spfsql_txt.replace('!!!instance!!!', instance)
    first_loop = True
    all_loss_code = ''
    for curr_loss_code in losscode_list:
        if not first_loop:
            all_loss_code = all_loss_code + ','
        all_loss_code = all_loss_code + f"'{curr_loss_code}'"
        first_loop = False
    first_loop = True
    temp_resp_vars = ''
    temp_response_flag_columns = ''
    temp_vars_agg = ''
    temp_vars_max = ",'" + resp_col_name + "' : {'$max': [ "
    var_count = 1
    for curr_resp_col in response_col_list:
        if not first_loop:
            temp_resp_vars = temp_resp_vars + ','
            temp_vars_max = temp_vars_max + ' ,  '
        curr_var_name = f'temp_{resp_col_name}{var_count}'
        temp_resp_vars = temp_resp_vars + curr_var_name
        temp_response_flag_columns = temp_response_flag_columns + ",'" + curr_var_name + "' : {'$switch':{'branches':[{'case':{'$in':[{'$toString': '$" + curr_resp_col + "'}, [" + all_loss_code + \
                                                                  "]]},'then':1 },{'case':{'$not': ['$" + curr_resp_col + "']},'then': " + null_resp + " }],'default':0}}\n"
        temp_vars_agg = temp_vars_agg + ",'" + curr_var_name + "' : { '$ifNull': [ '$" + curr_var_name + "', '' ] }\n"
        temp_vars_max = temp_vars_max + "'$" + curr_var_name + "'"
        first_loop = False
        var_count += 1
    temp_vars_max = temp_vars_max + '  ]}'
    all_resp_vars = temp_resp_vars + f',{resp_col_name}'
    spfsql_txt = spfsql_txt.replace('!!!response_flag_list!!!', all_resp_vars)
    spfsql_txt = spfsql_txt.replace('!!!response_flag_formulas!!!', temp_response_flag_columns)
    spfsql_txt = spfsql_txt.replace('!!!temp_response_flag_columns!!!', temp_vars_agg)
    spfsql_txt = spfsql_txt.replace('!!!temp_response_flag_max!!!', temp_vars_max)
    with open(spf_sql_file_name, "w") as text_file:
        text_file.write(spfsql_txt)


def Drop_Downstream_Opers(id_column, data_file, response_oper, threshold=0.75, unit_sample=10000, max_retries=5, un='', pw='', logger=None):
    if response_oper == '':
        return []
    logger.info(f'\nDropping operations downstream of: {response_oper}')
    if response_oper != '' and id_column == '':
        logger.error('  -Identifier column not passed, returning empty string')
        print('\n')
        return []
    try:
        logger.debug(f'  -Reading column: {id_column} from file: {data_file}')
        id_column = id_column.lower()
        delim = GetDataFileType(data_file, logger=logger)
        if delim == 'parquet':
            df = pd.read_parquet(data_file, columns=[id_column])
        else:
            df = pd.read_csv(data_file, sep=delim, usecols=[id_column])
    except:
        logger.error('  -Column with ids was not found in the data file, returning empty string\n')
        return []

    logger.debug('  -Obtaining list of units from file')
    list_of_units = df[id_column].drop_duplicates().to_list()

    conn_success = False
    retry_num = 1
    error_multiplier = 3
    if len(list_of_units) == 0:
        logger.info('  -No units passed of file empty, returning empty list')
        return []
    while not conn_success and retry_num <= max_retries:
        try:
            if un == '':
                logger.debug('  -Connecting to KitchenSink Trace using default SQLPathFinder Connector')
                api_args = {'DBTYPE': 'KITCHENSINK2', 'ERROR': ''}
                api_args2 = {'DBTYPE': 'KITCHENSINK3', 'ERROR': ''}
                server = ATTDMongoDBHandler().ConnectMongoDB(api_args)  # Get MongoDB Connection
                server2 = ATTDMongoDBHandler().ConnectMongoDB(api_args2)
                db = api_args['DB']
                db2 = api_args2['DB']
                collection = db['ATM_KS_TRACE_DIE']
                collection2 = db2['ATM_KS_TRACE_UNIT']
                logger.debug('  -Connection to KitchenSink Successful')
                conn_success = True
            else:
                import pymongo

                logger.info('  -Connecting to KitchenSink Trace using pymongo')
                client = pymongo.MongoClient('ATDSPWMONGOMD4:27019')
                client2 = pymongo.MongoClient('ATDSPWMONGOMD10:27019')
                db = client['ATM_KS']
                db.authenticate(un, pw)
                db2 = client2['ATM_KS']
                db2.authenticate(un, pw)
                collection = db['ATM_KS_TRACE_DIE']
                collection2 = db['ATM_KS_TRACE_UNIT']
                logger.debug('  -Connection to KitchenSink Successful')
                conn_success = True
        except Exception as error:
            logger.error(f'  -Connection to MongoDB Failed, retry {retry_num} will happen after {error_multiplier * retry_num}s...')
            time.sleep(error_multiplier * retry_num)
            retry_num += 1
            if retry_num > max_retries:
                errMsg = f"  -Unable to connect to MongoDB - No operations will be returned: {error}"
                logger.error(errMsg)
    if conn_success:
        logger.debug(f'  -Sampling max of {unit_sample} units from list of {len(list_of_units)}')
        df_data = pd.DataFrame({'units': list_of_units})
        df_data['rand'] = np.random.uniform(low=0, high=1, size=len(df_data))
        df_data = df_data.sort_values('rand')
        df_data = df_data[:unit_sample]

        final_unit_list = df_data['units'].to_list()

        logger.debug(f'  -Extracting traceability from KitchenSink Trace Collection')
        query_dict = {'identifier': {"$in": final_unit_list}}
        results_dict = {'identifier': 1, 'operation': 1, 'out_date': 1}

        cursor = collection.find(query_dict, results_dict)
        df = pd.DataFrame(list(cursor), dtype=str)

        cursor2 = collection2.find(query_dict, results_dict)
        df2 = pd.DataFrame(list(cursor2), dtype=str)

        collection2 = db2['ATM_KS_TRACE_WAFER']
        cursor3 = collection2.find(query_dict, results_dict)
        df3 = pd.DataFrame(list(cursor3), dtype=str)

        df = pd.concat([df, df2, df3])

        req_cols = ['_id', 'identifier', 'operation', 'out_date']
        for curr_col in req_cols:
            if curr_col not in df.columns:
                df[curr_col] = np.nan

        if len(df) > 0:
            logger.debug(f'  -Determining operation after the target operation')
            del df['_id']

            df['rank_oper'] = df.sort_values(['out_date'], ascending=[False]).groupby(['identifier', 'operation']).cumcount() + 1
            df = df[df['rank_oper'] == 1]
            del df['rank_oper']

            df_resp = df[df['operation'] == response_oper]
            df_resp['rank'] = df_resp.sort_values(['out_date'], ascending=[False]).groupby(['identifier']).cumcount() + 1
            df_resp = df_resp[df_resp['rank'] == 1]

            df_resp = df_resp[['identifier', 'out_date']]
            df_resp.rename(columns={'out_date': 'out_date_resp'}, inplace=True)
            df = pd.merge(df, df_resp, on='identifier', how='left')
            df['out_date_resp'] = df['out_date_resp'].fillna('3000-01-01 00:00:00')

            df.loc[df['out_date'] > df['out_date_resp'], 'downstream'] = 1
            df.loc[df['out_date'] <= df['out_date_resp'], 'downstream'] = 0

            df = df[['operation', 'downstream']].groupby(['operation'])
            df = df.agg({'downstream': 'mean'})
            df = df.reset_index()

            df = df[df['downstream'] >= threshold]

            list_of_opers = df['operation'].drop_duplicates().to_list()
            if len(list_of_opers) > 0:
                logger.info(f'  -Operations: {list_of_opers} found to be after signal operation and will be dropped from the analysis.\n')
            else:
                logger.info(f'  -No downstream operations found after signal, all operations will be used in the analysis.\n')
            return list_of_opers
        else:
            logger.debug('  -No data found in KitchenSink Trace Collection - Empty list returned\n')
            return []
    else:
        logger.debug('  -Connection to KitchenSink Trace Collection could not be established - Empty list returned\n')
        print('\n')
        return []


def Drop_Opers_from_Model(oper_list, model, logger=None):
    if type(model) != pd.Series:
        logger.error('Model results passed not pandas Series')
        return model
    if type(oper_list) != list:
        logger.error('Incorrect list of operation passed')
        return model
    for curr_oper in oper_list:
        if type(curr_oper) == str and (len(curr_oper) >= 4) and (len(curr_oper) <= 6):
            logger.debug(f'Dropping variables for operation {curr_oper}')
            model = model[~((model.index.str.find('#' + curr_oper + '#') >= 0) | (model.index.str.find('#' + curr_oper + '_') >= 0))]
    return model



if __name__ == "__main__":
    pass
