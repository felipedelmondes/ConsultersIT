# Dockerfile para ConsultersIT.API (.NET 8)

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia arquivos de projeto e a pasta src
COPY ConsultersIT.sln ./
COPY src/ ./src/
RUN dotnet restore

# Publica a aplicação
RUN dotnet publish src/ConsultersIT.API/ConsultersIT.API.csproj -c Release -o /out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

# Configuração básica
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080

# Configuração da connection string como secret
ENV ConnectionStrings__DefaultConnection=${DB_CONNECTION_STRING:-"Server=localhost;Database=ConsultersIT_Dev;User=sa;Password=DevPassword123;"}

EXPOSE 8080
ENTRYPOINT ["dotnet", "ConsultersIT.API.dll"]
