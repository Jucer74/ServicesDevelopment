USE [master]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [userschool]    Script Date: 8/12/2022 8:58:55 PM ******/
CREATE LOGIN [userschool] WITH PASSWORD=N'EDQDM/t1KUddFUXiBCLI/VTntNJYZ7oowJu/+a8JArs=', DEFAULT_DATABASE=[SchoolDB], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

ALTER LOGIN [userschool] DISABLE
GO

ALTER SERVER ROLE [sysadmin] ADD MEMBER [userschool]
GO

USE [SchoolDB]
GO

/****** Object:  User [userschool]    Script Date: 8/12/2022 8:59:49 PM ******/
CREATE USER [userschool] FOR LOGIN [userschool] WITH DEFAULT_SCHEMA=[dbo]
GO

