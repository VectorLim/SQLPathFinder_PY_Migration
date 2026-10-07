"""Static legacy-option to public-keyword contract shared with the compiler."""

QUERY_OPTIONS = {
    "NODE": "node", "OLEDB": "oledb", "ENGINE": "engine",
    "UN": "username", "PW": "password", "WORKDIR": "workdir",
    "CSV": "output", "TABLE": "tables", "HEADERS": "headers",
    "CTROW": "ct_rows", "CTVALUE": "ct_value", "CTHEADER": "ct_header",
    "CTARRAY": "ct_array", "RECORD": "record", "RESET": "reset",
    "T": "show_result", "TS": "timestamp", "DELETE": "delete",
    "SQLITE_DT": "sqlite_types", "QUOTECSV": "quote_csv",
    "HEADERS_UNIQUE": "unique_headers", "INSTANCE": "instance",
    "PROMPT-TEXT": "prompt", "HADOOP_SERVER_DEFAULT": "hadoop_server",
}

REPORT_OPTIONS = {
    "INSTANCE": "instance", "PROMPT-TEXT": "prompt",
    "APP_SERVER_DEFAULT": "app_server", "OUTLOOK": "outlook",
    "JSON-ONLY": "json_only", "CHART-INSTANCE": "chart_instance",
}
