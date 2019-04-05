# CoreBot Backend API
CoreBot Backend API is the backend part of the CoreBot which is an application you can use in order to track Characters in the game Tibia.

## Prerequisites
.NET Core Framework 2.2.0
SQL Server Database

## Installation
Run the following commands in the Package Manager Console in Visual Studio in order to set the database up:
```sh
$ Add-Migration InitialMigration
$ Update-Migration
```

Compile & run the .net core application by through either Visual Studio or by the console with the following commands:
```sh
$ dotnet build
$ dotnet run
```

## API Endpoints

Following are the API Endpoints you can call in order to get certain information from the Tibia Website.

### HUNTEDLIST
| Endpoint | Action |
| ------ | ------ |
| api/list | GET: Show All lists |
| | POST: Add new list |
| api/list/{id} | GET: Show specified list |
|  | DELETE: Delete specified list |
|  | PUT: Change specified list |
| api/list/{id}/characters | POST: Add specified character to list |
|  | DELETE: Delete specified character from list |
| api/list/{id}/guild | POST: Add all characters from a guild to a specified list |
|  | DELETE: Delete all characters from a guild in a specified list |

### CHARACTER
| Endpoint | Action |
| ------ | ------ |
| api/character | GET: Show all characters |
| api/character/{id} | GET: Show specified character |
| api/character/{id}/huntingspot | POST: Add specified hunting spot to character |
|  | DELETE: Delete specified hunting spot from character |

### HUNTINGSPOT
| Endpoint | Action |
| ------ | ------ |
| api/huntingspot | GET: Show all hunting spots |
|  | POST: Add a hunting spot |
| api/huntingspot/{id} | GET: Show specified hunting spot |
|  | PUT: Change specified hunting spot |
|  | DELETE: Delete specified hunting spot |