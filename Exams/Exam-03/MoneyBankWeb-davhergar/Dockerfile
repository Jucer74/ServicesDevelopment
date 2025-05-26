FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MoneyBankWeb_davhergar.csproj", "./"]
RUN dotnet restore "MoneyBankWeb_davhergar.csproj"
COPY . .
RUN dotnet build "MoneyBankWeb_davhergar.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MoneyBankWeb_davhergar.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MoneyBankWeb_davhergar.dll"]
