# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_USE_POLLING_FILE_WATCHER=true  
ENV ASPNETCORE_URLS=http://+:80  
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build  
WORKDIR /app

# Copy everything else and build
COPY *.sln .
COPY . .
WORKDIR /app/People.Api
RUN dotnet restore

FROM build AS publish
RUN dotnet publish -c Release -o out


# Build runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/People.Api/out .
ENTRYPOINT ["dotnet", "People.Api.dll"]