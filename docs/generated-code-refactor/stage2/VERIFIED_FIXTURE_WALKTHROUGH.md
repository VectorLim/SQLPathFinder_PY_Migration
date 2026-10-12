# Stage 2 verified source-preserved fixture walkthrough

This is a reproducible **PARTIAL original VG2 fixture**: consecutive local SQLite blocks 3–4 from tests/fixtures/hamizah.txt. Its prior and later Oracle/MARS blocks were **not** executed. The current generated Python and SQL assets were reverified from [GitHub Actions workflow 38203059174](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38203059174), which actually translated and ran the job with deterministic local synthetic inputs. The original fixture/output evidence also appears in [workflow 38182724183](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38182724183). This is not verification of the original ScriptHost engine. CI stdout was normalized for presentation; assertions verify parsed CSV/TSV values and asset-edit reruns.

## Original VG2 (two consecutive blocks)

```text
<OPTIONS>
/RESET=Y
/NODE=.\
/OLEDB=SQLite
/ENGINE=SQLite
/UN=
/PW=
/WORKDIR=.\
/T=Yes
/TS=_20260810212903
/CSV=input.csv
/TABLE=yeuchuan_a0_5547.tab,yeuchuan_a1_5547.tab
/HEADERS=test_lot,tcb_lot,entity,prodgroup3
/DELETE=*Instance*
/INSTANCE=5547
/SQLITE_DT=,test_lot(c),from_to(c),tcb_lot(c),lot_1(c),entity(c),prodgroup3(c)
/QUOTECSV=Y
/HADOOP_SERVER_DEFAULT=ATD_ATM.HADOOP
</OPTIONS>


DROP INDEX IF EXISTS IdxA1;
Create Index IF NOT EXISTS IdxA1 ON [yeuchuan_a1_5547] ([lot_1]);

SELECT /*L0*/  DISTINCT 
          a0.[test_lot] AS [test_lot]
         ,a0.[tcb_lot] AS [tcb_lot]
         ,a1.[entity] AS [entity]
         ,a1.[prodgroup3] AS [prodgroup3]
FROM 
           [yeuchuan_a0_5547] a0
 INNER JOIN [yeuchuan_a1_5547] a1
  ON a0.[tcb_lot] = a1.[lot_1]

<---- New Query ---->

<OPTIONS>
/NODE=.\
/OLEDB=SQLite
/ENGINE=SQLite
/UN=
/PW=
/WORKDIR=.\
/T=
/TS=_20260810212903
/CSV=yeuchuan_SQL_5547.tab
/TABLE=input.csv
/HEADERS=entity,prodgroup3
/INSTANCE=5547
/PROMPT-TEXT=Step 3-1.1. Fetching Text (SQLite) Data
/HEADERS_UNIQUE=Y
/QUOTECSV=Y
/HADOOP_SERVER_DEFAULT=ATD_ATM.HADOOP
</OPTIONS>

SELECT /*L0*/ 
          a0.[entity] AS [entity]
         ,a0.[prodgroup3] AS [prodgroup3]
FROM 
[input] a0
```

## Generated main.py

```python
from vg2c.runtime import SqliteReader, job_entrypoint

@job_entrypoint(__file__)
def run(job):
    # Source block 0, line 1
    job.sql('sql/query_000_input.sql', reader=SqliteReader(), output='input.csv', inputs=['yeuchuan_a0_5547.tab', 'yeuchuan_a1_5547.tab'], header=['test_lot', 'tcb_lot', 'entity', 'prodgroup3'])
    # Source block 1, line 38
    job.sql('sql/query_001_yeuchuan_SQL_5547.sql', reader=SqliteReader(), output='yeuchuan_SQL_5547.tab', inputs=['input.csv'], header=['entity', 'prodgroup3'])

if __name__ == "__main__":
    run()
```

## Generated SQL asset: query_000_input.sql

```sql
-- vg2c-output-headers: ["test_lot", "tcb_lot", "entity", "prodgroup3"]
DROP INDEX IF EXISTS IdxA1;
Create Index IF NOT EXISTS IdxA1 ON [yeuchuan_a1_5547] ([lot_1]);

SELECT /*L0*/  DISTINCT 
          a0.[test_lot] AS [test_lot]
         ,a0.[tcb_lot] AS [tcb_lot]
         ,a1.[entity] AS [entity]
         ,a1.[prodgroup3] AS [prodgroup3]
FROM 
           [yeuchuan_a0_5547] a0
 INNER JOIN [yeuchuan_a1_5547] a1
  ON a0.[tcb_lot] = a1.[lot_1]
```

## Generated SQL asset: query_001_yeuchuan_SQL_5547.sql

```sql
-- vg2c-output-headers: ["entity", "prodgroup3"]
SELECT /*L0*/ 
          a0.[entity] AS [entity]
         ,a0.[prodgroup3] AS [prodgroup3]
FROM 
[input] a0
```


**Current generated-project ownership:** `main.py` no longer declares `BASE_DIR`/`WORK_DIR`, resolves `workdir`, constructs `JobRuntime`, or repeats output `header=` lists. The runtime entrypoint initializes each isolated job and still allows `run(workdir=...)`. Ordinary query output headers come directly from SQL column names and aliases. These two original blocks explicitly supplied `/HEADERS`, so their optional name-based overrides are encoded as harmless first-line SQL comments. Remove that line from an edited SQL asset to use its SQL result column names directly; retaining it preserves the original script's output selection/casing. No .header.json sidecar is emitted.

## Synthetic upstream yeuchuan_a0_5547.tab

```tsv
test_lot	from_to	tcb_lot
TEST-1	F	TCB-1
TEST-2	F	TCB-2
```

## Synthetic upstream yeuchuan_a1_5547.tab

```tsv
lot_1	entity	prodgroup3
TCB-1	ENTITY-A	GROUP-A
OTHER	ENTITY-X	GROUP-X
```

## Verified first result input.csv

```csv
test_lot,tcb_lot,entity,prodgroup3
TEST-1,TCB-1,ENTITY-A,GROUP-A
```

## Verified downstream yeuchuan_SQL_5547.tab

```tsv
entity	prodgroup3
ENTITY-A	GROUP-A
```

## Additional executed regressions

The integration tests assert both full header/row sets and the tab output delimiter, verify that absent upstream input raises FileNotFoundError rather than silently returning an empty relation, and edit the second generated SQL file to replace the entity expression with the literal PATCHED before rerunning. The rerun produces PATCHED,GROUP-A (with a tab separator). See tests/runtime/test_fixture_multistep_hamizah_stage2.py. Runtime/editor optional metadata is not required by this generated job.
