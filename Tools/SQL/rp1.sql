Declare @DateReport Date
BEGIN 
SET  @DateReport= GETDATE()

SELECT 
   b.Id as [BoardID]
  ,b.[Name] as [Board]
  ,bNumbers.Female
  ,bNumbers.Male 
  ,bNumbers.NSW
  ,bNumbers.WA
  ,bNumbers.VIC 
  ,bNumbers.QLD
  ,bNumbers.SA
  ,bNumbers.TAS
  ,bNumbers.NT
  ,bNumbers.ACT
  ,b.new_ownerpositionoptionsname as[OwnerPosition]
  ,b.new_ownerdivisionoptionname as [OwnerDivision]
  ,b.new_responsibleofficerusername as[ResponsibleOfficer]
  ,b.LegislationReference as [LegislationReference]
  ,b.Constitution as [Constitution]
  ,b.NominationCommittee as [NominationCommittee]
  ,b.MinimumMembers as [MinimumMembers]
  ,b.OptimumMembers as [OptimumMembers]
  ,b.MaximumMembers as [MaximumMembers]
  ,b.QuorumRequiredText as [Quorum]
  ,b.Description as [Description]
  ,b.PendingAction as [ActionPending]
  ,b.new_responsibleas as SESContactOfficer
  ,b.new_responsibleasphone as SESContactPhone
  ,r.Id as [RoleID]
  ,r.Name as [Role]
  ,case when r.LeadTimeToAppoint is null then 0 else r.LeadTimeToAppoint end as [LeadTimeToAppoint]
  ,r.Term as [TermLimit]
  ,r.new_appointedbyname as [RoleAppointedBy]
  ,r.new_typename as [PositionType]
  ,CASE WHEN r.new_minsub = 'Date' THEN CONVERT(nvarchar(12),r.new_minsubdate,103) ELSE r.new_minsub END as MinSub
  ,r.new_minsubdate as MinSubDate
  ,r.new_minsublocationname as MinSubLocation
  ,r.new_ministerialdecisionby
  ,CASE WHEN r.new_minlettertopmname = 'Date' THEN convert(nvarchar(12),r.new_minlettertopmdate,103) ELSE r.new_minlettertopmname END as MinLetterToPM
  ,CASE WHEN r.new_cabinetname = 'Date' THEN convert(nvarchar(12),r.new_cabinetdate,103) ELSE r.new_cabinetname END as Cabinet
  ,CASE WHEN r.new_exconame = 'Date' THEN convert(nvarchar(12),r.new_excodate,103) ELSE r.new_exconame END as ExCo
  ,CASE WHEN r.new_notificationlettersname = 'Date' THEN convert(nvarchar(12),r.new_notificationlettersdate,103) ELSE r.new_notificationlettersname END as NotificationLetters
  ,a.new_sourcename as [CandidateSource]
  ,a.new_selectionprocessname as [SelectionProcess]
  ,a.new_judicialname as [Judicial]
  ,a.new_semidiscretionaryname as [SemiDiscretionary]
  ,a.new_exofficioname as [ExOfficio]
  ,case  when a.new_currentappointment is null then r.new_fulltimeparttimename  else a.new_fulltimeparttimename  end [FullTime]
  ,a.new_remuneration as [Remuneration]
  ,a.new_remunerationperiodname as[RemunerationPeriod]
  ,a.new_substantiveactingname as [Acting]
  ,case when r.new_genderreportablename is null then 'True' else (case when r.new_genderreportablename = 'Yes' then 'True' else 'False' end) end as [GenderReportable]
  ,a.new_appointmentid as [AppointmentID]
  ,a.new_startdate as [StartDate]
  ,CASE WHEN fap.new_enddate is null THEN a.new_enddate ELSE fap.new_enddate END as [EndDate]
  ,a.new_appointedby as [AppointmentBy]
  ,a.new_briefnumber as [BriefNumber]
  ,a.new_appointmentdateutc as [BriefDate]
  ,c.Id as [PersonID]
  ,c.FullName as [FullName]
  ,c.Title as [Title]
  ,c.firstname as [FirstName]
  ,c.lastname as [LastName]
  ,c.PostNominals as [PostNomials]
  ,'blank' as [ExtraFullName]
  ,c.GenderName as [Gender]
  ,c.StreetAddress_State as [State]
  ,(select COUNT(*) from [dbo].[VwBoardAppointments] where BoardRoleId in (select id from [dbo].[VwBoardRoles] where BoardID = b.id) and AppointeeId = c.Id) [CurrentTerm]
  ,r.processstatus as [ProcessStatus]
  ,r.nextsteps as [ProcessNextStep]
  ,fa.extrafullname as [Proposed appointee]
  ,fap.Name as [FututreAppoint]
FROM [dbo].[VwBoards] b 
LEFT JOIN [dbo].[VwBoardRoles] r on b.Id = r.BoardId
LEFT JOIN [dbo].[VwBoardAppointments] a on  r.Id = a.BoardRoleId and a.IsCurrent=1
LEFT JOIN (SELECT fba.[Name],fba.enddate,fba.BoardRoleId FROM [dbo].[VwBoardAppointments] fba WHERE fba.StartDate > @DateReport) fap on fap.BoardRoleId = r.Id
LEFT JOIN [dbo].[VwAppointee] c ON c.Id = a.AppointeeId
LEFT JOIN (SELECT (pba.[Name]+  + ' (' + pap.StreetAddress_State + ')' ) as ProposedAppointeeName, (pap.FullName + case when pap.PostNominals is null then '' else ' ' + pap.PostNominals end + ' (' + pap.StreetAddress_State + ')') as ExtraFullname,pba.BoardRoleId
			 FROM [dbo].[VwBoardAppointments] pba LEFT JOIN [dbo].[VwAppointee] pap ON pap.Id= pba.AppointeeId WHERE pba.proposed=1) fa on fa.BoardRoleId = r.Id
JOIN(SELECT bd.Id as [BoardID],bd.[Name] as [Board]
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
) as bNumbers on b.Id = bNumbers.BoardID 
where ((a.EndDate >= @DateReport and a.EndDate <= DateAdd(month,6,@DateReport) or r.MinisterActionDate >= @DateReport and r.MinisterActionDate <= DateAdd(month,6,@DateReport)) or (a.StartDate is null ))
AND (fap.Name is null or fap.EndDate <= DateAdd(month,6,@DateReport))
order by new_expirydate

END





