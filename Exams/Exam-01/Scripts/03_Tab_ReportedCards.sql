USE [NetBankDB]
GO

/****** Object:  Table [dbo].[ReportedCards]    Script Date: 3/10/2022 6:51:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ReportedCards](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IssuingNetwork] [varchar](50) NOT NULL,
	[CreditCardNumber] [varchar](50) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[StatusCard] [varchar](10) NOT NULL,
	[ReportedDate] [date] NOT NULL,
	[LastUpdatedDate] [date] NOT NULL,
 CONSTRAINT [PK_ReportedCards] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ReportedCards] ADD  CONSTRAINT [DF_ReportedCards_StatusCard]  DEFAULT ('Stolen') FOR [StatusCard]
GO

ALTER TABLE [dbo].[ReportedCards] ADD  CONSTRAINT [DF_ReportedCards_ReportedDate]  DEFAULT (getdate()) FOR [ReportedDate]
GO

ALTER TABLE [dbo].[ReportedCards] ADD  CONSTRAINT [DF_ReportedCards_LastUpdatedDate]  DEFAULT (getdate()) FOR [LastUpdatedDate]
GO

