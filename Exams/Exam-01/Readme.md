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

