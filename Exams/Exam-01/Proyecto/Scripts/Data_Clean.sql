DELETE FROM `arepasdb`.`orderdetails` WHERE Id >= 0;
ALTER TABLE `arepasdb`.`orderdetails` AUTO_INCREMENT = 1;
DELETE FROM `arepasdb`.`orders` WHERE Id >= 0;
ALTER TABLE `arepasdb`.`orders` AUTO_INCREMENT = 1;
DELETE FROM `arepasdb`.`products` WHERE Id >= 0;
ALTER TABLE `arepasdb`.`products` AUTO_INCREMENT = 1;
DELETE FROM `arepasdb`.`customers` WHERE Id >= 0;
ALTER TABLE `arepasdb`.`customers` AUTO_INCREMENT = 1;