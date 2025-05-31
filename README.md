# BistroPulseApi

Una API REST desarrollada en .NET 8 para la gestión de restaurantes, pedidos y entregas. El sistema incluye autenticación basada en roles, gestión de usuarios, y integración con PostgreSQL.

## 🚀 Características Principales

- **Autenticación y Autorización**: Sistema basado en ASP.NET Core Identity
- **Logging Estructurado**: Integración con Serilog y Seq para monitoreo 
- **API Documentation**: Swagger/OpenAPI integrado
- **Soporte Docker**: Configuración para contenedores

## 📋 Requisitos Previos

| Requisito | Versión | Propósito |
|-----------|---------|-----------|
| .NET SDK | 8.0+ | Framework de desarrollo |
| PostgreSQL | 12+ | Base de datos principal |
| Docker Desktop | Última | Despliegue en contenedores (opcional) |

## 🛠️ Instalación y Configuración

### 1. Clonar el Repositorio
```bash
git clone https://github.com/BitterSweetBoy/BistroPulseApi.git
cd BistroPulseApi
```

### 2. Configurar Base de Datos
Actualiza la cadena de conexión en los archivos de configuración:

**Desarrollo**: 
```json
{
  "ConnectionStrings": {
    "BistroPulseDB": "Host=localhost;Database=BistroPulseDB;Username=tu_usuario;Password=tu_password"
  }
}
```

### 3. Configurar Logging (Opcional)
Para usar Seq como servidor de logs: 
```json
{
  "Serilog": {
    "SeqServerUrl": "http://localhost:5341"
  }
}
```

### 4. Ejecutar la Aplicación

**Desarrollo**:
```bash
dotnet run --project BistroPulseApi --launch-profile Development
```

**Producción**:
```bash
dotnet run --project BistroPulseApi --launch-profile Production
```

**Docker**:
```bash
docker build -t bistropulseapi .
docker run -p 8080:8080 -p 8081:8081 bistropulseapi
```

## 🌐 Acceso a la API

- **Desarrollo**: https://localhost:7273/swagger
- **IIS Express**: http://localhost:22593/swagger
- **Docker**: http://localhost:8080/swagger

## 📁 Estructura del Proyecto

```
BistroPulseApi/
├── BistroPulseApi/              # API principal
│   ├── Controllers/             # Controladores REST
│   ├── Properties/              # Configuración de lanzamiento
│   └── appsettings*.json       # Archivos de configuración
├── Module.Core/                 # Entidades del dominio
├── Module.Infrastructure/       # Acceso a datos
└── Module.Shared/              # Utilidades compartidas
```

## 🤝 Contribución

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request
