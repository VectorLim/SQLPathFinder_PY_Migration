import pandas as pd
variable_file = r"C:\Data\examples\Variables.txt"
output_file = r"C:\Data\examples\processed_list.csv"

# Mode	Description
# 'r'	Open a file for reading. (default)
# 'w'	Open a file for writing. Creates a new file if it does not exist or truncates the file if it exists.
# 'x'	Open a file for exclusive creation. If the file already exists, the operation fails.
# 'a'	Open for appending at the end of the file without truncating it. Creates a new file if it does not exist.
# 't'	Open in text mode. (default)
# 'b'	Open in binary mode.
# '+'	Open a file for updating (reading and writing)

first_line = True
name = ''
variable_type = ''
variable_type_num = ''
split_type = ''
role = ''
role_num = ''
with open(output_file, 'w') as outfile:
    with open(variable_file, 'r') as infile:
        outfile.write('name,variable_type,split_type,role\n')
        for line in infile:
            if line.find('[Name]') >=0:
                if first_line == False:
                    outfile.write(name + ',' + variable_type  + ',' + split_type  + ',' + role + '\n')
                name = line.strip().split('[Name]')[1].strip().replace('"', '')
                variable_type = ''
                variable_type_num = ''
                split_type = ''
                role = ''
                role_num = ''
                first_line = False
            if line.find('[Modeling type]') >= 0:
                variable_type_num = line.strip().split('[Modeling type]')[1].strip().replace('"', '')
                if variable_type_num == '0':
                    variable_type = 'Categorical'
                elif variable_type_num == '1':
                    variable_type = 'Numeric'
                elif variable_type_num == '2':
                    variable_type = 'Ordinal'
            if line.find('[Is one vs the rest]') >= 0:
                if split_type == '' or split_type == '0':
                    split_type = line.strip().split('[Is one vs the rest]')[1].strip().replace('"', '')
            if line.find('[Is interval]') >= 0:
                if split_type == '' or split_type == '0':
                    split_type = line.strip().split('[Is interval]')[1].strip().replace('"', '')
            if line.find('[Role]') >= 0:
                role_num = line.strip().split('[Role]')[1].strip().replace('"', '')
                if role_num == '0':
                    role = 'Input'
                elif role_num == '1':
                    role = 'Output'
                elif role_num == '2':
                    role = 'Input/Output'
                elif role_num == '3':
                    role = 'Ignored'



#
#
#
#
#
#
#
# line = '      [Name] "SORT_LOT"'
# line.strip().split('[Name]')[1].strip().replace('"','')
#
# line = '[Modeling type] 0'
# line.strip().split('[Modeling type]')[1].strip().replace('"','')
#
