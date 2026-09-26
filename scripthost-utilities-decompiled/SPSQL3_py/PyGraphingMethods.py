"""
-------------------------------------------------------------------------
File: PyGraphingMethods      Date: 05/12/2019

Version History:
========================================================================================================================
| Date         |  Who        | Ver    |  Description                                                                   |
========================================================================================================================
| 05/01/2019   |  gcarmiol   |  1.0   |  Initial Version                                                               |
| 08/31/2019   |  gcarmiol   |  2.0   |  Final Release for Production                                                  |
| 09/01/2020   |  gcarmiol   |  2.1   |  Added defaults in case y limits are nan                                       |
| 03/02/2021   |  gcarmiol   |  2.2   |  Fixed bug occurring in mpl 3.3.2 with repeated tick marks                     |
| 07/12/2022   |  gcarmiol   |  2.3   |  Fixes to allow the utility to work with KitchenSink data correctly and change |
|              |             |        |  in logging to work with Catalyst execution                                    |
| 07/25/2022   |  gcarmiol   |  2.4   |  Add model scores and display note for charts only showing some groups and     |
|              |             |        |  set minimum bin for display to 10                                             |
| 07/27/2022   |  gcarmiol   |  2.5   |  Changing matplotlib backed to Agg to prevent TK crash                         |
| 10/25/2022   |  gcarmiol   |  3.0   |  Modifications for improved integration with KitchenSink Data                  |
| 03/14/2023   |  gcarmiol   |  4.0   |  Added better charting for WLA area                                            |
| 05/04/2023   |  gcarmiol   |  4.1   |  Bug fix when running in python 3.8                                            |
| 05/17/2023   |  gcarmiol   |  4.2   |  Removed deprecated method to hide grid in order to work with new MPL          |
| 08/14/2023   |  gcarmiol   |  4.3   |  Bug when running datatime commonality charts                                  |
| 08/04/2025   |  gcarmiol   |  4.4   |  Fix of bug where a string can be interpreted as int in matplotlib             |
========================================================================================================================

Description:
============
Several Classes that perform do Python Graphing

Classes:
========
Correlation_Plot
Basic_Charts
    -Boxplot
    -Violinplot
    -Timetrend
Carrier_Map
Wafer_Map

"""
__version__ = '4.4'

import warnings
warnings.filterwarnings("ignore")
import os
import sys
import logging
import pandas as pd
import numpy as np
import seaborn as sns
import matplotlib as mpl
import matplotlib.pyplot as plt
from matplotlib import cm
from matplotlib.patches import Patch
from matplotlib.lines import Line2D
from .PyUtils import BuildArgs
from datetime import timedelta as td
mpl.use('Agg')


class Correlation_Plot(object):
    def __init__(self, inputs, interactive_run=True, logger=None):
        '''
        Description:
            Init method for the class

        Arguments:
            inputs:       Dictionary with arguments passed by code creating the HTML report

        Auto-Execution:
                    _Correlation_Plot - Creates the matplotlib figure for the correlation plot

        Object Variables:
                    fig - matplotlib figure with plot
        '''
        self._logger = logger
        self._logger.debug('Running Correlation_Plot class init method')
        warnings.filterwarnings("ignore")
        default_inputs = {'data_df': pd.DataFrame(),
                          'resp': '',
                          'com_col': '',
                          'filename': '',
                          'min_bin_data': 10,
                          'figsizex': 10,
                          'figsizey': 6,
                          'resp_in_pct': True,
                          'dpi': 120,
                          'sorttype': 'descending',
                          'max_bins': 20,
                          'estimator': 'mean',
                          'circleedgecolor': (0, 0, 0.5, 1),
                          'ciclelinewidth': 0.5,
                          'circlefillcolor': (0.12, 0.45, 1, 1),
                          'barcolor': (0.96, 0.87, 0.70, 1),
                          'circlealpha': 0.85,
                          'labelsize': 8,
                          'xlabelrotation': 30,
                          'xlabel': 'default',
                          'ylabel': 'default',
                          'y2label': 'Total Units per Group',
                          'charttitle': 'default',
                          'titlesize': 15,
                          'minxval': -99999999.0,
                          'maxxval': 99999999.0,
                          'minyval': -99999999.0,
                          'maxyval': 99999999.0,
                          'miny2val': -99999999.0,
                          'maxy2val': 99999999.0,
                          }
        self._interactive_run = interactive_run
        self._arguments = BuildArgs(inputs, default_inputs, ini_section='CORRELATION PLOT CONFIGURATION', logger=self._logger)
        self._comm_db = self._arguments['data_df']
        self._com_col = self._arguments['com_col']
        self._resp = self._arguments['resp']
        self._CheckResponse()
        self._Correlation_Plot()

    def _CheckResponse(self):
        """
        Description:
                Internal Function - Checks that the response variable is continuous or 1/0
        :return:            N/A
        """
        self._logger.debug('Running _CheckResponse method')
        if self._comm_db[self._resp].dtype in ['int64', 'float64', 'Int64', 'Float64', 'float']:
            myrespvals = list(self._comm_db[self._resp].drop_duplicates())
            if len(myrespvals) > 2 or 1 not in myrespvals or 0 not in myrespvals:
                self._arguments['resp_in_pct'] = False
                self._arguments['resp_good_bad'] = False
            else:
                self._arguments['resp_good_bad'] = True
        else:
            print('\n')
            self._logger.error("======================================================================================")
            self._logger.error("Correlation plots need 0/1 (Good/Bad) or continuous values in the response variable.")
            self._logger.error("Values for response: " + str(self._resp) + " don't meet the requirement. Exiting ...")
            self._logger.error("======================================================================================")
            print("\n")
            print("\n")
            if self._interactive_run:
                sys.exit(1)

    def _GetEstimator(self):
        self._logger.debug('Running _GetEstimator method')
        try:
            if self._arguments['estimator'] == 'percent':
                myestimator = 'mean'
            elif self._arguments['estimator'] == 'median':
                myestimator = 'median'
            elif self._arguments['estimator'] == 'mean':
                myestimator = 'mean'
            else:
                raise Exception
        except:
            self._logger.warning('###############################################')
            self._logger.warning("Error in estimator provided.....using 'mean'")
            self._logger.warning("Estimators allowed are:")
            self._logger.warning("         -'mean' (default)")
            self._logger.warning("         -'median'")
            self._logger.warning("         -'percent'")
            self._logger.warning('###############################################')
            myestimator = 'mean'
        return myestimator

    def _GetDataDiscrete(self):
        """
        Description:
                Internal Function - Provides data needed to chart dots when commonality variable is discrete
        :return:            Tuple with 3 pandas Series: chart centers (x bubbles), chart means (y), splits_vol (x bars)
        """
        self._logger.debug('Running _GetDataDiscrete method')
        myestimator = self._GetEstimator()
        self._comm_db = self._comm_db.groupby([self._com_col])
        self._comm_db = self._comm_db.agg({self._resp: myestimator, 'count': 'count'})
        if self._arguments['estimator'] == 'percent' or self._arguments['resp_in_pct']:
            self._comm_db[self._resp] = self._comm_db[self._resp] * 100
        self._comm_db = self._comm_db[self._comm_db['count'] > self._arguments['min_bin_data']]
        self._comm_db.reset_index(inplace=True)
        if (self._arguments['sorttype'] == 'ascending') | (self._arguments['sorttype'] == 'asc'):
            self._comm_db.sort_values(self._resp, inplace=True, ascending=True)
        else:
            self._comm_db.sort_values(self._resp, inplace=True, ascending=False)
        if len(self._comm_db) > self._arguments['max_bins']:
            self._comm_db = self._comm_db[:self._arguments['max_bins']]
            self._add_groups_note = True
        splits_centers = self._comm_db[self._com_col]
        if splits_centers.dtype in ['int64', 'float64', 'Int64', 'Float64', 'float']:
            splits_centers = splits_centers.astype('str') + '_'
        splits_means = self._comm_db[self._resp]
        splits_vol = self._comm_db['count']
        return splits_centers, splits_means, splits_vol

    def _GetContData(self):
        """
        Description:
                Internal Function - Provides data needed to chart dots when commonality variable is continuous
        :return:            Tuple with 3 pandas Series: chart centers (x bubbles), chart means (y), splits_vol (x bars)
        """
        self._logger.debug('Running _GetContData method')
        if len(self._comm_db[self._com_col].drop_duplicates()) > self._arguments['max_bins']:
            if self._comm_db[self._com_col].dtype == 'datetime64[ns]':
                iqr = (self._comm_db[self._com_col].quantile(.75) - self._comm_db[self._com_col].quantile(.25))
                outlow = min(self._comm_db[self._com_col].quantile(.25) - 1.5 * iqr, self._comm_db[self._com_col].quantile(.01))
                outhigh = max(self._comm_db[self._com_col].quantile(.75) + 1.5 * iqr, self._comm_db[self._com_col].quantile(.99))
            else:
                iqr = (self._comm_db[self._com_col].astype('float').quantile(.75) - self._comm_db[self._com_col].astype('float').quantile(.25))
                outlow = min(self._comm_db[self._com_col].astype('float').quantile(.25) - 1.5 * iqr, self._comm_db[self._com_col].astype('float').quantile(.01))
                outhigh = max(self._comm_db[self._com_col].astype('float').quantile(.75) + 1.5 * iqr, self._comm_db[self._com_col].astype('float').quantile(.99))
        else:
            outlow = min(self._comm_db[self._com_col])
            outhigh = max(self._comm_db[self._com_col])
        split_bins = (outhigh - outlow) / self._arguments['max_bins']
        if type(split_bins) != pd.Timedelta and split_bins > 0:
            mylog = np.log10(split_bins)
            if mylog > 3:
                digits = 0
            elif mylog > 1:
                digits = 1
            else:
                digits = int(round(abs(mylog), 0)) + 2
        elif type(split_bins) == pd.Timedelta:
            digits = 0
        else:
            digits = 3
        splits_centers = pd.Series([(outlow + i * split_bins + split_bins / 2) for i in range(self._arguments['max_bins'])])
        splits_low = splits_centers - split_bins / 2
        splits_high = splits_centers + split_bins / 2
        if type(split_bins) != pd.Timedelta:
            splits_centers = splits_centers.round(digits)
        else:
            splits_centers = pd.Series(splits_centers.values.astype('datetime64[s]'))
        if type(splits_low[0]) != pd.Timestamp:
            splits_low[0] = self._comm_db[self._com_col].min()-999999999999999
        else:
            splits_low[0] = self._comm_db[self._com_col].min() - pd.Timedelta(days=1000)
        splits_high[self._arguments['max_bins'] - 1] = self._comm_db[self._com_col].max()
        splits_means = []
        splits_vol = []
        for i in range(self._arguments['max_bins']):
            myseries = self._comm_db[(self._comm_db[self._com_col] > splits_low[i]) &
                                              (self._comm_db[self._com_col] <= splits_high[i])][self._resp]
            myval = myseries.agg(func=self._GetEstimator())
            if self._arguments['estimator'] == 'percent' or self._arguments['resp_in_pct']:
                myval = myval * 100
            splits_means.append(myval)
            splits_vol.append(len(self._comm_db[(self._comm_db[self._com_col] > splits_low[i]) & (self._comm_db[self._com_col] <= splits_high[i])]))
        splits_means = pd.Series(splits_means)
        splits_vol = pd.Series(splits_vol)
        splits_means[splits_vol < self._arguments['min_bin_data']] = np.nan
        return splits_centers, splits_means, splits_vol

    def _GraphData(self):
        """
        :Description:
                Internal Function - Provides data needed to crete the response chart, this calls the function to get data depending on x data type
        :return:             Tuple with 3 pandas Series: chart centers (x bubbles), chart means (y), splits_vol (x bars)
        """
        self._logger.debug('Running _GraphData method')
        self._comm_db['count'] = 1
        self._comm_db = self._comm_db[[self._com_col, self._resp, 'count']]
        self._comm_db.dropna(inplace=True)
        self._isdate = False
        self._add_groups_note = False
        if (self._comm_db[self._com_col].dtype in ['int64', 'float64', 'Int64', 'Float64', 'float']) or (self._comm_db[self._com_col].dtype == 'datetime64[ns]'):
            if self._comm_db[self._com_col].dtype == 'datetime64[ns]':
                self._isdate = True
            return_data = self._GetContData()
        else:
            return_data = self._GetDataDiscrete()
        return return_data

    def _Correlation_Plot(self):
        """
        :Description:
                Creates matplotlib figure with Correlation Plot
        :return:             This method doesn't return anything, only creates the figure
        """

        self._logger.debug('Running _Correlation_Plot method')

        if 'results_dict' in self._arguments:
            show_score = True
            if self._arguments['com_col'] in self._arguments['results_dict'][self._arguments['resp']]:
                model_score_val = round(self._arguments['results_dict'][self._arguments['resp']][self._arguments['com_col']], 4)
            else:
                model_score_val = 0
        else:
            show_score = False
        if self._arguments['xlabel'] == 'default':
            x_label_val = self._arguments['com_col']
        else:
            x_label_val = self._arguments['xlabel']
        bubble_size = 24 - self._arguments['max_bins'] / 5
        splits_centers, splits_means, splits_vol = self._GraphData()

        if self._isdate:
            if (splits_centers.max() - splits_centers.min()) > td(hours=30):
                splits_centers = list(splits_centers)
                splits_centers = [a.strftime('%Y-%m-%d %Hh') for a in splits_centers]
            elif (splits_centers.max() - splits_centers.min()) > td(minutes=30):
                splits_centers = list(splits_centers)
                splits_centers = [a.strftime('%Y-%m-%d %Hh:%M') for a in splits_centers]
            else:
                splits_centers = list(splits_centers)

        if len(splits_centers) > 0:
            if self._arguments['resp_good_bad']:
                minyval = min(np.nanmin(splits_means), 0)
            else:
                minyval = np.nanmin(splits_means)
            if self._arguments['minyval'] != -99999999.0:
                minyval = self._arguments['minyval']
            maxyval = np.nanmax(splits_means)
            if self._arguments['maxyval'] != 99999999.0:
                maxyval = self._arguments['maxyval']
            if str(minyval) == 'nan':
                minyval = 0
            if str(maxyval) == 'nan':
                maxyval = 0
            graph_y_range = maxyval - minyval
            minyval = minyval - (graph_y_range) * 0.05
            maxyval = maxyval + (graph_y_range) * 0.05

            self.fig = plt.figure(figsize=(self._arguments['figsizex'],self._arguments['figsizey']), dpi=self._arguments['dpi'])
            try:
                ax1 = sns.stripplot(x=splits_centers, y=splits_means, edgecolor=self._arguments['circleedgecolor'], size=bubble_size,
                                    linewidth=self._arguments['ciclelinewidth'], color=self._arguments['circlefillcolor'], alpha=self._arguments['circlealpha'])
            except:
                if splits_centers.dtype == 'O':
                    splits_centers = splits_centers + "_"
                    ax1 = sns.stripplot(x=splits_centers, y=splits_means, edgecolor=self._arguments['circleedgecolor'], size=bubble_size,
                                        linewidth=self._arguments['ciclelinewidth'], color=self._arguments['circlefillcolor'], alpha=self._arguments['circlealpha'])
                else:
                    raise
            ax1.tick_params('both', labelsize=self._arguments['labelsize'])
            ax1.grid(visible=True, which='both', axis='y', color='gray', linewidth=1, alpha=0.2)
            ax_vals = ['bottom', 'top', 'left', 'right']
            if len(set(splits_centers)) == 1:
                ax1.set_xticklabels([splits_centers[0]], rotation=self._arguments['xlabelrotation'], ha='right')
            else:
                ax1.set_xticklabels(splits_centers, rotation=self._arguments['xlabelrotation'], ha='right')
            ax1.set_xlabel(x_label_val)
            if self._arguments['ylabel'] == 'default':
                if self._arguments['resp_in_pct'] or self._arguments['estimator'] == 'percent':
                    ax1.set_ylabel(str(self._resp) + ' (%)')
                else:
                    ax1.set_ylabel(str(self._resp) + ' (' + self._arguments['estimator'] + ')')
            else:
                ax1.set_ylabel(self._arguments['ylabel'])
            ax1.set_ylim(ymin=minyval, ymax=maxyval)
            if self._arguments['minxval'] != -99999999.0:
                ax1.set_xlim(xmin=self._arguments['minxval'])
            if self._arguments['maxxval'] != 99999999.0:
                ax1.set_xlim(xmax=self._arguments['maxxval'])
            ax2 = ax1.twinx()
            ax2 = sns.barplot(x=splits_centers, y=splits_vol, color=self._arguments['barcolor'])

            if self._arguments['miny2val'] != -99999999.0:
                miny2val = self._arguments['miny2val']
            else:
                miny2val = 0
            if self._arguments['maxy2val'] != 99999999.0:
                maxy2val = self._arguments['miny2val']
            else:
                maxy2val = max(splits_vol) * 1.1
            if str(miny2val) == 'nan':
                miny2val = 0
            if str(maxy2val) == 'nan':
                maxy2val = 1
            ax2.set_ylim(ymin=miny2val, ymax=maxy2val)
            ax2.set_ylabel(self._arguments['y2label'])
            ax2.grid(visible=False)
            ax1.set_zorder(ax2.get_zorder() + 1)
            ax1.patch.set_visible(False)
            if self._arguments['charttitle'] != 'default':
                mytitle = self._arguments['charttitle']
            else:
                mytitle = self._com_col + " Commonality Plot"
            for curr_ax in ax_vals:
                ax1.spines[curr_ax].set_color('lightgray')
                ax1.spines[curr_ax].set_linewidth(1)
                ax2.spines[curr_ax].set_color('lightgray')
                ax2.spines[curr_ax].set_linewidth(1)
            plt.title(mytitle, size=self._arguments['titlesize'])
            plt.tight_layout()
            if show_score:
                plt.text(0.8, 0.01, f'Var Importance Score: {model_score_val}', fontsize=8, transform=plt.gcf().transFigure)
            if self._add_groups_note:
                plt.text(0.05, 0.01, f'Note: Only top 20 groups shown', fontsize=8, transform=plt.gcf().transFigure)
            plt.close()
        else:
            self.fig = plt.figure(figsize=(self._arguments['figsizex'], self._arguments['figsizey']), dpi=self._arguments['dpi'])
            ax = plt.subplot(111)
            ax_vals = ['bottom', 'top', 'left', 'right']
            for curr_ax in ax_vals:
                ax.spines[curr_ax].set_color('lightgray')
                ax.spines[curr_ax].set_linewidth(1)
            ax.text(0.5, 0.6, self._com_col, ha="center", va="center", fontsize=15)
            ax.text(0.5, 0.4, "No data to display or too many levels to meet minimum volume", ha="center", va="center", fontsize=12)
            ax.patch.set_visible(False)
            # plt.axis('off')
            plt.grid(visible=False)
            plt.close()

    def Save_figure(self):
        """
        :Description:
                Saves matplotlib figure with Correlation Plot to a file
        :return:             This method doesn't return anything, only saves the figure
        """
        self._logger.debug('Running Save_figure method')
        self.fig.savefig(self._arguments['filename'])


class Basic_Charts(object):
    def __init__(self, inputs, interactive_run=True, logger=None):
        '''
        Description:
            Init method for the class

        Arguments:
            inputs:       Dictionary with arguments passed by code creating the HTML report

        Auto-Execution:
                    _Correlation_Plot - Creates the matplotlib figure for the correlation plot

        Object Variables:
                    fig - matplotlib figure with plot
        '''
        self._logger = logger
        self._logger.debug('Running Basic_Charts class init method')
        warnings.filterwarnings("ignore")
        self._interactive_run = interactive_run
        default_inputs = {'data_df': pd.DataFrame(),
                          'xvar': '',
                          'yvar': '',
                          'filename': '',
                          'groupvar': 'default',
                          'estimator': 'mean',
                          'resp_in_pct': True,
                          'min_bin_data': 10,
                          'colx': 'not_implemented',
                          'coly': 'not_implemented',
                          'col_wrap': 'not_implemented',
                          'figsizex': 6,
                          'figsizey': 6,
                          'timefigsizex': 10,
                          'timefigsizey': 6,
                          'dpi': 120,
                          'maxgroups': 20,
                          'jitter': True,
                          'sharey': 'default',
                          'dotedgecolor': (0.3, 0.3, 0.3, 1),
                          'dotlinewidth': 1,
                          'dotalpha': 0.4,
                          'dotsize': 4,
                          'labelsize': 12,
                          'xlabelrotation': 30,
                          'xlabel': 'default',
                          'ylabel': 'default',
                          'y2label': 'default',
                          'charttitle': 'default',
                          'titlesize': 16,
                          'showline': False,
                          'linewidth': 2,
                          'showmarkers': True,
                          'markersize': 7,
                          'showci': False,
                          'legendloc': 'outside',
                          'showgrid': True,
                          'markerlist': ['o','X','v','s','*','p','<','8','h','^','D','>','d','.'] * 10
                          }
        sns.set_style("whitegrid")
        self._arguments = BuildArgs(inputs, default_inputs, ini_section='GENERAL CHART CONFIGURATION', logger=self._logger)
        self._CheckResponse()

    def _CheckResponse(self):
        """
        Description:
                Internal Function - Checks that the response variable is continuous or 1/0
        :return:            N/A
        """
        self._logger.debug('Running _CheckResponse method')
        if self._arguments['data_df'][self._arguments['yvar']].dtype in ['int64', 'float64', 'Int64', 'Float64', 'float']:
            myrespvals = list(self._arguments['data_df'][self._arguments['yvar']].drop_duplicates())
            if len(myrespvals) > 2 or 1 not in myrespvals or 0 not in myrespvals:
                self._arguments['resp_in_pct'] = False
                self._arguments['resp_good_bad'] = False
            else:
                self._arguments['resp_good_bad'] = True
        else:
            print('\n')
            self._logger.error("======================================================================================")
            self._logger.error("Basic charts need 0/1 (Good/Bad) or continuous values in the response variable.")
            self._logger.error("Values for response: " + str(self._arguments['yvar']) + " don't meet the requirement. Exiting ...")
            self._logger.error("======================================================================================")
            print("\n")
            print("\n")
            if self._interactive_run:
                sys.exit(1)

    def _DisplayGroups(self, data_df, group_var):
        self._logger.debug('Running _DisplayGroups method')
        subset_vars = False
        if len(data_df[group_var].drop_duplicates()) > self._arguments['maxgroups']:
            data_df_g = data_df[[group_var, self._arguments['yvar']]]
            data_df_g = data_df.groupby([group_var])
            data_df_g = data_df_g.agg({self._arguments['yvar']: 'mean'})
            data_df_g.reset_index(inplace=True)
            data_df_g.sort_values(self._arguments['yvar'], inplace=True, ascending=False)
            data_df_g = data_df_g[:self._arguments['maxgroups']]
            del data_df_g[self._arguments['yvar']]
            data_df.rename(columns={group_var: group_var + '_orig'}, inplace=True)
            data_df = pd.merge(data_df, data_df_g, left_on=group_var + '_orig', right_on=group_var, how='left')
            data_df[group_var] = data_df[group_var].fillna('Other')
        return data_df

    def Boxplot(self):
        self._logger.debug('Running Boxplot method')
        self.fig = plt.figure(figsize=(self._arguments['figsizex'], self._arguments['figsizey']))
        self._arguments['data_df'][self._arguments['xvar']] = self._arguments['data_df'][self._arguments['xvar']].astype('object')
        ax = sns.boxplot(x=self._arguments['xvar'], y=self._arguments['yvar'], data=self._arguments['data_df'])
        ax = sns.stripplot(x=self._arguments['xvar'], y=self._arguments['yvar'], data=self._arguments['data_df'],
                      jitter=self._arguments['jitter'], size=self._arguments['dotsize'],
                      edgecolor=self._arguments['dotedgecolor'], linewidth=self._arguments['dotlinewidth'],
                      alpha=self._arguments['dotalpha'])
        if self._arguments['charttitle'] != 'default':
            ax.set_title(self._arguments['charttitle'], size=self._arguments['titlesize'], y=1.05)
        #x-Label
        if self._arguments['xlabel'] == 'default':
            ax.set_xlabel(self._arguments['xvar'], size=self._arguments['labelsize'])
        else:
            ax.set_xlabel(self._arguments['xlabel'], size=self._arguments['labelsize'])
        # y-Label
        if self._arguments['ylabel'] == 'default':
            ax.set_ylabel(self._arguments['yvar'], size=self._arguments['labelsize'])
        else:
            ax.set_ylabel(self._arguments['ylabel'], size=self._arguments['labelsize'])
        plt.xticks(rotation=self._arguments['xlabelrotation'], ha='right')
        plt.tight_layout()
        plt.close()

    def Violinplot(self):
        self._logger.debug('Running Violinplot method')
        self.fig = plt.figure(figsize=(self._arguments['figsizex'], self._arguments['figsizey']))
        data_changed = False
        if str(self._arguments['data_df'][self._arguments['yvar']].dtype) in ['Int64', 'int64', 'int']:
            orig_type = str(self._arguments['data_df'][self._arguments['yvar']].dtype)
            self._arguments['data_df'][self._arguments['yvar']] = self._arguments['data_df'][self._arguments['yvar']].astype(float)
            data_changed = True
        ax = sns.violinplot(x=self._arguments['xvar'], y=self._arguments['yvar'], data=self._arguments['data_df'])
        if data_changed:
            self._arguments['data_df'][self._arguments['yvar']] = self._arguments['data_df'][self._arguments['yvar']].astype(orig_type)
        if self._arguments['charttitle'] != 'default':
            ax.set_title(self._arguments['charttitle'], size=self._arguments['titlesize'], y=1.05)
        #x-Label
        if self._arguments['xlabel'] == 'default':
            ax.set_xlabel(self._arguments['xvar'], size=self._arguments['labelsize'])
        else:
            ax.set_xlabel(self._arguments['xlabel'], size=self._arguments['labelsize'])
        # y-Label
        if self._arguments['ylabel'] == 'default':
            ax.set_ylabel(self._arguments['yvar'], size=self._arguments['labelsize'])
        else:
            ax.set_ylabel(self._arguments['ylabel'], size=self._arguments['labelsize'])
        plt.xticks(rotation=self._arguments['xlabelrotation'], ha='right')
        plt.tight_layout()
        plt.close()

    def _GetEstimator(self):
        self._logger.debug('Running _GetEstimator method')
        try:
            if self._arguments['estimator'] == 'percent':
                myestimator = 'mean'
            elif self._arguments['estimator'] == 'median':
                myestimator = 'median'
            elif self._arguments['estimator'] == 'mean':
                myestimator = 'mean'
        except:
            self._logger.warning('###############################################')
            self._logger.warning("Error in estimator provided.....using 'mean'")
            self._logger.warning("Estimators allowed are:")
            self._logger.warning("         -'mean' (default)")
            self._logger.warning("         -'median'")
            self._logger.warning("         -'percent'")
            self._logger.warning('###############################################')
            myestimator = 'mean'
        return myestimator

    def _Check_x_timevar(self):
        self._logger.debug('Running _Check_x_timevar method')
        if self._arguments['data_df'][self._arguments['xvar']].dtype != 'datetime64[ns]':
            try:
                self._arguments['data_df'][self._arguments['xvar']] = \
                    self._arguments['data_df'][self._arguments['xvar']].str.replace('/', '-').astype('datetime64')
            except:
                print('\n')
                self._logger.error('#############################################################')
                self._logger.error('Variable: ' + self._arguments['xvar'] + ' is not a date.... exiting.')
                self._logger.error('#############################################################')
                if self._interactive_run:
                    sys.exit(1)

    def Timetrend(self):
        self._logger.debug('Running Timetrend method')
        myestimator = self._GetEstimator()
        self._Check_x_timevar()

        self.fig = plt.figure(figsize=(self._arguments['timefigsizex'], self._arguments['timefigsizey']))

        if self._arguments['showline']:
            mylinewidth = self._arguments['linewidth']
        else:
            mylinewidth = 0
        if self._arguments['showmarkers']:
            mymarkersize = self._arguments['markersize']
        else:
            mymarkersize = 0
        if self._arguments['showci']:
            myci = 'sd'
        else:
            myci = None

        data = self._arguments['data_df']

        if self._arguments['groupvar'] == 'default':
            mygroups = [self._arguments['xvar']]
        else:
            mygroups = [self._arguments['xvar'], self._arguments['groupvar']]

        data['count'] = 1

        data_df_summ = data.groupby(mygroups)
        try:
            data_df_summ = data_df_summ.agg({self._arguments['resp']: myestimator, self._arguments['yvar']: myestimator,  'count': 'sum'})
        except:
            data[self._arguments['resp']] = pd.to_numeric(data[self._arguments['resp']])
            data[self._arguments['yvar']] = pd.to_numeric(data[self._arguments['yvar']])
            data_df_summ = data.groupby(mygroups)
            data_df_summ = data_df_summ.agg({self._arguments['resp']: myestimator, self._arguments['yvar']: myestimator, 'count': 'sum'})

        data_df_summ.reset_index(inplace=True)
        data_df_summ = data_df_summ[data_df_summ['count'] >= self._arguments['min_bin_data']]

        if self._arguments['estimator'] == 'percent' or self._arguments['resp_in_pct']:
            data_df_summ[self._arguments['yvar']] = data_df_summ[self._arguments['yvar']] * 100.0

        if self._arguments['groupvar'] == 'default':
            ax1 = self.fig.add_subplot(111)
            ax1.plot_date(data_df_summ[self._arguments['xvar']], data_df_summ[self._arguments['yvar']], fmt='.', color='k', markersize=mymarkersize, markerfacecolor='navy', markeredgecolor='navy')
            xvals = ax1.get_xlim()
        else:
            data_df_summ = self._DisplayGroups(data_df_summ, self._arguments['groupvar'])
            if data_df_summ[self._arguments['groupvar']].dtype == 'O':
                try:
                    data_df_summ[self._arguments['groupvar']].astype('float64')
                    data_df_summ[self._arguments['groupvar'] + '_'] = data_df_summ[self._arguments['groupvar']].astype('str') + '_'
                except:
                    pass
            curr_marker_list = self._arguments['markerlist'] * 5
            curr_marker_list = curr_marker_list[:len(data_df_summ[self._arguments['groupvar']].drop_duplicates())]
            ax1 = sns.lineplot(x=self._arguments['xvar'], y=self._arguments['yvar'], data=data_df_summ, estimator='mean',
                              dashes=False, markers=curr_marker_list, markersize=mymarkersize,
                              linewidth=mylinewidth, ci=myci,
                              hue=(self._arguments['groupvar']), style=(self._arguments['groupvar']))

        plt.xticks(rotation=self._arguments['xlabelrotation'], ha='right')
        if self._arguments['legendloc'] != 'inside' and self._arguments['groupvar'] != 'default':
            # plt.legend()
            handles, labels = ax1.get_legend_handles_labels()
            plt.legend(handles=handles[1:], labels=labels[1:], loc=2, bbox_to_anchor=(1.05, 1), borderaxespad=0.0)
        if self._arguments['charttitle'] != 'default':
            ax1.set_title(self._arguments['charttitle'], size=self._arguments['titlesize'], y=1.05)
        # x-Label
        if self._arguments['xlabel'] == 'default':
            ax1.set_xlabel(self._arguments['xvar'], size=self._arguments['labelsize'])
        else:
            ax1.set_xlabel(self._arguments['xlabel'], size=self._arguments['labelsize'])
        # y-Label
        if self._arguments['ylabel'] == 'default':
            ax1.set_ylabel(self._arguments['yvar'] + ' (mean)', size=self._arguments['labelsize'])
        else:
            ax1.set_ylabel(self._arguments['ylabel'], size=self._arguments['labelsize'])
        if self._arguments['ylabel'] == 'default':
            if self._arguments['estimator'] == 'percent' or self._arguments['resp_in_pct']:
                ax1.set_ylabel(str(self._arguments['yvar']) + ' (%)')
            else:
                ax1.set_ylabel(str(self._arguments['yvar']) + ' (' + myestimator + ')')
        else:
            ax1.set_ylabel(self._arguments['ylabel'])

        if self._arguments['groupvar'] == 'default':
            ax2 = ax1.twinx()
            ax2.plot_date(data_df_summ[self._arguments['xvar']], data_df_summ[self._arguments['resp']], fmt='.',  color='k', marker='s', markersize=mymarkersize/1.5, markerfacecolor='red', markeredgecolor='red')
            ax2.set_xlim(xmin=xvals[0], xmax=xvals[1])
            if self._arguments['y2label'] == 'default':
                if self._arguments['estimator'] == 'percent' or self._arguments['resp_in_pct']:
                    ax2.set_ylabel(str(self._arguments['resp']) + ' (%)')
                else:
                    ax2.set_ylabel(str(self._arguments['resp']) + ' (' + myestimator + ')')
            else:
                ax2.set_y2label(self._arguments['y2label'])
            ax2.patch.set_visible(False)
            ax2.grid(False)
            legend_elements = [Line2D([0], [0], marker='.', label='Comm_Col', color='navy', markersize=mymarkersize, markerfacecolor='navy', markeredgecolor='navy'),
                               Line2D([0], [0], marker='s', label='Response', color='red', markerfacecolor='red', markeredgecolor='red', markersize=mymarkersize/1.5)]
            plt.legend(handles=legend_elements, loc=2, bbox_to_anchor=(1.05, 1), borderaxespad=0.0)
        plt.tight_layout()
        plt.close()

    def Color_to_RBGA(self, color):
        self._logger.debug('Running Color_to_RBGA method')
        print('The RBGA value is: ' + str(mpl.colors.to_rgba(color)))

    def Save_figure(self):
        """
        :Description:
                Saves matplotlib figure with Correlation Plot to a file
        :return:             This method doesn't return anything, only saves the figure
        """
        self._logger.debug('Running Save_figure method')
        self.fig.savefig(self._arguments['filename'])


class Carrier_Map(object):
    def __init__(self, inputs, interactive_run=True, logger=None):
        '''
        Description:
            Init method for the class

        Arguments:
            inputs:       Dictionary with arguments passed by code creating the Carrier map

        Auto-Execution:
                    _Carrier_plot - Creates the matplotlib figure for the strip map

        Object Variables:
                    fig - matplotlib figure with plot
        '''
        self._logger = logger
        self._logger.debug('Running Carrier_Map class init method')
        warnings.filterwarnings("ignore")
        self._interactive_run = interactive_run
        default_inputs = {'data_df': pd.DataFrame(),
                          'xvar': '',
                          'yvar': '',
                          'resp': '',
                          'filename': '',
                          'carrierx': 150,
                          'carriery': 50,
                          'minresp': 99999.9,
                          'maxresp': 99999.9,
                          'xunits': 99999,
                          'yunits': 99999,
                          'estimator': 'mean',
                          'resp_in_pct': True,
                          'scale_label': 'default',
                          'minlocx': 99999,
                          'minlocy': 99999,
                          'isstrip': False,
                          'figsizex': 10.0,
                          'figsizey': 7.0,
                          'dpi': 120,
                          'jitter': True,
                          'border_prop': 0.03,
                          'unitspacingx': 0.2,
                          'unitspacingy': 0.2,
                          'stripgap_prop': 0.03,
                          'color_map': 'Blues',
                          'charttitle': 'default',
                          'titlesize': 20
                          }
        sns.set_style("whitegrid")
        self._arguments = BuildArgs(inputs, default_inputs, ini_section='CARRIER MAP CONFIGURATION', logger=self._logger)
        self._CheckResponse()
        self._df = self._arguments['data_df']
        self._df_all = self._arguments['data_df_all']
        if len(self._df[self._arguments['xvar']].drop_duplicates()) > len(self._df[self._arguments['yvar']].drop_duplicates()):
            self._xvar = self._arguments['xvar']
            self._yvar = self._arguments['yvar']
        else:
            self._xvar = self._arguments['yvar']
            self._yvar = self._arguments['xvar']
        self._resp = self._arguments['resp']
        self._arguments['border'] = self._arguments['carrierx'] * self._arguments['border_prop']
        self._arguments['useareax'] = self._arguments['carrierx'] - 2 * self._arguments['border']
        self._arguments['useareay'] = self._arguments['carriery'] - 2 * self._arguments['border']

        self._CheckVars()
        self._carrier_vals = self._Find_carrier_defaults()

        if self._arguments['minlocx'] == 99999:
            self._arguments['minlocx'] = self._carrier_vals[0]
        if self._arguments['minlocy'] == 99999:
            self._arguments['minlocy'] = self._carrier_vals[2]

        if self._arguments['xunits'] == 99999:
            self._arguments['xunits'] = self._carrier_vals[1] - self._arguments['minlocx'] + 1
        if self._arguments['yunits'] == 99999:
            self._arguments['yunits'] = self._carrier_vals[3] - self._arguments['minlocy'] + 1

        if self._arguments['isstrip']:
            self._arguments['unitgapx'] = 0
            self._arguments['unitgapy'] = 0
            self._arguments['stripgap'] = self._arguments['useareax'] * self._arguments['stripgap_prop']
            self._arguments['unitsizex'] = (self._arguments['useareax'] - self._arguments['stripgap']) / self._arguments['xunits']
            self._arguments['unitsizey'] = self._arguments['useareay'] / self._arguments['yunits']
        else:
            self._arguments['stripgap'] = 0
            self._arguments['unitsizex'] = self._arguments['useareax'] * (1 - self._arguments['unitspacingx']) / self._arguments['xunits']
            if self._arguments['xunits'] - 1 == 0:
                self._arguments['unitgapx'] = 0
            else:
                self._arguments['unitgapx'] = (self._arguments['useareax'] - self._arguments['unitsizex'] * self._arguments['xunits']) / (self._arguments['xunits'] - 1)
            self._arguments['unitsizey'] = self._arguments['useareay'] * (1 - self._arguments['unitspacingy']) / self._arguments['yunits']
            if self._arguments['yunits'] - 1 == 0:
                self._arguments['unitgapy'] = 0
            else:
                self._arguments['unitgapy'] = (self._arguments['useareay'] - self._arguments['unitsizey'] * self._arguments['yunits']) / (self._arguments['yunits'] - 1)

        myestimator = self._GetEstimator()

        self._total_vals = self._df[self._resp].count()
        self._sum_vals = self._df[self._resp].sum()

        self._df_summ = self._df
        self._df_summ = self._df_summ[[self._xvar, self._yvar, self._resp]]
        self._df_summ = self._df_summ.groupby([self._xvar, self._yvar])
        self._df_summ = self._df_summ.agg({self._resp: myestimator})

        if self._arguments['estimator'] == 'percent' or self._arguments['resp_in_pct']:
            self._df_summ[self._resp] = self._df_summ[self._resp] * 100

        self._df_summ.reset_index(inplace=True)
        self._Carrier_plot()

    def _CheckResponse(self):
        """
        Description:
                Internal Function - Checks that the response variable is continuous or 1/0
        :return:            N/A
        """
        self._logger.debug('Running _CheckResponse method')
        if len(self._arguments['data_df'][self._arguments['resp']].drop_duplicates()) <= 5:
            try:
                self._arguments['data_df'][self._arguments['resp']] = self._arguments['data_df'][self._arguments['resp']].astype('int64')
            except:
                pass
        else:
            try:
                self._arguments['data_df'][self._arguments['resp']] = self._arguments['data_df'][self._arguments['resp']].astype('float64')
            except:
                pass
        if self._arguments['data_df'][self._arguments['resp']].dtype in ['int64', 'float64', 'Int64', 'Float64', 'float']:
            myrespvals = list(self._arguments['data_df'][self._arguments['resp']].drop_duplicates())
            if len(myrespvals) > 2 or 1 not in myrespvals or 0 not in myrespvals:
                self._arguments['resp_in_pct'] = False
                self._arguments['resp_good_bad'] = False
            else:
                self._arguments['resp_good_bad'] = True
        else:
            print('\n')
            self._logger.error("======================================================================================")
            self._logger.error("Carrier Maps need 0/1 (Good/Bad) or continuous values in the response variable.")
            self._logger.error("Values for response: " + str(self._arguments['resp']) + " don't meet the requirement. Exiting ...")
            self._logger.error("======================================================================================")
            print("\n")
            print("\n")
            if self._interactive_run:
                sys.exit(1)

    def _GetEstimator(self):
        self._logger.debug('Running _GetEstimator method')
        try:
            if self._arguments['estimator'] == 'percent':
                myestimator = 'mean'
            elif self._arguments['estimator'] == 'median':
                myestimator = 'median'
            elif self._arguments['estimator'] == 'mean':
                myestimator = 'mean'
        except:
            self._logger.warning('###############################################')
            self._logger.warning("Error in estimator provided.....using 'mean'")
            self._logger.warning("Estimators allowed are:")
            self._logger.warning("         -'mean' (default)")
            self._logger.warning("         -'median'")
            self._logger.warning("         -'percent'")
            self._logger.warning('###############################################')
            myestimator = 'mean'
        return myestimator

    def _Find_carrier_defaults(self):
        """
        Description:
                Internal Function - Gets min and max for carrier pockets
        :return:    Minimium X, Maximum X, Minimum Y, Maximum Y in a list
        """
        self._logger.debug('Running _Find_carrier_defaults method')
        max_val_x = self._df_all[self._xvar].max(skipna=True)
        min_val_x = self._df_all[self._xvar].min(skipna=True)
        max_val_y = self._df_all[self._yvar].max(skipna=True)
        min_val_y = self._df_all[self._yvar].min(skipna=True)
        return [min_val_x, max_val_x, min_val_y, max_val_y]

    def _CheckVars(self):
        """
        Description:
                Internal Function - Checks that the response variable and the carrier x and y are numeric
                                    Additionally it deletes all the rows that don't have complete data since they can't be graphed
        :return:            N/A
        """
        self._logger.debug('Running _CheckVars method')
        self._df = self._df[~pd.isnull(self._df[self._xvar])]
        self._df = self._df[~pd.isnull(self._df[self._yvar])]
        self._df = self._df[~pd.isnull(self._df[self._resp])]
        if len(self._arguments['data_df'][self._arguments['resp']].drop_duplicates()) <= 5:
            try:
                self._arguments['data_df'][self._arguments['resp']] = self._arguments['data_df'][self._arguments['resp']].astype('int64')
            except:
                pass
        else:
            try:
                self._arguments['data_df'][self._arguments['resp']] = self._arguments['data_df'][self._arguments['resp']].astype('float64')
            except:
                pass
        if self._df[self._resp].dtype in ['int64', 'float64', 'Int64', 'Float64', 'float']:
            pass
        else:
            print('\n')
            self._logger.error("======================================================================================")
            self._logger.error("Carrier/Strip Plots require a numeric Response Variable.")
            self._logger.error("Values for: " + str(self._resp) + " don't meet the requirement. Exiting ...")
            self._logger.error("======================================================================================")
            print("\n")
            print("\n")
            if self._interactive_run:
                sys.exit(1)
        myxvals = self._df[self._xvar].drop_duplicates()
        errorsFound = False
        for mv in myxvals:
            try:
                if mv % 1 != 0:
                    errorsFound = True
            except:
                errorsFound = True
        if errorsFound:
            self._logger.error("======================================================================================")
            self._logger.error("Carrier/Strip Plots require an integer X value.")
            self._logger.error("Values for: " + str(self._resp) + " don't meet the requirement. Exiting ...")
            self._logger.error("======================================================================================")
            print("\n")
            print("\n")
            if self._interactive_run:
                sys.exit(1)
        else:
            self._df[self._xvar] = self._df[self._xvar].astype('int64')
        myyvals = self._df[self._yvar].drop_duplicates()
        errorsFound = False
        for mv in myyvals:
            try:
                if mv % 1 != 0:
                    errorsFound = True
            except:
                errorsFound = True
        if errorsFound:
            print('\n')
            self._logger.error("======================================================================================")
            self._logger.error("Carrier/Strip Plots require an integer Y value.")
            self._logger.error("Values for: " + str(self._resp) + " don't meet the requirement. Exiting ...")
            self._logger.error("======================================================================================")
            print("\n")
            print("\n")
            if self._interactive_run:
                sys.exit(1)
        else:
            self._df[self._yvar] = self._df[self._yvar].astype('int64')

    def _Carrier_plot(self):
        """
        Description:
                Internal Function - Plots the figure with the carrier
        :return:            N/A
        """
        self._logger.debug('Running _Carrier_plot method')
        self.fig = plt.figure(figsize=(self._arguments['figsizex'], self._arguments['figsizey']))
        ax = plt.subplot2grid((5, 15), (0, 0), colspan=14, rowspan=5)
        ax.set_aspect('equal')
        ax.axis('off')
        ax.set_xlim((0, self._arguments['carrierx']))
        ax.set_ylim((0, self._arguments['carriery'] + self._arguments['carriery'] * 0.1))
        carrier = mpl.patches.Rectangle((0, 0), self._arguments['carrierx'], self._arguments['carriery'], color='lightgrey', ec='black')
        ax.add_patch(carrier)
        ax.set_facecolor('White')
        if self._arguments['minresp'] == 99999.9:
            self._arguments['minresp'] = min(self._df_summ[self._resp])
        if self._arguments['maxresp'] == 99999.9:
            self._arguments['maxresp'] = max(self._df_summ[self._resp])
        norm = mpl.colors.Normalize(vmin=self._arguments['minresp'], vmax=self._arguments['maxresp'])
        cmap = cm.get_cmap(self._arguments['color_map'])
        for i in range(int(self._arguments['xunits'])):
            midgap = self._arguments['xunits'] // 2
            if i >= midgap:
                extra_space = self._arguments['stripgap']
            else:
                extra_space = 0
            for j in range(int(self._arguments['yunits'])):
                unit = mpl.patches.Rectangle(
                    (self._arguments['border'] + (self._arguments['unitsizex'] + self._arguments['unitgapx']) * i + extra_space,
                     self._arguments['border'] + (self._arguments['unitsizey'] + self._arguments['unitgapy']) * j),
                    self._arguments['unitsizex'], self._arguments['unitsizey'], color='white', ec='black')
                ax.add_patch(unit)

        for k in self._df_summ[self._xvar]:
            midgap = self._arguments['xunits'] // 2
            if k >= midgap:
                extra_space = self._arguments['stripgap']
            else:
                extra_space = 0
            for l in self._df_summ[self._yvar]:
                i = k - self._arguments['minlocx']
                j = l - self._arguments['minlocy']
                if len(self._df_summ[(self._df_summ[self._xvar] == k) & (self._df_summ[self._yvar] == l)][self._resp]) > 0:
                    colvalue = float(self._df_summ[(self._df_summ[self._xvar] == k) & (self._df_summ[self._yvar] == l)][self._resp])
                    unit = mpl.patches.Rectangle(
                        (self._arguments['border'] + (self._arguments['unitsizex'] + self._arguments['unitgapx']) * i + extra_space,
                         self._arguments['border'] + (self._arguments['unitsizey'] + self._arguments['unitgapy']) * j),
                         self._arguments['unitsizex'], self._arguments['unitsizey'], color=cmap(norm(colvalue)), ec='gray')
                    ax.add_patch(unit)
        if self._arguments['estimator'] == 'percent' or self._arguments['resp_in_pct']:
            if self._total_vals > 0:
                textstr = '{} Rate: {}/{} = {}%'.format(self._resp, self._sum_vals, self._total_vals, round(self._sum_vals / self._total_vals * 100, 3))
                ax.text(0, -5, textstr, horizontalalignment='left', verticalalignment='center', color='blue', fontsize=12)

        ax2 = plt.subplot2grid((5, 15), (1, 14), rowspan=3)
        cb1 = mpl.colorbar.ColorbarBase(ax2, cmap=cmap, norm=norm)
        # ax2.set_aspect(0.8)
        if self._arguments['scale_label'] == 'default':
            if self._arguments['scale_label'] == 'default':
                if self._arguments['estimator'] == 'percent' or self._arguments['resp_in_pct']:
                    ax2.set_ylabel(self._resp + ' (%)', labelpad=10)
                else:
                    ax2.set_ylabel(self._resp + ' (' + self._arguments['estimator'] + ')', labelpad=10)
        else:
            ax2.set_ylabel(self._arguments['scale_label'], labelpad = 10)
        if self._arguments['charttitle'] == 'default':
            mytitle = 'Carrier map for ' + self._resp
        else:
            mytitle = self._arguments['charttitle']
        self.fig.suptitle(mytitle, fontsize=self._arguments['titlesize'], color='navy')
        plt.tight_layout()
        plt.close()

    def Save_figure(self):
        """
        :Description:
                Saves matplotlib figure with the Carrier plot to a file
        :return:             This method doesn't return anything, only saves the figure
        """
        self._logger.debug('Running Save_figure method')
        self.fig.savefig(self._arguments['filename'])
        return self._arguments['minresp'], self._arguments['maxresp']


class Wafer_Map(object):
    def __init__(self, inputs, interactive_run=True, logger=None):
        '''
        Description:
            Init method for the class

        Arguments:
            inputs:       Dictionary with arguments passed by code creating the HTML report

        Auto-Execution:
                    _Check_vars: Checks that the response variable and the wafer x and y are numeric
                    _Wafer_plot - Creates the matplotlib figure for the wafer plot

        Object Variables:
                    fig - matplotlib figure with plot
        '''
        self._logger = logger
        self._logger.debug('Running Wafer_Map class init method')
        warnings.filterwarnings("ignore")
        self._interactive_run = interactive_run
        default_inputs = {'data_df': pd.DataFrame(),
                          'xvar': '',
                          'yvar': '',
                          'resp': '',
                          'filename': '',
                          'resp_in_pct': True,
                          'estimator': 'mean',
                          'scale_label': 'default',
                          'diameter': 300,
                          'minlocx': 99999,
                          'maxlocx': 99999,
                          'minlocy': 99999,
                          'maxlocy': 99999,
                          'die_pitch_x': 99999,
                          'die_pitch_y': 99999,
                          'figsizex': 8.0,
                          'figsizey': 8.0,
                          'dpi': 120,
                          'color_map': 'Blues',
                          'charttitle': 'default',
                          'titlesize': 20
                          }
        sns.set_style("whitegrid")
        self._arguments = BuildArgs(inputs, default_inputs, ini_section='WAFER MAP CONFIGURATION', logger=self._logger)
        self._CheckResponse()
        self._df = self._arguments['data_df']
        self._df_all = self._arguments['data_df_all']
        self._xvar = self._arguments['xvar']
        self._yvar = self._arguments['yvar']
        self._resp = self._arguments['resp']

        self._CheckVars()
        self._wafer_vals = self._Find_wafer_defaults()

        if self._arguments['minlocx'] == 99999:
            self._arguments['minlocx'] = self._wafer_vals[0]
        if self._arguments['maxlocx'] == 99999:
            self._arguments['maxlocx'] = self._wafer_vals[1]
        if self._arguments['minlocy'] == 99999:
            self._arguments['minlocy'] = self._wafer_vals[2]
        if self._arguments['maxlocy'] == 99999:
            self._arguments['maxlocy'] = self._wafer_vals[3]

        self._arguments['xvalues'] = self._arguments['maxlocx'] - self._arguments['minlocx'] + 0.75
        self._arguments['yvalues'] = self._arguments['maxlocy'] - self._arguments['minlocy'] + 0.75
        if self._arguments['die_pitch_x'] == 99999:
            self._arguments['die_pitch_x'] = 300 / (self._arguments['xvalues'] + 3)
        if self._arguments['die_pitch_y'] == 99999:
            self._arguments['die_pitch_y'] = 300 / (self._arguments['yvalues'] + 3)

        self._arguments['centerx'] = (self._arguments['maxlocx'] * self._arguments['die_pitch_x'] + self._arguments['die_pitch_x'] -
                                      self._arguments['minlocx'] * self._arguments['die_pitch_x']) / 2 + self._arguments['minlocx'] * self._arguments['die_pitch_x']
        self._arguments['centery'] = (self._arguments['maxlocy'] * self._arguments['die_pitch_y'] + self._arguments['die_pitch_y'] -
                                      self._arguments['minlocy'] * self._arguments['die_pitch_y']) / 2 + self._arguments['minlocy'] * self._arguments['die_pitch_y']

        myestimator = self._GetEstimator()

        self._total_vals = self._df[self._resp].count()
        self._sum_vals = self._df[self._resp].sum()
        self._df_summ = self._df
        self._df_summ = self._df_summ[[self._xvar, self._yvar, self._resp]]
        self._df_summ = self._df_summ.loc[:, ~self._df_summ.columns.duplicated()]
        self._df_summ = self._df_summ.groupby([self._xvar, self._yvar])
        self._df_summ = self._df_summ.agg({self._resp: myestimator})
        if self._arguments['estimator'] == 'percent' or self._arguments['resp_in_pct']:
            self._df_summ[self._resp] = self._df_summ[self._resp] * 100
        self._df_summ.reset_index(inplace=True)
        if 'wafer_param' in self._arguments.keys():
            self._df_summ_all = self._df_all
            self._df_summ_all = self._df_summ_all[[self._xvar, self._yvar, self._resp]]
            self._df_summ_all = self._df_summ_all.loc[:, ~self._df_summ_all.columns.duplicated()]
            self._df_summ_all = self._df_summ_all.groupby([self._xvar, self._yvar])
            self._df_summ_all = self._df_summ_all.agg({self._resp: myestimator})
            if self._arguments['estimator'] == 'percent' or self._arguments['resp_in_pct']:
                self._df_summ_all[self._resp] = self._df_summ_all[self._resp] * 100
            self._df_summ_all.reset_index(inplace=True)
        self._Wafer_plot()

    def _CheckResponse(self):
        """
        Description:
                Internal Function - Checks that the response variable is continuous or 1/0
        :return:            N/A
        """
        self._logger.debug('Running _CheckResponse method')
        if len(self._arguments['data_df'][self._arguments['resp']].drop_duplicates()) <= 5:
            try:
                self._arguments['data_df'][self._arguments['resp']] = self._arguments['data_df'][self._arguments['resp']].astype('int64')
            except:
                pass
        else:
            try:
                self._arguments['data_df'][self._arguments['resp']] = self._arguments['data_df'][self._arguments['resp']].astype('float64')
            except:
                pass
        if self._arguments['data_df'][self._arguments['resp']].dtype in ['int64', 'float64', 'Int64', 'Float64', 'float']:
            myrespvals = list(self._arguments['data_df'][self._arguments['resp']].drop_duplicates())
            if len(myrespvals) > 2 or 1 not in myrespvals or 0 not in myrespvals:
                self._arguments['resp_in_pct'] = False
                self._arguments['resp_good_bad'] = False
            else:
                self._arguments['resp_good_bad'] = True
        else:
            print("\n")
            self._logger.error("\n======================================================================================")
            self._logger.error("Wafer Maps need 0/1 (Good/Bad) or continuous values in the response variable.")
            self._logger.error("Values for response: " + str(self._arguments['yvar']) + " don't meet the requirement. Exiting ...")
            self._logger.error("======================================================================================")
            print("\n")
            print("\n")
            if self._interactive_run:
                sys.exit(1)

    def _GetEstimator(self):
        self._logger.debug('Running _GetEstimator method')
        try:
            if self._arguments['estimator'] == 'percent':
                myestimator = 'mean'
            elif self._arguments['estimator'] == 'median':
                myestimator = 'median'
            elif self._arguments['estimator'] == 'mean':
                myestimator = 'mean'
            # elif self._arguments['estimator'][:8] == 'quantile':
            #     val = self._arguments['estimator'][9:self._arguments['estimator'].find(')')]
            #     val = float(val)
            #     if val >= 0 and val <= 1:
            #         val = val * 100.0
            #         myestimator = lambda x: np.nanpercentile(x, val)
            #     else:
            #         raise Exception
            # elif self._arguments['estimator'][:10] == 'percentile':
            #     val = self._arguments['estimator'][11:self._arguments['estimator'].find(')')]
            #     val = float(val)
            #     if val >= 0 and val <= 100:
            #         myestimator = lambda x: np.nanpercentile(x, val)
            #     else:
            #         raise Exception
            # else:
            #     raise Exception
        except:
            self._logger.warning('###############################################')
            self._logger.warning("Error in estimator provided.....using 'mean'")
            self._logger.warning("Estimators allowed are:")
            self._logger.warning("         -'mean' (default)")
            self._logger.warning("         -'median'")
            self._logger.warning("         -'percent'")
            self._logger.warning('###############################################')
            myestimator = 'mean'
        return myestimator

    def _Find_wafer_defaults(self):
        self._logger.debug('Running _Find_wafer_defaults method')
        """
        Description:
                Internal Function - Gets min and max for wafer locations
        :return:    Minimium X, Maximum X, Minimum Y, Maximum Y in a list
        """
        max_val_x = self._df_all[self._xvar][self._df_all[self._xvar] <= 75].max(skipna=True)
        min_val_x = self._df_all[self._xvar][self._df_all[self._xvar] <= 75].min(skipna=True)
        max_val_y = self._df_all[self._yvar][self._df_all[self._yvar] <= 75].max(skipna=True)
        min_val_y = self._df_all[self._yvar][self._df_all[self._yvar] <= 75].min(skipna=True)
        max_x_abs = max(max_val_x, abs(min_val_x))
        if max_val_x < max_x_abs - 1:
            max_val_x = max_x_abs
        if abs(min_val_x) < max_x_abs - 1:
            min_val_x = -max_x_abs
        max_y_abs = max(max_val_y, abs(min_val_y))
        if max_val_y < max_y_abs - 1:
            max_val_y = max_y_abs
        if abs(min_val_y) < max_y_abs - 1:
            min_val_y = -max_y_abs
        return [min_val_x, max_val_x, min_val_y, max_val_y]

    def _CheckVars(self):
        """
        Description:
                Internal Function - Checks that the response variable and the wafer x and y are numeric
                                    Additionally it deletes all the rows that don't have complete data since they can't be graphed
        :return:            N/A
        """
        self._logger.debug('Running _CheckVars method')
        self._df = self._df[~pd.isnull(self._df[self._xvar])]
        self._df = self._df[~pd.isnull(self._df[self._yvar])]
        self._df = self._df[~pd.isnull(self._df[self._resp])]
        if self._df[self._resp].dtype in ['int64', 'float64', 'Int64', 'Float64', 'float']:
            pass
        else:
            print("\n")
            self._logger.error("======================================================================================")
            self._logger.error("Wafer Plots require a numeric Response Variable.")
            self._logger.error("Values for: " + str(self._resp) + " don't meet the requirement. Exiting ...")
            self._logger.error("======================================================================================")
            print("\n")
            print("\n")
            if self._interactive_run:
                sys.exit(1)
        myxvals = self._df[self._xvar].drop_duplicates()
        errorsFound = False
        for mv in myxvals:
            try:
                if mv % 1 != 0:
                    errorsFound = True
            except:
                errorsFound = True
        if errorsFound:
            print("\n")
            self._logger.error("======================================================================================")
            self._logger.error("Wafer Plots require an integer X value.")
            self._logger.error("Values for: " + str(self._xvar) + " don't meet the requirement. Exiting ...")
            self._logger.error("======================================================================================")
            print("\n")
            print("\n")
            if self._interactive_run:
                sys.exit(1)
        else:
            self._df[self._xvar] = self._df[self._xvar].astype('int64')
        myyvals = self._df[self._yvar].drop_duplicates()
        errorsFound = False
        for mv in myyvals:
            try:
                if mv % 1 != 0:
                    errorsFound = True
            except:
                errorsFound = True
        if errorsFound:
            print("\n")
            self._logger.error("======================================================================================")
            self._logger.error("Wafer Plots require an integer Y value.")
            self._logger.error("Values for: " + str(self._yvar) + " don't meet the requirement. Exiting ...")
            self._logger.error("======================================================================================")
            print("\n")
            print("\n")
            if self._interactive_run:
                sys.exit(1)
        else:
            self._df[self._yvar] = self._df[self._yvar].astype('int64')

    def _Wafer_plot(self):
        """
        Description:
                Internal Function - Plots the figure with the wafer
        :return:            N/A
        """
        self._logger.debug('Running _Wafer_plot method')
        self.fig = plt.figure(figsize=(self._arguments['figsizex'], self._arguments['figsizey']))
        ax = plt.subplot2grid((1, 15), (0, 0), colspan=14)
        ax.set_aspect('equal')
        ax.axis('off')
        ax.set_xlim((self._arguments['centerx'] - 150 * 1.01, self._arguments['centerx'] + 150 * 1.05))
        ax.set_ylim((self._arguments['centery'] - 150 * 1.01, self._arguments['centery'] + 150 * 1.01))
        wafer = mpl.patches.Circle((self._arguments['centerx'], self._arguments['centery']), radius=150, color='white', ec='black')
        ax.add_patch(wafer)
        ax.set_facecolor('White')
        if 'wafer_param' not in self._arguments.keys():
            norm = mpl.colors.Normalize(vmin=self._df_summ[self._resp].min(), vmax=self._df_summ[self._resp].max())
        else:
            norm = mpl.colors.Normalize(vmin=self._df_summ_all[self._resp].min(), vmax=self._df_summ_all[self._resp].max())
        cmap = cm.get_cmap(self._arguments['color_map'])
        for i in range(int(abs(self._arguments['minlocx'])), int(abs(self._arguments['maxlocx'])) + 1):
            for j in range(int(abs(self._arguments['minlocy'])), int(abs(self._arguments['maxlocy'])) + 1):
                xval = max(abs(i * self._arguments['die_pitch_x'] - self._arguments['centerx']),
                           abs(i * self._arguments['die_pitch_x'] + self._arguments['die_pitch_x'] - self._arguments['centerx'])
                           )
                yval = max(abs(j * self._arguments['die_pitch_y'] - self._arguments['centery']),
                           abs(j * self._arguments['die_pitch_y'] + self._arguments['die_pitch_y'] - self._arguments['centery'])
                           )
                dist = np.sqrt(xval**2 + yval**2)
                if dist < 150:
                    unit = mpl.patches.Rectangle((i * self._arguments['die_pitch_x'], j * self._arguments['die_pitch_y']),
                                                 self._arguments['die_pitch_x'], self._arguments['die_pitch_y'], color='white', ec='gainsboro', alpha=0.3)
                    ax.add_patch(unit)
        for i in self._df_summ.iterrows():
            dielocx = i[1][self._xvar]
            dielocy = i[1][self._yvar]
            colvalue = float(self._df_summ[(self._df_summ[self._xvar] == dielocx) & (self._df_summ[self._yvar] == dielocy)][self._resp])
            unit = mpl.patches.Rectangle((dielocx * self._arguments['die_pitch_x'], dielocy * self._arguments['die_pitch_y']),
                                         self._arguments['die_pitch_x'], self._arguments['die_pitch_y'], color=cmap(norm(colvalue)), ec='gray')
            ax.add_patch(unit)
        if self._arguments['estimator'] == 'percent' or self._arguments['resp_in_pct']:
            if self._total_vals > 0:
                textstr = '{} Rate: {}/{} = {}%'.format(self._resp, self._sum_vals, self._total_vals, round(self._sum_vals / self._total_vals * 100, 3))
                ax.text(0, -160, textstr, horizontalalignment='center', verticalalignment='center', color='blue', fontsize=12)
        ax2 = plt.subplot2grid((1, 15), (0, 14))
        cb1 = mpl.colorbar.ColorbarBase(ax2, cmap=cmap, norm=norm)
        # if 'wafer_param' not in self._arguments.keys():
        #     ax2.set_aspect(0.12)
        if self._arguments['scale_label'] == 'default':
            if self._arguments['estimator'] == 'percent' or self._arguments['resp_in_pct']:
                ax2.set_ylabel(self._resp + ' (%)', labelpad = 10)
            else:
                ax2.set_ylabel(self._resp + ' (' + self._arguments['estimator'] + ')', labelpad = 10)
        else:
            ax2.set_ylabel(self._arguments['scale_label'], labelpad = 10)
        if self._arguments['charttitle'] == 'default':
            mytitle = 'Wafer map for ' + self._resp
        else:
            mytitle = self._arguments['charttitle']
        self.fig.suptitle(mytitle, fontsize=self._arguments['titlesize'], color='navy')
        plt.tight_layout()
        plt.close()

    def Save_figure(self):
        """
        :Description:
                Saves matplotlib figure with the Wafer plot to a file
        :return:             This method doesn't return anything, only saves the figure
        """
        self._logger.debug('Running Save_figure method')
        self.fig.savefig(self._arguments['filename'])



if __name__ == "__main__":
    pass
    # import pickle
    # from SPF_DF import SPF_DF
    # import os
    #
    # ###Basic Charts
    #
    # # with open(r"C:\Data\Mongo\results_dict.pickle", "rb") as pickle_in:
    # #     results_dict = pickle.load(pickle_in)
    # # with open(r"C:\Data\Mongo\results_list.pickle", "rb") as pickle_in:
    # #     all_key_vars = pickle.load(pickle_in)
    # # all_key_vars = all_key_vars + ['ib15', 'fb1530', 'fb112']
    # # data_file = r"C:\Data\Mongo\BDX-EP_Data_v2.csv"
    # # data_file = r"C:\Data\Mongo\BDX-EP_Data_v2.csv"
    #
    # all_key_vars = ['7721IB', '7721OUTDATE','1204HOLDFORCEDURATION_21', '1204OUTDATE','ib15', 'fb1530', 'fb112']
    # data_file = r"C:\Data\Mongo\CATTS_Data_v2.csv"
    # spf_df = SPF_DF(data_file, 'catts', all_key_vars).df
    #
    # #
    # myvar = '7721IB'
    # mydate = '7721OUTDATE'
    # #
    # spf_df[mydate] = pd.to_datetime(spf_df[mydate], format="%m/%d/%Y %H:%M:%S")
    # #
    # # # mygroup = 'll#attrlot#sol_preo1_sli'
    # # # testcol = 'ul#attr#7721_cpbic1#ldbid'
    # # # myres = 'ib15'
    # # #
    # #
    # myinput = {'data_df': spf_df,
    #            'yvar': myvar,
    #            'xvar': mydate,
    #            # 'groupvar': mygroup,
    #            'filename': r'C:\Data\graphs\myplot.png',
    #            'estimator': 'median',
    #            'charttitle': 'Test_title',
    #            'figsizex': 9,
    #            'figsizey': 4,
    #            'showline': False,
    #            'showgrid': True}
    # #
    # mychart = Basic_Charts(myinput)
    # mychart.Timetrend()
    # mychart.Save_figure()
    #
    # ###Carrier Map
    #
    # data_file = r'C:\\Data\\Mongo\\BDX-EP_Data_v2.csv'
    # with open(r"C:\Data\Mongo\results_list.pickle", "rb") as pickle_in:
    #     all_key_vars = pickle.load(pickle_in)
    # spf_df = SPF_DF(data_file, all_key_vars)
    # spf_df = spf_df.df
    #
    # inputs = {'data_df': spf_df,
    #           'xvar': 'ul#attr#1252_sihs#media_x_location',
    #           'yvar': 'ul#attr#1252_sihs#media_y_location',
    #           'resp': 'ib15',
    #           'filename': r"C:\Data\graphs\mycarrier.png"}
    #
    # mycarrier = Carrier_Map(inputs)
    # mycarrier.Save_figure()
    #



    # data_df = pd.read_csv(r"C:\Data\examples\2a CD6 ARP TEST B8_CA_REPORT_02.17.11@18.22.35_199857_Sample_clean.csv", usecols=['1210ENTITY', 'RESPONSE_FLAG', '1210OUTDATE'])
    # data_df.to_csv(r"C:\Data\examples\outdate_csv.csv", index=False)

    # mygroups = ['1210OUTDATE', '1210ENTITY']
    # data_df_summ = data_df.groupby(mygroups)
    # df_summary = df_grouped.agg({'RESPONSE_FLAG': 'mean', 'col2': 'max', 'col3': 'median'})


    # default_inputs = {'data_df': pd.DataFrame(),
                      # 'xvar': '',
                      # 'yvar': '',
                      # 'filename': '',
                      # 'groupvar': 'default',
                      # 'estimator': 'mean',
                      # 'colx': 'not_implemented',
                      # 'coly': 'not_implemented',
                      # 'col_wrap': 'not_implemented',
                      # 'figsizex': 6,
                      # 'figsizey': 6,
                      # 'timefigsizex': 10,
                      # 'timefigsizey': 6,
                      # 'dpi': 120,
                      # 'maxgroups': 20,
                      # 'jitter': True,
                      # 'sharey': 'default',
                      # 'dotedgecolor': (0.3, 0.3, 0.3, 1),
                      # 'dotlinewidth': 1,
                      # 'dotalpha': 0.4,
                      # 'dotsize': 4,
                      # 'labelsize': 12,
                      # 'xlabelrotation': 30,
                      # 'xlabel': 'default',
                      # 'ylabel': 'default',
                      # 'charttitle': 'default',
                      # 'titlesize': 16,
                      # 'showline': False,
                      # 'linewidth': 2,
                      # 'showmarkers': True,
                      # 'markersize': 7,
                      # 'showci': False,
                      # 'legendloc': 'outside',
                      # 'showgrid': True,
                      # 'markerlist': ['o', 'X', 'v', 's', '*', 'p', '<', '8', 'h', '^', 'D', '>', 'd', '.'] * 10
                      # }
    # input = {}
    # input['xvar'] = outdate_var
    # input['yvar'] = self._tab
    # input['groupvar'] = self._var
    # input['figsizex'] = 8
    # input['figsizey'] = 5
    # input['filename'] = self._var.replace('#', '+') + '_time_trend_disc.png'
    # charts_loaded.append(input['filename'])
    # input['filename'] = os.path.join(self._report_dir, input['filename'])
    # mytt = Basic_Charts(input)
    # mytt.Timetrend()
    # mytt.Save_figure()
    pass