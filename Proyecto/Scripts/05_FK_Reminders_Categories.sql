USE [ReminderDB]
GO

ALTER TABLE [dbo].[Reminders]  WITH CHECK ADD  CONSTRAINT [FK_Reminders_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO

ALTER TABLE [dbo].[Reminders] CHECK CONSTRAINT [FK_Reminders_Categories]
GO

