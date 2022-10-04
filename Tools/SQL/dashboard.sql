USE [DI_Boards27Sep]
GO

/****** Object:  StoredProcedure [dbo].[GetDashBoardData1]    Script Date: 4/10/2022 3:54:43 PM ******/
DROP PROCEDURE [dbo].[GetDashBoardData1]
GO

/****** Object:  StoredProcedure [dbo].[GetDashBoardData1]    Script Date: 4/10/2022 3:54:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetDashBoardData1]
AS   
    SET NOCOUNT ON;  
   	
	SELECT 
	'Boards' AS Title,
	'Total Boards' AS [Description],
	Cast(Count(*) as varchar(10)) AS [Value],
	'with '+(SELECT Cast(Count(*) as varchar(10)) FROM [dbo].[VwBoardRoles] )+' active roles' AS Result,
	'' AS ResultColor,
	'' AS Icon
	From [dbo].[VwBoards]
	UNION ----//
	SELECT 
	'Female' AS Title,
	'Total female appointees' AS [Description],
	Cast(Count(*) as varchar(10)) AS [Value],
	'Out of '+(SELECT Cast(Count(*) as varchar(10)) FROM [dbo].[VwAppointee] ) AS Result,
	'' AS ResultColor,
	'' AS Icon
	From [dbo].[VwAppointee] rx WHERE rx.Gender=2
		UNION ----//
	SELECT 
	'Male' AS Title,
	'Total male appointees' AS [Description],
	Cast(Count(*) as varchar(10)) AS [Value],
	'Out of '+(SELECT Cast(Count(*) as varchar(10)) FROM [dbo].[VwAppointee] ) AS Result,
	'' AS ResultColor,
	'' AS Icon
	From [dbo].[VwAppointee] rx WHERE rx.Gender=1
GO


