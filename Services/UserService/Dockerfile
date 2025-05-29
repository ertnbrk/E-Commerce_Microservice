# 1. Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# 2. Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Gerekli .csproj dosyalarını kopyala
COPY ["Services/UserService/UserService.csproj", "Services/UserService/"]
COPY ["SharedKernel/SharedKernel.csproj", "SharedKernel/"]
COPY ["Shared.Logging/Shared.Logging.csproj", "Shared.Logging/"]
COPY ["Shared.Messaging/Shared.Messaging.csproj", "Shared.Messaging/"]

# Tüm kaynakları kopyala
COPY . .

# Build işlemi
WORKDIR /src/Services/UserService
RUN dotnet restore
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# 3. Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 4. Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.dll"]
