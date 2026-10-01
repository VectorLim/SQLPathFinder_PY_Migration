import os
import datetime as dt
import pandas as pd
import sys
import re
from plotly import __version__
import plotly.express as px
##### New-Start
import plotly.graph_objects as go
from plotly.subplots import make_subplots
import math
##### New-End

class SPFPlotly:

    def __init__(self, my_dataframe):
        self.df = my_dataframe
        print ('  using Plotly version: ' + str(__version__) + '\n');
        pver=str(__version__).split(".")
        if (pver[0].isnumeric() and float(pver[0]) < 5.0) or (pver[0]=="5" and len(pver) >=2 and pver[1].isnumeric() and float(pver[1]) < 8.0):
                print('#################################################################################');
                print('This plotting class requires Plotly 5.8.0 or higher  to be installed. Exiting ...');
                print('#################################################################################\n');
                sys.exit(1);
        
    def get_color(self,name):
        ############################
        #Get Color Scheme
        ############################
        mycolor={
        'plotly':px.colors.qualitative.Plotly, 'd3':px.colors.qualitative.D3, 'g10':px.colors.qualitative.G10, 't10':px.colors.qualitative.T10
       ,'alphabet':px.colors.qualitative.Alphabet, 'dark24':px.colors.qualitative.Dark24, 'light24':px.colors.qualitative.Light24, 'set1':px.colors.qualitative.Set1
       ,'pastel1':px.colors.qualitative.Pastel1, 'dark2':px.colors.qualitative.Dark2, 'set2':px.colors.qualitative.Set2, 'antique':px.colors.qualitative.Antique
       ,'bold':px.colors.qualitative.Bold, 'pastel':px.colors.qualitative.Pastel, 'prism':px.colors.qualitative.Prism, 'safe':px.colors.qualitative.Safe
       ,'vivid':px.colors.qualitative.Vivid
       }
        if name in mycolor:
            return mycolor[name]
        else:
            return None
 
    def writehtm(self,chart,myHTM):
        ###########################
        #Write HTML to File
        ###########################
        if os.path.exists(chart['htmfile']):
            os.remove(chart['htmfile']) 
        f=open(chart['htmfile'], 'w')
        f.write(myHTM) 
        f.close()

    def get_htm(self,fig,chart):
        ########################################
        #Get Plotly HTML Code for a Chart Figure
        ########################################
        if chart['includeplotlyjs']=="Y":
            myincjs='directory'
        else:
            myincjs=False
        if chart['savetype']=='HTM':
            myhtmdata=fig.to_html(
              config=None
             ,auto_play=True
             ,include_plotlyjs=myincjs ##True, False'cdn','directory'
             ,full_html=False
             ,default_width=chart['width']    #E.g., 100%
             ,default_height=chart['height']  #E.g., 100%
             );
            if chart['includeplotlyjs']=="Y":            
                myhtmdata=myhtmdata.replace('plotly.min.js','https://dtdpathwebnlb.ch.intel.com/sqlpathfinder_reports/plotly/plotly.min.js')
        else:   #elif chart['savetype']=='PNG'
           myfile_tup=os.path.splitext(chart['htmfile'])
           #print("Image=", myfile_tup[0].strip() + '.' + chart['savetype'].lower()); #debug
           myimage=myfile_tup[0].strip() + '.' + chart['savetype'].lower()
           fig.write_image(myimage)
           myhtmdata='<table><tr class="tblout"><td class="tblout"><img src="' + myimage + '"></td></tr></table>'

        return myhtmdata;

 
    def get_min_max(self, mymin, mymax, my_list):
        ############################
        # Compute Min and Max Values
        # of a Var or Variable List
        #
        #Arguments:
        #---------
        #  mymin  : min value as string or automin or ""
        #  mymax  : max value string or automax or ""
        #  my_list: List of variables
        ############################
        min_val=None; max_val=None; compute_min=False; compute_max=False
        #print("min,max,list=", mymin, mymax); print(my_list) #Debug
        if mymin != None and mymax != None and mymin !='' and mymax != '':
            if mymin.lower() == 'automin':
               min_val0=None
               for y in my_list:                   
                   min_val0 = float(min(self.df[y]))
                   if min_val==None or min_val > min_val0:
                       min_val=min_val0
               compute_min=True
            elif mymin.replace('.','',1).isdigit():
               min_val = float(mymin)

            if mymax.lower() == 'automax':
               max_val0=None
               for y in my_list:                   
                   max_val0 = float(max(self.df[y]))
                   if max_val==None or max_val < max_val0:
                       max_val=max_val0
               compute_max=True
            elif mymax.replace('.','',1).isdigit():
               max_val = float(mymax)

            if compute_min:
                min_val = min_val - 0.1 * (max_val - min_val)
            if compute_max:
                max_val = max_val + 0.1 * (max_val - min_val)
            return min_val, max_val
            
    def assign_chart_variables_multiy(self, chart, df_bar_groups, df_line_groups):

        if chart['yinc'] != None and chart['yinc'] != '' and chart['yinc'].replace('.','').replace('-','').isdigit():
            pass
        else:
            chart['yinc'] = None

        if chart['y2inc'] != None and chart['y2inc'] != '' and chart['y2inc'].replace('.','').replace('-','').isdigit():
            pass
        else:
            chart['y2inc'] = None

        if chart['xinc'] != None and chart['xinc'] != '' and chart['xinc'].replace('.','').replace('-','').isdigit():
            pass
        else:
            chart['xinc'] = None

        chart['range_y']=None
        if chart['ymin'] != None and chart['ymin'] !='' and chart['ymax'] != None and chart['ymax'] != '':
            min_val,max_val=self.get_min_max(chart['ymin'], chart['ymax'], df_bar_groups)
            if min_val != None and max_val != None:
                chart['range_y']=[min_val,max_val]
 
        chart['range_y2']=None
        if chart['y2min'] != None and chart['y2min'] !='' and chart['y2max'] != None and chart['y2max'] != '':
            min_val,max_val=self.get_min_max(chart['y2min'], chart['y2max'], df_line_groups)
            if min_val != None and max_val != None:
                chart['range_y2']=[min_val,max_val]
        
        chart['range_x']=None
        my_list=[chart['colX']]
        if chart['xmin'] != None and chart['xmin'] !='' and chart['xmax'] != None and chart['xmax'] != '':
            min_val,max_val=self.get_min_max(chart['xmin'], chart['xmax'], my_list)
            if min_val != None and max_val != None:
                chart['range_x']=[min_val,max_val]

        chart['width']=None;chart['height']=None
        if chart['framex'] != None  and chart['framex'] !='':
            if chart['framex'].isdigit():
                chart['width']=int(chart['framex'])
            else:
                chart['width']=chart['framex']

        if chart['framey'] != None and chart['framey'] !='':
            if chart['framey'].isdigit():
                chart['height']=int(chart['framey'])
            else:
                chart['height']=chart['framey']


        chart['xrotatei']=-1
        if chart['xrotate'] !='' and chart['xrotate'].replace('-','',1).isdigit():
            chart['xrotatei']=int(chart['xrotate']);

        chart['yrotatei']=-1
        if chart['yrotate'] !='' and chart['yrotate'].replace('-','',1).isdigit():
            chart['yrotatei']=int(chart['yrotate']);

        chart['y2rotatei']=-1
        if chart['y2rotate'] !='' and chart['y2rotate'].replace('-','',1).isdigit():
            chart['y2rotatei']=int(chart['y2rotate']);

        if chart['template']=='':
            chart['template']=None

        if chart['facetc'] != None and chart['facetc'] != '' and chart['facetc'].replace('.','').isdigit():
            chart['facetc']=float(chart['facetc'])
        else:
            chart['facetc']=None

        if chart['facetr'] != None and chart['facetr']!='' and chart['facetr'].replace('.','').isdigit():
            chart['facetr']=float(chart['facetr'])
        else:
            chart['facetr']=None
            

    def add_reference_lines(self,fig, chart):
        #################################
        #Plot X/Y Reference lines. E.g.,:
        #
        # Arguments:
        #   fig  : Plotly Figure
        #   chart: Input Dictionary. chart['y_ref'] or chart['x_ref'] will be of format:
        #      25;Solid;Black;Label1
        #      50;Dashed;Red;Label2
        #################################

        if chart['y_ref']!='':
           tmplist1=chart['y_ref'].splitlines();
           for i,tmp1 in enumerate(tmplist1):
               if tmp1 != '':
                  tmplist2=tmp1.split(";")
                  if len(tmplist2)==4:
                     if tmplist2[1].lower()=='':
                        tmplist2[1]='solid'
                     if tmplist2[2].lower() == '':
                        tmplist2[2]='black'

                     fig.add_hline(y=float(tmplist2[0]), line_width=1, line_dash=tmplist2[1], line_color=tmplist2[2], annotation_text=tmplist2[3],annotation_position='top left',row='all',col='all')

        if chart['x_ref']!='':
           tmplist1=chart['x_ref'].splitlines();
           for i,tmp1 in enumerate(tmplist1):
               if tmp1 != '':
                  tmplist2=tmp1.split(";")
                  if len(tmplist2)==4:
                     if tmplist2[1].lower()=='':
                        tmplist2[1]='solid'
                     if tmplist2[2].lower() == '':
                        tmplist2[2]='black'

                     fig.add_vline(x=float(tmplist2[0]), line_width=1, line_dash=tmplist2[1], line_color=tmplist2[2], annotation_text=tmplist2[3],annotation_position='top left',row='all',col='all')

    def update_axes_multiy(self, fig, chart):
        #################################
        # Update Chart axes. E.g., grid
        # lines & axes
        #
        # Arguments:
        #   fig  : Plotly Figure
        #   chart: Input Dictionary
        #################################

        if chart['xtype'] != None and chart['xtype'] != '':
            fig.update_xaxes(type=chart['xtype'])

        if chart['ytype'] != None and chart['ytype'] != '':
            fig.update_yaxes(type=chart['ytype'], secondary_y=False )

        if chart['y2type'] != None and chart['y2type'] != '':
            fig.update_yaxes(type=chart['y2type'], secondary_y=True )

        if chart['grid_x']=='0':
            fig.update_xaxes(showgrid=False)
        else:
            fig.update_xaxes(showgrid=True)

        if chart['grid_y']=='0':
            fig.update_yaxes(showgrid=False)
        else:
            fig.update_yaxes(showgrid=True)

        fig.update_xaxes(showticklabels=True)
        fig.update_yaxes(showticklabels=True, secondary_y=False )
        fig.update_yaxes(showticklabels=True, secondary_y=True )
        #fig.update_yaxes(matches=None, secondary_y=False )  #Autoscale Y-axes
        #fig.update_yaxes(matches=None, secondary_y=True )   #Autoscale Y2-axes
        if chart['xinc'] != None:
            fig.update_xaxes(dtick=chart['xinc'] )

        if chart['xrotatei'] != -1:
            fig.update_xaxes(tickangle = chart['xrotatei'])

        if chart['xtickformat']!='':
            fig.update_xaxes(tickformat = chart['xtickformat'])

        if chart['yinc'] != None:
            fig.update_yaxes(dtick=chart['yinc'], secondary_y=False )

        if chart['yrotatei'] != -1:
            fig.update_yaxes(tickangle = chart['yrotatei'], secondary_y=False )

        if chart['ytickformat']!='':
            fig.update_yaxes(tickformat = chart['ytickformat'], secondary_y=False )

        if chart['y2inc'] != None:
            fig.update_yaxes(dtick=chart['y2inc'], secondary_y=True )

        if chart['y2rotatei'] != -1:
            fig.update_yaxes(tickangle = chart['y2rotatei'], secondary_y=True )

        if chart['y2tickformat']!='':
            fig.update_yaxes(tickformat = chart['y2tickformat'], secondary_y=True )
             
        if chart['x-sort']!= None and chart['x-sort']!='':
            fig.update_xaxes(categoryorder=chart['x-sort']) #E.g., total descending or category ascending


    def add_legend(self, fig, chart):
        #################################
        # Update Plotly Legend
        #
        # Arguments:
        #   fig  : Plotly Figure
        #   chart: Input Dictionary
        #################################

        chart['legend_show_o']=None; chart['legend_color_o']=None; chart['legend_title_o']=None; chart['legend_y_o']=None; chart['legend_x_o']=None 
        chart['legend-orient']=None; chart['legend-bordercolor']=None; chart['legend-borderwidth']=None; chart['yanchor']=None; chart['xanchor']=None

        if chart['legend_show']=='TRUE':
           chart['legend_show_o']=True
           if chart['legend_color'] != '' and chart['legend_color'].lower() != 'transparent':
               chart['legend_color_o']=chart['legend_color']
               chart['legend-borderwidth']=1
               chart['legend-bordercolor']='Black'

           if chart['legend_title'] != '':
               chart['legend_title_o']=chart['legend_title']

           if chart['legend_pos'] == 'TOP':
               chart['legend-orient']='h'
               chart['yanchor']='top'
               chart['xanchor']='left'
               chart['legend_y_o']=1.5
           elif  chart['legend_pos'] == 'BOTTOM':
               chart['legend-orient']='h'
               chart['yanchor']='bottom'
               chart['xanchor']='left'
               chart['legend_y_o']=-.7
           elif  chart['legend_pos'] == 'LEFT':
               chart['legend-orient']='v'
               chart['yanchor']='bottom'
               chart['xanchor']='left'
               chart['legend_y_o']=.5
               chart['legend_y_o']=-.14

           if chart['legend_x'] != '' and chart['legend_x'].replace('-','',1).replace('.','',1).isdigit():
              chart['legend_x']=float(chart['legend_x']);               
           if chart['legend_y'] != '' and chart['legend_y'].replace('-','',1).replace('.','',1).isdigit():
              chart['legend_y']=float(chart['legend_y']);               

        else:
           chart['legend_show_o']=False

        fig.update_layout(showlegend=chart['legend_show_o'], legend_title_text=chart['legend_title_o'], legend=dict(yanchor=chart['yanchor'], y=chart['legend_y_o'], xanchor=chart['xanchor'], x=chart['legend_x_o'], orientation=chart['legend-orient'], bgcolor=chart['legend_color_o'], bordercolor=chart['legend-bordercolor'], borderwidth=chart['legend-borderwidth']))
        
            
    def set_x_type(self, chart):
        #################################
        #Set X Type
        #################################
        chart['colX0']=None;chart['colX1']=None
        if chart['xtype']=='category':
            self.df[chart['colX']]=self.df[chart['colX']].map(str)
        elif chart['xtype']=='multicategory':
            _myxl=chart['colX'].split(",")
            chart['colX0']=_myxl[0]
            chart['colX1']=_myxl[1] 
            self.df[chart['colX0']]=self.df[chart['colX0']].map(str)
            self.df[chart['colX1']]=self.df[chart['colX1']].map(str)
        elif chart['xtype']=='date':
            self.df[chart['colX']]= pd.to_datetime(self.df[chart['colX']]) 


    def get_custom_theme(self, chart):
        ######################################
        #Parse Custom Themes
        #
        # Arguments:
        # ---------
        #  Chart : Input arguments. E.g.,
        #    chart['customtheme']= '#FF8080','#FF0000'
        #                      'circle','square'
        ######################################
        chart['colorseq']=None ; chart['symbolseq']=None; chart['linetypeseq']=None;
        #print('Custom Theme=' +chart['customtheme']) #debug
        if chart['customtheme'] != None and chart['customtheme'] !='' and chart['customtheme'] !='N/A':
            chart['customtheme']=chart['customtheme'].replace("'","")
            tL=chart['customtheme'].split('\n')
            for idx in tL:
                #print('Element=' + idx) #Debug
                if (idx+ '    ').upper()[0:4] == 'COL=':
                   chart['colorseq']=idx[4:].strip().split(",")      
                elif (idx+ '    ').upper()[0:4] == 'SYM=':
                   chart['symbolseq']=idx[4:].strip().split(",")      
                elif (idx+ '    ').upper()[0:4] == 'LTY=':
                   chart['linetypeseq']=idx[4:].strip().split(",")      
        #print(chart['colorseq']); print(chart['symbolseq']); print(chart['linetypeseq']) #debug

    ##### New-Start
    def get_multi_y_vars (self, my_y_str, mycolumns_list):
        #####################################
        #Get Y Variables
        #
        #ARGUMENTS:
        #---------
        # my_y_str      : Comma delimited list of Y variable or Y variable patterns preceded by startingwith:, endingwith: or containing:
        # mycolumns_list: List of Columns in DF
        #
        #RETURNS:
        #-------
        # List with Y Variables
        #####################################
        df_groups=[]
        my_y_list=my_y_str.split(",")
        for my_y in my_y_list:
            my_y=my_y.strip()
            if my_y != "":
                if (my_y.lower() + "             ")[0:13]=="startingwith:" or (my_y.lower() + "           ")[0:11]=="endingwith:" or (my_y.lower() + "           ")[0:11]=="containing:":
                    if (my_y.lower() + "             ")[0:13]=="startingwith:":
                        pattern="^" + my_y[13:].strip()
                    elif (my_y.lower() + "           ")[0:11]=="endingwith:":
                        pattern=my_y[11:].strip() + "$"
                    else: #Containing
                        pattern=my_y[11:].strip() 
                    for y in mycolumns_list:
                        res=re.search(pattern, y)
                        #print("res="); print(res) #Debug
                        if res != None:
                            df_groups.append(y)
                else:
                    df_groups.append(my_y)
        return df_groups

    def process_y_vars_multi_y (self, type, my_y, chart):
        ######################################
        #Separate Y variables for Multi-Y Plot
        #
        # Arguments:
        #   type : bar or line for type of variables to separate
        #   my_y : Y variables
        #   chart: Input Dictionary
        #   E.g.,: my_y='StartingWith:bar -> Data, EndingWith:bar -> Data, Containing:bar -> Data,PROCESS_TIME -> Data,QUEUE_TIME -> Size,PROCESS_TIME -> Text'
        #
        #  Sample Invocation:
        #    process_y_vars_multi_y("bar",chart['colY'], chart)
        #################################
        #print(type); print(my_y) #Debug

        chart[type+'_size']=None ; #chart[type+'_text']=None; chart[type+'_hovername']=None; chart[type+'_hoverdata']=None;
        yLOuter=my_y.split(',')
        if type=="bar":
           chart['colY']=""
        else:
           chart['color']=""

        for yo in yLOuter:
            #print(yo)                                #Debug
            yLInner=yo.split('->')
            ytype=yLInner[1].strip().upper()
            #print(ytype); print(yLInner[0].strip()); #Debug
            if ytype=='DATA':
               if type=="bar":
                   if chart['colY']=="":
                       chart['colY']=yLInner[0].strip()
                   else:
                       chart['colY']=chart['colY'] + "," + yLInner[0].strip()
               else: #line
                   if chart['color']=="":
                       chart['color']=yLInner[0].strip()
                   else:
                       chart['color']=chart['color'] + "," + yLInner[0].strip()
            elif ytype=='SIZE':
               chart[type+'_size']=yLInner[0].strip()
            #elif ytype=='TEXT':
            #   chart[type+'_text']=yLInner[0].strip()
            #elif ytype=='HOVERNAME':
            #   chart[type+'_hovername']=yLInner[0].strip()
            #elif ytype=='HOVERDATA':
            #   chart[type+'_hoverdata']=[yLInner[0].strip()]

        if chart[type+'_size'] != None:
           self.df[chart[type+'_size']]=pd.to_numeric(self.df[chart[type+'_size']],errors='coerce')
           self.df[chart[type+'_size']] = self.df[chart[type+'_size']].fillna(0)
        #print(chart['colY']) #debug
        #print(chart['color']) #debug

    def process_data_labels(self, chart, mylabel):
    ########################################
    #Process Data Labels. How used:
    #         ,textposition="inside"
    #         ,texttemplate = '%{text:.2f}'
    #         ,textfont_size=2
    #
    # Arguments:
    # ---------
    # Chart   : Chart config dictionary
    # mylabel : Data Label. E.g., "inside,2f,4" with position, format, size
    #
    # Returns:
    #   position,format,size
    ########################################
        labelpos=None; labelformat=None; labelsize=None
        if mylabel != None and mylabel != "" and mylabel.upper() != "NONE":
            mylabel_list=mylabel.split(",")
            l=len(mylabel_list)
            if l >= 1 and mylabel_list[0].strip() != "":
                labelpos=mylabel_list[0].strip()                                               #e.g., inside or outside
            if l >= 2 and mylabel_list[1].strip() != "":
                labelformat="%{text:" + mylabel_list[1].strip() + "}"                          #e.g., %{text:.2f}
            if l >= 3 and mylabel_list[2].strip() != "" and mylabel_list[2].strip().isdigit():
                labelsize=int(mylabel_list[2].strip())                                         #e.g., 10
        return labelpos,labelformat,labelsize


    def multi_y(self, chart):
        ###########################
        #Plot Bar and Scatter on Two
        #Y-axes with a common X-Axis
        #
        #ARGUMENTS:
        #---------
        # Chart : Chart Dictionary of Variables
        ############################

        #########################
        #Set DF columns lowercase
        #########################
        if chart['colcase'] == '1':
            chart['colX']=chart['colX'].lower()
            chart['colY']=chart['colY'].lower()
            chart['color']=chart['color'].lower()
            chart['by']=chart['by'].lower()

        #########################
        #Add -> Data if line var
        #only contains a variable
        #########################
        if chart['color'] != "" and chart['color'].find("-> Data") == -1:
            chart['color']= chart['color'] + " -> Data"

        #chart['color_discrete_sequence']=None
        self.get_custom_theme(chart)
        #print(chart['colorseq']) #Debug Color sequence
        #print(chart['symbolseq']) #Debug Line symbol sequence
        #print(chart['linetypeseq']) #Debug Line Type sequence

        ##########################################################
        #Create BY Groups and calculate Number of Rows and Columns
        ##########################################################
        df_by_groups=None; no_by_groups=0; rows=1
        cols=int(chart['ncol'])
        if chart['by'] != "":
            df_by_groups=list(self.df[chart['by']].drop_duplicates().sort_values(ascending=True))
            no_by_groups=len(df_by_groups)
            if no_by_groups < cols:
                cols = no_by_groups
            rows=math.ceil( float(no_by_groups) / float(cols) )
        else:
            rows=1;cols=1

        #######################################
        #Separate out Variables from the Y cols
        #######################################
        self.process_y_vars_multi_y ("bar" ,chart['colY'] , chart)  #separate Y Variables for BARs
        self.process_y_vars_multi_y ("line",chart['color'], chart)  #separate Y Variables for LINEs
        #print("Y vars=", chart['colY'], chart['color']) #Debug

        ###################################
        #Determine bar and line y variables
        ###################################
        df_bar_groups=self.get_multi_y_vars(chart['colY'], self.df.columns)
        no_bar_groups=len(df_bar_groups)
        df_line_groups=self.get_multi_y_vars(chart['color'], self.df.columns)
        no_line_groups=len(df_line_groups)

        #print("chart['colY']=" + chart['colY'])        #Debug
        #print("df_bar_groups=");print(df_bar_groups)   #Debug
        #print("chart['color']=" + chart['color'])      #Debug
        #print("df_line_groups=");print(df_line_groups) #Debug
          

        #######################################
        #Determine Color Dictionary (df_colors)
        #######################################
        df_colors={}
        if chart['colorseq']==None or chart['colorseq']=="":
            colors=['#3366CC','#DC3912','#FF9900','#109618','#990099','#3B3EAC','#0099C6','#DD4477','#66AA00','#B82E2E','#316395','#994499','#22AA99','#AAAA11','#6633CC','#E67300','#8B0707','#329262','#5574A6','#3B3EAC','#CD950C','#00CD00','#EE30A7','#8B6508','#9ACD32','#36648B','#548B54','#8B7500','#006400','#8B4C39']
        else:
            colors=chart['colorseq']
        #print("colors="); print(colors) #Debug
        
        j=0
        for i in range(no_bar_groups):
            if j >= len(colors):
                j=0
            df_colors["b_" + df_bar_groups[i]]=colors[j]
            j=j+1
        for i in range(no_line_groups):
            if j >= len(colors):
                j=0
            df_colors["l_" + df_line_groups[i]]=colors[j]
            j=j+1

        #print("df_colors="); print(df_colors)          #Debug

        #######################################
        #Get X/Y Ranges ...
        #######################################
        self.assign_chart_variables_multiy(chart, df_bar_groups, df_line_groups)


        self.set_x_type(chart)                 #Set the type of X-axis
            
        ################################################
        #Create Subplots and subplot titles
        ################################################
        #fig = make_subplots(rows=2, cols=2, specs=[
        # [{"secondary_y": True},{"secondary_y": True}]
        #,[{"secondary_y": True},{"secondary_y": True}]
        #]
        #,subplot_titles=("Plot 1", "Plot 2", "Plot 3", "Plot 4")
        #)
        ################################################

        subplot=[]
        subplot_cols=[]
        for c in range(cols):
            subplot_cols.append({"secondary_y": True})
        for r in range(rows):
            subplot.append(subplot_cols)

             
        mylist=[]; i=0
        for r in range(rows):
            for c in range(cols):
                if i < no_by_groups:
                    mylist.append(chart['by'] + "=" + df_by_groups[i])
                i=i+1
        fig = make_subplots(rows=rows, cols=cols, specs=subplot, subplot_titles=tuple(mylist), shared_xaxes='all', shared_yaxes='all'
        ,horizontal_spacing=chart['facetc'] ,vertical_spacing=chart['facetr']
        )


        #print("df=");print(df)                          #Debug
        #print("df_by_groups=");print(df_by_groups)      #Debug
        #print("no_by_groups=", str(no_by_groups))       #Debug
        #print("df_bar_groups="); print(df_bar_groups)   #Debug
        #print("df_line_groups="); print(df_line_groups) #Debug
        #print("df_colors="); print(df_colors)           #Debug
        #print("No Cols = " + str(cols));                #Debug
        #print("No Rows = " + str(rows));                #Debug

        #################################
        #Set Y-Marker for bubble chart or
        #a constant size
        #################################
        chart['ymarker']=None
        if chart['line_size'] != None:
            chart['ymarker']=chart['line_size']
        else:
            if chart['marker'] != '' and chart['marker'].isdigit():
                chart['ymarker']=int(chart['marker'])
            
        i=0
        legendshow=True
        if chart['lineopacity']==None or chart['lineopacity']=="":
           lineopacity=None
        else:
           lineopacity = float(chart['lineopacity'])
           
        barlabel_pos,barlabel_format,barlabel_size    = self.process_data_labels(chart, chart['barlabels'])
        linelabel_pos,linelabel_format,linelabel_size = self.process_data_labels(chart, chart['linelabels'])
        #print("Bar Labels="); print(barlabel_pos); print(barlabel_format); print(barlabel_size)     #Debug
        #print("Line Labels="); print(linelabel_pos); print(linelabel_format); print(linelabel_size) #Debug
        for r in range(rows):
            for c in range(cols):
                if (i < no_by_groups or no_by_groups == 0):

                    if chart['by'] != "":
                        b=df_by_groups[i]
                        dfb = self.df[self.df[chart['by']]==b]
                    else:
                        dfb=self.df
                    if chart['multi-x']=="Y":
                        _myX=[dfb[chart['colX0']], dfb[chart['colX1']]]
                    else:
                        _myX=dfb[chart['colX']]
                    #print(_myX) #Debug
                                                
                    for t in df_bar_groups:
                        #print(t)  #Debug
                        mylabels=None
                        if barlabel_pos != None:
                            mylabels=dfb[t]
                        fig.add_trace(
                        go.Bar(
                            x=_myX #dfb[chart['colX']]
                           ,y=dfb[t]
                           ,name=t
                           ,marker=dict(
                            color=df_colors["b_" + t]
                            )
                           ,showlegend=legendshow
                           ,text=mylabels
                           ,textposition=barlabel_pos
                           ,texttemplate=barlabel_format
                           ,textfont_size=barlabel_size
                        )
                           ,row=r+1,col=c+1
                           ,secondary_y=False
                        )
                    fig.update_layout(barmode=chart['chartopt0'])
                    
                    L=-1  
                    for t in df_line_groups:
                        mylabels=None
                        if linelabel_pos != None:
                            mylabels=dfb[t]
                        L=L+1
                        line_sym=None; line_type=None
                        if chart['symbolseq'] != None and len(chart['symbolseq']) >= L:
                            line_sym=chart['symbolseq'][L]
                        if chart['linetypeseq'] != None and len(chart['linetypeseq']) >= L:
                            line_type=chart['linetypeseq'][L]
                        #print("Line Symbols="); print(str(L));  print(line_sym);print(chart['symbolseq']) #Debug
                        #print("Line Type="); print(str(L)); print(line_type);print(chart['linetypeseq']) #Debug
                        
                        fig.add_trace(
                         go.Scatter(
                            x=_myX #dfb[chart['colX']]
                           ,y=dfb[t]
                           ,name=t
                           ,mode=chart['linemode']
                           ,line_color=df_colors["l_" + t]
                           ,opacity=lineopacity
                           ,showlegend=legendshow
                           ,marker_size=chart['ymarker']
                           ,marker_symbol=line_sym
                           ,line_dash=line_type
                           ,text=mylabels
                           ,textposition=linelabel_pos
                           ,texttemplate = linelabel_format
                           ,textfont_size=linelabel_size
                           #,marker=dict(size=7, color="red", symbol='circle-open'),
                         )
                           ,row=r+1, col=c+1
                           ,secondary_y=True
                         )
                legendshow=False
                i=i+1

        self.update_axes_multiy(fig, chart)      #Update grid & axes format
        self.add_reference_lines(fig, chart)     #Add any reference lines
        self.add_legend(fig, chart)              #Update Legend

        mycat="category"
        if chart['multi-x'] =="Y":
            mycat="multicategory"

        if chart['x-sort'] != None and chart['x-sort'] != '':
            fig.update_xaxes(type=mycat,categoryorder=chart['x-sort']) #E.g., total descending or total ascending

        if chart['x-sort'] != None and chart['x-sort']=="category ascending":
            if chart['multi-x'] =="N": 
                x_list=list(self.df[chart['colX']].drop_duplicates().sort_values(ascending=True))
            else:
                #x_list=list(self.df[chart['colX1']].drop_duplicates().sort_values(ascending=True))
                #print(self.df[[chart['colX0'],chart['colX1']]]) #Debug
                #print(self.df[[chart['colX0'],chart['colX1']]].drop_duplicates()) #Debug 
                #print(self.df[[chart['colX0'],chart['colX1']]].drop_duplicates().sort_values(by = [chart['colX0'],chart['colX1']], ascending = [True, True])) #Debug                
                _df=self.df[[chart['colX0'],chart['colX1']]].drop_duplicates().sort_values(by = [chart['colX0'],chart['colX1']], ascending = [True, True])
                x_list=[_df[chart['colX0']],_df[[chart['colX1']]]]                
                #print(x_list) #Debug 
            fig.update_xaxes(type=mycat,categoryorder='array', categoryarray= x_list)       #'linear'|'log'|'date'|'category'|'multicategory'

        if chart['x-sort'] != None and chart['x-sort']=="category descending":
            if chart['multi-x'] =="N":
                x_list=list(self.df[chart['colX']].drop_duplicates().sort_values(ascending=False))
            else:
                #x_list=list(self.df[chart['colX1']].drop_duplicates().sort_values(ascending=False))
                _df=self.df[[chart['colX0'],chart['colX1']]].drop_duplicates().sort_values(by = [chart['colX0'],chart['colX1']], ascending = [False, False])
                x_list=[_df[chart['colX0']],_df[[chart['colX1']]]]                
            fig.update_xaxes(type=mycat,categoryorder='array', categoryarray= x_list)       #'linear'|'log'|'date'|'category'|'multicategory'

        #################
        #Add X-axis Title
        #################
        if chart['xlabel'] == None or chart['xlabel'] == '':
            chart['xlabel']=chart['colX']
            
        if chart['xlabel'] != '':
            fig.update_xaxes(
                title_text=chart['xlabel']
#               ,showticklabels=True
#           ,ticktext=x_list
#           ,tickvals=x_list
#           ,tickangle = 45
#           ,title_font=dict(size=18, family='Courier', color='crimson'))
#           ,title_standoff = 25
#           ,showticklabels=False
#           ,ticklabelstep=2
#           ,nticks=20,tick0=0.25, dtick=0.5
            )
 
        ##################
        #Add Y1-axis Title
        ##################
        if chart['ylabel'] == None or chart['ylabel'] == '':
            noy=len(df_bar_groups)
            if noy >= 1:
                chart['ylabel']=df_bar_groups[0]
            if noy >= 2:
                chart['ylabel']=chart['ylabel'] + ", " + df_bar_groups[1]
            if noy >= 3:
                chart['ylabel']=chart['ylabel'] + "..."
        
        if chart['ylabel'] != '':
            fig.update_yaxes(title_text=chart['ylabel'],secondary_y=False)

        if chart['range_y'] != None:
            fig.update_yaxes(range=chart['range_y'],secondary_y=False)
            
        ##################
        #Add Y2-axis Title
        ##################     
        if chart['y2label'] == None or chart['y2label'] == '':
            noy=len(df_line_groups)
            if noy >= 1:
                chart['y2label']=df_line_groups[0]
            if noy >= 2:
                chart['y2label']=chart['y2label'] + ", " + df_line_groups[1]
            if noy >= 3:
                chart['y2label']=chart['y2label'] + "..."

        if chart['y2label'] != '':
            fig.update_yaxes(title_text=chart['y2label'], secondary_y=True)

        if chart['range_y2'] != None:
            fig.update_yaxes(range=chart['range_y2'], secondary_y=True)

        #################
        #Add Figure Title
        #################
        if chart['title']==None or chart['title']=="":
           chart['title']='Plot of ' + chart['ylabel'] + ' and ' + chart['y2label'] + ' vs. ' + chart['colX']
           if chart['by'] != '':
               chart['title']=chart['title'] + ' by ' + chart['by']

        if chart['title'] !='':
            fig.update_layout(
                title_text=chart['title']
            #   ,tickmode="sync"
            )
            
        if chart['template'] != None and chart['template'] != "":
            fig.update_layout(template=chart['template'])

        if chart['width'] != None and chart['width'] != '' and chart['height'] != None and chart['height'] != '' and str(chart['width']).find("%") == -1 and str(chart['height']).find("%") == -1:
            fig.update_layout(
                height=chart['height']
               ,width=chart['width'] 
            )
 
        #################
        #HoverMode
        #################
        hovermode=None
        if chart['hovermode']==None or chart['hovermode'].upper() == "NONE" or chart['hovermode'] == "":
            pass
        else:
            hovermode=chart['hovermode']
        
        fig.update_traces(hovertemplate=None)
        fig.update_layout(hovermode=hovermode)
        
        return fig
    ##### New-End




    def plot(self,chart_type, chart):
        ####################################
        #Create Plotly Plot & return HTML script
        #
        # ARGUMENTS:
        # ---------
        # chart_type : Type of Chart. E.g.,BOXPLOT
        # chart      : Chart Input Arguments
        ####################################

        if chart_type == 'BOXPLOT':
            myoutfig=self.boxplot(chart); 
        elif chart_type == 'BARCHART':
            myoutfig=self.barchart(chart); 
        elif chart_type == 'SCATTER':
            myoutfig=self.scatter(chart); 
        elif chart_type == 'LINECHART':
            myoutfig=self.linechart(chart); 
        elif chart_type == 'DENSITY_HEATMAP':
            myoutfig=self.density_heatmap(chart); 
        ##### New-Start
        elif chart_type == 'MULTI_Y':
            myoutfig=self.multi_y(chart); 
        ##### New-End

        return self.get_htm(myoutfig,chart) #html script

