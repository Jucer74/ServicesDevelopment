DELETE FROM `pricatdb`.`Products` WHERE Id >= 0;
ALTER TABLE `pricatdb`.`Products` AUTO_INCREMENT = 1;
DELETE FROM `pricatdb`.`Categories` WHERE Id >= 0;
ALTER TABLE `pricatdb`.`Categories` AUTO_INCREMENT = 1;