
	SELECT 
	bd.Id as [BoardID],
	bd.Name As [Board],
	dbo.GetCodeLabel(bd.[OwnerPositionId],'OwnerPosition' )AS OwnerPosition,
	dbo.GetCodeLabel(bd.[OwnerDivisionId],'OwnerDivision' )AS OwnerDivision,
	ro.[FullName] AS ResponsibleOfficer,
	bd.[LegislationReference],
	bd.Constitution,
	bd.NominationCommittee,
	bd.MinimumMembers,
	bd.MaximumMembers,
	bd.OptimumMembers,
	bd.QuorumRequiredText,
	bd.PendingAction,
	br.id AS RoleId,
	br.[Name] AS [Role],
	0 AS [LeadTimeToAppoint],
	br.Term AS [TermLimit],
	'TODO' AS [RoleAppointedBy],
	'TODO' AS [PositionType],
	br.PositionRemunerated AS [Remunerated],
	br.PaAmount AS [Remuneration],
	br.[NextSteps] AS [ProcessNextStep],
	'TODO' AS  [ProcessStatus],
	ba.ExclGenderReport AS [GenderReportable],
	ba.Id AS [AppointmentID],
	dbo.GetCodeLabel(ba.[RemunerationPeriodId],'RemunerationPeriod' )AS [RemunerationPeriod],
	dbo.GetCodeLabel(ba.[AppointmentSourceId],'AppointmentSource' )AS [CandidateSource],
	dbo.GetCodeLabel(ba.[SelectionProcessId],'SelectionProcess' )AS [SelectionProcess],
	dbo.GetCodeLabel(ba.[JudicialId],'Judicial' )AS  [Judicial],
	'TODO' AS [SemiDiscretionary],
	ba.[IsFullTime] AS [FullTime],
	ba.IsExOfficio AS [ExOfficio],
	ba.StartDate as [StartDate],
	ba.EndDate as [EndDate],
	ba.BriefNumber as [BriefNumber],
	ba.AppointmentDate [BriefDate],
	ba.[ActingInRole] as [Acting],
	'TODO' AS [AppointmentBy],
	ap.Id AS [PersonID],
	ap.FullName AS [FullName],
	ap.Title AS [Title],
	ap.PostNominals,
	ap.Gender AS [Gender],
	ap.StreetAddress_State AS [State],
	'TODO' AS [CurrentTerm]
	FROM dbo.VwBoards bd
	LEFT OUTER JOIN dbo.VwBoardRoles br ON br.BoardId= bd.Id
	LEFT OUTER JOIN dbo.VwBoardAppointments ba ON ba.BoardRoleId= br.Id
	LEFT OUTER JOIN dbo.VwAppointee ap ON ap.Id= ba.AppointeeId
	LEFT OUTER JOIN dbo.vwUsers ro ON ro.Id= bd.ResponsibleUserId

GO