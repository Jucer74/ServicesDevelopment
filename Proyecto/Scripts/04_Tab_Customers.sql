CREATE TABLE `arepasdb`.`customers` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `UserEmail` VARCHAR(250) NOT NULL,
  `FullName` VARCHAR(100) NOT NULL,
  `Address` VARCHAR(250) NOT NULL,
  `PhoneNumber` VARCHAR(50) NOT NULL,
  `Password` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `UserEmail_UNIQUE` (`UserEmail` ASC) VISIBLE);
