USE moneybankdb;

-- Eliminar todos los registros
DELETE FROM accounts;

-- Reiniciar el autoincremento
ALTER TABLE accounts AUTO_INCREMENT = 1;

-- (Opcional) Volver a insertar datos de prueba
INSERT INTO accounts (AccountType, CreationDate, AccountNumber, OwnerName, BalanceAmount, OverdraftAmount) VALUES
('C', '2023-06-30', '3016892501', 'Yurley Orejuela Ramirez', 1500000, 0),
('A', '2023-07-23', '5263418932', 'Libia Oquenda', 250000, 0),
('C', '2023-08-25', '6703001327', 'John Fredy Vásquez Izquierdo', 50000, 950000);
