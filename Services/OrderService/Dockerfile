# 1. Base image: sadece runtime için
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# 2. Build image: proje derlenir
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Gerekli .csproj dosyalarını kopyala
COPY ["Services/OrderService/OrderService.csproj", "Services/OrderService/"]
COPY ["SharedKernel/SharedKernel.csproj", "SharedKernel/"]
COPY ["Shared.Logging/Shared.Logging.csproj", "Shared.Logging/"]
COPY ["Shared.Messaging/Shared.Messaging.csproj", "Shared.Messaging/"]

# Tüm kaynak dosyaları kopyala
COPY . .

# Build işlemi
WORKDIR /src/Services/OrderService
RUN dotnet restore
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# 3. Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 4. Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderService.dll"]
