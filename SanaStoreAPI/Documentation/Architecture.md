### SanaStoreAPI

Bienvenido al repositorio de SanaStoreAPI, una API RESTful desarrollada con ASP.NET Core que proporciona funcionalidades básicas para un sistema de tienda en línea.

#### Descripción
SanaStoreAPI es una API diseñada para ser utilizada en un sistema de comercio electrónico. Proporciona endpoints para gestionar productos, categorías, clientes y órdenes.

#### Requerimientos
La API debe cumplir con los siguientes requisitos:

1. **Productos y Categorías:**
   - Los productos pueden pertenecer a múltiples categorías.
   - Se pueden listar productos junto con su información básica, como nombre, descripción, precio y stock.
   - Las categorías pueden ser creadas, modificadas, eliminadas y listadas.

2. **Clientes y Órdenes:**
   - Los clientes pueden realizar órdenes.
   - Las órdenes pueden contener múltiples productos.
   - Se debe validar el stock antes de realizar una orden.
   - Se pueden listar órdenes junto con su información básica, como fecha, cliente y productos asociados.

#### Arquitectura
El proyecto sigue los principios de Clean Architecture, que promueve una separación clara de las responsabilidades y una arquitectura modular y escalable. La estructura de carpetas del proyecto es la siguiente:

- **SanaStoreAPI**
    - **SanaStoreAPI.Application**: Capa de aplicación que contiene la lógica de negocio de la aplicación.
    - **SanaStoreAPI.Domain**: Capa de dominio que define los modelos de dominio y las interfaces de repositorio.
    - **SanaStoreAPI.Infrastructure**: Capa de infraestructura que implementa la lógica de acceso a datos y la integración con servicios externos.
    - **SanaStoreAPI.Presentation**: Capa de presentación que contiene los controladores API y la configuración de la interfaz de usuario.
  - **test**
    - *(Sección para las pruebas unitarias y de integración)*  NO SE IMPLEMENTARON
  - **Documentation**
    - *(Sección para documentación relevante del proyecto)*


```
DB/
|
├── Migrations/
├── Models/
│   └── Category.cs/
├── Context/
│   └── SanaStoreContext.cs
SanaStoreAPI/
│
├── Domain/
│   └── Models/
│       └── ProductDTO.cs
│
├── Application/
│   └── Services/
│       └── ProductService.cs
│
├── Infrastructure/
│   └── Data/
│       └── ProductRepository.cs
│
├── Documentation/
│   └── README.md
│
└── Presentation/
    └── Controllers/
        └── ProductController.cs
```


#### Getting Started
Para comenzar a utilizar la API, sigue estos pasos:

1. **Clona el repositorio:**
   ```
   git clone https://github.com/tu-usuario/SanaStoreAPI.git
   ```

2. **Instala las dependencias:**
   ```
   cd SanaStoreAPI/src/SanaStore.Presentation
   dotnet restore
   ```

3. **Configura la base de datos:**
   - *(Instrucciones para configurar la base de datos según el proveedor utilizado)*

4. **Ejecuta la aplicación:**
   ```
   dotnet run
   ```

5. **Comienza a utilizar la API:**
   - *(Instrucciones para acceder a los endpoints y utilizar la API)*

   ### Instrucciones para acceder y utilizar la API

A continuación, te proporciono una guía detallada sobre cómo acceder y utilizar los endpoints de la API que has configurado en tus controladores. Estos endpoints te permitirán gestionar categorías, clientes, pedidos y productos a través de tu aplicación.

#### **Acceder a la API:**
1. **Base URL**: Todas las llamadas a la API deben realizarse a la URL base que configures para tu servidor (En este caso debe correrse el back para poder conectarse al front).
2. **Autenticación**: De momento no se implemento nada en torno a la autenticación.

#### **Utilizar los Endpoints:**

#### Categorías
- **Obtener todas las categorías**
  - **Método**: GET
  - **Endpoint**: `/api/categories`
  - **Descripción**: Devuelve todas las categorías disponibles.

- **Obtener una categoría por ID**
  - **Método**: GET
  - **Endpoint**: `/api/categories/{id}`
  - **Descripción**: Devuelve los detalles de una categoría específica.

- **Crear una nueva categoría**
  - **Método**: POST
  - **Endpoint**: `/api/categories`
  - **Body**: JSON con los detalles de la nueva categoría.

- **Actualizar una categoría**
  - **Método**: PUT
  - **Endpoint**: `/api/categories/{id}`
  - **Body**: JSON con los datos actualizados de la categoría.

- **Eliminar una categoría**
  - **Método**: DELETE
  - **Endpoint**: `/api/categories/{id}`

#### Clientes
- **Obtener todos los clientes**
  - **Método**: GET
  - **Endpoint**: `/api/customers`

- **Obtener un cliente por ID**
  - **Método**: GET
  - **Endpoint**: `/api/customers/{id}`

- **Obtener un cliente por correo electrónico**
  - **Método**: GET
  - **Endpoint**: `/api/customers/email/{email}`

- **Crear un cliente**
  - **Método**: POST
  - **Endpoint**: `/api/customers`
  - **Body**: JSON con los detalles del nuevo cliente.

- **Actualizar un cliente**
  - **Método**: PUT
  - **Endpoint**: `/api/customers/{id}`

- **Eliminar un cliente**
  - **Método**: DELETE
  - **Endpoint**: `/api/customers/{id}`

#### Pedidos
- **Obtener todos los pedidos**
  - **Método**: GET
  - **Endpoint**: `/api/orders`

- **Obtener un pedido por ID**
  - **Método**: GET
  - **Endpoint**: `/api/orders/{id}`

- **Crear un pedido**
  - **Método**: POST
  - **Endpoint**: `/api/orders`
  - **Body**: JSON con los detalles del nuevo pedido.

- **Actualizar un pedido**
  - **Método**: PUT
  - **Endpoint**: `/api/orders/{id}`

- **Eliminar un pedido**
  - **Método**: DELETE
  - **Endpoint**: `/api/orders/{id}`

#### Productos
- **Obtener lista paginada de productos**
  - **Método**: GET
  - **Endpoint**: `/api/products`
  - **Parámetros**: `pageNumber` y `pageSize`

#### **Consumo de la API:**
- Utiliza herramientas como Postman o Curl para probar los endpoints.
- Asegúrate de manejar correctamente los códigos de estado HTTP para gestionar errores en tu aplicación cliente.

Estas instrucciones deben ser adaptadas a las particularidades de tu entorno de producción y desarrollo, incluyendo la configuración de seguridad, CORS y manejo de errores más detallado según sea necesario.
---
