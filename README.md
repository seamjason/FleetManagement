# Fleet Management API

Fleet Management POC Project is configured for Visual Studio 2022 or higher.

Source code repo can be found online at https://github.com/seamjason/FleetManagement/

## Installation
* Ensure the .NET 7 SDK is installed

https://dotnet.microsoft.com/en-us/download/visual-studio-sdks

* Unzip the contents of the .zip package to a local folder.

* Open a Powershell command window, and navigate to the folder containing `fleetmanagement.csproj` (i.e. `src/FleetManagement/`).

* Run command `dotnet restore`.

* Navigate to the folder containing `fleetmanagementtests.csproj` (i.e. `tests/FleetManagementTests').

* Run command `dotnet restore`.

* The local Sqlite database used for testing is included in the .zip package.  However, it can be rebuilt by navigating to the `data` folder and typing `sqlite3 fleetmanagement.db < fleetmanagement.sql`.

* Open `FleetManagement.sln` from the top level folder in Visual Studio 2022.

* In the Visual Studio Solution Explorer, navigate to `src/FleetManagement/appsettings.json/appsettings.Development.json`.  Open the file, and edit the `sqliteConnectionString` setting to contain the absolute path to the `fleetmanagement.db` file in the `data` directory.

## Unit Tests

* Run the unit tests, by right-clicking the `FleetManagementTests` project in Solution Explorer, and click `Run Tests`.

## Running the application

* Start the API by clicking the `> https` button on the toolbar.  This should open a browser and display the Swagger docs for the API.  Note that Visual Studio will likely prompt you to trust and install the self-signed certificate.

* Open the `UI/index.html` file in a browser, from the local file system.  This contains only client-side code, so no server is necessary.

## Deployment

This project is configured for Sqlite, for local development/testing only.  In a production environment, I would recommend using Azure SQL Server.  The API can easily be published to an Azure application container.

