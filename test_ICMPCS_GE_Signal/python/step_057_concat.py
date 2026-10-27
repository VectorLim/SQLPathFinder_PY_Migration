
import pandas as pd
import os


current_dir = os.getcwd()

dispo = pd.read_csv('./DispoByScreenDataFile.csv',usecols=lambda x: x.strip().lower() == 'visualid')
kill = pd.read_csv('./unit_killed.csv',usecols=lambda x: x.strip().lower() == 'visualid')
dispo.columns = ['VisualId']
kill.columns = ['VisualId']
result = pd.concat([dispo, kill], ignore_index=True)
result = result.dropna(subset=['VisualId'])
result['VisualId'] = result['VisualId'].astype(str).str.strip()
result = result[result['VisualId'] != '']
result = result.drop_duplicates(subset=['VisualId'], keep='first')
result.to_csv('./dispo_append.csv', index=False)
