-- Drop the table if it exists
DROP TABLE IF EXISTS `moneybankdb`.`accounts`;

-- Create the table again
CREATE TABLE `moneybankdb`.`accounts` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `AccountType` VARCHAR(1) NOT NULL,
  `CreationDate` DATETIME NOT NULL,
  `AccountNumber` VARCHAR(10) NOT NULL,
  `OwnerName` VARCHAR(100) NOT NULL,
  `BalanceAmount` DECIMAL(18,2) NOT NULL,
  `OverdraftAmount` DECIMAL(18,2) NOT NULL,
  PRIMARY KEY (`Id`)
);

-- Insert the data again
INSERT INTO `moneybankdb`.`accounts` 
(`Id`, `AccountType`, `CreationDate`, `AccountNumber`, `OwnerName`, `BalanceAmount`, `OverdraftAmount`) 
VALUES
(1, 'C', '2023-06-30', '3016892501', 'Yurley Orejuela Ramirez', 1500000, 0), 
(2, 'A', '2023-07-23', '5263418932', 'Libia Oquenda', 250000, 0), 
(3, 'C', '2023-08-25', '6703001327', 'John Fredy Vásquez Izquierdo', 50000, 950000);