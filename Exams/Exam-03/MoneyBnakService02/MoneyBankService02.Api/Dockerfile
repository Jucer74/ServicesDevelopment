# Base para ejecutar el contenedor en producción
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Fase de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# 🔹 Copiar cada carpeta explícitamente
COPY MoneyBankService02.Api/ MoneyBankService02.Api/
COPY MoneyBankService02.Application/ MoneyBankService02.Application/
COPY MoneyBankService02.Domain/ MoneyBankService02.Domain/
COPY MoneyBankService02.Infrastructure/ MoneyBankService02.Infrastructure/

# 🔹 Restaurar dependencias antes de compilar
RUN dotnet restore "MoneyBankService02.Api/MoneyBankService02.Api.csproj"

WORKDIR "/src/MoneyBankService02.Api"
RUN dotnet build "MoneyBankService02.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Fase de publicación
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MoneyBankService02.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase final con SDK y herramientas necesarias
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# 🔹 Instalar `dotnet ef` dentro del contenedor
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

ENTRYPOINT ["dotnet", "MoneyBankService02.Api.dll"]
