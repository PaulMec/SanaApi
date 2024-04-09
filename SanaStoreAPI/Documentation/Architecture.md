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
  - **src**
    - **SanaStoreAPI.Application**: Capa de aplicación que contiene la lógica de negocio de la aplicación.
    - **SanaStoreAPI.Domain**: Capa de dominio que define los modelos de dominio y las interfaces de repositorio.
    - **SanaStoreAPI.Infrastructure**: Capa de infraestructura que implementa la lógica de acceso a datos y la integración con servicios externos.
    - **SanaStoreAPI.Presentation**: Capa de presentación que contiene los controladores API y la configuración de la interfaz de usuario.
  - **test**
    - *(Sección para las pruebas unitarias y de integración)*
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
│       └── Product.cs
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

#### Contribución
¡Agradecemos tu interés en contribuir al desarrollo de SanaStoreAPI! Si deseas contribuir, sigue estas pautas:
- *(Pautas para enviar solicitudes de extracción, escribir pruebas y mantener la calidad del código)*

---
