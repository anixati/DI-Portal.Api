USE [DI_Boards5]
GO
/****** Object:  StoredProcedure [acl].[GetUserRoles]    Script Date: 28/07/2022 4:01:29 PM ******/
DROP PROCEDURE [acl].[GetUserRoles]
GO
/****** Object:  View [dbo].[VwUsers]    Script Date: 28/07/2022 4:01:29 PM ******/
DROP VIEW [dbo].[VwUsers]
GO
/****** Object:  View [dbo].[VwBoards]    Script Date: 28/07/2022 4:01:29 PM ******/
DROP VIEW [dbo].[VwBoards]
GO
/****** Object:  View [dbo].[VwBoardRoles]    Script Date: 28/07/2022 4:01:29 PM ******/
DROP VIEW [dbo].[VwBoardRoles]
GO
/****** Object:  View [dbo].[VwBoardAppointments]    Script Date: 28/07/2022 4:01:29 PM ******/
DROP VIEW [dbo].[VwBoardAppointments]
GO
/****** Object:  View [dbo].[VwAppointee]    Script Date: 28/07/2022 4:01:29 PM ******/
DROP VIEW [dbo].[VwAppointee]
GO
/****** Object:  View [dbo].[SkillsView]    Script Date: 28/07/2022 4:01:29 PM ******/
DROP VIEW [dbo].[SkillsView]
GO
/****** Object:  View [dbo].[SecretariesView]    Script Date: 28/07/2022 4:01:29 PM ******/
DROP VIEW [dbo].[SecretariesView]
GO
/****** Object:  View [dbo].[PortfoliosView]    Script Date: 28/07/2022 4:01:29 PM ******/
DROP VIEW [dbo].[PortfoliosView]
GO
/****** Object:  View [dbo].[MinisterTermsView]    Script Date: 28/07/2022 4:01:29 PM ******/
DROP VIEW [dbo].[MinisterTermsView]
GO
/****** Object:  View [dbo].[MinistersView]    Script Date: 28/07/2022 4:01:29 PM ******/
DROP VIEW [dbo].[MinistersView]
GO
/****** Object:  View [dbo].[BoardRolesView]    Script Date: 28/07/2022 4:01:29 PM ******/
DROP VIEW [dbo].[BoardRolesView]
GO
/****** Object:  View [dbo].[AppointmentsView]    Script Date: 28/07/2022 4:01:29 PM ******/
DROP VIEW [dbo].[AppointmentsView]
GO
/****** Object:  View [dbo].[AppointeesView]    Script Date: 28/07/2022 4:01:29 PM ******/
DROP VIEW [dbo].[AppointeesView]
GO
/****** Object:  View [dbo].[AppointeeSkillsView]    Script Date: 28/07/2022 4:01:29 PM ******/
DROP VIEW [dbo].[AppointeeSkillsView]
GO
/****** Object:  View [dbo].[ActiveUsersView]    Script Date: 28/07/2022 4:01:29 PM ******/
DROP VIEW [dbo].[ActiveUsersView]
GO
/****** Object:  View [dbo].[ActiveBoardsView]    Script Date: 28/07/2022 4:01:29 PM ******/
DROP VIEW [dbo].[ActiveBoardsView]
GO

/****** Object:  View [dbo].[vwBoardCounts]    Script Date: 28/07/2022 4:01:29 PM ******/
DROP VIEW [dbo].[vwBoardCounts]
GO


DROP VIEW [dbo].[VwSecretaries]
GO

/****** Object:  UserDefinedFunction [dbo].[GetCodeValue]    Script Date: 28/07/2022 4:01:29 PM ******/
DROP FUNCTION [dbo].[GetCodeValue]
GO
/****** Object:  UserDefinedFunction [dbo].[GetCodeLabel]    Script Date: 28/07/2022 4:01:29 PM ******/
DROP FUNCTION [dbo].[GetCodeLabel]
GO





--------------------------------------------------------
--- Functions
--------------------------------------------------------

/****** Object:  UserDefinedFunction [dbo].[GetCodeLabel]    Script Date: 28/07/2022 4:01:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  UserDefinedFunction [dbo].[GetCodeValue]    Script Date: 28/07/2022 4:01:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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



--------------------------------------------------------
--- PROCEDURES
--------------------------------------------------------

/****** Object:  StoredProcedure [acl].[GetUserRoles]    Script Date: 28/07/2022 4:01:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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





--------------------------------------------------------
--- Triggers
--------------------------------------------------------
--DROP TRIGGER IF EXISTS [dbo].[TrgSetRoleIncumbent]
--GO

--CREATE TRIGGER [dbo].[TrgSetRoleIncumbent] ON [dbo].[BoardAppointments]
--FOR UPDATE 
--AS 
--BEGIN
--set nocount on;
--DECLARE @AppId BIGINT
--SELECT @AppId=ins.ID FROM INSERTED ins;
--IF (UPDATE ([IsCurrent]))  
--BEGIN  

-- UPDATE br
-- SET br.IncumbentId= ap.Id
-- FROM [dbo].[BoardRoles] AS br
-- JOIN [dbo].[BoardAppointments] ba ON br.Id=ba.BoardRoleId
-- JOIN [dbo].[Appointee] ap ON ap.ID=ba.AppointeeId
-- WHERE ba.Id=@AppId AND ba.IsCurrent=1
--END;  


--END
--GO



--------------------------------------------------------
--- VIEWS
--------------------------------------------------------

/****** Object:  View [dbo].[ActiveBoardsView]    Script Date: 28/07/2022 4:01:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
			ORDER BY Name OFFSET 0 ROWS;


GO
/****** Object:  View [dbo].[ActiveUsersView]    Script Date: 28/07/2022 4:01:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  View [dbo].[AppointeeSkillsView]    Script Date: 28/07/2022 4:01:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  View [dbo].[AppointeesView]    Script Date: 28/07/2022 4:01:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  View [dbo].[AppointmentsView]    Script Date: 28/07/2022 4:01:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  View [dbo].[BoardRolesView]    Script Date: 28/07/2022 4:01:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW  [dbo].[BoardRolesView] AS 
	  SELECT 
	  brl.Id,
	  brl.BoardId,
	  apt.Id as [IncumbentId],
	  (apt.FirstName+' '+apt.LastName) As Incumbent,
	  brl.[Name],
	  pos.[Label] AS Position,
	  brl.[PositionId],
	  brl.[RoleAppointerId] AS [AppointerId],
	 (SELECT os.[Label] FROM OptionSet os where os.ID= brl.RoleAppointerId) AS  Appointer,
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
	  LEFT JOIN dbo.BoardAppointments ba on brl.Id = ba.BoardRoleId and ba.StartDate <= GETDATE() and ba.EndDate > GETDATE() and ba.Deleted=0
	  LEFT OUTER JOIN [dbo].[Appointee] apt on ba.AppointeeId = apt.Id AND apt.Deleted=0
	  WHERE brl.Deleted=0 AND bds.Deleted=0 AND pos.Deleted=0


GO
/****** Object:  View [dbo].[MinistersView]    Script Date: 28/07/2022 4:01:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MinistersView] AS 
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
/****** Object:  View [dbo].[MinisterTermsView]    Script Date: 28/07/2022 4:01:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  View [dbo].[PortfoliosView]    Script Date: 28/07/2022 4:01:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PortfoliosView] AS 
                    SELECT
                    Pf.Id,
                    pf.[Name] AS PortfolioName,
                    pf.[Description] AS [Description],
                    pf.CreatedOn,
                    pf.[Disabled]
                    FROM [dbo].[Portfolios] pf
                    WHERE pf.Deleted =0
GO
/****** Object:  View [dbo].[SecretariesView]    Script Date: 28/07/2022 4:01:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  View [dbo].[SkillsView]    Script Date: 28/07/2022 4:01:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  View [dbo].[VwAppointee]    Script Date: 28/07/2022 4:01:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW  [dbo].[VwAppointee] AS 
	SELECT 
	(COALESCE(app.Title+' ','')+app.FirstName+' '+COALESCE(app.MiddleName+' ','')+app.LastName) As FullName,
	(CASE WHEN app.Gender=1 THEN 'Male' WHEN app.Gender=2 THEN 'Female' ELSE '' END)AS GenderName,
	app.*
	FROM [dbo].[Appointee] app WHERE app.Deleted=0 AND app.[Disabled]=0
GO
/****** Object:  View [dbo].[VwBoardAppointments]    Script Date: 28/07/2022 4:01:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW  [dbo].[VwBoardAppointments] AS 
	SELECT ba.* 
	,(SELECT os.[Label] FROM OptionSet os where os.ID=ba.AppointerId) AS AppointerName
	FROM [dbo].[BoardAppointments] ba WHERE ba.Deleted=0 AND ba.[Disabled]=0
GO
/****** Object:  View [dbo].[VwBoardRoles]    Script Date: 28/07/2022 4:01:29 PM ******/
SET ANSI_NULLS ON
GO



SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW  [dbo].[VwBoardRoles] AS 
	SELECT brs.*
	 ,(SELECT os.[Label] FROM OptionSet os where os.ID=brs.[RoleAppointerId]) AS AppointerName
	FROM [dbo].[BoardRoles] brs WHERE brs.Deleted=0 AND brs.[Disabled]=0
GO
/****** Object:  View [dbo].[VwBoards]    Script Date: 28/07/2022 4:01:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW  [dbo].[VwBoards] AS 
	SELECT * FROM  [dbo].[Boards] bds WHERE bds.Deleted=0 AND bds.[Disabled]=0
GO


/****** Object:  View [dbo].[VwUsers]    Script Date: 28/07/2022 4:01:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW  [dbo].[VwUsers] AS 
	SELECT 	(COALESCE(usr.Title+' ','')+usr.FirstName+' '+COALESCE(usr.MiddleName+' ','')+usr.LastName) As FullName,usr.*
	FROM [acl].[Users] usr WHERE usr.Deleted=0 
GO



/****** Object:  View [dbo].[VwSecretaries]    Script Date: 28/07/2022 4:01:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW  [dbo].[VwSecretaries] AS 
	SELECT 	(COALESCE(usr.Title+' ','')+usr.FirstName+' '+COALESCE(usr.MiddleName+' ','')+usr.LastName) As FullName,usr.*
	FROM [dbo].[Secretaries] usr WHERE usr.Deleted=0 
GO


/****** Object:  View [dbo].[vwBoardCounts]    Script Date: 28/07/2022 4:01:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW  [dbo].[vwBoardCounts] AS 
	SELECT 
	bd.Id as [BoardID]
	,bd.[Name] as [Board]
	,SUM(case when ap.Gender=2  and br.ExcludeGenderReport=0 THEN 1 ELSE 0 END) as Female
	,sum(case when ap.Gender=1  and br.ExcludeGenderReport=0 THEN 1 ELSE 0 END) as Male
	,sum(case when ap.StreetAddress_State = 'NSW' THEN 1 ELSE 0 END) as NSW
	,sum(case when ap.StreetAddress_State = 'WA' THEN 1 ELSE 0 END) as WA
	,sum(case when ap.StreetAddress_State = 'VIC' THEN 1 ELSE 0 END) as VIC
	,sum(case when ap.StreetAddress_State = 'QLD' THEN 1 ELSE 0 END) as QLD
	,sum(case when ap.StreetAddress_State = 'SA' THEN 1 ELSE 0 END) as SA
	,sum(case when ap.StreetAddress_State = 'TAS' THEN 1 ELSE 0 END) as TAS
	,sum(case when ap.StreetAddress_State = 'NT' THEN 1 ELSE 0 END) as NT
	,sum(case when ap.StreetAddress_State = 'ACT' THEN 1 ELSE 0 END) as ACT
	FROM [dbo].[VwBoards] bd
	LEFT JOIN [dbo].[VwBoardRoles] br ON br.BoardId= bd.Id
	LEFT JOIN [dbo].[VwBoardAppointments] ba ON ba.BoardRoleId = br.Id
	LEFT JOIN [dbo].[VwAppointee] ap ON ap.Id= ba.AppointeeId
	GROUP BY bd.Id,bd.[Name]

GO

