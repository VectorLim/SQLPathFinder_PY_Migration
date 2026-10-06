# ***************************************************************************
# Module:            lemsUtilities/autoQuant
# Creation date:     August 15, 2023
# Written by:        Farzin Guilak
# Modification:      $Id: autoQuant.py,v 1.38 2024/02/27 23:44:12 fguilak Exp $
# **************************************************************************

from dataclasses import dataclass
from jenkspy import JenksNaturalBreaks as JNB
from typing import Sequence
import datetime as dt
# import multiprocessing as mp
import logging
import numpy as np
import pandas as pd
import re
# from multiprocessing import freeze_support



import pylems_rulesParser as rp
#import rulesParser as rp # for development

# To ignore UserWarning to specify format from datetime conversion 
import warnings
warnings.filterwarnings( "ignore", category=UserWarning )

__version__ = '$Id: autoQuant.py,v 1.38 2024/02/27 23:44:12 fguilak Exp $'

# ======================================================================
# Quantization info
#
@dataclass
class JQinfo:
    '''Structure holding quantization information for each numeric variable:
      - `colName` column name
      - `order` number of clusters specified for Fisher-Jenks algorithm
      - `gvf` goodness-of-variance fit measure
      - `breaks` (order-1) breakpoints used in quantizing numerics
    
    '''
    colName: str             # Name of column
    order: int               # Quantization order 
    gvf: float               # Goodness-of-variance fit
    breaks: Sequence         # (order-1) break points


# ======================================================================
# Helpers
#

# --------------------------------------------------
# Attempt to convert a provided array, list, or Series to a numpy.ndarray.  Returns
# the converted array, or raises an exception if the conversion fails (i.e. data
# isn't numeric).  Key requirement is that NAs in the input be converted to
# numpy.nan values.
#
def _toNumeric( c ):
    try:
        numArr = pd.to_numeric( c )
    except ValueError:
        raise

    return numArr

# --------------------------------------------------
# Attempt to convert a provided array, list, or Series to datetime.  Returns the
# converted array, or raises an exception if the conversion fails.  Key
# requirement is that NAs in the input be converted to numpy.nan values.
#
def _toDatetime( c ):
    try:
        dtArr = pd.to_datetime( c )
    except ValueError:
        raise

    return dtArr


# --------------------------------------------------

def qInfoToStr( qInfo ):
    '''Function to convert a list of JQinfo structures to newline-separated CSV lines,
    suitable for writing to a file.

    Inputs:
      - `qInfo` list of JQinfo structures

    Output:
      - A string comprised of a CSV header and lines containing information for
        each JQinfo structure in the input.
    '''

    outStr = [ 'colName,order,GVF,breaks' ]
    for q in qInfo:
        if q is not None:
            outStr.append( ','.join([q.colName, str(q.order), str(q.gvf), rp._stripChars( str(q.breaks),'[]' )]))

    return '\n'.join( outStr ) + '\n'

# =========================================================================

class autoQuant:
    """This class and supporting functions facilitate quantization of numeric arrays using the
       Fisher-Jenks natural break algorithm, a clustering-based quantizer provided by the
       `jenkspy` module.  Fisher-Jenks groups elements according to "natural" breakpoints
       in their distributions.  It generally handles any distribution, and provides a much
       more accurate representation of multimodal or skewed distributions than quantile
       binning.

       This class automates selection of number of classes to be used in the quantization
       of each numeric using a heuristic based on the goodness of variance fit, a measure
       quantifying average within-class variance against overall variance of data.  Higher
       values of this measure indicate lower intra-class variance of the clusters.  

       The heuristic iterates from order 2 to `maxOrder` (default 5), returning the lowest
       order whose goodness of variance fit exceeds `varThresh` (default 0.9).  If none
       exceed, it returns the highest order tested.
      
       NOTE: the implementation used in this class, `jenkspy`, has ~O(n^2) complexity so
       completion time increases rapidly with input array size.  As a rough indication, on
       my laptop (3.1GHz i7) an array of 16,000 takes approximately 0.5 sec, one of 32,000
       takes 2 sec, and 64,000 takes 4 sec.  Benchmark on your system before running with 
       variables containing hundreds of thousands of values.
      
       Functions and methods intended to be `public` have documentation you can view from a
       Python interpreter, for example `>>> help(lemsUtilities.autoQuant)`.
     
       Instancing the class
        - Instance the `autoQuant` class, optionally setting `nProcs`, `maxOrder`, `varThresh`,
          and `rulesFile`
          - `nProcs` specifies the number of processor cores to use in the quantization and
             defaults to None, indicating use of all available cores
          - `rulesFile` is a LEMS-format rules file supporting the `keep` directive, which will
             override a match against any `ignore` regexp. Column names matching `ignore` regexps
             AND NOT matching `keep` regexps won't be in the output dataframe and those matching
             categoricals will be copied without quantization.
        - `maxOrder` is the greatest number of clusters checked by the order-selection algorithm;
           it defaults to 5
        - `varThresh` is the variance threhold for stopping at iterations with fewer that 
           `maxOrder` clusters; it defaults to 0.9

       Methods
        - To quantize a data file, use `quantizeFile`
        - To quantize a single array, use `quantizeArray`
        - To get a CSV representation of quantization info (as a single string), use
          `qInfoToStr` and write the results to a file

    """

    def __init__( self, nProcs=None, rulesFile=None, maxOrder=5, varThresh=0.9 ):
        self.startOrder = 2
        self.maxOrder = maxOrder
        self.nProcs = nProcs
        self.varThresh = varThresh

        if rulesFile is not None:
            try:
                self.rules = rp.rulesParser( rulesFile )
            except IOError as err:
                logging.error( f'autoQuant|init|error reading rules file {err}' )
                raise
            except ValueError as err:
                logging.error( err )
                raise

        else:
            self.rules = None

        self._reset()

    def _reset( self ):
        self.cleanArray = None
        self.colNames = None
        self.fjq = None
        self.inArray = None
        self.isDatetime = None
        self.inDF = None
        self.isNumeric = None
        self.outDF = None
        self.nCores = None
        self.qArray = None
        self.qInfo = None
        self._setOrder( self.startOrder )
        
    def _setOrder( self, order ):
        self.fjq = JNB( order )

    def _getOrder( self ):
        return self.fjq.n_classes

    # --------------------------------------------------
    # Method to convert input array to numpy ndarray using pd.to_numeric, which
    # converts missing values to NaN.  Also sets instance variables used in fitting
    # and quantization.
    #
    # Output: set instance vars isNumeric, isDatetime, cleanArray
    #
    def _setInputArray( self, inDat ):

        self.isNumeric = True
        self.isDatetime = False

        # ValueError exception if data can't be parsed as numeric
        try:
            self.inArray = _toNumeric( inDat )
        except ValueError:
            logging.info( f'{id(self)}:_setInputArray: non-numeric input array' )
            self.isNumeric = False

        if self.isNumeric:
            logging.info( f'{id(self)}:_setInputArray: numeric input array' )
            self.cleanArray = self.inArray[ ~np.isnan( self.inArray )]
            return

        # If not numeric, try datetime.  This may throw userwarnings since not defining a
        # format (FUTURE)
        try:
            self.inArray = _toDatetime( inDat )
        except ValueError:
            logging.info( f'{id(self)}:_setInputArray: non-datetime input array, treating as categorical' )
            self.isDatetime = False
            self.inArray = inDat
            self.cleanArray = None
            return

        # If no exceptions to this point, input was datetime, convert to epoch time and
        # clean up for quantization
        logging.info( f'{id(self)}:_setInputArray: datetime input array' )
        self.isDatetime = True
        self.inArray = (self.inArray - dt.datetime( 1970, 1, 1)).total_seconds()
        self.cleanArray = self.inArray[ ~pd.isnull( self.inArray )]

    # --------------------------------------------------
    # Method to fit JNB quantizer instance (specify order with _setOrder prior to
    # invoking _fitColumn) to data in `self.cleanArray`, which must not have any NA
    # elements. Also, the instance's order must be less than or equal to the
    # variable's cardinality.
    #
    # Returns a JQinfo structure including information on this fit operation.
    #
    def _fitColumn( self ):
        try:
            self.fjq.fit( self.cleanArray )
        except ValueError as err:
            logging.error( f'{id(self)}:_fitColumn|NaN or non-numeric data provided to fit(): {err}' )
            raise

        brks = self.fjq.inner_breaks_
        
        if self.isDatetime:
            dti = pd.to_datetime( brks, unit='s' )  # DatetimeIndex (list of Timestamp)
            innerBreaks = [ str(dt) for dt in dti ] # Timestamp to str
        else:
            innerBreaks = brks
            
        return JQinfo( colName = '',
                       order = self._getOrder(),
                       gvf = self.fjq.goodness_of_variance_fit( self.cleanArray ),
                       breaks = innerBreaks )
        

    # --------------------------------------------------
    # Method to fit and quantize input array.  Order must be set prior to invoking.
    # 
    def  _doFitAndQuant( self ):
        try:
            self.qInfo = self._fitColumn()
        except ValueError as err:
            logging.error( f'{id(self)}:_doFitAndQuant|_fitColumn() failed: {err}' )
            raise
        
        # bump index by 1 to get J1 ... Jn as output bins
        self.qArray =  [ '' if np.isnan(x) else 'J'+str(self.fjq.predict(x).item()+1) for x in self.inArray ]

    # --------------------------------------------------
    def quantizeArray( self, inDat, order=0 ):
        '''Method to optionally select the best order, then fit and quantize the provided array.
        
        Inputs
          - `inDat` input array to be fit and quantized
          - `order` default value 0 runs heuristic search for best order, otherwise uses specified value

        Output
          - (`None`,`None`) if input array is not numeric or if it has cardinality of one
          - (`quantizeArray`, `JQinfo`) - tuple comprising `JQinfo` structure with quantizer info and 
            `quantizeArray` with quantized output

        '''

        self._reset()
        self._setInputArray( inDat )
        if not self.isNumeric:
            logging.info( f'{id(self)}:quantizeArray|array is not numeric, skipping fit and quantize' )
            return None,None

        if order == 1:
            logging.error( f'{id(self)}:quantizeArray|requested order must be greater than 1' )
            raise ValueError( 'quantizeArray|requested order must be greater than 1' )

        if order == 0:
            self._selectOrder()
            if self.qInfo is None:
                logging.info( f'{id(self)}:quantizeArray|array has cardinality of one, skipping fit and quantize' )
                return None,None
            
            order = self.qInfo.order

        self._setOrder( order )

        try:
            self._doFitAndQuant()
        except ValueError as err:
            logging.error( f'{id(self)}:quantizeArray|_doFitandQuant() failed: {err}' )
            raise

        # Return a tuple of [quantized numerics],qInfo
        return self.qArray, self.qInfo


    # --------------------------------------------------
    # Private method to fit and quantize the current column, for use by quantizeFile.
    # Returns tuple: (quantized DF, qInfo) for the data provided.
    # 
    def _quantizeColumn( self, col, inVals ):

        if self.rules is not None:
            ruleLookup = self.rules.processName( col )

            if ruleLookup == "ignore":
                logging.debug( f'{id(self)}:_quantizeColumn|<{col}> matched ignore rule, skipping' )
                return None,None
            elif ruleLookup == "categorical":
                logging.debug( f'{id(self)}:_quantizeColumn|<{col}> matched categorical rule, copying' )
                return pd.DataFrame( { col : inVals } ), None

        self._setInputArray( inVals )
        
        # If the column isn't a numeric or datetime, assume it's categorical.
        # Log and copy it out unchanged
        if not self.isNumeric and not self.isDatetime:
            logging.debug( f'{id(self)}:_quantizeColumn|column <{col}> is not numeric, copying to output' )
            return pd.DataFrame( { col : inVals } ), None

        logging.info( f'{id(self)}:_quantizeColumn|quantizing column <{col}>' )
        try:
            self._selectOrder()
        except ValueError as err:
            logging.error( f'{id(self)}:_quantizeColumn|_selectOrder failed for column <{col}>' )
            raise

        # _selectOrder sets self.qInfo to None if the column has only
        # one value, or is all NA
        if self.qInfo is None:
            logging.debug( f'{id(self)}:_quantizeColumn|column <{col}> has cardinality of one, copying to output' )
            return pd.DataFrame( { col : inVals } ), None
            
        if self.qInfo.gvf < self.varThresh:
            logging.debug( f'{id(self)}:_quantizeColumn|<{col}> best GVF <{self.qInfo.gvf}> does not exceed varThresh <{self.varThresh}>' )

        # If self.qInfo isn't None, set the best order, fit, and quantize
        self._setOrder( self.qInfo.order )
        

        try:
            self._doFitAndQuant()
        except ValueError as err:
            logging.error( f'{id(self)}:_quantizeColumn|_doFitAndQuant() failed for column <{col}>: {err}' )
            raise
            
        # Set its name here, since selectOrder doesn't
        self.qInfo.colName = col
        
        # Rename the column by appending quantization order
        qName = col + '-J' + str( self.qInfo.order )

        # Return a tuple of quantized output as a DF and its qInfo
        return pd.DataFrame( {qName : self.qArray} ), self.qInfo

    # --------------------------------------------------

    def _selectOrder( self ):
        '''Select order (number of classes) for Fisher-Jenks quantization of `self.inArray`.  The
        selection algorithm iterates from 2 to `maxOrder` and picks the lowest order for which
        computed goodness-of-variance fit (GVF) exceeds `varThresh`.  Both of these parameters can
        be overridden at instantiation.  If the threshold is not exceeded at any order, `maxOrder` is
        used and the event is logged at `info` level.
    
        Inputs
          - None. Operates on `self.cleanArray` (set via `_setInputArray`)
    
        Output
          - None. Sets `self.qInfo` structure to reflect selected order, variance
            measure, and break points.  If the input array has cardinality of one,
            sets `self.qInfo` to `None`
    
        Exceptions
          - None

        '''
      
        card = len( set( self.cleanArray ))
        logging.debug( f'{id(self)}:_selectOrder|cleanArray cardinality: {card}' )
        # card == 0 if all NA, card == 1 for valid data
        if card < 2:
            self.qInfo = None
            return

        oInfo = [] # JQinfo for each order
        for oIdx in range( self.startOrder, self.maxOrder + 1 ):
            self._setOrder( oIdx )

            logging.debug( f'{id(self)}:_selectOrder|fitting column with order {oIdx}' )
            try:
                qInfo = self._fitColumn()
            except ValueError as err:
                logging.error( f'{id(self)}:_selectOrder|_fitColumn failed due to bad or missing value: {err}' )
                raise

            # Save results of this order iteration
            oInfo.append( qInfo )

            if oIdx == card:
                logging.info( f'{id(self)}:_selectOrder|search order == cardinality {card}, breaking' )
                break

        logging.debug( f'{id(self)}:_selectOrder|oInfo: {oInfo}' )

        # Process the goodness-of-fit values to determine the order for this variable.
        # Heuristic:
        #   - smallest order that exceeds varThresh
        #   - if none exceed varThresh, use largest order
        filtInfo = [ x for x in oInfo if x.gvf >= self.varThresh ]

        if len( filtInfo ) == 0:
            # Pick the one with the largest order
            oInfoSelect = sorted( oInfo, key=lambda x: x.order, reverse=True)[0]
            logging.info( f'{id(self)}:_selectOrder|best GVF <{oInfoSelect.gvf}> does not exceed varThresh <{self.varThresh}>' )
        else:
            # Pick the one with the smallest order
            oInfoSelect = sorted( filtInfo, key=lambda x: x.order)[0]
        
        self.qInfo = oInfoSelect

    # --------------------------------------------------
    # Method to concatenate a column from the input dataframe to the output dataframe
    #
    def _concatDF( self, inCol ):
        self.outDF = pd.concat( [self.outDF, inCol.fillna( '' )], axis=1 )

    # --------------------------------------------------
    # Method to process all variable names in the input file against the rules.
    # Use these to generate an include list and dtype hash for read_csv.
    # 
    def _processNamesAndRead( self, inFile, sep ):
        try:
            inNames = pd.read_csv( inFile, sep=sep, nrows=0).columns.tolist()
        except Exception as err:
            logging.error( f'autoQuant|processVarNames|error reading input file {err}' )
            raise

        if self.rules:
            # Returns LEMS rulesfile format list of strings with ignore and categorical
            # overrides specified using anchored regexps (i.e. exact variable names)
            rulesList = self.rules.getRulesFileList( inNames )

            # Compile the regexps specified for ignores in rulesList
            igRe = [re.compile(y[1]) for y in [x.split(',') for x in rulesList] if y[0]=='ignore']
        
            # Take out the regexp anchors from categoricals in rulesList, since dtype takes a
            # hash of names (not regexps) to type
            catList = [y[1] for y in [x.split(',') for x in rulesList] if y[0]=='categorical']
            catHash = { "".join( [x for x in k if x not in ['^','$']]):str for k in catList }

            logging.debug( f'\n  catHash: {catHash}\n  igRe: {igRe}' )

            useFunc = lambda x: not any( regexp.search(x) for regexp in igRe)
        
        else:
            useFunc = None
            catHash = None

        try:
            self.inDF = pd.read_csv( inFile,
                                     sep = sep,
                                     usecols = useFunc,
                                     dtype = catHash,
                                     skip_blank_lines=False,
                                     low_memory=False )
        except Exception as err:
            logging.error( f'autoQuant|processNamesAndRead|file read failed for {inFile}: {err}' )
            raise
        

    # --------------------------------------------------
    # Do the quantization with desired number of cores.
    # 
    def _multiCoreQuant( self ):
        # Set up the multiprocessing pool of resources.
        # p = mp.Pool( processes = self.nProcs )
        
        # Pull out all names and values for parallel processing
        colNames = self.inDF.columns.values.tolist()
        # colVals  = [ self.inDF[ x ].values for x in colNames ]
        #
        # res = p.starmap( self._quantizeColumn, zip( colNames, colVals ))
        #
        # # Separate out tuples returned by _quantizeColumn
        # dfList,quantInfo = zip( *res )
        dfList = []
        quantInfo = []

        for curr_col in colNames:
            dfList1, quantInfo1 = self._quantizeColumn(curr_col, self.inDF[curr_col].values)
            dfList.append(dfList1)
            quantInfo.append(quantInfo1)

        # On larger inputs, simply appending a column was causing fragmentation warnings,
        # so using concat instead.
        self.outDF = pd.concat( dfList, axis=1)

        return self.outDF, quantInfo

    # --------------------------------------------------

    def quantizeFile( self, inFile, sep=',' ):
        '''Quantize all numeric columns in the input file using overrides specified by the
        rules file provided to the autoQuant constructor.  The algorithm implemented in
        this class will select the best cluster order for each numeric using a
        variance-based heuristic.  Non-numeric variables, or those overridden to be
        categorical by the rules file, are returned as-is in the output dataframe.
        Variables meeting ignore AND not keep/categorical directives in the rules file are
        excluded from the output.
        
        Inputs:
          - `inFile` input file
          - `sep` data field separator, defaults to ',' for CSV, set to '\\t' for
             tab-separated fields

        Output:
          - Returns a tuple: `(outDF,[JQinfo])` comprising the output dataframe with 
            quantized numerics and a list of `JQinfo` structures containing details of
            each quantized column.

            In `outDF`, column names of quantized numerics are suffixed with `-Jn` to
            indicate Fisher-Jenks quantization of order `n` used for the column.  Values
            in the column range from `J1` to `Jn`.

            Use `qInfoToStr()` to convert the `[JQinfo]` array to a CSV string that
            you can write to a file.

        '''

        if ( sep not in ",\t" ):
            logging.error( f"autoQuant|quantizeFile|separator <{sep}> - must be ',' or '\\t'" )
            raise ValueError

        # Preprocess the variable names using the rules and read
        self._processNamesAndRead( inFile, sep )

        return self._multiCoreQuant()
    
    # --------------------------------------------------

    def quantizeDF( self, inDF ):
        '''Quantize all numeric columns in the input dataframe, selecting the best order 
        using a variance-based heuristic.  Non-numeric columns are returned as-is in the
        output dataframe.  Uses rule file optionally supplied at instantiation.
        
        Inputs:
          - `inDF` input dataframe

        Output:
          - Returns a tuple: `(outDF,[JQinfo])` comprising the output dataframe with 
            quantized numerics and a list of `JQinfo` structures containing details of
            each quantized column.

            In `outDF`, column names of quantized numerics are suffixed with `-Jn` to
            indicate Fisher-Jenks quantization of order `n` used for the column.  Values
            in the column range from `J1` to `Jn`.

            Use `qInfoToStr()` to convert the `[JQinfo]` array to a CSV string that
            you can write to a file.

        '''

        self.inDF = inDF
        
        return self._multiCoreQuant()

if __name__ == "__main__":
    freeze_support()