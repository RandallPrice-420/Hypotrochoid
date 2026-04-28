@echo off
:: Rainbow Colors in Windows CMD using ANSI Escape Codes
:: Works on Windows 10+ with Virtual Terminal Processing enabled

:: Define ESC character
for /f "delims=" %%A in ('echo prompt $E ^| cmd') do set "ESC=%%A"

:: Reset color
set "RESET=%ESC%[0m"

:: Define some colors (foreground only)
set "RED=%ESC%[31m"
set "ORANGE=%ESC%[38;5;208m"
set "YELLOW=%ESC%[33m"
set "GREEN=%ESC%[32m"
set "BLUE=%ESC%[34m"
set "INDIGO=%ESC%[38;5;54m"
set "VIOLET=%ESC%[35m"

:: Print rainbow
echo %RED%RED%RESET%
echo %ORANGE%ORANGE%RESET%
echo %YELLOW%YELLOW%RESET%
echo %GREEN%GREEN%RESET%
echo %BLUE%BLUE%RESET%
echo %INDIGO%INDIGO%RESET%
echo %VIOLET%VIOLET%RESET%

echo.
pause
