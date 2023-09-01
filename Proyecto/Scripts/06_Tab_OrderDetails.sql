CREATE TABLE `arepasdb`.`orderdetails` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `OrderId` INT NOT NULL,
  `ProductId` INT NOT NULL,
  `Quantity` INT NOT NULL,
  `PriceOrd` DECIMAL(13,2) NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `FX_OrderDetails_Orders_idx` (`OrderId` ASC) VISIBLE,
  INDEX `FK_OrderDetails_Products_idx` (`ProductId` ASC) VISIBLE,
  CONSTRAINT `FK_OrderDetails_Orders`
    FOREIGN KEY (`OrderId`)
    REFERENCES `arepasdb`.`orders` (`Id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE,
  CONSTRAINT `FK_OrderDetails_Products`
    FOREIGN KEY (`ProductId`)
    REFERENCES `arepasdb`.`products` (`Id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE);
