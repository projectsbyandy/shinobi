# Project Shinobi
This project demonstrates the use of
- WebAPI
- Entity Framework ORM
  - Use of SqlServer or In-Memory db
- Mocking DBContext for Unit testing
- Layered Configuration handling

## Pre-Req

This project has the option of using a real SqlServer db or In-Memory DB. The default configuration is In-Memory. To change this options, override the option in appsettings

```
 "DbConfiguration": {
    "UseMock": true
  }
```

The following instructions detail the pre-requisites for a Mac setup. If you are using Windows, install SqlServer and skip to the Create Project section.

1. Download Azure Data Studio
	https://learn.microsoft.com/en-us/azure-data-studio/download-azure-data-studio?tabs=win-install%2Cwin-user-install%2Credhat-install%2CmacOS-uninstall%2Credhat-uninstall
        - Open Azure Data Studio.app

2. Docker Setup for SQL Server Edge
      - docker pull mcr.microsoft.com/azure-sql-edge
      - docker run -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=<selected password>’-p 1433:1433 --name sqlserver -d mcr.microsoft.com/azure-sql-edge
      - Test using docker ps -a
         - azure-sql-edge will be listed

## Connecting Azure Data Studio to Sql Server Edge docker instance
- New Connection
    - Server - localhost
    - Username - sa
    - Password - <selected password>
    - Name - SqlServerEdgeDocker
    - Press Connect and you should be presented with the status page

## Create project
Install EF Tools
dotnet tool install --global dotnet-ef

Update the EF Tools
dotnet tool update --global dotnet-ef

We need to install the following packages:
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design

Scaffold
Browse into the project directory
dotnet ef dbcontext scaffold "Server=localhost; Initial Catalog=shinobi; user=sa;Password=<insert here>;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models

Create a appsettings.local.json with the following entry
```
{
  "SqlConnectionDetails": {
    "Password": "<your password>"
  }
}
```
## Run the Setup SQL script to create the mandatory tables

## Troubleshooting

Skipping NuGet package signature verification.
Tools directory '/Users/andrewchang/.dotnet/tools' is not currently on the PATH environment variable.
If you are using zsh, you can add it to your profile by running the following command:

cat << \EOF >> ~/.zprofile

# Add .NET Core SDK tools
export PATH="$PATH:/Users/andrewchang/.dotnet/tools"
EOF




