@echo off
SET MIGFILE=Initial_Migration
echo %MIGFILE%
SET ROOT=%~dp0
SET ADMROOT=%ROOT%Boards\Boards.Infrastructure
echo Running from %ADMROOT%

ECHO Dropping  and creating Db ...
dotnet-ef database drop -f -p  %ADMROOT% --context BoardsDbContext
dotnet-ef migrations remove -f -p  %ADMROOT% --context BoardsDbContext
dotnet-ef migrations add %MIGFILE% -p  %ADMROOT% --context BoardsDbContext
dotnet-ef database update -p  %ADMROOT% --context BoardsDbContext
dotnet ef migrations script -p  %ADMROOT% --context BoardsDbContext --output tools\sql\schema.sql

ECHO Done!