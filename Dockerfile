# Usar uma imagem base do .NET 8
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DesafioBackend.API/DesafioBackend.API.csproj", "DesafioBackend.API/"]
COPY ["DesafioBackend.Application/DesafioBackend.Application.csproj", "DesafioBackend.Application/"]
COPY ["DesafioBackend.Infrastructure/DesafioBackend.Infrastructure.csproj", "DesafioBackend.Infrastructure/"]
COPY ["DesafioBackend.Core/DesafioBackend.Core.csproj", "DesafioBackend.Core/"]
RUN dotnet restore "DesafioBackend.API/DesafioBackend.API.csproj"

COPY . .
WORKDIR "/src/DesafioBackend.API"
RUN dotnet publish "DesafioBackend.API.csproj" -c Release -o /app/publish

# Usar a imagem base para rodar a aplicação
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DesafioBackend.API.dll"]
