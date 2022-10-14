#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Pricat.Api/Pricat.Api.csproj", "Pricat.Api/"]
COPY ["Pricat.Infrastructure/Pricat.Infrastructure.csproj", "Pricat.Infrastructure/"]
COPY ["Pricat.Domain/Pricat.Domain.csproj", "Pricat.Domain/"]
COPY ["Pricat.Application/Pricat.Application.csproj", "Pricat.Application/"]
COPY ["Pricat.Utilities/Pricat.Utilities.csproj", "Pricat.Utilities/"]
RUN dotnet restore "Pricat.Api/Pricat.Api.csproj"
COPY . .
WORKDIR "/src/Pricat.Api"
RUN dotnet build "Pricat.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pricat.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pricat.Api.dll"]