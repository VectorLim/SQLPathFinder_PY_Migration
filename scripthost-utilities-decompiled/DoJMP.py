r"""
==================================================================
 Procedure: DoJMP                        Date: 9/09/2021
 
Called from JMP to help add images to a PowerPoint presentation
or to email a report

 Version History:
    Date           Prgrmr         Version     Description
   ----------------------------------------------------------------------------
    09/01/2021     J.Clarke       1.0         Creation
    09/09/2021     J.Clarke       1.1         Ensured email procedure works on ScriptHost
    07/26/2025     vanatara       1.2         Update to support Python 3.13

 ARGUMENTS:
 ---------
  1 : Mode of Operation. E.g., PPT or EMAIL
  2 : For PPT     : PPT Input Template
      For EMAIL/Q : HTML File to Email
  3 : For PPT   : PPT Output File
      For EMAIL/Q : Distribution List
  4 : For PPT   : Path and Name of File listing Images stored in the same folder as the file which are to be loaded
    : For EMAIL : Folder With Images
    : For EMAILQ: Query attachment to email    
  5 : For PPT   : N/A
    : For EMAIL : Subject Line
  6 : ll_Outlook: Y for Outlook

 Sample Invocation:
 =================
   python.exe testppt.py  -m PPT -s '' -o new.pptx -i spfgfx_11660.images_Win4_11660
   python.exe testppt.py  -m PPT -s existing.pptx -o new2.pptx -i spfgfx_11660.images_Win4_11660
   python.exe testppt.py  -m EMAIL -s JMP_Test.htm -o self -i .\gfx -e Test-Email-from-JMP -t Y
   python.exe testppt.py  -m EMAILQ -s out.txt -o self -i test.vg2 -e Help-Needed -t Y
"""

import os
import sys, getopt
import datetime as dt
from pptx import Presentation
from pptx.util import Inches
from pptx.enum.shapes import MSO_SHAPE
from pptx.util import Pt
import subprocess


def get_input_args(argv,input_args):
    #print(argv); #debug
    input_args['mode'] = ''
    input_args['src'] = ''
    input_args['out'] = ''
    input_args['img'] = ''
    input_args['esubj'] = 'Email from SQLPathFinder - Sent ' + str(dt.datetime.today())
    input_args['etype'] = 'N' 

    
    opts, args = getopt.getopt(argv,"m:s:o:i:e:t:",["mode=","src=","out","img=","esubj=","etype="]);
    for opt, arg in opts:
        #print(opt) #debug
        #print(arg) #debug
        if opt in ("-m", "--mode"):
           input_args['mode'] = arg.upper().strip();
        elif opt in ("-s", "--src"):
            input_args['src'] = arg.strip();
        elif opt in ("-o", "--out"):
            input_args['out'] = arg.strip();
        elif opt in ("-i", "--img"):
            input_args['img'] = arg.strip();
        elif opt in ("-e", "--esubj"):
            input_args['esubj'] = arg.strip();
        elif opt in ("-t", "--etype"):
            input_args['etype'] = arg.upper().strip();
            if input_args['etype'] == "O":
               input_args['etype']="Y"
 
    if input_args['mode'] != 'PPT' and input_args['mode'] != "EMAIL" and input_args['mode'] != "EMAILQ":
        print('');
        print('############################################################################\n');
        print ('Error: Argument -m must be PPT or EMAIL or EMAILQ: (' + input_args['mode'] + ')');
        print ('Exiting ...\n');
        print('############################################################################');
        print('');
        sys.exit(1);
    elif input_args['mode'] == "PPT":
        if input_args['out'] == "":
            print('');
            print('############################################################################\n');
            print ('Error: Argument -o missing. PowerPoint Output file not passed');
            print ('Exiting ...\n');
            print('############################################################################');
            print('');
            sys.exit(1);
        elif input_args['img'] == "":
            print('');
            print('############################################################################\n');
            print ('Error: Argument -i missing. File with Images not passed');
            print ('Exiting ...\n');
            print('############################################################################');
            print('');
            sys.exit(1);
    elif input_args['mode'] == "EMAIL":
        if input_args['out'] == "":
            print('');
            print('############################################################################\n');
            print ('Error: Argument -o missing. Email Distribution list not passed');
            print ('Exiting ...\n');
            print('############################################################################');
            print('');
            sys.exit(1);
        elif input_args['src'] == "":
            print('');
            print('############################################################################\n');
            print ('Error: Argument -s missing. File to Email not passed');
            print ('Exiting ...\n');
            print('############################################################################');
            print('');
            sys.exit(1);
        elif input_args['img'] == "":
            print('');
            print('############################################################################\n');
            print ('Error: Argument -i missing. Folder with Images not passed');
            print ('Exiting ...\n');
            print('############################################################################');
            print('');
            sys.exit(1);
        elif not os.path.isdir(input_args['img']):
            print('');
            print('############################################################################\n');
            print ('Error: Argument -i error. Folder with Images not found: ' + input_args['img']);
            print ('Exiting ...\n');
            print('############################################################################');
            print('');
            sys.exit(1);     

        if (input_args['out'] + "      ").upper()[0:6] == "EMAIL:":
            input_args['out'] = input_args['out'][6:]
        elif (input_args['out'] + "        ").upper()[0:6] == "EMAIL-A:":
            input_args['out'] = input_args['out'][9:]
        if input_args['out'] == "":
            print('');
            print('############################################################################\n');
            print ('Error: Argument -o missing. Email Distribution List not passed');
            print ('Exiting ...\n');
            print('############################################################################');
            print('');
            sys.exit(1); 
    elif input_args['mode'] == "EMAILQ":
        if input_args['out'] == "":
            print('');
            print('############################################################################\n');
            print ('Error: Argument -o missing. Email Distribution list not passed');
            print ('Exiting ...\n');
            print('############################################################################');
            print('');
            sys.exit(1);
        elif input_args['img'] == "":
            print('');
            print('############################################################################\n');
            print ('Error: Argument -i missing. Query to email not passed');
            print ('Exiting ...\n');
            print('############################################################################');
            print('');
            sys.exit(1);

        """
        try:
            if os.path.exists(input_args['conf']) and os.path.isfile(input_args['conf']):
                with open(input_args['conf'],'r') as my_file:
                    input_args['conn'] = my_file.read();
            else:
                print('############################################################################\n');
                print ('Error: Unable to find connection file:\n' + input_args['conf']);
                print('############################################################################\n');
                sys.exit(1);
        except Exception as e:
            print('############################################################################\n');
            print ('Error: Unable to read connection file:\n' + input_args['conf'] + '\n' + str(e) + '\n');
            print('############################################################################\n');
            sys.exit(1); 
        """

        
def AddPPT (MyPPTIn, MyPPTOut, MySrcFolder, MyDefDir):
    """
    ==============================================
    Add Images to PowerPoint
     ARGUMENTS:
     ---------
      MyPPTIn     : PPT Input Template
      MyPPTOut    : PPT Output File
      MySrcFolder : File With Image paths
      MyDefDir    : Default Directory
    ==============================================
    """

    print("=====================================================================================================")
    print("PowerPoint Input File : " + MyPPTIn)
    print("Image File            : " + MySrcFolder)
    print("PowerPoint Output File: " + MyPPTOut)
    print("=====================================================================================================")

    ######################################
    #Open File with path to Images to load
    ######################################
    if MySrcFolder.rfind("/") == -1 :
        MySrcFolder = os.path.join(MyDefDir,MySrcFolder)
    isok="Y"
    
    try:
        if os.path.isfile(MySrcFolder):
            with open(MySrcFolder) as f:
                images = f.readlines()

            #####################
            #Cleanse Image List
            #####################
            for i, s in reversed(list(enumerate(images))):
                imgfile = s.replace("\n","")
                splitf = os.path.splitext(imgfile)
                myext="" 
                if len(splitf) == 2:
                    myext=splitf[1].upper().strip()
                if (myext == ".PNG" or myext == ".BMP" or myext == ".GIF" or myext == ".JPG") and os.path.isfile(imgfile):
                    images[i]=imgfile
                else:
                    del images[i]
            noimages=len(images)
        else:
            print("==========================================================")
            print("The PowerPoint Image File List was not found: " + MySrcFolder)
            print("Exiting ...")
            print("==========================================================")
            sys.exit(1); 
         
    except Exception as e:
        isok="N"
        print('############################################################################\n');
        print ('Error: Unable to process file with images:\n' + MySrcFolder + '\n' + str(e) + '\n');
        print('############################################################################\n');
        sys.exit(1); 
    
    if isok=="Y":
    
        if noimages <= 0:
            print("==========================================================")
            print("No image files found. Exiting ...")
            print("==========================================================")
            sys.exit(1); 

        else:
        
            try:
                prs=Presentation()
            
                if MyPPTIn == "":
                    #####################################################
                    #For each image, add a slide with a Title & load Image
                    #####################################################
                    for imgfile in images:
                        ###################
                        # Extract file name
                        ###################
                        filename = os.path.basename(imgfile)
                        slide_layout = prs.slide_layouts[5]
                        slide = prs.slides.add_slide(slide_layout)
                        title=slide.shapes.title
                        title.text =filename
                        slide.shapes.title.text_frame.paragraphs[0].font.size=Pt(24)
                        top = Inches(2); left = Inches(1) ; height= Inches(5); width=Inches(7.5)
                        pic = slide.shapes.add_picture(imgfile, left=left, top=top, width=width, height=height)
                    prs.save(MyPPTOut)
                
                else:
            
                    #####################################################
                    #Load an Existing Presentation & Insert Images
                    #####################################################
                    prs = Presentation(MyPPTIn)

                    #noslides=len(prs.slides)
                    i=0
                    for slide in prs.slides:
                        if i >= noimages:
                            break
                        else:
                            for shape in slide.shapes:
                                if i >= noimages:
                                    break
                                else:
                                    if shape.shape_type == MSO_SHAPE.RECTANGLE and shape.text == "":
                                        #print(shape.shape_id);print(shape.name);print(shape.shape_type)
                                        imgfile=images[i]
                                        pic=slide.shapes.add_picture(imgfile, shape.left, shape.top, shape.width, shape.height)
                                        slide.shapes.element.remove(shape.element)
                                        i=i+1

                    prs.save(MyPPTOut)
                    
            except Exception as e:
                print('############################################################################\n');
                print ('Error: Problem running PowerPoint:\n' + str(e) + '\n');
                print('############################################################################\n');
                sys.exit(1);

def SPFEmail(MyAttachment, edistlist, esubject, ebody, etype):
    ##########################
    #Email Items
    ##########################
    try:    
        mypyexe=sys.executable #python exe
        myspfpath=""
        isSH =os.path.expandvars('%SHServer%')    #ScriptHost Environmenty Variable
        if isSH =='%SHServer%': # Local as Env Var SHServer is set on ScriptHost
            pos=mypyexe.lower().rfind("\\python3\\python.exe")
            if pos != -1:
                myspfpath=mypyexe[0:pos]
        else: #On ScriptHost
            myspfpath =os.path.expandvars('%temp%')
            
        if myspfpath != "":
            myspfpath=os.path.join(myspfpath,'SPFSQL3.py')
            myspffile='_spfemail.spfsql'
            #mydata='<OPTIONS>\n/WORKDIR=.\\\n/INSTANCE=9\n/OUTLOOK=' + etype.upper() + '\n/UTILITIES=@EXEDIR@\SQLPathFinder_Email.va "' + MyAttachment + '" "' + edistlist + '" "' + esubject + '" "' + ebody + '" "" "" "" "N" "' + etype.upper() + '"\n</OPTIONS>'
            mydata='<OPTIONS>\n/WORKDIR=.\\\n/INSTANCE=9\n/UTILITIES=@EXEDIR@\\SQLPathFinder_Email.va "' + MyAttachment + '" "' + edistlist + '" "' + esubject + '" "' + ebody + '" "" "" "" "N" "' + etype.upper() + '"\n</OPTIONS>'
            f= open(myspffile,"w")
            f.write(mydata) #Save SPF spfsql file
            f.close()      
            subprocess.call([mypyexe, myspfpath, '/SPFSQL=' + myspffile, '/SPFINSTANCE=9'])
    except Exception as e:
        print('############################################################################\n');
        print ('Error: Problem sending email:\n' + str(e) + '\n');
        print('############################################################################\n');
        sys.exit(1); 

if __name__ == "__main__":
    currdate = dt.datetime.today()
    print('\nStarting Email/PowerPoint Script, v3.1 ... ... ' + str(dt.datetime.today()) + '\n');
    ######################################
    # Get Input Arguments
    ######################################
    input_args = {};
    get_input_args(sys.argv[1:], input_args);
    
    if input_args['mode'] == "PPT":
        MyDefDir=os.path.dirname(os.path.realpath(__file__)).strip()
        AddPPT (input_args['src'], input_args['out'], input_args['img'], MyDefDir)
        
    elif input_args['mode'] == "EMAIL":
        try:
            files=os.listdir(input_args['img'])
            myimglist=""
            for file in files:
                splitf = os.path.splitext(file)
                myext="" 
                if len(splitf) == 2:
                    myext=splitf[1].upper().strip()
                    if (myext == ".PNG" or myext == ".BMP" or myext == ".GIF" or myext == ".JPG"):
                        myimglist = myimglist + "," + os.path.join(input_args['img'].replace("/","\\"),file)
            if myimglist != "":
                myimglist=myimglist[1:]
        except Exception as e:
            print('############################################################################\n');
            print ('Error: Problem organizing images to email:\n' + str(e) + '\n');
            print('############################################################################\n');
            sys.exit(1); 
        SPFEmail (myimglist, input_args['out'], input_args['esubj'], input_args['src'], input_args['etype'])
    elif input_args['mode'] == "EMAILQ":
        SPFEmail (input_args['img'], input_args['out'], input_args['esubj'], input_args['src'], input_args['etype'])
