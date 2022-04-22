@echo off
SET ROOT=%~dp0
REM C:\Projects\dotars-api\
SET ADMROOT=%ROOT%Boards\Boards.Infrastructure
echo Running from %ADMROOT%

ECHO Dropping  and creating Db ...
dotnet-ef database drop -f -p  %ADMROOT% --context BoardsDbContext
dotnet-ef migrations remove -f -p  %ADMROOT% --context BoardsDbContext
dotnet-ef migrations add InitVersion -p  %ADMROOT% --context BoardsDbContext
dotnet-ef database update -p  %ADMROOT% --context BoardsDbContext

ECHO Done!