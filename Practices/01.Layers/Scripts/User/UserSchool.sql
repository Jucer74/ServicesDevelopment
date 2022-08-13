USE [master]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [userschool]    Script Date: 8/12/2022 8:32:14 PM ******/
CREATE LOGIN [userschool] WITH PASSWORD=N'OZf+RlkRQAfcnunAIGxdxDnwJ1KVZ2nlYPkLz67Z+IM=', DEFAULT_DATABASE=[SchoolDB], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

ALTER LOGIN [userschool] DISABLE
GO

ALTER SERVER ROLE [sysadmin] ADD MEMBER [userschool]
GO

