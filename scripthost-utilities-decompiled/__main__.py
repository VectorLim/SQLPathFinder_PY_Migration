import sys
import os
import datetime as dt
import zipfile
import subprocess
import shutil
import random

"""
==================================================================
 Procedure: RunSPF                            Date: 8/6/2024

 Description:
    Runs a SQLPathFinder ZIP

 Version History:
Date           Prgrmr         Version      Description
----------------------------------------------------------------------------
05/05/2023     J.Clarke        1.0         Creation
05/10/2023     J.Clarke        1.5         Adjusted ZIP File path depending on whether local or SH & where files are unzipped
10/31/2023     J.Clarke        1.6a        Corrected check for whether to unzip SPSQL3_PY.zip
08/06/2024     J.Clarke        1.7         Fixed bug assigning R_VERSION

 Arguments:
 =========
  /MACROFILE=   Optional Macro File to use. Query variables bounded by <<< >>> will be
                replaced by values in row 1 of the equivalent Macro File column header 
  /SPFDIR=      Optional path to the SPF Install directory
  /STARTDIR=    Optional Start up folder
  /R_VERSION=   Optional R version to use (DEFAULT,NEXT). Default is DEFAULT
  /SPFUSEPYEESH=Optional Y or N. If = Y use PyEE on SH share. If = N use Engine version packed with the job

==================================================================
"""
__version__ = '1.7'

def get_input_args(argv,input_args, isSH):
    input_args['allargs']=""
    input_args['macrofile'] = ""
    input_args['spfdir'] = ""
    input_args['startdir'] = ""
    input_args['r_version'] = ""
    input_args['spfusepyeesh'] = "" #default use packed PyEE. Pass /SPFUSEPYEESH=Y to use PyEE available on SH. 
    input_args['zipfile'] = sys.argv[0]
    #print(input_args['zipfile']); #Debug
    input_args['zipfile']=os.path.abspath(input_args['zipfile'])
    #print(input_args['zipfile']); #Debug
    #if isSH != '%SHServer%': #on Script Host
    #    input_args['zipfile']=os.path.basename(input_args['zipfile']) #Get File Name only
    n=len(argv)
    #print("Total arguments passed = " + str(n)) #debug
    arg_err_count=0

    for i in range(1,n):
        argv[i]=argv[i].strip()
        pos=argv[i].find("=")
        if argv[i][0:1]=="/" and pos != -1 and argv[i] != "/=":
            #print("Argument " + str(i) + " = " + sys.argv[i]) #debug
            mytoken=argv[i][1:pos].upper()
            myval=argv[i][pos+1:]
            #print("    Argument Token=" + mytoken + " and Argument Value=" + myval); #debug
            if mytoken=="MACROFILE": 
                input_args['macrofile'] = myval
                input_args['macrofile'] = input_args['macrofile'].replace("\\","/")
                input_args['allargs'] = input_args['allargs'] + ' /MACROFILE="' + input_args['macrofile'] + '"'
            elif mytoken == "SPFDIR" and isSH =='%SHServer%': #Local Only
                input_args['spfdir'] = myval
                input_args['spfdir'] = os.path.expandvars(input_args['spfdir'])
                input_args['spfdir'] = input_args['spfdir'].replace("\\","/")
                input_args['allargs'] = input_args['allargs'] + ' /SPFDIR="' + input_args['spfdir'] + '"'
            elif mytoken == "STARTDIR":  #Local Only
                input_args['startdir']=myval
                input_args['startdir']=os.path.expandvars(input_args['startdir'])
                input_args['startdir']=input_args['startdir'].replace("\\","/")
                input_args['allargs'] = input_args['allargs'] + ' /STARTDIR="' + input_args['startdir'] + '"'
            elif mytoken == "R_VERSION":
                input_args['r_version'] = myval.upper().strip()
            elif mytoken == "SPFUSEPYEESH":
                input_args['spfusepyeesh'] = myval.upper().strip()
            elif mytoken != "SPFSQL":
                input_args['allargs'] = input_args['allargs'] + ' /' + mytoken + '="' + myval + '"'

        else:
            arg_err_count+=1
            print("Unrecognized argument passed in position " + str(i) + ": " + argv[i])
            break
    if arg_err_count >0:
        print('==========================================================================================================================================');
        print('An error was found in one or more arguments passed at the command line.')
        print('Make sure there are no spaces between the argument, equal sign and value and values with spaces are bounded in quotes. E.g., /DATA="123 45"');
        print('Exiting ...')
        print('==========================================================================================================================================');
        sys.exit(1);

    ######################################################
    #Test whether any Macro File passed to job exists
    ######################################################
    if input_args['macrofile'] != "" and (not os.path.exists(input_args['macrofile']) or not os.path.isfile(input_args['macrofile'])) :
        print('==========================================================================================================================================');
        print('Macro File Passed to this job does not exist: ' + input_args['macrofile'])
        print('Exiting ...')
        print('==========================================================================================================================================');
        sys.exit(1);

    ######################################################
    #Test Whether SPF Install DIR, if passed, exists
    ######################################################
    if input_args['spfdir'] != "" and not os.path.isdir(input_args['spfdir']):
        print('==========================================================================================================================================');
        print('The SQLPathFinder Install directory passed to this job does not exist: ' + input_args['spfdir'])
        print('Exiting ...')
        print('==========================================================================================================================================');
        sys.exit(1);

    ##########################################################
    #Test Whether Start DIR, if passed, exists and is writable
    ##########################################################
    if input_args['startdir'] != "" and input_args['startdir'].upper() != "%TEMP%" and input_args['startdir'][0:2] != '//' and input_args['startdir'][0:2] != '\\\\' and (not os.path.isdir(input_args['startdir']) or not os.access(input_args['startdir'], os.W_OK) ) :
        print('==========================================================================================================================================');
        print('The Start folder passed to job either does not exist, is not writable or is not local:' + input_args['startdir'])
        print('Exiting ...')
        print('==========================================================================================================================================');
        sys.exit(1);   

    if input_args['r_version']=="NEXT":
        input_args['allargs'] = input_args['allargs'] + ' /R_VERSION="NEXT"'   

def delete_dir(isSH, myzipdir):
    ####################################
    #Remove ZIP folder for local queries
    ####################################
    if isSH =='%SHServer%': # Local
        try:
            shutil.rmtree(myzipdir, ignore_errors = True)
        except Exception as error:
            print(f"Error removing work folder: {myzipdir}\n{error}")
            pass
         
if __name__ == "__main__":

    #currdate = dt.datetime.today()
    print(f'Starting RunSPF Py, v{__version__} ... ' + str(dt.datetime.today()) + '\n',flush=True);

    input_args = {};

    g_SPFSWSHDir="d:/sqlpathfinder/software"  #SH SPF SW Dir
    mypyexe=sys.executable                    #Python executable
    isSH =os.path.expandvars('%SHServer%')    #ScriptHost Environment Variable

    get_input_args(sys.argv, input_args, isSH);

    #==========================================================================================================
    #See if SPFDir can be determined if it is not passed for local queries. If it is not set or cannot be
    #determined, then exit with an error message
    #==========================================================================================================
    if isSH =="%SHServer%" and input_args['spfdir']=="":
        pos=mypyexe.lower().rfind("\\python3\\python.exe")  #is this call using SPF's Python
        if pos != -1:
            input_args['spfdir']=mypyexe[0:pos]
        else:
            print('==========================================================================================================================================');
            print('You must pass argument /SPFDIR="<SQLPathFinder install folder>" so the SPF Captive Python install can be located')
            print('Exiting ...')
            print('==========================================================================================================================================');
            sys.exit(1);
  
    #==========================================================================================================
    #Get SPF Extract Engine Script Path & Optionally set the tns_admin_path
    #==========================================================================================================
    if input_args['startdir'] != "":
        os.chdir(input_args['startdir'])
    else:
        input_args['startdir'] = os.getcwd()
    if isSH =='%SHServer%': # Local as Note that Env Var SHServer is set on ScriptHost
        myspfpath=input_args['spfdir']
        rnumstr = str(random.randint(0, sys.maxsize-1))
        mysubdir=os.path.join(input_args['startdir'],"spf_"+rnumstr)
        if not os.path.exists(mysubdir):
            os.mkdir(mysubdir)
        myzipdir=mysubdir
        tns_admin_path = os.path.join(myspfpath,"schema"); os.environ["tns_admin"] = str(tns_admin_path)
        mypath = os.path.expandvars('%path%'); os.environ["path"] = myspfpath.replace("/","\\").strip() + r"\Oracle\instantclient_19_17;" + mypath
    else: #On ScriptHost
        myzipdir =os.path.expandvars('%temp%')
        myspfpath=myzipdir
        tns_admin_path = g_SPFSWSHDir; os.environ["tns_admin"] = str(tns_admin_path)
    print('Start folder = ' + input_args['startdir'],flush=True)
    print('Python Path  = ' + mypyexe,flush=True)
    print('SPF EE Path  = ' + myspfpath,flush=True)
    print('Unzip  Path  = ' + myzipdir,flush=True)
    print('Zip to Run   = ' + input_args['zipfile'] + '\n',flush=True)

    mypyee = os.path.join(myspfpath,'SPFSQL3.py')

    #==========================================================================================================
    #Unzip DoJMP.py, SPSQL3_py.zip, and SPFFileToLoad (must determine file name)
    #==========================================================================================================
    #SPFFileToLoad=""
    SPF_List=[]
    spfzip=zipfile.ZipFile(input_args['zipfile'],'r')
    zipinfos = spfzip.infolist()
    for zipinfo in zipinfos:
        fname=zipinfo.filename.lower()
        split_ext = os.path.splitext(fname)
        #print(fname); #debug
        #if fname != 'dojmp.py' and fname != 'spsql3_py.zip' and fname != '__main__.py':
        if len(split_ext) >=2 and (split_ext[1]==".spf" or split_ext[1]==".spfsql"):
            SPFFileToLoad=str(zipinfo.filename)
            SPF_List.append(str(zipinfo.filename))
            #break
    #print("SPFFileToLoad= " + SPFFileToLoad); #debug
    SPF_List.sort()
    #print("SPF_List="); print(SPF_List); print(len(SPF_List))#debug
    if len(SPF_List) == 0:
        print('==========================================================================================================================================');
        print('Missing SPF Files to run in this zip file: ' + input_args['zipfile'])
        print('Exiting ...')
        print('==========================================================================================================================================');
        spfzip.close()
        sys.exit(1);
    else:
        for zipinfo in zipinfos:
            fname=zipinfo.filename.lower()
            if fname == 'dojmp.py':
                spfzip.extract(zipinfo,input_args['startdir'])
            else:
                spfzip.extract(zipinfo,myzipdir)
        #spfzip.extractall(path=myzipdir) #save to work folder..
        spfzip.close()

    #==========================================================================================================
    #On SH, unzip spsql3_py.zip and SPFBin.zip
    #==========================================================================================================
    if isSH != '%SHServer%': #on Script Host
        if  not os.path.exists(os.path.join(myzipdir,"SPFLib","SPFSQL3.py")): #Zip file is not already unzipped
            if input_args['spfusepyeesh'] == "Y":
                myzipf = os.path.join(g_SPFSWSHDir, "SPSQL3_py.zip")
            else:
                myzipf=os.path.join(myzipdir, "SPSQL3_py.zip")
            spfzip=zipfile.ZipFile(myzipf,'r')
            spfzip.extractall(path=myzipdir)
            spfzip.close()
            myzipf = os.path.join(g_SPFSWSHDir, "SPFBin.zip")
            spfzip=zipfile.ZipFile(myzipf,'r')
            spfzip.extractall(path=myzipdir)
            spfzip.close()

    try:
    
        for SPFFileToLoad in SPF_List:
            myfile = os.path.join(myzipdir, SPFFileToLoad) 
            input_args['allargs'] = ' -u ' + '/SPFSQL="' + myfile + '"' +  input_args['allargs']

            if isSH =="%SHServer%": #Local
                input_args['allargs'] = input_args['allargs'] + ' /MYLOCAL="' + input_args['spfdir'] + '" /EXEDIR="' +  input_args['spfdir'] + '"'
            else:
                input_args['allargs'] = input_args['allargs'] + ' /MYLOCAL=N /EXEDIR="//atdfile3.ch.intel.com/atd-web/PathFinding/SQLPathFinder/Software/Library"'
    
            mycommand = '"' + mypyexe + '" "' + mypyee + '" ' + input_args['allargs']
            #print(mycommand) #debug
            myreturn=subprocess.call(mycommand, shell=True)
            if myreturn !=0:
                break;
    except Exception as err:
        print(err)
    finally:
        #cum_time=str(dt.datetime.today() - currdate);
        delete_dir(isSH, myzipdir)
        if myreturn!=0:
            print('==========================================================================================================================================');
            print('RunSPF completed with errors!')
            print('==========================================================================================================================================');
        print(    "Done ... " + str(dt.datetime.today()) + '\n');
        sys.exit(myreturn)