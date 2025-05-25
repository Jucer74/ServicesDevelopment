# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copiar solución y proyectos
COPY *.sln .
COPY Domain/MoneyBankService.Domain/*.csproj ./MoneyBankService.Domain/
COPY Application/MoneyBankService.Application/*.csproj ./MoneyBankService.Application/
COPY Infrastructure/MoneyBankService.Infrastructure/*.csproj ./MoneyBankService.Infrastructure/
COPY Api/MoneyBankService.Api/*.csproj ./MoneyBankService.Api/

# Restaurar dependencias
RUN dotnet restore

# Copiar todo el código fuente
COPY . .

# Publicar el proyecto API
WORKDIR /source/MoneyBankService.Api
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiar los archivos publicados
COPY --from=build /app/publish .

# Configuración adicional si necesitas base de datos
# COPY MoneyBankService.Api/scripts/*.sql ./scripts/

EXPOSE 80
ENTRYPOINT ["dotnet", "MoneyBankService.Api.dll"]