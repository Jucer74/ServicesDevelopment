#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
ARG envName=Development
ENV ASPNETCORE_ENVIRONMENT="${envName}"
ENV DOTNET_USE_POLLING_FILE_WATCHER=1
ENV NUGET_PACKAGES=/.nuget/fallbackpackages2
ENV NUGET_FALLBACK_PACKAGES=/.nuget/fallbackpackages;/.nuget/fallbackpackages2
ENV PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin
ENV APP_UID=1654
ENV ASPNETCORE_HTTP_PORTS=7000
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV ASPNETCORE_URLS=http://+:7000
ENV DOTNET_VERSION=8.0.0-rc.2.23479.6
ENV ASPNET_VERSION=8.0.0-rc.2.23480.2
ENV TZ="America/New_York"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MoneyBankService.Api/MoneyBankService.Api.csproj", "MoneyBankService.Api/"]
COPY ["MoneyBankService.Domain/MoneyBankService.Domain.csproj", "MoneyBankService.Domain/"]
COPY ["MoneyBankService.Application/MoneyBankService.Application.csproj", "MoneyBankService.Application/"]
COPY ["MoneyBankService.Infrastructure/MoneyBankService.Infrastructure.csproj", "MoneyBankService.Infrastructure/"]

RUN dotnet restore "./MoneyBankService.Api/./MoneyBankService.Api.csproj"
COPY . .
WORKDIR "/src/MoneyBankService.Api"
RUN dotnet build "./MoneyBankService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MoneyBankService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MoneyBankService.Api.dll"]