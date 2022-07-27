USE [DI_Boards2]
GO


ALTER TABLE [dbo].[BoardRoles]
	ADD LeadTimeToAppoint int NULL;
	Go

	
ALTER TABLE [dbo].[BoardAppointments]
	ADD IsCurrent bit NULL;
	GO