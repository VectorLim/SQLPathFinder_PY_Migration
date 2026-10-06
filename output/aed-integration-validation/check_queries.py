import re
from pathlib import Path

root = Path(__file__).resolve().parents[2]
for name in ('ICMPCS.txt', 'CSR_IAM_v2.txt'):
    before = (Path(__file__).parent / 'baseline' / name).read_text(encoding='utf-8-sig')
    after = (root / name).read_text(encoding='utf-8-sig')
    pieces = before.split('<---- New Query ---->')
    start = next(i for i, b in enumerate(pieces) if 'Step 2. Apply variables' in b)
    end = next(i for i, b in enumerate(pieces) if re.search(r'^/CSV=DATA.csv$', b, re.M))
    core = [b.split('</OPTIONS>', 1)[1].strip() for b in pieces[start:end]
            if '/ENGINE=' in b]
    assert core and all(body in after for body in core), name
    assert after.count('/UTILITIES={AED}') == 1
    assert not any(token in after for token in ('setsiteparam', 'RoboCopy.va', '<<<DCSR>>>'))
    print(name, len(core), 'detection query bodies unchanged; one AED task')
