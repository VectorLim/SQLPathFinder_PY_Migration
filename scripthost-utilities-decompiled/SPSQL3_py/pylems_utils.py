# ***************************************************************************
# Module:            utils - miscellaneous utilities for use with LEMS
# Creation date:     April 29, 2022
# Written by:        Farzin Guilak
# Modification:      $Id: utils.py,v 1.9 2024/05/20 22:22:04 fguilak Exp $
# **************************************************************************
# 
from functools import partial
import logging
import pandas as pd
import itertools
import multiprocess as mp

__version__ = '$Id: utils.py,v 1.9 2024/05/20 22:22:04 fguilak Exp $'


# ======================================================================
# Functions for use with info_theory_pairs output
#
def itPairs( pairVals, param1, param2 = None ):
    """Output from lems.info_theory_pairs(), provided in pairVals, might
       have either variable in the X or Y position.  When param2 is
       *not* specified, this helper function finds the rows in
       pairVals with either X or Y equal to param1.  When param2 *is*
       specified, it returns the single row in pairVals that includes
       both param1 and param2.

    """
    if param2 is None:
        return pairVals[ (pairVals[ 'X' ] == param1) | (pairVals[ 'Y' ] == param1 ) ]
    else:
        return pairVals[ ((pairVals[ 'X' ] == param1) & (pairVals[ 'Y' ] == param2 )) |
                         ((pairVals[ 'X' ] == param2) & (pairVals[ 'Y' ] == param1 )) ]


# --------------------------------------------------
# Redundancy info
#
def filterRedundantFeaturesNMI( pairVals, colNames, threshold ):
    """Given pairVals (must be output from lems.info_theory_pairs()) and
       list of all column names, filter out any parameters that are
       redundant as determined by applying the specified threshold to
       their normalized mutual information.  Returns a dict mapping
       each unique feature to a list of those redundant with it.  

       Note: due to symmetry of NMI(X;Y), the choice of which feature
       is deemed unique and which are redundant is arbitrary and based
       on ordering in pairVals.

       Ways to view this dict as a DataFrame:
          nrf = filterRedundantFeaturesNMI( ... )
          pd.DataFrame.from_dict( nrf, orient='index')
          pd.DataFrame.from_dict( nrf, orient='index').stack()
          pd.DataFrame.from_dict( nrf, orient='index').stack().reset_index( level=0 )

    """

    highNMI = pairVals[ pairVals[ 'NMI(X;Y)' ] >= threshold ]

    return uniqFeatureMapping( highNMI, colNames )

def uniqFeatureMapping( pairVals, colNames ):
    """Given pairVals (dataframe output from lems.info_theory_pairs() that
       has already been filtered to comprise only redundant pairs) and
       a list of all column names, generate a dict mapping unique
       features to a list of redundant ones.  This function is used by
       filterRedundantFeaturesNMI(), and is provided separately to
       support customized redundancy determination.

       Ways to view this dict as a DataFrame:
          nrf = uniqFeaturesDict( ... )
          pd.DataFrame.from_dict( nrf, orient='index')
          pd.DataFrame.from_dict( nrf, orient='index').stack()
          pd.DataFrame.from_dict( nrf, orient='index').stack().reset_index( level=0 )

    """

    selFs = {}
    for c in colNames:
        selFs[ c ] = []

    for idx,row in pairVals.iterrows():
        selFs[ row['X']].append( row[ 'Y' ])

    # Use list() to copy keys prior to iterating, otherwise can't
    # change underlying dict
    for key in list(selFs):
        # 'get' the key to return None
        if selFs.get( key ):
            for equiv in selFs[ key ]:
                # Check in case equiv removed before
                if selFs.get( equiv ) is not None: selFs.pop( equiv )

    return selFs


# ======================================================================
# Functions facilitating access to LEMS Field info
#
def colNameToIndex( lemsDB, colName ):
    """Return the internal LEMS index corresponding to the specified column name
       in the LEMS store.  Throws a ValueError if the name isn't found.
    """
    idx = None
    try:
        idx = list( map( lambda v: v.name==colName, lemsDB.columns )).index( True )
    except ValueError:
        raise ValueError
        
    return idx

# Return hash of field information for a given column index
def getFieldInfo( lemsDB, colName ):
    """Return LEMS Field information for the specified variable from the connected LEMS store.

       This function returns a Python dict with the following fields:
         'name'    : Variable name
         'index'   : Variable's index in the LEMS kernel
         'type'    : Variable's inferred type at ingest
         'card'    : Number of distinct values detected at ingest
         'qcard'   : Number of distinct values stored (generally differs from card for numerics)
         'cats'    : List of strings ingested for categorical data, or None if not categorical
         'group'   : The variable's assigned group. 'default' unless changed via control file
         'mapping' : Input mapping to LEMS representation: 'native', 'quantized', 'categorical'
         'qzero'   : None for non-numerics, otherwise Boolean reflecting zero binning status
         'qmode'   : None for non-numerics, otherwise the quantization mode
         'qbounds' : None for non-numerics, otherwise a list of values specifying bin 
                     boundaries for quantile/logarithmic quantization
    """
    try:
        colIdx = colNameToIndex( lemsDB, colName )
    except ValueError:
        print( 'Error: column name <{}> not found in LEMS store'.format( colName ))
        return None
    
    field = lemsDB.columns[ colIdx ]
    qp = field.quantize_params
    cats = field.categories
    return { 'name'  : field.name,
             'type'  : field.type,
             'card'  : field.cardinality,
             'qcard' : field.distinct_stored,
             'index' : field.index,
             'cats'  : list( cats.keys()) if cats else None,
             'group' : field.group,
             'mapping' : field.mapping_mode,
             'qzero' : qp[ 'zero_bucket' ] if qp else None,
             'qmode' : qp[ 'mode' ] if qp else None,
             'qbounds' : qp[ 'buckets' ] if qp else None }



# ======================================================================
# Functions to provide counts from probability values
#
# -----------------------------------
# Given a LEMS store, hash of event vars/vals, and hash of condition
# vars/values, compute the corresponding count from probability
# values.  Returns a tuple containing the probability value and
# the corresponding count:  (pVal,pCount)
# 
# Note: this will eventually be built into the LEMS kernel
#
def probability_count( ldb, evntHash, condHash ):
    """Return episode count corresponding to probability.

    Parameters
    ----------
    ldb : LEMS connection object
        Connection to a previously-ingested LEMS store

    evntHash : dict
        {'var':'val', ...} specifying event(s)

    condHash : dict
        {'var':'val', ...} specifying condition(s)

    Returns
    -------
    Tuple (probability,count)
    """

    if not condHash:
        condProb = 1.0
    else:
        condProb = ldb.probability( condHash, {} )

    pVal = ldb.probability( evntHash, condHash )

    return (pVal, int( round( pVal * condProb * ldb.total_count )))


# -----------------------------------
# Convert '' hash values to None for use by probability functions
#
def cleanNAs( inHash ):
    for k,v in inHash.items():
        if v=='':
            inHash[k] = None
    return inHash

# -----------------------------------
# Compute the count corresponding to the probability on
# one row of a posterior_likelihood table
#
def plRowCount( ldb, row ):
    colVals = [(c, row[c]) for c in row.index]
    eviVals = [ (t[0][5:],t[1]) for t in colVals if t[0].startswith( 'evi' )]
    hypVals = [ (t[0][5:],t[1]) for t in colVals if t[0].startswith( 'hyp' )]
    eviHash = { t[0]:t[1] for t in eviVals }
    hypHash = { t[0]:t[1] for t in hypVals }
    pCountPair = probability_count( ldb, cleanNAs( eviHash ), cleanNAs( hypHash ))
    return pCountPair[1]

# -----------------------------------
# Given a LEMS store, set of evidence variables, and set of
# hypothesis variables, compute the posterior_likelihood then
# determine the counts for each row.  Return the counts as a
# new column at the end of the posterior_likelihood dataframe.
#
# Notes:
#   - Name is overloaded with pylems; access this one via lemsUtilities
#   - This function will eventually be built into the LEMS kernel
#
def posterior_likelihood( ldb, evi, hyp ):
    """Return posterior/likelihood table augmented with episode counts

    Parameters
    ----------
    ldb : LEMS connection object
        Connection to a previously-ingested LEMS store

    evi : string list
        List of column name(s) to be used as evidence variables

    hyp : string list
        List of column name(s) to be used as hypothesis variables

    Returns
    -------
    Posterior/likelihood dataframe augmented with "Count" column
    """
    pl = ldb.posterior_likelihood( evi, hyp )
    plrc = partial( plRowCount, ldb )
    pl[ 'Count' ] = pl.apply( plrc, axis=1 )
    return pl

# ======================================================================
# Iterator to provide probability tables, sorted by either posterior or likelihood,
# for all `nComb`-combinations variables in the input list, subject to the specified 
# output variable and value.
#

# -----------------------------------
# Function for parallelizing posterior/likelihood calculation
#
def postLikeComputeProbs( oVar, oVal, hyp ):
    pl = posterior_likelihood( LDB, [ oVar ], hyp  )
    pTable = pl[ pl[ 'evi: '+oVar ] == oVal ]
    maxP = pTable[ 'Posterior' ].max()
    maxL = pTable[ 'Likelihood' ].max()
    maxC = pTable[ 'Count' ].max()
    return ( pTable, hyp, maxP, maxL, maxC )
    
# -----------------------------------
# Static variable to hold ldb for parallelization, and init method to set it
# when creating the mulit-processing pool
#
LDB = None
def postLikeInitWorker( lemsDB ):
    global LDB
    LDB = lemsDB

# -----------------------------------
# Iterator class definition
#
class postLikeHypCombs:
    """
    Iterator class to provide ranked posterior/likelihood probability tables for
    hypotheses formed by every `nComb`-combination of the input variable list, and
    filtered to include only rows for which the evidence variable has a specified
    value.

    The tables are ranked in decreasing order of the `sortBy` parameter, which can be
    "Posterior", "Likelihood", or "Count".

    At instantiation all probability computations are done in parallel on `nProcs` cores
    (default uses all available cores).

    When reading results with `next`, `for`, or in a comprehension, the iterator returns a
    tuple `(probabilityTable,hypotheses)` for the next remaining hypothesis combination
    with the largest `sortBy` value.  See constructor for details.

    """
    
    def __init__( self, ldb, oVar, oVal, sortBy, varList, nCombs=2, nProcs=None ):
        """
        Parameters
        ----------
        ldb : LEMS connection object
            Connection to a previously-ingested LEMS store

        oVar : string
            Outcome variable name, probability table filtered for oVar==oVal

        oVal : string
            Outcome variable value, probability table filtered for oVar==oVal

        sortBy : string
            field used to rank probability tables.  Can be "Posterior", "Likelihood", or
            "Count".  Count is the number of episodes corresponding to each row of the
            probability table

        varList : list of strings
            Variable names used in forming hypothesis combinations

        nCombs : int - optional
            How many variable names per combination, default 2

        nProcs : int - optional
            Number of cores to use, default None uses all available cores
        

        Returns
        -------
        The iterator's `next` method returns a tuple (probabilityTable,hypothese)
           - `probabilityTable` is the ranked and filtered probability table
           - `hypotheses` is the list of strings used as hypothesis variables
        """
       
        validSortBy = [ 'Posterior', 'Likelihood', 'Count' ]
        if sortBy not in validSortBy:
            raise ValueError( f'postLikeHypCombs|sortBy must be "Posterior", "Likelihood", or "Count"' )

        byIdx = validSortBy.index( sortBy )
        hPairs = list( itertools.combinations( varList, nCombs ))
        hList = [ list(x) for x in hPairs ]

        with mp.Pool( processes=nProcs, initializer=postLikeInitWorker, initargs=(ldb,)) as pool:
            self.outList = pool.starmap( postLikeComputeProbs, [(oVar,oVal,hyp) for hyp in hList])

        # validSortBy:         [ post, like, count]
        #    byIdx:               0     1     2
        # outList: (pTable, hyp, maxP, maxL, maxC)
        #    sIdx:    0      1    2     3     4
        sIdx = byIdx + 2
        smpl = sorted( self.outList, key = lambda x: x[sIdx], reverse=True )

        self.outList = [ (x[0].sort_values( by=sortBy, ascending=False ),x[1]) for x in smpl ]
        self.nList = len( self.outList )
        self.idx = 0

    def __iter__( self ):
        return self

    def __next__( self ):
        if self.idx < self.nList:
            next = self.outList[ self.idx ]
            self.idx += 1
            return next
        else:
            raise StopIteration

# ======================================================================
# do() calculation
#
# --------------------------------------------------
# Function to compute a probability for a set of variables over
# which to marginalize, 'mvs', and their values provided by the 'pl'
# output from posterior_likelihood().
#
def getMargProbs( ldb, mvs, pl ):
    # Build up array of variables and values to marginalize
    lookup = []
    for mv in mvs:
        curVal = pl[ 'hyp: ' + mv ]
        if curVal == '': curVal = None
        lookup.append( (mv, curVal ))

    # Convert the full array to a dict for call to probability()
    probDict = dict( lookup )
    return ldb.probability( probDict, {}  )

# -----------------------------------
# TBD Docstring
# Perform do() calculation on all values for each of depVars (list of
# column names) for all values of outVar.  Results in a dataframe
# with:
#
# | outVar | outVal | var | val | P( out\|do(var) | P( var|out ) | P( out|var )|
#
def do( ldb, depVars, outVar ):
    # Get values, replacing missing with None for probability call
    outVals = [ v if v != '' else None for v in ldb.column_values( outVar )]

    doDF = pd.DataFrame( columns=( 'outVar', 'outVal', 'var', 'val',
                                   'P(out|do(var))', 'P(var|out)', 'P(out|var)' ))
    
    # Loop through direct dependency variables, selecting one to
    # intervene on at each iteration
    locIdx = 0
    for doVar in depVars:
        # Get values, replacing missig with None for probability call
        doVals = [ v if v != '' else None for v in ldb.column_values( doVar )]

        # Marginalize over everything but the doVar
        margVars = [ var for var in depVars if var != doVar ]
        logging.debug( f'margVars: {margVars}\n' )
        
        # Get the full set, extracted as required in loop below
        fullPL = ldb.posterior_likelihood( [outVar], margVars + [doVar] )
        logging.debug( f'fullPL: {fullPL}\n' )

        for outVal in outVals:

            for doVal in doVals:
                margPL = fullPL[ ( fullPL[ 'evi: '+outVar ] == outVal) & (fullPL[ 'hyp: '+doVar ] == doVal )]
                logging.debug( f'margPL: {margPL}\n' )
                
                rowProb = 0.0
                for idx,row in margPL.iterrows():
                    rowProb += ( getMargProbs( ldb, margVars, row ) * row.Likelihood )

                doDF.loc[ locIdx ] = [ outVar, outVal, doVar, doVal,
                                       rowProb,
                                       ldb.probability( {doVar:doVal}, {outVar:outVal} ),
                                       ldb.probability( {outVar:outVal}, {doVar:doVal} ) ]

                locIdx += 1

    return doDF

        
