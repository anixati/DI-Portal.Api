@echo off
FOR /f %%a IN ('WMIC OS GET LocalDateTime ^| FIND "."') DO SET DTS=%%a
SET DateTime=%DTS:~0,4%-%DTS:~4,2%-%DTS:~6,2%_%DTS:~8,2%-%DTS:~10,2%-%DTS:~12,2%
echo %DateTime%
SET MIGFILE=Update_%DateTime%
echo %MIGFILE%
SET ROOT=%~dp0
REM C:\Projects\dotars-api\
SET ADMROOT=%ROOT%Boards\Boards.Infrastructure
echo Running from %ADMROOT%

ECHO creating migr ..
dotnet-ef migrations add %MIGFILE% -p  %ADMROOT% --context BoardsDbContext
dotnet-ef database update -p  %ADMROOT% --context BoardsDbContext
dotnet-ef migrations script -p  %ADMROOT% --context BoardsDbContext --output tools\sql\updateschema.sql

ECHO Done!