FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src


COPY MoneyBankService/MoneyBankService.sln ./
COPY MoneyBankService/MoneyBankService.Api/*.csproj ./MoneyBankService.Api/
COPY MoneyBankService/MoneyBankService.Application/*.csproj ./MoneyBankService.Application/
COPY MoneyBankService/MoneyBankService.Domain/*.csproj ./MoneyBankService.Domain/
COPY MoneyBankService/MoneyBankService.Infrastructure/*.csproj ./MoneyBankService.Infrastructure/


RUN dotnet restore MoneyBankService.sln


COPY MoneyBankService/. ./


WORKDIR /src/MoneyBankService.Api
RUN dotnet publish -c Release -o /src/publish


FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /src
COPY --from=build /src/publish .


ENTRYPOINT ["dotnet", "MoneyBankService.Api.dll"]


