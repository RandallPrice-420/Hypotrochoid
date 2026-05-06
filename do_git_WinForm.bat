@echo off

:: Define ESC variable (ASCII 27)
for /F %%A in ('echo prompt $E ^| cmd') do set "ESC=%%A"

:: -----------------------------------------------------------------------------
:: Color codes:
::   30 = Black   31 = Red     32 = Green
::   33 = Yellow  34 = Blue    35 = Magenta
::   36 = Cyan    37 = White
::   90–97 = Bright colors
:: -----------------------------------------------------------------------------

set _black=%ESC%[30m
set _red=%ESC%[31m
set _green=%ESC%[32m
set _yellow=%ESC%[33m
set _blue=%ESC%[34m
set _magenta=%ESC%[35m
set _cyan=%ESC%[36m
set _white=%ESC%[37m

set b_black=%ESC%[90m
set b_red=%ESC%[91m
set b_green=%ESC%[92m
set b_yellow=%ESC%[93m
set b_blue=%ESC%[94m
set b_magenta=%ESC%[95m
set b_cyan=%ESC%[96m
set b_white=%ESC%[97m

:: echo %ESC%[30;43mBlack on Yellow%ESC%[0m and %ESC%[37;41mWhite on Red%ESC%[0m
set _white_black=%ESC%[37;40m
set _white_red=%ESC%[37;41m
set _black_yellow=%ESC%[30;43m

:: ----------------------------------------------------------------------------
::  Configure some git settings.
:: ----------------------------------------------------------------------------
set project_name=Spirograph_v1
set local_directory=C:\Repos\WinForm\%project_name%
set remote_origin=https://github.com/RandallPrice-420/%project_name%

echo %_cyan%----------------------------------------------------------------------------
echo                                  %_black_yellow%IMPORTANT:%_white_black%
echo %_cyan%----------------------------------------------------------------------------%_white%
echo   1.  %_green%Close Unity and Visual Studio BEFORE running this batch file.%_white%
echo.
echo   2.  %_green%Do NOT add a .gitignore or a README.md file in github:%_white%
echo       %_green%a.  Add the .gitignore file to you Unity project.%_white%
echo       %_green%b.  The README.md file is created from this batch file.%_white%
echo %_cyan%----------------------------------------------------------------------------%_white%
echo.
set GIT_TRACE_PACKET=1
set GIT_TRACE=1
set GIT_CURL_VERBOSE=1

:: ----------------------------------------------------------------------------
::  Display the project information.
:: ----------------------------------------------------------------------------
echo project_name.....:  %_yellow%%project_name%%_white%
echo local_directory..:  %_yellow%%local_directory%%_white%
echo remote_origin....:  %_yellow%%remote_origin%%_white%

:: ----------------------------------------------------------------------------
::  Check if the remote repository is already created in GitHub.
:: ----------------------------------------------------------------------------
:Check_Repository
echo.
set /p check="%_blue%Is the %_yellow%%project_name%%_blue% repository in GitHub? (%_red%Enter%_blue%=Y, Y, N)  "
if not defined check ( set "check=y" )
if /I "%check%"=="y" goto Repository_Ready
if /I "%check%"=="n" goto Prompt_Create_Repository
echo %_magenta%You must enter %b_red%Y%_magenta% or %b_red%N%_magenta%.%_white%
goto Check_Repository

:: ----------------------------------------------------------------------------
::  Prompt to create the remote repository in GitHub.
:: ----------------------------------------------------------------------------
:Prompt_Create_Repository
echo.
set /p open="%_blue%Create the %_yellow%%project_name%%_blue% repository in GitHub? (%_red%Enter%_blue%=Y, Y, N)  "
if not defined open ( set "open=y" )
if /I "%open%"=="y" goto Create_Repository
if /I "%open%"=="n" goto No_Repository
echo.
echo %_magenta%You must enter %b_red%Y%_magenta% or %b_red%N%_magenta%.%_white%
goto Prompt_Create_Repository

:: ----------------------------------------------------------------------------
::  Create the remote repository in GitHub.
::   https://github.com/RandallPrice-420?tab=repositories
:: ----------------------------------------------------------------------------
:Create_Repository
echo.
echo Creating %project_name% repository in GitHub...
:: gh auth login
gh repo create %remote_origin% --public
start msedge %remote_origin%

:: ----------------------------------------------------------------------------
::  Repository in GitHub, prompt for the step to perform.
:: ----------------------------------------------------------------------------
:Repository_Ready
echo.
set /p step= "%_blue%Enter the step to perform (%b_red%F%_blue% = First time, %b_red%A%_blue% = ADD changes and commit, %b_red%Q%_blue% = Quit):  "%_white%
:: echo You entered:  %step%
if /I "%step%"=="f" goto First_Time
if /I "%step%"=="a" goto Add_And_Commit
if /I "%step%"=="q" goto Done
echo %_magenta%You must enter %b_red%F%_magenta%, %b_red%A%_magenta% or %b_red%Q%_maghenta%.
goto Repository_Ready

:First_Time
:: ----------------------------------------------------------------------------
:: One-time configuration for this project.
::
::  - Configure some github global settings
::  - Delete the README.md file if it exists
::  - Create the README.md file
::  - Initialize the repository
::  - Add and commit the README.md file
::  - Refresh the local files from the master branch
::  - Set the remote origin  
::  - Push to the remote repository
::  - Show the status
:: ----------------------------------------------------------------------------
echo.
git config --global --add safe.directory %local_directory%
git config --global user.email "randall_price@hotmail.com"
git config --global user.name  "Randall Price"

:: ----------------------------------------------------------------------------
::  These settings are to resolve the following error:
::    error: RPC failed; HTTP 408 curl 22 The requested URL returned error: 408
::    send-pack: unexpected disconnect while reading sideband packet
::    fatal: the remote end hung up unexpectedly
:: 157286400
:: ----------------------------------------------------------------------------
git config --global http.postBuffer 524288000
git config --global core.compression 0
echo.

set filePath=README.md
if exist %filePath% (
     del %filePath%
    echo %filePath% file deleted.
)
echo ^<h1^>%project_name%^<^/h1^>>> %filePath%
echo.>> %filePath%
echo %project_name% sample C# WinForm application.>> %filePath%
echo.>> %filePath%

git init
git add %filePath%
git commit -m "Initial project upload."
git branch -M master
git remote add origin %_yelllow%%remote_origin%%_white%
git push -u origin master
git status

echo.
echo %b_green%First time configuration:%_white%
echo   %b_green%- %_yellow%%filePath%%b_green% created and commited.%_white%
echo.


:Add_And_Commit
:: ----------------------------------------------------------------------------
::  - Prompt for the commit message
::      Example:  Added Part 1 - Spaceship Controls and Part 2 - Bullets.
::  - Add and commit the changes
::  - Push to the remote repository
:: ----------------------------------------------------------------------------
echo.
set "defaultValue=Initial project upload."
set /p "commit_message=%_blue%Enter commit message (%_red%Enter%_blue% = <%_red%%defaultValue%%_blue%>, %b_red%Q%_blue% = Quit):  "
if /I "%commit_message%"=="q" goto Done
if not defined commit_message ( set "commit_message=%defaultValue%" )
echo %_magenta%You entered: %_red%%commit_message%%_white%

git pull origin master
git add .
git commit -m "%commit_message%"
git push -u origin master

echo.
echo %b_green%- Changed files successfully committed and pushed to %b_blue%%remote_origin%%_white%
echo.
pause
exit


:: ----------------------------------------------------------------------------
::  Repository in GitHub.
:: ----------------------------------------------------------------------------
:No_Repository
echo.
echo %b_red%- Repository is NOT in GitHub and will NOT be created.%_white%
echo.
pause
exit

:: ----------------------------------------------------------------------------
::  Finished!
:: ----------------------------------------------------------------------------
:Done
echo.
echo Done
pause
exit
