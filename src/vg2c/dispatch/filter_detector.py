from __future__ import annotations
import re
from vg2c.dispatch.models import SQLFilter

# Define regex patterns and keywords
IDENT_RE = r'[a-zA-Z_][a-zA-Z0-9_]*'
PART_RE = rf'(?:{IDENT_RE}|"{IDENT_RE}"|\[{IDENT_RE}\])'
# A single column reference: qualified (a.b) or unqualified (b)
COL_REF_RE = re.compile(rf'^\s*{PART_RE}(?:\.{PART_RE})*\s*$')

QUALIFIED_COL_RE = re.compile(
    rf'\b(?:{IDENT_RE}|"{IDENT_RE}"|\[{IDENT_RE}\])\.(?:{IDENT_RE}|"{IDENT_RE}"|\[{IDENT_RE}\])\b'
)

SQL_KEYWORDS = {
    'SYSDATE', 'TRUNC', 'NVL', 'NULL', 'AND', 'OR', 'NOT', 'LIKE', 'IN', 'BETWEEN',
    'IS', 'SELECT', 'FROM', 'WHERE', 'ON', 'ROWNUM', 'TO_DATE', 'TO_CHAR', 'DECODE',
    'CASE', 'WHEN', 'THEN', 'ELSE', 'END', 'COALESCE', 'ABS', 'MIN', 'MAX', 'SUM', 'COUNT'
}

JOIN_OPS = {'=', '<>', '!=', '<=', '>=', '<', '>'}

IS_NULL_RE = re.compile(r'(?i)\bIS\s+NULL\s*$')
IS_NOT_RE = re.compile(r'(?i)\bIS\s+NOT\s+NULL\s*$')

def strip_comments(sql: str) -> str:
    # strip multi-line comments
    sql = re.sub(r'/\*.*?\*/', '', sql, flags=re.DOTALL)
    # strip single-line comments
    sql = re.sub(r'--.*', '', sql)
    return sql

def extract_condition_texts(sql: str) -> list[str]:
    sql = strip_comments(sql)
    matches = list(re.finditer(r'\b(WHERE|ON)\b', sql, re.IGNORECASE))
    
    stop_keywords = {
        "WHERE", "ON", "GROUP", "ORDER", "HAVING", "UNION",
        "SELECT", "FROM", "JOIN", "LEFT", "RIGHT", "INNER", "OUTER", "LIMIT"
    }
    
    condition_texts = []
    for match in matches:
        start_idx = match.end()
        depth = 0
        i = start_idx
        while i < len(sql):
            char = sql[i]
            if char == '(':
                depth += 1
            elif char == ')':
                depth -= 1
                if depth < 0:
                    break
            
            if depth == 0:
                if i == start_idx or not (sql[i-1].isalnum() or sql[i-1] == '_'):
                    remaining = sql[i:]
                    word_match = re.match(r'^[a-zA-Z_][a-zA-Z0-9_]*\b', remaining)
                    if word_match:
                        word = word_match.group(0).upper()
                        if word in stop_keywords:
                            break
            i += 1
        chunk = sql[start_idx:i].strip()
        if chunk:
            condition_texts.append(chunk)
    return condition_texts

def split_by_conjunctions(condition_text: str) -> list[str]:
    predicates = []
    current_start = 0
    depth = 0
    i = 0
    n = len(condition_text)
    while i < n:
        char = condition_text[i]
        if char == '(':
            depth += 1
        elif char == ')':
            depth -= 1
        elif depth == 0:
            remaining = condition_text[i:]
            conj_match = re.match(r'^(?:AND|OR)\b', remaining, re.IGNORECASE)
            if conj_match:
                pred = condition_text[current_start:i].strip()
                if pred:
                    predicates.append(pred)
                i += len(conj_match.group(0))
                current_start = i
                continue
        i += 1
    pred = condition_text[current_start:].strip()
    if pred:
        predicates.append(pred)
    return predicates

def strip_outer_parentheses(pred: str) -> str:
    pred = pred.strip()
    while pred.startswith('(') and pred.endswith(')'):
        depth = 0
        matched = True
        for char in pred[1:-1]:
            if char == '(':
                depth += 1
            elif char == ')':
                depth -= 1
                if depth < 0:
                    matched = False
                    break
        if matched and depth == 0:
            pred = pred[1:-1].strip()
        else:
            break
    return pred

def split_predicate_at_depth_0(pred: str) -> tuple[str, str, str] | None:
    operators = [
        (re.compile(r'(?i)^\bNOT\s+LIKE\b'), 'NOT LIKE'),
        (re.compile(r'(?i)^\bLIKE\b'), 'LIKE'),
        (re.compile(r'(?i)^\bNOT\s+IN\b'), 'NOT IN'),
        (re.compile(r'(?i)^\bIN\b'), 'IN'),
        (re.compile(r'(?i)^\bNOT\s+BETWEEN\b'), 'NOT BETWEEN'),
        (re.compile(r'(?i)^\bBETWEEN\b'), 'BETWEEN'),
        (re.compile(r'^<>'), '<>'),
        (re.compile(r'^!='), '!='),
        (re.compile(r'^<='), '<='),
        (re.compile(r'^>='), '>='),
        (re.compile(r'^='), '='),
        (re.compile(r'^<'), '<'),
        (re.compile(r'^>'), '>'),
    ]
    depth = 0
    i = 0
    n = len(pred)
    while i < n:
        char = pred[i]
        if char == '(':
            depth += 1
        elif char == ')':
            depth -= 1
        elif depth == 0:
            remaining = pred[i:]
            for op_regex, op_name in operators:
                m = op_regex.match(remaining)
                if m:
                    lhs = pred[:i].strip()
                    rhs = pred[i + len(m.group(0)):].strip()
                    return lhs, op_name, rhs
        i += 1
    return None

def check_postfix_operators(pred: str) -> tuple[str, str] | None:
    m = IS_NOT_RE.search(pred)
    if m:
        depth = 0
        for char in pred[:m.start()]:
            if char == '(':
                depth += 1
            elif char == ')':
                depth -= 1
        if depth == 0:
            return pred[:m.start()].strip(), 'IS NOT NULL'
            
    m = IS_NULL_RE.search(pred)
    if m:
        depth = 0
        for char in pred[:m.start()]:
            if char == '(':
                depth += 1
            elif char == ')':
                depth -= 1
        if depth == 0:
            return pred[:m.start()].strip(), 'IS NULL'
    return None

def strip_sql_literals(expr: str) -> str:
    expr = re.sub(r"'[^']*'", '', expr)
    expr = re.sub(r'"[^"]*"', '', expr)
    return expr

def clean_identifier(s: str) -> str:
    return s.replace('"', '').replace('[', '').replace(']', '')

def extract_col_refs(expr: str) -> list[str]:
    expr_clean = strip_sql_literals(expr)
    qualified = QUALIFIED_COL_RE.findall(expr_clean)
    if qualified:
        return [clean_identifier(c) for c in qualified]
        
    all_idents = re.findall(rf'\b{IDENT_RE}\b', expr_clean)
    cols = []
    for ident in all_idents:
        upper_ident = ident.upper()
        if upper_ident not in SQL_KEYWORDS and not ident.isdigit():
            cols.append(ident)
    return cols

def detect_filters(sql: str, step_name: str) -> list[SQLFilter]:
    condition_texts = extract_condition_texts(sql)
    filters = []
    for text in condition_texts:
        predicates = split_by_conjunctions(text)
        for pred in predicates:
            pred = strip_outer_parentheses(pred)
            if not pred:
                continue
                
            # Try postfix first
            postfix = check_postfix_operators(pred)
            if postfix is not None:
                lhs, op = postfix
                cols = extract_col_refs(lhs)
                if cols:
                    filters.append(SQLFilter(
                        step_name=step_name,
                        attributes=tuple(sorted(list(set(cols)))),
                        sql_statement=pred
                    ))
                continue
                
            # Try binary next
            binary = split_predicate_at_depth_0(pred)
            if binary is not None:
                lhs, op, rhs = binary
                # Check if it is a table-to-table JOIN condition
                if op in JOIN_OPS and COL_REF_RE.match(lhs) and COL_REF_RE.match(rhs):
                    # It's a join condition, ignore!
                    continue
                    
                # Extract column references
                cols = extract_col_refs(lhs) + extract_col_refs(rhs)
                if cols:
                    filters.append(SQLFilter(
                        step_name=step_name,
                        attributes=tuple(sorted(list(set(cols)))),
                        sql_statement=pred
                    ))
            else:
                # Fallback: extract col refs from the entire predicate
                cols = extract_col_refs(pred)
                if cols:
                    filters.append(SQLFilter(
                        step_name=step_name,
                        attributes=tuple(sorted(list(set(cols)))),
                        sql_statement=pred
                    ))
    return filters
