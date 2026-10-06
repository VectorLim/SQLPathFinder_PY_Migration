"""
-----------------------------------------------------
Class: SPF_DF.py      Date: 05/01/2019

Version History:
========================================================================================================================
| Date         |  Who        | Ver    |  Description                                                                   |
========================================================================================================================
| 05/01/2019   |  gcarmiol   |  1.0   |  Initial Version                                                               |
| 08/31/2019   |  gcarmiol   |  2.0   |  Final Release for Production                                                  |
| 08/05/2021   |  gcarmiol   |  2.1   |  Fixes to allow the utility to work with KitchenSink data correctly            |
| 06/02/2022   |  gcarmiol   |  2.2   |  Small improvements, version to share with Catalyst Team                       |
| 07/12/2022   |  gcarmiol   |  2.3   |  Modified logging to work with Catalyst Team execution                         |
| 10/25/2022   |  gcarmiol   |  3.0   |  Improved integration with KitchenSink                                         |
| 03/14/2023   |  gcarmiol   |  4.0   |  Support for parquet files                                                     |
| 06/14/2024   |  gcarmiol   |  4.1   |  Removal of deprecated arguments                                               |
| 07/27/2025   |  vanatara   |  4.2   |  Support for Python 3.13                                                       |
========================================================================================================================

Description:
============
Class containing methods to commonality dataframe based on Ideal output in Python 3.6.x/3.9.x


Methods:
========
__init__:       Initializes object and executes _FindCols method
Fix Dates       Method to try to set any column containing the word "date" as Pandas date
_FindPattern:   Internal method used by _FindCols, this returns a list with columns related
                an operation passed.
_FindCols:      Internal method that goes through the important columns passed to the class
                and returns hte other columns that should be loaded.
LoadData:       Loads the columns identified by FindCols from the commonality datafile

Properties:
===========
df:             Pandas DataFrame with loaded data.
"""
__version__ = '4.2'

import os
import re
import sys
import logging
import numpy as np
import pandas as pd
import pyarrow.parquet as pq
from PyUtils import GetDataFileType
from PyIdealMethods import IdealMethods


class SPF_DF(object):
    def __init__(self, data_file_name, data_orig, key_vars=[], categ_vars=[], proper_vars=[], other_patterns_to_keep=[], logger=None):
        '''
        Description:
            Init method for the class

        Arguments:
            data_file:    Commonality Data File to be used
            data_orig:    Origin of the data (CATTS, KS, other)
            key_vars:     (Optional) List with important variables defined by Ideal or commonality tool,
                          only this variables or it's related variables to be loaded by Pandas.
            categ_vars:   List of categorical values from IDEAL, this is to convert them to categorical once Pandas imports them
            proper_vars:  List of proper variables from IDEAL, this will make sure invalid variables are not kept in the dataframe
            other_patterns_to_keep:  Patterns that will be maintained based on important variables

        Auto-Execution:
            _FindCols():  Will find columns related to key_vars in the data_table, returns all columns if
                          key_vars list not passed
        '''
        self._logger = logger
        self._logger.debug('Running SPF_DF class init method')
        self._data_file_name = data_file_name
        self._delim = GetDataFileType(data_file_name, logger=self._logger)
        #######
        if data_orig.lower() == 'kitchen_sink' or data_orig.lower() == 'kitchensink' or data_orig.lower() == 'ks':
            self._data_origin = 'ks'
        elif data_orig.lower() == 'catts':
            self._data_origin = 'catts'
        else:
            self._data_origin = 'other'
        #######
        if type(key_vars) == list:
            self._key_vars = key_vars
        else:
            self._key_vars = []
        #######
        if type(categ_vars) == list:
            self._categ_vars = categ_vars
        else:
            self._categ_vars = []
        #######
        if type(proper_vars) == list:
            self._proper_vars = proper_vars
        else:
            self._proper_vars = []
        #######
        if type(other_patterns_to_keep) == list:
            self._other_patterns = other_patterns_to_keep
        else:
            self._other_vars_to_keep = []
        #######
        self._main_identifiers = ['VISUAL_ID', 'visual_id', 'die_index', 'wafer_id', 'identifier']
        self._data_loaded = False
        #######
        self._ks_key_patterns_list = [r'll#key#lot#<<<oper>>>',
                                      r'<<<oper>>>.*#out_date',
                                      r'<<<oper>>>.*#entity',
                                      r'<<<oper>>>.*tester_id',
                                      r'<<<oper>>>.*site_id',
                                      r'<<<oper>>>.*tester_interface_unit_id',
                                      r'<<<oper>>>.*device_tester_id',
                                      r'<<<oper>>>.*thermal_head_id',
                                      r'<<<oper>>>.*bonding_station',
                                      r'<<<oper>>>.*tester_interface_unit_id',
                                      r'<<<oper>>>.*#ceid',
                                      r'<<<oper>>>.*#media_._location',
                                      r'<<<oper>>>.*#carrier_.',
                                      r'<<<oper>>>.*#sort_.',
                                      r'<<<oper>>>.*#media_in_.',
                                      r'<<<oper>>>.*#entity_id',
                                      r'<<<oper>>>.*lane',
                                      r'<<<oper>>>.*head',
                                      r'<<<oper>>>.*#prgnm',
                                      r'<<<oper>>>.*#sort_.'
                                      ]
        self._catts_key_patterns_list = [r'^<<<oper>>>OUTDATE',
                                         '^<<<oper>>>.*_X',
                                         '^<<<oper>>>.*_Y',
                                         '^<<<oper>>>.*ENTITY',
                                         '^<<<oper>>>.*ENTITY-ENTITY_ID']
        #######
        self._FindCols()

    def Fix_Dates(self, outdate_pattern='DATE', date_format_list=['%Y-%m-%d %H:%M:%S']):
        '''
        Description: Coverts dates from string to date object
        Arguments:
                    outdate_pattern: string used to find columns with dates
                    date_format: default date format list to be used for date conversion
        '''
        self._logger.debug('Running Fix_Dates method')
        if self._data_loaded:
            if self._delim != 'parquet':
                print('\nConverting Dates...')
                col_list = self.df.columns
                for mycol in col_list:
                    if mycol.lower().find(outdate_pattern.lower()) >= 0:
                        print('   ' + mycol)
                        success = False
                        for curr_format in date_format_list:
                            if not success:
                                try:
                                    self.df[mycol] = pd.to_datetime(self.df[mycol], format=curr_format)
                                    success = True
                                except:
                                    pass
                        if not success:
                            try:
                                self.df[mycol] = pd.to_datetime(self.df[mycol])
                            except:
                                print("       -No valid date format found")
                                pass
        else:
            print('\n')
            self._logger.warning("=================================================================================================")
            self._logger.warning("WARNING:  Data has not been loaded, run Load_Data method first.  Dates won't be converted...")
            self._logger.warning("=================================================================================================")
            print('\n')

    def _FindPattern(self, all_cols, patterns):
        '''
        Description: Finds all columns matching a set of regular expression patterns
        Arguments:
                    all_cols: list of all columns in the dataframe
                    patterns: regular expression patterns to find
        '''
        self._logger.debug('Running _FindPattern method')
        list_final = []
        for i in patterns:
            list_final = list_final + [mycol for mycol in all_cols if len(re.findall(i.lower(), mycol.lower())) > 0]
        list_final = [i for i in list_final if i in self._all_cols]
        return list(set(list_final))

    def _FindCols(self):
        '''
        Description: Finds important columns to load based on the inputs in the init, method gets automatically
                     executed during the init
        Arguments:
                    None
        '''
        self._logger.debug('Running _FindPattern method')
        if self._delim != 'parquet':
            temp_df = pd.read_csv(self._data_file_name, sep=self._delim, nrows=1)
            self._all_cols = list(temp_df.columns)
        else:
            parquet_schema = pq.read_schema(self._data_file_name)
            self._all_cols = parquet_schema.names
        main_ident_final = [i for i in self._main_identifiers if i in self._all_cols]
        self._required_cols = main_ident_final + list(set(self._key_vars))
        self.custom_key_vars = self._FindPattern(self._all_cols, self._other_patterns)
        self._required_cols = self._required_cols + self.custom_key_vars
        if self._key_vars != []:
            oper_set = set()
            for currcol in self._key_vars:
                if self._data_origin == 'ks':
                    oper_found = False
                    oper_split = currcol.split('#')
                    if (len(oper_split) >= 3 and len(oper_split[2]) >= 4) or (len(oper_split) >= 4 and len(oper_split[3]) >= 4) or (len(oper_split) >= 5 and len(oper_split[4]) >= 4):
                        regexp = re.compile(r'\d{4,6}')
                        try:
                            myoper = regexp.findall(oper_split[2])[0]
                            int(myoper)
                            oper_set.add(myoper)
                            oper_found = True
                        except:
                            pass
                        if not oper_found:
                            try:
                                myoper = regexp.findall(oper_split[3])[0]
                                int(myoper)
                                oper_set.add(myoper)
                                oper_found = True
                            except:
                                pass
                        if not oper_found:
                            try:
                                myoper = regexp.findall(oper_split[4])[0]
                                int(myoper)
                                oper_set.add(myoper)
                                oper_found = True
                            except:
                                pass
                elif self._data_origin == 'catts':
                    oper_split = currcol[:4]
                    try:
                        int(oper_split)
                        oper_set.add(oper_split)
                    except:
                        pass
            oper_patterns = []
            if len(oper_set) > 0:
                for oper in list(oper_set):
                    oper = str(oper)
                    if self._data_origin == 'ks':
                        oper_patterns = oper_patterns + [i.replace('<<<oper>>>', oper) for i in self._ks_key_patterns_list]
                    elif self._data_origin == 'catts':
                        oper_patterns = oper_patterns + [i.replace('<<<oper>>>', oper) for i in self._catts_key_patterns_list]

            self._required_cols = self._required_cols + self._FindPattern(self._all_cols, oper_patterns)
            self._required_cols = [i for i in self._required_cols if i in self._all_cols]
        self._required_cols = list(set(self._required_cols))

    def LoadData(self):
        '''
        Description: Internal method that reads the data table using the columns key columns previously identified
        '''
        self._logger.debug('Running LoadData method')
        self._required_cols = list(set(self._required_cols))
        if self._delim != 'parquet':
            self.df = pd.read_csv(self._data_file_name, sep=self._delim, usecols=self._required_cols, low_memory=False)
        else:
            self.df = pd.read_parquet(self._data_file_name, columns=self._required_cols)
        if len(self._categ_vars) > 0:
            if sys.version_info.major == 3 and sys.version_info.minor >= 13:
                pd.set_option('future.no_silent_downcasting', True)
            for i in self._categ_vars:
                try:
                    self.df[i] = self.df[i].astype('object').replace(['nan', ' ', '  ', 'nat', '<NA>', ''], np.nan)
                except:
                    pass
        self._data_loaded = True

if __name__ == "__main__":

    pass