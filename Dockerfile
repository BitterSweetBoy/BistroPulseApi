FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5020
EXPOSE 5021

# Fase de build y publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY . .

# Restauramos las dependencias
RUN dotnet restore "BistroPulseApi/BistroPulseApi.csproj" 

# Cambiamos al directorio del proyecto principal
WORKDIR "/src/BistroPulseApi"


# Publicamos con log detallado
RUN dotnet publish "BistroPulseApi.csproj" -c Release -o /app/publish 

# Fase final de la imagen
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5010
ENTRYPOINT ["dotnet", "BistroPulseApi.dll"]
