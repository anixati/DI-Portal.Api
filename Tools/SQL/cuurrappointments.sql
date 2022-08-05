
SELECT 
b.[Name] as Board
,r.[Name] as Position
,c.Title as NameTitle
,c.firstname as FirstName
,c.lastname as LastName
,c.PostNominals as PostNominals
,c.GenderName AS gendercodename 
,a.EndDate as AppointmentEndDate
,bc.Female
,bc.Male
,bc.ExOfficio 
FROM [dbo].[VwBoards] b
LEFT JOIN [dbo].[VwBoardRoles] r ON r.BoardId= b.Id
LEFT JOIN [dbo].[VwBoardAppointments] a ON a.BoardRoleId = r.Id
LEFT JOIN [dbo].[VwAppointee] c ON c.Id= a.AppointeeId
JOIN (SELECT 
		b1.Id as [BoardID]
		,SUM(case when c1.Gender=2 and r1.IsExOfficio=0 THEN 1 ELSE 0 END) as Female
		,sum(case when c1.Gender=1 and r1.IsExOfficio=0	THEN 1 ELSE 0 END) as Male
		,sum(case when r1.IsExOfficio=1 THEN 1 ELSE 0 END) as ExOfficio
		FROM [dbo].[VwBoards] b1
		LEFT JOIN [dbo].[VwBoardRoles] r1 ON r1.BoardId= b1.Id
		LEFT JOIN [dbo].[VwBoardAppointments] a1 ON a1.BoardRoleId = r1.Id
		LEFT JOIN [dbo].[VwAppointee] c1 ON c1.Id= a1.AppointeeId
		GROUP BY b1.Id
)bc on b.Id = bc.BoardID 
order by b.name

