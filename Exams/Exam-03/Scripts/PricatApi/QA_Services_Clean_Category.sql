-- Clean Categories
DELETE FROM `categoryservicedb`.`Categories` WHERE Id >= 0;
ALTER TABLE `categoryservicedb`.`Categories` AUTO_INCREMENT = 1;

-- Insert Categories
INSERT INTO `categoryservicedb`.`Categories`(`Description`) VALUES('Alimentos');
INSERT INTO `categoryservicedb`.`Categories`(`Description`) VALUES('Bebidas');
INSERT INTO `categoryservicedb`.`Categories`(`Description`) VALUES('Productos de Aseo');
INSERT INTO `categoryservicedb`.`Categories`(`Description`) VALUES('Ropa');
INSERT INTO `categoryservicedb`.`Categories`(`Description`) VALUES('Medicamentos');