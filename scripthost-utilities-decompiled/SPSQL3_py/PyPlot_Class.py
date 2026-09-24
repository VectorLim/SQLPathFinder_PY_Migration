import os
import datetime as dt
import pandas as pd
import sys
import re
from plotly import __version__
import plotly.express as px

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

    def assign_chart_variables(self, chart):
        ####################################
        #Assign/Verify Chart Input Variables
        ####################################

        if 'colYTitle' in chart:
           yt=chart['colYTitle']
        else:
           yt=chart['colY']

        if chart['title']=='':
           chart['title']='Plot of ' + yt + ' vs. ' + chart['colX']
           if chart['by'] != '':
               chart['title']=chart['title'] + ' by ' + chart['by']

        if chart['color']=='':
            chart['color']=None

        chart['ncoli']=None
        if chart['by'] =='':
            chart['by']=None
        else:
            if chart['ncol'] !='' and chart['ncol'].isdigit():
                chart['ncoli']=int(chart['ncol'])

        ############################################################################
        #set X and Y labels
        ############################################################################
        chart['labels']=None
        if chart['xlabel'] != '' or chart['ylabel'] != '':
            chart['labels']={}
            if chart['xlabel'] != '':
                chart['labels'][chart['colX']] = chart['xlabel']
            if chart['ylabel'] != '':
                chart['labels'][chart['colY']] = chart['ylabel']

        ############################################################################
        #If there is no assigned Y label but Y was transformed, set the Y Label
        ############################################################################
        if chart['ylabel'] == '' and 'colYTitle' in chart:
            if chart['labels']==None:
                chart['labels']={}
            chart['labels'][chart['colY']] = chart['colYTitle']

        if chart['chartopt0']=='':
            chart['chartopt0']=None

        if chart['yinc'] != '' and chart['yinc'].replace('.','').replace('-','').isdigit():
            pass
        else:
            chart['yinc'] = None

        if chart['xinc'] != '' and chart['xinc'].replace('.','').replace('-','').isdigit():
            pass
        else:
            chart['xinc'] = None

        chart['range_y']=None

        if chart['ymin'] !='' and chart['ymax'] != '':
            min_val=None; max_val=None; compute_min=False; compute_max=False
            if chart['ymin'].lower() == 'automin':
               min_val = float(min(self.df[chart['colY']]))
               compute_min=True
            elif chart['ymin'].replace('.','',1).isdigit():
               min_val = float(chart['ymin'])

            if chart['ymax'].lower() == 'automax':
               max_val = float(max(self.df[chart['colY']]))
               compute_max=True
            elif chart['ymax'].replace('.','',1).isdigit():
               max_val = float(chart['ymax'])

            if compute_min:
                min_val = min_val - 0.05 * (max_val - min_val)
            if compute_max:
                max_val = max_val + 0.05 * (max_val - min_val)

            if min_val != None and max_val != None:
                chart['range_y']=[min_val,max_val]
    
        chart['range_x']=None

        if chart['xmin'] !='' and chart['xmax'] != '':
            min_val=None; max_val=None; compute_min=False; compute_max=False
            if chart['xmin'].lower() == 'automin':
               min_val = float(min(self.df[chart['colX']]))
               compute_min=True
            elif chart['xmin'].replace('.','',1).isdigit():
               min_val = float(chart['xmin'])

            if chart['xmax'].lower() == 'automax':
               max_val = float(max(self.df[chart['colX']]))
               compute_max=True
            elif chart['xmax'].replace('.','',1).isdigit():
               max_val = float(chart['xmax'])

            if compute_min:
                min_val = min_val - 0.05 * (max_val - min_val)
            if compute_max:
                max_val = max_val + 0.05 * (max_val - min_val)

            if min_val != None and max_val != None:
                chart['range_x']=[min_val,max_val]



        chart['color_discrete_sequence']=None     
        if chart['color_scheme'] !='':
            chart['color_discrete_sequence']=self.get_color(chart['color_scheme'])

        chart['width']=None;chart['height']=None
        if chart['framex'] !='' and chart['framex'].isdigit():
            chart['width']=int(chart['framex'])

        if chart['framey'] !='' and chart['framey'].isdigit():
            chart['height']=int(chart['framey'])
 
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

        if chart['chartmode']=='':
            chart['chartmode']=None

        if chart['facetc']!='' and chart['facetc'].replace('.','').isdigit():
            chart['facetc']=float(chart['facetc'])
        else:
            chart['facetc']=None

        if chart['facetr']!='' and chart['facetr'].replace('.','').isdigit():
            chart['facetr']=float(chart['facetr'])
        else:
            chart['facetr']=None

        chart['dltext']=None; chart['dlloc']=None
        if 'datalabelfmt' in chart and chart['datalabelfmt']!='' and chart['datalabelfmt'].find(',') != -1:
            dl=chart['datalabelfmt'].split(',')
            if dl[0].strip() != '':
                chart['dlloc']=dl[0].strip().lower() #E.g., inside or outside
            if dl[1].strip() != '':
                chart['dltext']= '%{text:' + dl[1].strip().lower() + '}' #e.g., %{text:.2f}
            
            

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

    def update_axes(self, fig, chart):
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
            fig.update_yaxes(type=chart['ytype'])

        if chart['grid_x']=='0':
            fig.update_xaxes(showgrid=False)
        else:
            fig.update_xaxes(showgrid=True)

        if chart['grid_y']=='0':
            fig.update_yaxes(showgrid=False)
        else:
            fig.update_yaxes(showgrid=True)

        if chart['marker'] != '' and chart['marker'].isdigit():
            fig.update_traces(marker_size=int(chart['marker']))

        fig.update_xaxes(showticklabels=True)
        fig.update_yaxes(showticklabels=True)
        fig.update_yaxes(matches=None)  #Autoscale Y-axes

        if chart['yinc'] != None:
            fig.update_yaxes(dtick=chart['yinc'] )

        if chart['xinc'] != None:
            fig.update_xaxes(dtick=chart['xinc'] )

        if chart['xrotatei'] != -1:
            fig.update_xaxes(tickangle = chart['xrotatei'])

        if chart['yrotatei'] != -1:
            fig.update_yaxes(tickangle = chart['yrotatei'])

        if chart['xtickformat']!='':
            fig.update_layout(xaxis_tickformat = chart['xtickformat'])

        if chart['ytickformat']!='':
            fig.update_layout(yaxis_tickformat = chart['ytickformat'])

        if chart['dltext'] != None or chart['dlloc'] != None:
             fig.update_traces(texttemplate=chart['dltext'], textposition=chart['dlloc'])
             
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

    def summarize_bardata(self,chart):

        chart['text']=None; chart['hovername']=None; chart['hoverdata']=None; chart['pattern_shape']=None
        is_summary=False

        yL=chart['colY'].split(',')
        #print(yL); #Debug
        for yo in yL:
            #print(yo)  #debug
            if yo.find("->") != -1:
                yLInner=yo.split('->')
                ytype=yLInner[1].strip().upper()
                #print(ytype); print(yLInner[0].strip()); #debug
                if ytype=='TEXT':
                   chart['text']=yLInner[0].strip()
                elif ytype=='HOVERNAME':
                   chart['hovername']=yLInner[0].strip()
                elif ytype=='HOVERDATA':
                   chart['hoverdata']=[yLInner[0].strip()]
                elif ytype=='PATTERN-SHAPE':
                   chart['pattern_shape']=yLInner[0].strip()
                elif ytype=='DATA':
                   chart['colY']=yLInner[0].strip()
                else:
                    yC=yLInner[0].strip()
                    yF=yLInner[1].strip()
                    is_summary=True
            else:
                self.df[chart['colY']] = self.df[yo].astype(float)
                chart['colY']=yo

        if is_summary:
            if chart['by']=='' and chart['color']=='':
                grpx=chart['colX']
            else:
                grpx=[]
                if chart['by']!='':
                    grpx.append(chart['by'])
                if chart['color']!='' and chart['color'] != chart['by']:
                    grpx.append(chart['color'])
                if chart['colX'] != chart['by'] and chart['colX'] != chart['color']: #Make sure X is not a duplicate of 'color' or 'by' column
                    grpx.append(chart['colX'])

            if yF !="count":
                self.df[yC] = self.df[yC].astype(float)

            self.df=self.df.groupby(grpx,as_index=False).agg({ yC : [yF] })
            self.df.columns = self.df.columns.get_level_values(0)
            #print(self.df.columns);

            chart['colY']=yC;
            chart['colYTitle']=yF+'(' +yC+')'
        else:
            self.df[chart['colY']] = self.df[chart['colY']].astype(float)
            
    def set_x_type(self, chart):
        #################################
        #Set X Type
        #################################
        if chart['xtype']=='category':
            self.df[chart['colX']]=self.df[chart['colX']].map(str)
        elif chart['xtype']=='date':
            self.df[chart['colX']]= pd.to_datetime(self.df[chart['colX']]) 

    def process_y_vars (self,chart):
        ######################################
        #Separate Y variables for Scatter Plot
        #& extract the Data, Size, Label, & Symbol vars
        #
        # Arguments:
        #   chart: Input Dictionary
        #   E.g.,: chart['colY']='PROCESS_TIME -> Data,QUEUE_TIME -> Size,ENTITY_TYPE -> Symbol,PROCESS_TIME -> Text'
        #################################

        chart['size']=None ; chart['symbol']=None; chart['text']=None; chart['hovername']=None; chart['hoverdata']=None; chart['linedash']=None; chart['linegroup']=None

        yLOuter=chart['colY'].split(',')
        for yo in yLOuter:
            #print(yo)  #debug
            yLInner=yo.split('->')
            ytype=yLInner[1].strip().upper()
            #print(ytype); print(yLInner[0].strip()); #debug
            if ytype=='DATA' or ytype=='Y':
               chart['colY']=yLInner[0].strip()
            elif ytype=='SIZE':
               chart['size']=yLInner[0].strip()
            elif ytype=='SYMBOL':
               chart['symbol']=yLInner[0].strip()
            elif ytype=='TEXT':
               chart['text']=yLInner[0].strip()
            elif ytype=='HOVERNAME':
               chart['hovername']=yLInner[0].strip()
            elif ytype=='HOVERDATA':
               chart['hoverdata']=[yLInner[0].strip()]
            elif ytype=='LINEDASH':
               chart['linedash']=yLInner[0].strip()
            elif ytype=='LINEGROUP':
               chart['linegroup']=yLInner[0].strip()

        if chart['size'] != None:
           self.df[chart['size']]=pd.to_numeric(self.df[chart['size']],errors='coerce')
           self.df[chart['size']] = self.df[chart['size']].fillna(0)
        #print(chart['colY']) #debug
        #print(chart['size']) #debug
        #print(chart['symbol']) #debug
        #print(chart['hovername']) #debug
        #print(chart['hoverdata']) #debug
        #print(chart['linedash']) #debug
        #print(chart['linegroup']) #debug

    def process_trend_scatter(self,chart):
        ######################################
        #Process Scatter Plot trendline &
        #split into trendline, scope, and
        #options.
        #
        # Arguments:
        #   chart: Input Dictionary
        #   E.g.,: chart['chartopt0']='ols:trace:' or rolling:trace:window=5
        #################################
        chart['trendline']=None; chart['trendscope']=None; chart['trendopt']=None
        if chart['chartopt0'] != None and chart['chartopt0'] != '' and chart['chartopt0'].count(':') == 2:
            tL=chart['chartopt0'].split(':')
            if tL[0].strip() != '':
               chart['trendline']=tL[0].strip()
               if tL[1].strip() != '':
                   chart['trendscope'] = tL[1].strip()
                   if tL[2].strip() !='':
                       #chart['trendopt'] = dict(tL[2].strip())
                       #print(tL[2].strip()) #debug
                       mydic_tmp={}
                       for element in tL[2].split(','): #debug
                           #print(element) #debug
                           a=element.split('=')
                           if len(a)==2:
                               if a[1].strip().isdigit(): #integer
                                  b=int(a[1].strip())
                               elif re.match(r"\d+\.*\d*", a[1].strip()): #real number
                                  b=float(a[1].strip())
                               elif a[1].strip()=="True" or a[1].strip()=="False":
                                  b=bool(a[1].strip())
                               else:
                                  b=a[1].strip().replace("'","")
                           mydic_tmp[a[0].strip()]=b
                       chart['trendopt'] = mydic_tmp
        #print(chart['trendscope'] ) #debug

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
        if chart['colorseq'] != None:
           chart['color_discrete_sequence'] = chart['colorseq']

    def density_heatmap(self,chart):
        ####################################
        #Create Density HeatMap
        ####################################
      
        self.process_y_vars (chart)         #Process chart['colY'] and separate out the Y, Size and Symbol variables
        self.assign_chart_variables(chart)     #Assign Chart Variables
        self.set_x_type(chart)                 #optionally set type of X-axis
        self.get_custom_theme(chart) 
        chart['opacity']=None
        if chart['chartmode'] != None and chart['chartmode'] != "" and chart['chartmode'].replace(".","").isdigit():
           chart['opacity'] = float(chart['chartmode'])
        if chart['nbinsx'] !='' and chart['nbinsx'].isdigit():
                chart['nbinsx']=int(chart['nbinsx'])
        else:
           chart['nbinsx']=None;
        if chart['nbinsy'] !='' and chart['nbinsy'].isdigit():
                chart['nbinsy']=int(chart['nbinsy'])
        else:
           chart['nbinsy']=None;
        if chart['histnorm']=='':
           chart['histnorm']=None;

             
        fig=px.density_heatmap(
          data_frame=self.df
         ,x=chart['colX']                       #X field in DF
         ,y=chart['colY']                       #Y field in DF
         ,z=chart['color']                      #Z field in DF
         ,facet_col=chart['by']                 #Field in DF for By in horiz direction
         ,facet_col_wrap=chart['ncoli']         #Facet col count before wrap
         ,hover_name=chart['hovername']         #add bold field value to Hover tooltip
         ,hover_data=chart['hoverdata']         #add field and value to Hover tooltip
         ,title=chart['title']                  #Chart title
         ,labels=chart['labels']                #x/y labels. E.g., {pop='Population', b='...'}
         ,orientation=None                      #h or v
         ,range_x=chart['range_x']              #E.g., [100,1000]
         ,range_y=chart['range_y']              #E.g., [100,1000]
         ,color_continuous_scale=chart['color_discrete_sequence']  #CSS colors. E.g., color_discrete_sequence=px.colors.qualitative.G10 or ['blue', 'orange', 'green', 'brown']
         ,width=chart['width']                  #width in pixels
         ,height=chart['height']                #height in pixels
         ,template=chart['template']            #Plotly Theme/Template
         ,facet_col_spacing=chart['facetc']     #float between 0 and 1 (def=.02)
         ,facet_row_spacing=chart['facetr']     #float between 0 & 1 (def=.07)
         ,opacity=chart['opacity']              #Opacity between 0 and 1
         ,histfunc=chart['chartopt0']           #default='count' if no arguments provided, else 'sum' – One of 'count', 'sum', 'avg', 'min', or 'max'.Function used to aggregate values for summarization (note: can be normalized with histnorm). The arguments to this function are the values of z.
         ,histnorm=chart['histnorm']            #One of 'percent', 'probability', 'density', or 'probability density' If None, the output of histfunc is used as is. If 'probability', the output of histfunc for a given bin is divided by the sum of the output of histfunc for all bins. If 'percent', the output of histfunc for a given bin is divided by the sum of the output of histfunc for all bins and multiplied by 100. If 'density', the output of histfunc for a given bin is divided by the size of the bin. If 'probability density', the output of histfunc for a given bin is normalized such that it corresponds to the probability that a random event whose distribution is described by the output of histfunc will fall into that bin.
         ,nbinsx=chart['nbinsx']                #Positive integer. Sets the number of bins along the x axis.
         ,nbinsy=chart['nbinsy']                #Positive integer. Sets the number of bins along the y axis.
         ,text_auto=True                        #Display Labels True or False
         #,category_orders=''                   #force specific category order. E.g., {'day':['Thu','Fri','Sat']}
         #,log_x=False                          #Make x-axes logarithmic
         #,animation_frame=''                   #animation field
         );

        self.update_axes(fig, chart)             #Update grid axes
        self.add_reference_lines(fig, chart)     #Add any reference lines
        self.add_legend(fig, chart)              #Update Legend

        #return self.get_htm(fig,chart)
        return fig
            

    def linechart(self,chart):
        ####################################
        #Create LineChart
        ####################################
      
        self.process_y_vars (chart)            #Process chart['colY'] and separate out the Y and Symbol variables

        self.assign_chart_variables(chart)     #Assign Chart Variables
        self.set_x_type(chart)                 #optionally set type of X-axis
        self.get_custom_theme(chart) 

        chart['markers']=None
        if chart['markers0']=='TRUE':
           chart['markers']=True
        else:
           chart['markers']=False

        chart['datalabels']=None
        #print(chart['chartmode'] );  #debuf
        if chart['chartmode'] != None and chart['chartmode'].upper() == "TRUE":
           chart['datalabels']=chart['colY']

        fig=px.line(
          data_frame=self.df
         ,x=chart['colX']                       #X field in DF
         ,y=chart['colY']                       #Y field in DF
         ,line_group=chart['linegroup']         #Values from this column or array_like are used to group rows of data_frame into lines
         ,line_dash=chart['linedash']           #Values from this column are used to assign dash-patterns to lines.
         ,markers=chart['markers']              #If True, markers are shown on lines.
         ,line_shape =chart['chartopt0']        #One of 'linear' or 'spline'.
         ,text=chart['datalabels']              #Shows Variable as Data labels
         ,symbol=chart['symbol']
         ,color=chart['color']                  #Group/color field
         ,hover_name=chart['hovername']         #add bold field value to Hover tooltip
         ,hover_data=chart['hoverdata']         #add field and value to Hover tooltip
         ,facet_col=chart['by']                 #Field in DF for By in horiz direction
         ,facet_col_wrap=chart['ncoli']         #Facet col count before wrap
         ,facet_col_spacing=chart['facetc']     #float between 0 and 1 (def=.02)
         ,facet_row_spacing=chart['facetr']     #float between 0 & 1 (def=.07)
         ,title=chart['title']                  #Chart title
         ,labels=chart['labels']                #x/y labels. E.g., {pop='Population', b='...'}
         ,orientation='v'                       #h or v
         ,range_x=chart['range_x']              #E.g., [100,1000]
         ,range_y=chart['range_y']              #E.g., [100,1000]
         ,color_discrete_sequence=chart['color_discrete_sequence']  #CSS colors. E.g., color_discrete_sequence=px.colors.qualitative.G10 or ['blue', 'orange', 'green', 'brown']
         ,line_dash_sequence=chart['linetypeseq'] #Line Type (e.g., dash)
         ,symbol_sequence=chart['symbolseq']    #E.g., ['circle-open', 'circle', 'circle-open-dot', 'square']
         ,width=chart['width']                  #width in pixels
         ,height=chart['height']                #height in pixels
         ,template=chart['template']            #Plotly Theme/Template
         ,render_mode='auto'                    #'auto', 'svg' or 'webgl', default 'auto' Controls the browser API used to draw marks. 'svg’ is appropriate for figures of less than 1000 data points, and will allow for fully-vectorized output. 'webgl' is likely necessary for acceptable performance above 1000 points but rasterizes part of the output. 'auto' uses heuristics to choose the mode.
         #,category_orders=''                   #force specific category order. E.g., {'day':['Thu','Fri','Sat']}
         #,log_x=False                          #Make x-axes logarithmic
         #,animation_frame=''                   #animation field
         );

        self.update_axes(fig, chart)             #Update grid axes
        self.add_reference_lines(fig, chart)     #Add any reference lines
        self.add_legend(fig, chart)              #Update Legend
        #fig.update_traces(mode='markers+text',textposition='middle center', textfont_size=10, textfont_color='white')


        #return self.get_htm(fig,chart)
        return fig


    def scatter(self,chart):
        ####################################
        #Create Scatter Plot
        ####################################
      
        self.process_y_vars (chart)         #Process chart['colY'] and separate out the Y, Size and Symbol variables
        self.process_trend_scatter (chart)     #Extract out any trendline options from chart['chartopt0']
        #print(chart['trendline']); #debug
        #print(chart['trendscope']); #debug
        #print(chart['trendopt']); #debug

        self.assign_chart_variables(chart)     #Assign Chart Variables
        self.set_x_type(chart)                 #optionally set type of X-axis
        self.get_custom_theme(chart) 
        chart['opacity']=None
        if chart['chartmode'] != None and chart['chartmode'] != "" and chart['chartmode'].replace(".","").isdigit():
           chart['opacity'] = float(chart['chartmode'])
             
        fig=px.scatter(
          data_frame=self.df
         ,x=chart['colX']                       #X field in DF
         ,y=chart['colY']                       #Y field in DF
         ,size=chart['size']
         ,symbol=chart['symbol']
         ,color=chart['color']                  #Group/color field
         ,facet_col=chart['by']                 #Field in DF for By in horiz direction
         ,facet_col_wrap=chart['ncoli']         #Facet col count before wrap
         ,title=chart['title']                  #Chart title
         ,labels=chart['labels']                #x/y labels. E.g., {pop='Population', b='...'}
         ,orientation='v'                       #h or v
         ,range_x=chart['range_x']              #E.g., [100,1000]
         ,range_y=chart['range_y']              #E.g., [100,1000]
         ,text=chart['text']                    #Shows Variable as Data labels
         ,color_discrete_sequence=chart['color_discrete_sequence']  #CSS colors. E.g., color_discrete_sequence=px.colors.qualitative.G10 or ['blue', 'orange', 'green', 'brown']
         ,symbol_sequence=chart['symbolseq']    #E.g., ['circle-open', 'circle', 'circle-open-dot', 'square']
         ,color_continuous_scale=chart['color_discrete_sequence']  #CSS colors or color_continuous_scale=plotly.express.colors.sequential or plotly.express.colors.diverging
         ,width=chart['width']                  #width in pixels
         ,height=chart['height']                #height in pixels
         ,template=chart['template']            #Plotly Theme/Template
         ,facet_col_spacing=chart['facetc']     #float between 0 and 1 (def=.02)
         ,facet_row_spacing=chart['facetr']     #float between 0 & 1 (def=.07)
         ,hover_name=chart['hovername']         #add bold field value to Hover tooltip
         ,hover_data=chart['hoverdata']         #add field and value to Hover tooltip
         ,opacity=chart['opacity']              #Opacity between 0 and 1
         ,trendline=chart['trendline']          #ols, lowess, rolling, expanding or ewm
         ,trendline_scope=chart['trendscope']   #trace or overall. If 'trace', then one trendline is drawn per trace (i.e. per color, symbol, facet, animation frame etc) and if 'overall' then one trendline is computed for the entire dataset, and replicated across all facets.
         ,trendline_options=chart['trendopt']   #e.g., dict(frac=0.1) dict(window=5)
         ,render_mode='auto'                    #'auto', 'svg' or 'webgl', default 'auto' Controls the browser API used to draw marks. 'svg’ is appropriate for figures of less than 1000 data points, and will allow for fully-vectorized output. 'webgl' is likely necessary for acceptable performance above 1000 points but rasterizes part of the output. 'auto' uses heuristics to choose the mode.
         #,category_orders=''                   #force specific category order. E.g., {'day':['Thu','Fri','Sat']}
         #,log_x=False                          #Make x-axes logarithmic
         #,animation_frame=''                   #animation field
         );

        self.update_axes(fig, chart)             #Update grid axes
        self.add_reference_lines(fig, chart)     #Add any reference lines
        self.add_legend(fig, chart)              #Update Legend

        #fig.for_each_trace(lambda t: t.update(textfont_color='white', textposition='middle center', textfont_size=9))
        #fig.update_traces(mode='markers+text',textposition='middle center', textfont_size=9), textfont_color='white')

        #return self.get_htm(fig,chart)
        return fig



    def barchart(self,chart):
        ####################################
        #Create BarChart
        ####################################
        chart['hoverdata']=None; chart['pattern_shape']=None
        if chart['colY'].find(' -> ') != -1:
            self.summarize_bardata(chart)        

        self.assign_chart_variables(chart)     #Assign Chart Variables
        self.set_x_type(chart)                 #Sort by X and optionally set the type of X-axis
        self.get_custom_theme(chart) 

        chart['datalabels']=None
        #print(chart['chartmode'] );  #debuf
        if chart['chartmode'] != None and chart['chartmode'].upper() == "TRUE":
           chart['datalabels']=chart['colY']

        #print("Pattern Shape=", chart['pattern_shape'], ' + ', chart['color'] ) #Debug
       
        #self.df = self.df.sort_values(chart['colX']).reset_index(drop=True)
       
        fig=px.bar(
          data_frame=self.df
         ,x=chart['colX']                       #X field in DF
         ,y=chart['colY']                       #Y field in DF
         ,color=chart['color']                  #Group/color field
         ,facet_col=chart['by']                 #Field in DF for By in horiz direction
         ,facet_col_wrap=chart['ncoli']         #Facet col count before wrap
         ,title=chart['title']                  #Chart title
         ,labels=chart['labels']                #x/y labels. E.g., {pop='Population', b='...'}
         ,orientation='v'                       #h or v
         ,barmode=chart['chartopt0']            #'group' or 'overlay' or 'relative'
         #,range_x=                             #E.g., [100,1000]
         ,range_y=chart['range_y']              #E.g., [100,1000]
         ,text=chart['datalabels']              #Shows Variable as Data labels
         ,color_discrete_sequence=chart['color_discrete_sequence']  #CSS colors or color_discrete_sequence=px.colors.qualitative.G10
         ,color_continuous_scale=chart['color_discrete_sequence']  #CSS colors or color_continuous_scale=plotly.express.colors.sequential or plotly.express.colors.diverging
         ,color_discrete_map=chart['color_discrete_map']  #Dictionary Mapping of group labels to colors. E.g., {'unknown': 'blue', 'L4': 'red', 'L5': 'green', 'L6', 'purple', 'L8': 'orange'}
         ,pattern_shape=chart['pattern_shape']  #Hatching to use
         #,pattern_shape_sequence=chart['symbolseq']  #list of pattern shapes pattern_shape="nation", pattern_shape_sequence=[".", "x", "+"])
         ,pattern_shape_map=chart['pattern_shape_map']  #Dictionary Mapping of  labels to pattern shapes. E.g., {'RWA': '.', 'TDP': 'x', '': '+'}
         ,width=chart['width']                  #width in pixels
         ,height=chart['height']                #height in pixels
         ,template=chart['template']            #Plotly Theme/Template
         ,facet_col_spacing=chart['facetc']     #float between 0 and 1 (def=.02)
         ,facet_row_spacing=chart['facetr']     #float between 0 & 1 (def=.07)
         #,opacity=chart['opacity']             #Opacity between 0 and 1
         #,hover_name=chart['hovername']        #add bold field value to Hover tooltip
         ,hover_data=chart['hoverdata']         #add field and value to Hover tooltip
         ,category_orders=chart['category_orders']  #force specific category order. E.g., {'day':['Thu','Fri','Sat']}
         #,log_x=False                          #Make x-axes logarithmic
         #,animation_frame=''                   #animation field
         );

        self.update_axes(fig, chart)             #Update grid axes
        self.add_reference_lines(fig, chart)     #Add any reference lines
        self.add_legend(fig, chart)              #Update Legend

        if chart['x-sort'] != None and chart['x-sort'] != '':
            fig.update_xaxes(categoryorder=chart['x-sort']) #E.g., total descending or category ascending

        if chart['x-sort']=="category ascending" or chart['x-sort']=="category descending": 
            xlist=self.df[chart['colX']].unique()
            if chart['x-sort']=="category ascending": 
                xlist=sorted(xlist)
            else:
                xlist=sorted(xlist, reverse=True)
            #print(xlist)
            fig.update_xaxes(categoryorder='array',categoryarray=xlist);

        #return self.get_htm(fig,chart)
        return fig



    def boxplot(self,chart):
        ####################################
        #Create BoxPlot
        ####################################

        self.process_y_vars (chart)                    #Process chart['colY'] and separate out the Y, and Hover Variables
        self.assign_chart_variables(chart)                #Assign Chart Variables
        #self.df=self.df.sort_values(by=[chart['colX']]);  #Sort by X
        self.set_x_type(chart)                            #Optionally set the type of X-axis
        self.get_custom_theme(chart) 
        pointsdata=chart['chartopt0']
        if pointsdata.lower()=='false':
            pointsdata=False

        fig=px.box(
          data_frame=self.df
         ,x=chart['colX']                       #X field in DF
         ,y=chart['colY']                       #Y field in DF
         ,color=chart['color']                  #Group/color field
         ,facet_col=chart['by']                 #Field in DF for By in horiz direction
         ,facet_col_wrap=chart['ncoli']         #Facet col count before wrap
         ,title=chart['title']                  #Chart title
         ,labels=chart['labels']                #x/y labels. E.g., {pop='Population', b='...'}
         ,orientation='v'                       #h or v
         ,boxmode=chart['chartmode']            #'group' or 'overlay' 
         ,notched=False
         #,range_x=                             #E.g., [100,1000]
         ,range_y=chart['range_y']              #E.g., [100,1000]
         ,points=pointsdata                     #Shows Data points 'outliers' 'suspectedoutliers' 'all' False
         ,color_discrete_sequence=chart['color_discrete_sequence']  #CSS colors or color_discrete_sequence=px.colors.qualitative.G10
         ,width=chart['width']                  #width in pixels
         ,height=chart['height']                #height in pixels
         ,template=chart['template']            #Plotly Theme/Template
         ,facet_col_spacing=chart['facetc']     #float between 0 and 1 (def=.02)
         ,facet_row_spacing=chart['facetr']     #float between 0 & 1 (def=.07)
         ,hover_name=chart['hovername']         #add bold field value to Hover tooltip
         ,hover_data=chart['hoverdata']         #add field and value to Hover tooltip
         #,category_orders=''                   #force specific category order. E.g., {'day':['Thu','Fri','Sat']}
         #,log_x=False                          #Make x-axes logarithmic
         #,animation_frame=''                   #animation field
         );

        #fig.update_traces(quartilemethod='linear')  #'linear', 'exclusive', 'inclusive'
        #fig.update_traces(quartilemethod='linear', jitter=0, col=1)

        self.update_axes(fig, chart)          #Update grid axes
        self.add_reference_lines(fig, chart)  #Add any reference lines
        self.add_legend(fig, chart)           #Update Legend
        fig.update_xaxes(categoryorder='category ascending')

        #return self.get_htm(fig,chart)
        return fig

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

        return self.get_htm(myoutfig,chart) #html script

