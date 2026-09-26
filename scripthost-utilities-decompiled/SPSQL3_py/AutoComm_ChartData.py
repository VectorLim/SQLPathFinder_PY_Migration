"""
-------------------------------------------------------------------------
Class: AC_HTML_Report      Date: 05/01/2019

Version History:
========================================================================================================================
| Date         |  Who        | Ver    |  Description                                                                   |
========================================================================================================================
| 05/01/2019   |  gcarmiol   |  1.0   |  Initial Version                                                               |
| 08/31/2019   |  gcarmiol   |  2.0   |  Final Release for Production                                                  |
| 09/01/2020   |  gcarmiol   |  2.1   |  Added _fixstring method to all file names                                     |
| 01/15/2121   |  gcarmiol   |  2.2   |  Consolidation of outputs in single directory                                  |
| 08/05/2021   |  gcarmiol   |  2.3   |  Fixes to allow the utility to work with KitchenSink data correctly            |
| 07/12/2022   |  gcarmiol   |  2.4   |  Changes in logging to work with Catalyst execution                            |
| 07/25/2022   |  gcarmiol   |  2.5   |  Pass model scores to charting class                                           |
| 10/25/2022   |  gcarmiol   |  3.0   |  Improved integration with KitchenSink                                         |
| 03/14/2023   |  gcarmiol   |  4.0   |  Added better charting for WLA area                                            |
========================================================================================================================

Description:
============
Class containing the script to create the AutoCommonality Main HTML report

Methods:
========
__init__:       Initializes object and executes multiple functions to create HTML code
main_html_txt:  Variable that contains the text of the that will be written to the HTML
Save_html:      Saves the HTML file to the variable defined in inputs
"""
__version__ = '4.0'

import pandas as pd
import sys
import os
import re
import pickle
import logging
from .PyGraphingMethods import Basic_Charts
from .PyGraphingMethods import Correlation_Plot
from .PyGraphingMethods import Carrier_Map
from .PyGraphingMethods import Wafer_Map
from .PyUtils import FindPattern
from .PyUtils import IsRectangle


class AC_Chart_Data(object):
    def __init__(self, variable, tab, spf_df, report_dir, inputs, interactive_run=True, logger=None):
    # def __init__(self, variable, tab, spf_df, report_dir, data_origin, my_instance=''):
        '''
        Description:
            Init method for the class

        Arguments:
            inputs:       Dictionary with arguments passed by code creating the HTML report

        Auto-Execution:
                    _Load_text() - Adds the top of the html file, txt file needs to exist in directory
                    _Create_col_formatter() - Formats the total columns and spacing
                    _Create_Header() - Adds report title and subtitle
                    _Create_Tabs() - Creates the required tabs, one for each response variable
                    _Create_Tabs_Data() - Adds the graphs to each of the tabs
                    _Load_text() - Adds the bottom of the html file, txt file needs to exist in directory

        Object Variables:
                    main_html_txt - Contains str data of html code
        '''
        self._logger = logger
        self._logger.debug('Running AC_Chart_Data class init method')
        self._interactive_run = interactive_run
        self._inputs = inputs
        if not 'ini_file' in self._inputs:
            self._inputs['ini_file'] = 'none'
        self._var = variable
        self._tab = tab
        self._spf_df = spf_df
        self._spf_df_all = spf_df
        self._entity_pattern_list = [
            '.*ul#user#<<<oper>>>.*',
            '.*ul#attr#<<<oper>>>.*#entity-bonder-bondstage$',
            '.*ul#attr#<<<oper>>>.*#entity-bonder$',
            '.*ul#attr#<<<oper>>>.*#entity-lane_num$',
            '.*ul#metro#<<<oper>>>.*#entity-lane_num$',
            '.*ul#attr#<<<oper>>>.*#entity-lane_num$',
            '.*ul#metro#<<<oper>>>.*#entity-test_head_id$',
            '.*ul#attr#<<<oper>>>.*#entity_id$',
            '.*ul#metro#<<<oper>>>.*#tester_id$',
            '.*ul#test#<<<oper>>>.*#tester_id$',
            '.*ul#tdx#<<<oper>>>.*#entity_id$',
            '.*ul#metro#<<<oper>>>.*#fac_entity$',
            '.*ll#opinfo#<<<oper>>>.*#fac_entity$',
            '.*ll#opinfo#<<<oper>>>.*#entity$',
            '.*ll#opinfo#<<<oper>>>.*#entity$',
            '.*#<<<oper>>>.*#wafer_id$',
            '.*#<<<oper>>>.*#wafer_scribe$'
        ]
        self._my_instance = self._inputs['my_instance']
        self._response_type = self._Check_response()
        self._variable_type = self._Check_variable()
        self._report_dir = os.path.join(report_dir, self._inputs['report_name_no_path'], self._tab)
        self._data_origin = self._inputs['data_origin'].lower()
        self._script_path = os.path.split(os.path.realpath(__file__))[0]
        self._html_top_file = os.path.join(self._script_path, 'ac_data_page_top.txt')
        self._html_bott_file = os.path.join(self._script_path, 'ac_data_page_bott.txt')
        self.main_html_txt = ''
        self._Load_text(self._html_top_file)
        self.main_html_txt = self.main_html_txt + 'Charts for: ' + self._var + '</h2>\n\n'
        self.main_html_txt = self.main_html_txt + '<div class="container">\n'
        self.main_html_txt = self.main_html_txt + '	<div class="container-image">\n'
        self._Create_charts()
        self._Load_text(self._html_bott_file)

    def _Check_response(self):
        self._logger.debug('Running _Check_response method')
        self._spf_df = self._spf_df[~pd.isnull(self._spf_df[self._tab])]

        if self._spf_df[self._tab].dtype in ['int64', 'Int64'] and len(self._spf_df[self._tab].drop_duplicates()) == 2:
            return 'good-bad'
        elif self._spf_df[self._tab].dtype in ['float64', 'Float64', 'float'] and len(self._spf_df[self._tab].drop_duplicates()) == 2 and (0.0 in self._spf_df[self._tab].drop_duplicates().values) and (1.0 in self._spf_df[self._tab].drop_duplicates().values):
            return 'good-bad'
        elif self._spf_df[self._tab].dtype in ['float64', 'Float64', 'float'] or self._spf_df[self._tab].dtype in ['int64', 'Int64']:
            return 'cont'
        elif len(self._spf_df[self._tab].drop_duplicates()) == 0:
            return 'no-data'
        else:
            print("=============================================================")
            print("Response Variable '" + self._tab +  "' need to be continuous.  Exiting ...")
            print("=============================================================")
            sys.exit(1)

    def _Check_variable(self):
        self._logger.debug('Running _Check_variable method')
        if self._var != 'General_Charts':
            self._spf_df = self._spf_df[~pd.isnull(self._spf_df[self._var])]
            if self._spf_df[self._var].dtype in ['float64', 'Float64', 'float'] or self._spf_df[self._var].dtype in ['int64', 'Int64']:
                return 'cont'
            elif self._spf_df[self._var].dtype in ['object', 'string']:
                return 'discrete'
            elif self._spf_df[self._var].dtype == 'datetime64[ns]':
                return 'date'
            elif len(self._spf_df[self._var].drop_duplicates()) == 0:
                return 'no-data'
            else:
                print("=============================================================")
                print("Error with commonality variable '" + self._var + "'.  Exiting ...")
                print("=============================================================")
                sys.exit(1)
        else:
            return 'gen-charts'

    def _Find_die_cols_KS(self, mycols):
        self._logger.debug('Running _Find_die_cols_KS method')
        die_dict = {}
        for col in mycols:
            if len(re.findall(r'ul#die#.*x_location$', col)) > 0:
                if col.split('#')[2][0] == 'u':
                    mydie = col.split('#')[2]
                else:
                    mydie = 'u1'
                if mydie not in die_dict.keys():
                    die_dict[mydie] = {}
                die_dict[mydie]['x'] = col
            if len(re.findall(r'ul#die#.*y_location$', col)) > 0:
                if col.split('#')[2][0] == 'u':
                    mydie = col.split('#')[2]
                else:
                    mydie = 'u1'
                if mydie not in die_dict.keys():
                    die_dict[mydie] = {}
                die_dict[mydie]['y'] = col
        return die_dict

    def _Create_gencharts_KS(self):
        self._logger.debug('Running _Create_gencharts_KS method')
        charts_loaded = []
        input = {'data_df': self._spf_df,
                 'data_df_all': self._spf_df_all,
                 'resp': self._tab,
                 'ini_file': self._inputs['ini_file'],
                 'results_dict': self._inputs['results_dict']}
        key_vars = self._inputs['custom_key_vars']
        key_vars = [i for i in key_vars if i in self._spf_df.columns]
        for myvar in key_vars:
            if myvar != self._tab:
                input['com_col'] = myvar
                if len(self._spf_df[myvar].dropna().drop_duplicates()) > 1:
                    no_chart_cols = ['x_location', 'y_location', 'upi', 'minor_id', 'visual_id', 'fuse_id', 'die_index',
                                     'fab_wafer', 'partial_wafer_id', 'glass_wafer_seq_x_y', 'wafer_x', 'wafer_y',
                                     'wafer_seq_x', 'wafer_seq_y', 'wafer_seq_num', '#param#']
                    if len([i for i in no_chart_cols if myvar.find(i) >= 0]) == 0:
                        input['filename_simple'] = self._fixstring(myvar) + '_comm_plot.png'
                        input['filename'] = os.path.join(self._report_dir, input['filename_simple'])
                        myplot = Correlation_Plot(input, interactive_run=self._interactive_run, logger=self._logger)
                        myplot.Save_figure()
                        charts_loaded.append(input['filename_simple'])
                if 'ul#key#strip_xloc' in self._spf_df.columns and 'ul#key#strip_yloc' in self._spf_df.columns:
                    input['xvar'] = 'ul#key#strip_xloc'
                    input['yvar'] = 'ul#key#strip_yloc'
                    input['filename_simple'] = 'strip_map.png'
                    input['filename'] = os.path.join(self._report_dir, input['filename_simple'])
                    if not os.path.isfile(input['filename']):
                        myplot = Carrier_Map(input, interactive_run=self._interactive_run, logger=self._logger)
                        myplot.Save_figure()
                    charts_loaded.append(input['filename_simple'])
        die_positions = self._Find_die_cols_KS(self._spf_df.columns)
        regx = re.compile(r'^ul#die#.*x_location')
        regy = re.compile(r'^ul#die#.*y_location')
        key_vars = [i for i in self._spf_df.columns if ((len(regx.findall(i)) > 0) or (len(regy.findall(i)) > 0))]
        if len(key_vars) >= 0:
            for mydie in die_positions.keys():
                if len(die_positions[mydie].keys()) == 2:
                    input['data_df'] = self._spf_df
                    input['color_map'] = 'Blues'
                    input['xvar'] = die_positions[mydie]['x']
                    input['yvar'] = die_positions[mydie]['y']
                    input['charttitle'] = mydie + ' - All Fabs - Wafer Map for ' + self._tab
                    input['filename_simple'] = self._fixstring(mydie) + '_all_fabs_wafer_map.png'
                    input['filename'] = os.path.join(self._report_dir, input['filename_simple'])
                    if not os.path.isfile(input['filename']):
                        myplot = Wafer_Map(input, interactive_run=self._interactive_run, logger=self._logger)
                        myplot.Save_figure()
                    charts_loaded.append(input['filename_simple'])
                    fab_found = False
                    if input['xvar'].split(mydie)[0] + mydie + '#fab_plant_yr_ww' in input['data_df'].columns:
                        curr_fab_plant_col = input['xvar'].split(mydie)[0] + mydie + '#fab_plant_yr_ww'
                        fab_found = True
                    elif input['xvar'].split(mydie)[0] + mydie + '#fab_plant' in input['data_df'].columns:
                        curr_fab_plant_col = input['xvar'].split(mydie)[0] + mydie + '#fab_plant'
                        fab_found = True
                    if fab_found:
                        myfablist = self._get_subgraph_list(curr_fab_plant_col, input['xvar'], input['yvar'])
                        for curr_fab in myfablist:
                            mysplitdf = self._spf_df[self._spf_df[curr_fab_plant_col] == curr_fab]
                            input['data_df'] = mysplitdf
                            input['color_map'] = 'Oranges'
                            input['charttitle'] = mydie + ' - Fab: ' + curr_fab + ' - Wafer Map for ' + self._tab
                            input['filename_simple'] = self._fixstring(mydie) + '_' + curr_fab + '_wafer_map.png'
                            input['filename'] = os.path.join(self._report_dir, input['filename_simple'])
                            if not os.path.isfile(input['filename']):
                                myplot = Wafer_Map(input, interactive_run=self._interactive_run, logger=self._logger)
                                myplot.Save_figure()
                            charts_loaded.append(input['filename_simple'])
        return charts_loaded

    def _Create_opercharts_KS(self):
        self._logger.debug('Running _Create_opercharts_KS method')
        are_inter_charts = False
        inter_df_file = os.path.join(self._report_dir, 'Inter_chart_list.csv')
        if os.path.isfile(inter_df_file):
            inter_charts_df = pd.read_csv(inter_df_file)
            are_inter_charts = True
        charts_loaded = []
        input = {'data_df': self._spf_df,
                 'data_df_all': self._spf_df_all,
                 'resp': self._tab,
                 'ini_file': self._inputs['ini_file'],
                 'results_dict': self._inputs['results_dict']}
        best_die = ''
        add_die = ''
        best_die_found = False
        input['com_col'] = self._var
        input['filename'] = self._fixstring(self._var) + '_comm_plot.png'
        charts_loaded.append(input['filename'])
        input['filename'] = os.path.join(self._report_dir, input['filename'])
        if not os.path.isfile(input['filename']):
            myplot = Correlation_Plot(input, interactive_run=self._interactive_run, logger=self._logger)
            myplot.Save_figure()
        if self._variable_type == 'cont' and self._response_type == 'good-bad':
            input['xvar'] = self._tab
            input['yvar'] = self._var
            input['filename_simple'] = self._fixstring(self._var) + '_boxplot.png'
            input['filename'] = os.path.join(self._report_dir, input['filename_simple'])
            if not os.path.isfile(input['filename']):
                myplot = Basic_Charts(input, interactive_run=self._interactive_run, logger=self._logger)
                myplot.Boxplot()
                myplot.Save_figure()
            charts_loaded.append(input['filename_simple'])
            input['filename_simple'] = self._fixstring(self._var) + '_violinplot.png'
            input['filename'] = os.path.join(self._report_dir, input['filename_simple'])
            if not os.path.isfile(input['filename']):
                myplot2 = Basic_Charts(input, interactive_run=self._interactive_run, logger=self._logger)
                myplot2.Violinplot()
                myplot2.Save_figure()
            charts_loaded.append(input['filename_simple'])
        oper_split = self._var.split('#')
        plot_oper_charts = False
        regexp = re.compile(r'\d{4,6}')
        myoper = regexp.findall(self._var)
        if len(myoper) > 0:
            try:
                myoper = myoper[0]
                int(myoper)
                plot_oper_charts = True
            except:
                pass
        if self._var[-8:] == 'wafer_id' or self._var[-12:] == 'wafer_scribe' and not plot_oper_charts:
            regexp = re.compile(r'^u\d{1,2}$|^u\d{1,2}_u\d{1,2}$')
            for curr_val in oper_split:
                mydie = regexp.findall(curr_val)
                if len(mydie) > 0:
                    myoper = mydie[0]
                    plot_oper_charts = True
        out_var_found = False
        is_rect_carrier = True
        if plot_oper_charts:
            try:
                if oper_split[0] + '#ll#opinfo#' + myoper + '#out_date' in self._spf_df.columns:
                    outdate_var = oper_split[0] + '#ll#opinfo#' + myoper + '#out_date'
                    out_var_found = True
                elif 'll#opinfo#' + myoper + '#out_date' in self._spf_df.columns:
                    outdate_var = 'll#opinfo#' + myoper + '#out_date'
                    out_var_found = True
                if out_var_found:
                    if self._spf_df[outdate_var].dtype == 'datetime64[ns]':
                        if plot_oper_charts and self._variable_type == 'discrete':
                            try:
                                input['xvar'] = outdate_var
                                input['yvar'] = self._tab
                                input['groupvar'] = self._var
                                input['filename_simple'] = self._fixstring(self._var) + '_time_trend_disc.png'
                                input['filename'] = os.path.join(self._report_dir, input['filename_simple'])
                                if not os.path.isfile(input['filename']):
                                    mytt = Basic_Charts(input, interactive_run=self._interactive_run, logger=self._logger)
                                    mytt.Timetrend()
                                    mytt.Save_figure()
                                charts_loaded.append(input['filename_simple'])
                            except:
                                pass
                        if plot_oper_charts and self._variable_type == 'cont':
                            try:
                                input['xvar'] = outdate_var
                                input['yvar'] = self._var
                                input['groupvar'] = 'default'
                                input['filename_simple'] = self._fixstring(self._var) + '_time_trend_cont.png'
                                input['filename'] = os.path.join(self._report_dir, input['filename_simple'])
                                if 'resp_in_pct' in input:
                                    old_val = input['resp_in_pct']
                                    input['resp_in_pct'] = False
                                    value_existed = True
                                else:
                                    input['resp_in_pct'] = False
                                    value_existed = False
                                if not os.path.isfile(input['filename']):
                                    mytt = Basic_Charts(input, interactive_run=self._interactive_run, logger=self._logger)
                                    mytt.Timetrend()
                                    mytt.Save_figure()
                                if value_existed:
                                    input['resp_in_pct'] = old_val
                                else:
                                    del input['resp_in_pct']
                                charts_loaded.append(input['filename_simple'])
                            except:
                                pass
                        if plot_oper_charts and self._variable_type == 'date':
                            try:
                                input['xvar'] = outdate_var
                                input['yvar'] = self._tab
                                input['groupvar'] = 'default'
                                input['filename_simple'] = self._fixstring(self._var) + '_time_trend_date.png'
                                input['filename'] = os.path.join(self._report_dir, input['filename_simple'])
                                if not os.path.isfile(input['filename']):
                                    mytt = Basic_Charts(input, interactive_run=self._interactive_run, logger=self._logger)
                                    mytt.Timetrend()
                                    mytt.Save_figure()
                                charts_loaded.append(input['filename_simple'])
                            except:
                                pass
                carrier_x = FindPattern(self._spf_df.columns, ['.*ul#attr#' + myoper + '.*#(media_x_location|carrier_x|media_in_x|sort_x|wafer_x)',
                                                               '.*ul#metro#' + myoper + '.*#(media_x_location|carrier_x|media_in_x|sort_x|wafer_x)',
                                                               '.*ul#tdx#' + myoper + '.*#(media_x_location|carrier_x|media_in_x|sort_x|wafer_x)',
                                                               '.*ul#test#' + myoper + '.*#(media_x_location|carrier_x|media_in_x|last#sort_x|wafer_x)',
                                                               'ul#die#' + myoper + '#wafer_x',
                                                               'ul#die#' + myoper + '#x_location'
                                                               ],
                                        interactive_run=self._interactive_run, logger=self._logger)
                carrier_y = FindPattern(self._spf_df.columns, ['.*ul#attr#' + myoper + '.*#(media_y_location|carrier_y|media_in_y|sort_y|wafer_y)',
                                                               '.*ul#metro#' + myoper + '.*#(media_y_location|carrier_y|media_in_y|sort_y|wafer_y)',
                                                               '.*ul#tdx#' + myoper + '.*#(media_y_location|carrier_y|media_in_y|sort_y|wafer_y)',
                                                               '.*ul#test#' + myoper + '.*#(media_y_location|carrier_y|media_in_y|last#sort_y|wafer_y)',
                                                               'ul#die#' + myoper + '#wafer_y',
                                                               'ul#die#' + myoper + '#y_location'
                                                               ],
                                        interactive_run=self._interactive_run, logger=self._logger)
                die_positions = set()
                regexp = re.compile(r'^u\d{1,2}$|^u\d{1,2}_u\d{1,2}$')
                if len(carrier_x) > 1:
                    for curr_col in carrier_x:
                        vals = curr_col.split('#')
                        for curr_val in vals:
                            mydie = regexp.findall(curr_val)
                            if len(mydie) > 0:
                                die_positions.add(mydie[0])
                    priority_list = ['u1_u1', 'u2_u1', 'u3_u1', 'u1', 'u2', 'u3', 'u4', 'u5']
                    best_die_found = False
                    for curr_die in priority_list:
                        if not best_die_found:
                            if curr_die in die_positions:
                                best_die = curr_die
                                best_die_found = True
                    if best_die_found:
                        carrier_x = [i for i in carrier_x if i.find('#' + best_die + '#') >= 0]
                    carrier_x.sort()
                if len(carrier_y) > 1:
                    if best_die_found:
                        carrier_y = [i for i in carrier_y if i.find('#' + best_die + '#') >= 0]
                    carrier_y.sort()
                if self._var[-8:] == 'wafer_id' or self._var[-12:] == 'wafer_scribe':
                    carrier_x_test = [i for i in carrier_x if (i.find('sort_x') > 0 or i.find('wafer_x') > 0 or i.find('x_location') > 0)]
                    carrier_y_test = [i for i in carrier_y if (i.find('sort_y') > 0 or i.find('wafer_y') > 0 or i.find('y_location') > 0)]
                    if len(carrier_x_test) > 0 and len(carrier_y_test) > 0:
                        carrier_x = carrier_x_test
                        carrier_y = carrier_y_test
                if len(carrier_x) >= 1 and len(carrier_y) >= 1:
                    input['xvar'] = carrier_x[0]
                    input['yvar'] = carrier_y[0]
                    is_rect_carrier = IsRectangle(self._spf_df, input['xvar'], input['yvar'], logger=self._logger)
                    if best_die_found and best_die != '':
                        add_die = '_' + best_die
                    if is_rect_carrier:
                        input['filename_simple'] = self._fixstring(myoper) + add_die + '_media_map.png'
                        input['filename'] = os.path.join(self._report_dir, input['filename_simple'])
                    else:
                        input['filename_simple'] = self._fixstring(myoper) + add_die + '_wafer_map.png'
                        input['filename'] = os.path.join(self._report_dir, input['filename_simple'])
                    if not os.path.isfile(input['filename']):
                        if is_rect_carrier:
                            myplot = Carrier_Map(input, interactive_run=self._interactive_run, logger=self._logger)
                        else:
                            myplot = Wafer_Map(input, interactive_run=self._interactive_run, logger=self._logger)
                        myplot.Save_figure()
                    charts_loaded.append(input['filename_simple'])
                    myentity = 'NOENTITY'
                    if self._var[-6:-1] == 'sort_' or self._var[-9:] == '_location' or self._var[-8:] == 'wafer_id' or self._var[-12:] == 'wafer_scribe':
                        entity_pattern_list = [i.replace('<<<oper>>>', myoper) for i in self._entity_pattern_list[-2:]]
                        wafer_id_map = True
                    else:
                        entity_pattern_list = [i.replace('<<<oper>>>', myoper) for i in self._entity_pattern_list]
                        wafer_id_map = False
                    for curr_entity_patt in entity_pattern_list:
                        if myentity == 'NOENTITY':
                            myentity_list = FindPattern(self._spf_df.columns, [curr_entity_patt], interactive_run=self._interactive_run, logger=self._logger)
                            if len(myentity_list) > 0:
                                myentity = myentity_list[0]
                    if myentity != 'NOENTITY':
                        mylist = self._get_subgraph_list(myentity, input['xvar'], input['yvar'])
                        if best_die_found:
                            add_die = best_die + '_'
                        else:
                            add_die = ''
                        for curr_entity in mylist:
                            mysplitdf = self._spf_df[self._spf_df[myentity] == curr_entity]
                            input['data_df'] = mysplitdf
                            input['color_map'] = 'Oranges'
                            input['charttitle'] = add_die + 'Media map for {}\n{}'.format(self._tab, curr_entity)
                            if wafer_id_map:
                                input['filename_simple'] = add_die + 'wafer_map_for_' + self._fixstring(curr_entity) + '.png'
                            else:
                                input['filename_simple'] = self._fixstring(myoper) + '_' + add_die + 'media_map_' + self._fixstring(curr_entity) + '.png'
                            input['filename'] = os.path.join(self._report_dir, input['filename_simple'])
                            if not os.path.isfile(input['filename']):
                                if not is_rect_carrier:
                                    myplot = Wafer_Map(input, interactive_run=self._interactive_run, logger=self._logger)
                                    myplot.Save_figure()
                                else:
                                    myplot = Carrier_Map(input, interactive_run=self._interactive_run, logger=self._logger)
                                    myplot.Save_figure()
                            charts_loaded.append(input['filename_simple'])
                    if self._variable_type == 'cont':
                        for curr_entity in mylist:
                            mysplitdf = self._spf_df[self._spf_df[myentity] == curr_entity]
                            input['data_df'] = mysplitdf
                            input['resp'] = self._var
                            input['color_map'] = 'Greens'
                            input['charttitle'] = add_die + 'Media map for {}\n{}'.format(self._var, curr_entity)
                            if 'resp_in_pct' in input:
                                old_val = input['resp_in_pct']
                                input['resp_in_pct'] = False
                                value_existed = True
                            else:
                                input['resp_in_pct'] = False
                                value_existed = False
                            input['wafer_param'] = True
                            if is_rect_carrier:
                                input['filename_simple'] = self._fixstring(self._var) + '_' + add_die + 'media_map_for_' + self._fixstring(curr_entity) + '.png'
                                input['filename'] = os.path.join(self._report_dir, input['filename_simple'])
                            else:
                                input['filename_simple'] = self._fixstring(self._var) + '_' + add_die + 'wafer_map_for_' + self._fixstring(curr_entity) + '.png'
                                input['filename'] = os.path.join(self._report_dir, input['filename_simple'])
                            if not os.path.isfile(input['filename']):
                                if is_rect_carrier:
                                    myplot = Carrier_Map(input, interactive_run=self._interactive_run, logger=self._logger)
                                else:
                                    myplot = Wafer_Map(input, interactive_run=self._interactive_run, logger=self._logger)
                                myplot.Save_figure()
                            del_vals = ['wafer_param', 'estimator']
                            for curr_del in del_vals:
                                try:
                                    del input[curr_del]
                                except:
                                    pass
                            charts_loaded.append(input['filename_simple'])
                            if value_existed:
                                input['resp_in_pct'] = old_val
                            else:
                                del input['resp_in_pct']
            except:
                pass
        if ((self._var.find('ul#die#') >= 0) or (self._var.find('#sort_') >= 0) or (self._var.find('#wafer_') >= 0) or ('ul#key#die_index' in self._spf_df.columns)) and is_rect_carrier:
            try:
                wafer_x_col_found = False
                wafer_y_col_found = False
                vals = self._var.split('#')
                regexp = re.compile(r'(^u\d{1,2}_u\d{1,2}$|^u\d{1,2}$)')
                mydie = ''
                for curr_val in vals:
                    mydie_test = regexp.findall(curr_val)
                    if len(mydie_test) > 0:
                        mydie = mydie_test[0]
                if 'ul#die#' + mydie + '#x_location' in self._spf_df.columns:
                    wafer_x_col = 'ul#die#' + mydie + '#x_location'
                    wafer_x_col_found = True
                elif 'patch#ul#die#' + mydie + '#x_location' in self._spf_df.columns:
                    wafer_x_col = 'patch#ul#die#' + mydie + '#x_location'
                    wafer_x_col_found = True
                elif 'ul#die#' + mydie + '#fab_wafer_x_location' in self._spf_df.columns:
                    wafer_x_col = 'ul#die#' + mydie + '#fab_wafer_x_location'
                    wafer_x_col_found = True
                elif 'patch#ul#die#' + mydie + '#fab_wafer_x_location' in self._spf_df.columns:
                    wafer_x_col = 'patch#ul#die#' + mydie + '#fab_wafer_x_location'
                    wafer_x_col_found = True
                elif 'ul#key#x_location' in self._spf_df.columns:
                    wafer_x_col = 'ul#key#x_location'
                    wafer_x_col_found = True
                elif self._var[-6:] == 'sort_x':
                    wafer_x_col = self._var
                    wafer_x_col_found = True
                elif self._var[-7:] == 'wafer_x':
                    wafer_x_col = self._var
                    wafer_x_col_found = True
                if 'ul#die#' + mydie + '#y_location' in self._spf_df.columns:
                    wafer_y_col = 'ul#die#' + mydie + '#y_location'
                    wafer_y_col_found = True
                elif 'patch#ul#die#' + mydie + '#y_location' in self._spf_df.columns:
                    wafer_y_col = 'patch#ul#die#' + mydie + '#y_location'
                    wafer_y_col_found = True
                elif 'ul#die#' + mydie + '#fab_wafer_y_location' in self._spf_df.columns:
                    wafer_y_col = 'ul#die#' + mydie + '#fab_wafer_y_location'
                    wafer_y_col_found = True
                elif 'patch#ul#die#' + mydie + '#fab_wafer_y_location' in self._spf_df.columns:
                    wafer_y_col = 'patch#ul#die#' + mydie + '#fab_wafer_y_location'
                    wafer_y_col_found = True
                elif 'ul#key#y_location' in self._spf_df.columns:
                    wafer_y_col = 'ul#key#y_location'
                    wafer_y_col_found = True
                elif self._var[-6:] == 'sort_y':
                    wafer_y_col = self._var
                    wafer_y_col_found = True
                elif self._var[-7:] == 'wafer_y':
                    wafer_y_col = self._var
                    wafer_y_col_found = True
                if wafer_x_col_found and wafer_y_col_found:
                    input['data_df'] = self._spf_df
                    input['xvar'] = wafer_x_col
                    input['yvar'] = wafer_y_col
                    input['charttitle'] = mydie + ' - All Fabs - Wafer Map for ' + self._tab
                    input['filename_simple'] = self._fixstring(mydie) + '_all_fabs_wafer_map.png'
                    input['filename'] = os.path.join(self._report_dir, input['filename_simple'])
                    if not os.path.isfile(input['filename']):
                        myplot = Wafer_Map(input, interactive_run=self._interactive_run, logger=self._logger)
                        myplot.Save_figure()
                    charts_loaded.append(input['filename_simple'])

                    entity_pattern_list = [i.replace('<<<oper>>>', myoper) for i in self._entity_pattern_list]

                    myentity = 'NOENTITY'
                    for curr_entity_patt in entity_pattern_list:
                        if myentity == 'NOENTITY':
                            myentity_list = FindPattern(self._spf_df.columns, [curr_entity_patt], interactive_run=self._interactive_run, logger=self._logger)
                            if len(myentity_list) > 0:
                                myentity = myentity_list[0]

                    classif_col = "ul#die#" + mydie + "#classification"
                    if classif_col not in self._spf_df.columns and myentity != 'NOENTITY':
                        classif_col = myentity

                    if classif_col in self._spf_df.columns:
                        mylist = self._get_subgraph_list(classif_col, input['xvar'], input['yvar'])
                        for curr_classif in mylist:
                            mysplitdf = self._spf_df[self._spf_df[classif_col] == curr_classif]
                            input['data_df'] = mysplitdf
                            input['charttitle'] = 'Wafer map for {}\n{}'.format(self._tab, curr_classif)
                            input['filename_simple'] = self._fixstring(mydie) + '_wafer_map_' + self._fixstring(curr_classif) + '.png'
                            input['filename'] = os.path.join(self._report_dir, input['filename_simple'])
                            if not os.path.isfile(input['filename']):
                                myplot = Wafer_Map(input, interactive_run=self._interactive_run, logger=self._logger)
                                myplot.Save_figure()
                            charts_loaded.append(input['filename_simple'])
                    if self._variable_type == 'cont':
                        input['data_df'] = self._spf_df_all
                        input['resp'] = self._var
                        input['color_map'] = 'Greens'
                        input['charttitle'] = 'Wafer map for\n{}'.format(self._var)
                        input['filename_simple'] = self._fixstring(self._var) + '_wafer_map.png'
                        input['filename'] = os.path.join(self._report_dir, input['filename_simple'])
                        if 'resp_in_pct' in input:
                            old_val = input['resp_in_pct']
                            input['resp_in_pct'] = False
                            value_existed = True
                        else:
                            input['resp_in_pct'] = False
                            value_existed = False
                        input['wafer_param'] = True
                        # input['estimator'] = 'median'
                        if not os.path.isfile(input['filename']):
                            myplot = Wafer_Map(input, interactive_run=self._interactive_run, logger=self._logger)
                            myplot.Save_figure()
                        del_vals = ['wafer_param', 'estimator']
                        for curr_del in del_vals:
                            try:
                                del input[curr_del]
                            except:
                                pass
                        charts_loaded.append(input['filename_simple'])
                        if value_existed:
                            input['resp_in_pct'] = old_val
                        else:
                            del input['resp_in_pct']

                        for curr_classif in mylist:
                            mysplitdf = self._spf_df_all[self._spf_df_all[classif_col] == curr_classif]
                            input['data_df'] = mysplitdf
                            input['resp'] = self._var
                            input['color_map'] = 'Greens'
                            input['charttitle'] = 'Wafer map for\n{}\n{}'.format(self._var, curr_classif)
                            input['filename_simple'] = self._fixstring(self._var) + '_wafer_map_' + str(curr_classif) +'.png'
                            input['filename'] = os.path.join(self._report_dir, input['filename_simple'])
                            if 'resp_in_pct' in input:
                                old_val = input['resp_in_pct']
                                input['resp_in_pct'] = False
                                value_existed = True
                            else:
                                input['resp_in_pct'] = False
                                value_existed = False
                            input['wafer_param'] = True
                            if not os.path.isfile(input['filename']):
                                myplot = Wafer_Map(input, interactive_run=self._interactive_run, logger=self._logger)
                                myplot.Save_figure()
                            del_vals = ['wafer_param', 'estimator']
                            for curr_del in del_vals:
                                try:
                                    del input[curr_del]
                                except:
                                    pass
                            charts_loaded.append(input['filename_simple'])
                            if value_existed:
                                input['resp_in_pct'] = old_val
                            else:
                                del input['resp_in_pct']
            except:
                pass

        if are_inter_charts:
            curr_inter = inter_charts_df[(inter_charts_df['param1'] == self._var) | (inter_charts_df['param2'] == self._var)]
            if len(curr_inter) > 0:
                curr_inter.loc[curr_inter['param1'] >= curr_inter['param2'], 'p1'] = curr_inter['param1']
                curr_inter.loc[curr_inter['param1'] >= curr_inter['param2'], 'p2'] = curr_inter['param2']
                curr_inter.loc[curr_inter['param1'] < curr_inter['param2'], 'p1'] = curr_inter['param2']
                curr_inter.loc[curr_inter['param1'] < curr_inter['param2'], 'p2'] = curr_inter['param1']
                del curr_inter['param1']
                del curr_inter['param2']
                curr_inter = curr_inter.drop_duplicates()
                curr_inter = curr_inter.sort_values('mi', ascending=False)
                curr_inter = curr_inter[:3]
                charts_loaded = charts_loaded + list(set(curr_inter['filename'].to_list()))
        return charts_loaded

    def _Find_sort_cols_catts(self, mycols):
        self._logger.debug('Running _Find_sort_cols_catts method')
        die_dict = {}
        for col in mycols:
            if len(re.findall('^SORT_X', col)) > 0:
                if col.find('SORT_X_') >= 0:
                    mydie = 'U' + col.replace('SORT_X_', '')
                else:
                    mydie = 'U1'
                if mydie not in die_dict.keys():
                    die_dict[mydie] = {}
                die_dict[mydie]['X'] = col
            if len(re.findall('^SORT_Y', col)) > 0:
                if col.find('SORT_Y_') >= 0:
                    mydie = 'U' + col.replace('SORT_Y_', '')
                else:
                    mydie = 'U1'
                if mydie not in die_dict.keys():
                    die_dict[mydie] = {}
                die_dict[mydie]['Y'] = col
        return die_dict

    def _Create_gencharts_CATTS(self):
        self._logger.debug('Running _Create_gencharts_CATTS method')
        charts_loaded = []
        input = {'data_df': self._spf_df,
                 'data_df_all': self._spf_df_all,
                 'resp': self._tab,
                 'ini_file': self._inputs['ini_file']}
        key_vars = self._inputs['custom_key_vars']
        key_vars = [i for i in key_vars if i in self._spf_df.columns]
        for myvar in key_vars:
            if myvar != self._tab and myvar[:6] != 'SORT_X' and myvar[:6] != 'SORT_Y':
                print('        Charting: ' + str(myvar))
                if len(self._spf_df[myvar].drop_duplicates()) > 1:
                    input['com_col'] = myvar
                    # input['filename'] = myvar.replace('#', '+') + '_comm_plot.png'
                    input['filename'] = self._fixstring(myvar) + '_comm_plot.png'
                    charts_loaded.append(input['filename'])
                    input['filename'] = os.path.join(self._report_dir, input['filename'])
                    if not os.path.isfile(input['filename']):
                        myplot = Correlation_Plot(input, interactive_run=self._interactive_run, logger=self._logger)
                        myplot.Save_figure()

        sort_cols = self._Find_sort_cols_catts(self._spf_df.columns)
        if len(sort_cols.keys()) > 0:
            for mydie in sort_cols.keys():
                if len(sort_cols[mydie].keys()) == 2:
                    input['data_df'] = self._spf_df
                    input['xvar'] = sort_cols[mydie]['X']
                    input['yvar'] = sort_cols[mydie]['Y']
                    input['charttitle'] = mydie + ' - All Fabs - Wafer Map for ' + self._tab
                    # input['filename'] = mydie + '_all_fabs_wafer_map.png'
                    input['filename'] = self._fixstring(mydie) + '_all_fabs_wafer_map.png'
                    charts_loaded.append(input['filename'])
                    input['filename'] = os.path.join(self._report_dir, input['filename'])
                    if not os.path.isfile(input['filename']):
                        myplot = Wafer_Map(input, interactive_run=self._interactive_run, logger=self._logger)
                        myplot.Save_figure()

        if 'STRIP_X_LOC' in self._spf_df.columns and 'STRIP_Y_LOC' in self._spf_df.columns:
            input['xvar'] = 'STRIP_X_LOC'
            input['yvar'] = 'STRIP_Y_LOC'
            input['charttitle'] = 'Strip Map for ' + self._tab
            input['isstrip'] = True
            input['filename'] = 'strip_map.png'
            charts_loaded.append(input['filename'])
            input['filename'] = os.path.join(self._report_dir, input['filename'])
            if not os.path.isfile(input['filename']):
                myplot = Carrier_Map(input, interactive_run=self._interactive_run, logger=self._logger)
                myplot.Save_figure()
        return charts_loaded

    def _fixstring(self, mystr):
        self._logger.debug('Running _fixstring method')
        pattern = re.compile('[^0-9a-zA-Z~-]+')
        return pattern.sub('_', str(mystr)).lower()

    def _get_subgraph_list(self, mygroup, xvar, yvar):
        self._logger.debug('Running _get_subgraph_list method')
        mydf = self._spf_df[~self._spf_df[mygroup].isnull()]
        mydf = mydf[~mydf[xvar].isnull()]
        mydf = mydf[~mydf[yvar].isnull()]
        mydf = mydf[~mydf[self._tab].isnull()]
        mydf = mydf[[mygroup, self._tab]]
        mydf.rename(columns={self._tab: 'response'}, inplace=True)
        mydf['mycount'] = mydf['response']
        try:
            mydf['response'] = mydf['response'].astype(float)
            mydf = mydf.groupby(mygroup)
            mydf = mydf.agg({'mycount': 'count', 'response': 'mean'})
            mydf.reset_index(inplace=True)
            mydf = mydf[mydf['mycount'] > 30]
            mydf.sort_values(by=['response'], ascending=False, inplace=True)
            return list(mydf[mygroup][:5].to_numpy())
        except:
            return []

    def _Create_opercharts_CATTS(self):
        self._logger.debug('Running _Create_opercharts_CATTS method')
        charts_loaded = []
        input = {'data_df': self._spf_df,
                 'data_df_all': self._spf_df_all,
                 'resp': self._tab,
                 'ini_file': self._inputs['ini_file']}
        input['com_col'] = self._var
        input['filename'] = self._fixstring(self._var) + '_comm_plot.png'
        charts_loaded.append(input['filename'])
        input['filename'] = os.path.join(self._report_dir, input['filename'])
        if not os.path.isfile(input['filename']):
            mycorrplot = Correlation_Plot(input, interactive_run=self._interactive_run, logger=self._logger)
            mycorrplot.Save_figure()
        if self._variable_type == 'cont' and self._response_type == 'good-bad':
            input['xvar'] = self._tab
            input['yvar'] = self._var
            input['filename'] = self._fixstring(self._var) + '_boxplot.png'
            charts_loaded.append(input['filename'])
            input['filename'] = os.path.join(self._report_dir, input['filename'])
            if not os.path.isfile(input['filename']):
                mybp = Basic_Charts(input, interactive_run=self._interactive_run, logger=self._logger)
                mybp.Boxplot()
                mybp.Save_figure()
            input['filename'] = self._fixstring(self._var) + '_violinplot.png'
            charts_loaded.append(input['filename'])
            input['filename'] = os.path.join(self._report_dir, input['filename'])
            if not os.path.isfile(input['filename']):
                myvp = Basic_Charts(input, interactive_run=self._interactive_run, logger=self._logger)
                myvp.Violinplot()
                myvp.Save_figure()
        myoper = self._var[:4]
        plot_oper_charts = False
        try:
            int(myoper)
            plot_oper_charts = True
        except:
            pass
        outdate_var = myoper + 'OUTDATE'
        if outdate_var in self._spf_df.columns:
            if self._spf_df[outdate_var].dtype == 'datetime64[ns]':
                if plot_oper_charts and self._variable_type == 'discrete':
                    try:
                        input['xvar'] = outdate_var
                        input['yvar'] = self._tab
                        input['groupvar'] = self._var
                        # input['filename'] = self._var.replace('#', '+') + '_time_trend_disc.png'
                        input['filename'] = self._fixstring(self._var) + '_time_trend_disc.png'
                        charts_loaded.append(input['filename'])
                        input['filename'] = os.path.join(self._report_dir, input['filename'])
                        if not os.path.isfile(input['filename']):
                            mytt = Basic_Charts(input, interactive_run=self._interactive_run, logger=self._logger)
                            mytt.Timetrend()
                            mytt.Save_figure()
                    except:
                        pass
                if plot_oper_charts and self._variable_type == 'cont':
                    try:
                        input['xvar'] = outdate_var
                        input['yvar'] = self._var
                        input['groupvar'] = 'default'
                        input['filename'] = self._fixstring(self._var) + '_time_trend_cont.png'
                        charts_loaded.append(input['filename'])
                        input['filename'] = os.path.join(self._report_dir, input['filename'])
                        if 'resp_in_pct' in input:
                            old_val = input['resp_in_pct']
                            input['resp_in_pct'] = False
                            value_existed = True
                        else:
                            input['resp_in_pct'] = False
                            value_existed = False
                        if not os.path.isfile(input['filename']):
                            mytt = Basic_Charts(input, interactive_run=self._interactive_run, logger=self._logger)
                            mytt.Timetrend()
                            mytt.Save_figure()
                        if value_existed:
                            input['resp_in_pct'] = old_val
                        else:
                            del input['resp_in_pct']
                    except:
                        pass
                if plot_oper_charts and self._variable_type == 'date':
                    try:
                        input['xvar'] = outdate_var
                        input['yvar'] = self._tab
                        input['groupvar'] = 'default'
                        input['filename'] = self._fixstring(myoper) + '_time_trend_date.png'
                        charts_loaded.append(input['filename'])
                        input['filename'] = os.path.join(self._report_dir, input['filename'])
                        if not os.path.isfile(input['filename']):
                            mytt = Basic_Charts(input, interactive_run=self._interactive_run, logger=self._logger)
                            mytt.Timetrend()
                            mytt.Save_figure()
                    except:
                        pass
        carrier_x = FindPattern(self._spf_df.columns, ['^' + myoper + '.+' + '_X$'], interactive_run=self._interactive_run, logger=self._logger)
        carrier_y = FindPattern(self._spf_df.columns, ['^' + myoper + '.+' + '_Y$'], interactive_run=self._interactive_run, logger=self._logger)
        carrier_x = [i for i in carrier_x if i.find("_") == i.rfind("_")]
        carrier_y = [i for i in carrier_y if i.find("_") == i.rfind("_")]
        if len(carrier_x) == 1 and len(carrier_y) == 1:
            input['xvar'] = carrier_x[0]
            input['yvar'] = carrier_y[0]
            input['charttitle'] = 'Media Map for {}\n{}'.format(self._tab, 'All Entities')
            input['filename'] = self._fixstring(myoper) + '_media_map_all_entities.png'
            charts_loaded.append(input['filename'])
            input['filename'] = os.path.join(self._report_dir, input['filename'])
            if not os.path.isfile(input['filename']):
                myplot = Carrier_Map(input, interactive_run=self._interactive_run, logger=self._logger)
                minresp, maxresp = myplot.Save_figure()
            if myoper + 'ENTITY-ENTITY_ID' in self._spf_df.columns:
                myentity = myoper + 'ENTITY-ENTITY_ID'
            elif myoper + 'ENTITY_ID-TEST_HEAD_ID' in self._spf_df.columns:
                myentity = myoper + 'ENTITY_ID-TEST_HEAD_ID'
            elif myoper + 'ENTITY' in self._spf_df.columns:
                myentity = myoper + 'ENTITY'
            else:
                myentity = 'NOENTITY'
            if myentity != 'NOENTITY':
                mylist = self._get_subgraph_list(myentity, input['xvar'], input['yvar'])
                for curr_entity in mylist:
                    mysplitdf = self._spf_df[self._spf_df[myentity] == curr_entity]
                    input['data_df'] = mysplitdf
                    input['charttitle'] = 'Media map for {}\n{}'.format(self._tab, curr_entity)
                    input['filename'] = self._fixstring(myoper) + '_media_map_' + self._fixstring(curr_entity) + '.png'
                    charts_loaded.append(input['filename'])
                    input['filename'] = os.path.join(self._report_dir, input['filename'])
                    if not os.path.isfile(input['filename']):
                        myplot = Carrier_Map(input, interactive_run=self._interactive_run, logger=self._logger)
                        myplot.Save_figure()
            input['data_df'] = self._spf_df
            if self._variable_type == 'cont' and self._var != input['xvar'] and self._var != input['yvar']:
                input['resp'] = self._var
                input['color_map'] = 'Greens'
                input['filename'] = self._fixstring(myoper) + '_media_map_var_all_entities.png'
                input['charttitle'] = 'Media map for {}\n{}'.format(self._var, "All Entities")
                charts_loaded.append(input['filename'])
                input['filename'] = os.path.join(self._report_dir, input['filename'])
                if 'resp_in_pct' in input:
                    old_val = input['resp_in_pct']
                    input['resp_in_pct'] = False
                    value_existed = True
                else:
                    input['resp_in_pct'] = False
                    value_existed = False
                input['wafer_param'] = True
                # input['estimator'] = 'median'
                if not os.path.isfile(input['filename']):
                    myplot = Carrier_Map(input, interactive_run=self._interactive_run, logger=self._logger)
                    myplot.Save_figure()
                del_vals = ['wafer_param', 'estimator']
                for curr_del in del_vals:
                    try:
                        del input[curr_del]
                    except:
                        pass
                if value_existed:
                    input['resp_in_pct'] = old_val
                else:
                    del input['resp_in_pct']

        #New wafer maps plots
        if str(self._var)[:3] == 'FAB' or str(self._var)[:4] == 'SORT':
            if str(self._var)[:3] == 'FAB':
                mydie = str(self._var).replace('FAB_', '')[:str(self._var).replace('FAB_', '').find('_')]
                try:
                    int(mydie)
                except:
                    mydie = 'NA'
            if str(self._var)[:4] == 'SORT':
                mydie = str(self._var)[-str(self._var)[::-1].find('_'):]
                try:
                    int(mydie)
                except:
                    mydie = 'NA'
            if mydie != 'NA':
                sortxcol = 'SORT_X_' + mydie
                sortycol = 'SORT_Y_' + mydie
            else:
                sortxcol = 'SORT_X'
                sortycol = 'SORT_Y'
            if sortxcol in self._spf_df.columns and sortycol in self._spf_df.columns:
                max_sort_x = self._spf_df[sortxcol].max()
                min_sort_x = self._spf_df[sortxcol].min()
                max_sort_y = self._spf_df[sortycol].max()
                min_sort_y = self._spf_df[sortycol].min()
                input['data_df'] = self._spf_df
                input['xvar'] = sortxcol
                input['yvar'] = sortycol
                input['minlocx'] = min_sort_x
                input['maxlocx'] = max_sort_x
                input['minlocy'] = min_sort_y
                input['maxlocy'] = max_sort_y
                if mydie != 'NA':
                    mymdpos = 'U' + mydie
                else:
                    mymdpos = 'U1'
                input['charttitle'] = mymdpos + ' - All Fabs - Wafer Map for ' + self._tab
                # input['filename'] = mymdpos + '_all_fabs_wafer_map.png'
                input['filename'] = self._fixstring(mymdpos) + '_all_fabs_wafer_map.png'
                charts_loaded.append(input['filename'])
                input['filename'] = os.path.join(self._report_dir, input['filename'])
                if not os.path.isfile(input['filename']):
                    myplot = Wafer_Map(input, interactive_run=self._interactive_run, logger=self._logger)
                    myplot.Save_figure()
                # if self._var == 'FAB_PLANT_' + mydie + '_YEAR_WW' or self._var == 'FAB_PLANT_YEAR_WW':
                # else:
                if mydie != 'NA':
                    if self._var == 'FAB_' + mydie + '_PLANT_YEAR_WW':
                        fabcol = 'FAB_' + mydie + '_PLANT_YEAR_WW'
                    else:
                        fabcol = 'FAB_' + mydie + '_PLANT'
                else:
                    if self._var == 'FAB_PLANT_YEAR_WW':
                        fabcol = 'FAB_PLANT_YEAR_WW'
                    else:
                        fabcol = 'FAB_PLANT'
                if fabcol in self._spf_df.columns:
                    mylist = self._get_subgraph_list(fabcol, input['xvar'], input['yvar'])
                    for myfab in mylist:
                        mysplitdf = self._spf_df[self._spf_df[fabcol] == myfab]
                        input['data_df'] = mysplitdf
                        input['charttitle'] = mymdpos + ' - ' + myfab + ' - Wafer Map for ' + self._tab
                        # input['filename'] = mymdpos + '_' + myfab + '_wafer_map.png'
                        input['filename'] = self._fixstring(mymdpos + '_' + myfab) + '_wafer_map.png'
                        charts_loaded.append(input['filename'])
                        input['filename'] = os.path.join(self._report_dir, input['filename'])
                        if not os.path.isfile(input['filename']):
                            myplot = Wafer_Map(input, interactive_run=self._interactive_run, logger=self._logger)
                            myplot.Save_figure()
        return charts_loaded


    def _Create_corr_only(self):
        self._logger.debug('Running _Create_corr_only method')
        charts_loaded = []
        input = {'data_df': self._spf_df,
                 'data_df_all': self._spf_df_all,
                 'resp': self._tab,
                 'ini_file': self._inputs['ini_file']}
        input['com_col'] = self._var
        # input['filename'] = self._var.replace('#', '+') + '_comm_plot.png'
        input['filename'] = self._fixstring(self._var) + '_comm_plot.png'
        charts_loaded.append(input['filename'])
        input['filename'] = os.path.join(self._report_dir, input['filename'])
        myplot = Correlation_Plot(input, interactive_run=self._interactive_run, logger=self._logger)
        myplot.Save_figure()
        return charts_loaded

    def _Create_png_html(self, charts):
        self._logger.debug('Running _Create_png_html method')
        tot_pngs = len(charts)
        mycounter = 1
        for mypng in charts:
            self.main_html_txt = self.main_html_txt + '	  <div class="mySlides">\n'
            self.main_html_txt = self.main_html_txt + '		<div class="numbertext">' + str(mycounter) + ' / ' + str(tot_pngs) + '</div>\n'
            self.main_html_txt = self.main_html_txt + '		<center>\n'
            self.main_html_txt = self.main_html_txt + '			<img src="' + mypng + '" style="height:70vh">\n'
            self.main_html_txt = self.main_html_txt + '		</center>\n'
            self.main_html_txt = self.main_html_txt + '	  </div>\n\n'
            mycounter += 1

    def _Add_thumbnails_html(self, charts):
        self._logger.debug('Running _Add_thumbnails_html method')
        counter = 1
        for mypng in charts:
            self.main_html_txt = self.main_html_txt + '    <div class="column">\n'
            self.main_html_txt = self.main_html_txt + '      <img class="demo cursor" src="' + mypng + '" style="width:100%" onclick="currentSlide(' + str(counter) + ')" alt="Data Chart">\n'
            self.main_html_txt = self.main_html_txt + '    </div>\n\n'
            counter += 1

    def _Create_charts(self):
        self._logger.debug('Running _Create_charts method')
        all_charts_loaded = []
        if self._data_origin.lower() == 'kitchensink' and self._var == 'General_Charts':
            all_charts_loaded = all_charts_loaded + self._Create_gencharts_KS()
        elif self._data_origin.lower() == 'kitchensink':
            all_charts_loaded = all_charts_loaded + self._Create_opercharts_KS()
        elif self._data_origin.lower() == 'catts' and self._var == 'General_Charts':
            all_charts_loaded = all_charts_loaded + self._Create_gencharts_CATTS()
        elif self._data_origin.lower() == 'catts':
            all_charts_loaded = all_charts_loaded + self._Create_opercharts_CATTS()
        else:
            all_charts_loaded = all_charts_loaded + self._Create_corr_only()
        self._Create_png_html(all_charts_loaded)
        self.main_html_txt = self.main_html_txt + '  <a class="prev" onclick="plusSlides(-1)"><font color=#ABB2B9><<<</font></a>\n'
        self.main_html_txt = self.main_html_txt + '  <a class="next" onclick="plusSlides(1)"><font color=#ABB2B9>>>></font></a>\n\n'
        self.main_html_txt = self.main_html_txt + '  <div class="caption-container">\n'
        self.main_html_txt = self.main_html_txt + '    <p id="caption"></p>\n'
        self.main_html_txt = self.main_html_txt + '  </div>\n\n'
        self.main_html_txt = self.main_html_txt + '  <div class="row">\n'
        self._Add_thumbnails_html(all_charts_loaded)
        self.main_html_txt = self.main_html_txt + '  </div>'

    def _Load_text(self, name):
        '''
        Description: Loads data from text to be added to HTML
        '''
        self._logger.debug('Running _Load_text method')
        with open(name, 'r') as myfile:
            data = myfile.read()
        self.main_html_txt = self.main_html_txt + data

    def Save_html(self):
        '''
        Description: Saves html string to a file defined in inputs
        '''
        self._logger.debug('Running Save_html method')
        # with open(os.path.join(self._report_dir, self._var.replace('#', '+') + '.html'), 'w') as myfile:
        with open(os.path.join(self._report_dir, self._fixstring(self._var) + '.html'), 'w') as myfile:
            myfile.write(self.main_html_txt)

if __name__ == "__main__":
    pass
    # variable = 'll#oper_info#1990_iam-p#operation_time'
    #
    # data_origin = 'kitchensink'
    #
    # with open(r"C:\Data\Mongo\spf_df.pickle", "rb") as pickle_in:
    #     spf_df = pickle.load(pickle_in)
    #
    # tab = 'ib15'
    # # report_dir = r'\\azatshfs.intel.com\azatanalysis$\MAOATM\Config\ATTD_Yield\gcarmiol\test_report'
    # report_dir = r'C:\Data\graphs\web_test'
    #
    # mypage = AC_Chart_Data(variable, tab, spf_df, report_dir, data_origin)
    # mypage.Save_html()

