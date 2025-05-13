# 1. Base image: sadece runtime için
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# 2. Build image: proje derlenir
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Yalnızca .csproj dosyasını kopyala ve restore yap
COPY Services/OrderService/OrderService.csproj Services/OrderService/
RUN dotnet restore Services/OrderService/OrderService.csproj

# Geri kalan tüm dosyaları kopyala
COPY . .

# OrderService dizinine geç ve build yap
WORKDIR /src/Services/OrderService
RUN dotnet build OrderService.csproj -c Release -o /app/build

# 3. Publish stage
FROM build AS publish
RUN dotnet publish OrderService.csproj -c Release -o /app/publish /p:UseAppHost=false

# 4. Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderService.dll"]
