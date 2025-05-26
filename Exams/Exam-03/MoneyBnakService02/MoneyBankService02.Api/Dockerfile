# Base para ejecutar el contenedor en producción
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Fase de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["MoneyBnakService02.Api/MoneyBnakService02.Api.csproj", "MoneyBnakService02.Api/"]
COPY ["MoneyBnakService02.Application/MoneyBnakService02.Application.csproj", "MoneyBnakService02.Application/"]
COPY ["MoneyBnakService02.Domain/MoneyBnakService02.Domain.csproj", "MoneyBnakService02.Domain/"]
COPY ["MoneyBnakService02.Infrastructure/MoneyBnakService02.Infrastructure.csproj", "MoneyBnakService02.Infrastructure/"]

RUN dotnet restore "MoneyBankService02.Api/MoneyBankService02.Api.csproj"
COPY . .
WORKDIR "/src/MoneyBankService02.Api"
RUN dotnet build "MoneyBankService02.Api.csproj" -c Release -o /app/build

# Fase de publicación
FROM build AS publish
RUN dotnet publish "MoneyBankService02.Api.csproj" -c Release -o /app/publish

# Fase final con SDK y herramientas necesarias
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MoneyBankService02.Api.dll"]
