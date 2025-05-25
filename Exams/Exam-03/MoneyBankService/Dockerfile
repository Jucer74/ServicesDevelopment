# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solución y proyectos
COPY ["MoneyBankService.sln", "."]
COPY ["MoneyBankService.Domain/MoneyBankService.Domain.csproj", "MoneyBankService.Domain/"]
COPY ["MoneyBankService.Application/MoneyBankService.Application.csproj", "MoneyBankService.Application/"]
COPY ["MoneyBankService.Infrastructure/MoneyBankService.Infrastructure.csproj", "MoneyBankService.Infrastructure/"]
COPY ["MoneyBankService.Api/MoneyBankService.Api.csproj", "MoneyBankService.Api/"]

# Restaurar dependencias
RUN dotnet restore "MoneyBankService.sln"

# Copiar todo el código
COPY . .

# Publicar
WORKDIR "/src/MoneyBankService.Api"
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MoneyBankService.Api.dll"]