
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /src 


COPY MoneyBankService.Api/MoneyBankService.Api.csproj ./MoneyBankService.Api/
COPY MoneyBankService.Application/MoneyBankService.Application.csproj ./MoneyBankService.Application/
COPY MoneyBankService.Domain/MoneyBankService.Domain.csproj ./MoneyBankService.Domain/
COPY MoneyBankService.Infrastructure/MoneyBankService.Infrastructure.csproj ./MoneyBankService.Infrastructure/

WORKDIR /src/MoneyBankService.Api 
RUN dotnet restore 


WORKDIR /src
COPY MoneyBankService.Api/ ./MoneyBankService.Api/
COPY MoneyBankService.Application/ ./MoneyBankService.Application/
COPY MoneyBankService.Domain/ ./MoneyBankService.Domain/
COPY MoneyBankService.Infrastructure/ ./MoneyBankService.Infrastructure/

WORKDIR /src/MoneyBankService.Api
RUN dotnet publish -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build-env /app/publish .

EXPOSE 7000 

ENTRYPOINT ["dotnet", "MoneyBankService.Api.dll"]