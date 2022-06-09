CREATE TABLE `pricatdb`.`products` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `CategoryId` INT NOT NULL,
  `EanCode` VARCHAR(13) NOT NULL,
  `Description` VARCHAR(50) NOT NULL,
  `Unit` VARCHAR(20) NOT NULL,
  `Price` DECIMAL(13,2) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `EanCode_UNIQUE` (`EanCode` ASC) VISIBLE,
  INDEX `FX_Products_Categories_idx` (`CategoryId` ASC) VISIBLE,
  CONSTRAINT `FX_Products_Categories` FOREIGN KEY (`CategoryId`)  REFERENCES `pricatdb`.`categories` (`Id`)
);
