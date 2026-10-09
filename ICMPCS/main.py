from pathlib import Path
from vg2c.runtime import (
    OracleClient,
    SqliteReader,
    copy_file,
    csv_report,
    delete_files,
    execute_sql,
    read_macro_row,
    rename_file,
    render_html,
    row_count,
    run_program,
    send_mail,
    smart_append,
    snapshot_values,
    substitute,
    write_file,
)

OracleClient.configure()
from datasyncx import AriesReader
from datasyncx import MarsReader

BASE_DIR = Path(__file__).resolve().parent
WORK_DIR = BASE_DIR / "output"


def run(workdir=WORK_DIR):
    workdir = Path(workdir).resolve()
    job_values = snapshot_values(workdir)
    macro_values = {}
    reports = {}
    styles = {}
    css_file = None
    # Source block 0, line 2
    css_file = "sqlpathfinder_style_1.css"
    styles["Column-Headers"] = [
        "background-color:#dbd9c0",
        "color:#444",
        "font-family:Arial",
        "font-size:12",
        "font-style:normal",
        "font-weight:bold",
        "text-align:left",
        "text-decoration:normal",
        "vertical-align:middle",
    ]
    styles["Column-Data"] = [
        "background-color:white",
        "color:#444",
        "font-family:Arial",
        "font-size:12",
        "font-style:normal",
        "text-align:left",
        "vertical-align:middle",
        "",
    ]
    styles["Column-Alt-Row"] = [
        "background-color:#f7f5dc",
        "color:#333",
        "font-family:Arial",
        "font-size:12",
        "font-style:normal",
        "text-align:left",
        "vertical-align:middle",
        "",
    ]
    styles["At-Top-of-Report"] = [
        "background-color:white",
        "color:#444",
        "font-family:Arial",
        "font-size:15",
        "font-style:normal",
        "font-weight:bold",
        "text-align:center",
        "vertical-align:middle",
    ]
    styles["At-Top-of-Col1"] = [
        "background-color:white",
        "color:#444",
        "font-family:Arial",
        "font-size:12",
        "font-style:normal",
        "font-weight:bold",
        "text-align:left",
        "vertical-align:middle",
    ]
    styles["At-Top-of-Col2"] = [
        "background-color:white",
        "color:#444",
        "font-family:Arial",
        "font-size:12",
        "font-style:normal",
        "font-weight:bold",
        "text-align:left",
        "vertical-align:middle",
    ]
    styles["At-Top-of-Col3"] = [
        "background-color:white",
        "color:#444",
        "font-family:Arial",
        "font-size:12",
        "font-style:normal",
        "font-weight:bold",
        "text-align:left",
        "vertical-align:middle",
    ]
    styles["JQX-All-IChart-Text"] = [
        "background-color:white",
        "color:black",
        "font-family:Verdana",
        "font-size:11",
        "font-style:normal",
        "font-weight:normal",
        "text-align:left",
        "vertical-align:middle",
    ]
    styles["COLUMN-BORDER"] = [
        "border-color:#cc9",
        "border-collapse:collapse",
        "border-style:solid",
        "border-width:1px",
        "border-spacing:4px",
        "",
        "",
        "",
    ]
    # Source block 1, line 68
    render_html(
        BASE_DIR / "html/report_001.html",
        output="revision.htm",
        workdir=workdir,
        reports=reports,
        values=job_values,
        macros=macro_values,
        styles=styles,
        instance="22697",
        css_file="sqlpathfinder_style_1.css",
        embed_css=True,
    )
    # Source block 2, line 99
    reports.clear()
    styles.clear()
    css_file = None
    # Source block 3, line 107
    write_file(
        path="macrotmp.csv",
        template="\nSfolder,underDEV,useCSR,useMMS\nICMPCS_CWFNCO_CSR_IAM,N,Y,Y",
        workdir=workdir,
        values=job_values,
        macros=macro_values,
    )
    # Source block 4, line 118
    write_file(
        path="getcsrsu.bat",
        template='\n@echo off\nset PriCSR="\\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\Patrol\\*.___"\nset SecCSR="\\\\KMATSHFS.intel.com\\KMATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\Patrol\\*.___"\nset BakCSR="\\\\SHUser-ProdAT.intel.com\\SHProdATUser$\\%username%\\Patrol\\*.___"\ncopy %PriCSR% . || copy %SecCSR% . || copy %BAKCSR% .\nren setsiteparam.___ setsiteparam.exe',
        workdir=workdir,
        values=job_values,
        macros=macro_values,
    )
    # Source block 5, line 132
    run_program(argv=["getcsrsu.bat"], workdir=workdir)
    macro_row_9 = read_macro_row(
        substitute("macrotmp.csv", values=job_values, macros=macro_values),
        workdir=workdir,
    )
    if macro_row_9 is not None:
        macro_values_9 = {
            **macro_values,
            **{key.upper(): value for key, value in macro_row_9.items()},
        }
        # Source block 7, line 155
        run_program(
            argv=[
                "setsiteparam.exe",
                "KM",
                substitute("<<<SFOLDER>>>", values=job_values, macros=macro_values_9),
                substitute("<<<UNDERDEV>>>", values=job_values, macros=macro_values_9),
                substitute("<<<USECSR>>>", values=job_values, macros=macro_values_9),
                substitute("<<<USEMMS>>>", values=job_values, macros=macro_values_9),
            ],
            workdir=workdir,
        )
        # Source block 8, line 167
        write_file(
            path="TT",
            template="\nSST Rev3g",
            workdir=workdir,
            values=job_values,
            macros=macro_values_9,
        )
    # Source block 10, line 186
    delete_files(
        paths=["macrotmp.csv", "getcsrsu.bat", "setsiteparam.exe", "csrsu.txt"],
        workdir=workdir,
    )
    macro_row_39 = read_macro_row(
        substitute("ctime.csv", values=job_values, macros=macro_values), workdir=workdir
    )
    if macro_row_39 is not None:
        macro_values_39 = {
            **macro_values,
            **{key.upper(): value for key, value in macro_row_39.items()},
        }
        # Source block 12, line 210
        macro_values_39["CONFIG"] = str(row_count("ICMPCS_config.csv", workdir=workdir))
        if int(
            substitute("<<<CONFIG>>>", values=job_values, macros=macro_values_39)
        ) <= int("0"):
            # Source block 14, line 234
            pass
            # send_mail(to='alex.chin.hooi.lee@intel.com', subject='Critical: ICMPCS config file not found - Path: \\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\' + substitute('<<<SFOLDER>>>', values=job_values, macros=macro_values_39) + '\\KM\\Config', body='', workdir=workdir)
        else:
            # Source block 16, line 257
            execute_sql(
                BASE_DIR / "sql/query_016_configsets.sql",
                reader=SqliteReader(),
                output="configsets.csv",
                workdir=workdir,
                values=job_values,
                macros=macro_values_39,
                inputs=["ICMPCS_config.csv"],
                crosstab={
                    "row_keys": [
                        "icmpcs",
                        "STARTTS",
                        "UTC",
                        "TZONE",
                        "TZS",
                        "SFOLDER",
                        "FAC",
                        "MARS",
                        "MARSN",
                        "RIMS",
                        "EIMS",
                        "ARIES",
                        "OASYS",
                        "MONGO",
                        "MMS",
                        "MMSI",
                        "TOOLLOG",
                        "VFMARS",
                        "VFARIES",
                        "VFMONGO",
                        "CSRPATH",
                        "MMSPATH",
                        "MIPPATH",
                        "LURL",
                        "IREPOP",
                        "UNDERDEV",
                        "CSRV",
                        "MMSV",
                    ],
                    "header_key": "parameter",
                    "value_key": "value",
                },
            )
            # Source block 17, line 383
            macro_values_39["CONFIGSETS"] = str(
                row_count("configsets.csv", workdir=workdir)
            )
            if int(
                substitute(
                    "<<<CONFIGSETS>>>", values=job_values, macros=macro_values_39
                )
            ) != int("1"):
                # Source block 19, line 407
                pass
                # send_mail(to='alex.chin.hooi.lee@intel.com', subject='Alert: Pls check ICMPCS (' + substitute('<<<SFOLDER>>>', values=job_values, macros=macro_values_39) + ') config file as it contains not equal to 1 row', body='', attachments=['ICMPCS_config.csv', 'configsets.csv'], workdir=workdir)
            else:
                macro_row_34 = read_macro_row(
                    substitute(
                        "configsets.csv", values=job_values, macros=macro_values_39
                    ),
                    workdir=workdir,
                )
                if macro_row_34 is not None:
                    macro_values_34 = {
                        **macro_values_39,
                        **{key.upper(): value for key, value in macro_row_34.items()},
                    }
                    if (
                        substitute(
                            "<<<CSRV>>>", values=job_values, macros=macro_values_34
                        )
                        == "FAIL"
                        and substitute(
                            "<<<UNDERDEV>>>", values=job_values, macros=macro_values_34
                        )
                        == "N"
                    ):
                        # Source block 23, line 454
                        write_file(
                            path="CSRVerror.htm",
                            template="\n<!DOCTYPE html>\n<html>\n<body>\n<p>It is detected that you cannot access to CSR depository path for <strong>KM</strong> site.</p>\n\n<p>This could be due to you do NOT have the <strong>CSR Superuser</strong> access.</p>\n\n<p>Script Name: <strong><<<SFOLDER>>></strong>\nPath: <<<CSRPATH>>></p>\n</body>\n</html>",
                            workdir=workdir,
                            values=job_values,
                            macros=macro_values_34,
                        )
                        # Source block 24, line 474
                        pass
                        # send_mail(to='alex.chin.hooi.lee@intel.com', subject='Critical: Cannot access to ' + substitute('<<<CSRPATH>>>', values=job_values, macros=macro_values_34), body='CSRVerror.htm', workdir=workdir)
                    if (
                        substitute(
                            "<<<MMSV>>>", values=job_values, macros=macro_values_34
                        )
                        == "FAIL"
                        and substitute(
                            "<<<UNDERDEV>>>", values=job_values, macros=macro_values_34
                        )
                        == "N"
                    ):
                        # Source block 27, line 508
                        write_file(
                            path="MMSVerror.htm",
                            template="\n<!DOCTYPE html>\n<html\n<body>\n<p>It is detected that you cannot access to MMS Signal Tracer depository path for <strong>KM</strong> site.</p>\n\n<p>This could be due to you do NOT have the <strong>MMS Signal Tracer Admin</strong> access.</p>\n\n<p>Script Name: <strong><<<SFOLDER>>></strong><br/>\nPath: <<<MMSPATH>>></p>\n</body>\n</html>",
                            workdir=workdir,
                            values=job_values,
                            macros=macro_values_34,
                        )
                        # Source block 28, line 528
                        pass
                        # send_mail(to='alex.chin.hooi.lee@intel.com', subject='Critical: Cannot access to ' + substitute('<<<MMSPATH>>>', values=job_values, macros=macro_values_34), body='MMSVerror.htm', workdir=workdir)
                    # Source block 30, line 550
                    copy_file(
                        src=str(
                            Path(
                                "\\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\"
                                + substitute(
                                    "<<<SFOLDER>>>",
                                    values=job_values,
                                    macros=macro_values_34,
                                )
                                + "\\KM\\HIST"
                            )
                            / "HIST.txt"
                        ),
                        dst=".",
                        workdir=workdir,
                    )
                    # Source block 31, line 562
                    macro_values_34["HIST"] = str(
                        row_count("HIST.txt", workdir=workdir)
                    )
                    if int(
                        substitute(
                            "<<<HIST>>>", values=job_values, macros=macro_values_34
                        )
                    ) <= int("0"):
                        # Source block 33, line 586
                        write_file(
                            path="HIST.csv",
                            template="\nLOT,OUT_DATE\nDUMMY,2000-01-01 00:00:00",
                            workdir=workdir,
                            values=job_values,
                            macros=macro_values_34,
                        )
                        # Source block 34, line 597
                        write_file(
                            path="HISTERROR.txt",
                            template="\nERROR\nERROR\nERROR",
                            workdir=workdir,
                            values=job_values,
                            macros=macro_values_34,
                        )
                    else:
                        # Source block 36, line 618
                        rename_file(src="HIST.txt", dst="HIST.csv", workdir=workdir)
    macro_row_75 = read_macro_row(
        substitute("configsets.csv", values=job_values, macros=macro_values),
        workdir=workdir,
    )
    if macro_row_75 is not None:
        macro_values_75 = {
            **macro_values,
            **{key.upper(): value for key, value in macro_row_75.items()},
        }
        # Source block 43, line 695
        execute_sql(
            BASE_DIR / "sql/query_043_yeuchuan_a1_22697.sql",
            reader=MarsReader(),
            output="yeuchuan_a1_22697.tab",
            workdir=workdir,
            values=job_values,
            macros=macro_values_75,
            node="<<<MARS>>>",
            header=[
                "lot_1",
                "operation_1",
                "out_date",
                "oldqty1",
                "newqty1",
                "Interposer_SLI",
                "Patch_SLI",
                "prodgroup3_1",
                "entity",
                "transaction",
            ],
        )
        # Source block 44, line 761
        execute_sql(
            BASE_DIR / "sql/query_044_yeuchuan_a0_22697.sql",
            reader=AriesReader(),
            output="yeuchuan_a0_22697.tab",
            workdir=workdir,
            values=job_values,
            macros=macro_values_75,
            node="<<<ARIES>>>",
            crosstab={
                "row_keys": [
                    "facility",
                    "operation",
                    "module_name",
                    "tool_entity",
                    "primary_entity",
                    "processing_start_date",
                    "processing_end_date",
                    "lot",
                    "product",
                    "prodgroup3",
                    "product_desc",
                    "owner",
                    "visual_id",
                    "ws_loss_code",
                    "media_in_x",
                    "media_in_y",
                ],
                "header_key": "parameter",
                "value_key": "numeric_value",
            },
        )
        # Source block 45, line 858
        execute_sql(
            BASE_DIR / "sql/query_045_PARMI_IPM_RAW.sql",
            reader=SqliteReader(),
            output="PARMI_IPM_RAW.csv",
            workdir=workdir,
            values=job_values,
            macros=macro_values_75,
            inputs=["yeuchuan_a1_22697.tab", "yeuchuan_a0_22697.tab"],
        )
        # Source block 46, line 918
        execute_sql(
            BASE_DIR / "sql/query_046_IPM_Data.sql",
            reader=SqliteReader(),
            output="IPM_Data.csv",
            workdir=workdir,
            values=job_values,
            macros=macro_values_75,
            inputs=["PARMI_IPM_RAW.csv"],
            header=[
                "lot_1",
                "newqty1",
                "facility",
                "operation",
                "tool_entity",
                "primary_entity",
                "processing_end_date",
                "lot",
                "prodgroup3",
                "product",
                "visual_id",
                "ws_loss_code",
                "media_in_x",
                "media_in_y",
                "height",
                "patch_lift_roi1",
                "patch_lift_roi2",
                "patch_lift_roi3",
                "patch_lift_roi4",
                "patch_lift_roi5",
                "patch_lift_roi6",
                "patch_lift_roi7",
                "patch_lift_roi8",
                "patch_lift_roi_max",
                "patch_sli",
                "interposer_sli",
                "NCO_Risk",
                "VIDCount",
                "FlagLot",
            ],
        )
        # Source block 47, line 1132
        execute_sql(
            BASE_DIR / "sql/query_047_DATA.sql",
            reader=SqliteReader(),
            output="DATA.csv",
            workdir=workdir,
            values=job_values,
            macros=macro_values_75,
            inputs=["IPM_Data.csv"],
            header=["Lot_NCORisk"],
        )
        # Source block 48, line 1161
        macro_values_75["SIGNAL"] = str(row_count("data.csv", workdir=workdir))
        if int(
            substitute("<<<SIGNAL>>>", values=job_values, macros=macro_values_75)
        ) > int("0"):
            if (
                substitute("<<<DCSR>>>", values=job_values, macros=macro_values_75)
                == "Y"
            ):
                # Source block 51, line 1197
                execute_sql(
                    BASE_DIR / "sql/query_051_RESULT__username__CWFNCO_TS.sql",
                    reader=SqliteReader(),
                    output="RESULT_<<<%username%>>>_CWFNCO{TS}.csv",
                    workdir=workdir,
                    values=job_values,
                    macros=macro_values_75,
                    inputs=[("data.csv", "T0")],
                    header=[
                        "HOLD_LOT",
                        "AUTO_CONTAIN",
                        "LOT",
                        "EMAIL",
                        "CCB_NUMBER",
                        "HOLD_NOTE",
                        "HOLDCATEGORY",
                        "Module",
                    ],
                )
                # Source block 52, line 1229
                copy_file(
                    src=str(Path(".") / "RESULT_*.csv"),
                    dst=substitute(
                        "<<<CSRPATH>>>", values=job_values, macros=macro_values_75
                    ),
                    workdir=workdir,
                )
            # Source block 54, line 1255
            reports["MYREPORT3"] = csv_report(
                "IPM_Data.csv",
                columns=[
                    "facility",
                    "operation",
                    "tool_entity",
                    "primary_entity",
                    "processing_end_date",
                    "lot",
                    "prodgroup3",
                    "product",
                    "visual_id",
                    "ws_loss_code",
                    "media_in_x",
                    "media_in_y",
                    "height",
                    "patch_lift_roi1",
                    "patch_lift_roi2",
                    "patch_lift_roi3",
                    "patch_lift_roi4",
                    "patch_lift_roi5",
                    "patch_lift_roi6",
                    "patch_lift_roi7",
                    "patch_lift_roi8",
                    "patch_lift_roi_max",
                    "lot_1",
                    "patch_sli",
                    "interposer_sli",
                    "nco_risk",
                ],
                headers=[
                    "Facility",
                    "Operation",
                    "Tool Entity",
                    "Primary Entity",
                    "Processing End Date",
                    "Lot",
                    "Prodgroup3",
                    "Product",
                    "Visual Id",
                    "Ws Loss Code",
                    "Media In X",
                    "Media In Y",
                    "Height",
                    "Patch Lift Roi1",
                    "Patch Lift Roi2",
                    "Patch Lift Roi3",
                    "Patch Lift Roi4",
                    "Patch Lift Roi5",
                    "Patch Lift Roi6",
                    "Patch Lift Roi7",
                    "Patch Lift Roi8",
                    "Patch Lift Roi Max",
                    "Lot 1",
                    "Patch Sli",
                    "Interposer Sli",
                    "Nco Risk",
                ],
                alignment=[
                    "middle-left",
                    "middle-left",
                    "middle-left",
                    "middle-left",
                    "middle-left",
                    "middle-left",
                    "middle-left",
                    "middle-left",
                    "middle-left",
                    "middle-left",
                    "middle-left",
                    "middle-left",
                    "middle-left",
                    "middle-left",
                    "middle-left",
                    "middle-left",
                    "middle-left",
                    "middle-left",
                    "middle-left",
                    "middle-left",
                    "middle-left",
                    "middle-left",
                    "middle-left",
                    "middle-left",
                    "middle-left",
                    "middle-left",
                ],
                output_file="SQLPathFinder.htm",
            )
            # Source block 55, line 1282
            render_html(
                BASE_DIR / "html/report_055.html",
                output="EMAIL:<<<dEmail>>>",
                workdir=workdir,
                reports=reports,
                values={
                    **job_values,
                    "VALUE_1": substitute(
                        "<<<DCSR>>>", values=job_values, macros=macro_values_75
                    ),
                    "VALUE_2": substitute(
                        "<<<DMWF>>>", values=job_values, macros=macro_values_75
                    ),
                    "VALUE_3": substitute(
                        "<<<DMST>>>", values=job_values, macros=macro_values_75
                    ),
                },
                macros=macro_values_75,
                styles=styles,
                instance="22697",
                css_file="sqlpathfinder_style_1.css",
                embed_css=True,
            )
            # Source block 56, line 1328
            reports.clear()
            styles.clear()
            css_file = None
            # Source block 57, line 1336
            macro_values_75["HISTERR"] = str(
                row_count("HISTERROR.txt", workdir=workdir)
            )
            if int(
                substitute("<<<HISTERR>>>", values=job_values, macros=macro_values_75)
            ) <= int("0"):
                # Source block 59, line 1360
                smart_append("HIST.csv", "data.csv", workdir=workdir)
                # Source block 60, line 1372
                rename_file(src="HIST.csv", dst="HIST.txt", workdir=workdir)
                # Source block 61, line 1384
                copy_file(
                    src=str(Path(".") / "HIST.txt"),
                    dst=substitute(
                        "<<<IREPOP>>>", values=job_values, macros=macro_values_75
                    )
                    + "\\"
                    + substitute(
                        "<<<SFOLDER>>>", values=job_values, macros=macro_values_75
                    )
                    + "\\KM\\HIST",
                    workdir=workdir,
                )
        # Source block 64, line 1418
        macro_values_75["SIGNAL"] = str(row_count("data.csv", workdir=workdir))
        if int(
            substitute("<<<SIGNAL>>>", values=job_values, macros=macro_values_75)
        ) > int("0"):
            if (
                substitute("<<<DMWF>>>", values=job_values, macros=macro_values_75)
                == "Y"
            ):
                # Source block 67, line 1454
                write_file(
                    path="spfheaderfile.csv",
                    template="DATE,EMAIL,MODULE,SIGNALTYPE,COMPONENT_ID,HOLD_LOT,AUTO_CONTAIN,SHOULD_EMAIL,MMS,ALARMTYPE,SUB_ENTITY_1,SUB_ENTITY_2,SUB_ENTITY_3,FACILITY,PRODGROUP3,ENTITY,DEFECT_SIDE,DEFECT_MODE,LOT_OWNR,IMPACT_LOT_VI,IMPACT_VIDS\n",
                    workdir=workdir,
                    values=job_values,
                    macros=macro_values_75,
                )
                # Source block 68, line 1464
                execute_sql(
                    BASE_DIR / "sql/query_068_SPFMMSTMP.sql",
                    reader=SqliteReader(),
                    output="SPFMMSTMP.csv",
                    workdir=workdir,
                    values=job_values,
                    macros=macro_values_75,
                    inputs=[("IPM_Data.csv", "T0")],
                    header=[
                        "MODULE",
                        "SIGNALTYPE",
                        "ALARMTYPE",
                        "EMAIL",
                        "HOLD_LOT",
                        "AUTO_CONTAIN",
                        "PRODGROUP3",
                        "IMPACT_LOT_VI",
                        "IMPACT_VIDS",
                    ],
                )
                # Source block 69, line 1499
                smart_append("spfheaderfile.csv", "SPFMMSTMP.csv", workdir=workdir)
                # Source block 70, line 1507
                rename_file(
                    src="spfheaderfile.csv",
                    dst="<TS>_"
                    + substitute(
                        "<<<%USERNAME%>>>", values=job_values, macros=macro_values_75
                    )
                    + "_output.pickle.csv",
                    workdir=workdir,
                )
                # Source block 71, line 1517
                delete_files(paths=["SPFMMSTMP.csv"], workdir=workdir)
                # Source block 72, line 1526
                copy_file(
                    src=str(Path(".") / "*_output.pickle.csv"),
                    dst=substitute(
                        "<<<MIPPATH>>>", values=job_values, macros=macro_values_75
                    ),
                    workdir=workdir,
                )
        # Source block 75, line 1558
        write_file(
            path="update.bat",
            template="\n@echo off\nset currd=%date:~10,4%-%date:~4,2%-%date:~7,2%\nset currms=%time:~2,6%\nset /a currh=%time:~0,2%\nif %currh% LSS 10 set currh=0%currh%\nset currt=%currh%%currms%\nset PriConfig=<<<IREPOP>>>\\<<<SFOLDER>>>\n\n\nREM ************** edit here only if needed **************\n\nset record=<<<DCSR>>>,<<<DMST>>>,<<<DMWF>>>\n\n\n\n\nREM *************** do not edit below here ***************\n\ncall :retry\ngoto:eof\n\n:retry\nset /a tries=300\n\n:loop\nif %tries% LEQ 0 goto return\necho <<<STARTTS>>>,%currd% %currt%,<<<UTC>>>,KM,<<<%username%>>>,<<<UNDERDEV>>>,%record%>>%PriConfig%\\VF_CE.txt && goto return || set /a tries-=1 && ping -n 1 127.0.0.1>nul 2>&1 && goto loop\n\n:return\n@exit /B",
            workdir=workdir,
            values=job_values,
            macros=macro_values_75,
        )
        # Source block 76, line 1596
        run_program(argv=["update.bat"], workdir=workdir)
        # Source block 77, line 1607
        delete_files(paths=["update.bat"], workdir=workdir)


if __name__ == "__main__":
    run()
