CLS
dotnet tool update --global dotnet-ef

$cDir=Get-Location
$stpPrj="$($cDir )\Hosting\Boards.ApiHost"
$migPrj="$($cDir )\Boards\Boards.Infrastructure"
$migFldr="$($cDir )\Boards\Boards.Infrastructure\Migrations"

$ctx="BoardsDbContext";

Write-Host "Deleting migrations " $migFldr
Remove-Item -LiteralPath $migFldr -Force -Recurse
Write-Host "Running from  " $migprj

dotnet-ef database drop -s $stpPrj -p $migPrj  -c $ctx -f --verbose
dotnet-ef database update 0  -s $stpPrj -p $migPrj  -c $ctx
dotnet-ef migrations remove -s $stpPrj -p $migPrj  -c $ctx
dotnet-ef migrations add Initialize -s $stpPrj -p $migPrj  -c $ctx
dotnet-ef database update -s $stpPrj -p $migPrj -c $ctx