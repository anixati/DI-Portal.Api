@echo off
SET MIGFILE=Initial_%date:~4,2%%date:~7,2%%date:~10,4%_%time:~0,2%%time:~3,2%%time:~6,2%
echo %MIGFILE%
SET ROOT=%~dp0
REM C:\Projects\dotars-api\
SET ADMROOT=%ROOT%Boards\Boards.Infrastructure
echo Running from %ADMROOT%

ECHO Dropping  and creating Db ...
dotnet-ef database drop -f -p  %ADMROOT% --context BoardsDbContext
dotnet-ef migrations remove -f -p  %ADMROOT% --context BoardsDbContext
dotnet-ef migrations add %MIGFILE% -p  %ADMROOT% --context BoardsDbContext
dotnet-ef database update -p  %ADMROOT% --context BoardsDbContext
dotnet ef migrations script -p  %ADMROOT% --context BoardsDbContext --output tools\sql\schema.sql

ECHO Done!