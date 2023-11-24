CREATE TABLE `productservicedb`.`Products` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `CategoryId` INT NOT NULL,
  `CategoryName` VARCHAR(50) NOT NULL,  
  `EanCode` VARCHAR(13) NOT NULL,
  `Description` VARCHAR(50) NOT NULL,
  `Unit` VARCHAR(20) NOT NULL,
  `Price` DECIMAL(13,2) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `EanCode_UNIQUE` (`EanCode` ASC) VISIBLE);
