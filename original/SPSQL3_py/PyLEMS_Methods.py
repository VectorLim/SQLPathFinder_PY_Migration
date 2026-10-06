"""
-----------------------------------------------------
Procedure: PyLEMS_Methods.py      Date: 07/31/2024


Version History:
========================================================================================================================
| Date         |  Who        | Ver    |  Description                                                                   |
========================================================================================================================
| 07/31/2024   |  gcarmiol   |  1.0   |  Initial development                                                           |
| 06/14/2024   |  gcarmiol   |  1.1   |  Fixes bug where there might not be enough data to calculate interactions      |
| 01/27/2024   |  gcarmiol   |  1.2   |  Validation of interactions to ensure they come from same die                  |
========================================================================================================================

Description:
============
SQLPathFinder PyLEMS implementation was created with the collaboration of Farzin Guilak from yield and Rajath Kantharaj
from module engineering.  The code automates the execution of LEMS in Screen and Visualize Features Utility and
creates interaction charts previously not available in previous machine learning models.

"""
__version__ = '1.2'

import os
import re
import sys
import math
import itertools
import pandas as pd
import pylems as lems
import pylems_autoQuant as aq
from numpy import arange as ar
from numpy import unique as npu
from re import search as regex_search
# Heat map libraries
import pylems_HeatMap as lv
from matplotlib import pyplot as plt


def var_config_to_lems_rules(var_conf_file, lems_rules_file, opers_to_remove=[]):
    var_config_df = pd.read_csv(var_conf_file, dtype=str)
    var_config_df.loc[var_config_df['variable_type'].str.lower().isin(['categorical', 'category', 'cat']) &
                      ~var_config_df['role'].str.lower().isin(['ignored', 'ignore']), 'role'] = 'categorical'
    var_config_df.loc[var_config_df['role'].str.lower().isin(['ignored', 'ignore']), 'role'] = 'ignore'
    var_config_df.loc[var_config_df['role'].str.lower().isin(['output']), 'role'] = 'categorical'
    var_config_df = var_config_df[var_config_df['role'].isin(['ignore', 'categorical'])]
    var_config_df = var_config_df[['role', 'name']]
    if len(opers_to_remove) > 0:
        var_config_df = pd.concat([var_config_df, pd.DataFrame({'role': ['ignore'] * len(opers_to_remove), 'name': opers_to_remove})])
    var_config_df.to_csv(lems_rules_file, index=False, header=False)


def Run_LEMS_and_Interaction_Charting(commonality_data_file, report_name, response_var, rules_file_name, output_dir, max_var_importance_display):

    probType = 'Likelihood' # Heat map input variable for colorbar value
    outcomeVal = '1'
    defMaxOrder = 5
    defVarThresh = 0.9
    N_pair = 2
    round_digits = 3
    TopThree = 3 # Top 3 interacting KPPs with a reference KPP
    minEntropy = 10e-10  # Minimum entropy to filter out data
    maxCardRatio = 100  # Maximum cardinality to filter out data
    naThresh = 0.20
    redundancy_thresh = 0.9

    single_rank_outfile = 'single_var_imp.csv'
    pairwise_rank_outfile = 'pairwise_var_imp.csv'
    interaction_rank_outfile = 'interaction_var_imp.csv'

    # ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    # Define project name, paths and file names
    project = 'KPP Interaction'

    file_name_quant = os.path.join(output_dir, 'AQ-' + os.path.split(commonality_data_file)[1])
    quantInfo = os.path.join(output_dir, 'qInfo-AQ-' + os.path.split(commonality_data_file)[1])
    # file_name_quant = 'AQ-' + commonality_data_file # Name of the quantized file

    # -------------------------------------------------------------------------------------------------------------------------------
    # Initialize the quantizer
    quantizer = aq.autoQuant(nProcs=1,
                             maxOrder=defMaxOrder,
                             varThresh=defVarThresh,
                             rulesFile=rules_file_name)

    sepStr = ',' if os.path.splitext(commonality_data_file)[1] == '.csv' else '\t'

    # Quantize the input datafile and output to the working directory
    outDF, qInfo = quantizer.quantizeFile(commonality_data_file, sep=sepStr)
    outDF.to_csv(file_name_quant, index=False)

    with open(quantInfo, 'w') as f:
        f.write(aq.qInfoToStr(qInfo))
        f.close()

    # ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    # LEMS filename and directory
    LEMS_filename = file_name_quant
    LEMS_directory = 'LEMS_analysis_' + response_var

    LEMS_directory = os.path.join(output_dir, report_name, LEMS_directory)
    if not os.path.exists(LEMS_directory):
        os.makedirs(LEMS_directory)

    chart_directory = os.path.join(output_dir, report_name, response_var)
    if not os.path.exists(chart_directory):
        os.makedirs(chart_directory)

    lemsDB = LEMS_directory
    dataFile = os.path.join(output_dir, commonality_data_file)

    # ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    # Ingest the quantized dataset
    lems.ingest(os.path.join(output_dir, LEMS_filename), LEMS_directory, quantize=False)

    # Establish connection to LEMS database
    ldb = lems.Connection(LEMS_directory)

    # Compute entropies dataframe of all the features
    entropies = pd.DataFrame([(x.name, x.type, x.distinct_stored, ldb.probability({x.name: None}, {response_var: '1'}), ldb.entropy([x.name]))
                              for x in ldb.columns if x.name != response_var], columns=['X', 'Type', 'Cardinality', 'P(NA)', 'H(X)'])

    colNames = ldb.column_list

    y_index = colNames.index(response_var)
    x = ldb.columns[y_index]

    # Compute entropies dataframe of the response variable
    entropies_Y = pd.DataFrame( [[x.name, x.type, x.distinct_stored, ldb.probability( {x.name:None},{} ), ldb.entropy([x.name])]],
                                index=[y_index], columns=['X', 'Type', 'Cardinality', 'P(NA)', 'H(X)'])

    # Concatenate the entropies dataframe for the features and the response variable
    entropies = pd.concat((entropies, entropies_Y))
    # ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    # Compute normalized entropy
    entropies['NH(X)'] = entropies.apply(lambda row: (row['H(X)'] / math.log(row.Cardinality, 2)) if row['H(X)'] > 0.0 else 0.0, axis=1)
    entropies.sort_values(by='NH(X)', ascending=False, inplace=True)
    entropies.head()

    # ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    # Filter the entropies dataframe for very low entropies
    lowEnt = entropies[entropies['H(X)'] <= minEntropy]
    entropies.drop(lowEnt.index, inplace=True)

    # Filter the entropies dataframe for N/A values
    naVars = entropies[entropies['P(NA)'] > naThresh]
    entropies.drop(naVars.index, inplace=True)

    # Filter the entropies dataframe for high cardinality entries
    highCard = entropies[(entropies['Type'] == 'Categorical') & \
                         (ldb.total_count / entropies['Cardinality'] < maxCardRatio)]
    entropies.drop(highCard.index, inplace=True)

    colNames = entropies['X'].tolist()
    # all_cols = ldb.column_list

    # ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    # Rank the KPPs using MAX_CMI algorithm
    gbFeatures = ldb.max_cmi([response_var], [x for x in colNames if x != response_var], 500)

    # ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    # Entropy of the response variable
    H_Y = entropies_Y['H(X)'].values[0]

    # ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    # Variable importance
    # Create list of ranked features and response variable
    variable_importance_list = gbFeatures.copy()
    variable_importance_list.append(response_var)

    # Compute mutual information using info_theory_pairs method
    var_imp = ldb.info_theory_pairs(variable_importance_list)

    # Filter the results dataframe to select the response variable, Y, as one of the pairs
    var_imp_filter_df = var_imp.loc[(var_imp['X'] == response_var) | (var_imp['Y'] == response_var)]
    var_imp_filter_df_sorted = var_imp_filter_df.sort_values(by='NMI(X;Y)', ascending=False)
    max_nmi = var_imp_filter_df_sorted['NMI(X;Y)'].max()
    var_imp_filter_df_sorted['var_importance'] = var_imp_filter_df_sorted['NMI(X;Y)'] / max_nmi

    output_vars = var_imp_filter_df_sorted[['X', 'var_importance']]
    final_result = pd.Series(output_vars['var_importance'].to_list(), index=output_vars['X'].to_list())
    final_result.index = final_result.index.str.split('-J').str[0]
    output_vars = output_vars[output_vars['var_importance'] >= max_var_importance_display]

    # Re-sort the ranked list of features
    gbFeatures = output_vars['X'] #.apply(lambda x: x[:-3])
    variable_importance = round(output_vars['var_importance'], round_digits)

    # ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    # Model score for ranking the individual KPPs
    LEMS_model_score = ldb.mutual_info([response_var], gbFeatures) / H_Y

    # ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    # Pairwise KPP analysis
    # Construct pairwise KPP from the ranked list
    pairwise_comb = list(itertools.combinations(gbFeatures, N_pair))

    # Compute mutual information b/w the response and pairwise KPP
    pairwise_MI = [ldb.mutual_info([response_var], list(X)) for X in pairwise_comb]

    # Store data in pandas dataframe
    pairwise_MI_df = pd.DataFrame()

    # Store results in dataframe
    ref_interact_kpp = pd.DataFrame()

    # Creating default dataframe
    pairwise_output = pd.DataFrame(columns=['KPP_1', 'KPP_2', 'variable importance'])

    if len(pairwise_comb) > 0:
        valid_interactions = True
    else:
        valid_interactions = False
        df_inter_charts = pd.DataFrame(columns=['param1', 'param2'])

    if valid_interactions:
        KPP_1, KPP_2 = map(list, zip(*pairwise_comb))

        pairwise_MI_df['Pairwise_KPP_1'] = KPP_1
        pairwise_MI_df['Pairwise_KPP_2'] = KPP_2
        pairwise_MI_df['Pairwise_MI'] = pairwise_MI

        # Sort in descending order of MI
        pairwise_MI_df.sort_values(by='Pairwise_MI', ascending=False, inplace=True)
        # Cleaning invalid interactions
        die_pos_pattern_group = r'(U\d{1,2}_U\d{1,2}#|#U\d{1,2}_U\d{1,2}#|#U\d{1,2}_U\d{1,2}$|~U\d{1,2}_U\d{1,2}$|~U\d{1,2}#|#U\d{1,2}#|#U\d{1,2}$|~U\d{1,2}$)'
        die_pos_pattern_die = r'(U\d{1,2}_U\d{1,2}|U\d{1,2})'
        pairwise_MI_df['kpp1_uxx'] = pairwise_MI_df['Pairwise_KPP_1'].str.extract(die_pos_pattern_group, flags=re.IGNORECASE)
        pairwise_MI_df['kpp1_uxx'] = pairwise_MI_df['kpp1_uxx'].str.extract(die_pos_pattern_die, flags=re.IGNORECASE)
        pairwise_MI_df['kpp1_uxx'] = pairwise_MI_df['kpp1_uxx'].str.lower()
        pairwise_MI_df['kpp2_uxx'] = pairwise_MI_df['Pairwise_KPP_2'].str.extract(die_pos_pattern_group, flags=re.IGNORECASE)
        pairwise_MI_df['kpp2_uxx'] = pairwise_MI_df['kpp2_uxx'].str.extract(die_pos_pattern_die, flags=re.IGNORECASE)
        pairwise_MI_df['kpp2_uxx'] = pairwise_MI_df['kpp2_uxx'].str.lower()
        if len(pairwise_MI_df) > 0:
            pairwise_MI_df.loc[pairwise_MI_df['kpp1_uxx'] == pairwise_MI_df['kpp2_uxx'], 'Keep'] = 'Y'
            pairwise_MI_df.loc[~pairwise_MI_df['kpp1_uxx'].isnull() & pairwise_MI_df['kpp1_uxx'].isnull(), 'Keep'] = 'Y'
            pairwise_MI_df.loc[pairwise_MI_df['kpp1_uxx'].isnull() & ~pairwise_MI_df['kpp1_uxx'].isnull(), 'Keep'] = 'Y'
            pairwise_MI_df.loc[pairwise_MI_df['kpp1_uxx'].isnull() & pairwise_MI_df['kpp1_uxx'].isnull(), 'Keep'] = 'Y'
            pairwise_MI_df = pairwise_MI_df[pairwise_MI_df['Keep'] == 'Y']
        del_cols = ['kpp1_uxx', 'kpp2_uxx', 'Keep']
        pairwise_MI_df.drop(columns=del_cols, inplace=True)

        if len(pairwise_MI_df) > 0:
            valid_interactions = True
        else:
            valid_interactions = False
            df_inter_charts = pd.DataFrame(columns=['param1', 'param2'])

    if valid_interactions:

        # Subset out the first N features e.g., 20 or 40
        gbFeatures_pairwise = pairwise_MI_df[['Pairwise_KPP_1', 'Pairwise_KPP_2']]
        # Get the variable importance of the pairwise ranking
        variable_importance_pairwise = round(pairwise_MI_df['Pairwise_MI'] / H_Y, round_digits)

        pairwise_output = pd.DataFrame(data={'KPP_1': gbFeatures_pairwise['Pairwise_KPP_1'],
                                             'KPP_2': gbFeatures_pairwise['Pairwise_KPP_2'],
                                             'variable importance': variable_importance_pairwise})

        # ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        # For each KPP ranked in gbFeatures, get the top 3 interacting KPPs
        # These interactions will be charted out as a 2D heat map
        rank_interaction = ar(1, TopThree+1)

        reference_kpp = list()
        interaction_kpp = list()
        interaction_rank = list()

        # Init heat map object
        heatMap = lv.lemsHeatMap(project,
                                 lemsStore=lemsDB,
                                 rawData=dataFile,
                                 qInfo=quantInfo)

        df_inter_charts = pd.DataFrame(columns=['param1', 'param2'])
        chart_counter = 1

        for ijk, ranked_kpp in enumerate(gbFeatures):
            # Look for the reference kpp in the pairwise dataframe
            pairwise_top_three = pairwise_MI_df.loc[(pairwise_MI_df['Pairwise_KPP_1'] == ranked_kpp) |
                                                    (pairwise_MI_df['Pairwise_KPP_2'] == ranked_kpp)]

            # Get the top three values
            # ap = pairwise_top_three.iloc[:TopThree, :]
            ap = pairwise_top_three.iloc[:, :]

            charts_executed = 0
            for index, row in ap.iterrows():

                hypX = row['Pairwise_KPP_1']
                hypY = row['Pairwise_KPP_2']
                mi = row['Pairwise_MI']

                hm_file_name = f'Int-hm-g{chart_counter}.png'
                param1 = hypX.split('-J')[0]
                param2 = hypY.split('-J')[0]

                df_check = df_inter_charts[(df_inter_charts['param1'] == param1) & (df_inter_charts['param2'] == param2)]

                if len(df_check) == 0:
                    try:
                        heatMap.setOutcome(response_var, outcomeVal)
                        fig = heatMap.plotHeatmap(hypX, hypY, probType)
                        fig.savefig(os.path.join(chart_directory, hm_file_name), dpi=300) # Use plt.savefig() as an alternative
                        plt.close(fig)
                        print(f'        Creating Interaction chart for: {param1} - {param2}')
                        if len(df_inter_charts) == 0:
                            df_inter_charts = pd.DataFrame([{'param1': param1, 'param2': param2, 'filename': hm_file_name, 'mi': mi}])
                        else:
                            df_inter_charts = pd.concat([df_inter_charts, pd.DataFrame([{'param1': param1, 'param2': param2, 'filename': hm_file_name, 'mi': mi}])])
                        # df_inter_charts = pd.concat([df_inter_charts, pd.DataFrame([{'param1': param2, 'param2': param1, 'filename': hm_file_name, 'mi': mi}])])
                        chart_counter += 1
                        charts_executed += 1
                        if charts_executed >= 3:
                            break
                    except (KeyError, lems.LemsException, ValueError) as err:
                        continue




        ref_interact_kpp['Reference_KPP'] = reference_kpp
        ref_interact_kpp['Interaction_KPP'] = interaction_kpp
        ref_interact_kpp['Rank'] = interaction_rank

        # ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        # Remove the trailing -JN
        # From gbFeatures
        gbFeatures = gbFeatures.apply(lambda x: x[:-3] if regex_search('-J', x) else x)

        # From ref_interaction dataframe
        ref_interact_kpp['Reference_KPP'] = ref_interact_kpp['Reference_KPP'].apply(lambda x: x[:-3] if regex_search('-J', x) else x)
        ref_interact_kpp['Interaction_KPP'] = ref_interact_kpp['Interaction_KPP'].apply(lambda x: x[:-3] if regex_search('-J', x) else x)

        # From pairwise dataframe
        pairwise_output['KPP_1'] = pairwise_output['KPP_1'].apply(lambda x: x[:-3] if regex_search('-J', x) else x)
        pairwise_output['KPP_2'] = pairwise_output['KPP_2'].apply(lambda x: x[:-3] if regex_search('-J', x) else x)

    # ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    # Write to output files
    # single_output = pd.DataFrame(data={'variable': gbFeatures, 'variable importance': variable_importance,
    #                                    'model score': [LEMS_model_score] * len(gbFeatures)})

    # ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    # Save all dataframes to CSV files
    ref_interact_kpp.to_csv(os.path.join(output_dir, interaction_rank_outfile), index=False)
    # single_output.to_csv(os.path.join(output_dir, single_rank_outfile), index=False)
    pairwise_output.to_csv(os.path.join(output_dir, pairwise_rank_outfile), index=False)
    # ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    df_inter_charts.to_csv(os.path.join(chart_directory, 'Inter_chart_list.csv'), index=False)

    return final_result, LEMS_model_score



if __name__ == "__main__":
    Run_LEMS_and_Interaction_Charting(r"C:\temp\SandV_Testing\KS_data.csv", 'SPFCommonality', 'Bin1530', r'C:\temp\SandV_Testing\Run\variable_config_lems_rules.txt', r"C:\temp\SandV_Testing\Run", 0.4)
