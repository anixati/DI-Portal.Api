USE [DI_Boards2]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP VIEW IF EXISTS [dbo].[VwBoards]
GO
CREATE VIEW  [dbo].[VwBoards] AS 
	SELECT * FROM  [dbo].[Boards] bds WHERE bds.Deleted=0 AND bds.[Disabled]=0
GO

DROP VIEW IF EXISTS [dbo].[VwBoardRoles]
GO
CREATE VIEW  [dbo].[VwBoardRoles] AS 
	SELECT * FROM [dbo].[BoardRoles] brs WHERE brs.Deleted=0 AND brs.[Disabled]=0
GO

DROP VIEW IF EXISTS [dbo].[VwBoardAppointments]
GO
CREATE VIEW  [dbo].[VwBoardAppointments] AS 
	SELECT * FROM [dbo].[BoardAppointments] ba WHERE ba.Deleted=0 AND ba.[Disabled]=0
GO

DROP VIEW IF EXISTS [dbo].[VwAppointee]
GO
CREATE VIEW  [dbo].[VwAppointee] AS 
	SELECT 
	(COALESCE(app.Title+' ','')+app.FirstName+' '+COALESCE(app.MiddleName+' ','')+app.LastName) As FullName,app.*
	FROM [dbo].[Appointee] app WHERE app.Deleted=0 AND app.[Disabled]=0
GO

DROP VIEW IF EXISTS [dbo].[VwUsers]
GO
CREATE VIEW  [dbo].[VwUsers] AS 
	SELECT 	(COALESCE(usr.Title+' ','')+usr.FirstName+' '+COALESCE(usr.MiddleName+' ','')+usr.LastName) As FullName,usr.*
	FROM [acl].[Users] usr WHERE usr.Deleted=0 AND usr.[Disabled]=0
GO
