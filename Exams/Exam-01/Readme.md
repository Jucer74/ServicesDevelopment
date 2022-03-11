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
Cree la base de datos **NetBakDB** ejecutando los siguientes pasos:

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
En la solucion **NetBank** adicione el proyecto para crear la API.

1. Adicione el proyecto **NetBank.Models** de tipo **Class Library** para crear la entidad **ReportedCard** basandose en la estructura de la misma tabla.   
2. Adicione el proyecto **NetBank.Api** de tipo ASP.NET Core Web Api para crear la API con los siguientes endpoints:

### Endpoints

#### GET /api/v1.0/ReportedCards
Obtener la lista de todas las tarjetas reportadas

##### Request
No tiene

##### Response
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

 
- 
- /api/v1.0/ReportedCards/IssuingNetwork/{issuingNetworkName}
- /api/v1.0/ReportedCard/{CardNumber}
- /api/v1.0/ReportedCard/{CardNumber}
- /api/v1.0/ReportedCard/{CardNumber}

3. aaa 