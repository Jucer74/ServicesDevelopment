# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copia y publica la app
COPY *.sln .
COPY UserManagement.Domain/*.csproj ./UserManagement.Domain/
COPY UserManagement.Application/*.csproj ./UserManagement.Application/
COPY UserManagement.Infrastructure/*.csproj ./UserManagement.Infrastructure/
COPY UserManagement.Api/*.csproj ./UserManagement.Api/
RUN dotnet restore

COPY . .
WORKDIR /source/UserManagement.Api
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
# Copiar la base de datos desde la carpeta UserManagement.Api
COPY UserManagement.Api/PeopleDB.db /app/PeopleDB.db
EXPOSE 80
ENTRYPOINT ["dotnet", "UserManagement.Api.dll"]
