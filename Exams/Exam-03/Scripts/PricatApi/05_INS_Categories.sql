DELETE FROM `pricatdb`.`Categories` WHERE Id >= 0;
ALTER TABLE `pricatdb`.`Categories` AUTO_INCREMENT = 1;
INSERT INTO `pricatdb`.`Categories`(`Description`) VALUES('Alimentos');
INSERT INTO `pricatdb`.`Categories`(`Description`) VALUES('Bebidas');
INSERT INTO `pricatdb`.`Categories`(`Description`) VALUES('Productos de Aseo');
INSERT INTO `pricatdb`.`Categories`(`Description`) VALUES('Ropa');
INSERT INTO `pricatdb`.`Categories`(`Description`) VALUES('Medicamentos');
