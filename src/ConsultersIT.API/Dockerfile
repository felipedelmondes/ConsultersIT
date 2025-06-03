# Dockerfile para ConsultersIT.API (.NET 8)

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia arquivos de projeto e a pasta src
COPY ConsultersIT.sln ./
COPY src/ ./src/
RUN dotnet restore

# Publica a aplicação
WORKDIR /app/src/ConsultersIT.API
RUN dotnet publish -c Release -o /out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "ConsultersIT.API.dll"]

# Comandos para build e execução
# docker build -t consultersit-api -f src/ConsultersIT.API/Dockerfile .
# docker run -d -p 8080:8080 --name consultersit-api consultersit-api
