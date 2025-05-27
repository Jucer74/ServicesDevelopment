-- Drop the table if it exists
DELETE FROM `moneybankdb`.`accounts` WHERE Id >= 0;
ALTER TABLE `moneybankdb`.`accounts` AUTO_INCREMENT = 1;

-- Insert the data again
INSERT INTO `moneybankdb`.`accounts` 
(`Id`, `AccountType`, `CreationDate`, `AccountNumber`, `OwnerName`, `BalanceAmount`, `OverdraftAmount`) 
VALUES
(1, 'C', '2023-06-30', '3016892501', 'Yurley Orejuela Ramirez', 1500000, 0), 
(2, 'A', '2023-07-23', '5263418932', 'Libia Oquenda', 250000, 0), 
(3, 'C', '2023-08-25', '6703001327', 'John Fredy Vásquez Izquierdo', 50000, 950000);