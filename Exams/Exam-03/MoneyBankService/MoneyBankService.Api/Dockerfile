FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MoneyBankService.Api/MoneyBankService.Api.csproj", "MoneyBankService.Api/"]
COPY ["MoneyBankService.Application/MoneyBankService.Application.csproj", "MoneyBankService.Application/"]
COPY ["MoneyBankService.Domain/MoneyBankService.Domain.csproj", "MoneyBankService.Domain/"]
COPY ["MoneyBankService.Infrastructure/MoneyBankService.Infrastructure.csproj", "MoneyBankService.Infrastructure/"]
RUN dotnet restore "MoneyBankService.Api/MoneyBankService.Api.csproj"
COPY . .
WORKDIR "/src/MoneyBankService.Api"
RUN dotnet build "MoneyBankService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MoneyBankService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MoneyBankService.Api.dll"]
