# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiar la solución y los proyectos
COPY MoneyBankService/MoneyBankService.sln ./
COPY MoneyBankService/MoneyBankService.Api/*.csproj ./MoneyBankService.Api/
COPY MoneyBankService/MoneyBankService.Application/*.csproj ./MoneyBankService.Application/
COPY MoneyBankService/MoneyBankService.Domain/*.csproj ./MoneyBankService.Domain/
COPY MoneyBankService/MoneyBankService.Infrastructure/*.csproj ./MoneyBankService.Infrastructure/

# Restaurar dependencias
RUN dotnet restore MoneyBankService.sln

# Copiar el resto del código fuente
COPY MoneyBankService/. ./

# Publicar el proyecto API
WORKDIR /src/MoneyBankService.Api
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "MoneyBankService.Api.dll"]