USE [DI_Boards2]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------
-- FUNCTIONS
---------------------------
DROP FUNCTION IF EXISTS [dbo].[GetCodeValue]
GO
CREATE FUNCTION [dbo].[GetCodeValue] (@id bigint,@code varchar(100))
RETURNS VARCHAR(255)
WITH EXECUTE AS CALLER
AS
BEGIN
    DECLARE @RV VARCHAR(255);
	SELECT TOP 1 @RV=os.[Value] FROM [dbo].[OptionSet] os
		JOIN [dbo].[OptionKeys] ok ON ok.Id= os.OptionKeyId
		WHERE os.Id = @id AND ok.Code =@code
    RETURN(@RV);
END;
GO

DROP FUNCTION IF EXISTS [dbo].[GetCodeLabel]
GO
CREATE FUNCTION [dbo].[GetCodeLabel] (@id bigint,@code varchar(100))
RETURNS VARCHAR(255)
WITH EXECUTE AS CALLER
AS
BEGIN
    DECLARE @RV VARCHAR(255);
	SELECT TOP 1 @RV=os.[Label] FROM [dbo].[OptionSet] os
		JOIN [dbo].[OptionKeys] ok ON ok.Id= os.OptionKeyId
		WHERE os.Id = @id AND ok.Code =@code
    RETURN(@RV);
END;
GO


---------------------------
-- PROCEDURES
---------------------------
--DROP FUNCTION IF EXISTS [dbo].[GetCodeValue]
--GO
--CREATE FUNCTION [dbo].[GetCodeValue] (@id bigint,@code varchar(100))
--RETURNS VARCHAR(255)
--WITH EXECUTE AS CALLER
--AS
--BEGIN
--    DECLARE @RV VARCHAR(255);
--	SELECT TOP 1 @RV=os.Value FROM [dbo].[OptionSet] os
--		JOIN [dbo].[OptionKeys] ok ON ok.Id= os.OptionKeyId
--		WHERE os.Id = @id AND ok.Code =@code
--    RETURN(@RV);
--END;
--GO


---------------------------
-- VIEWS
---------------------------
DROP VIEW IF EXISTS [dbo].[AppointeesView]
GO
CREATE VIEW [dbo].[AppointeesView] AS 
                    SELECT 
                    ap.Id,
                    (COALESCE(ap.Title+' ','')+ap.FirstName+' '+COALESCE(ap.MiddleName+' ','')+ap.LastName) As FullName,
                    (case ap.Gender when 1 then 'Male' when 2 then 'Female' else 'NA' end) As Gender,
                    ap.HomePhone AS Phone,
                    ap.MobilePhone AS Mobile,
                    ap.FaxNumber As Fax,
                    ap.Email1 as Email,
                    ap.StreetAddress_City AS City,
                    ap.StreetAddress_State AS State,
                    (case ap.IsAboriginal when 1 then 'Yes' when 2 then 'No' else '' end) AS Aboriginal,
                    (case ap.IsDisabled when 1 then 'Yes' when 2 then 'No' else '' end) AS Handicapped,
                    (case ap.IsRegional when 1 then 'Yes' when 2 then 'No' else '' end)   AS Regional,
                    (case ap.ExecutiveSearch when 1 then 'Yes' when 2 then 'No' else '' end)  AS Executive,
                    ap.[Disabled],
                    (SELECT os.Label FROM OptionSet os JOIN OptionKeys ok on os.OptionKeyId= ok.Id WHERE ok.Code='new_positiontype' AND os.Value= ap.CapabilitiesId AND os.Deleted=0) AS Capability,
                    (SELECT os.Label FROM OptionSet os JOIN OptionKeys ok on os.OptionKeyId= ok.Id WHERE ok.Code='new_positiontype' AND os.Value= ap.ExperienceId AND os.Deleted=0) AS Experience
                    FROM [dbo].[Appointee] ap
                    WHERE ap.Deleted=0 

GO

---------------------------
DROP VIEW IF EXISTS [dbo].[SecretariesView]
GO
CREATE VIEW [dbo].[SecretariesView] AS 
                    SELECT 
                    mns.Id,
                    (COALESCE(mns.Title+' ','')+mns.FirstName+' '+COALESCE(mns.MiddleName+' ','')+mns.LastName) As FullName,
                    (case mns.Gender when 1 then 'Male' when 2 then 'Female' else 'NA' end) As Gender,
                    mns.HomePhone AS Phone,
                    mns.MobilePhone AS Mobile,
                    mns.FaxNumber As Fax,
                    mns.Email1 as Email,
                    mns.StreetAddress_City AS City,
                    mns.StreetAddress_State AS State,
                    mns.[Disabled]
                    FROM [dbo].[Secretaries] mns
                    WHERE mns.Deleted=0
GO

---------------------------
DROP VIEW IF EXISTS [dbo].MinistersView
GO
CREATE VIEW MinistersView AS 
                    SELECT 
                    mns.Id,
                    (COALESCE(mns.Title+' ','')+mns.FirstName+' '+COALESCE(mns.MiddleName+' ','')+mns.LastName) As FullName,
                    (case mns.Gender when 1 then 'Male' when 2 then 'Female' else 'NA' end) As Gender,
                    mns.HomePhone AS Phone,
                    mns.MobilePhone AS Mobile,
                    mns.FaxNumber As Fax,
                    mns.Email1 as Email,
                    mns.StreetAddress_City AS City,
                    mns.StreetAddress_State AS State,
                    mns.Disabled
                    FROM [dbo].[Ministers] mns
                    WHERE mns.Deleted=0
GO
-------------------------
DROP VIEW IF EXISTS [dbo].[PortfoliosView]
GO
CREATE VIEW [dbo].[PortfoliosView] AS 
                    SELECT
                    Pf.Id,
                    pf.[Name] AS PortfolioName,
                    pf.[Description] AS [Description],
                    pf.CreatedOn,
                    (COALESCE(mn.Title+' ','')+mn.FirstName+' '+COALESCE(mn.MiddleName+' ','')+mn.LastName) As Minister,
                    mt.StartDate,
                    mt.EndDate,
                    pf.[Disabled]
                    FROM [dbo].[Portfolios] pf
                    LEFT OUTER JOIN [dbo].[MinisterTerms] mt ON mt.PortfolioId = pf.Id AND mt.Deleted =0
                    LEFT OUTER JOIN [dbo].[Ministers] mn ON mn.Id = mt.MinisterId AND mn.Deleted =0
                    WHERE pf.Deleted =0
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
			bds.AppTeamId,
			bds.[Disabled]
			FROM [dbo].[Boards] bds
			LEFT JOIN [dbo].[Portfolios] pf on pf.Id = bds.PortfolioId AND pf.Deleted =0
			LEFT OUTER JOIN [dbo].[Secretaries] ats on ats.Id = bds.AsstSecretaryId AND ats.Deleted =0
			LEFT OUTER JOIN [acl].[Users] rou On rou.Id = bds.ResponsibleUserId AND rou.Deleted =0
			LEFT OUTER JOIN [acl].[Users] apu On apu.Id = bds.ApprovedUserId AND apu.Deleted =0
			WHERE bds.Deleted=0

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
	  JOIN [dbo].[Boards] bds on brl.BoardId = bds.Id 
	  JOIN OptionSet pos On pos.Id = brl.PositionId
	  LEFT OUTER JOIN [dbo].[Appointee] apt on apt.Id= brl.IncumbentId  AND apt.Deleted=0
	  WHERE brl.Deleted=0 AND bds.Deleted=0 AND pos.Deleted=0
Go

	  
---------------------------
DROP VIEW IF EXISTS [dbo].[AppointmentsView]
GO

CREATE VIEW  [dbo].[AppointmentsView] AS 
	  SELECT 
	  bap.Id,
	  bap.BoardRoleId,
	  brl.BoardId,
	  bap.AppointeeId,
	  bap.Name AS AppointmentName,
	  brl.Name AS RoleName,
	  bap.Name AS AppointeeName,
	  brd.Name AS BoardName,
	  bap.BriefNumber,
	  bap.StartDate,
	  bap.EndDate,
	  bap.[Locked],
	  bap.[Disabled]
	  FROM BoardAppointments bap
	  JOIN BoardRoles brl ON brl.Id=bap.BoardRoleId 
	  JOIN Boards brd ON brd.Id=brl.BoardId
	  JOIN [dbo].[Appointee] apt on apt.Id= bap.AppointeeId 
	  WHERE bap.Deleted=0 AND brl.Deleted=0 AND brd.Deleted=0 AND apt.Deleted=0

GO
---------------------------
DROP VIEW IF EXISTS [dbo].[MinisterTermsView]
GO

CREATE VIEW  [dbo].[MinisterTermsView] AS 
	 SELECT 
	  mts.Id,
	  mns.Id AS MinisterId,
	  (COALESCE(mns.Title+' ','')+mns.FirstName+' '+mns.LastName) As Minister,
	  pfs.Id AS PortfolioId,
	  pfs.Name AS Portfolio,
	  mts.StartDate,
	  mts.EndDate,
	  mts.Disabled
	  FROM MinisterTerms mts
	  JOIN Ministers mns ON mns.Id = mts.MinisterId
	  JOIN Portfolios pfs ON pfs.Id= mts.PortfolioId
	  WHERE mts.Deleted = 0 AND mns.Deleted=0 AND pfs.Deleted =0
GO	  
	  
-------------------------
-- Security 
-------------------------
DROP VIEW IF EXISTS [dbo].[ActiveUsersView]
GO
CREATE VIEW  [dbo].[ActiveUsersView] AS 
       SELECT 
			usr.Id,
			usr.[UserId],
			(COALESCE(usr.Title+' ','')+usr.FirstName+' '+COALESCE(usr.MiddleName+' ','')+usr.LastName) As FullName,
			usr.HomePhone AS Phone,
            usr.Email1 as Email,
			usr.CreatedOn,
			usr.[Disabled]
			FROM [acl].[Users] usr
			WHERE usr.Deleted=0

GO
--------------------------
DROP VIEW IF EXISTS [dbo].[SkillsView]
GO
CREATE VIEW  [dbo].[SkillsView] AS 
	SELECT skl.Id,
		skl.[Name],
		(SELECT os.[Label] FROM OptionSet os where os.ID= skl.[SkillTypeId]) AS SkillType,
		skl.[Description],
		skl.[CreatedOn],
		skl.[ModifiedOn],
		skl.[Disabled]
	FROM [dbo].[Skill] skl
	WHERE skl.Deleted=0 

GO
---------------------------
DROP VIEW IF EXISTS [dbo].[AppointeeSkillsView]
GO
CREATE VIEW  [dbo].[AppointeeSkillsView] AS 
   SELECT 
   aks.[Id],
   aks.[AppointeeId],
   aks.[SkillId],
   skl.[Name] AS Skill,
   (SELECT os.[Label] FROM OptionSet os where os.ID= skl.[SkillTypeId]) AS SkillType,
   (COALESCE(apt.Title+' ','')+apt.FirstName+' '+apt.LastName) As FullName,
   (case apt.Gender when 1 then 'Male' when 2 then 'Female' else 'NA' end) As Gender,
   apt.HomePhone AS Phone,
   apt.Email1 as Email,
   apt.StreetAddress_City AS City,
   aks.Disabled
   FROM [dbo].[AppointeeSkill] aks
   JOIN  [dbo].[Skill] skl on aks.SkillId= skl.Id AND skl.Deleted=0
   JOIN [dbo].[Appointee] apt on apt.Id= aks.AppointeeId AND apt.Deleted=0
   WHERE aks.Deleted=0 and aks.Disabled=0


GO


--------------------------------------------------------
--- PROCEDURES
--------------------------------------------------------
DROP PROCEDURE IF EXISTS [acl].[GetUserRoles]
GO
CREATE PROCEDURE[acl].[GetUserRoles]
    @userId nvarchar(255)
AS   
    SET NOCOUNT ON;  
   	SELECT rls.Code
	FROM [acl].[UserRoles] urs
	JOIN [acl].[Users] usr ON usr.Id = urs.AppUserId AND usr.Deleted=0 AND usr.Disabled=0
	JOIN [acl].[Roles] rls ON rls.Id = urs.AppRoleId AND rls.Deleted=0 AND rls.Disabled=0
	WHERE urs.Deleted=0 AND urs.Disabled=0 AND usr.UserId= @userId
	UNION
	SELECT rls.Code
	FROM [acl].[TeamUsers] tur
	JOIN [acl].[Users] usr ON usr.Id = tur.AppUserId AND usr.Deleted=0 AND usr.Disabled=0
	JOIN [acl].[TeamRoles] trs ON trs.AppTeamId=tur.AppTeamId AND trs.Deleted=0 AND trs.Disabled=0
	JOIN [acl].[Roles] rls ON rls.Id = trs.AppRoleId AND rls.Deleted=0 AND rls.Disabled=0
	WHERE tur.Deleted=0 AND tur.Disabled=0 AND usr.UserId= @userId

GO  