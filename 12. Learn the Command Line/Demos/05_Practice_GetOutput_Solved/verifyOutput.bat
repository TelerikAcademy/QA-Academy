@echo off
findstr "Parameter" output.txt | findstr "2"
IF %ERRORLEVEL% EQU 0 (
	echo Parameter 2 exists in output
) ELSE (
	echo Parameter 2 does NOT exist in output 
)