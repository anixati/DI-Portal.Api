USE [DI_Boards2]
GO



ALTER TABLE [dbo].[Boards]
	ADD MaxServicePeriod int NULL;
	Go


ALTER TABLE [dbo].[BoardRoles]
	ADD LeadTimeToAppoint int NULL;
	Go

ALTER TABLE [dbo].[BoardRoles]
	ADD ProcessStatus NVARCHAR(2000) NULL;
	Go

ALTER TABLE [dbo].[BoardRoles] ADD MinSubDateType int NULL;
Go
UPDATE [dbo].[BoardRoles] SET MinSubDateType=0 WHERE MinSubDateType IS NULL
GO
ALTER TABLE [dbo].[BoardRoles] ALTER COLUMN MinSubDateType int NOT NULL;
Go
ALTER TABLE [dbo].[BoardRoles] ADD MinSubDate Datetime2(7) NULL;
Go


	
ALTER TABLE [dbo].[BoardAppointments]
	ADD IsCurrent bit NULL;
	GO

ALTER TABLE [dbo].[BoardAppointments]
	ADD Proposed bit NULL;
	GO

ALTER TABLE [dbo].[BoardAppointments]
	ADD IsSemiDiscretionary bit NULL;
	GO


ALTER TABLE [dbo].[BoardAppointments]
	ADD AppointerId bigint NULL;
	GO

ALTER TABLE [dbo].[BoardAppointments]  WITH CHECK ADD  CONSTRAINT [FK_BoardAppointments_OptionSet_AppointerId] FOREIGN KEY([AppointerId])
  REFERENCES [dbo].[OptionSet] ([Id])
GO
ALTER TABLE [dbo].[BoardAppointments] CHECK CONSTRAINT [FK_BoardAppointments_OptionSet_AppointerId]
GO