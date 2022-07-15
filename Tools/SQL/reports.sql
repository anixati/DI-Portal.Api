USE [DI_Boards]
GO

DROP PROCEDURE [dbo].[RptBoardsOverView]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE[dbo].[RptBoardsOverView]
AS   
   SET NOCOUNT ON;  
   SELECT 
	   'Active portfolio Appointees' AS Title, 
	   COUNT(ba.id) AS Val,
	   '' AS Notes,
	   '' AS Icon
    FROM [dbo].[BoardAppointments] ba
    JOIN [dbo].[BoardRoles] br ON br.Id= ba.BoardRoleId
    JOIN [dbo].[Boards] bd ON bd.Id =ba.BoardId
	JOIN [dbo].[Appointee] ap On ap.Id = ba.AppointeeId
   WHERE ba.Deleted=0 AND ba.Disabled= 0
     AND br.Deleted=0 AND br.Disabled= 0 AND bd.Deleted=0 AND bd.Disabled= 0
	 AND ap.Deleted =0 AND ap.Disabled=0
	 AND ba.EndDate > GETDATE()
UNION
   SELECT 
	   'Total Current Vacancies' AS Title, 
	   COUNT(br.id) AS Val,
	   '' AS Notes,
	   '' AS Icon
    FROM [dbo].[BoardRoles] br
    JOIN [dbo].[Boards] bd ON bd.Id =br.BoardId
   WHERE br.Deleted=0 AND br.Disabled= 0 AND bd.Deleted=0 AND bd.Disabled= 0
     AND br.IncumbentId is null
UNION	 
	 SELECT 
	   'Positions ending within 6 months' AS Title, 
	   COUNT(ba.id) AS Val,
	   '' AS Notes,
	   '' AS Icon
    FROM [dbo].[BoardAppointments] ba
    JOIN [dbo].[BoardRoles] br ON br.Id= ba.BoardRoleId
    JOIN [dbo].[Boards] bd ON bd.Id =ba.BoardId
	JOIN [dbo].[Appointee] ap On ap.Id = ba.AppointeeId
   WHERE ba.Deleted=0 AND ba.Disabled= 0
     AND br.Deleted=0 AND br.Disabled= 0 AND bd.Deleted=0 AND bd.Disabled= 0
	 AND ap.Deleted =0 AND ap.Disabled=0
	 AND ba.EndDate > GETDATE() AND ba.EndDate <= DATEADD(month, 6, GETDATE())
UNION
	 SELECT 
	   'Leadership roles held by Women' AS Title, 
	   COUNT(ba.id) AS Val,
	   '' AS Notes,
	   '' AS Icon
    FROM [dbo].[BoardAppointments] ba
    JOIN [dbo].[BoardRoles] br ON br.Id= ba.BoardRoleId
    JOIN [dbo].[Boards] bd ON bd.Id =ba.BoardId
	JOIN [dbo].[Appointee] ap On ap.Id = ba.AppointeeId
   WHERE ba.Deleted=0 AND ba.Disabled= 0
     AND br.Deleted=0 AND br.Disabled= 0 AND bd.Deleted=0 AND bd.Disabled= 0
	 AND ap.Deleted =0 AND ap.Disabled=0
	 AND ba.EndDate > GETDATE()
	 AND ap.Gender =2 --female





GO


