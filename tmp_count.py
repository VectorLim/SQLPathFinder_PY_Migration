import re

text = open("tests/fixtures/actual_script.txt", encoding="utf-8").read()
m = "/*BEGIN SQL*/"
e = "/*END SQL*/"
beginpos = [j for j in range(len(text)) if text.startswith(m, j)]
target = text.find("SUBPLANEANGLEX")
for bp in beginpos:
    ep = text.index(e, bp)
    if bp <= target <= ep:
        block = text[bp : ep + len(e)]
        print(
            "FOUND BLOCK lines",
            text[:bp].count("\n") + 1,
            "-",
            text[:ep].count("\n") + 1,
        )
        print(
            "opens:",
            block.count("("),
            "closes:",
            block.count(")"),
            "imbalance:",
            block.count("(") - block.count(")"),
        )
        expanded1 = re.sub(r"SQL_Get_CSV_List\([^)]*\)", "('A','B')", block)
        print(
            "after macro returns ( ): imbalance=",
            expanded1.count("(") - expanded1.count(")"),
        )
        expanded2 = re.sub(r"SQL_Get_CSV_List\([^)]*\)", "('A','B'))", block)
        print(
            "after macro returns ( )): imbalance=",
            expanded2.count("(") - expanded2.count(")"),
        )
        break
