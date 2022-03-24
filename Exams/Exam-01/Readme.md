
# Evaluación Teórica (40%)
Responda las preguntas del siguiente Enlace:

[https://forms.gle/WRLtjodfrUC6ZWg67](https://forms.gle/WRLtjodfrUC6ZWg67)


# Ejercicio (60%)
Utilizando los conocimientos adquiridos en clase, desarrolle el siguiente ejercicio.

# Contexto
**NetBank** es una empresa reguladora de tarjetas de credito a la cual todos los bancos le reportan las tarjetas robadas.

# Ejercicio
Implemente una Web API que permita :

1. Obtener la lista de todas las tarjetas
2. Obtener la lista de las tarjetas por la red emisora
3. Obtener los datos de la tarjeta por su numero
4. Reactivar una tarjeta, marcandola como recuperada
5. Validar si el Numero de una tarjeta es valido por su codigo de verificacion.

# Pasos

## Base de datos
Cree la base de datos **NetBankDB** ejecutando los siguientes pasos:

1. Inice sesion en la base de datos con Autenticacion Windows <br>

![](https://github.com/Jucer74/ServicesDevelopment/blob/main/Exams/Exam-01/Images/Image-01.jpg)

2. Abra el archivo **01_Create_Database.sql** que se encuentra en la ruta **Exams/Exam-01/Scripts** <br>

![](https://github.com/Jucer74/ServicesDevelopment/blob/main/Exams/Exam-01/Images/Image-02.jpg)

![](https://github.com/Jucer74/ServicesDevelopment/blob/main/Exams/Exam-01/Images/Image-03.jpg)

3. Ejecute el script<br>

![](https://github.com/Jucer74/ServicesDevelopment/blob/main/Exams/Exam-01/Images/Image-04.jpg)

4. Refresque la conexión y valide que la base de datos fue creada<br>

![](https://github.com/Jucer74/ServicesDevelopment/blob/main/Exams/Exam-01/Images/Image-05.jpg)

5. Abra el archivo **02_Grant_User_Admin.sql** y ejecutelo<br>

![](https://github.com/Jucer74/ServicesDevelopment/blob/main/Exams/Exam-01/Images/Image-06.jpg)

![](https://github.com/Jucer74/ServicesDevelopment/blob/main/Exams/Exam-01/Images/Image-07.jpg)


6. Abra el archivo **03_Tab_ReportedCards.sql** y ejecutelo<br>

![](https://github.com/Jucer74/ServicesDevelopment/blob/main/Exams/Exam-01/Images/Image-08.jpg)

![](https://github.com/Jucer74/ServicesDevelopment/blob/main/Exams/Exam-01/Images/Image-09.jpg)


7. Abra el archivo **04_Ins_ReportedCards.sql** y ejecutelo<br>

![](https://github.com/Jucer74/ServicesDevelopment/blob/main/Exams/Exam-01/Images/Image-10.jpg)

![](https://github.com/Jucer74/ServicesDevelopment/blob/main/Exams/Exam-01/Images/Image-11.jpg)

8. Desconectese y Reconectese con el usuario **Admin**<br>
![](https://github.com/Jucer74/ServicesDevelopment/blob/main/Exams/Exam-01/Images/Image-12.jpg)

![](https://github.com/Jucer74/ServicesDevelopment/blob/main/Exams/Exam-01/Images/Image-13.jpg)

9. Ejecute el query de seleccion de los datos para validar que puede obtener los valores.

```sql
USE [NetBankDB]
GO

SELECT * 
FROM ReportedCards
```

![](https://github.com/Jucer74/ServicesDevelopment/blob/main/Exams/Exam-01/Images/Image-14.jpg)

## Preparación de Ambiente
Utilizando la herramienta **GitHub Desktop** sincronice su rama con la rama **main**.

1. Seleccione su Rama y escoja la opcion de hacer **merge** con su rama<br>

![](https://github.com/Jucer74/ServicesDevelopment/blob/main/Exams/Exam-01/Images/Image-15.jpg)

2. Seleccione la rama **main**, resuelva los posibles conflictos y realice el merge.<br>

![](https://github.com/Jucer74/ServicesDevelopment/blob/main/Exams/Exam-01/Images/Image-16.jpg)

3. Realice el **Push** de sus cambios para sincronizar su rama.<br>

![](https://github.com/Jucer74/ServicesDevelopment/blob/main/Exams/Exam-01/Images/Image-17.jpg)

## Proyectos
En la solucion **NetBank** adicione los proyectos para crear la API que permita suplir los servicios necesarios, basandose en la siguiente Vista Logica:

![](https://github.com/Jucer74/ServicesDevelopment/blob/main/Exams/Exam-01/Images/Logical_View.png)


1. Adicione el proyecto **NetBank.Models** de tipo **Class Library** para crear la entidad **ReportedCard** basandose en la estructura de la misma tabla.
2. Adicione el proyecto **NetBank.BusinessLogic** de tipo **Class Library** y crear la clase **CreditCardBL**, para incluir la validacion de la tarjeta y la clase **ReportedCardBL** para la logica de las tarjetas reportadas.
3. Adicione el proyecto **NetBank.DataAccess** de tipo **Class Library** y cree la clase **ReportedCardDA** para incluir la logica de Acceso a los datos.
4. Adicione el proyecto **NetBank.Api** de tipo **ASP.NET Core Web Api** para crear la API con los siguientes endpoints:

![](https://github.com/Jucer74/ServicesDevelopment/blob/main/Exams/Exam-01/Images/Image-18.jpg)


### Endpoints

#### GET /api/v1.0/ReportedCards
Obtener la lista de todas las tarjetas reportadas

##### Request
No tiene

##### Response
Retorna la lista de las tarjetas reportadas, con la siguiente estructura.<br> 

| Property         | Type     | Description                                                                                      |
|------------------|----------|--------------------------------------------------------------------------------------------------|
| Id               | int      | Identificador del registro                                                                       |
| IssuingNetwork   | string   | Nombre de la red emisora                                                                         |
| CreditCardNumber | string   | Numero de la tarjeta de credito (sin caracteres ni separadores)                                  |
| FirstName        | string   | Nombre del propietario de la tarjeta                                                             |
| LastName         | string   | Apellido del propietario de la tarjeta                                                           |
| StatusCard       | string   | Estado de la tarjeta (Stolen, Recovered), or defecto es Stolen                                   |
| ReportedDate     | DateTime | Fecha cuando se reporta la tarjeta                                                               |
| LastUpdatedDate  | DateTime | Fecha de la ultima actualizacion del registro. Inicialmente es igual a la misma fecha de reporte |

##### Sample Response

```json
[{
  "id": 1,
  "issuingNetwork": "visa",
  "creditCardNumber": "4041379326358",
  "firstName": "Ovendale",
  "lastName": "McLaughlan",
  "statusCard": "Stolen",
  "reportedDate": "05/05/2021",
  "lastUpdatedDate": "05/05/2021"
}, {
  "id": 2,
  "issuingNetwork": "maestro",
  "creditCardNumber": "5020049452926487504",
  "firstName": "Brewin",
  "lastName": "Mallindine",
  "statusCard": "Stolen",
  "reportedDate": "04/19/2021",
  "lastUpdatedDate": "04/19/2021"
}, {
  "id": 3,
  "issuingNetwork": "americanexpress",
  "creditCardNumber": "374622152415484",
  "firstName": "Morehall",
  "lastName": "Cantera",
  "statusCard": "Recovered",
  "reportedDate": "06/01/2021",
  "lastUpdatedDate": "06/02/2021"
}, {
  "id": 4,
  "issuingNetwork": "jcb",
  "creditCardNumber": "3565051407123466",
  "firstName": "Frenchum",
  "lastName": "Bailes",
  "statusCard": "Stolen",
  "reportedDate": "06/17/2021",
  "lastUpdatedDate": "06/17/2021"
}, {
  "id": 5,
  "issuingNetwork": "mastercard",
  "creditCardNumber": "5197866564889976",
  "firstName": "Assard",
  "lastName": "Teacy",
  "statusCard": "Stolen",
  "reportedDate": "04/10/2021",
  "lastUpdatedDate": "04/10/2021"
}, {
  "id": 6,
  "issuingNetwork": "mastercard",
  "creditCardNumber": "5527460598288123",
  "firstName": "Hlavecek",
  "lastName": "Glabach",
  "statusCard": "Stolen",
  "reportedDate": "02/14/2022",
  "lastUpdatedDate": "02/14/2022"
}, {
  "id": 7,
  "issuingNetwork": "jcb",
  "creditCardNumber": "3573205851843629",
  "firstName": "MacConnechie",
  "lastName": "Guwer",
  "statusCard": "Recovered",
  "reportedDate": "07/31/2021",
  "lastUpdatedDate": "08/02/2021"
}, {
  "id": 8,
  "issuingNetwork": "jcb",
  "creditCardNumber": "3589587616567128",
  "firstName": "Noot",
  "lastName": "Del Castello",
  "statusCard": "Recovered",
  "reportedDate": "07/28/2021",
  "lastUpdatedDate": "08/02/2021"
}, {
  "id": 9,
  "issuingNetwork": "diners-club-us-ca",
  "creditCardNumber": "5448056326734523",
  "firstName": "Robbey",
  "lastName": "MacArte",
  "statusCard": "Recovered",
  "reportedDate": "08/12/2021",
  "lastUpdatedDate": "08/16/2021"
}, {
  "id": 10,
  "issuingNetwork": "jcb",
  "creditCardNumber": "3562580544482081",
  "firstName": "Myner",
  "lastName": "Petrowsky",
  "statusCard": "Stolen",
  "reportedDate": "07/14/2021",
  "lastUpdatedDate": "07/14/2021"
}]

``` 
---
#### GET /api/v1.0/ReportedCards/IssuingNetwork/{issuingNetworkName}
Obtiene la lista de tarjetas reportadas por una entidad emisora

##### Request
El Nombre de la red emisora se recibe por parametro

| Property           | Type   | Description              |
|--------------------|--------|--------------------------|
| IssuingNetworkName | string | Nombre de la red emisora |

##### Response
Retorna la lista de las tarjetas reportadas para la red emisora, con la siguiente estructura.<br> 

| Property         | Type     | Description                                                                                      |
|------------------|----------|--------------------------------------------------------------------------------------------------|
| Id               | int      | Identificador del registro                                                                       |
| IssuingNetwork   | string   | Nombre de la red emisora                                                                         |
| CreditCardNumber | string   | Numero de la tarjeta de credito (sin caracteres ni separadores)                                  |
| FirstName        | string   | Nombre del propietario de la tarjeta                                                             |
| LastName         | string   | Apellido del propietario de la tarjeta                                                           |
| StatusCard       | string   | Estado de la tarjeta (Stolen, Recovered), or defecto es Stolen                                   |
| ReportedDate     | DateTime | Fecha cuando se reporta la tarjeta                                                               |
| LastUpdatedDate  | DateTime | Fecha de la ultima actualizacion del registro. Inicialmente es igual a la misma fecha de reporte |


##### Sample Response
```json
[{
  "id": 1,
  "issuingNetwork": "mastercard",
  "creditCardNumber": "5100178996051746",
  "firstName": "Beston",
  "lastName": "Roj",
  "statusCard": "Stolen",
  "reportedDate": "10/11/2021",
  "lastUpdatedDate": "10/11/2021"
}, {
  "id": 2,
  "issuingNetwork": "mastercard",
  "creditCardNumber": "5048379452725766",
  "firstName": "Epton",
  "lastName": "MacAne",
  "statusCard": "Stolen",
  "reportedDate": "08/02/2021",
  "lastUpdatedDate": "08/02/2021"
}, {
  "id": 3,
  "issuingNetwork": "mastercard",
  "creditCardNumber": "5178589816288416",
  "firstName": "Ogelbe",
  "lastName": "Berisford",
  "statusCard": "Stolen",
  "reportedDate": "05/03/2021",
  "lastUpdatedDate": "05/03/2021"
}, {
  "id": 4,
  "issuingNetwork": "mastercard",
  "creditCardNumber": "5002356268783019",
  "firstName": "Stangroom",
  "lastName": "Petlyura",
  "statusCard": "Stolen",
  "reportedDate": "03/28/2021",
  "lastUpdatedDate": "03/28/2021"
}, {
  "id": 5,
  "issuingNetwork": "mastercard",
  "creditCardNumber": "5226107894299742",
  "firstName": "Haster",
  "lastName": "Blaisdale",
  "statusCard": "Stolen",
  "reportedDate": "03/03/2022",
  "lastUpdatedDate": "03/03/2022"
}, {
  "id": 6,
  "issuingNetwork": "mastercard",
  "creditCardNumber": "5007663038297591",
  "firstName": "Ciric",
  "lastName": "Schanke",
  "statusCard": "Recovered",
  "reportedDate": "06/28/2021",
  "lastUpdatedDate": "07/02/2021"
}, {
  "id": 7,
  "issuingNetwork": "mastercard",
  "creditCardNumber": "5108755268505103",
  "firstName": "Tesseyman",
  "lastName": "Shadwick",
  "statusCard": "Stolen",
  "reportedDate": "06/13/2021",
  "lastUpdatedDate": "06/13/2021"
}, {
  "id": 8,
  "issuingNetwork": "mastercard",
  "creditCardNumber": "5108753057854592",
  "firstName": "McLeary",
  "lastName": "Cosstick",
  "statusCard": "Recovered",
  "reportedDate": "02/02/2022",
  "lastUpdatedDate": "02/03/2022"
}, {
  "id": 9,
  "issuingNetwork": "mastercard",
  "creditCardNumber": "5100175647837036",
  "firstName": "Gerrish",
  "lastName": "Walster",
  "statusCard": "Stolen",
  "reportedDate": "04/04/2021",
  "lastUpdatedDate": "04/04/2021"
}, {
  "id": 10,
  "issuingNetwork": "mastercard",
  "creditCardNumber": "5100132246802267",
  "firstName": "Kaas",
  "lastName": "Bortoloni",
  "statusCard": "Recovered",
  "reportedDate": "09/27/2021",
  "lastUpdatedDate": "10/04/2021"
}]
```
---
#### GET /api/v1.0/ReportedCards/{creditCardNumber}
Obtiene los datos de la tarjeta por su numero

##### Request
El Numero de la tarjeta se recipe por parametro.

| Property          | Type   | Description          |
|-------------------|--------|----------------------|
| CreditCardNumbwer | string | Numero de la tarjeta |

##### Response
Retorna el registro un solo registro con los datos de la tarjeta

| Property         | Type     | Description                                                                                      |
|------------------|----------|--------------------------------------------------------------------------------------------------|
| Id               | int      | Identificador del registro                                                                       |
| IssuingNetwork   | string   | Nombre de la red emisora                                                                         |
| CreditCardNumber | string   | Numero de la tarjeta de credito (sin caracteres ni separadores)                                  |
| FirstName        | string   | Nombre del propietario de la tarjeta                                                             |
| LastName         | string   | Apellido del propietario de la tarjeta                                                           |
| StatusCard       | string   | Estado de la tarjeta (Stolen, Recovered), or defecto es Stolen                                   |
| ReportedDate     | DateTime | Fecha cuando se reporta la tarjeta                                                               |
| LastUpdatedDate  | DateTime | Fecha de la ultima actualizacion del registro. Inicialmente es igual a la misma fecha de reporte |

##### Sample Response
```json
{
  "id": 1,
  "issuingNetwork": "mastercard",
  "creditCardNumber": "5010120742586830",
  "firstName": "Hammerman",
  "lastName": "Blitzer",
  "statusCard": "Stolen",
  "reportedDate": "12/30/2021",
  "lastUpdatedDate": "12/30/2021"
}
```
---
#### PUT /api/v1.0/ReportedCards/{creditCardNumber}
Reactiva la tarjeta y la marca como recuperada

##### Request
El Numero de la tarjeta se recipe por parametro.

| Property          | Type   | Description          |
|-------------------|--------|----------------------|
| CreditCardNumbwer | string | Numero de la tarjeta |

##### Response
Retorna el estado Success (Status Code 200) con el texto **Credit Card Recovered**.

---
#### POST /api/v1.0/ReportedCards/{creditCardNumber}
Validar si el Numero de una tarjeta es valido por su codigo de verificacion, utilizando el algoritmo de [Luhn](https://www.pcihispano.com/el-algoritmo-de-luhn-y-su-importancia-para-la-validacion-de-tarjetas-de-pago/#:~:text=El%20d%C3%ADgito%20de%20verificaci%C3%B3n%20es,el%20siguiente%20m%C3%BAltiplo%20de%2010.).

Revise este [link](https://www.freeformatter.com/credit-card-number-generator-validator.html) par determinar lsa condiciones sobre los tipos de tarjetas.

| Credit Card Issuer          | Starts With ( IIN Range )                                | Length ( Number of digits ) |
|-----------------------------|----------------------------------------------------------|-----------------------------|
| American Express            | 34, 37                                                   | 15                          |
| Diners Club - Carte Blanche | 300, 301, 302, 303, 304, 305                             | 14                          |
| Diners Club - International | 36                                                       | 14                          |
| Diners Club - USA & Canada  | 54                                                       | 16                          |
| Discover                    | 6011, 622126 to 622925, 644, 645, 646, 647, 648, 649, 65 | 16-19                       |
| InstaPayment                | 637, 638, 639                                            | 16                          |
| JCB                         | 3528 to 3589                                             | 16-19                       |
| Maestro                     | 5018, 5020, 5038, 5893, 6304, 6759, 6761, 6762, 6763     | 16-19                       |
| MasterCard                  | 51, 52, 53, 54, 55, 222100-272099                        | 16                          |
| Visa                        | 4                                                        | 13-16-19                    |
| Visa Electron               | 4026, 417500, 4508, 4844, 4913, 4917                     | 16                          |

[Algoritmo de Luhn en C#](https://github.com/marcrabadan/blog/tree/main/luhn/LuhnAlgorithm)

##### Request
El Numero de la tarjeta se recibe por parametro.

| Property          | Type   | Description          |
|-------------------|--------|----------------------|
| CreditCardNumbwer | string | Numero de la tarjeta |

##### Response
Retorna el estado Success (Status Code 200) con el texto:
- **Credit Card is Valid**: Si el numero de la tarjeta es valido
- **Credit Card is NOT Valid**: Si el Numero de la tarjeta no es valido.

#### Codigos de Respuesta

| HTTP Code | Description           | Scenarios                                                                                                                      |
|-----------|-----------------------|--------------------------------------------------------------------------------------------------------------------------------|
| 200       | Success               | Aplica para todas las respuestas positivas y sin error de todos los endpoints                                                  |
| 400       | Bad Request           | Aplica para los endpoints que reciben el numero de la tarjeta pero  que su digito de verificacion no es valido                 |
| 404       | Not Found             | Aplica para los endpoints que reciben parametros y estos valores  no existen en la base de datos                               |
| 500       | Internal Server Error | Aplica para todos los endpoints cuando se presenta un error no controlado,  por ejemplo no se pudo conectar a la base de datos |

## El Modelo
En el proyecto **NetBank.Models** en la clase **ReportedCard** debe garantizar que adiciona las anotaciones necesarias para identificar que existe un campo llave, y saber cuales campos son o no requeridos.

De igual forma estas anotaciones deben adicionar un mensaje de Error para los campos requeridos.

> **Tip:** Recuerde usar las **DataAnnotations**


## El Acceso a Datos
En el proyecto **NetBank.DataAccess**: 

1. Adicione el paquete para refrenciar el Entity Framework (**Microsoft.EntityFrameworkCore**) en la version adecuado (5.x.x)
2. Adicione el paquete para referenciar la base de datos de SQL Server (**Microsoft.EntityFrameworkCore.SqlServer**) en la version adecuado (5.x.x)
3. Referencie el Projecto de Models para poder hacer uso de las entidades
4. Agregue la clase **AppDbContext** para Referenciar la Base de datos.
5. Verifique que eta clase Hereda de **DbContext**
6. Adicione los contrustores necesarios (Default y Options)
7. Adicione la variable de tipo **DbSet** que corresponde a la tabla **ReportedCards** y que relaciona al modelo **ReportedCard**
8. En la clase **ReportedCardDA**:
	- En el Constructor de la clase, inyecte la instancia del contexto que referencia la base de datos  
	- Implemente la logica para soportar las operaciones necesarias:
		- GetAllReportedCards
		- GetAllReportedCardsByIssuingNetworkName
		- GetReportedCard
		- PutCreditCardReactivated
9. Tenga Presente que estas funciones deben ser Tareas Asincronicas (**async Task<xxx>**)
10. No olvide que la funcion de reactivar Tarjeta, debe:
	- verificar si la tarjeta existe o no, y en caso de No existir, debe retornar el mensaje **"Credit Card Not found"**
	- Actualizar el Status a "Recovered" 
	- Actualizar la Ultima fecha de actualizacion con la fecha actual
	- Retornar el Mensaje **"Credit Card Recovered"** si la operacion es Existosa.

## Logica de Negocio
En el Proyecto **NetBank.BusinessLogic**:
1. Referencie los proyectos de Models y de Acceso a Datos
2. Para la clase **CreditCardBL**:
	- Incluya la logica de validacion del numero de tarjeta, que implementa el algoritmo the Lunh, teniendo presente las longitudes maxima y minima que hay para todas las entidades.
	- Modifique la Implementación de la funcion **IsValid** para que maneje Constantes en lugar de Numeros Magicos
	- Cambie tanto la clase **CreditCardBL** como la funcion **IsValid** para que sean Estaticas y no requerir un constructor.
	- 
> **Tip:** Verifique cual es la longitud maxima y minima de todas las tarjetas segun la redes emisoras que hay.

3. Para la Clase **ReportedCardBL**:
	- En el constructor de la clase , inyecte la instancia al objeto de Acceso a datos
	- Implemente las Funciones necesarias que hagan el llamado a cada una de las funciones del objeo de acceso a datos, correspondiente
		- GetAllReportedCards
		- GetAllReportedCardsByIssuingNetworkName
		- GetReportedCard
		- PutCreditCardReactivated
	- Tenga Presente que estas funciones deben ser Tareas Asincronicas (**async Task<xxx>**)

## Web API
En el Proyecto **NetBank.Api**:
1. Referencie los proyectos de Models y de Logica de Negocio
2. Adicione el paquete que permite referenciar la documentacion de Swagger (**Swashbuckle.AspNetCore**)
3. Adicione el controlador **ReportedCardsController**, de tipo **API copntroller with read/write actions**
4. en el nuevo controlador (**ReportedCardsController**)
	- Modifique la ruta para que incluya la version (**"api/v1.0/[controller]"**)
	- Agregue el constructor que inyecte la instancia del objeto de logica de negocios (**ReportedCardBL**)
	- Cree los Endpoints necesarios para cubrir las funcionalidades requeridas
		- GetAllReportedCards
		- GetAllReportedCardsByIssuingNetworkName
		- GetReportedCard
		- PostCheckCreditCardDigit
		- PutCreditCardReactivated
	- Tenga presente que cada funcion debe ser una tarea asyncronica (**async Task<xxx>**)con una operacion de tipo **ActionResult**, por ejemplo: para el metodo **GetAllReportedCards** la definicion de la funcion seria usando esta definicion:
	```csharp
    // GET: api/v1.0/<ReportedCardsController>
    [HttpGet]
    public async Task<ActionResult<IList<ReportedCard>>> GetAllReportedCards()
    {
       return Ok(await _reportedCardBL.GetAllReportedCards());
    }
	```
	- Complete los demas endpoints teniendo en cuenta la definicion de los mismos, tenga presente que las anotaciones definien si el action es GET, PUT, POST, etc.

5. Adicione la nueva cadena de conexion al archivo **AppSettings**

```json
"ConnectionStrings": {
      "CnnStr": "data source=localhost; initial catalog=NetBankDB; User Id=Admin; Password=Admin123; MultipleActiveResultSets=true"
   }
```
6. En el **StartUp**
	- En el Metodo de **Configureservices**
		- Adicione el llamado al DB Context leyendo la cadena de conexion
		```csharp
		services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CnnStr")));
		```

		- Adicione al Scope el llamado de los objetos de Logica de Negocio y de Acceso a datos
		```csharp
         services.AddScoped<ReportedCardDA>();
         services.AddScoped<ReportedCardBL>();
		````
 
7. Limpie el proyecto, eliminando la referencia a controladores y modelos innecesarios, por ejemplo el controladore del clima (al controladore del clima (**WeatherForecastController**) y el modelo (**WeatherForecast**)

8. Modifique la solución para que su punto de entrada sea la API.

![](https://github.com/Jucer74/ServicesDevelopment/blob/main/Exams/Exam-01/Images/Image-19.jpg)

9. Compile, ejecute y pruebe. Recuerde usar la Version de **NetBank.Api** y no la version de IIS Express.

![](https://github.com/Jucer74/ServicesDevelopment/blob/main/Exams/Exam-01/Images/Image-20.jpg)


# NOTA
RECUERDE SUBIR SU SOLUCIÓN A SU RAMA DE ESTE REPOSITORIO.

---

# Revision

## General (10 pts)
- Proyecto Bien Organizado y en carpetas (1 pt)
- Compila (0 Errors, 0 Warnings) (2pt)
- Funciona y hace lo que debe sin errores (7 pt)
  ReportedCards
  - GetAllReportedCards = 1
  - GetAllReportedCardsByIssuingNetworkName = 1
  - GetReportedCard = 1
  - PutCreditCardReactivated = 2 (Reactivated y Not Found)
  CreditCard
  - IsValid = 2 (Valid , Invalid)

## Models (2pts)
- ReportedCard 
  - Estructura = 1
  - Campos Requeridos = 1
  
## Data Access (10 pts)
- AppDbContext (4 pts)
  - Constructor
    - Default = 1
    - Options = 1
  - DbSet (singular class, Plural Entidad)  = 2
- ReportedCardDA (6 pts)
  - Constructor con Inyeccion de Context Object = 1
  - GetAllReportedCards = 1
  - GetAllReportedCardsByIssuingNetworkName = 1
  - GetReportedCard = 1
  - PutCreditCardReactivated = 2 (Reactivated y Not Found)

## Business Logic (15 pts)
- CreditCardBL (10 pts)
  - Clase y Metodo IsValid, estaticos (2 pts)
  - Constantes y no Numeros (5 pts)
    - MAX_ADDEND = 9;
    - MAX_CREDIT_CARD_LENGTH = 19;
    - MIN_CREDIT_CARD_LENGTH = 13;
    - MOD_10 = 10;
    - MULTIPLY_NUMBER = 2;
  - Min y Max length (13-19) = 2 
  - GetDigits (1 pts)
    - Refactor usando Where
- ReportedCardBL (5 pts)
  - Constructor con Inyeccion de DA Object = 1
  - GetAllReportedCards = 1
  - GetAllReportedCardsByIssuingNetworkName = 1
  - GetReportedCard = 1
  - PutCreditCardReactivated = 1
    
## API (13 pts)
- ReportedCardsController (8 pts)
  - Inclusin de version en la Ruta = 1
  - Inyeccion del BL Object = 1
  - GetAllReportedCards = 1
  - GetAllReportedCardsByIssuingNetworkName = 1
  - GetReportedCard = 1
  - PostCheckCreditCardDigit = 2 ( Valid, Not Valid)
  - PutCreditCardReactivated = 1
- StartUp (5 pts)
  - Referencia a Swagger = 1
  - Adicion del AppDbContext Object = 1
  - Adicion de los Scopes (2 pts)
    - ReportedCardDA = 1
    - ReportedCardBL = 1
  - Limpieza de otras referencias (Weather) = 1
  

 