SELECT 
  b.Id as [BoardID]
  ,b.Name as [Board]
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
  ,r.Id as [RoleID]
  ,r.Name as [Role]
  ,case when r.LeadTimeToAppoint is null then 0 else r.LeadTimeToAppoint end as [LeadTimeToAppoint]
  ,r.Term as [TermLimit]
  ,r.AppointerId as [RoleAppointedBy]
  ,(SELECT os.[Label] FROM OptionSet os where os.ID=r.PositionId) as [PositionType]
  ,(SELECT os.[Label] FROM OptionSet os where os.ID=a.AppointmentSourceId) as [CandidateSource]
  ,(SELECT os.[Label] FROM OptionSet os where os.ID=a.SelectionProcessId) as [SelectionProcess]
  ,(SELECT os.[Label] FROM OptionSet os where os.ID=a.JudicialId) as [Judicial]
  ,(CASE WHEN a.[IsSemiDiscretionary]=1 then 'Yes' else 'No'End) as [SemiDiscretionary]
  ,(CASE WHEN a.IsExOfficio=1 then 'Yes' else 'No' End) as [ExOfficio]
  ,(CASE WHEN a.Id is null THEN (CASE when r.IsFullTime=1 then 'Yes' else 'No' End) ELSE (CASE when a.IsFullTime=1 then 'Yes' else 'No' End)  END) AS [FullTime]
  ,(CASE WHEN r.PositionRemunerated=0 then 'Yes' When r.PositionRemunerated=1 then 'No' else 'Not disclosed' End) as [Remunerated]
  ,r.PaAmount as [Remuneration]
  ,(SELECT os.[Label] FROM OptionSet os where os.ID= r.RemunerationMethodId) as[RemunerationPeriod]
  ,(CASE when a.ActingInRole=1 then 'Yes' else 'No'End) as [Acting]
  ,(CASE WHEN r.ExcludeGenderReport is null THEN 'True' ELSE (case when r.ExcludeGenderReport=1 then 'True' else 'False' end) END) AS [GenderReportable]
  ,a.Id as [AppointmentID]
  ,a.StartDate as [StartDate]
  ,a.EndDate as [EndDate]
  ,(SELECT os.[Label] FROM OptionSet os where os.ID=a.AppointerId) as [AppointmentBy]
  ,a.BriefNumber as [BriefNumber]
  ,a.AppointmentDate as [BriefDate]
  ,c.Id as [PersonID]
  ,c.fullname as [FullName]
  ,c.Title as [Title]
  ,c.firstname as [FirstName]
  ,c.lastname as [LastName]
  ,c.PostNominals as [PostNominals]
  ,'blank' as [ExtraFullName]
  ,c.GenderName as [Gender]
  ,c.StreetAddress_State as [State]
  ,(select COUNT(*) from [dbo].[VwBoardAppointments] where BoardRoleId in (select Id from  [dbo].[VwBoardRoles] where Id = b.Id) and AppointeeId = c.Id) [CurrentTerm]
  ,r.[ProcessStatus] as [ProcessStatus]
  ,r.NextSteps as [ProcessNextStep]
FROM [dbo].[VwBoards] b
LEFT JOIN [dbo].[VwBoardRoles] r ON r.BoardId= b.Id
LEFT JOIN [dbo].[VwBoardAppointments] a ON a.BoardRoleId = r.Id
LEFT JOIN [dbo].[VwAppointee] c ON c.Id= a.AppointeeId
WHERE a.EndDate <= DATEADD(YEAR,1,GetDate()) or a.EndDate is null
ORDER BY b.Name,r.Name