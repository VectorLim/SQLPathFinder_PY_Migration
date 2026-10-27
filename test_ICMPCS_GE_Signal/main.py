from contextlib import closing
from pathlib import Path
from vg2c.runtime import DuckDbReader, JobRuntime, OracleClient, csv_report

TEST_MODE_EXCLUDED = {'csr', 'email', 'mms'}

def run():
    job = JobRuntime.from_script(__file__, test_mode_excluded=TEST_MODE_EXCLUDED)
    macros = job.macros
    # Source block 0, line 1
    job.define_css('sqlpathfinder_style_1.css', 'styles/report_000.css')
    # Source block 1, line 68
    job.html('html/report_001.html', output='revision.htm', instance='3182', embed_css=True, css_file='sqlpathfinder_style_1.css')
    # Source block 2, line 99
    job.delete_html()
    # Source block 3, line 107
    job.load_file(path='macrotmp.csv', asset='files/macrotmp.csv')
    # Source block 4, line 118
    job.load_file(path='getcsrsu.bat', asset='files/getcsrsu.bat')
    # Source block 5, line 132
    job.run_program(argv=['getcsrsu.bat'])
    macro_row_9 = job.read_macro_row('macrotmp.csv')
    if macro_row_9 is not None:
        with macros.scope(macro_row_9):
            # Source block 7, line 155
            job.run_program(argv=['setsiteparam.exe', 'VN', macros['SFOLDER'], macros['UNDERDEV'], macros['USECSR'], macros['USEMMS']])
            # Source block 8, line 167
            job.load_file(path='TT', asset='files/TT')
    # Source block 10, line 186
    job.delete_files(paths=['macrotmp.csv', 'getcsrsu.bat', 'setsiteparam.exe', 'csrsu.txt'])
    macro_row_29 = job.read_macro_row('ctime.csv')
    if macro_row_29 is not None:
        with macros.scope(macro_row_29):
            # Source block 12, line 210
            if job.row_count('ICMPCS_config.csv') <= 0:
                pass
            else:
                # Source block 15, line 245
                job.sql('sql/query_015_configsets.sql', output='configsets.csv', inputs=['ICMPCS_config.csv'], pivot_columns='parameter', pivot_values='value', pivot_header_ref='SQL,3182')
                # Source block 16, line 371
                if job.row_count('configsets.csv') != 1:
                    pass
                else:
                    macro_row_24 = job.read_macro_row('configsets.csv')
                    if macro_row_24 is not None:
                        with macros.scope(macro_row_24):
                            # Source block 20, line 418
                            job.copy_file(src=str(Path('\\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\' + macros['SFOLDER'] + '\\VN\\HIST') / 'HIST.txt'), dst='.')
                            # Source block 21, line 430
                            if job.row_count('HIST.txt') <= 0:
                                # Source block 23, line 454
                                job.load_file(path='HIST.csv', asset='files/HIST.csv')
                                # Source block 24, line 465
                                job.load_file(path='HISTERROR.txt', asset='files/HISTERROR.txt')
                            else:
                                # Source block 26, line 486
                                job.rename_file(src='HIST.txt', dst='HIST.csv')
    macro_row_93 = job.read_macro_row('configsets.csv')
    if macro_row_93 is not None:
        with macros.scope(macro_row_93):
            # Source block 33, line 563
            job.sql('sql/query_033_yeuchuan_a0_3182.sql', output='yeuchuan_a0_3182.tab', node=macros['MARS'])
            # Source block 34, line 625
            job.sql('sql/query_034_yeuchuan_a1_3182.sql', output='yeuchuan_a1_3182.tab', node=macros['ARIES'])
            # Source block 35, line 662
            job.sql('sql/query_035_cam_lot_list.sql', output='cam_lot_list.csv', inputs=['yeuchuan_a0_3182.tab', 'yeuchuan_a1_3182.tab'])
            # Source block 36, line 790
            if job.row_count('cam_lot_list.csv') > 0:
                # Source block 38, line 814
                job.delete_files(paths=['data.csv'])
                with closing(job.csv_chunks('cam_lot_list.csv', 'temp_cam_lot_list.csv', 1)) as chunks_41:
                    for chunk_41 in chunks_41:
                        with macros.scope():
                            # Source block 40, line 838
                            job.sql('sql/query_040_yeuchuan_a0_3182.sql', output='yeuchuan_a0_3182.tab', node=macros['ARIES'], pivot_columns='test_name', pivot_values='all_value', pivot_header_ref='a0,3182')
                            # Source block 41, line 936
                            job.sql('sql/query_041_yeuchuan_a2_agg_3182.sql', output='yeuchuan_a2_agg_3182.tab', node=macros['ARIES'])
                            # Source block 42, line 977
                            job.sql('sql/query_042_yeuchuan_a2_3182.sql', reader=DuckDbReader(), output='yeuchuan_a2_3182.tab', inputs=[('yeuchuan_a2_agg_3182.tab', 't0')], pivot_columns='test_name_1', pivot_values='all_value_1', pivot_header_ref='a2,3182')
                            # Source block 43, line 1025
                            job.sql('sql/query_043_yeuchuan_a1_3182.sql', output='yeuchuan_a1_3182.tab', node=macros['ARIES'])
                            # Source block 44, line 1060
                            job.sql('sql/query_044_xdata.sql', output='xdata.csv', inputs=['yeuchuan_a0_3182.tab', 'yeuchuan_a2_3182.tab', 'yeuchuan_a1_3182.tab'])
                            # Source block 45, line 1115
                            job.smart_append('data.csv', 'xdata.csv')
                # Source block 47, line 1138
                job.sql('sql/query_047_flagged_data.sql', output='flagged_data.csv', inputs=['data.csv'])
                # Source block 48, line 1678
                job.copy_file(src=str(Path('\\\\AZatshfs.intel.com\\AZatanalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\ICMPCS_GE_Signal_CSR_CAM') / 'prod_hold.csv'), dst='.\\')
                # Source block 49, line 1690
                job.sql('sql/query_049_final_data.sql', output='final_data.csv', inputs=['prod_hold.csv', 'flagged_data.csv'])
                if macros['VN'] == 'KM':
                    # Source block 51, line 1768
                    job.copy_file(src=str(Path('\\\\AZatshfs.intel.com\\AZatanalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\ICMPCS_GE_Signal_CSR_CAM') / 'prod_kill.csv'), dst='.\\')
                    # Source block 52, line 1780
                    job.copy_file(src=str(Path('\\\\datagrovekm.kssm.intel.com\\AED_HYBRID_SCREENING\\ScreenData\\BDW_CAM_OUTLIER_11829695\\DiePlacementOffset') / 'DispoByScreenDataFile.csv'), dst='.')
                    # Source block 53, line 1792
                    job.sql('sql/query_053_yeuchuan_SQL_3182.sql', output='yeuchuan_SQL_3182.tab', inputs=['prod_kill.csv', 'flagged_data.csv'])
                    # Source block 54, line 1856
                    job.sql('sql/query_054_unit_killed.sql', output='unit_killed.csv', inputs=['yeuchuan_SQL_3182.tab'])
                    # Source block 55, line 1939
                    if job.row_count('unit_killed.csv') > 0:
                        # Source block 57, line 1963
                        job.run_python('python/step_057_concat.py')
                        # Source block 58, line 1988
                        job.run_program(argv=['@EXEDIR@\\Run_Python_Script.va', 'concat.py', '', 'N', 'atd_atm.hadoop', 'Python-v3'])
                    # Source block 60, line 2009
                    job.sql('sql/query_060_yeuchuan_SQL_3182.sql', output='yeuchuan_SQL_3182.tab', inputs=['dispo_append.csv'])
                    # Source block 61, line 2034
                    job.sql('sql/query_061_yeuchuan_a1_3182.sql', output='yeuchuan_a1_3182.tab', node=macros['ARIES'])
                    # Source block 62, line 2076
                    job.sql('sql/query_062_DispoByScreenDataFile.sql', output='DispoByScreenDataFile.csv', inputs=['yeuchuan_SQL_3182.tab', 'yeuchuan_a1_3182.tab'])
                    # Source block 63, line 2112
                    job.copy_file(src=str(Path('.') / 'DispoByScreenDataFile.csv'), dst='\\\\datagrovekm.kssm.intel.com\\AED_HYBRID_SCREENING\\ScreenData\\BDW_CAM_OUTLIER_11829695\\DiePlacementOffset')
                # Source block 65, line 2135
                if job.row_count('final_data.csv') > 0:
                    if job.enabled('csr') and macros['DCSR'] == 'Y':
                        # Source block 69, line 2202
                        aed = job.aed(macros['FAC'])
                        comment = job.csr_comment(macros['DHNOTE'], ccb=macros['DCCB'])
                        for lot in job.csr_lots('final_data.csv', 'lot'):
                            # Legacy CSR fields: EMAIL=<<<dEmail>>>, FUTURE_OPERATION=<<<dHoldOperation>>>, PHQ=<<<dPHQ>>>
                            # Supply the confirmed AED hold operation and hold code.
                            aed.lot_hold(lotid=lot, operation='', holdcode='', comment=comment)
                    # Source block 71, line 2225
                    pass  # Skipped unsupported query block.
                    # Source block 72, line 2382
                    job.reports['MYREPORT3'] = csv_report('final_data.csv', output_file='SQLPathFinder.htm')
                    # Source block 73, line 2409
                    csr_report = job.html('html/report_073.html', output='EMAIL:' + macros['DEMAIL'], instance='3182', embed_css=True, values={'VALUE_1': macros['DCSR'], 'VALUE_2': macros['DMWF'], 'VALUE_3': macros['DMST'], 'AED_OUTCOME': job.csr_status}, css_file='sqlpathfinder_style_1.css')
                    job.send_mail(to=macros['DEMAIL'], subject='CAM GE TRIGGERING' + ' - ' + job.csr_status, body=str(csr_report))
                    # Source block 74, line 2460
                    job.delete_html()
                    # Source block 75, line 2468
                    if job.enabled('csr') and (job.row_count('HISTERROR.txt') <= 0):
                        # Source block 77, line 2492
                        job.smart_append('HIST.csv', 'final_data.csv')
                        # Source block 78, line 2504
                        job.rename_file(src='HIST.csv', dst='HIST.txt')
                        # Source block 79, line 2516
                        job.copy_file(src=str(Path('.') / 'HIST.txt'), dst=macros['IREPOP'] + '\\' + macros['SFOLDER'] + '\\VN\\HIST')
                # Source block 82, line 2550
                if job.row_count('final_data.csv') > 0:
                    if job.enabled('mms') and macros['DMWF'] == 'Y':
                        # Source block 85, line 2586
                        job.load_file(path='spfheaderfile.csv', asset='files/spfheaderfile.csv')
                        # Source block 86, line 2596
                        job.sql('sql/query_086_SPFMMSTMP.sql', output='SPFMMSTMP.csv', inputs=[('final_data.csv', 'T0')])
                        # Source block 87, line 2631
                        job.smart_append('spfheaderfile.csv', 'SPFMMSTMP.csv')
                        # Source block 88, line 2639
                        job.rename_file(src='spfheaderfile.csv', dst='<TS>_' + macros['%USERNAME%'] + '_output.pickle.csv')
                        # Source block 89, line 2649
                        job.delete_files(paths=['SPFMMSTMP.csv'])
                        # Source block 90, line 2658
                        job.submit_mms(src=str(Path('.') / '*_output.pickle.csv'), dst=macros['MIPPATH'])
            # Source block 94, line 2700
            job.update_file(path='update.bat', asset='files/update.bat')
            # Source block 95, line 2739
            job.run_program(argv=['update.bat'])
            # Source block 96, line 2750
            job.delete_files(paths=['update.bat'])

if __name__ == "__main__":
    run()
