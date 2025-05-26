FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MoneyBamkWeb-josoterad.csproj", "./"]
RUN dotnet restore "MoneyBamkWeb-josoterad.csproj"
COPY . .
RUN dotnet build "MoneyBamkWeb-josoterad.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MoneyBamkWeb-josoterad.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MoneyBamkWeb-josoterad.dll"]
