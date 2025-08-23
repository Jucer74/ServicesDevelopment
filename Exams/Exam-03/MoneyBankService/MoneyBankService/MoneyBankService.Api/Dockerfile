# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 7000
EXPOSE 7001

ENV ASPNETCORE_URLS="https://+:7001;http://+:7000"
ENV ASPNETCORE_Kestrel__Certificates__Default__Password="your-password"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path="/root/.aspnet/https/aspnetapp.pfx"
# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MoneyBankService.Api/MoneyBankService.Api.csproj", "MoneyBankService.Api/"]
COPY ["MoneyBankService.Application/MoneyBankService.Application.csproj", "MoneyBankService.Application/"]
COPY ["MoneyBankService.Domain/MoneyBankService.Domain.csproj", "MoneyBankService.Domain/"]
COPY ["MoneyBankService.Infrastructure/MoneyBankService.Infrastructure.csproj", "MoneyBankService.Infrastructure/"]
RUN dotnet restore "./MoneyBankService.Api/MoneyBankService.Api.csproj"
COPY . .
WORKDIR "/src/MoneyBankService.Api"
RUN dotnet build "./MoneyBankService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MoneyBankService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MoneyBankService.Api.dll"]