FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY GatewayService/GatewayService.csproj ./GatewayService/
RUN dotnet restore ./GatewayService/GatewayService.csproj

COPY . .
WORKDIR /src/GatewayService
RUN dotnet build GatewayService.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish GatewayService.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GatewayService.dll"]
