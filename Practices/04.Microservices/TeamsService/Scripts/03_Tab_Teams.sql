CREATE TABLE `teamsservicedb`.`Teams` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(50) NOT NULL,
  `Coach`  VARCHAR(50) NULL,
  `Conference`  VARCHAR(20) NULL,
  PRIMARY KEY (`Id`));