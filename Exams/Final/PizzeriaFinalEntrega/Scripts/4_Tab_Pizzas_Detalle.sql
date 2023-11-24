CREATE TABLE `pizzadb`.`PizzasCategoria` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `PizzasId` INT NOT NULL,
  `Nombre` VARCHAR(50) NOT NULL,
  `Tamaño` VARCHAR(50) NOT NULL,
  `Precio` INT NOT NULL,
  PRIMARY KEY (`Id`));
  
ALTER TABLE `pizzadb`.`PizzasCategoria`
ADD FOREIGN KEY (PizzasId) REFERENCES Pizzas(Id);  
  
  