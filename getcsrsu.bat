@echo off
set PriCSR="\\AZATSHFS.intel.com\AZATAnalysis$\MAOATM\Config\VF_POR_Cfg\ICM_PCS\Patrol\*.___"
set SecCSR="\\KMATSHFS.intel.com\KMATAnalysis$\MAOATM\Config\VF_POR_Cfg\ICM_PCS\Patrol\*.___"
set BakCSR="\\SHUser-ProdAT.intel.com\SHProdATUser$\%username%\Patrol\*.___"
copy %PriCSR% . || copy %SecCSR% . || copy %BAKCSR% .
ren setsiteparam.___ setsiteparam.exe