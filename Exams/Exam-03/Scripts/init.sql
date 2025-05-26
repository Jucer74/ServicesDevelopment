CREATE DATABASE IF NOT EXISTS moneybankdb;
USE moneybankdb;

CREATE USER IF NOT EXISTS 'moneybankuser'@'%' IDENTIFIED BY 'M0n3yB4nkUs3r*01';
GRANT ALL PRIVILEGES ON moneybankdb.* TO 'moneybankuser'@'%';

DROP TABLE IF EXISTS Accounts;

CREATE TABLE Accounts (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    AccountType CHAR(1) NOT NULL,
    CreationDate DATETIME NOT NULL,
    AccountNumber VARCHAR(10) NOT NULL,
    OwnerName VARCHAR(100) NOT NULL,
    BalanceAmount DECIMAL(18,2) NOT NULL,
    OverdraftAmount DECIMAL(18,2) NOT NULL
);

INSERT INTO Accounts VALUES
(1, 'C', '2023-06-30', '3016892501', 'Yurley Orejuela Ramirez', 1500000, 0), 
(2, 'A', '2023-07-23', '5263418932', 'Libia Oquenda', 250000, 0), 
(3, 'C', '2023-08-25', '6703001327', 'John Fredy Vásquez Izquierdo', 50000, 950000);

FLUSH PRIVILEGES;
