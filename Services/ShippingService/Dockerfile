# syntax=docker/dockerfile:1.7

######## 1) Restore (cache için ideal) ########
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS restore
WORKDIR /src
COPY ["Services/ShippingService/ShippingService.csproj", "Services/ShippingService/"]
COPY ["SharedKernel/SharedKernel.csproj", "SharedKernel/"]
COPY ["Shared.Logging/Shared.Logging.csproj", "Shared.Logging/"]
COPY ["Shared.Messaging/Shared.Messaging.csproj", "Shared.Messaging/"]
RUN dotnet restore "Services/ShippingService/ShippingService.csproj"

######## 2) Publish (tek adımda build+optimizasyon) ########
FROM restore AS publish
WORKDIR /src
COPY . .
WORKDIR /src/Services/ShippingService
RUN dotnet publish -c Release -o /app/publish \
    /p:UseAppHost=false \
    /p:SelfContained=false \
    /p:PublishReadyToRun=true \
    /p:TieredPGO=true

######## 3) Runtime (küçük final imaj) ########
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
WORKDIR /app

# Globalization tercihi:
# En küçük imaj için invariant aç:
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
# Tam kültür desteği gerekirse yukarıyı kapat, şunları kullan:
# RUN apk add --no-cache icu-libs
# ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=0

# Güvenli non-root kullanıcı (APP_UID'e bağımlılık yok)
RUN adduser -u 10001 -D appuser
USER 10001

ENV ASPNETCORE_URLS=http://+:8080 \
    DOTNET_EnableDiagnostics=0

# Sadece gerçekten kullandığın portu expose et
EXPOSE 8080

COPY --from=publish /app/publish ./
ENTRYPOINT ["dotnet", "ShippingService.dll"]
