FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /MoneyBankService

COPY ./MoneyBankService/. .

RUN dotnet restore MoneyBankService.sln
RUN dotnet publish MoneyBankService.Api/MoneyBankService.Api.csproj -c Release -o /app


FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "MoneyBankService.Api.dll"]
