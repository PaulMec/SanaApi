## Getting Started con SanaStoreAPI y SanaStore Frontend

Este documento te guiará a través de los pasos necesarios para configurar y comenzar a utilizar la SanaStoreAPI junto con el frontend desarrollado en React. Sigue estos pasos para configurar tu entorno y comenzar a usar la aplicación.

### Configuración del Ambiente de Desarrollo

#### 1. Clonar el repositorio
Para comenzar, necesitas clonar el repositorio de la API y del frontend a tu máquina local. Usa el siguiente comando para clonar la API:

```bash
git clone https://github.com/PaulMec/SanaStoreAPI.git
```

#### 2. Instalar las dependencias
Navega al directorio del proyecto clonado y ejecuta el siguiente comando para instalar todas las dependencias necesarias para el backend y el frontend.

**Backend:**
```bash
cd SanaStoreAPI/src/SanaStore.Presentation
dotnet restore
```

**Frontend:**
```bash
cd path-to-your-frontend
npm install
```

#### 3. Configurar la base de datos
Configura tu base de datos local o remota para trabajar con Entity Framework Core. Asegúrate de actualizar la cadena de conexión en el archivo `appsettings.json` del proyecto backend.

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=tu-servidor;Database=tu-base-de-datos;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true"
}
```

Luego, aplica las migraciones para estructurar tu base de datos:

```bash
dotnet ef database update
```

#### 4. Ejecutar la aplicación
Una vez que todo está configurado, puedes iniciar el servidor backend y el servidor frontend.

**Backend:**
```bash
dotnet run
```

**Frontend:**
```bash
npm start
```

#### 5. Acceder a la API
Una vez que ambos servidores están corriendo, puedes empezar a utilizar la API a través de los endpoints configurados. Por ejemplo, para acceder a los productos, visita:

```
http://localhost:5000/api/products
```

### Utilizar la API
Puedes interactuar con la API utilizando herramientas como Postman o directamente desde tu frontend mediante llamadas AJAX o utilizando bibliotecas como Axios en React.

**Endpoints importantes:**

- **Categorías**
  - Listar todas: GET `/api/categories`
  - Crear nueva: POST `/api/categories`
  - Actualizar: PUT `/api/categories/{id}`
  - Eliminar: DELETE `/api/categories/{id}`

- **Clientes**
  - Listar todos: GET `/api/customers`
  - Detalles por ID: GET `/api/customers/{id}`
  - Crear nuevo: POST `/api/customers`
  - Actualizar: PUT `/api/customers/{id}`
  - Eliminar: DELETE `/api/customers/{id}`

- **Pedidos**
  - Listar todos: GET `/api/orders`
  - Detalles por ID: GET `/api/orders/{id}`
  - Crear nuevo: POST `/api/orders`
  - Actualizar: PUT `/api/orders/{id}`
  - Eliminar: DELETE `/api/orders/{id}`

- **Productos**
  - Listar todos: GET `/api/products`
  - Crear nuevo: POST `/api/products`
  - Actualizar: PUT `/api/products/{id}`
  - Eliminar: DELETE `/api/products/{id}`

### Consumo desde el Frontend
El frontend en React ya está configurado para interactuar con la API. Asegúrate de que las URLs en el archivo `config.js` del frontend coincidan con la URL base de tu backend.

- **Agregar productos al carrito**
- **Crear pedidos**
- **Buscar clientes por correo electrónico**
- **Actualizar y eliminar productos y categorías**

### Finalizar
Sigue estos pasos para asegurarte de que tu ambiente está completamente configurado y listo para ser usado. Si encuentras algún problema durante la configuración, verifica las configuraciones y asegúrate de que todos los servicios necesarios están en funcionamiento.