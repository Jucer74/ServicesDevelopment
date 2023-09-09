CREATE TABLE `teamsdb`.`TeamMembers` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `TeamId` INT NOT NULL,
    `FirstName` VARCHAR(50) NOT NULL,
    `LastName` VARCHAR(50) NOT NULL,
    `BirthDate` DATETIME NOT NULL,
    `Phone`  VARCHAR(50) NULL,
  PRIMARY KEY (`Id`));

ALTER TABLE `teamsdb`.`TeamMembers`
ADD FOREIGN KEY (TeamId) REFERENCES Teams(Id);