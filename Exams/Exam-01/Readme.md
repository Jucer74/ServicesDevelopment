# Contexto
**NetBank** es una empresa reguladora de tarjetas de credito ala cual todos los bancos le reportan las tarjetas robadas.

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

## Web API
En la solucion **NetBank** adicione el proyecto para crear la API basandose en la siguiente Vista Logica:

![](https://github.com/Jucer74/ServicesDevelopment/blob/main/Exams/Exam-01/Images/Logical_View.png)


1. Adicione el proyecto **NetBank.Models** de tipo **Class Library** para crear la entidad **ReportedCard** basandose en la estructura de la misma tabla.
2. Adiciones el proyecto **NetBank.BusinessLogic** de tipo **Class Library** para crear la entidad **CreditCardBL**, para incluir la validacion de la tarjeta.
3. Adicione el proyecto **NetBank.DataAccess** de tipo **Class Library** para incluir la logica de Acceso a los datos.
4. Adicione el proyecto **NetBank.Api** de tipo ASP.NET Core Web Api para crear la API con los siguientes endpoints:

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


# NOTA
RECUERDE SUBIR SU SOLUCIÓN A SU RAMA DE ESTE REPOSITORIO.

En la creacion de la API incluir la Ruta en el startup
para adicionar el Swagger ,incluir el MVC
Adicionar el controller como read, write
Cambiar la firma de los metodos del controlador a async Task<Actionresult> 