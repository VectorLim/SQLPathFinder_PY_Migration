from contextlib import closing
from pathlib import Path
from vg2c.runtime import DuckDbReader, JobRuntime, OracleClient, SqliteReader
OracleClient.configure()
from datasyncx import AriesReader
from datasyncx import MarsReader

def run():
    job = JobRuntime.from_script(__file__)
    macros = job.macros
    # Source block 0, line 3
    job.define_css('sqlpathfinder_style_1.css', 'styles/report_000.css')
    # Source block 1, line 69
    job.html('html/report_001.html', output='revision.htm', instance='18379', embed_css=True, css_file='sqlpathfinder_style_1.css')
    # Source block 2, line 100
    job.delete_html()
    # Source block 3, line 108
    job.write_file(path='macrotmp.csv', template='Sfolder,underDEV,useCSR,useMMS\nICMPCS_GE_Signal_CSR_CAM,N,Y,Y')
    # Source block 4, line 119
    job.write_file(path='getcsrsu.bat', template='@echo off\nset PriCSR="\\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\Patrol\\*.___"\nset SecCSR="\\\\KMATSHFS.intel.com\\KMATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\Patrol\\*.___"\nset BakCSR="\\\\SHUser-ProdAT.intel.com\\SHProdATUser$\\%username%\\Patrol\\*.___"\ncopy %PriCSR% . || copy %SecCSR% . || copy %BAKCSR% .\nren setsiteparam.___ setsiteparam.exe')
    # Source block 5, line 133
    job.run_program(argv=['getcsrsu.bat'])
    macro_row_9 = job.read_macro_row('macrotmp.csv')
    if macro_row_9 is not None:
        with macros.scope(macro_row_9):
            # Source block 7, line 156
            job.run_program(argv=['setsiteparam.exe', 'KM', macros['SFOLDER'], macros['UNDERDEV'], macros['USECSR'], macros['USEMMS']])
            # Source block 8, line 168
            job.write_file(path='TT', template='SST Rev3g')
    # Source block 10, line 187
    job.delete_files(paths=['macrotmp.csv', 'getcsrsu.bat', 'setsiteparam.exe', 'csrsu.txt'])
    macro_row_29 = job.read_macro_row('ctime.csv')
    if macro_row_29 is not None:
        with macros.scope(macro_row_29):
            # Source block 12, line 211
            macros['CONFIG'] = str(job.row_count('ICMPCS_config.csv'))
            if int(macros['CONFIG']) <= int('0'):
                pass
            else:
                # Source block 15, line 246
                job.sql('sql/query_015_configsets.sql', reader=SqliteReader(), output='configsets.csv', inputs=['ICMPCS_config.csv'], pivot_columns='parameter', pivot_values='value', pivot_header_ref='SQL,18379')
                # Source block 16, line 372
                macros['CONFIGSETS'] = str(job.row_count('configsets.csv'))
                if int(macros['CONFIGSETS']) != int('1'):
                    pass
                else:
                    macro_row_24 = job.read_macro_row('configsets.csv')
                    if macro_row_24 is not None:
                        with macros.scope(macro_row_24):
                            # Source block 20, line 419
                            job.copy_file(src=str(Path('\\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\' + macros['SFOLDER'] + '\\KM\\HIST') / 'HIST.txt'), dst='.')
                            # Source block 21, line 431
                            macros['HIST'] = str(job.row_count('HIST.txt'))
                            if int(macros['HIST']) <= int('0'):
                                # Source block 23, line 455
                                job.write_file(path='HIST.csv', template='LOT,OUT_DATE\nDUMMY,2000-01-01 00:00:00')
                                # Source block 24, line 466
                                job.write_file(path='HISTERROR.txt', template='ERROR\nERROR\nERROR')
                            else:
                                # Source block 26, line 487
                                job.rename_file(src='HIST.txt', dst='HIST.csv')
    macro_row_45 = job.read_macro_row('configsets.csv')
    if macro_row_45 is not None:
        with macros.scope(macro_row_45):
            # Source block 33, line 564
            job.sql('sql/query_033_yeuchuan_a0_18379.sql', reader=MarsReader(), output='yeuchuan_a0_18379.tab', node=macros['MARS'])
            # Source block 34, line 626
            job.sql('sql/query_034_yeuchuan_a1_18379.sql', reader=AriesReader(), output='yeuchuan_a1_18379.tab', node=macros['ARIES'])
            # Source block 35, line 663
            job.sql('sql/query_035_cam_lot_list.sql', reader=SqliteReader(), output='cam_lot_list.csv', inputs=['yeuchuan_a0_18379.tab', 'yeuchuan_a1_18379.tab'])
            # Source block 36, line 791
            macros['ROWSINFILE'] = str(job.row_count('cam_lot_list.csv'))
            if int(macros['ROWSINFILE']) > int('0'):
                # Source block 38, line 815
                job.delete_files(paths=['data.csv'])
                with closing(job.csv_chunks('cam_lot_list.csv', 'temp_cam_lot_list.csv', 10)) as chunks_41:
                    for chunk_41 in chunks_41:
                        with macros.scope():
                            # Source block 40, line 839
                            job.sql('sql/query_040_yeuchuan_a0_18379.sql', reader=AriesReader(), output='yeuchuan_a0_18379.tab', node=macros['ARIES'], pivot_columns='test_name', pivot_values='all_value', pivot_header_ref='a0,18379')
                            # Source block 41, line 935
                            job.sql('sql/query_041_yeuchuan_a2_agg_18379.sql', reader=AriesReader(), output='yeuchuan_a2_agg_18379.tab', node=macros['ARIES'])
                            # Source block 42, line 978
                            job.sql('sql/query_042_yeuchuan_a2_18379.sql', reader=DuckDbReader(), output='yeuchuan_a2_18379.tab', inputs=[('yeuchuan_a2_agg_18379.tab', 't0')], pivot_columns='test_name_1', pivot_values='all_value_1', pivot_header_ref='a2,18379')
                            # Source block 43, line 1029
                            job.sql('sql/query_043_yeuchuan_a1_18379.sql', reader=AriesReader(), output='yeuchuan_a1_18379.tab', node=macros['ARIES'])
                            # Source block 44, line 1064
                            job.sql('sql/query_044_xdata.sql', reader=SqliteReader(), output='xdata.csv', inputs=['yeuchuan_a0_18379.tab', 'yeuchuan_a2_18379.tab', 'yeuchuan_a1_18379.tab'])
                            # Source block 45, line 1120
                            job.smart_append('data.csv', 'xdata.csv')
                # Source block 47, line 1143
                job.sql('sql/query_047_flagged_data.sql', reader=SqliteReader(), output='flagged_data.csv', inputs=['data.csv'])

if __name__ == "__main__":
    run()
