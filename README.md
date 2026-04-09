# Taller 2 - Gestión de Autos Eléctricos

## Estructura del Repositorio
```
/servidor   → Java Spring Boot (REST API)
/cliente    → C# WinForms (cliente de escritorio)
```

## Servidor (Java Spring Boot)

### Requisitos
- Java 17+
- Maven

### Ejecutar
```bash
cd servidor
mvn spring-boot:run
```
El servidor corre en `http://localhost:8080`

### Endpoints REST (Postman)

| Método | URL | Descripción |
|--------|-----|-------------|
| POST | `/autos` | Agregar auto |
| GET | `/autos/{id}` | Buscar por ID |
| PUT | `/autos/{id}` | Actualizar |
| DELETE | `/autos/{id}` | Eliminar |
| GET | `/autos` | Listar todos |
| GET | `/autos/filtrar/marca/{marca}` | Filtrar por marca |
| GET | `/autos/filtrar/anio/{anio}` | Filtrar por año |

### Ejemplo JSON (POST/PUT)
```json
{
  "marca": "Tesla",
  "modelo": "Model 3",
  "anio": 2023,
  "autonomiaKm": 500.0,
  "fechaRegistro": "2024-01-15T10:30:00",
  "bateria": {
    "tipo": "Li-Ion",
    "capacidadKwh": 75.0,
    "ciclosVida": 1500
  }
}
```

## Cliente (C# WinForms)

### Requisitos
- .NET 8 SDK

### Ejecutar
```bash
cd cliente/AutosCliente
dotnet run
```

## Integrantes
- Yaser Rondón
- Ismael Cardozo
- Juan Mancipe

Versión: 1.0
