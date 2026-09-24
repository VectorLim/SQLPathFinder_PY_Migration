"""
-----------------------------------------------------
Procedure: AutoCommonalitySPFUtility.py      Date: 12/06/2018

Version History:
========================================================================================================================
| Date         |  Who        | Ver    |  Description                                                                   |
========================================================================================================================
| 04/30/2019   |  gcarmiol   |  1.0   |  Initial Version                                                               |
| 08/31/2019   |  gcarmiol   |  2.0   |  Production Release Version                                                    |
| 11/11/2019   |  gcarmiol   |  2.1   |  Added CheckDateIdeal execution to remove MKL bug                              |
| 11/23/2020   |  gcarmiol   |  2.2   |  Changes to consolidate variable importance output file                        |
| 11/30/2020   |  gcarmiol   |  2.3   |  Fix bug when pulling score for feature selection                              |
| 01/15/2121   |  gcarmiol   |  2.4   |  Consolidation of outputs in single directory                                  |
| 08/05/2021   |  gcarmiol   |  2.5   |  Fixes to allow the utility to work with KitchenSink data correctly            |
| 05/26/2022   |  gcarmiol   |  3.0   |  Improvements to KitchenSink Commonality and enabling of import of the utility |
|              |             |        |  to allow execution from another program from an import                        |
| 07/12/2022   |  gcarmiol   |  3.1   |  Changed logging to make it compatible with Catalyst execution                 |
| 07/25/2022   |  gcarmiol   |  3.2   |  Skip charting for not proper output vars and display all model scores         |
| 07/26/2022   |  gcarmiol   |  3.3   |  Deleting Ideal objects to prevent crashes                                     |
| 10/25/2022   |  gcarmiol   |  4.0   |  Improved integration with KitchenSink data                                    |
| 11/10/2022   |  gcarmiol   |  4.1   |  Fix output file						                                       |
| 11/15/2022   |  vanatara   |  4.2   |  Fix to check for Env Var 'SQLPFAASHPC'	                                       |
| 03/14/2023   |  gcarmiol   |  5.0   |  Added better charting for WLA area and support for parquet files              |
| 05/19/2023   |  gcarmiol   |  5.1   |  Drop downstream operations directly in Ideal and save competitor table for st |
| 06/14/2024   |  gcarmiol   |  6.0   |  Addition of LEMS to obtain variable importance and interactions               |
| 07/31/2024   |  gcarmiol   |  6.1   |  Fixed no interactions and old python version bugs in LEMS                     |
| 08/01/2025   |  gcarmiol   |  6.2   |  Updates to fix bug where config.ini wasn't being used properly                |
| 08/12/2025   |  gcarmiol   |  6.3   |  Bug fix where older SandV would require id_col that was introduced later      |
| 08/21/2025   |  vanatara   |  6.4   |  Support Python 3.13                                                           |
========================================================================================================================

Description:
============
SQLPathFinder Python utility that creates an HTML commonality report

"""
__version__ = '6.4'

import os
import sys
import shutil
import logging
import warnings
import webbrowser
import pandas as pd
from datetime import datetime as dt
from SPF_DF import SPF_DF
from PyUtils import Drop_Downstream_Opers
from PyUtils import Drop_Opers_from_Model
from PyUtils import CleanHeaders
from PyUtils import GetDataFileType
from PyUtils import CheckDateIdeal
from PyUtils import BuildArgs
from PyIdealMethods import IdealMethods
from AutoComm_HTML_Report import AC_HTML_Report
try:
    from PyLEMS_Methods import Run_LEMS_and_Interaction_Charting
    from PyLEMS_Methods import var_config_to_lems_rules
    pylems_available = True
except:
    pylems_available = False

warnings.simplefilter("ignore")
pd.options.mode.chained_assignment = None


def _get_curr_time():
    return str(dt.today().replace(microsecond=0))


def _validate_inputs(exec_input, default_values):
    arg_dict = {}
    errors = []
    warnings = []
    ###################
    ##### Errors #####:
    ###################
    # data_file
    if 'data_file' in exec_input.keys() and os.path.isfile(exec_input['data_file']):
        arg_dict['data_file'] = exec_input['data_file']
        del exec_input['data_file']
    else:
        errors.append(f'Commonality data file "{exec_input["data_file"]}" not found')
    # var_conf
    if 'var_conf' in exec_input.keys() and os.path.isfile(exec_input['var_conf']):
        arg_dict['var_conf'] = exec_input['var_conf']
        del exec_input['var_conf']
    else:
        errors.append(f'Variable config file "{exec_input["var_conf"]}" not found')
    #####################
    ##### Warnings #####:
    #####################
    # data_origin
    if 'data_origin' in exec_input.keys() and exec_input['data_origin'].lower() in ['kitchensink', 'catts']:
        arg_dict['data_origin'] = exec_input['data_origin'].lower()
    else:
        warnings.append(f'Origin of data passed was "{exec_input["data_origin"]}" and can only be KitchenSink or CATTS, defaulting to "{default_values["data_origin"]}"')
        arg_dict['data_origin'] = default_values["data_origin"]
    if 'data_origin' in exec_input.keys():
        del exec_input['data_origin']
    # min_importance
    if 'min_importance' in exec_input.keys():
        try:
            int(exec_input['min_importance'])
            arg_dict['min_importance'] = int(exec_input['min_importance'])
        except:
            arg_dict['min_importance'] = default_values['min_importance']
            warnings.append(f'Minimum importance passed was "{exec_input["min_importance"]}" and needs to be integer, defaulting to "{default_values["min_importance"]}"')
    else:
        arg_dict['min_importance'] = default_values['min_importance']
    if 'min_importance' in exec_input.keys():
        del exec_input['min_importance']
    # columns
    if 'columns' in exec_input.keys():
        try:
            int(exec_input['columns'])
            arg_dict['columns'] = int(exec_input['columns'])
        except:
            arg_dict['columns'] = default_values['columns']
            warnings.append(f'Report columns passed was "{exec_input["columns"]}" needs to be integer, defaulting to "{default_values["columns"]}"')
    else:
        arg_dict['columns'] = default_values['columns']
    if 'columns' in exec_input.keys():
        del exec_input['columns']
    # ini_file
    if 'ini_file' in exec_input.keys() and os.path.isfile(exec_input['ini_file']):
        arg_dict['ini_file'] = exec_input['ini_file']
    elif 'ini_file' in exec_input.keys() and not os.path.isfile(exec_input['ini_file']) and exec_input['ini_file'] != 'None':
        arg_dict['ini_file'] = default_values['ini_file']
        warnings.append(f'Report ini file "{exec_input["ini_file"]}" is not valid and will be ignored')
    else:
        arg_dict['ini_file'] = default_values['ini_file']
    if 'ini_file' in exec_input.keys():
        del exec_input['ini_file']
    # report_name
    if 'report_name' in exec_input.keys():
        base_rep_dir = os.path.split(exec_input['report_name'])[0]
        rep_file_name = os.path.split(exec_input['report_name'])[1]
        rep_file_base = os.path.splitext(rep_file_name)[0]
        rep_file_ext = os.path.splitext(rep_file_name)[1]
        if base_rep_dir != '' and not os.path.isdir(base_rep_dir):
            os.makedirs(base_rep_dir)
            warnings.append(f'Report directory "{base_rep_dir}" not found and will be created')
        if rep_file_name == '':
            rep_file_base = os.path.splitext(default_values['report_name'])[0]
            rep_file_ext = os.path.splitext(default_values['report_name'])[1]
            warnings.append(f'Report name not provided, defaulting to "{default_values["report_name"]}"')
        if rep_file_ext not in ['.htm', '.html']:
            rep_file_ext = os.path.splitext(default_values['report_name'])[1]
            warnings.append(f'Report extension passed "{rep_file_ext}" can only be .htm or .html, using default of .htm')
        arg_dict['report_name'] = os.path.join(base_rep_dir, rep_file_base + rep_file_ext)
    else:
        warnings.append(f'Report name not provided, defaulting to "{default_values["report_name"]}"')
        rep_file_base = os.path.splitext(default_values['ini_file'])[0]
        arg_dict['report_name'] = default_values['report_name']
    if 'report_name' in exec_input.keys():
        del exec_input['report_name']
    # out_ide
    if 'out_ide' in exec_input.keys():
        base_ide_dir = os.path.split(exec_input['out_ide'])[0]
        ide_file_name = os.path.split(exec_input['out_ide'])[1]
        ide_file_base = os.path.splitext(ide_file_name)[0]
        ide_file_ext = os.path.splitext(ide_file_name)[1]
        if base_ide_dir != '' and not os.path.isdir(base_ide_dir):
            warnings.append(f'IDE file directory "{base_ide_dir}" not found and will be created')
            os.makedirs(base_ide_dir)
        if ide_file_name == '':
            ide_file_base = rep_file_base
            ide_file_ext = '.ide'
            warnings.append(f'IDE file name invalid, defaulting to {rep_file_base}.ide')
        if ide_file_ext not in ['.ide', '.idt']:
            ide_file_ext = '.ide'
            warnings.append(f'IDE file "{ide_file_ext}" needs to have .ide or .idt extension and will be changed to .ide')
        arg_dict['out_ide'] = os.path.join(base_ide_dir, ide_file_base + ide_file_ext)
    else:
        warnings.append(f'IDE file name not provided, defaulting to {rep_file_base}.ide')
        arg_dict['report_name'] = f'{rep_file_base}.ide'
    if 'out_ide' in exec_input.keys():
        del exec_input['out_ide']
    # imp_var_file
    if 'imp_var_file' in exec_input.keys() and exec_input['imp_var_file'].upper() != 'NONE':
        base_imp_dir = os.path.split(exec_input['imp_var_file'])[0]
        imp_file_name = os.path.split(exec_input['imp_var_file'])[1]
        imp_file_base = os.path.splitext(imp_file_name)[0]
        imp_file_ext = os.path.splitext(imp_file_name)[1]
        if base_imp_dir != '' and not os.path.isdir(base_imp_dir):
            warnings.append(f'Important variable data file directory "{base_imp_dir}" not found and will be created')
            os.makedirs(base_imp_dir)
        if imp_file_name == '':
            imp_file_base = rep_file_base
            imp_file_ext = '.csv'
            warnings.append(f'Important variable data file name invalid, defaulting to {rep_file_base}.csv')
        if imp_file_ext not in ['.csv', '.tab']:
            imp_file_ext = '.csv'
            warnings.append(f'Important variable data file type "{imp_file_ext}" can only be .csv or .tab, changing to .csv')
        arg_dict['imp_var_file'] = os.path.join(base_imp_dir, imp_file_base + imp_file_ext)
    else:
        arg_dict['imp_var_file'] = default_values['imp_var_file']
    if 'imp_var_file' in exec_input.keys():
        del exec_input['imp_var_file']
    # add_general_charts
    if 'add_general_charts' in exec_input.keys():
        if str(exec_input['add_general_charts']) in ['True', 'False']:
            arg_dict['add_general_charts'] = bool(exec_input['add_general_charts'])
        else:
            arg_dict['add_general_charts'] = default_values['add_general_charts']
            warnings.append(f'Add general charts variable passed "{exec_input["add_general_charts"]}" can only be True or False, defaulting to "{default_values["add_general_charts"]}"')
    else:
        arg_dict['add_general_charts'] = default_values['add_general_charts']
    if 'add_general_charts' in exec_input.keys():
        del exec_input['add_general_charts']
    # my_instance
    if 'my_instance' not in exec_input.keys():
        arg_dict['my_instance'] = default_values['my_instance']
    else:
        arg_dict['my_instance'] = str(exec_input['my_instance'])
    if 'my_instance' in exec_input.keys():
        del exec_input['my_instance']
    # report_title
    if 'report_title' not in exec_input.keys():
        arg_dict['report_title'] = default_values['report_title']
    # report_subtitle
    if 'report_subtitle' not in exec_input.keys():
        arg_dict['report_subtitle'] = default_values['report_subtitle']
    else:
        arg_dict['report_subtitle'] = exec_input['report_subtitle']
    if arg_dict['report_subtitle'] == 'default':
        arg_dict['report_subtitle'] = 'Created:  ' + dt.today().strftime('%Y-%m-%d %H:%M:%S')
    if 'report_subtitle' in exec_input.keys():
        del exec_input['report_subtitle']
    # model
    if 'model' not in exec_input.keys():
        arg_dict['model'] = default_values['model']
    # open_browser
    if 'open_browser' not in exec_input.keys():
        arg_dict['open_browser'] = default_values['open_browser']
    # Other input args
    for curr_key in exec_input.keys():
        arg_dict[curr_key] = exec_input[curr_key]
    if 'id_column' not in exec_input.keys():
        arg_dict['id_column'] = default_values['id_column']
    if 'response_operation' not in exec_input.keys():
        arg_dict['response_operation'] = default_values['response_operation']
    if 'un' not in exec_input.keys():
        arg_dict['un'] = default_values['un']
    if 'pw' not in exec_input.keys():
        arg_dict['pw'] = default_values['pw']
    ##### Returning Data #####
    return arg_dict, errors, warnings


def Screen_and_Visualize_Features(input_args, report_warnings='Y', interactive_run=True, logger=None):

    print('\n')
    logger.info(f'Starting Screen_and_Visualize Utility, v{__version__}, ' + _get_curr_time() + ' ...')
    print('\n')

    default_values = {'data_origin': 'KitchenSink',
     'model': 'Ideal_Forest',
     'ini_file': 'None',
     'out_ide': '',
     'id_column': '',
     'response_operation': '',
     'un': '',
     'pw': '',
     'min_importance': 5,
     'add_general_charts': True,
     'columns': 5,
     'report_name': 'SPFCommonality.htm',
     'report_title': 'Feature Screening Report',
     'report_subtitle': 'default',
     'imp_var_file': 'None',
     'my_instance': '9999',
     'open_browser': True}

    input_args = BuildArgs(input_args, default_values, ini_section="REPORT CONFIGURATION", require_default='N', logger=logger)

    myargs, input_errors, input_warnings = _validate_inputs(input_args, default_values)

    if len(input_errors) > 0:
        print('\n')
        logger.error('========================================================================================')
        logger.error("EXECUTION STOPPED: The errors below were found in the data input, exiting...")
        for curr_error in input_errors:
            logger.error(f"    -{curr_error}")
        logger.error('========================================================================================')
        print("\n")
        print("\n")
        if interactive_run:
            sys.exit(1)
    if len(input_warnings) > 0 and report_warnings.upper()[0] == 'Y':
        logger.warning("Corrections done to data input:")
        for curr_warn in input_warnings:
            logger.warning(f"    -{curr_warn}")
        print("\n")
        print("\n")

    catts_key_patterns = [
        '^FAB_PLANT$',
        r'^FAB_[^\d].*_PLANT$',
        '^SORT_LOT',
        '^SORT_X',
        '^SORT_Y',
        '^ASSM_LOT',
        '^ASSM_LOT',
        '^SLI',
        '^PRODUCT_FAMILY$',
        '^OWNER$']

    ks_key_patterns = ['ul#die#',
                       'ul#key#sub_sli',
                       'ul#key#sub_vend',
                       'ul#key#fab_lot',
                       'ul#key#wafer_id',
                       'ul#key#int_vend',
                       'ul#key#int_sli'
                       ]

    script_path = os.path.split(os.path.realpath(__file__))[0]

    #if 'SHServer' in os.environ.keys():
    if 'SHServer' in os.environ.keys() and os.getenv('SQLPFAASHPC') not in ['', None]:
        os.environ['IDEAL_LOG_USAGE'] = 'NO'
    else:
        CheckDateIdeal(interactive_run=interactive_run, logger=logger)

    data_file = myargs['data_file']
    data_origin = myargs['data_origin']
    config_file = myargs['var_conf']
    output_ide = myargs['out_ide']
    min_importance = myargs['min_importance']
    imp_var_file = myargs['imp_var_file']
    model = myargs['model']
    if min_importance >= 0.5:
        min_importance = min_importance / 100.0

    if data_origin.lower() == 'catts':
        myargs['custom_patterns'] = catts_key_patterns
    elif data_origin.lower() == 'kitchensink':
        myargs['custom_patterns'] = ks_key_patterns
    else:
        myargs['custom_patterns'] = []

    delim = GetDataFileType(data_file, interactive_run=interactive_run, logger=logger)
    report_name = myargs['report_name']
    report_name_no_path = os.path.split(report_name)[1]
    report_name_no_path = os.path.splitext(report_name_no_path)[0]
    report_path = os.path.split(report_name)[0]
    myargs['report_name_no_path'] = report_name_no_path

    if model.lower() == 'lems-rca' and not pylems_available:
        logger.info("\nWARNING: LEMS-RCA requires the latest version of Python in SQLPathFinder")
        logger.info("   -To install it go to Menu -> Tools -> Update/Install Python -> Default Version")
        logger.info("   -Installation is large and will take time in a slow connection")
        if interactive_run:
            sys.exit(1)

    if delim != 'parquet':
        data_file = CleanHeaders(data_file, interactive_run=interactive_run, logger=logger)
    else:
        if model.lower() == 'lems-rca':
            print("\n")
            logger.error("======================================================================")
            logger.error("Parquet data not supported in LEMS ...exiting")
            logger.error("======================================================================")
            print("\n")
            print("\n")
            if interactive_run:
                sys.exit(1)

    try:
        if os.path.isdir(report_name_no_path):
            logger.debug('Report directory exists, deleting...')
            shutil.rmtree(report_name_no_path)
    except:
        print("\n")
        logger.error("======================================================================")
        logger.error("Couldn't delete directory where report data will be stored ...exiting")
        logger.error("======================================================================")
        print("\n")
        print("\n")
        if interactive_run:
            sys.exit(1)

    list_of_ds_opers = []
    if myargs['response_operation'] != '':
        if myargs['id_column'] != '':
            myargs['id_column'] = 'visual_id_or_die_index'
        list_of_ds_opers = Drop_Downstream_Opers(myargs['id_column'], myargs['data_file'], myargs['response_operation'], logger=logger, un=myargs['un'], pw=myargs['pw'])

    if model.lower() == 'lems-rca' and pylems_available:
        var_conf_name = os.path.split(config_file)[1]
        lems_rules_file_name = os.path.join(report_path, os.path.splitext(var_conf_name)[0] + '_lems_rules.txt')
        var_config_to_lems_rules(config_file, lems_rules_file_name, opers_to_remove=list_of_ds_opers)

    if model.lower() != 'lems-rca':
        logger.info("Obtaining variables of importance with IDEAL...\n")
    myIdeal = IdealMethods(data_file, config_file, interactive_run=interactive_run, opers_to_drop=list_of_ds_opers, logger=logger)

    output_vars = myIdeal.output_variables
    categ_vars = myIdeal.categorical_variable_list
    proper_vars = myIdeal.proper_variable_list

    correct_output_var = False
    for curr_out in output_vars:
        if curr_out in proper_vars:
            correct_output_var = True

    if correct_output_var:
        results_dict = {}
        all_key_vars = set()

        all_var_imp = pd.DataFrame(columns=['output', 'variable', 'importance', 'model_score'])

        if model.lower() == 'decision_tree_1_surrogate':
            all_competitors = pd.DataFrame(columns=['output', 'competitor', 'weight'])

        for out_var in output_vars:
            if model.lower() == 'feature_selection':
                logger.debug('Running IDEAL feature selection')
                results, model_score = myIdeal.Get_varimp_fs(out_var)
                logger.debug('Running IDEAL Decision tree with 1 surrogate')
            elif model.lower() == 'decision_tree_1_surrogate':
                results, model_score, competitors = myIdeal.Get_varimp_tree(out_var)
                all_competitors = pd.concat([all_competitors, competitors])
            elif model.lower() == 'lems-rca' and pylems_available:
                logger.info("Obtaining variables of importance with LEMS and creating interaction charts...\n")
                results, model_score = Run_LEMS_and_Interaction_Charting(data_file, report_name_no_path, out_var, lems_rules_file_name, report_path, min_importance)
            else:
                logger.debug('Running IDEAL Random Forest')
                results, model_score = myIdeal.Get_varimp_if(out_var)
            if type(model_score) == float:
                model_score = round(model_score, 3)

            if myargs['response_operation'] != '':
                results = Drop_Opers_from_Model(list_of_ds_opers, results, logger=logger)

            df_curr_out = pd.DataFrame(results).reset_index()
            df_curr_out.rename(columns={'index': 'variable', 0: 'importance'}, inplace=True)
            df_curr_out['output'] = out_var
            df_curr_out['model_score'] = model_score
            df_curr_out = df_curr_out[['output', 'variable', 'importance', 'model_score']]
            df_curr_out['importance'] = df_curr_out['importance'].round(3)
            if len(all_var_imp) == 0:
                all_var_imp = df_curr_out
            else:
                all_var_imp = pd.concat([all_var_imp, df_curr_out])

            all_var_imp.loc[all_var_imp['importance'] >= min_importance, 'comments'] = 'Charts generated'
            all_var_imp.loc[all_var_imp['importance'] < min_importance, 'comments'] = 'Ignored -> Below importance limit'
            all_var_imp.to_csv(os.path.join(report_path, report_name_no_path + '_ml_model_results.csv'), index=False)
            all_var_imp[all_var_imp['importance'] >= min_importance].to_csv(os.path.join(report_path, report_name_no_path + '_var_imp.csv'), index=False)

            try:
                if model.lower() == 'decision_tree_1_surrogate':
                    all_competitors.to_csv(os.path.join(report_path, report_name_no_path + '_competitors.csv'), index=False)
            except:
                pass

            input_args['model_score'] = all_var_imp

            results = results[results >= min_importance]

            results_dict[out_var] = results
            all_key_vars.update(list(results.index))

        # print('\n')
        logger.info('Saving Ideal Table...')
        myIdeal.Save_table(output_ide)
        logger.info('Cleaning Ideal Objects...\n')
        myIdeal.Clean_Ideal_Objects()
        del myIdeal

        all_key_vars = list(all_key_vars) + list(output_vars)

        logger.info('\nLoading important variables...')
        spf_df = SPF_DF(data_file, data_origin, all_key_vars, categ_vars, proper_vars, myargs['custom_patterns'], logger=logger)
        spf_df.LoadData()
        if data_origin == 'kitchensink':
            spf_df.Fix_Dates(date_format_list=['%Y-%m-%d %H:%M:%S', '%Y/%m/%d %H:%M:%S', '%m/%d/%Y %H:%M:%S', '%m/%d/%Y %I:%M:%S %p'])
        elif data_origin == 'catts':
            spf_df.Fix_Dates(date_format_list=['%m/%d/%Y %I:%M:%S %p', '%m/%d/%Y %H:%M:%S', '%Y-%m-%d %H:%M:%S', '%Y/%m/%d %H:%M:%S'])
        else:
            spf_df.Fix_Dates()

        myargs['custom_key_vars'] = spf_df.custom_key_vars
        spf_df = spf_df.df

        try:
            if imp_var_file.upper() != 'NONE':
                delim = GetDataFileType(imp_var_file, check_file_exists='N', interactive_run=interactive_run, logger=logger)
                if delim in [',', '\t', '+']:
                    print('\n')
                    print('\n')
                    logger.info('Saving Data File with Important Variables...')
                    spf_df.to_csv(imp_var_file, sep=delim, index=False)
        except:
            print('\n')
            logger.warning("===========================================================================")
            logger.warning("Error saving data file with important variables, step will be skipped...")
            logger.warning("===========================================================================")
            print('\n')

        myargs['data_df'] = spf_df
        myargs['results_dict'] = results_dict

        if not (data_origin.lower() == 'kitchensink' or data_origin.lower() == 'catts'):
            myargs['add_general_charts'] = False

        print('\n')
        logger.info('Creating HTML Feature Screening Report...')
        myhtml = AC_HTML_Report(myargs, logger=logger)
        myhtml.Save_html()

        #if 'SHServer' not in os.environ.keys() and myargs['open_browser']:
        if 'SHServer' not in os.environ.keys() and os.getenv('SQLPFAASHPC') in ['', None] and myargs['open_browser']:
            webbrowser.open('file://' + os.path.realpath(report_name))

        print('\n')
        logger.info('Done at ' + str(dt.today()) + ' ...\n')

    else:
        logger.info("Output variable seems to have a single value or is invalid, analysis can't be run.  Exiting... " + str(dt.today()))


if __name__ == "__main__":

    logger = logging.getLogger(__name__)
    logger.setLevel(logging.INFO)
    formatter = logging.Formatter('%(message)s')
    logging_handler = logging.StreamHandler()
    logging_handler.setFormatter(formatter)
    logger.addHandler(logging_handler)

    if sys.version_info.major < 3:
        print("\n")
        logger.error("=================================================================================================")
        logger.error("This utility only runs with Python engine version 3.x")
        logger.error("Please install by going to:")
        logger.error("    SQLPathFinder -> Tools -> Update/Install Python -> Version 3.x")
        logger.error("Then select this version as default engine at:")
        logger.error("    SQLPathFinder -> Tools -> Configure SQLPathFinder -> Advanced -> Use Python 3.x for Engine")
        logger.error("=================================================================================================")
        print("\n")
        print("\n")
        sys.exit(1)

    if len(sys.argv) > 1:
        input_args = {
            'data_file': sys.argv[1],
            'var_conf': sys.argv[2],
            'out_ide': sys.argv[3],
            'report_name': sys.argv[4],
            'data_origin': sys.argv[5],
            'min_importance': sys.argv[6],
            'ini_file': sys.argv[7],
            'imp_var_file': sys.argv[8],
            'model': sys.argv[9],
            'my_instance': sys.argv[10]
        }
        if len(sys.argv) > 12:
            input_args['id_column'] = sys.argv[10]
            input_args['response_operation'] = sys.argv[11]
            input_args['my_instance'] = sys.argv[12]
    else:
        # input_args = {
        #     'data_file': r'commonality_data.tab',
        #     'var_conf': r'variable_config.csv',
        #     'data_origin': 'kitchensink',
        #     'id_column': 'visual_id_or_die_index',
        #     'response_operation': '',
        #     'un': '',
        #     'pw': '',
        #     'min_importance': 5,
        #     'columns': 5,
        #     'ini_file': 'None',
        #     'report_name': 'sqlpf_commonality.htm',
        #     'out_ide': r'sqlpf_commonality.ide',
        #     'imp_var_file': 'None',
        #     'add_general_charts': "True",
        #     'my_instance': '29974',
        #     'model': 'Ideal_Forest',
        #     'open_browser': True
        # }

        input_args = {
            'data_file': 'E:\\MAOATM\\Personal\\gcarmiol\\Commonality_WLA\\Commonality_data.parquet',
            'var_conf': 'E:\\MAOATM\\Personal\\gcarmiol\\Commonality_WLA\\variable_config.csv',
            'out_ide': 'Ideal_out.ide',
            'report_name': 'SPFCommonality.htm',
            'data_origin': 'KitchenSink',
            'min_importance': '1',
            'ini_file': 'None',
            'imp_var_file': 'None',
            'model': 'Ideal_Forest',
            'id_column': '',
            'response_operation': '',
            'my_instance': '14453',
            'open_browser': True}


    try:
        Screen_and_Visualize_Features(input_args, interactive_run=True, logger=logger)

    except Exception as e:
        if str(e).find('Failed to create new classifier. No relevant features found') >= 0:
            print("\n")
            logger.error('========================================================================================')
            logger.error("Data not big enough to run Feature_Selection, please try Random_Forest, exiting..." )
            logger.error('========================================================================================')
            print("\n")
            print("\n")
            sys.exit(1)
        else:

            print("\n")
            logger.error("Done. Error: (" + str(e) + "). Exiting with exit code 1 at " + str(dt.today()) + " ...\n" )
            print("\n")
            print("\n")
            sys.exit(1)

    sys.exit(0)

