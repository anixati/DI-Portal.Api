USE [DI_Boards]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------
DROP FUNCTION IF EXISTS [dbo].[GetCodeValue]
GO
CREATE FUNCTION [dbo].[GetCodeValue] (@id bigint,@code varchar(100))
RETURNS VARCHAR(255)
WITH EXECUTE AS CALLER
AS
BEGIN
    DECLARE @RV VARCHAR(255);
	SELECT TOP 1 @RV=os.Value FROM [dbo].[OptionSet] os
		JOIN [dbo].[OptionKeys] ok ON ok.Id= os.OptionKeyId
		WHERE os.Id = @id AND ok.Code =@code
    RETURN(@RV);
END;
GO


---------------------------
DROP VIEW IF EXISTS [dbo].[ActiveBoardsView]
GO
CREATE VIEW  [dbo].[ActiveBoardsView] AS 
       SELECT 
			bds.Id,
			bds.Acronym,
			bds.[Name],
			pf.Id AS PortfolioId,
			pf.[Name] As Portfolio,
			(SELECT os.[Label] FROM OptionSet os where os.ID=[bds].[OwnerDivisionId]) AS OwnerDivision,
			(SELECT os.[Label] FROM OptionSet os where os.ID=[bds].[OwnerPositionId]) AS OwnerPosition,
			rou.Id AS RespOfficerId,
			(COALESCE(rou.Title+' ','')+rou.FirstName+' '+rou.LastName) As RespOfficer,
			apu.Id AS ApprovedUserId,
			(COALESCE(apu.Title+' ','')+apu.FirstName+' '+apu.LastName) As ApprovedUser,         
			ats.Id AS AsstSecretaryId,
			(COALESCE(ats.Title+' ','')+ats.FirstName+' '+ats.LastName) As AsstSecretary,  
			0 AS CurrentRoles,
			bds.[Disabled]
			FROM [dbo].[Boards] bds
			LEFT JOIN [dbo].[Portfolios] pf on pf.Id = bds.PortfolioId
			LEFT OUTER JOIN [dbo].[Secretaries] ats on ats.Id = bds.AsstSecretaryId
			LEFT OUTER JOIN [acl].[Users] rou On rou.Id = bds.ResponsibleUserId
			LEFT OUTER JOIN [acl].[Users] apu On apu.Id = bds.ApprovedUserId


GO

---------------------------
DROP VIEW IF EXISTS [dbo].[BoardRolesView]
GO
CREATE VIEW  [dbo].[BoardRolesView] AS 
	  SELECT 
	  brl.Id,
	  brl.BoardId,
	  brl.[IncumbentId],
	  (apt.FirstName+' '+apt.LastName) As Incumbent,
	  brl.[Name],
	  pos.[Label] AS Position,
	  brl.[PositionId],
	  brl.[AppointerId],
	 (SELECT os.[Label] FROM OptionSet os where os.ID= brl.[AppointerId]) AS  Appointer,
	  brl.[IsFullTime],
	  brl.[IsExecutive],
	  brl.[IsExOfficio],
	  brl.[IsApsEmployee],
	  brl.[IsExNominated],
	  brl.[Term],
	  brl.[PositionRemunerated],
	  brl.[PaAmount],
	  brl.[RemunerationMethodId],
	  (SELECT os.[Label] FROM OptionSet os where os.ID= brl.[RemunerationMethodId]) AS  RemunerationMethod,
	  brl.[VacantFromDate],
	  brl.[ExcludeFromOrder15],
	  brl.[ExcludeGenderReport],
	  brl.[IsSignAppointment],
	  brl.[PDMSNumber], 
	  brl.[MinSubLocationId],
	  brl.[MinisterOfficeDate],
	  brl.[MinisterActionDate],
	  brl.[LetterToPmDateType],
	  brl.[LetterToPmDate],
	  brl.[ExCoDateType], 
	  brl.[ExCoDate],
	  brl.[NotifyLetterDateType],
	  brl.[NotifyLetterDate], 
	  brl.[CabinetDateType], 
	  brl.[CabinetDate],
	  brl.[Locked],
	  brl.[Disabled]
	  FROM [dbo].[BoardRoles] brl
	  JOIN [dbo].[Boards] bds on brl.Id = bds.Id 
	  JOIN OptionSet pos On pos.Id = brl.PositionId
	  LEFT OUTER JOIN [dbo].[Appointee] apt on apt.Id= brl.IncumbentId

Go

	  
---------------------------
DROP VIEW IF EXISTS [dbo].[BoardAppointmentsView]
GO

CREATE VIEW  [dbo].[BoardAppointmentsView] AS 
	  SELECT 
	  bap.Id,
	  bap.BoardRoleId,
	  brl.BoardId,
	  bap.AppointeeId,
	  brl.Name AS RoleName,
	  bap.Name AS AppointeeName,
	  bap.BriefNumber,
	  bap.StartDate,
	  bap.EndDate,
	  bap.[Locked],
	  bap.[Disabled]
	  FROM BoardAppointments bap
	  JOIN BoardRoles brl ON brl.Id=bap.BoardRoleId
	  JOIN [dbo].[Appointee] apt on apt.Id= bap.AppointeeId