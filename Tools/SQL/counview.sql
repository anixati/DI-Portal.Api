

SELECT Name,StartDate,EndDate from BoardAppointments where [IsCurrent] is  null

select * from BoardRoles where IncumbentId is not null


SELECT 
bd.Id as [BoardID],
bd.Name as [Board],
MAX(CASE WHEN ap.Gender=2 THEN 1 ELSE 0 END )AS Female,
MAX(CASE WHEN ap.Gender=1 THEN 1 ELSE 0 END )AS Male,

FROM [dbo].[VwBoards] bd
LEFT JOIN [dbo].[VwBoardRoles] br ON br.BoardId= bd.Id
LEFT JOIN [dbo].[VwBoardAppointments] ba ON ba.BoardRoleId = br.Id
LEFT JOIN [dbo].[VwAppointee]ap ON ap.Id= ba.AppointeeId 
--WHERE ba.IsCurrent=1
GROUP BY bd.Id,bd.Name



SUM(case when c.gendercodename = 'Female'  and r.new_genderreportablename = 'No' 			THEN 1 ELSE 0 END) as Female
,sum(case when c.gendercodename = 'Male'  and r.new_genderreportablename = 'No'			THEN 1 ELSE 0 END) as Male
sum(case when c.territorycodename = 'NSW' THEN 1 ELSE 0 END) as NSW
sum(case when c.territorycodename = 'WA' THEN 1 ELSE 0 END) as WA
sum(case when c.territorycodename = 'VIC' THEN 1 ELSE 0 END) as VIC
sum(case when c.territorycodename = 'QLD' THEN 1 ELSE 0 END) as QLD
sum(case when c.territorycodename = 'SA' THEN 1 ELSE 0 END) as SA
sum(case when c.territorycodename = 'TAS' THEN 1 ELSE 0 END) as TAS
sum(case when c.territorycodename = 'NT' THEN 1 ELSE 0 END) as NT
sum(case when c.territorycodename = 'ACT' THEN 1 ELSE 0 END) as ACT
--SELECT b.new_boardid as [BoardID]	,b.new_name as [Board]
--	,SUM(case when c.gendercodename = 'Female'  and r.new_genderreportablename = 'No' 
--			THEN 1 ELSE 0 END) as Female
--	,sum(case when c.gendercodename = 'Male'  and r.new_genderreportablename = 'No' 
--			THEN 1 ELSE 0 END) as Male
--	,sum(case when c.territorycodename = 'NSW' THEN 1 ELSE 0 END) as NSW
--	,sum(case when c.territorycodename = 'WA' THEN 1 ELSE 0 END) as WA
--	,sum(case when c.territorycodename = 'VIC' THEN 1 ELSE 0 END) as VIC
--	,sum(case when c.territorycodename = 'QLD' THEN 1 ELSE 0 END) as QLD
--	,sum(case when c.territorycodename = 'SA' THEN 1 ELSE 0 END) as SA
--	,sum(case when c.territorycodename = 'TAS' THEN 1 ELSE 0 END) as TAS
--	,sum(case when c.territorycodename = 'NT' THEN 1 ELSE 0 END) as NT
--	,sum(case when c.territorycodename = 'ACT' THEN 1 ELSE 0 END) as ACT

--from Filterednew_board as b 
--left join Filterednew_role as r on b.new_boardid = r.new_boardroleid and b.statecodename = 'active'
--left join Filterednew_appointment as a on r.new_roleid = a.new_roleappointmentid and a.statecodename = 'active' and a.new_currentappointment = 1
--left join FilteredContact as c on a.new_contactappointmentid = c.contactid
--group by b.new_boardid
--	,b.new_name