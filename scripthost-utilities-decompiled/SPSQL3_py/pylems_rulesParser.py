# ***************************************************************************
# Module:            rulesParser
# Creation date:     January 18, 2024
# Written by:        Farzin Guilak
# Modification:      $Id: rulesParser.py,v 1.7 2024/05/01 17:23:10 fguilak Exp $
# **************************************************************************

import logging
import re

__version__ = '$Id: rulesParser.py,v 1.7 2024/05/01 17:23:10 fguilak Exp $'

# --------------------------------------------------
# Remove all chars in set from a string.
#
def _stripChars( inStr, rmSet ):
    return ''.join( c for c in inStr if c not in rmSet )

# --------------------------------------------------
# Parse a rules file, return tuple of lists of compiled regexps for
# (ignore,categorical,keep).
#
# Note:
#  - Only uses lines starting with 'ignore', 'categorical', or 'keep'
#  - Removes all single and double quotes from regexps
#  - Equivalence not validated with Rust regex library
#
def _parseRules( inFile ):
    try:
        fh = open( inFile, 'r' )
    except:
        raise

    ignores = []
    cats = []
    keeps = []
    
    for line in fh:
        l = line.strip()
        if l:
            if l.startswith( 'ignore' ):
                restr = l.split( ',', 1 )[1].strip('\n\r')
                ignores.append( _stripChars( restr, '"\'' ))

            elif l.startswith( 'categorical' ):
                restr = l.split( ',', 1 )[1].strip('\n\r')
                cats.append( _stripChars( restr, '"\'' ))

            elif l.startswith( 'keep' ):
                restr = l.split( ',', 1 )[1].strip('\n\r')
                keeps.append( _stripChars( restr, '"\'' ))

            elif not l.startswith( '#' ):
                raise ValueError( 'rulesParser|_parseRules invalid rules directive: <' + l + '>' )
        
    return ( list( map( re.compile, ignores )),
             list( map( re.compile, cats )),
             list( map( re.compile, keeps )))

# =========================================================================

class rulesParser:
    """
    `rulesParser` provides functionality to processes variable names against a rules
    file, supporting refinement of ignored names with a `keep` directive.  This
    allows specification of very broad regular expressions to `ignore` while reversing
    the ignore with subsets (or exact names) to `keep`.
    
    Using this module:
     - Instance the `rulesParser` class with a rules file
     - Invoke methods to process rules against columns:  processList() or processName()

    `keep` rules have precedence: if a variable name matches a `keep` regexp, it will
    not be ignored, regardless of how many `ignore` regexps it matches.

    The `categorical` directive implies `keep` so if a variable name that matches an
    `ignore` regexp matches a `categorical`, it will act as an implicit keep.  The
    variable will be kept and overriden to categorical.

    Functions and methods intended to be `public` have documentation you can view from a
    Python interpreter, for example `>>> help(lemsUtilities.rulesParser)`.

    """

    def __init__( self, fileName ):
        self.rulesFile = fileName
        self._reset()

        try:
            self.igRe,self.catRe,self.keepRe = _parseRules( self.rulesFile )
        except IOError as err:
            logging.error( f'rulesParser|init|error reading rules file {err}' )
            raise
        except ValueError as err:
            logging.error( f'rulesParser|init|bad directive in rules file {err}' )
            raise

        logging.debug( f'  rulesParser|init\n' +
                       f'  igRe: <{self.igRe}>\n  catRe: <{self.catRe}>\n  keepRe: <{self.keepRe}>' )


    def _reset( self ):
        self.igRe = None
        self.catRe = None
        self.keepRe = None
        self.outList = []
        
    def processName( self, colName ):
        """
        Assess a variable (column) name against the rules file used to instance the
        parser class.

        Returns:
         - "ignore" if the variable name matches only an ignore regexp
         - "keep" if the variable name matches an ignore *and* keep regexp
         - "categorical" if the variable matches a categorical regexp.  This
           match also provides an implicit keep, i.e. overrides a match to any
           ignore regexp
         - None if no overrides apply.  This is returned if the variable name
           doesn't match regexps for any directives, or if it matches ignore and
           keep, but not categorical.

        """

        igHit = any( regexp.search( colName ) for regexp in self.igRe )
        keepHit = any( regexp.search( colName ) for regexp in self.keepRe )
        catHit = any( regexp.search( colName ) for regexp in self.catRe )
        
        logging.debug( f'  rulesParser|processName\n' +
                       f'  col: <{colName}>\n  igHit: <{igHit}>,  keepHit: <{keepHit}>,  catHit: <{catHit}>' )

        if ( igHit ):
            if ( catHit ):
                logging.info( f'{id(self)}:processName|<{colName}> matched categorical keep rule' )
                return "categorical"

            if ( not keepHit ):
                logging.info( f'{id(self)}:processName|<{colName}> matched ignore rule' )
                return "ignore"
            else:
                logging.info( f'{id(self)}:processName|<{colName}> matched keep rule' )
                # do NOT return here, it may match catRe
                
        if ( catHit ):
            logging.info( f'{id(self)}:processName|<{colName}> matched categorical rule' )
            return "categorical"

        logging.info( f'{id(self)}:processName|<{colName}> no overrides' )
        return None

    def getRulesFileList( self, colNames ):
        for col in colNames:
            logging.debug( f'{id(self)}:getRulesFileList|processing <{col}>' )
            match = self.processName( col )
            if match == "ignore":
                self.outList.append( "ignore,^" + col +  "$" )

            if match == "categorical":
                self.outList.append( "categorical,^" + col +  "$" )
            
        return self.outList
