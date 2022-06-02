USE [DI_Boards]
GO

DROP VIEW [dbo].[AppointeesView]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW  [dbo].[ActiveBoardsView] AS 
       SELECT 
			bds.Id,
			bds.Acronym,
			bds.[Name],
			pf.[Name] As Portfolio,
			bds.OwnerDivision,
			bds.OwnerPosition,
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
