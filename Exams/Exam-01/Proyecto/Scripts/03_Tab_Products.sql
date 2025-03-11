CREATE TABLE `arepasdb`.`products` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(50) NOT NULL,
  `Description` VARCHAR(250) NULL,
  `Price` DECIMAL(13,2) NOT NULL,
  `Image` VARCHAR(250) NULL,
  PRIMARY KEY (`Id`));
