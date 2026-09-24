"""
-------------------------------------------------------------------------
Class: AC_HTML_Report      Date: 05/01/2019

Version History:
========================================================================================================================
| Date         |  Who        | Ver    |  Description                                                                   |
========================================================================================================================
| 05/01/2019   |  gcarmiol   |  1.0   |  Initial Version                                                               |
| 08/31/2019   |  gcarmiol   |  2.0   |  Final Release for Production                                                  |
| 09/01/2020   |  gcarmiol   |  2.1   |  Added FixString function to htm file name                                     |
| 01/15/2121   |  gcarmiol   |  2.2   |  Consolidation of outputs in single directory                                  |
| 07/12/2022   |  gcarmiol   |  2.3   |  Changes in logging to work with Catalyst execution                            |
| 07/25/2022   |  gcarmiol   |  2.4   |  Pass model scores to charting class                                           |
| 10/25/2022   |  gcarmiol   |  3.0   |  Improved integration with KitchenSink                                         |
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
__version__ = '3.0'

import sys
import os
import pickle
import shutil
import logging
import pandas as pd
from datetime import datetime as dt
from PyUtils import BuildArgs
from PyUtils import FixString
from AutoComm_ChartData import AC_Chart_Data


class AC_HTML_Report(object):
    def __init__(self, inputs, logger=None):
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
        try:
            del IdealMethods
        except:
            pass
        self._logger = logger
        self._logger.debug('Running AC_HTML_Report class init method')
        default_inputs = {'data_df': pd.DataFrame(),
                          'results_dict': {},
                          'report_name': '',
                          'add_general_charts': True,
                          'columns': 5,
                          'data_origin': '',
                          'report_title':    'Feature Screening Report',
                          'report_subtitle': 'Created:  ' + dt.today().strftime('%Y-%m-%d %H:%M:%S'),
                          'my_instance': '9999999'}
        # inputs = {'results_dict':    results_dict,
        #           'report_name': r'\\azatshfs.intel.com\azatanalysis$\MAOATM\Config\ATTD_Yield\gcarmiol\test_report\report_test.html'}
        self._inputs = BuildArgs(inputs, default_inputs, logger=self._logger)
        self._spf_df = self._inputs['data_df']
        self._report_dir = os.path.split(self._inputs['report_name'])[0]
        if self._report_dir == '':
            self._report_dir = os.getcwd()
        self._script_path = os.path.split(os.path.realpath(__file__))[0]
        if self._inputs['my_instance'] == '9999999':
            self._my_instance = ''
        else:
            self._my_instance = '_' + str(self._inputs['my_instance'])
        self._html_top_file = os.path.join(self._script_path, 'ac_html_rep_top.txt')
        self._html_bott_file = os.path.join(self._script_path, 'ac_html_rep_bott.txt')
        self.main_html_txt = ''
        if self._inputs['add_general_charts'] == True:
            for myresp in self._inputs['results_dict'].keys():
                self._inputs['results_dict'][myresp] = pd.concat([pd.Series({'General_Charts': '2.0'}), self._inputs['results_dict'][myresp]])
        self._Load_text(self._html_top_file)
        self._Create_col_formatter()
        self._Create_Header()
        self._Create_Tabs()
        self._Create_Tabs_Data()
        self._Load_text(self._html_bott_file)

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
        print('\nSaving Final HTML file....\n')
        with open(self._inputs['report_name'], 'w') as myfile:
            myfile.write(self.main_html_txt)

    def _Create_col_formatter(self):
        '''
        Description: Formats the total columns and spacing
        '''
        self._logger.debug('Running _Create_col_formatter method')
        total_cols = self._inputs['columns']
        col_spacing = str(100.0/total_cols)
        mystr = '.column {\n'
        mystr = mystr + '  -ms-flex: ' + col_spacing + '%; /* IE10 */\n'
        mystr = mystr + '  flex: ' + col_spacing + '%;\n'
        mystr = mystr + '  max-width: ' + col_spacing + '%;\n'
        mystr = mystr + '  padding: 0 6px;\n'
        mystr = mystr + '}\n\n\n'
        mystr = mystr + '</style>\n'
        mystr = mystr + '</head>\n\n'
        mystr = mystr + '<body>\n\n'
        self.main_html_txt = self.main_html_txt + mystr

    def _Create_Header(self):
        '''
        Description: Adds report title and subtitle
        '''
        self._logger.debug('Running _Create_Header method')
        mystr = '<!-- Header -->\n' + '<div class="header">\n'
        mystr = mystr + '  <h1>' + self._inputs['report_title'] + '</h1>\n'
        mystr = mystr + '  <p>' + self._inputs['report_subtitle'] + '</p>\n'
        mystr = mystr + '</div>\n\n'
        self.main_html_txt = self.main_html_txt + mystr

    def _Create_Tabs(self):
        '''
        Description: Creates tabs for each response variable
        '''
        self._logger.debug('Running _Create_Tabs method')
        results_dict = self._inputs['results_dict']
        first = True
        mystr = '<!-- Tab links -->\n' + '<div class="tab">\n'
        for mytab in results_dict.keys():
            mystr = mystr + '  <button class='
            if first:
                mystr = mystr + '"tablinks active" onclick="openTab(event, ' + "'" + mytab + "')" + '">' + mytab + '</button>\n'
                first = False
            else:
                mystr = mystr + '"tablinks" onclick="openTab(event, ' + "'" + mytab + "')" + '">' + mytab + '</button>\n'
        mystr = mystr + '</div>\n\n'
        self.main_html_txt = self.main_html_txt + mystr

    def _Create_Tab_Dir(self,  mytab):
        '''
        Description: Ensures directory is created for every tab in the report
        Arguments:
                Tab being added to report
        '''
        self._logger.debug('Running _Create_Tab_Dir method')
        if not os.path.isdir(os.path.join(self._report_dir, mytab))and self._report_dir != '':
            os.makedirs(os.path.join(self._report_dir, mytab))
        if self._inputs['add_general_charts'] == True:
            shutil.copy(os.path.join(self._script_path, 'General_Charts.png'), os.path.join(self._report_dir, mytab))

    def _Create_image_order(self, total, columns):
        '''
        Description: Creates the order in which the charts will be written in the HTML report so the graphs are
                     organized in rows based on inportance
        Arguments:
                Total reports and total columns that will be displayed in the html
        '''
        self._logger.debug('Running _Create_image_order method')
        full_order = []
        for c in range(columns):
            curr_oder = []
            currcol = c
            while currcol < total:
                curr_oder.append(currcol)
                currcol += columns
            full_order.append(curr_oder)
        return full_order

    def _Create_Tabs_Data(self):
        '''
        Description: Adds the charts that will be displayed in each tab to the html
        '''
        self._logger.debug('Running _Create_Tabs_Data method')
        results_dict = self._inputs['results_dict']
        mystr = '<!-- Tab content -->\n'
        first_tab=True
        if not os.path.isdir(os.path.join(self._report_dir, self._inputs['report_name_no_path'])) and self._report_dir != '':
            os.makedirs(os.path.join(self._report_dir, self._inputs['report_name_no_path']))
        for mytab in results_dict.keys():
            print('\nCreating data pages for response: ' + str(mytab) + '\n')
            first_chart=True
            mytab_instance = os.path.join(self._inputs['report_name_no_path'], mytab)
            self._Create_Tab_Dir(mytab_instance)
            if first_tab:
                mystr = mystr + '<div id="' + mytab + '" class="tabcontent" style="display:block">\n'
                first_tab = False
            else:
                mystr = mystr + '<div id="' + mytab + '" class="tabcontent">\n'
            mystr = mystr + '	<div class="row">\n'
            charts_done = 0
            total_cols = self._inputs['columns']
            col_order = self._Create_image_order(len(results_dict[mytab].index), total_cols)
            for c in col_order:
                mystr = mystr + '	  <div class="column">\n'
                for r in c:
                    myvar = results_dict[mytab].index[r]
                    # myvar_fixed = myvar.replace('#', '+')
                    myvar_fixed = FixString(myvar, logger=self._logger)
                    if myvar == 'General_Charts':
                        mystr = mystr + "		<a target='_blank' href='" + \
                                                mytab_instance + '/' + myvar_fixed + '.html' + "'><img src='" + \
                                                mytab_instance + '/' + myvar_fixed + '.png' + "' style=" + '"width:100%"></a>\n'
                    else:
                        mystr = mystr + "		<a target='_blank' href='" + \
                                                mytab_instance + '/' + myvar_fixed + '.html' + "'><img src='" + \
                                                mytab_instance + '/' + myvar_fixed + '_comm_plot.png' + "' style=" + '"width:100%"></a>\n'
                    print('    Creating charts for: ' + str(myvar))
                    # mypage = AC_Chart_Data(myvar, mytab, self._spf_df, self._report_dir, self._inputs['data_origin'], my_instance=self._my_instance)
                    mypage = AC_Chart_Data(myvar, mytab, self._spf_df, self._report_dir, self._inputs, logger=self._logger)
                    mypage.Save_html()
                mystr = mystr + '	  </div>\n'
            mystr = mystr + '	</div>\n' + '</div>\n\n'
        self.main_html_txt = self.main_html_txt + mystr


if __name__ == "__main__":
    pass



