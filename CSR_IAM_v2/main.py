from pathlib import Path

from vg2c.runtime import OracleClient, SqliteReader, csv_report, job_entrypoint

OracleClient.configure()
from datasyncx import AriesReader
from datasyncx import MarsReader


@job_entrypoint(__file__)
def run(job):
    macros = job.macros
    # Source block 0, line 3
    job.define_css("sqlpathfinder_style_1.css", "styles/report_000.css")
    # Source block 1, line 69
    job.html(
        "html/report_001.html",
        output="revision.htm",
        instance="17680",
        embed_css=True,
        css_file="sqlpathfinder_style_1.css",
    )
    # Source block 2, line 100
    job.delete_html()
    # Source block 3, line 108
    job.write_file(
        path="macrotmp.csv",
        template="Sfolder,underDEV,useCSR,useMMS\nICMPCS_CWFNCO_CSR_IAM,N,Y,Y",
    )
    # Source block 4, line 119
    job.write_file(
        path="getcsrsu.bat",
        template='@echo off\nset PriCSR="\\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\Patrol\\*.___"\nset SecCSR="\\\\KMATSHFS.intel.com\\KMATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\Patrol\\*.___"\nset BakCSR="\\\\SHUser-ProdAT.intel.com\\SHProdATUser$\\%username%\\Patrol\\*.___"\ncopy %PriCSR% . || copy %SecCSR% . || copy %BAKCSR% .\nren setsiteparam.___ setsiteparam.exe',
    )
    # Source block 5, line 133
    job.run_program(argv=["getcsrsu.bat"])
    macro_row_9 = job.read_macro_row("macrotmp.csv")
    if macro_row_9 is not None:
        with macros.scope(macro_row_9):
            # Source block 7, line 156
            job.run_program(
                argv=[
                    "setsiteparam.exe",
                    "KM",
                    macros["SFOLDER"],
                    macros["UNDERDEV"],
                    macros["USECSR"],
                    macros["USEMMS"],
                ]
            )
            # Source block 8, line 168
            job.write_file(path="TT", template="SST Rev3g")
    # Source block 10, line 187
    job.delete_files(
        paths=["macrotmp.csv", "getcsrsu.bat", "setsiteparam.exe", "csrsu.txt"]
    )
    macro_row_39 = job.read_macro_row("ctime.csv")
    if macro_row_39 is not None:
        with macros.scope(macro_row_39):
            # Source block 12, line 211
            macros["CONFIG"] = str(job.row_count("ICMPCS_config.csv"))
            if int(macros["CONFIG"]) <= int("0"):
                # Source block 14, line 235
                pass
                # job.send_mail(
                #     to="alex.chin.hooi.lee@intel.com",
                #     subject="Critical: ICMPCS config file not found - Path: \\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\"
                #     + macros["SFOLDER"]
                #     + "\\KM\\Config",
                #     body="",
                # )
            else:
                # Source block 16, line 258
                job.sql(
                    "sql/query_016_configsets.sql",
                    reader=SqliteReader(),
                    output="configsets.csv",
                    inputs=["ICMPCS_config.csv"],
                    pivot_columns="parameter",
                    pivot_values="value",
                    pivot_header_ref="SQL,17680",
                )
                # Source block 17, line 384
                macros["CONFIGSETS"] = str(job.row_count("configsets.csv"))
                if int(macros["CONFIGSETS"]) != int("1"):
                    # Source block 19, line 408
                    pass
                    # job.send_mail(
                    #     to="alex.chin.hooi.lee@intel.com",
                    #     subject="Alert: Pls check ICMPCS ("
                    #     + macros["SFOLDER"]
                    #     + ") config file as it contains not equal to 1 row",
                    #     body="",
                    #     attachments=["ICMPCS_config.csv", "configsets.csv"],
                    # )
                else:
                    macro_row_34 = job.read_macro_row("configsets.csv")
                    if macro_row_34 is not None:
                        with macros.scope(macro_row_34):
                            if macros["CSRV"] == "FAIL" and macros["UNDERDEV"] == "N":
                                # Source block 23, line 455
                                job.write_file(
                                    path="CSRVerror.htm",
                                    template="<!DOCTYPE html>\n<html>\n<body>\n<p>It is detected that you cannot access to CSR depository path for <strong>KM</strong> site.</p>\n\n<p>This could be due to you do NOT have the <strong>CSR Superuser</strong> access.</p>\n\n<p>Script Name: <strong>"
                                    + macros["SFOLDER"]
                                    + "</strong>\nPath: "
                                    + macros["CSRPATH"]
                                    + "</p>\n</body>\n</html>",
                                )
                                # Source block 24, line 475

                                # job.send_mail(
                                #     to="alex.chin.hooi.lee@intel.com",
                                #     subject="Critical: Cannot access to "
                                #     + macros["CSRPATH"],
                                #     body="CSRVerror.htm",
                                # )
                            if macros["MMSV"] == "FAIL" and macros["UNDERDEV"] == "N":
                                # Source block 27, line 509
                                job.write_file(
                                    path="MMSVerror.htm",
                                    template="<!DOCTYPE html>\n<html\n<body>\n<p>It is detected that you cannot access to MMS Signal Tracer depository path for <strong>KM</strong> site.</p>\n\n<p>This could be due to you do NOT have the <strong>MMS Signal Tracer Admin</strong> access.</p>\n\n<p>Script Name: <strong>"
                                    + macros["SFOLDER"]
                                    + "</strong><br/>\nPath: "
                                    + macros["MMSPATH"]
                                    + "</p>\n</body>\n</html>",
                                )
                                # Source block 28, line 529
                                # job.send_mail(
                                #     to="alex.chin.hooi.lee@intel.com",
                                #     subject="Critical: Cannot access to "
                                #     + macros["MMSPATH"],
                                #     body="MMSVerror.htm",
                                # )
                            # Source block 30, line 551
                            job.copy_file(
                                src=str(
                                    Path(
                                        "\\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\"
                                        + macros["SFOLDER"]
                                        + "\\KM\\HIST"
                                    )
                                    / "HIST.txt"
                                ),
                                dst=".",
                            )
                            # Source block 31, line 563
                            macros["HIST"] = str(job.row_count("HIST.txt"))
                            if int(macros["HIST"]) <= int("0"):
                                # Source block 33, line 587
                                job.write_file(
                                    path="HIST.csv",
                                    template="LOT,OUT_DATE\nDUMMY,2000-01-01 00:00:00",
                                )
                                # Source block 34, line 598
                                job.write_file(
                                    path="HISTERROR.txt",
                                    template="ERROR\nERROR\nERROR",
                                )
                            else:
                                # Source block 36, line 619
                                job.rename_file(src="HIST.txt", dst="HIST.csv")
    macro_row_78 = job.read_macro_row("configsets.csv")
    if macro_row_78 is not None:
        with macros.scope(macro_row_78):
            # Source block 43, line 696
            job.sql(
                "sql/query_043_yeuchuan_a1_17680.sql",
                reader=MarsReader(),
                output="yeuchuan_a1_17680.tab",
                node=macros["MARS"],
            )
            # Source block 44, line 762
            job.sql(
                "sql/query_044_yeuchuan_a0_17680.sql",
                reader=AriesReader(),
                output="yeuchuan_a0_17680.tab",
                node=macros["ARIES"],
                pivot_columns="parameter",
                pivot_values="numeric_value",
                pivot_header_ref="a0,17680",
            )
            # Source block 45, line 859
            job.sql(
                "sql/query_045_PARMI_IPM_RAW.sql",
                reader=SqliteReader(),
                output="PARMI_IPM_RAW.csv",
                inputs=["yeuchuan_a1_17680.tab", "yeuchuan_a0_17680.tab"],
            )
            # Source block 46, line 919
            macros["ROWSINFILE"] = str(job.row_count("PARMI_IPM_RAW.csv"))
            if int(macros["ROWSINFILE"]) > int("0"):
                # Source block 48, line 943
                job.sql(
                    "sql/query_048_IPM_Data.sql",
                    reader=SqliteReader(),
                    output="IPM_Data.csv",
                    inputs=["PARMI_IPM_RAW.csv"],
                )
                # Source block 49, line 1159
                job.sql(
                    "sql/query_049_DATA.sql",
                    reader=SqliteReader(),
                    output="DATA.csv",
                    inputs=["IPM_Data.csv"],
                )
            # Source block 51, line 1199
            macros["SIGNAL"] = str(job.row_count("data.csv"))
            if int(macros["SIGNAL"]) > int("0"):
                if macros["DCSR"] == "Y":
                    # Source block 54, line 1235
                    job.sql(
                        "sql/query_054_RESULT__username__CWFNCO_TS.sql",
                        reader=SqliteReader(),
                        output="RESULT_" + macros["%USERNAME%"] + "_CWFNCO{TS}.csv",
                        inputs=[("data.csv", "T0")],
                    )
                    # Source block 55, line 1267
                    job.copy_file(
                        src=str(Path(".") / "RESULT_*.csv"), dst=macros["CSRPATH"]
                    )
                # Source block 57, line 1293
                job.reports["MYREPORT3"] = csv_report(
                    "IPM_Data.csv", output_file="SQLPathFinder.htm"
                )
                # Source block 58, line 1320
                job.html(
                    "html/report_058.html",
                    output="EMAIL:" + macros["DEMAIL"],
                    instance="17680",
                    embed_css=True,
                    values={
                        "VALUE_1": macros["DCSR"],
                        "VALUE_2": macros["DMWF"],
                        "VALUE_3": macros["DMST"],
                    },
                    css_file="sqlpathfinder_style_1.css",
                )
                # Source block 59, line 1366
                job.delete_html()
                # Source block 60, line 1374
                macros["HISTERR"] = str(job.row_count("HISTERROR.txt"))
                if int(macros["HISTERR"]) <= int("0"):
                    # Source block 62, line 1398
                    job.smart_append("HIST.csv", "data.csv")
                    # Source block 63, line 1410
                    job.rename_file(src="HIST.csv", dst="HIST.txt")
                    # Source block 64, line 1422
                    job.copy_file(
                        src=str(Path(".") / "HIST.txt"),
                        dst=macros["IREPOP"] + "\\" + macros["SFOLDER"] + "\\KM\\HIST",
                    )
            # Source block 67, line 1456
            macros["SIGNAL"] = str(job.row_count("data.csv"))
            if int(macros["SIGNAL"]) > int("0"):
                if macros["DMWF"] == "Y":
                    # Source block 70, line 1492
                    job.write_file(
                        path="spfheaderfile.csv",
                        template="DATE,EMAIL,MODULE,SIGNALTYPE,COMPONENT_ID,HOLD_LOT,AUTO_CONTAIN,SHOULD_EMAIL,MMS,ALARMTYPE,SUB_ENTITY_1,SUB_ENTITY_2,SUB_ENTITY_3,FACILITY,PRODGROUP3,ENTITY,DEFECT_SIDE,DEFECT_MODE,LOT_OWNR,IMPACT_LOT_VI,IMPACT_VIDS\n",
                    )
                    # Source block 71, line 1502
                    job.sql(
                        "sql/query_071_SPFMMSTMP.sql",
                        reader=SqliteReader(),
                        output="SPFMMSTMP.csv",
                        inputs=[("IPM_Data.csv", "T0")],
                    )
                    # Source block 72, line 1537
                    job.smart_append("spfheaderfile.csv", "SPFMMSTMP.csv")
                    # Source block 73, line 1545
                    job.rename_file(
                        src="spfheaderfile.csv",
                        dst="<TS>_" + macros["%USERNAME%"] + "_output.pickle.csv",
                    )
                    # Source block 74, line 1555
                    job.delete_files(paths=["SPFMMSTMP.csv"])
                    # Source block 75, line 1564
                    job.copy_file(
                        src=str(Path(".") / "*_output.pickle.csv"),
                        dst=macros["MIPPATH"],
                    )
            # Source block 78, line 1596
            job.write_file(
                path="update.bat",
                template="@echo off\nset currd=%date:~10,4%-%date:~4,2%-%date:~7,2%\nset currms=%time:~2,6%\nset /a currh=%time:~0,2%\nif %currh% LSS 10 set currh=0%currh%\nset currt=%currh%%currms%\nset PriConfig="
                + macros["IREPOP"]
                + "\\"
                + macros["SFOLDER"]
                + "\n\n\nREM ************** edit here only if needed **************\n\nset record="
                + macros["DCSR"]
                + ","
                + macros["DMST"]
                + ","
                + macros["DMWF"]
                + "\n\n\n\n\nREM *************** do not edit below here ***************\n\ncall :retry\ngoto:eof\n\n:retry\nset /a tries=300\n\n:loop\nif %tries% LEQ 0 goto return\necho "
                + macros["STARTTS"]
                + ",%currd% %currt%,"
                + macros["UTC"]
                + ",KM,"
                + macros["%USERNAME%"]
                + ","
                + macros["UNDERDEV"]
                + ",%record%>>%PriConfig%\\VF_CE.txt && goto return || set /a tries-=1 && ping -n 1 127.0.0.1>nul 2>&1 && goto loop\n\n:return\n@exit /B",
            )
            # Source block 79, line 1634
            job.run_program(argv=["update.bat"])
            # Source block 80, line 1645
            job.delete_files(paths=["update.bat"])


if __name__ == "__main__":
    run()
