# Imagen base para tiempo de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Imagen para compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos los archivos de proyecto
COPY MoneyBankService.sln ./
COPY MoneyBankService.Api/MoneyBankService.Api.csproj MoneyBankService.Api/
COPY MoneyBankService.Application/MoneyBankService.Application.csproj MoneyBankService.Application/
COPY MoneyBankService.Domain/MoneyBankService.Domain.csproj MoneyBankService.Domain/
COPY MoneyBankService.Infrastructure/MoneyBankService.Infrastructure.csproj MoneyBankService.Infrastructure/

# Restauramos dependencias
RUN dotnet restore "MoneyBankService.Api/MoneyBankService.Api.csproj"

# Copiamos el resto del código
COPY .. .

# Compilamos el proyecto
WORKDIR "/src/MoneyBankService.Api"
RUN dotnet build "MoneyBankService.Api.csproj" -c Release -o /app/build

# Publicamos
FROM build AS publish
RUN dotnet publish "MoneyBankService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false --self-contained false


ENV ASPNETCORE_ENVIRONMENT=Development

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MoneyBankService.Api.dll"]