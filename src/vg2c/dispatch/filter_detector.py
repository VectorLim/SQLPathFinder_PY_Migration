from __future__ import annotations

import re

from vg2c.dispatch.models import SQLFilter

# Match qualified attribute like v1.batch_id or [v1].[batch_id] or "v1"."batch_id"
IDENT_RE = r'[a-zA-Z_][a-zA-Z0-9_]*'
ATTR_RE = re.compile(
    r'^(?:' + IDENT_RE + r'|"' + IDENT_RE + r'"|\[' + IDENT_RE + r'\])\.'
    r'(?:' + IDENT_RE + r'|"' + IDENT_RE + r'"|\[' + IDENT_RE + r'\])$'
)

QUALIFIED_RE = re.compile(
    r'(?:' + IDENT_RE + r'|"' + IDENT_RE + r'"|\[' + IDENT_RE + r'\])\.'
    r'(?:' + IDENT_RE + r'|"' + IDENT_RE + r'"|\[' + IDENT_RE + r'\])'
)

UNQUALIFIED_RE = re.compile(r'\b[a-zA-Z_][a-zA-Z0-9_]*\b')

SQL_KEYWORDS = {
    'SYSDATE', 'TRUNC', 'NVL', 'NULL', 'AND', 'OR', 'NOT', 'LIKE', 'IN', 'BETWEEN',
    'IS', 'SELECT', 'FROM', 'WHERE', 'ON', 'ROWNUM', 'TO_DATE', 'TO_CHAR', 'DECODE',
    'CASE', 'WHEN', 'THEN', 'ELSE', 'END', 'COALESCE', 'ABS', 'MIN', 'MAX', 'SUM', 'COUNT'
}

def strip_comments(sql: str) -> str:
    sql = re.sub(r'/\*.*?\*/', '', sql, flags=re.DOTALL)
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
            elif depth == 0:
                if i == start_idx or not (sql[i-1].isalnum() or sql[i-1] == '_'):
                    word_match = re.match(r'^[a-zA-Z_][a-zA-Z0-9_]*\b', sql[i:])
                    if word_match and word_match.group(0).upper() in stop_keywords:
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
            conj_match = re.match(r'^(?:AND|OR)\b', condition_text[i:], re.IGNORECASE)
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

def split_predicate(pred: str) -> tuple[str, str, str] | None:
    operators = ['<>', '>=', '<=', '=', '>', '<']
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
            for op in operators:
                if remaining.startswith(op):
                    lhs = pred[:i].strip()
                    rhs = pred[i + len(op):].strip()
                    return lhs, op, rhs
        i += 1
    return None

def clean_identifier(s: str) -> str:
    return s.replace('"', '').replace('[', '').replace(']', '')

def extract_attributes(lhs: str) -> list[str]:
    qualified = QUALIFIED_RE.findall(lhs)
    if qualified:
        return [clean_identifier(q) for q in qualified]
        
    lhs_clean = re.sub(r"'[^']*'", '', lhs)
    lhs_clean = re.sub(r'"[^"]*"', '', lhs_clean)
    
    idents = UNQUALIFIED_RE.findall(lhs_clean)
    cols = []
    for ident in idents:
        if ident.upper() not in SQL_KEYWORDS and not ident.isdigit():
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
                
            split = split_predicate(pred)
            if split is None:
                continue
                
            lhs, op, rhs = split
            
            # Table-to-table equality join
            if op == '=' and ATTR_RE.match(lhs) and ATTR_RE.match(rhs):
                continue
                
            attrs = extract_attributes(lhs)
            if attrs:
                filters.append(SQLFilter(
                    step_name=step_name,
                    attributes=tuple(sorted(list(set(attrs)))),
                    sql_statement=pred
                ))
    return filters
