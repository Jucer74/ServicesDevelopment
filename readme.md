## Author

```
Name: Juan David Jimenez
```

## Endpoints Students

```
GET     /api/students
GET     /api/students/{id}
POST    /api/students
PUT     /api/students/{id}
DELETE  /api/students/{id}
```

## Servers

```
http://localhost:3000 - json-server
https://localhost:5156 - .NET
```

## Description

```
This project is a simple example of a REST API using .NET 7.0 and json-server.
```

## Install json-server

```
npm install -g json-server
```

## Run json-server

```
json-server --watch StudentsData.json
```

## Install Newtonsoft.Json

```
dotnet add package Newtonsoft.Json --version 13.0.3
```

## Install System.Net.Http

```
dotnet add package System.Net.Http.Json --version 7.0.1
```

## Install Microsoft.AspNetCore.Mvc.NewtonsoftJson

```
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson --version 7.0.9
```

## Install Microsoft.Extensions.Options

```
dotnet add package Microsoft.Extensions.Options --version 7.0.1
```

## Packs nuget used

```
Project 'Students' has the following package references
   [net7.0]:
   Top-level Package                              Requested   Resolved
   > Microsoft.AspNetCore.Mvc.NewtonsoftJson      7.0.9       7.0.9
   > Microsoft.AspNetCore.OpenApi                 7.0.9       7.0.9
   > Microsoft.Extensions.Options                 7.0.1       7.0.1
   > Newtonsoft.Json                              13.0.3      13.0.3
   > Swashbuckle.AspNetCore                       6.5.0       6.5.0
   > System.Net.Http.Json                         7.0.1       7.0.1
```

## Run the project

```
dotnet run
```
