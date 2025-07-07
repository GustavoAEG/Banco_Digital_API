# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln .
COPY ContaCorrente.API/*.csproj ./ContaCorrente.API/
COPY ContaCorrente.Application/*.csproj ./ContaCorrente.Application/
COPY ContaCorrente.Domain/*.csproj ./ContaCorrente.Domain/
COPY ContaCorrente.Infrastructure/*.csproj ./ContaCorrente.Infrastructure/
COPY ContaCorrente.Tests/*.csproj ./ContaCorrente.Tests/    
RUN dotnet restore

COPY . .
RUN dotnet publish ContaCorrente.API -c Release -o /app/out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./
EXPOSE 80
ENTRYPOINT ["dotnet", "ContaCorrente.API.dll"]
