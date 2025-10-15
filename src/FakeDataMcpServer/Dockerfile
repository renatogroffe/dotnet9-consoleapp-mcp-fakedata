FROM mcr.microsoft.com/dotnet/sdk:9.0.306 AS build-env
WORKDIR /app

# Copiar csproj e restaurar dependencias
COPY *.csproj ./
RUN dotnet restore

# Build da aplicacao
COPY . ./
RUN dotnet publish -c Release -o out

# Build da imagem
FROM mcr.microsoft.com/dotnet/runtime:9.0.10
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "FakeDataMcpServer.dll"]