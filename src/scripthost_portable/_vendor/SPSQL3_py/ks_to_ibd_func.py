"""
--------------------------------------------------
Procedure: ks_to_ibd_func.py      Date: 06/03/2024

Version History:
Date         Who       Ver    Description
------------------------------------------
09/20/2023   jclarke   1.0    Creation
02/22/2024   jclarke   1.1    Added function description to output
05/01/2024   jclarke   2.0    Supported input arguments and being imported
05/17/2024   jclarke   2.1    Added new function mapping
06/03/2024   jclarke   3.0    Made input arguments positional
06/03/2024   jclarke   3.1    Added optional support for output_type, debug and drop_columns fields inteh Excel format
12/10/2025   gcarmiol  3.2    Addition of new option to allow pattern matching in formulas
      
Description:
 Converts an SPF-Kitchensink CSV Function File to an imBigData Function File

Args:
====
  SPF-Kitchen Sink CSV Function File
  imBigData FunctionFile to Create
--------------------------------------------------
"""

__version__ = '3.1'

import os
import sys
import getopt
import pandas as pd
import datetime as dt


def return_args(argall,args):
    """
     ===================================
     Returns Function Argument to a list
       
     ARGS:
     ----
     argall : Expression
     args   : List of arguments

     RETURNS:
     -------
      LIST of arguments
    ==============================
    """
    arg2=argall; ischange=""; return_args=""; cnt1=0

    while arg2 != "" and arg2 != ischange and return_args == "": #arg2 ~ empty, changed from last run & no error
        ischange = arg2
        for j in range(len(arg2)):
            cdata = arg2[j]
            if cdata == "(":
                cnt1 = cnt1+1
            elif cdata == ")":
                cnt1 = cnt1 - 1
            elif cdata == "," and cnt1 == 0:
                mypos3 = j
                args.append(arg2[0:mypos3])
                arg2 = (arg2 + " ")[mypos3 + 1 : ].strip()
                break
    if arg2 != "" and return_args == "":
        args.append(arg2) 
    #print("ARGS"); print(args) #Debug
    
def substitute_fn(query):
    """
    ====================================================================================
    Substitute for spf_fn$ functions (e.g., spf_fn$date_diff()
       
    ARGUMENTS:
    =========
    query     : Script to Parse
        
    RETURNS:
    -------
    Transformed Function
    =====================================================================================
    """
    function_dic={
     "concat_2_drop_ifnull" : "[[1]].concat('_', [[2]])"
    ,"concat_3_drop_ifnull" : "[[1]].concat('_', [[2]], '_', [[3]])"
    ,"concat_4_drop_ifnull" : "[[1]].concat('_', [[2]], '_', [[3]], '_', [[4]])"
    ,"substr_drop_ifnull"   : "[[1]].substring([[2]],[[3]])"
    ,"date_diff_str_round_drop_ifnull" : "parseFloat( ( new Date([[2]].substring(0, 4), [[2]].substring(5, 7), [[2]].substring(8, 10), [[2]].substring(11, 13), [[2]].substring(14, 16), [[2]].substring(17, 19)) - new Date([[1]].substring(0, 4), [[1]].substring(5, 7), [[1]].substring(8, 10), [[1]].substring(11, 13), [[1]].substring(14, 16), [[1]].substring(17, 19)) ) / (1000 * 60 * 60 * 24) ).toFixed(3)"
    ,"ifnull" : "ifnull([[1]],[[2]])"
    }

    function2_dic={
     "{'$cond':{'if':{'$eq':[{'$ifnull':[<1>,'n']},'n']},'then':'$$remove','else':{'$sqrt':{'$add':[{'$multiply':[{'$toint':<1>},{'$toint':<1>}]},{'$multiply':[{'$toint':<2>},{'$toint':<2>}]}]}}}}" : "Math.sqrt( Math.pow( parseFloat(${0}) ,2 ) + Math.pow( parseFloat(${1}) , 2))"
    ,"{'$cond':{'if':{'$eq':[{'$ifnull':[<1>,'n']},'n']},'then':'$$remove','else':{'$arrayelemat':[{'$split':[<1>,'_']},2]}}}" : "${0}.split('_')[2]"
    ,"{'$cond':{'if':{'$eq':[{'$ifnull':[<1>,'n']},'n']},'then':'$$remove','else':{'$arrayelemat':[{'$split':[<1>,'_']},3]}}}" : "${0}.split('_')[3]"
    ,"{'$cond':{'if':{'$or':[{'$eq':[{'$ifnull':[<1>,'n']},'n']},{'$eq':[{'$ifnull':[<0>,'n']},'n']}]},'then':'$$remove','else':{'$round':[{'$sqrt':{'$add':[{'$pow':[{'$toint':<1>},2]},{'$pow':[{'$toint':<2>},2]}]}},2]}}}" : "Math.sqrt( Math.pow( parseFloat(${0}) ,2 ) + Math.pow( parseFloat(${1}) , 2))"
    ,"{'$cond':{'if':{'$or':[{'$eq':[{'$ifnull':[<1>,'n']},'n']},{'$eq':[{'$ifnull':[<2>,'n']},'n']}]},'then':'$$remove','else':{'$round':[{'$sqrt':{'$add':[{'$pow':[{'$toint':<1>},2]},{'$pow':[{'$toint':<2>},2]}]}},2]}}}" : "Math.sqrt( Math.pow( parseFloat(${0}) ,2 ) + Math.pow( parseFloat(${1}) , 2))"
    ,"spf_fn$substr_drop_ifnull(<1>,0,-1)" : "${0}.substring(0)"
    ,"spf_fn$round_drop_ifnull(spf_fn$date_diff_str_drop_ifnull(<1>,<2>),2)" : "parseFloat( ( new Date(${0}.substring(0, 4), ${0}.substring(5, 7), ${0}.substring(8, 10), ${0}.substring(11, 13), ${0}.substring(14, 16), ${0}.substring(17, 19)) - new Date(${1}.substring(0, 4), ${1}.substring(5, 7), ${1}.substring(8, 10), ${1}.substring(11, 13), ${1}.substring(14, 16), ${1}.substring(17, 19)) ) / (1000 * 60 * 60 * 24) ).toFixed(2)"
    }

    if query in function2_dic.keys():
        finalquery = function2_dic[query]
    else:
        args=[]
        subtoken = "SPF_FN$"
        finalquery=query
        docontinue=True
        any_if_null=False
        #print("Query1a=", finalquery) #Debug
        while docontinue:
            #=========================================================
            #Search for spf_fn$ token
            #=========================================================
            #print("Query1b=", finalquery) #Debug
            mylen = len(finalquery) #May Change as substitution is done
            mypos = finalquery.upper().find(subtoken)
            if mypos != -1: #function token found
                #============================================================================
                #Get "("..E.g., spf_fn$date_diff(
                #============================================================================
                mypos1 = finalquery[mypos:].find("(")
                if mypos1 != -1:
                    token=finalquery[mypos + len(subtoken) : mypos+mypos1].lower().strip()
                    if token=="ifnull":
                        any_if_null=True
                    #print("Token=",token) #Debug
                else:
                    finalquery="ERROR: Could not find opening ( for " + subtoken + token
                    break
                
                argall = ""; arg1 = "'";  arg2 = ""; arg3 = ""; arg4 = ""

                #=================================
                #Get Arguments
                #=================================
                cnt = -1
                mypos2 = -99
                for i in range((mypos + mypos1), mylen):
                    cdata = finalquery[i]
                    #print("CData=", cdata) #Debug
                    if cdata == "(":
                        cnt = cnt + 1
                    elif cdata == ")" and cnt == 0:
                        mypos2 = i
                        break
                    elif cdata == ")":
                        cnt = cnt - 1

                if mypos2 == -99:
                    finalquery = "ERROR: Could not find closing ) for " + subtoken + token
                else:
                    argall = finalquery[mypos+mypos1+1 : mypos2 ].strip()
                    #print("ArgAll=", argall) #Debug
                
                    return_args(argall, args)
                    #print("Args="); print(args) #Debug
               
                    if token in function_dic.keys():
                        tmp = function_dic[token]
                    else:
                        tmp=""

                    if tmp != "":
                        argall = tmp
                        if 'substring' in argall:
                            try:
                                arg_fix2 = int(args[1])
                                arg_fix3 = arg_fix2 + int(args[2])
                                args[1] = str(arg_fix2)
                                args[2] = str(arg_fix3)
                            except:
                                pass
                        for i in range(len(args)):
                            argall = argall.replace("[[" + str(i + 1) + "]]", args[i])
                    else:
                        finalquery = "ERROR: " + subtoken + token + " is not supported"

                #============================================================================
                #Assign Substitute_Fn the string from current query up till where token was found
                #============================================================================
                if (finalquery+"     ")[0:5] != "ERROR":
                    finalquery = finalquery[0 : mypos] + argall + finalquery[mypos2 + 1:]
                    #print("FinalQuery2=", finalquery) #Debug
            args.clear()
            if mypos == -1 or (finalquery + "     ")[0:5] == "ERROR":
                docontinue=False
            #print("DoContinue="); print(docontinue) #Debug

        for i in range(20):
            finalquery = finalquery.replace("<" + str(i + 1) + ">", "${" + str(i) + "}")

        #print("FinalQuery3=", finalquery) #Debug

        if (finalquery+"     ")[0:5]!="ERROR":
            if any_if_null:
                finalquery="function ifnull(p1,p2) {if (p1==null) { return p2 } else {return p1} };\n\n" + "          " + finalquery

    return finalquery

def loaddata(myinfile):
    i_dlm=','
    if os.path.splitext(myinfile.lower())[1] == '.tab':
        i_dlm='\t';
    elif os.path.splitext(myinfile.lower())[1] == '.asc':
        i_dlm='|';
    elif os.path.splitext(myinfile.lower())[1] == '.plus':
        i_dlm='+';

    try:
        if not os.path.exists(myinfile):
            raise Exception(f'Error: Unable to locate file: {myinfile}')
            
        df = pd.pandas.read_csv(myinfile, sep=i_dlm, na_values=['.',''])
        df.columns = map(str.lower, df.columns) #Lowercase
        cols_list=df.columns

        ncols=len(cols_list)
        csv_version=-1
        if ncols==7 and cols_list[0]=="header" and cols_list[1]=="expression" and cols_list[2]=="fields" and cols_list[3]=="starting_pattern" and cols_list[4]=="output_type" and cols_list[5]=="debug" and cols_list[6]=="drop_column":
            csv_version=1
        elif ncols==4 and cols_list[0]=="header" and cols_list[1]=="expression" and cols_list[2]=="fields" and cols_list[3]=="starting_pattern":
            csv_version=0  
        if csv_version==-1:
            errMsg="An unsupported number of columns or invalid column headers were found in the SPF-CSV file."
            errMsg=errMsg + f"\nExpecting either 4 or 7 fields and found {ncols}."
            errMsg=errMsg + "\nHeaders expected are: header,expression,fields,starting_position,output_type,debug,drop_column"
            raise Exception(errMsg)
                   
        return df,csv_version
    except Exception as e:
        mye=str(e)
        errMsg=f'############################################################################\nError: Unable to process: {myinfile}\n {mye} ############################################################################\n'
        raise Exception(errMsg)

def get_input_args(argv,input_args):
    if len(argv) < 3:
        errMsg="Expecting two positional arguments: (1) The SPF CSV File to convert and (2) the imBigData Function file to create"
        raise Exception(errMsg)
    else:   
        input_args['infile'] = argv[1].strip()
        input_args['outfile'] = argv[2].strip()

        input_args['infile'] = input_args['infile'].replace("\\","/")
        input_args['outfile'] = input_args['outfile'].replace("\\","/")

        if input_args['infile'] == '' or (input_args['infile'] != '' and not os.path.isfile(input_args['infile'])):
            errMsg = f"The CSV function input file path is invalid\n  {input_args['infile']}"
            raise Exception(errMsg)

        if input_args['outfile'] == '':
            errMsg = f"The imBigData function output file path is invalid\n  {input_args['outfile']}"
            raise Exception(errMsg)

        try:
            if os.path.exists(input_args['outfile']):
                os.remove(input_args['outfile'])
        except Exception as e:
            errMsg = f"Unable to remove output file\n  {e}"
            raise Exception(errMsg)

def execute(cmd_line_args):
    """
    Description
        Serves as main execution method when when being used in an import

    Arguments:
        cmd_line_args      : Command line arguments as described in the main docstring
    """
    currdate = dt.datetime.today()
    print ('Convert SPF-KS CSV File to an imBigData Function File Utility, v' + __version__ + ' - ' + str(dt.datetime.today()) + '\n');

    ######################################
    # Get Input Arguments
    ######################################
    input_args = {}
    input_args['infile'] = ''
    input_args['outfile'] = 'ibd_Functions.txt'
    get_input_args(cmd_line_args, input_args)
    myinfile=input_args['infile']
    myoutfile=input_args['outfile']

    print('  KS Input Function File         = ' + myinfile)
    print('  imBigData Output Function File = ' + myoutfile)

    try:
        df,csv_version=loaddata(myinfile)
        myfunctions=""
        counter=0
        for i in range(len(df)):
            myfunc=""
            myheader=df.loc[i, "header"].strip()
            myexpression=df.loc[i, "expression"].strip().replace(" ","").lower()
            myfields=df.loc[i, "fields"].strip()
            mystart=df.loc[i, "starting_pattern"].strip()
            mytype="STRING"
            mydebug="N"
            mydrop_column=""
            if csv_version==1:
                mytype=str(df.loc[i, "output_type"]).strip().upper()
                if mytype!="STRING" and mytype != "LONG" and mytype != "DOUBLE" and mytype != "DATETIME":
                    mytype="STRING"
                mydebug=str(df.loc[i, "debug"]).strip().upper()
                if mydebug!="Y" and mydebug != "N":
                    mydebug="N"
                mydrop_column=str(df.loc[i, "drop_column"]).strip()
                if mydrop_column=="nan":
                    mydrop_column=""

            if myheader !="" and myexpression != "" and myfields != "" and mystart != "":
                counter=counter+1
                pos=myfields.find(",")
                if pos != -1:
                    myfield1=myfields[0:pos]
                else:
                    myfield1=myfields
                if mystart[0:1] != "(":
                    mystart="(" + mystart + ")"

                myfunc="\n       <Function ID      ='" + str(i) + "'"
                myfunc= myfunc + f"\n           OUTPUT_TYPE   ='{mytype}'"
                myfunc= myfunc + "\n           PATTERN       ='" + myfield1.replace("<pattern>",mystart) + "'"
                myfunc= myfunc + "\n           COLUMN_NAME   ='" + myheader.replace("<pattern>","$1") + "'"
                myfunc= myfunc + "\n           INPUT_COLUMN  ='" + myfields.replace("<pattern>","$1") + "'"
                myfunc= myfunc + "\n           COLUMN_MATCH_MODE='PATTERN'"
                if mydrop_column !="":
                    myfunc= myfunc + f"\n           DROP_COLUMN   ='{mydrop_column}'"
                myfunc= myfunc + f"\n           DEBUG         ='{mydebug}'>"
                myfunc =myfunc + "\n        <Expression>"
                myfunc =myfunc + "\n         <![CDATA["
                #print("Expression=", myexpression) #Debug
                myexpr=substitute_fn(myexpression)
                #print("Translated=", myexpr) #Debug

                if (myexpr+"     ")[0:5]=="ERROR":
                     myfunc= myfunc + "\n          " + myexpression + ";"
                else:
                     myfunc= myfunc + "\n          " + myexpr + ";"
                myfunc =myfunc + "\n         ]]>"
                myfunc =myfunc + "\n        </Expression>"
                myfunc =myfunc + "\n       </Function>\n"
 
                myfunctions= myfunctions + myfunc

                myheader2="       <Function    ID ='Opt. id. E.g., 1'\n           OUTPUT_TYPE ='Field Data type. I.e., DATETIME|DOUBLE|LONG|STRING'\n           PATTERN     ='List of patterns to extract bounded in parenthesis. E.g., ul#die#(.*)#x_location. Refer to patterns positionally, (e.g., $1 as 1st pattern) in COLUMN_NAME and INPUT_COLUMN.'\n"
                myheader2 +="           COLUMN_NAME ='Field header. E.g., ul#user#$1_radius'\n"
                myheader2 +="           INPUT_COLUMN='List of function input fields. E.g., ul#die#$1#x_location,ul#die#$1#y_location. Refer to input field position in EXPRESSION. E.g., ${0} for 1st field '\n           DROP_COLUMN ='Opt. list of exact field names to drop after function execution'\n           DEBUG       ='Y or N to include debug info to diagnose errors'>\n"
                myheader2 +="        <Expression>\n         <![CDATA[     E.g., Math.sqrt( Math.pow( parseFloat(${0}) ,2 ) + Math.pow( parseFloat(${1}) , 2));  \n           ]]>\n        </Expression>\n       </Function>"

        if myfunctions != "":
            myfunctions = "\n<!--\n" + myheader2 + "\n-->\n\n" + myfunctions
            with open(myoutfile, 'w') as f:
                f.write(myfunctions)
        print ("\n" + myheader2)
        #print (myfunctions)
        #print('  KS Input Function File         = ' + myinfile)
        #print('  imBigData Output Function File = ' + myoutfile)
        print('\n  Converted',str(counter), "functions:\n")
    except Exception as err:
        raise Exception(f'Error: {err} ...')

if __name__ == "__main__":
    # Execute entry point to the program passing command line arguments to the method
    try:
        execute(sys.argv)
    except Exception as err:
        raise Exception(f'Error : {err} ...\nExiting ...\n')

