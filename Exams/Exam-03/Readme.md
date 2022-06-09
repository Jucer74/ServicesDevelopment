
# Evaluación Teórica (40%)
Responda las preguntas del siguiente Enlace:

[Examen](https://forms.gle/eP8kEvxouTXvJYLG9)


# Ejercicio (60%)
Utilizando los conocimientos adquiridos en clase, y bandose en el modelo de Clean Architecture, desarrolle una API que soporte las operaciones de Catalogo de Precios (Pricat) para los productos de una empresa

# Contexto
Utilice la solucion **PricatApp** como base e implemente la API que soporte las siguientes operaciones.

## Categories
1. **GET /api/v1.0/Categories** : Obtener la lista de todas las categorias de producto
  - **Request**: Empty
  - **Response**: Example
  ```json
  [
    {
      "Id": 1,
      "Description": "Alimentos"
    },
    {
      "Id": 2,
      "Description": "Bebidas"
    },
    {
      "Id": 3,
      "Description": "Productos de Aseo"
    },
    {
      "Id": 4,
      "Description": "Ropa"
    },
    {
      "Id": 5,
      "Description": "Medicamentos"
    }
  ]
  ```
  - **Codes**:
    - 200 : OK: Success Response
    - 500: Internal Server Error: In case an unexpeted problem


2. **GET /api/v1.0/Categories/{id}** : Obtener la categoria por idententificador
  - **Request**: Empty
  - **Response**: Example
  ```json
  {
    "Id": 1,
    "Description": "Alimentos"
  }
  ```
  - **Codes**:
    - 200 : OK: Success Response
    - 400: Bad Request: In case the request has errors or invalid format
    - 404: Not Found: In case the Id not found
    - 500: Internal Server Error: In case an unexpeted problem

3. **POST /api/v1.0/Categories** : Adicionar una nueva categoria
  - **Request**: Example
  ```json
  {
    "Id": 0,
    "Description": "Ferreteria"
  }
  ```
  - **Response**: Example
  ```json
  {
    "Id": 6,
    "Description": "Ferreteria"
  }
  ```
  - **Codes**:
    - 200 : OK: Success Response
    - 400: Bad Request: In case the request has errors or invalid format
    - 500: Internal Server Error: In case an unexpeted problem

4. **PUT /api/v1.0/Categories/{id}** : Actualizar una categoria
  - **Request**: Example
  ```json
  {
    "Id": 6,
    "Description": "Herramientas y Ferreteria"
  }
  ```
  - **Response**: Empty
  - **Codes**:
    - 200 : OK: Success Response
    - 400: Bad Request: In case the request has errors or invalid format
    - 404: Not Found: In case the Id not found
    - 500: Internal Server Error: In case an unexpeted problem

5. **DELETE /api/v1.0/Categories/{id}**: Eliminar una categoria, incluidos todos los productos que dependan de ella
  - **Request**: Empty
  - **Response**: Empty
  - **Codes**:
    - 200 : OK: Success Response
    - 400: Bad Request: In case the request has errors or invalid format
    - 404: Nort Found: In case the Id not found
    - 500: Internal Server Error: In case an unexpeted problem

---
## Productos
1. **GET /api/v1.0/Products** : Obtener la lista de todos productos
  - **Request**: Empty
  - **Response**: Example
  ```json
  [
    {
      "Id": 1,
      "CategoryId": 1,
      "EanCode": "7707548516286",
      "Description": "Arroz",
      "Unit": "Lb",
      "Price": 500.00
    },
    {
      "Id": 2,
      "CategoryId": 1,
      "EanCode": "7707548941507",
      "Description": "Papa",
      "Unit": "Lb",
      "Price": 1500.00
    },
    {
      "Id": 3,
      "CategoryId": 2,
      "EanCode": "7707548160274",
      "Description": "Cocacola",
      "Unit": "Und",
      "Price": 2500.00
    },
    {
      "Id": 4,
      "CategoryId": 2,
      "EanCode": "7707548110958",
      "Description": "Pepsi",
      "Unit": "und",
      "Price": 2500.00
    },
    {
      "Id": 5,
      "CategoryId": 3,
      "EanCode": "7707548758303",
      "Description": "Detergente",
      "Unit": "Kg",
      "Price": 12500.00
    },
    {
      "Id": 6,
      "CategoryId": 3,
      "EanCode": "7707548210801",
      "Description": "Cloro",
      "Unit": "CC",
      "Price": 21500.00
    },
    {
      "Id": 7,
      "CategoryId": 4,
      "EanCode": "7707548472247",
      "Description": "Camisa",
      "Unit": "Und",
      "Price": 32500.00
    },
    {
      "Id": 8,
      "CategoryId": 4,
      "EanCode": "7707548427902",
      "Description": "Pantalon",
      "Unit": "CC",
      "Price": 42500.00
    },
    {
      "Id": 9,
      "CategoryId": 5,
      "EanCode": "7707548799412",
      "Description": "Camisa",
      "Unit": "Und",
      "Price": 32500.00
    },
    {
      "Id": 10,
      "CategoryId": 5,
      "EanCode": "7707548861546",
      "Description": "Pantalon",
      "Unit": "CC",
      "Price": 42500.00
    }
  ]
  ```
  - **Codes**:
    - 200 : OK: Success Response
    - 400: Bad Request: In case the request has errors or invalid format
    - 404: Nort Found: In case the Id not found
    - 500: Internal Server Error: In case an unexpeted problem

2. **GET /api/v1.0/Products/{id}** : Obtener el producto por identificador de producto
  - **Request**: Empty
  - **Response**: Example
  ```json
  {
    "Id": 1,
    "CategoryId": 1,
    "EanCode": "7707548516286",
    "Description": "Arroz",
    "Unit": "Lb",
    "Price": 500.00
  }
  ```
  - **Codes**:
    - 200 : OK: Success Response
    - 400: Bad Request: In case the request has errors or invalid format
    - 404: Nort Found: In case the Id not found
    - 500: Internal Server Error: In case an unexpeted problem

3. **GET /api/v1.0/Products/Category/{categoryId}** : Obtener la lista de productos por identificador de categoria
  - **Request**: Empty
  - **Response**: Example
  ```json
  [
    {
      "Id": 1,
      "CategoryId": 1,
      "EanCode": "7707548516286",
      "Description": "Arroz",
      "Unit": "Lb",
      "Price": 500.00
    },
    {
      "Id": 2,
      "CategoryId": 1,
      "EanCode": "7707548941507",
      "Description": "Papa",
      "Unit": "Lb",
      "Price": 1500.00
    }
  ]
  ```
  - **Codes**:
    - 200 : OK: Success Response
    - 400: Bad Request: In case the request has errors or invalid format
    - 404: Nort Found: In case the Id not found
    - 500: Internal Server Error: In case an unexpeted problem


4. **POST /api/v1.0/Products** : Adicionar un nuevo producto
  - **Request**: Empty
  ```json
  {
    "Id": 0,
    "CategoryId": 1,
    "EanCode": "7707548697640",
    "Description": "Yuca",
    "Unit": "Lb",
    "Price": 300.00
  }
  ```
  - **Response**: Example
  ```json
  {
    "Id": 11,
    "CategoryId": 1,
    "EanCode": "7707548697640",
    "Description": "Yuca",
    "Unit": "Lb",
    "Price": 300.00
  }
  ```
  - **Codes**:
    - 200 : OK: Success Response
    - 400: Bad Request: In case the request has errors or invalid format
    - 404: Nort Found: In case the Id not found
    - 500: Internal Server Error: In case an unexpeted problem

5. **PUT /api/v1.0/Products/{id}** : Actualizar un producto
  - **Request**: Example
  ```json
  {
    "Id": 11,
    "CategoryId": 1,
    "EanCode": "7707548697640",
    "Description": "Platano",
    "Unit": "Lb",
    "Price": 500.00
  }
  ```
  - **Response**: Empty
  - **Codes**:
    - 200 : OK: Success Response
    - 400: Bad Request: In case the request has errors or invalid format
    - 404: Nort Found: In case the Id not found
    - 500: Internal Server Error: In case an unexpeted problem

6. **DELETE /api/v1.0/Categories** : Eliminar un producto
  - **Request**: Empty
  - **Response**: Empty
  - **Codes**:
    - 200 : OK: Success Response
    - 400: Bad Request: In case the request has errors or invalid format
    - 404: Nort Found: In case the Id not found
    - 500: Internal Server Error: In case an unexpeted problem

# Base de datos
Utilizando una version de MySQL ejecutandose dentro de un contenedor por el puerto 3307, ejecute los scripts necesarios para crear la estructura de base de datos y tablas necesarias para almacenar la informacion de Categorias y Productos.

**Scripts:**
- 01_Create_Database.sql
- 02_Tab_Categories.sql
- 03_Tab_Products.sql
- 04_Pricat_User.sql

## Conexion
Adicione la nueva cadena de conexion al archivo **AppSettings**

```json
"ConnectionStrings": {
      "CnnStr": "server=localhost;uid=PricatUSer;pwd=Pr1c4tUs3r;database=pricatdb"
   }
```

# Container
Genere el contenedor utilizando el archivo **DockerFile**

## Cree la Imagen
En la ruta donde este el archivo **DockerFile**, ejecute el siguiente comando para construir la imagen.
```
docker build -t pricatapp .
```
para mas información puede consultar la siguiente [Guia](https://docs.docker.com/samples/dotnetcore/).

## Ejecute la Imagen 
Ejecute la imageny pruebe que funcionan los EndPoints configurados.

```
docker run -d -p 8080:80 --name Pricat pricatapp
```
## Publicar Imagen
- Cree una cuenta en [DockerHub](https://hub.docker.com/) y adicione su nuevo repositorio.
- Publique su imagen y suba su version de la API a **DockerHub** , para ello puede seguir esta [Guia](https://docs.docker.com/docker-hub/).

---

# Reglas
- Se debe validar que los Ids Existan antes de Efectuar la actualizacion o el borrado, en caso de no existir, debe retornar una excepcion de tipo 404-Not Found (Aplica para Categorias y Productos)
- Se debe Validar el EAN Code utilizando la verificacion de [digito de chequeo](https://en.wikipedia.org/wiki/International_Article_Number) para EAN 13 y en caso de no ser valido debe retornar una Exception de tipo 400-Bad Request. Puede implementar el algoritmo
- Valide los campos requeridos y sus longitudes en las entidades, para ello puede utilizar **Annotations** (Required, StringLength).
- Implemente el pattern, [Exception Middleware Handler](https://github.com/Jucer74/ExceptionHandler), para facilitar el control de errores y manejo de Excepciones de Negocio, pudiendo retornar estructuras de tipo **ErrorDetails** para estandarizar las respuestas de los errores.

# NOTA
RECUERDE SUBIR SU SOLUCIÓN A SU RAMA DE ESTE REPOSITORIO.

# Revision
1. Validar que todos EndPoints funcionan correctamente 
2. Los casos especiales y el manejo de los erroes y las excepciones
3. Revisar el Codigo
4. Descargar contenedor y probrar que funciona correctamente.
5. Verificar la Adherencia de la arquitecura y del patron Exception Ahndler.

# DockerHub 
Deje aqui el enlace de la imagen a descargar.

[PricatApp-Nickname]

