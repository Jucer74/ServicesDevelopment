FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /MoneyBankService

COPY ./MoneyBankService/. .
WORKDIR /MoneyBankService/MoneyBankService.Api
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "MoneyBankService.Api.dll"]