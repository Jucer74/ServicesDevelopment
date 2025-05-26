USE moneybankdb;

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