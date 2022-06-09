USE [ReminderDB]
GO

/****** Object:  Table [dbo].[Reminders]    Script Date: 3/25/2022 7:52:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Reminders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Description] [varchar](300) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[CronExpression] [varchar](50) NOT NULL,
	[NumberOfTimes] [int] NULL,
	[Enabled] [bit] NULL,
 CONSTRAINT [PK_Reminders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Reminders] ADD  CONSTRAINT [DF_Reminders_NumberOfTimes]  DEFAULT ((0)) FOR [NumberOfTimes]
GO

ALTER TABLE [dbo].[Reminders] ADD  CONSTRAINT [DF_Reminders_Enabled]  DEFAULT ((1)) FOR [Enabled]
GO

