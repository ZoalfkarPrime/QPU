param(
    [Parameter(Mandatory = $true)]
    [ValidateSet("add", "update", "list", "remove")]
    [string]$Command,

    [string]$Name
)

$project = ".\QPU_DataAccess\QPU_DataAccess.csproj"
$startupProject = ".\QPU_DataAccess\QPU_DataAccess.csproj"

switch ($Command) {
    "add" {
        if ([string]::IsNullOrWhiteSpace($Name)) {
            throw "Provide a migration name. Example: .\scripts\ef-migrations.ps1 -Command add -Name MigrationName"
        }
        dotnet ef migrations add $Name --project $project --startup-project $startupProject --output-dir Migrations
    }
    "update" {
        dotnet ef database update --project $project --startup-project $startupProject
    }
    "list" {
        dotnet ef migrations list --project $project --startup-project $startupProject
    }
    "remove" {
        dotnet ef migrations remove --project $project --startup-project $startupProject
    }
}

exit $LASTEXITCODE
