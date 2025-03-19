FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Fase de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BistroPulseApi/BistroPulseApi.csproj", "BistroPulseApi/"]
RUN dotnet restore "BistroPulseApi/BistroPulseApi.csproj"
COPY . .
WORKDIR "/src/BistroPulseApi"
RUN dotnet build "BistroPulseApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicación
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BistroPulseApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# Fase final
FROM base AS final
WORKDIR /app

# Recibir variables como argumentos y configurarlas como entorno
ARG BISTROPULSE_DB
ARG SEQ_SERVER_URL
ENV ConnectionStrings__BistroPulseDB=$BISTROPULSE_DB
ENV Serilog__SeqServerUrl=$SEQ_SERVER_URL

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BistroPulseApi.dll"]