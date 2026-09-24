"""
-----------------------------------------------------
Procedure: ideal_variable_config.py      Date: 12/06/2018

Version History:
Date          Who         Ver       Description
-----------------------------------------------------
12/11/2018    gcarmiol    1.0       Initial Version
04/23/2019    gcarmiol    2.0       Changes due to improvements in ideal_variable_config done for AutoCommonality
10/31/2022    gcarmiol    2.1       Adding logger required in ideal_table_config
09/30/2024    gcarmiol    2.2       Removed unnecessary sys.exit(0) which would give warning in some systems


Description:
============
SQLPF Python utility that executes the configuration of Ideal .ide file variables...works in Python 2.7.x/3.6.x
All methods used are in PyIdealMethods.py file
"""

from PyIdealMethods import IdealMethods
import logging
import sys
import os
import datetime as dt

if __name__ == "__main__":

    logger = logging.getLogger(__name__)
    logger.setLevel(logging.INFO)
    formatter = logging.Formatter('%(message)s')
    logging_handler = logging.StreamHandler()
    logging_handler.setFormatter(formatter)
    logger.addHandler(logging_handler)
    
    print('Starting Ideal Variable Config, v2.0, ' + str(dt.datetime.today()) + ' ...\n')

    if len(sys.argv) >= 2:
        data_file = sys.argv[1]
    else:
        print("\n===============================================================")
        print("Missing argument 1, the Data File to Load in Ideal. Exiting ...")
        print("===============================================================")
        print("\n")
        print("\n")
        sys.exit(1)
    if len(sys.argv) >= 3:
        config_file = sys.argv[2]
    else:
        print("\n=============================================================")
        print("Missing argument 2, the Ideal Configuration File. Exiting ...")
        print("=============================================================")
        print("\n")
        print("\n")
        sys.exit(1)
    if len(sys.argv) >= 4:
        output_ide = sys.argv[3]
    else:
        print("\n=============================================================")
        print("Missing argument 3, the Ideal IDE file to create. Exiting ...")
        print("=============================================================")
        print("\n")
        print("\n")
        sys.exit(1)

    myide = os.path.splitext(output_ide)[1]
    if not (myide.lower() == '.ide' or myide.lower() == '.idt'):
        print("\n")
        print("===================================================================")
        print("IDEAL file extension needs to be '.ide' or '.idt' ...exiting")
        print("===================================================================")
        print("\n")
        print("\n")
        sys.exit(1)

    try:
        print('\nInput arguments are:\n')
        print('  1. CSV File   : ' + data_file)
        print('  2. Config File: ' + config_file)
        print('  3. Output File: ' + output_ide)
        print('\n')
        myIdeal = IdealMethods(data_file, config_file, logger=logger)
    except:
        print("\n")
        print("\n=================================================================")
        print("IDEAL must be installed to use this utility,")
        print("please install at http://ideal.intel.com/sitefiles/main.asp")
        print("=================================================================")
        print("\n")
        print("\n")
        sys.exit(1)



    try:
        print('\nWriting Output .ide/.idt file\n')
        myIdeal.Save_table(output_ide)
        print('\nDone at ' + str(dt.datetime.today()) + ' ...')

    except:
        print("\nDone. Exiting with exit code 1 at " + str(dt.datetime.today()) + " ..." )
        print("\n")
        print("\n")
        sys.exit(1)

