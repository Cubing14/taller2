# Proyeceto 2 - Gestión de Vehículos Eléctricos e Híbridos

Aplicación distribuida para la gestión de vehículos eléctricos e híbridos, implementando una arquitectura cliente-servidor con servicios REST. Incluye dos clientes en lenguajes distintos (C# y JavaScript) y un servidor en Java Spring Boot.

## Arquitectura y Patrones
- **MVC**: Separación en Modelo, Vista y Controlador en todos los componentes.
- **Observer**: Actualización automática de la grilla al insertar/modificar/eliminar (implementado en el cliente JavaScript).
- **Singleton**: Servicios como instancias únicas.
- **Builder**: Para construcción de objetos complejos (vehículos y baterías).
- **SOLID**: Principios de diseño orientado a objetos aplicados.

## Estructura del Repositorio
```
Taller 2/
├── README.md
├── Taller 2.sln
├── servidor/          # Java Spring Boot (REST API)
│   ├── pom.xml
│   └── src/
├── cliente/           # C# WinForms (cliente de escritorio)
│   └── AutosCliente/
└── cliente-js/        # JavaScript (cliente web)
    ├── index.html
    ├── styles/
    ├── models/
    ├── controllers/
    ├── services/
    └── views/
```

## Requisitos del Sistema
- **Java 17+** (para el servidor)
- **.NET 8+** (para el cliente C#)
- **Python 3+** (para servir el cliente JS)
- **Maven** (para el servidor)
- Navegador web moderno (para el cliente JS)

## Instalación y Ejecución

### 1. Servidor (Java Spring Boot)
```powershell
cd servidor; mvn spring-boot:run
```
- **Puerto**: `http://localhost:8080`
- **Descripción**: Proporciona APIs REST para gestionar vehículos y baterías.

### 2. Cliente 1 (C# WinForms)
```powershell
cd cliente/AutosCliente; dotnet run
```
- **Descripción**: Aplicación de escritorio para gestión de vehículos.

### 3. Cliente 2 (JavaScript Web)
```powershell
cd cliente-js; python -m http.server 3000
```
- **Acceso**: Abre `http://localhost:3000/index.html` en un navegador.
- **Descripción**: Interfaz web para gestión de vehículos y baterías.

### Orden de Ejecución
1. Inicia el **servidor** primero.
2. Luego inicia los **clientes** (.NET y JS).
3. Prueba las operaciones CRUD en ambos clientes.

## Endpoints REST (Servidor)

### Vehículos Eléctricos
| Método | URL | Descripción |
|--------|-----|-------------|
| POST | `/autos` | Agregar vehículo eléctrico |
| GET | `/autos/{id}` | Buscar por ID |
| PUT | `/autos/{id}` | Actualizar |
| DELETE | `/autos/{id}` | Eliminar |
| GET | `/autos` | Listar todos |
| GET | `/autos/filtrar/marca/{marca}` | Filtrar por marca |
| GET | `/autos/filtrar/anio/{anio}` | Filtrar por año |
| GET | `/autos/{id}/costo` | Calcular costo de operación |

### Vehículos Híbridos
| Método | URL | Descripción |
|--------|-----|-------------|
| POST | `/hibridos` | Agregar vehículo híbrido |
| GET | `/hibridos/{id}` | Buscar por ID |
| PUT | `/hibridos/{id}` | Actualizar |
| DELETE | `/hibridos/{id}` | Eliminar |
| GET | `/hibridos` | Listar todos |
| GET | `/hibridos/filtrar/marca/{marca}` | Filtrar por marca |
| GET | `/hibridos/filtrar/anio/{anio}` | Filtrar por año |
| GET | `/hibridos/{id}/costo` | Calcular costo de operación |

### Baterías
| Método | URL | Descripción |
|--------|-----|-------------|
| POST | `/baterias` | Agregar batería |
| GET | `/baterias/{id}` | Buscar por ID |
| PUT | `/baterias/{id}` | Actualizar |
| DELETE | `/baterias/{id}` | Eliminar |
| GET | `/baterias` | Listar todas |
| GET | `/baterias/filtrar/tipo/{tipo}` | Filtrar por tipo |

## Ejemplos de JSON

### Vehículo Eléctrico (POST/PUT)
```json
{
  "marca": "Tesla",
  "modelo": "Model 3",
  "anio": 2023,
  "autonomiaKm": 500.0,
  "fechaRegistro": "2024-01-15T10:30:00",
  "baterias": [
    {
      "tipo": "Li-Ion",
      "capacidadKwh": 75.0,
      "ciclosVida": 1500,
      "fechaInstalacion": "2024-01-15T10:30:00",
      "estado": "nueva"
    }
  ]
}
```

### Batería (POST/PUT)
```json
{
  "tipo": "Li-Ion",
  "capacidadKwh": 75.0,
  "ciclosVida": 1500,
  "fechaInstalacion": "2024-01-15T10:30:00",
  "estado": "nueva"
}
```

## Funcionalidades
- **CRUD completo**: Insertar, buscar, actualizar, eliminar para vehículos y baterías.
- **Relación maestro-detalle**: Un vehículo puede tener múltiples baterías.
- **Filtros del lado del servidor**: Por marca y año.
- **Cálculo de costo**: Operación basada en autonomía y capacidad de batería.
- **Observer**: Actualización automática de listas al modificar datos (cliente JS).
- **Interfaz intuitiva**: Menús de navegación y "Acerca de" en ambos clientes.

## Equipo
- [Nombres del equipo aquí]

## Versión
1.0

## Notas
- Asegúrate de que el servidor esté corriendo antes de iniciar los clientes.
- Los filtros y cálculos se realizan del lado del servidor.
- El proyecto cumple con los requisitos de patrones de diseño y arquitectura distribuida.

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

Versión: 3.0
