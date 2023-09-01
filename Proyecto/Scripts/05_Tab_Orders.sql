CREATE TABLE `arepasdb`.`orders` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `CustomerId` INT NOT NULL,
  `DeliveryFullName` VARCHAR(100) NOT NULL,
  `DeliveryAddress` VARCHAR(250) NOT NULL,
  `DeliveryPhoneNumber` VARCHAR(50) NOT NULL,
  `TotalPrice` DECIMAL(13,2) NOT NULL,
  `Notes` VARCHAR(250) NULL,
  PRIMARY KEY (`Id`),
  INDEX `FK_Orders_Customers_idx` (`CustomerId` ASC) VISIBLE,
  CONSTRAINT `FK_Orders_Customers`
    FOREIGN KEY (`CustomerId`)
    REFERENCES `arepasdb`.`customers` (`Id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE);
