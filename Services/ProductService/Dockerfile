# Runtime base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy .csproj files for restore step
COPY ["Services/ProductService/ProductService.csproj", "Services/ProductService/"]
COPY ["SharedKernel/SharedKernel.csproj", "SharedKernel/"]
COPY ["Shared.Logging/Shared.Logging.csproj", "Shared.Logging/"]
COPY ["Shared.Messaging/Shared.Messaging.csproj", "Shared.Messaging/"]

# Copy all source code
COPY . .

# Restore and build
WORKDIR /src/Services/ProductService
RUN dotnet restore
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductService.dll"]
