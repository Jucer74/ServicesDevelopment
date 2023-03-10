# Evaluación Teórica (40%)
Responda las preguntas del siguiente Enlace:

[Examen](https://forms.gle/b2XM5whiojqnoMMM8)

# Evaluación Práctica (60%)
Implemente una Web API denominada **PricatAPI**, que permita realizar las operaciones de CRUD sobre los productos.

## Ambiente
Utilizando la base de datos Microsoft SQL server, Cree una base de datos denominada PricatDB y ejecute los Scripts provistos en el repositorio.

**Scripts**
- TAB_Products.sql
- INS_Products.sql

## Enpopints
Utilizando el proyecto **PricatAPI**, implemente los Endpoints necesarios para realizar las operaciones de CRUD sobre los productos

1. **GET /api/v1/products**: Obtener todos los Productos (GetProducts)
- **Request**: No se envia Nada
- **Response**: la lista de los productos en formato json.
```json
[
  {
    "id": 1,
    "imageName": "Arepa-Con-Chorizo.jpg",
    "name": "Arepa Con Chorizo",
    "description": "Deliciosa arepa de maiz tierno acompañada de chorizo santarosano",
    "price": 6000
  },
  ....
  More data Here
  ....
  {
    "id": 10,
    "imageName": "Arepa-Sencilla.jpg",
    "name": "Arepa Sencilla",
    "description": "Deliciosa arepa de maiz tierno aliñado, preparada al carbon",
    "price": 2500
  }
]
```

2. **GET /api/v1/products/{id}**: Obtener los datos del producto por Identificador (GetProduct)
- **Request**: Se envia el Id del producto
- **Response**: Los datos del producto segun el identificador en formato json.
```csharp
{
  "id": 1,
  "imageName": "Arepa-Con-Chorizo.jpg",
  "name": "Arepa Con Chorizo",
  "description": "Deliciosa arepa de maiz tierno acompañada de chorizo santarosano",
  "price": 6000
}
```

3. **POST /api/v1/products**: Adicionar un Nuevo Producto (PostProduct)
- **Request**: Se envian los datos del Producto a adicionar en formato json con el Id con valor en Cero
```json
{
  "id": 0,
  "imageName": "Arepas-Con-Camaron.jpg",
  "name": "Arepa Rellena de Camaron",
  "description": "Deliciosa arepa de maiz tierno con camaron",
  "price": 20000
}
```
- **Response**: Los datos del producto segun el identificador en formato json.
```json
{
  "id": 11,
  "imageName": "Arepas-Con-Camaron.jpg",
  "name": "Arepa Rellena de Camaron",
  "description": "Deliciosa arepa de maiz tierno con camaron, salsas de la casa y queso",
  "price": 20000
}
```

4. **PUT /api/v1/products/{id}**: Actualizar los datos de un producto (PutProduct)
- **Request**: Se envia el Id del producto y los datos del producto en formato json
```json
{
  "id": 11,
  "imageName": "Arepas-Con-Camaron.jpg",
  "name": "Arepa Rellena Con Camaron",
  "description": "Deliciosa arepa de maiz tierno con camaron, salsas de la casa y queso",
  "price": 20000
}
```
- **Response**: Los datos del producto segun el identificador en formato json, con los datos actualizados
```csharp
{
  "id": 11,
  "imageName": "Arepas-Con-Camaron.jpg",
  "name": "Arepa Rellena Con Camaron",
  "description": "Deliciosa arepa de maiz tierno con camaron, salsas de la casa y queso",
  "price": 20000
}
```

5. **DELETE /api/v1/products/{id}**: Eliminar un Producto (DeleteProduct)
- **Request**: Se envia el Id del producto
- **Response**: Sin Contenido

# Observaciones
- Implemente la aplicacion Usando **Visual Studio Community** 
- Utilice la solucion de este mismo Directorio **PricatAPI.sln**
- Utilice la base de datos Microsoft SQL Server y cree la base de datos con el siguiente nombre: ***PricatDB**
- Use la libreria de EntitityFramework Core para las operaciones de CRUD sobre la base de datos

# Notas
- Adicione las referencias los siguientes Nugets
```
Microsoft.EntityFrameworkCore               6.0.14
Microsoft.EntityFrameworkCore.Tools         6.0.14
Microsoft.EntityFrameworkCore.Design        6.0.14
Microsoft.EntityFrameworkCore.SqlServer     6.0.14
```
- Cree el modelo de productos validando los campos requeridos y la llave principal
```csharp
public class Product
{
    [Key]
    [Display(Name = "Id")]
    public int Id { get; set; }

    [Display(Name = "Imagen")]
    public string ImageName { get; set; } = null!;

    [Required]
    [Display(Name = "Nombre")]
    public string Name { get; set; } = null!;

    [Display(Name = "Descripcion")]
    public string Description { get; set; } = null!;

    [Required]
    [Display(Name = "Precio")]
    public decimal Price { get; set; }
}
```

- Adicione el Contexto Nombrandolo **AppDbContext**
```csharp
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}
```

- A nivel del **Program** adicione el llamado al Contexto
```csharp
// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CnnStr")));
```
- Cree el Controlador **ProductsController** de tipo API Controller con acciones usando el Entity Framework.
    - **Model**: Product
    - **Context**: AppDbContext

- Modifique la Ruta principal del Controlador para que maneje la version    
```csharp
[Route("api/v1/[controller]")]
```

