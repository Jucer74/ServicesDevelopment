/************
*-- Clean --*
************/
DELETE FROM `arepasdb`.`orderdetails` WHERE Id >= 0;
ALTER TABLE `arepasdb`.`orderdetails` AUTO_INCREMENT = 1;
DELETE FROM `arepasdb`.`orders` WHERE Id >= 0;
ALTER TABLE `arepasdb`.`orders` AUTO_INCREMENT = 1;
DELETE FROM `arepasdb`.`products` WHERE Id >= 0;
ALTER TABLE `arepasdb`.`products` AUTO_INCREMENT = 1;
DELETE FROM `arepasdb`.`customers` WHERE Id >= 0;
ALTER TABLE `arepasdb`.`customers` AUTO_INCREMENT = 1;
/****************
*-- Customers --*
****************/
INSERT INTO `arepasdb`.`customers`(`UserEmail`, `FullName`, `Address`, `PhoneNumber`, `Password`) VALUES ('Alejandro.Gonzalez@email.com','Alejandro Gonzalez','Calle 5 # 12-34, Barrio San Fernando','310-123-4567','Al3j4ndr0*01');
INSERT INTO `arepasdb`.`customers`(`UserEmail`, `FullName`, `Address`, `PhoneNumber`, `Password`) VALUES ('Sofia.Herrera@email.com','Sofia Herrera','Avenida 3N # 8-56, Barrio Granada','311-234-5678','S0f14*1.990!');
INSERT INTO `arepasdb`.`customers`(`UserEmail`, `FullName`, `Address`, `PhoneNumber`, `Password`) VALUES ('Mateo.Castro@email.com','Mateo Castro','Carrera 10 # 23-45, Barrio Santa Teresita','312-345-6789','M4t30#2.005!');
INSERT INTO `arepasdb`.`customers`(`UserEmail`, `FullName`, `Address`, `PhoneNumber`, `Password`) VALUES ('Valentina.Vargas@email.com','Valentina Vargas','Calle 8 # 17-89, Barrio San Antonio','313-456-7890','V4l3nt1n4#01');
INSERT INTO `arepasdb`.`customers`(`UserEmail`, `FullName`, `Address`, `PhoneNumber`, `Password`) VALUES ('Sebastian.Silva@email.com','Sebastian Silva','Avenida 6N # 9-87, Barrio El PeÃ±Ã³n','314-567-8901','S3b4st14n$01');
INSERT INTO `arepasdb`.`customers`(`UserEmail`, `FullName`, `Address`, `PhoneNumber`, `Password`) VALUES ('Isabella.Lopez@email.com','Isabella Lopez','Carrera 15 # 21-43, Barrio Centenario','315-678-9012','Is4b3ll4*123');
INSERT INTO `arepasdb`.`customers`(`UserEmail`, `FullName`, `Address`, `PhoneNumber`, `Password`) VALUES ('Daniel.Ramirez@email.com','Daniel Ramirez','Calle 12 # 30-54, Barrio Versalles','316-789-0123','D4n13l*2.000');
INSERT INTO `arepasdb`.`customers`(`UserEmail`, `FullName`, `Address`, `PhoneNumber`, `Password`) VALUES ('Camila.Guzman@email.com','Camila Guzman','Avenida 4N # 7-65, Barrio San Cayetano','317-890-1234','C4m1l4#1.980');
INSERT INTO `arepasdb`.`customers`(`UserEmail`, `FullName`, `Address`, `PhoneNumber`, `Password`) VALUES ('Nicolas.Morales@email.com','Nicolas Morales','Carrera 20 # 15-32, Barrio Alameda','318-901-2345','N1c0l4s%1234');
INSERT INTO `arepasdb`.`customers`(`UserEmail`, `FullName`, `Address`, `PhoneNumber`, `Password`) VALUES ('Valeria.Torres@email.com','Valeria Torres','Calle 9 # 18-76, Barrio La Flora','319-012-3456','V4l3r14#1995');
INSERT INTO `arepasdb`.`customers`(`UserEmail`, `FullName`, `Address`, `PhoneNumber`, `Password`) VALUES ('Santiago.Mendoza@email.com','Santiago Mendoza','Avenida 8N # 11-23, Barrio Ciudad JardÃ­n','320-123-4567','S4nt14g0&Cia');
INSERT INTO `arepasdb`.`customers`(`UserEmail`, `FullName`, `Address`, `PhoneNumber`, `Password`) VALUES ('Natalia.Jimenez@email.com','Natalia Jimenez','Carrera 12 # 25-67, Barrio San Bosco','321-234-5678','N4t4l14!2004');
INSERT INTO `arepasdb`.`customers`(`UserEmail`, `FullName`, `Address`, `PhoneNumber`, `Password`) VALUES ('Leonardo.Reyes@email.com','Leonardo Reyes','Calle 7 # 14-98, Barrio Gran Limonar','322-345-6789','L30n4rd0#123');
INSERT INTO `arepasdb`.`customers`(`UserEmail`, `FullName`, `Address`, `PhoneNumber`, `Password`) VALUES ('Mariana.Paredes@email.com','Mariana Paredes','Avenida 5N # 10-32, Barrio NormandÃ­a','323-456-7890','M4r14n4*1234');
INSERT INTO `arepasdb`.`customers`(`UserEmail`, `FullName`, `Address`, `PhoneNumber`, `Password`) VALUES ('Andres.Acosta@email.com','Andres Acosta','Carrera 18 # 21-43, Barrio San Fernando','324-567-8901','Andr3s#1.992');
INSERT INTO `arepasdb`.`customers`(`UserEmail`, `FullName`, `Address`, `PhoneNumber`, `Password`) VALUES ('Antonella.Rios@email.com','Antonella Rios','Calle 11 # 17-54, Barrio Granada','325-678-9012','Ant0n3ll4*01');
INSERT INTO `arepasdb`.`customers`(`UserEmail`, `FullName`, `Address`, `PhoneNumber`, `Password`) VALUES ('Rafael.Salazar@email.com','Rafael Salazar','Avenida 7N # 9-87, Barrio Santa Teresita','326-789-0123','R4f43l*1.997');
INSERT INTO `arepasdb`.`customers`(`UserEmail`, `FullName`, `Address`, `PhoneNumber`, `Password`) VALUES ('Gabriela.Bravo@email.com','Gabriela Bravo','Carrera 13 # 20-45, Barrio San Antonio','327-890-1234','Gabr13l4!321');
INSERT INTO `arepasdb`.`customers`(`UserEmail`, `FullName`, `Address`, `PhoneNumber`, `Password`) VALUES ('Emiliano.Ortega@email.com','Emiliano Ortega','Calle 4 # 7-89, Barrio El PeÃ±Ã³n','328-901-2345','Em1l14n0#123');
INSERT INTO `arepasdb`.`customers`(`UserEmail`, `FullName`, `Address`, `PhoneNumber`, `Password`) VALUES ('Angela.Gutierrez@email.com','Angela Gutierrez','Avenida 5N # 8-76, Barrio Centenario','329-012-3456','Ang3l4*2.000');
/***************
*-- Products --*
***************/
INSERT INTO `arepasdb`.`products` (`Name`, `Description`, `Price`, `Image`) VALUES ('Arepa Sencilla',null,2000,'Arepa-Sencilla.jpg');
INSERT INTO `arepasdb`.`products` (`Name`, `Description`, `Price`, `Image`) VALUES ('Arepa con queso doble crema',null,2500,'Arepa-Rellena-de-Queso.jpg');
INSERT INTO `arepasdb`.`products` (`Name`, `Description`, `Price`, `Image`) VALUES ('Arepa con Jamon y queso doble crema',null,3500,'Arepa-Rellena-Jamon-Queso,jpg');
INSERT INTO `arepasdb`.`products` (`Name`, `Description`, `Price`, `Image`) VALUES ('Arepa con Queso Cuajada',null,3000,'Arepa-Rellena-de-Queso-Cuajada.jpg');
INSERT INTO `arepasdb`.`products` (`Name`, `Description`, `Price`, `Image`) VALUES ('Arepa con Huevos Revueltos',null,4000,'Arepa-Rellena-Huevos-Revueltos.jpg');
INSERT INTO `arepasdb`.`products` (`Name`, `Description`, `Price`, `Image`) VALUES ('Arepa con Salchicha Ranchera',null,5000,'Arepa-Rellena-Salchicha-Ranchera.png');
INSERT INTO `arepasdb`.`products` (`Name`, `Description`, `Price`, `Image`) VALUES ('Arepa con Pollo Desmechado',null,6000,'Arepa-Rellena-Pollo.jpg');
INSERT INTO `arepasdb`.`products` (`Name`, `Description`, `Price`, `Image`) VALUES ('Arepa con Carne Desmechada',null,7000,'Arepa-Rellena-Carne.jpg');
INSERT INTO `arepasdb`.`products` (`Name`, `Description`, `Price`, `Image`) VALUES ('Arepa con Chorizo','Rico y delicioso chorizo santarosano y arepa 100% maiz ',8000,'Arepa-Con-Chorizo.jpg');
INSERT INTO `arepasdb`.`products` (`Name`, `Description`, `Price`, `Image`) VALUES ('Arepa Mixta 2 Ingredientes','Puedes elegir dos carnes Pollo, carne , chorizos y ranchera',8000,'Arepas-Rellenas.jfif');
INSERT INTO `arepasdb`.`products` (`Name`, `Description`, `Price`, `Image`) VALUES ('Arepa Mixta 3 Ingredientes','Puedes elegir tres carnes Pollo, carne chorizos y ranchera',9000,'Arepas-Rellenas.jfif');
INSERT INTO `arepasdb`.`products` (`Name`, `Description`, `Price`, `Image`) VALUES ('Arepa Mixta 4 Ingredientes','Contiene pollo,carne , chorizos y ranchera ',10000,'Arepas-Rellenas.jfif');
INSERT INTO `arepasdb`.`products` (`Name`, `Description`, `Price`, `Image`) VALUES ('Arepa con Todo','Contiene Queso doble crema, pollo, chicharrÃ³n, carne, chorizos, pico de gallo',13000,'Arepa-Rellena-Con-Todo.jpg');
/*************
*-- Orders --*
*************/
INSERT INTO `arepasdb`.`orders` ( `CustomerId`, `DeliveryFullName`, `DeliveryAddress`, `DeliveryPhoneNumber`, `TotalPrice`, `Notes`) VALUES (1,'Alejandro Gonzalez','Calle 5 # 12-34, Barrio San Fernando','310-123-4567',5500,'Tocar el Timbre');
INSERT INTO `arepasdb`.`orders` ( `CustomerId`, `DeliveryFullName`, `DeliveryAddress`, `DeliveryPhoneNumber`, `TotalPrice`, `Notes`) VALUES (1,'Alejandro Gonzalez','Avenida 3N # 8-56, Barrio Granada','311-234-5678',19000,'Estoy en casa de mi Novia');
INSERT INTO `arepasdb`.`orders` ( `CustomerId`, `DeliveryFullName`, `DeliveryAddress`, `DeliveryPhoneNumber`, `TotalPrice`, `Notes`) VALUES (3,'Mateo Castro','Carrera 10 # 23-45, Barrio Santa Teresita','312-345-6789',26000,'Traer Vuelta de un billete de 50');
INSERT INTO `arepasdb`.`orders` ( `CustomerId`, `DeliveryFullName`, `DeliveryAddress`, `DeliveryPhoneNumber`, `TotalPrice`, `Notes`) VALUES (4,'Valentina Vargas','Calle 8 # 17-89, Barrio San Antonio','313-456-7890',18000,'Sin Salsas');
INSERT INTO `arepasdb`.`orders` ( `CustomerId`, `DeliveryFullName`, `DeliveryAddress`, `DeliveryPhoneNumber`, `TotalPrice`, `Notes`) VALUES (5,'Sebastian Silva','Avenida 6N # 9-87, Barrio El Peñón','314-567-8901',12000,null);
/*******************
*-- OrderDetails --*
*******************/
INSERT INTO `arepasdb`.`orderdetails` ( `OrderId`, `ProductId`, `Quantity`, `PriceOrd`) VALUES (1,2,1,2500);
INSERT INTO `arepasdb`.`orderdetails` ( `OrderId`, `ProductId`, `Quantity`, `PriceOrd`) VALUES (1,4,1,3000);
INSERT INTO `arepasdb`.`orderdetails` ( `OrderId`, `ProductId`, `Quantity`, `PriceOrd`) VALUES (2,7,2,12000);
INSERT INTO `arepasdb`.`orderdetails` ( `OrderId`, `ProductId`, `Quantity`, `PriceOrd`) VALUES (2,8,1,7000);
INSERT INTO `arepasdb`.`orderdetails` ( `OrderId`, `ProductId`, `Quantity`, `PriceOrd`) VALUES (3,13,2,26000);
INSERT INTO `arepasdb`.`orderdetails` ( `OrderId`, `ProductId`, `Quantity`, `PriceOrd`) VALUES (4,6,1,5000);
INSERT INTO `arepasdb`.`orderdetails` ( `OrderId`, `ProductId`, `Quantity`, `PriceOrd`) VALUES (4,7,1,6000);
INSERT INTO `arepasdb`.`orderdetails` ( `OrderId`, `ProductId`, `Quantity`, `PriceOrd`) VALUES (4,8,1,7000);
INSERT INTO `arepasdb`.`orderdetails` ( `OrderId`, `ProductId`, `Quantity`, `PriceOrd`) VALUES (5,1,2,4000);
INSERT INTO `arepasdb`.`orderdetails` ( `OrderId`, `ProductId`, `Quantity`, `PriceOrd`) VALUES (5,5,2,8000);

