-- create the user on the target database for the login
USE [NetBankDB]
GO
CREATE USER [Admin] FOR LOGIN [Admin]
GO

-- add the user to the desired role
ALTER ROLE [db_owner] ADD MEMBER [Admin]
GO