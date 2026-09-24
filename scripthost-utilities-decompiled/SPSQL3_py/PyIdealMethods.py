"""
-----------------------------------------------------
Procedure: PyIdealMethods.py      Date: 12/06/2018

Version History:
========================================================================================================================
| Date         |  Who        | Ver    |  Description                                                                   |
========================================================================================================================
| 12/11/2018   |  gcarmiol   |  1.0   |  Initial Version                                                               |
| 04/18/2019   |  gcarmiol   |  2.0   |  Version to adjust to AutoCommonality                                          |
|              |             |        |  Priors now set automatically for every output column with 5 or less           |
|              |             |        |  different values                                                              |
| 11/23/2020   |  gcarmiol   |  2.1   |  Change to return model importance together with variable importance           |
| 11/30/2020   |  gcarmiol   |  2.2   |  Fix bug when pulling score for feature selection                              |
| 01/15/2021   |  gcarmiol   |  2.3   |  Fix bug in which output can be mistakenly ignored later                       |
| 08/05/2021   |  gcarmiol   |  2.4   |  Fixes to allow the utility to work with KitchenSink data correctly            |
| 06/02/2022   |  gcarmiol   |  2.5   |  Small improvements, version to share with Catalyst Team                       |
| 07/12/2022   |  gcarmiol   |  2.6   |  Changes in logging to work with Catalyst execution                            |
| 07/25/2022   |  gcarmiol   |  2.7   |  Update that saves model created to the ide file                               |
| 07/26/2022   |  gcarmiol   |  2.8   |  Creation of Clean_Ideal_Objects method                                        |
| 10/25/2022   |  gcarmiol   |  3.0   |  Improved integration with KitchenSink                                         |
| 12/15/2022   |  gcarmiol   |  3.1   |  Fix bug where last variable tested is ignored                                 |
| 03/14/2023   |  gcarmiol   |  4.0   |  Added support for loading parquet files                                       |
| 05/19/2023   |  gcarmiol   |  4.1   |  Support for dropping opers after signal directly in Ideal and export of       |
|              |             |        |  competitor scores for tree with 1 surrogate                                   |
| 07/18/2024   |  gcarmiol   |  4.2   |  Corrected error message warning when no categorical variables were found      |
| 08/04/2025   |  gcarmiol   |  4.3   |  Clarified error message when not enough data to run Feature Selection         |
========================================================================================================================

Description:
============
Class containing methods to work with Ideal in Python 2.7.x/3.x.x


Methods:
========
Load_dataframe:               Loads Pandas DataFrame into Ideal Table, not favored right, use _Load_file method 
                              (method not being used, preferred method is to load from file)
_Load_file:                   Loads data from a text file into Ideal Table, called during instantiation of Ideal Class
define_output:                Method to assign a single variable as Output for Ideal in case a variable config file 
                              doesn't want to be used (not being used, preferred method is use variable config file)
variable_list:                --Property-- Method returns list of all variables loaded into Ideal
proper_variable_list:         --Property-- Method returns list of all proper variables (usable) loaded into Ideal
categorical_variable_list     --Property-- Method returns list of all proper categorical variables loaded into Ideal
_ReEval:                      Internal method that returns the matching variable list for a regular expression 
                              pattern from a list
_Fix_headers:                 Method to fix headers of variable configuration file in case they are mistaken
_Fix_values:                  Method to fix values of variable configuration file in case they are mistaken
_Fix_config_table:            Method that corrects the config_df for mis-spelled values
_Define_variables_from_file:  Method that accepts configuration file to define variable characteristics in Ideal,
                              regular expressions allowed instead of variable names
_Set_priors_equal:            Method to set equal priors for when "bad" units are few and we want to penalize
                              mis-classification of this units, this should only be used when trying find
                              good(0)/bad(1) units
Get_varimp_if:                Obtain variable importance using Ideal Forest (Ideal version of random forest),
                              this uses the default tree settings but sets 200 iterations and out of bag error
                              estimation with 60% random sub-sampling
Get_varimp_tree:              Obtain variable importance using single tree, this uses the default tree settings
                              but sets of 1 surrogate and cross validation.
Get_varimp_fs:                Obtain variable importance using Ideal Feature Selection, this uses the ideal default
                              settings but sets 20 series with 20 trees, 1000 trees for residuals, 6 iterations and
                              60% random sub-sampling
Save_table:                   Saves .ide/.idt file with any changes done in the code for interactive use in Ideal,
                              it will save variable configuration changes as well as the model created if .idt
                              is used
Clean_Ideal_Objects:          Deletes all objects created by Ideal
"""
__version__ = '4.2'

import os
import re
import sys
import logging
import numpy as np
import pandas as pd
import win32com.client
from PyUtils import GetDataFileType


class IdealMethods(object):
    def __init__(self, data_file, config_file, usePriors = 'Y', usefixedseed=True, seed=77777, if_maxiters=200, interactive_run=True, opers_to_drop=[], logger=None):
        '''
        Description:
            Init method for the class

        Arguments:
            data_file:          Commonality Data File to be used
            config_file:        Variable Configuration File with minimum one output defined
            usePriors:          This is an flag that tells the class if UsePriors should be used in the model
            usefixedseed:       This argument accepts True or False, if the value is not a boolean default "True" will 
                                be used. This argument is set to True so Ideal will accept a fixed seed passed by the code
            seed:               This is the seed that Ideal will use in its calculations, if not a integer default will 
                                be used
            if_maxiters:        This is the max iterations to be used in Ideal Forest method
            interactive_run:    Variable controls if the errors should exit execution
            opers_to_drop:      This is a list of operations to drop from analysis (use in KitchenSink patterns)
        '''
        self._logger = logger
        self._logger.debug('Running IdealMethods class init method')
        self._interactive_run = interactive_run
        self.ideal = win32com.client.dynamic.Dispatch('IdealData.IdealData')
        if type(usefixedseed) == bool:
            self.ideal.UseFixedSeed = usefixedseed
        else:
            self.ideal.UseFixedSeed = True
        if type(seed) == int:
            self.ideal.Seed = seed
        else:
            self.ideal.Seed = 77777
            print('\n')
            self._logger.info("Ideal seed needs to be an integer, using default of 77777")
            print('\n')
        if type(if_maxiters) == int:
            self._if_maxiters = if_maxiters
        else:
            self._if_maxiters = 200
            print('\n')
            self._logger.info("\nMaxIters needs to be an integer, using default of 200")
            print('\n')
        if usePriors == 'Y' or usePriors == 'N':
            self._usepriors = usePriors
        else:
            self._usepriors = 'Y'
            print('\n')
            self._logger.info("\nPriors can only be 'Y' or 'N', using default of 'Y'")
            print('\n')
        self._usepriors = usePriors
        self._output_defined = 'N'
        self._model = 'default'
        self._conditions_shown = False
        self.output_variables = []
        self._model_dict_if = {}
        self._model_dict_fs = {}
        self._model_dict_tree = {}
        self._model_dict_gbt = {}
        self._drop_opers = opers_to_drop
        try:
            self._Load_file(data_file)
        except:
            self._logger.error("===============================================================================")
            self._logger.error("Error found when loading to IDEAL.  Either IDEAL is not installed")
            self._logger.error("or the data provided can't be loaded.")
            self._logger.error("For IDEAL installation please visit http://ideal.intel.com/sitefiles/main.asp")
            self._logger.error("===============================================================================")
            print("\n")
            print("\n")
            if interactive_run:
                sys.exit(1)
        if not os.path.isfile(config_file):
            self._logger.error("==========================================================================")
            self._logger.error("The variable configuration file provide can't be found.  Exiting......")
            self._logger.error("==========================================================================")
            print("\n")
            print("\n")
            if interactive_run:
                sys.exit(1)
        try:
            self._Define_variables_from_file(config_file)
        except:
            self._logger.error("=============================================================================================")
            self._logger.error("Error in the IDEAL variable configuration file, please follow the link below and")
            self._logger.error("use the template provided.")
            self._logger.error("https://wiki.ith.intel.com/display/SQLPathFinder/Adding+Utilities#AddingUtilities-IDEAL")
            self._logger.error("=============================================================================================")
            print("\n")
            print("\n")
            if interactive_run:
                sys.exit(1)

    def _Load_file(self, filename):
        '''
        Description: Loads a data text file into Ideal
        Arguments:
                    filename: This is the file to load into Ideal
        '''
        self._logger.debug('Running _Load_file method')
        delim = GetDataFileType(filename, logger=self._logger)
        if delim != 'parquet':
            self.table = self.ideal.CreateTable('Delimited')
            if delim == '\t':
                self.table.SourceParameters.Delimiter = ord('\t')
            self.table.SourceParameters.MaxSamplesToScan = 0
        else:
            self.table = self.ideal.CreateTable('Parquet')
        self.table.SourceParameters.FileName = filename
        self.table.ReadData()

    def define_output(self, varname):
        '''
        Description: Sets a variable as Output in an Ideal table
        Arguments:
                    varname: Variable to be set as output
        '''
        self._logger.debug('Running define_output method')
        try:
            self.table.GetVariablesMethod(str(varname)).Role = 'Output'
            self._output_defined = 'Y'
            self.output_variables.append(varname)
            if self._usepriors == 'Y':
                self._Set_priors_equal(varname)
        except:  # Generic except used since any number of errors can be returned by the COM object
            print('\n')
            self._logger.warning("Couldn't change role of variable " + str(varname) + ', try checking variable name')

    @property
    def variable_list(self):
        '''
        Description: Returns the list of variables in an Ideal Table, set as a property in the class
        Arguments:
                    None
        '''
        self._logger.debug('Running variable_list method (Property)')
        varlist = []
        try:
            varlist = varlist + list(self.table.GetVariablesByRole(0))
            varlist = varlist + list(self.table.GetVariablesByRole(1))
            varlist = varlist + list(self.table.GetVariablesByRole(2))
            varlist = varlist + list(self.table.GetVariablesByRole(3))
        except:  # Generic except used since any number of errors can be returned by the COM object
            pass
        if len(varlist) > 0:
            return varlist
        else:
            print('\n')
            self._logger.warning('#####################################################################')
            self._logger.warning('Error: No variables found, load valid data table first')
            self._logger.warning('#####################################################################')
            return varlist

    @property
    def proper_variable_list(self):
        '''
        Description: Returns the list of proper variables in an Ideal Table.
                     Proper variables are variables that are usable by Ideal, variables that have 
                     single or too many values are determined to be not proper.
                     Method set as a property in the class
        Arguments:
                    None
        '''
        self._logger.debug('Running proper_variable_list method (Property)')
        varlist = []
        try:
            varlist = varlist + list(self.table.GetVariablesByRole(0))
            varlist = varlist + list(self.table.GetVariablesByRole(1))
            varlist = varlist + list(self.table.GetVariablesByRole(2))
            varlist = varlist + list(self.table.GetVariablesByRole(3))
        except:  # Generic except used since any number of errors can be returned by the COM object
            pass
        if len(varlist) > 0:
            varlist = [i for i in varlist if self.table.GetVariablesMethod(i).IsProper == 1]
        if len(varlist) > 0:
            return varlist
        else:
            print('\n')
            self._logger.warning('#####################################################################')
            self._logger.warning('Error: No proper variables found, load valid data table first')
            self._logger.warning('#####################################################################')
            return varlist

    @property
    def categorical_variable_list(self):
        '''
        Description: Returns the list of proper variables in an Ideal Table.
                     Proper variables are variables that are usable by Ideal, variables that have
                     single or too many values are determined to be not proper.
                     Method set as a property in the class
        Arguments:
                    None
        '''
        self._logger.debug('Running categorical_variable_list method (Property)')
        varlist = []
        try:
            varlist = varlist + list(self.table.GetVariablesByRole(0))
        except:  # Generic except used since any number of errors can be returned by the COM object
            pass
        if len(varlist) > 0:
            varlist = [i for i in varlist if self.table.GetVariablesMethod(i).ModelingType == 'Categorical' and self.table.GetVariablesMethod(i).IsProper == 1]
        return varlist

    def _ReEval(self, varlist, str_re):
        '''
        Description: Internal method that returns all the variables in a list that match a regular expression
        Arguments:
                    varlist:  List with variables to match with a regular expression
                    str_re:   String containing a regular expression to use for matching in the list
        '''
        self._logger.debug('Running _ReEval method')
        str_re = 'r"' + str_re + '"'
        returnlist = [i for i in varlist if len(re.findall(eval(str_re), i)) > 0]
        return returnlist

    def _Fix_headers(self, var):
        self._logger.debug('Running _Fix_headers method')
        var = str(var).lower()
        if var.find('name') >= 0:
            return 'name'
        elif (var.find('variable_type') >= 0) | (var.find('variable type') >= 0):
            return 'variable_type'
        elif var.find('split') >= 0:
            return 'split_type'
        elif var.find('role') >= 0:
            return 'role'
        else:
            return var

    def _Fix_values(self, var):
        '''
        Description:    Internal method that finds mis-spelled values and returns the correct one
        Arguments:      
            var:        Value to find close match to
        '''
        self._logger.debug('Running _Fix_values method')
        var = str(var).lower()
        if var.find('categor') >= 0:
            return 'Categorical'
        elif (var.find('numer') >= 0) | (var.find('number') >= 0):
            return 'Numeric'
        elif var.find('ord') >= 0:
            return 'Ordinal'
        elif (var.find('input') >= 0) & (var.find('out') >= 0):
            return 'Input/Output'
        elif var.find('input') >= 0:
            return 'Input'
        elif var.find('out') >= 0:
            return 'Output'
        elif var.find('ignore') >= 0:
            return 'Ignored'
        elif (var.find('unchecked') >= 0) |  \
             (var.find('un checked') >= 0) | \
             (var.find('un-checked') >= 0) | \
             (var.find('un_checked') >= 0):
            return '0'
        elif var.find('checked') >= 0:
            return '1'
        else:
            return var

    def _Fix_config_table(self, config_df):
        '''
        Description:    Internal method that corrects the config_df for mis-spelled values
        Arguments:      
            config_df:  Configuration Table
        '''
        self._logger.debug('Running _Fix_config_table method')
        col_headers = list(config_df.columns)
        col_headers = [self._Fix_headers(i) for i in col_headers]
        config_df.columns = col_headers
        return config_df

    def _Define_variables_from_file(self, config_file):
        '''
        Description: Method that gets a configuration file that contains the setup of the variable 
                     patterns for the Ideal table
        Arguments:
                    config_file:  Text file with the patterns to configure in the Ideal table
        '''
        self._logger.debug('Running _Define_variables_from_file method')
        if not os.path.isfile(config_file):
            print('\n')
            self._logger.error('#################################################')
            self._logger.error('Error: Configuration File not Found....exiting')
            self._logger.error('#################################################')
            if self._interactive_run:
                sys.exit(1)
        i_dlm = ','
        if os.path.splitext(config_file.lower())[1] == '.tab':
            i_dlm = '\t'
        if os.path.splitext(config_file.lower())[1] == '.asc':
            i_dlm = '|'
        if os.path.splitext(config_file.lower())[1] == '.plus':
            i_dlm = '+'
        config_df_all = pd.read_csv(config_file, sep=i_dlm, dtype=str)
        config_df_all = self._Fix_config_table(config_df_all)

        if len(self._drop_opers) > 0:
            final_drop_oper = ['#' + i + '#' for i in self._drop_opers]
            final_drop_oper = final_drop_oper + ['#' + i + '_' for i in self._drop_opers]
            final_drop_oper_df = pd.DataFrame({'name': final_drop_oper})
            final_drop_oper_df['variable_type'] = np.nan
            final_drop_oper_df['split_type'] = np.nan
            final_drop_oper_df['role'] = 'Ignored'
            config_df_all = pd.concat([config_df_all, final_drop_oper_df])

        proper_vars = self.proper_variable_list
        proper_var_lower = [i.lower() for i in self.proper_variable_list]

        # First executing for non output variables, then  last for output variables
        config_list = [config_df_all[~((config_df_all['role'] == r'Input/Output') | (config_df_all['role'] == 'Output'))],
                       config_df_all[(config_df_all['role'] == r'Input/Output') | (config_df_all['role'] == 'Output')]]

        for config_df in config_list:
            if len(config_df) > 0:
                for i in config_df.iterrows():
                    vars_to_change = self._ReEval(proper_var_lower, str(i[1]['name']).lower())
                    for j in vars_to_change:
                        j = str(j)
                        full_var = proper_vars[proper_var_lower.index(j)]
                        if 'variable_type' in i[1].keys():
                            if str(i[1]['variable_type']) != 'nan':
                                myval = self._Fix_values(i[1]['variable_type'])
                                if (myval == 'Categorical') | \
                                   (myval == 'Numeric') | \
                                   (myval == 'Ordinal'):
                                    try:
                                        self.table.GetVariablesMethod(full_var).ModelingType = myval
                                    except:  # Generic except used since any number of errors can be returned by the COM object
                                        print('\n')
                                        self._logger.warning("Couldn't change variable type of " + full_var + ', try checking variable name')
                                else:
                                    print('\n')
                                    self._logger.warning('Error in variable: ' + full_var)
                                    self._logger.warning('    The value "' + i[1]['variable_type'] + '" is not allowed as Variable Type')
                                    self._logger.warning('    The only values allowed are: "Categorical", "Numeric", or "Ordinal"')
                        if 'role' in i[1].keys():
                            if str(i[1]['role']) != 'nan':
                                myval = self._Fix_values(i[1]['role'])
                                if (myval == 'Input') | \
                                   (myval == 'Output') | \
                                   (myval == r'Input/Output') | \
                                   (myval == 'Ignored'):
                                    try:
                                        self.table.GetVariablesMethod(full_var).Role = myval
                                        if myval == 'Output' or myval == r'Input/Output':
                                            self._output_defined = 'Y'
                                            self.output_variables.append(full_var)
                                            # if self._usepriors == 'Y':
                                            #     self._Set_priors_equal(full_var)
                                    except:  # Generic except used since any number of errors can be
                                             # returned by the COM object
                                        print('\n')
                                        self._logger.warning("Couldn't change role of variable " + full_var + ', try checking variable name')
                                else:
                                    print('\n')
                                    self._logger.warning('Error in variable: ', full_var)
                                    self._logger.warning('    The value "' + str(i[1]['role']) + '" is not allowed as Role')
                                    self._logger.warning(r'    The only values allowed are: "Input", "Output", "Input/Output", or "Ignored"')
                        if 'split_type' in i[1].keys():
                            if str(i[1]['split_type']) != 'nan':
                                myval = self._Fix_values(i[1]['split_type'])
                                if (myval == '1') | \
                                   (myval == '0'):
                                    try:
                                        self.table.GetVariablesMethod(full_var).IsOneVsTheRest = myval
                                        self.table.GetVariablesMethod(full_var).IsInterval = myval
                                    except:  # Generic except used since any number of errors can be returned
                                             # by the COM object
                                        self._logger.warning("Couldn't change split type of variable " + full_var + ', try checking variable name')
                                else:
                                    print('\n')
                                    self._logger.warning('Error in variable: ', full_var)
                                    self._logger.warning('    The value "' + str(i[1]['split_type']) + '" is not allowed as Split Type')
                                    self._logger.warning('    The only values allowed are: "1" or "0"')

    def _Set_priors_equal(self, varname):
        '''
        Description:    Method that sets the table to use equal priors when creating the model in Ideal
        Arguments:
            varname:    Variable to set to use equal priors, will be made Categorical as well
        '''
        self._logger.debug('Running _Set_priors_equal method')
        varname = str(varname)
        try:
            self._model_dict_tree[varname] = self.table.CreateModel(varname, 'TREE')
            self._model_dict_tree[varname].SetProperty("TreeGrowingParameters.NumberOfSurrogatesForImportance", 1)
            self._model_dict_tree[varname].SetProperty("ErrorEstimation.UseCrossValidationErrorEstimation", True)
            self._model_dict_tree[varname].IsSavedToEnvironment = 1
            self._model_dict_gbt[varname] = self.table.CreateModel(varname, 'GBT')
            self._model_dict_gbt[varname].IsSavedToEnvironment = 1
            self._model_dict_if[varname] = self.table.CreateModel(varname, 'IF')
            self._model_dict_if[varname].IsSavedToEnvironment = 1
            self._model_dict_if[varname].SetProperty("General.MaxIterations", self._if_maxiters)
            self._model_dict_if[varname].SetProperty("General.MaxComplexity", True)
            self._model_dict_fs[varname] = self.table.CreateFeatureSelection(varname)
            self._model_dict_fs[varname].SetProperty("SeriesParameters.NumberOfSeries", 20)
            self._model_dict_fs[varname].SetProperty("SeriesParameters.NumberOfTreesInSeries", 20)
            self._model_dict_fs[varname].SetProperty("SeriesParameters.NumberOfTreesForResiduals", 1000)
            self._model_dict_fs[varname].SetProperty("SeriesParameters.MaxTreeDepth", 6)
            if len(set(self.table.GetVariablesMethod(varname).GetValues())) <= 5 and self._usepriors == 'Y' and self._model == 'default':
                self.table.GetVariablesMethod(varname).ModelingType = "Categorical"
                self.table.GetVariablesMethod(varname).CategoryPriors = 'Equal'
                self._model_dict_if[varname].SetPriorsMethod((1,1))
                self._model_dict_if[varname].UsePriors = 1
                self._model_dict_tree[varname].SetPriorsMethod((1,1))
                self._model_dict_tree[varname].UsePriors = 1
                self._model_dict_gbt[varname].SetPriorsMethod((1,1))
                self._model_dict_gbt[varname].UsePriors = 1
                self._logger.info('Variable: ' + str(varname) + ' priors set to equal')
            elif (len(set(self.table.GetVariablesMethod(varname).GetValues())) > 5 or self._usepriors == 'N') and  self._model == 'default':
                self._logger.info('Variable: ' + str(varname) + ' priors set to default')
        except:  # Generic except used since any number of errors can be returned by the COM object
            print('\n')
            self._logger.warning('Error creating model or setting priors in variable: ' + str(varname) + ' check if variable exists')

    def Get_varimp_if(self, output):
        '''
        Description: Method that run an Ideal Forest (random forest) and return the variable importance
        Arguments:
            output:    This is the name of the output variable to use for the Ideal Forest variable importance
        '''
        self._logger.debug('Running Get_varimp_if method')
        if self._output_defined == 'Y':
            if not self._conditions_shown:
                self._logger.info("Using:")
                self._logger.info("    -IdealForest")
                self._logger.info("    -200 Iterations")
                self._logger.info("    -Out of Bag Error Estimation with 60% Random Sub-Sampling\n")
                self._conditions_shown = True
            self._Set_priors_equal(output)
            if output in self.output_variables:
                self._logger.info(f'  -Creating model for variable: {str(output)}')
                print('\n')
                self._model_dict_if[output].Build()
                scores = self._model_dict_if[output].VariableImportance
                variable_importance = pd.Series(dtype='float64')
                for i in range(0, len(scores)):
                    variable_importance[self._model_dict_if[output].GetVariablesMethod(i).name] = scores[i]
                variable_importance.sort_values(ascending=False, inplace=True)
                model_score = self._model_dict_if[output].Score
                return variable_importance, model_score
            else:
                print('\n')
                self._logger.warning(f"This variable {output} has not been set as an Output, use 'define_output' method or set it in the variable configuration file")
        else:
            print('\n')
            self._logger.warning("Output Variable hasn't been defined, please define it with 'define_output' method or in the variable configuration file")

    def Get_varimp_tree(self, output):
        '''
        Description: Method that run an Single Tree with 1 surrogate and cross validation and return thes variable importance
        Arguments:
            output:    This is the name of the output variable to use for the Ideal Forest variable importance
        '''
        self._logger.debug('Running Get_varimp_tree method')
        if self._output_defined == 'Y':
            if not self._conditions_shown:
                self._logger.info("Using:")
                self._logger.info("     -Single Tree")
                self._logger.info("     -Number of Surrogates: 1")
                self._logger.info("     -Cross validation: True")
                self._conditions_shown = True
            self._Set_priors_equal(output)
            if output in self.output_variables:
                self._logger.info(f'Creating model for variable: {str(output)}')
                print('\n')
                self._model_dict_tree[output].Build()

                try:
                    output_list = []
                    split_var_list = []
                    weight_list = []

                    n_competitors = self._model_dict_tree[output].NumberOfCompetitiorSplitsInRoot
                    for split_id in range(n_competitors)[:20]:
                        output_list.append(output)
                        split = self._model_dict_tree[output].GetCompetitorSplitInRoot(split_id)
                        split_var = split.SplitVariable
                        split_var_list.append(split_var)
                        weight_list.append(round(split.Weight, 6))

                    df_split = pd.DataFrame({'output': output_list,
                                             'competitor': split_var_list,
                                             'weight': weight_list
                                             })
                except:
                    df_split = pd.DataFrame({'output': [],
                                             'competitor': [],
                                             'weight': []})

                scores = self._model_dict_tree[output].VariableImportance
                variable_importance = pd.Series(dtype='float64')
                for i in range(0, len(scores)):
                    variable_importance[self._model_dict_tree[output].GetVariablesMethod(i).name] = scores[i]
                variable_importance.sort_values(ascending=False, inplace=True)
                model_score = self._model_dict_tree[output].Score
                return variable_importance, model_score, df_split
            else:
                print('\n')
                self._logger.warning(f"This variable {output} has not been set as an Output, use 'define_output' method or set it in the variable configuration file")
        else:
            print('\n')
            self._logger.warning("Output Variable hasn't been defined, please define it with 'define_output' method or in the variable configuration file")

    def Get_varimp_fs(self, output):
        '''
        Description: Method that run an Ideal Feature Selection and return the variable importance
        Arguments:
            output:    This is the name of the output variable to use for the Ideal Forest variable importance
        '''
        self._logger.debug('Running Get_varimp_fs method')
        if self._output_defined == 'Y':
            if not self._conditions_shown:
                self._logger.info("Using:")
                self._logger.info("     -Ideal Feature Selection")
                self._logger.info("     -20 Series with 20 Trees")
                self._logger.info("     -1000 Trees for Residuals")
                self._logger.info("     -6 Iterations")
                self._logger.info("     -60% Random Sub-Sampling")
                self._conditions_shown = True
            self._model = 'fs'
            self._Set_priors_equal(output)
            if output in self.output_variables:
                self._logger.info(f'Creating model for variable: {str(output)}')
                print('\n')
                try:
                    self._model_dict_fs[output].Build()
                except win32com.client.pywintypes.com_error:
                    self._logger.error("===================================================================================")
                    self._logger.error("Not enough data to run Feature_Selection, try Ideal_Forest algorithm...exiting")
                    self._logger.error("===================================================================================")
                    print("\n")
                    if self._interactive_run:
                        sys.exit(1)
                except Exception as e:
                    self._logger.error(f'\n\n{e}\n\n')
                    if self._interactive_run:
                        sys.exit(1)
                scores = self._model_dict_fs[output].VariableImportance
                variable_importance = pd.Series(dtype='float64')
                for i in range(0, len(scores)):
                    variable_importance[self._model_dict_fs[output].GetVariablesMethod(i).name] = scores[i]
                variable_importance.sort_values(ascending=False, inplace=True)
                model_score = 'NA'
                return variable_importance, model_score
            else:
                print('\n')
                self._logger.warning("This variable has not been set as an Output, use 'define_output' method or set it in the variable configuration file")
        else:
            print('\n')
            self._logger.warning("Output Variable hasn't been defined, please define it with 'define_output' method or in the variable configuration file")

    def Save_table(self, path1):
        '''
        Description:    Saves the .ide file with the table updated and models run by the code
        Arguments:
            path1:      Path and filename of the .ide file to be created
        '''
        self._logger.debug('Running Save_table method')
        try:
            curr_dir = os.getcwd()
            self.table.Save(path1)
            os.chdir(curr_dir)
        except:
            print('\n')
            print('\n')
            self._logger.error('########################################################')
            self._logger.error('Error: .ide/.idt file could not be saved....exiting')
            self._logger.error('########################################################')
            print('\n')
            print('\n')
            print('\n')
            if self._interactive_run:
                sys.exit(1)

    def Clean_Ideal_Objects(self):
        '''
        Description:    Deletes all the COM objects created to prevent Ideal from crashing
        '''
        self._logger.debug('Running Clean_Ideal_Objects method')
        try:
            del self._model_dict_if
        except:
            pass
        #
        try:
            del self._model_dict_tree
        except:
            pass
        #
        try:
            del self._model_dict_fs
        except:
            pass
        #
        try:
            del self._model_dict_gbt
        except:
            pass
        del self.table
        del self.ideal




