# Imagen base para ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Imagen base para construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solución y proyectos por separado para maximizar uso de caché
COPY ["MoneyBankService.sln", "./"]
COPY ["MoneyBankService.Api/MoneyBankService.Api.csproj", "MoneyBankService.Api/"]
COPY ["MoneyBankService.Application/MoneyBankService.Application.csproj", "MoneyBankService.Application/"]
COPY ["MoneyBankService.Domain/MoneyBankService.Domain.csproj", "MoneyBankService.Domain/"]
COPY ["MoneyBankService.Infrastructure/MoneyBankService.Infrastructure.csproj", "MoneyBankService.Infrastructure/"]

# Restaurar dependencias
RUN dotnet restore "MoneyBankService.sln"

# Copiar todo el contenido solo después de restaurar
COPY . .

# Construir el proyecto en modo Release
WORKDIR "/src/MoneyBankService.Api"
RUN dotnet publish "MoneyBankService.Api.csproj" -c Release -o /app/publish

# Imagen final con solo los archivos necesarios
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MoneyBankService.Api.dll"]
