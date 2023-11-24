-- Clean Products
DELETE FROM `productservicedb`.`Products` WHERE Id >= 0;
ALTER TABLE `productservicedb`.`Products` AUTO_INCREMENT = 1;

-- Insert Products
INSERT INTO `productservicedb`.`Products` (`CategoryId`, `CategoryName`,`EanCode`, `Description`, `Unit`, `Price`) VALUES ( 1, 'Alimentos', '7707548516286', 'Arroz', 'Lb', 500.00);
INSERT INTO `productservicedb`.`Products` (`CategoryId`, `CategoryName`,`EanCode`, `Description`, `Unit`, `Price`) VALUES ( 1, 'Alimentos', '7707548941507', 'Papa', 'Lb', 1500.00);
INSERT INTO `productservicedb`.`Products` (`CategoryId`, `CategoryName`,`EanCode`, `Description`, `Unit`, `Price`) VALUES ( 2, 'Bebidas', '7707548160274', 'Cocacola', 'Lb', 2500.00);
INSERT INTO `productservicedb`.`Products` (`CategoryId`, `CategoryName`,`EanCode`, `Description`, `Unit`, `Price`) VALUES ( 2, 'Bebidas', '7707548110958', 'Pepsi', 'Und', 2500.00);
INSERT INTO `productservicedb`.`Products` (`CategoryId`, `CategoryName`,`EanCode`, `Description`, `Unit`, `Price`) VALUES ( 3, 'Productos de Aseo', '7707548758303', 'Detergente', 'Kg', 12500.00);
INSERT INTO `productservicedb`.`Products` (`CategoryId`, `CategoryName`,`EanCode`, `Description`, `Unit`, `Price`) VALUES ( 3, 'Productos de Aseo', '7707548210801', 'Cloro', 'CC', 21500.00);
INSERT INTO `productservicedb`.`Products` (`CategoryId`, `CategoryName`,`EanCode`, `Description`, `Unit`, `Price`) VALUES ( 4, 'Ropa', '7707548472247', 'Camisa', 'Und', 1500.00);
INSERT INTO `productservicedb`.`Products` (`CategoryId`, `CategoryName`,`EanCode`, `Description`, `Unit`, `Price`) VALUES ( 4, 'Ropa', '7707548427902', 'Pantalon', 'Und', 1500.00);
INSERT INTO `productservicedb`.`Products` (`CategoryId`, `CategoryName`,`EanCode`, `Description`, `Unit`, `Price`) VALUES ( 5, 'Medicamentos', '7707548799412', 'Jarabe para la Tos', 'Und', 32500.00);
INSERT INTO `productservicedb`.`Products` (`CategoryId`, `CategoryName`,`EanCode`, `Description`, `Unit`, `Price`) VALUES ( 5, 'Medicamentos', '7707548861546', 'Aspirina 500 mg x 20 Unidades', 'Caja', 42500.00);
