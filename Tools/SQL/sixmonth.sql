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
  ,(SELECT os.[Label] FROM OptionSet os where os.ID=b.OwnerPositionId) as[OwnerPosition]
  ,(SELECT os.[Label] FROM OptionSet os where os.ID=b.OwnerDivisionId) as [OwnerDivision]
  ,(SELECT usr.FullName FROM [dbo].[VwUsers] usr WHERE usr.Id= b.ResponsibleUserId) as[ResponsibleOfficer]
  ,b.LegislationReference as [LegislationReference]
  ,b.Constitution as [Constitution]
  ,b.NominationCommittee as [NominationCommittee]
  ,b.MinimumMembers as [MinimumMembers]
  ,b.OptimumMembers as [OptimumMembers]
  ,b.MaximumMembers as [MaximumMembers]
  ,b.QuorumRequiredText as [Quorum]
  ,b.Description as [Description]
  ,b.PendingAction as [ActionPending]
  ,(SELECT sec.FullName FROM [dbo].[VwSecretaries] sec WHERE sec.Id= b.AsstSecretaryId) as SESContactOfficer
  ,b.AsstSecretaryPhone as SESContactPhone
  ,r.Id as [RoleID]
  ,r.Name as [Role]
  ,case when r.LeadTimeToAppoint is null then 0 else r.LeadTimeToAppoint end as [LeadTimeToAppoint]
  ,r.Term as [TermLimit]
  ,(SELECT os.[Label] FROM OptionSet os where os.ID=r.AppointerId) as [RoleAppointedBy]
  ,(SELECT os.[Label] FROM OptionSet os where os.ID=r.PositionId) as [PositionType]
  ,(SELECT os.[Label] FROM OptionSet os where os.ID=r.MinSubLocationId)as MinSubLocation
  ,r.MinisterActionDate as new_ministerialdecisionby
  ,r.MinisterOfficeDate as MinSubDate
  ,(CASE WHEN r.[MinSubDateType]=2 THEN CONVERT(nvarchar(12),r.[MinSubDate],103) WHEN r.[MinSubDateType]=1 THEN 'TBA' ELSE 'NA'  END) AS MinSub
  ,(CASE WHEN r.[LetterToPmDateType]=2 THEN CONVERT(nvarchar(12),r.[LetterToPmDate],103) WHEN r.[LetterToPmDateType]=1 THEN 'TBA' ELSE 'NA'  END) AS MinLetterToPM
  ,(CASE WHEN r.[CabinetDateType]=2 THEN CONVERT(nvarchar(12),r.[CabinetDate],103) WHEN r.[CabinetDateType]=1 THEN 'TBA' ELSE 'NA'  END) AS Cabinet
  ,(CASE WHEN r.[ExCoDateType]=2 THEN CONVERT(nvarchar(12),r.[ExCoDate],103) WHEN r.[ExCoDateType]=1 THEN 'TBA' ELSE 'NA'  END) AS ExCo
  ,(CASE WHEN r.[NotifyLetterDateType]=2 THEN CONVERT(nvarchar(12),r.[NotifyLetterDate],103) WHEN r.[NotifyLetterDateType]=1 THEN 'TBA' ELSE 'NA'  END) AS NotificationLetters
  ,(SELECT os.[Label] FROM OptionSet os where os.ID=a.AppointmentSourceId) as [CandidateSource]
  ,(SELECT os.[Label] FROM OptionSet os where os.ID=a.SelectionProcessId) as [SelectionProcess]
  ,(SELECT os.[Label] FROM OptionSet os where os.ID=a.JudicialId) as [Judicial]
  ,(CASE WHEN a.[IsSemiDiscretionary]=1 then 'Yes' else 'No'End) as [SemiDiscretionary]
  ,(CASE WHEN a.IsExOfficio=1 then 'Yes' else 'No' End) as [ExOfficio]
  ,(CASE WHEN a.Id is null THEN (CASE when r.IsFullTime=1 then 'Yes' else 'No' End) ELSE (CASE when a.IsFullTime=1 then 'Yes' else 'No' End)  END) AS [FullTime]
  ,(CASE WHEN r.PositionRemunerated=0 then 'Yes' When r.PositionRemunerated=1 then 'No' else 'Not disclosed' End) as [Remunerated]
  ,a.AnnumAmount as [Remuneration]
  ,(SELECT os.[Label] FROM OptionSet os where os.ID=a.RemunerationPeriodId) as[RemunerationPeriod]
  ,(CASE when a.ActingInRole=1 then 'Yes' else 'No'End) as [Acting]
  ,case when r.ExcludeGenderReport is null then 'True' else (case when r.ExcludeGenderReport=1 then 'True' else 'False' end) end as [GenderReportable]
  ,a.Id as [AppointmentID]
  ,a.StartDate as [StartDate]
  ,CASE WHEN fap.EndDate is null THEN a.EndDate ELSE fap.EndDate END as [EndDate]
  ,(SELECT os.[Label] FROM OptionSet os where os.ID=a.AppointerId) as [AppointmentBy]
  ,a.BriefNumber as [BriefNumber]
  ,a.AppointmentDate as [BriefDate]
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
  ,r.ProcessStatus as [ProcessStatus]
  ,r.nextsteps as [ProcessNextStep]
  ,fa.ExtraFullname as [Proposed appointee]
  ,fap.Name as [FututreAppoint]
  FROM [dbo].[VwBoards] b 
LEFT JOIN [dbo].[VwBoardRoles] r on b.Id = r.BoardId
LEFT JOIN [dbo].[VwBoardAppointments] a on  r.Id = a.BoardRoleId and a.IsCurrent=1
LEFT JOIN (SELECT fba.[Name],fba.enddate,fba.BoardRoleId FROM [dbo].[VwBoardAppointments] fba WHERE fba.StartDate > @DateReport) fap on fap.BoardRoleId = r.Id
LEFT JOIN [dbo].[VwAppointee] c ON c.Id = a.AppointeeId
LEFT JOIN (SELECT (pba.[Name]+  + ' (' + pap.StreetAddress_State + ')' ) as ProposedAppointeeName, (pap.FullName + case when pap.PostNominals is null then '' else ' ' + pap.PostNominals end + ' (' + pap.StreetAddress_State + ')') as ExtraFullname,pba.BoardRoleId
			 FROM [dbo].[VwBoardAppointments] pba LEFT JOIN [dbo].[VwAppointee] pap ON pap.Id= pba.AppointeeId WHERE pba.proposed=1) fa on fa.BoardRoleId = r.Id
JOIN [dbo].[vwBoardCounts] bNumbers on b.Id = bNumbers.BoardID 
where ((a.EndDate >= @DateReport and a.EndDate <= DateAdd(month,6,@DateReport) or r.MinisterActionDate >= @DateReport and r.MinisterActionDate <= DateAdd(month,6,@DateReport)) or (a.StartDate is null ))
AND (fap.Name is null or fap.EndDate <= DateAdd(month,6,@DateReport))
--order by new_expirydate