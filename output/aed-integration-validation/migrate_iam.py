from pathlib import Path
import re

DELIM = '<---- New Query ---->'
root = Path(__file__).resolve().parents[2]
baseline = Path(__file__).parent / 'baseline'
baseline.mkdir(exist_ok=True)

def block(*options, body=''):
    return '<OPTIONS>\n' + '\n'.join(options) + '\n</OPTIONS>\n' + body

for name in ('ICMPCS.txt', 'CSR_IAM_v2.txt'):
    path = root / name
    raw = path.read_bytes()
    backup = baseline / name
    if backup.exists():
        assert backup.read_bytes() == raw
    else:
        backup.write_bytes(raw)
    pieces = raw.decode('utf-8-sig').replace('\r\n', '\n').split(DELIM)
    start = next(i for i,b in enumerate(pieces) if 'Step 2. Apply variables' in b)
    candidates = next(i for i,b in enumerate(pieces) if re.search(r'^/CSV=DATA.csv$', b, re.M))
    reports = [b for b in pieces[candidates:] if any(
        token in b for token in ('/REPORT=HTML-DEFER', '/REPORT=HTML-LAYOUT', '/REPORT=HTML-DELETE')
    )]
    assert len(reports) == 3
    reports = [b.replace(
        'Lot on hold due to suspected MLI NCO. Lot will need to go through 100% XRAY (2846)',
        'Lots flagged for suspected MLI NCO. AED outcomes are recorded in AED_results.csv.'
    ).replace(
        'CSR enabled: <<<DCSR>>><br>ICMPCS MMS Workflow enabled: <<<DMWF>>><br>MMS Signal Tracer enabled: <<<DMST>>>',
        'AED enabled: <<<AED_ENABLED>>><br>Target attribute(s): <<<ATTR_LIST>>><br>Target value: <<<SKIP_OPERATION>>>'
    ).replace('CWF_MLINCO_LOTHOLD', 'CWF_MLINCO_AED') for b in reports]
    core = pieces[start:candidates]
    candidates_block = block(
        '/NODE=.\\', '/OLEDB=SQLite', '/ENGINE=SQLite', '/UN=', '/PW=', '/WORKDIR=.\\',
        '/CSV=AED_CANDIDATES.csv', '/TABLE=IPM_Data.csv', '/HEADERS=FACILITY,LOT',
        '/QUOTECSV=Y', '/PROMPT-TEXT=Select distinct flagged lots for AED',
        body='SELECT DISTINCT facility AS FACILITY, lot AS LOT FROM [IPM_Data];\n',
    )
    result = pieces[:4] + core + [
        candidates_block,
        block('/WORKDIR=.\\', '/UTILITIES={AED} "AED_CANDIDATES.csv"',
              '/PROMPT-TEXT=Apply and verify AED lot attributes'),
        block('/UTILITIES={ROWS-IN-FILE} "AED_CANDIDATES.csv" "SIGNAL" "N"'),
        block('/UTILITIES={IF-THEN} "SIGNAL" "GT" "0"'),
        *reports,
        block('/UTILITIES={END-IF}'),
    ]
    if name == 'CSR_IAM_v2.txt':
        result.append(block('/UTILITIES={END-IF}')) # Existing PARMI nonempty guard.
    result.append(block('/UTILITIES={END-MACRO}'))
    text = ('\n' + DELIM + '\n').join(b.strip() for b in result if b.strip()) + '\n'
    assert not any(token in text for token in ('setsiteparam', '<<<DCSR>>>', 'RoboCopy.va', 'KM\\'))
    path.write_text(text, encoding='utf-8', newline='\n')
    print(f'{name}: {len(pieces)-1} -> {len(result)} blocks; queries preserved')
