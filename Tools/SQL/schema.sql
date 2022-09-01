IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF SCHEMA_ID(N'acl') IS NULL EXEC(N'CREATE SCHEMA [acl];');
GO

CREATE TABLE [Dbo].[Activities] (
    [Id] bigint NOT NULL IDENTITY,
    [Title] nvarchar(500) NOT NULL,
    [Notes] nvarchar(max) NULL,
    [ContentType] nvarchar(255) NULL,
    [Locked] bit NOT NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Timestamp] rowversion NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedBy] nvarchar(50) NOT NULL,
    [ModifiedOn] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(50) NOT NULL,
    [EntityName] nvarchar(100) NOT NULL,
    [EntityId] bigint NOT NULL,
    CONSTRAINT [PK_Activities] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Dbo].[AuditHistory] (
    [Id] bigint NOT NULL IDENTITY,
    [AuditDate] datetime2 NOT NULL,
    [Action] nvarchar(50) NOT NULL,
    [TableName] nvarchar(50) NOT NULL,
    [Data] nvarchar(max) NULL,
    [Locked] bit NOT NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Timestamp] rowversion NULL,
    CONSTRAINT [PK_AuditHistory] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Dbo].[AutoNumbers] (
    [Id] bigint NOT NULL IDENTITY,
    [TenantId] uniqueidentifier NULL,
    [Name] nvarchar(max) NOT NULL,
    [Number] int NOT NULL,
    [Locked] bit NOT NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Timestamp] rowversion NULL,
    CONSTRAINT [PK_AutoNumbers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Dbo].[DeleteRecords] (
    [Id] bigint NOT NULL IDENTITY,
    [Notes] nvarchar(500) NULL,
    [Locked] bit NOT NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Timestamp] rowversion NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedBy] nvarchar(50) NOT NULL,
    [ModifiedOn] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(50) NOT NULL,
    [EntityName] nvarchar(100) NOT NULL,
    [EntityId] bigint NOT NULL,
    CONSTRAINT [PK_DeleteRecords] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Dbo].[Ministers] (
    [Id] bigint NOT NULL IDENTITY,
    [MigratedId] nvarchar(255) NULL,
    [Locked] bit NOT NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Timestamp] rowversion NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedBy] nvarchar(50) NOT NULL,
    [ModifiedOn] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(50) NOT NULL,
    [Title] nvarchar(255) NULL,
    [FirstName] nvarchar(255) NOT NULL,
    [LastName] nvarchar(255) NOT NULL,
    [MiddleName] nvarchar(255) NULL,
    [Gender] int NOT NULL,
    [HomePhone] nvarchar(10) NULL,
    [MobilePhone] nvarchar(10) NULL,
    [FaxNumber] nvarchar(10) NULL,
    [Email1] nvarchar(50) NULL,
    [Email2] nvarchar(50) NULL,
    [StreetAddress_Unit] nvarchar(max) NULL,
    [StreetAddress_Street] nvarchar(max) NULL,
    [StreetAddress_City] nvarchar(max) NULL,
    [StreetAddress_Postcode] smallint NULL,
    [StreetAddress_State] nvarchar(max) NULL,
    [StreetAddress_Country] nvarchar(max) NULL,
    [PostalAddress_Unit] nvarchar(max) NULL,
    [PostalAddress_Street] nvarchar(max) NULL,
    [PostalAddress_City] nvarchar(max) NULL,
    [PostalAddress_Postcode] smallint NULL,
    [PostalAddress_State] nvarchar(max) NULL,
    [PostalAddress_Country] nvarchar(max) NULL,
    CONSTRAINT [PK_Ministers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Dbo].[OptionKeys] (
    [Id] bigint NOT NULL IDENTITY,
    [Code] nvarchar(100) NOT NULL,
    [Locked] bit NOT NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Timestamp] rowversion NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedBy] nvarchar(50) NOT NULL,
    [ModifiedOn] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(50) NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [Description] nvarchar(2000) NULL,
    CONSTRAINT [PK_OptionKeys] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Dbo].[Portfolios] (
    [Id] bigint NOT NULL IDENTITY,
    [MigratedId] nvarchar(255) NULL,
    [Locked] bit NOT NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Timestamp] rowversion NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedBy] nvarchar(50) NOT NULL,
    [ModifiedOn] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(50) NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [Description] nvarchar(2000) NULL,
    CONSTRAINT [PK_Portfolios] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [acl].[Resources] (
    [Id] bigint NOT NULL IDENTITY,
    [Locked] bit NOT NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Timestamp] rowversion NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedBy] nvarchar(50) NOT NULL,
    [ModifiedOn] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(50) NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [Description] nvarchar(2000) NULL,
    CONSTRAINT [PK_Resources] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [acl].[Roles] (
    [Id] bigint NOT NULL IDENTITY,
    [Code] nvarchar(30) NOT NULL,
    [IsSystem] bit NOT NULL,
    [Locked] bit NOT NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Timestamp] rowversion NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedBy] nvarchar(50) NOT NULL,
    [ModifiedOn] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(50) NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [Description] nvarchar(2000) NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Dbo].[Secretaries] (
    [Id] bigint NOT NULL IDENTITY,
    [Locked] bit NOT NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Timestamp] rowversion NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedBy] nvarchar(50) NOT NULL,
    [ModifiedOn] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(50) NOT NULL,
    [Title] nvarchar(255) NULL,
    [FirstName] nvarchar(255) NOT NULL,
    [LastName] nvarchar(255) NOT NULL,
    [MiddleName] nvarchar(255) NULL,
    [Gender] int NOT NULL,
    [HomePhone] nvarchar(10) NULL,
    [MobilePhone] nvarchar(10) NULL,
    [FaxNumber] nvarchar(10) NULL,
    [Email1] nvarchar(50) NULL,
    [Email2] nvarchar(50) NULL,
    [StreetAddress_Unit] nvarchar(max) NULL,
    [StreetAddress_Street] nvarchar(max) NULL,
    [StreetAddress_City] nvarchar(max) NULL,
    [StreetAddress_Postcode] smallint NULL,
    [StreetAddress_State] nvarchar(max) NULL,
    [StreetAddress_Country] nvarchar(max) NULL,
    [PostalAddress_Unit] nvarchar(max) NULL,
    [PostalAddress_Street] nvarchar(max) NULL,
    [PostalAddress_City] nvarchar(max) NULL,
    [PostalAddress_Postcode] smallint NULL,
    [PostalAddress_State] nvarchar(max) NULL,
    [PostalAddress_Country] nvarchar(max) NULL,
    CONSTRAINT [PK_Secretaries] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Dbo].[Settings] (
    [Id] bigint NOT NULL IDENTITY,
    [TenantId] uniqueidentifier NULL,
    [Name] nvarchar(max) NOT NULL,
    [Value] nvarchar(max) NOT NULL,
    [Locked] bit NOT NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Timestamp] rowversion NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedBy] nvarchar(50) NOT NULL,
    [ModifiedOn] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Settings] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [acl].[Teams] (
    [Id] bigint NOT NULL IDENTITY,
    [IsSystem] bit NOT NULL,
    [Locked] bit NOT NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Timestamp] rowversion NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedBy] nvarchar(50) NOT NULL,
    [ModifiedOn] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(50) NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [Description] nvarchar(2000) NULL,
    CONSTRAINT [PK_Teams] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [acl].[Users] (
    [Id] bigint NOT NULL IDENTITY,
    [UserId] nvarchar(255) NOT NULL,
    [PasswordHash] nvarchar(max) NOT NULL,
    [NameId] nvarchar(500) NULL,
    [Upn] nvarchar(500) NULL,
    [DisplayName] nvarchar(500) NULL,
    [AccessRequest] datetime2 NULL,
    [AccessGranted] datetime2 NULL,
    [IsSystem] bit NOT NULL,
    [MigratedId] nvarchar(255) NULL,
    [Locked] bit NOT NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Timestamp] rowversion NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedBy] nvarchar(50) NOT NULL,
    [ModifiedOn] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(50) NOT NULL,
    [Title] nvarchar(255) NULL,
    [FirstName] nvarchar(255) NOT NULL,
    [LastName] nvarchar(255) NOT NULL,
    [MiddleName] nvarchar(255) NULL,
    [Gender] int NOT NULL,
    [HomePhone] nvarchar(10) NULL,
    [MobilePhone] nvarchar(10) NULL,
    [FaxNumber] nvarchar(10) NULL,
    [Email1] nvarchar(50) NULL,
    [Email2] nvarchar(50) NULL,
    [StreetAddress_Unit] nvarchar(max) NULL,
    [StreetAddress_Street] nvarchar(max) NULL,
    [StreetAddress_City] nvarchar(max) NULL,
    [StreetAddress_Postcode] smallint NULL,
    [StreetAddress_State] nvarchar(max) NULL,
    [StreetAddress_Country] nvarchar(max) NULL,
    [PostalAddress_Unit] nvarchar(max) NULL,
    [PostalAddress_Street] nvarchar(max) NULL,
    [PostalAddress_City] nvarchar(max) NULL,
    [PostalAddress_Postcode] smallint NULL,
    [PostalAddress_State] nvarchar(max) NULL,
    [PostalAddress_Country] nvarchar(max) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Dbo].[OptionSet] (
    [Id] bigint NOT NULL IDENTITY,
    [OptionKeyId] bigint NOT NULL,
    [Label] nvarchar(255) NOT NULL,
    [Value] int NOT NULL,
    [Order] int NOT NULL,
    [Description] nvarchar(500) NULL,
    [Locked] bit NOT NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Timestamp] rowversion NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedBy] nvarchar(50) NOT NULL,
    [ModifiedOn] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_OptionSet] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OptionSet_OptionKeys_OptionKeyId] FOREIGN KEY ([OptionKeyId]) REFERENCES [Dbo].[OptionKeys] ([Id])
);
GO

CREATE TABLE [Dbo].[MinisterTerms] (
    [Id] bigint NOT NULL IDENTITY,
    [MinisterId] bigint NOT NULL,
    [PortfolioId] bigint NOT NULL,
    [StartDate] datetime2 NULL,
    [EndDate] datetime2 NULL,
    [Locked] bit NOT NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Timestamp] rowversion NULL,
    CONSTRAINT [PK_MinisterTerms] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MinisterTerms_Ministers_MinisterId] FOREIGN KEY ([MinisterId]) REFERENCES [Dbo].[Ministers] ([Id]),
    CONSTRAINT [FK_MinisterTerms_Portfolios_PortfolioId] FOREIGN KEY ([PortfolioId]) REFERENCES [Dbo].[Portfolios] ([Id])
);
GO

CREATE TABLE [acl].[Permissions] (
    [Id] bigint NOT NULL IDENTITY,
    [AppResourceId] bigint NOT NULL,
    [AppRoleId] bigint NOT NULL,
    [Read] bit NOT NULL,
    [Create] bit NOT NULL,
    [Update] bit NOT NULL,
    [Delete] bit NOT NULL,
    [Mask] int NOT NULL,
    [Locked] bit NOT NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Timestamp] rowversion NULL,
    CONSTRAINT [PK_Permissions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Permissions_Resources_AppResourceId] FOREIGN KEY ([AppResourceId]) REFERENCES [acl].[Resources] ([Id]),
    CONSTRAINT [FK_Permissions_Roles_AppRoleId] FOREIGN KEY ([AppRoleId]) REFERENCES [acl].[Roles] ([Id])
);
GO

CREATE TABLE [acl].[TeamRoles] (
    [Id] bigint NOT NULL IDENTITY,
    [AppTeamId] bigint NOT NULL,
    [AppRoleId] bigint NOT NULL,
    [Locked] bit NOT NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Timestamp] rowversion NULL,
    CONSTRAINT [PK_TeamRoles] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TeamRoles_Roles_AppRoleId] FOREIGN KEY ([AppRoleId]) REFERENCES [acl].[Roles] ([Id]),
    CONSTRAINT [FK_TeamRoles_Teams_AppTeamId] FOREIGN KEY ([AppTeamId]) REFERENCES [acl].[Teams] ([Id])
);
GO

CREATE TABLE [acl].[TeamUsers] (
    [Id] bigint NOT NULL IDENTITY,
    [AppTeamId] bigint NOT NULL,
    [AppUserId] bigint NOT NULL,
    [Locked] bit NOT NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Timestamp] rowversion NULL,
    CONSTRAINT [PK_TeamUsers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TeamUsers_Teams_AppTeamId] FOREIGN KEY ([AppTeamId]) REFERENCES [acl].[Teams] ([Id]),
    CONSTRAINT [FK_TeamUsers_Users_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [acl].[Users] ([Id])
);
GO

CREATE TABLE [acl].[UserRoles] (
    [Id] bigint NOT NULL IDENTITY,
    [AppUserId] bigint NOT NULL,
    [AppRoleId] bigint NOT NULL,
    [Locked] bit NOT NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Timestamp] rowversion NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserRoles_Roles_AppRoleId] FOREIGN KEY ([AppRoleId]) REFERENCES [acl].[Roles] ([Id]),
    CONSTRAINT [FK_UserRoles_Users_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [acl].[Users] ([Id])
);
GO

CREATE TABLE [Dbo].[Appointee] (
    [Id] bigint NOT NULL IDENTITY,
    [Biography] nvarchar(2000) NULL,
    [PostNominals] nvarchar(255) NULL,
    [ResumeLink] nvarchar(255) NULL,
    [LinkedInProfile] nvarchar(255) NULL,
    [IsRegional] bit NULL,
    [IsAboriginal] bit NULL,
    [IsDisabled] bit NULL,
    [ExecutiveSearch] bit NULL,
    [CapabilitiesId] bigint NULL,
    [ExperienceId] bigint NULL,
    [MigratedId] nvarchar(255) NULL,
    [Locked] bit NOT NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Timestamp] rowversion NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedBy] nvarchar(50) NOT NULL,
    [ModifiedOn] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(50) NOT NULL,
    [Title] nvarchar(255) NULL,
    [FirstName] nvarchar(255) NOT NULL,
    [LastName] nvarchar(255) NOT NULL,
    [MiddleName] nvarchar(255) NULL,
    [Gender] int NOT NULL,
    [HomePhone] nvarchar(10) NULL,
    [MobilePhone] nvarchar(10) NULL,
    [FaxNumber] nvarchar(10) NULL,
    [Email1] nvarchar(50) NULL,
    [Email2] nvarchar(50) NULL,
    [StreetAddress_Unit] nvarchar(max) NULL,
    [StreetAddress_Street] nvarchar(max) NULL,
    [StreetAddress_City] nvarchar(max) NULL,
    [StreetAddress_Postcode] smallint NULL,
    [StreetAddress_State] nvarchar(max) NULL,
    [StreetAddress_Country] nvarchar(max) NULL,
    [PostalAddress_Unit] nvarchar(max) NULL,
    [PostalAddress_Street] nvarchar(max) NULL,
    [PostalAddress_City] nvarchar(max) NULL,
    [PostalAddress_Postcode] smallint NULL,
    [PostalAddress_State] nvarchar(max) NULL,
    [PostalAddress_Country] nvarchar(max) NULL,
    CONSTRAINT [PK_Appointee] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Appointee_OptionSet_CapabilitiesId] FOREIGN KEY ([CapabilitiesId]) REFERENCES [Dbo].[OptionSet] ([Id]),
    CONSTRAINT [FK_Appointee_OptionSet_ExperienceId] FOREIGN KEY ([ExperienceId]) REFERENCES [Dbo].[OptionSet] ([Id])
);
GO

CREATE TABLE [Dbo].[Boards] (
    [Id] bigint NOT NULL IDENTITY,
    [PortfolioId] bigint NOT NULL,
    [AppTeamId] bigint NOT NULL,
    [Summary] nvarchar(2000) NULL,
    [PendingAction] nvarchar(2000) NULL,
    [EstablishedByUnderText] nvarchar(2000) NULL,
    [NominationCommittee] nvarchar(255) NULL,
    [OwnerDivisionId] bigint NOT NULL,
    [OwnerPositionId] bigint NOT NULL,
    [Acronym] nvarchar(255) NULL,
    [LegislationReference] nvarchar(255) NULL,
    [Constitution] nvarchar(255) NULL,
    [QuorumRequiredText] nvarchar(255) NULL,
    [OptimumMembers] int NULL,
    [MaximumTerms] int NULL,
    [MaximumMembers] int NULL,
    [MinimumMembers] int NULL,
    [QuorumRequired] int NOT NULL,
    [ReportingApproved] bit NOT NULL,
    [ExcludeFromGenderBalance] bit NOT NULL,
    [BoardStatusId] bigint NULL,
    [EstablishedByUnderId] bigint NULL,
    [ApprovedUserId] bigint NULL,
    [ResponsibleUserId] bigint NULL,
    [AsstSecretaryId] bigint NULL,
    [AsstSecretaryPhone] nvarchar(50) NULL,
    [MaxServicePeriod] int NULL,
    [MigratedId] nvarchar(255) NULL,
    [Locked] bit NOT NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Timestamp] rowversion NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedBy] nvarchar(50) NOT NULL,
    [ModifiedOn] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(50) NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [Description] nvarchar(2000) NULL,
    CONSTRAINT [PK_Boards] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Boards_OptionSet_BoardStatusId] FOREIGN KEY ([BoardStatusId]) REFERENCES [Dbo].[OptionSet] ([Id]),
    CONSTRAINT [FK_Boards_OptionSet_EstablishedByUnderId] FOREIGN KEY ([EstablishedByUnderId]) REFERENCES [Dbo].[OptionSet] ([Id]),
    CONSTRAINT [FK_Boards_OptionSet_OwnerDivisionId] FOREIGN KEY ([OwnerDivisionId]) REFERENCES [Dbo].[OptionSet] ([Id]),
    CONSTRAINT [FK_Boards_OptionSet_OwnerPositionId] FOREIGN KEY ([OwnerPositionId]) REFERENCES [Dbo].[OptionSet] ([Id]),
    CONSTRAINT [FK_Boards_Portfolios_PortfolioId] FOREIGN KEY ([PortfolioId]) REFERENCES [Dbo].[Portfolios] ([Id]),
    CONSTRAINT [FK_Boards_Secretaries_AsstSecretaryId] FOREIGN KEY ([AsstSecretaryId]) REFERENCES [Dbo].[Secretaries] ([Id]),
    CONSTRAINT [FK_Boards_Teams_AppTeamId] FOREIGN KEY ([AppTeamId]) REFERENCES [acl].[Teams] ([Id]),
    CONSTRAINT [FK_Boards_Users_ApprovedUserId] FOREIGN KEY ([ApprovedUserId]) REFERENCES [acl].[Users] ([Id]),
    CONSTRAINT [FK_Boards_Users_ResponsibleUserId] FOREIGN KEY ([ResponsibleUserId]) REFERENCES [acl].[Users] ([Id])
);
GO

CREATE TABLE [Dbo].[Skill] (
    [Id] bigint NOT NULL IDENTITY,
    [SkillTypeId] bigint NOT NULL,
    [Locked] bit NOT NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Timestamp] rowversion NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedBy] nvarchar(50) NOT NULL,
    [ModifiedOn] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(50) NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [Description] nvarchar(2000) NULL,
    CONSTRAINT [PK_Skill] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Skill_OptionSet_SkillTypeId] FOREIGN KEY ([SkillTypeId]) REFERENCES [Dbo].[OptionSet] ([Id])
);
GO

CREATE TABLE [Dbo].[BoardRoles] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(255) NOT NULL,
    [BoardId] bigint NOT NULL,
    [IncumbentId] bigint NULL,
    [PositionId] bigint NOT NULL,
    [RoleAppointerId] bigint NOT NULL,
    [IsFullTime] int NOT NULL,
    [IsExecutive] bit NULL,
    [IsExOfficio] bit NULL,
    [IsApsEmployee] bit NULL,
    [IsExNominated] bit NULL,
    [Term] int NULL,
    [PositionRemunerated] int NOT NULL,
    [PaAmount] decimal(13,2) NOT NULL,
    [RemunerationMethodId] bigint NOT NULL,
    [RemunerationTribunal] nvarchar(255) NOT NULL,
    [VacantFromDate] datetime2 NULL,
    [ExcludeFromOrder15] bit NULL,
    [ExcludeGenderReport] bit NULL,
    [IsSignAppointment] bit NOT NULL,
    [NextSteps] nvarchar(2000) NULL,
    [InstrumentLink] nvarchar(2000) NULL,
    [PDMSNumber] nvarchar(max) NOT NULL,
    [MinSubLocationId] bigint NOT NULL,
    [MinisterOfficeDate] datetime2 NULL,
    [MinisterActionDate] datetime2 NULL,
    [LetterToPmDateType] int NOT NULL,
    [LetterToPmDate] datetime2 NULL,
    [ExCoDateType] int NOT NULL,
    [ExCoDate] datetime2 NULL,
    [NotifyLetterDateType] int NOT NULL,
    [NotifyLetterDate] datetime2 NULL,
    [CabinetDateType] int NOT NULL,
    [CabinetDate] datetime2 NULL,
    [InternalNotes] nvarchar(2000) NULL,
    [ProcessStatus] nvarchar(2000) NULL,
    [LeadTimeToAppoint] int NULL,
    [MinSubDateType] int NOT NULL,
    [MinSubDate] datetime2 NULL,
    [IncumbentName] nvarchar(500) NULL,
    [IncumbentStartDate] nvarchar(255) NULL,
    [IncumbentEndDate] nvarchar(255) NULL,
    [MigratedId] nvarchar(255) NULL,
    [AssistantSecretaryId] bigint NULL,
    [Locked] bit NOT NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Timestamp] rowversion NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedBy] nvarchar(50) NOT NULL,
    [ModifiedOn] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_BoardRoles] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BoardRoles_Appointee_IncumbentId] FOREIGN KEY ([IncumbentId]) REFERENCES [Dbo].[Appointee] ([Id]),
    CONSTRAINT [FK_BoardRoles_Boards_BoardId] FOREIGN KEY ([BoardId]) REFERENCES [Dbo].[Boards] ([Id]),
    CONSTRAINT [FK_BoardRoles_OptionSet_MinSubLocationId] FOREIGN KEY ([MinSubLocationId]) REFERENCES [Dbo].[OptionSet] ([Id]),
    CONSTRAINT [FK_BoardRoles_OptionSet_PositionId] FOREIGN KEY ([PositionId]) REFERENCES [Dbo].[OptionSet] ([Id]),
    CONSTRAINT [FK_BoardRoles_OptionSet_RemunerationMethodId] FOREIGN KEY ([RemunerationMethodId]) REFERENCES [Dbo].[OptionSet] ([Id]),
    CONSTRAINT [FK_BoardRoles_OptionSet_RoleAppointerId] FOREIGN KEY ([RoleAppointerId]) REFERENCES [Dbo].[OptionSet] ([Id]),
    CONSTRAINT [FK_BoardRoles_Secretaries_AssistantSecretaryId] FOREIGN KEY ([AssistantSecretaryId]) REFERENCES [Dbo].[Secretaries] ([Id])
);
GO

CREATE TABLE [Dbo].[AppointeeSkill] (
    [Id] bigint NOT NULL IDENTITY,
    [AppointeeId] bigint NOT NULL,
    [SkillId] bigint NOT NULL,
    [Locked] bit NOT NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Timestamp] rowversion NULL,
    CONSTRAINT [PK_AppointeeSkill] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AppointeeSkill_Appointee_AppointeeId] FOREIGN KEY ([AppointeeId]) REFERENCES [Dbo].[Appointee] ([Id]),
    CONSTRAINT [FK_AppointeeSkill_Skill_SkillId] FOREIGN KEY ([SkillId]) REFERENCES [Dbo].[Skill] ([Id])
);
GO

CREATE TABLE [Dbo].[BoardAppointments] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(255) NOT NULL,
    [BoardId] bigint NOT NULL,
    [BoardRoleId] bigint NOT NULL,
    [AppointeeId] bigint NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NULL,
    [BriefNumber] nvarchar(255) NULL,
    [IsCurrent] bit NULL,
    [IsExOfficio] bit NULL,
    [IsFullTime] bit NOT NULL,
    [ActingInRole] bit NOT NULL,
    [ExclGenderReport] bit NULL,
    [AnnumAmount] decimal(13,2) NOT NULL,
    [RemunerationPeriodId] bigint NOT NULL,
    [AppointmentSourceId] bigint NULL,
    [SelectionProcessId] bigint NULL,
    [JudicialId] bigint NULL,
    [AppointmentDate] datetime2 NULL,
    [InitialStartDate] datetime2 NULL,
    [PrevTerms] int NULL,
    [IsSemiDiscretionary] bit NULL,
    [Proposed] bit NULL,
    [AppointerId] bigint NULL,
    [MigratedId] nvarchar(255) NULL,
    [Locked] bit NOT NULL,
    [Disabled] bit NOT NULL,
    [Deleted] bit NOT NULL,
    [Timestamp] rowversion NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedBy] nvarchar(50) NOT NULL,
    [ModifiedOn] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_BoardAppointments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BoardAppointments_Appointee_AppointeeId] FOREIGN KEY ([AppointeeId]) REFERENCES [Dbo].[Appointee] ([Id]),
    CONSTRAINT [FK_BoardAppointments_BoardRoles_BoardRoleId] FOREIGN KEY ([BoardRoleId]) REFERENCES [Dbo].[BoardRoles] ([Id]),
    CONSTRAINT [FK_BoardAppointments_Boards_BoardId] FOREIGN KEY ([BoardId]) REFERENCES [Dbo].[Boards] ([Id]),
    CONSTRAINT [FK_BoardAppointments_OptionSet_AppointerId] FOREIGN KEY ([AppointerId]) REFERENCES [Dbo].[OptionSet] ([Id]),
    CONSTRAINT [FK_BoardAppointments_OptionSet_AppointmentSourceId] FOREIGN KEY ([AppointmentSourceId]) REFERENCES [Dbo].[OptionSet] ([Id]),
    CONSTRAINT [FK_BoardAppointments_OptionSet_JudicialId] FOREIGN KEY ([JudicialId]) REFERENCES [Dbo].[OptionSet] ([Id]),
    CONSTRAINT [FK_BoardAppointments_OptionSet_RemunerationPeriodId] FOREIGN KEY ([RemunerationPeriodId]) REFERENCES [Dbo].[OptionSet] ([Id]),
    CONSTRAINT [FK_BoardAppointments_OptionSet_SelectionProcessId] FOREIGN KEY ([SelectionProcessId]) REFERENCES [Dbo].[OptionSet] ([Id])
);
GO

CREATE INDEX [IX_Appointee_CapabilitiesId] ON [Dbo].[Appointee] ([CapabilitiesId]);
GO

CREATE INDEX [IX_Appointee_ExperienceId] ON [Dbo].[Appointee] ([ExperienceId]);
GO

CREATE INDEX [IX_AppointeeSkill_AppointeeId] ON [Dbo].[AppointeeSkill] ([AppointeeId]);
GO

CREATE INDEX [IX_AppointeeSkill_SkillId] ON [Dbo].[AppointeeSkill] ([SkillId]);
GO

CREATE INDEX [IX_BoardAppointments_AppointeeId] ON [Dbo].[BoardAppointments] ([AppointeeId]);
GO

CREATE INDEX [IX_BoardAppointments_AppointerId] ON [Dbo].[BoardAppointments] ([AppointerId]);
GO

CREATE INDEX [IX_BoardAppointments_AppointmentSourceId] ON [Dbo].[BoardAppointments] ([AppointmentSourceId]);
GO

CREATE INDEX [IX_BoardAppointments_BoardId] ON [Dbo].[BoardAppointments] ([BoardId]);
GO

CREATE INDEX [IX_BoardAppointments_BoardRoleId] ON [Dbo].[BoardAppointments] ([BoardRoleId]);
GO

CREATE INDEX [IX_BoardAppointments_JudicialId] ON [Dbo].[BoardAppointments] ([JudicialId]);
GO

CREATE INDEX [IX_BoardAppointments_RemunerationPeriodId] ON [Dbo].[BoardAppointments] ([RemunerationPeriodId]);
GO

CREATE INDEX [IX_BoardAppointments_SelectionProcessId] ON [Dbo].[BoardAppointments] ([SelectionProcessId]);
GO

CREATE INDEX [IX_BoardRoles_AssistantSecretaryId] ON [Dbo].[BoardRoles] ([AssistantSecretaryId]);
GO

CREATE INDEX [IX_BoardRoles_BoardId] ON [Dbo].[BoardRoles] ([BoardId]);
GO

CREATE INDEX [IX_BoardRoles_IncumbentId] ON [Dbo].[BoardRoles] ([IncumbentId]);
GO

CREATE INDEX [IX_BoardRoles_MinSubLocationId] ON [Dbo].[BoardRoles] ([MinSubLocationId]);
GO

CREATE INDEX [IX_BoardRoles_PositionId] ON [Dbo].[BoardRoles] ([PositionId]);
GO

CREATE INDEX [IX_BoardRoles_RemunerationMethodId] ON [Dbo].[BoardRoles] ([RemunerationMethodId]);
GO

CREATE INDEX [IX_BoardRoles_RoleAppointerId] ON [Dbo].[BoardRoles] ([RoleAppointerId]);
GO

CREATE INDEX [IX_Boards_ApprovedUserId] ON [Dbo].[Boards] ([ApprovedUserId]);
GO

CREATE INDEX [IX_Boards_AppTeamId] ON [Dbo].[Boards] ([AppTeamId]);
GO

CREATE INDEX [IX_Boards_AsstSecretaryId] ON [Dbo].[Boards] ([AsstSecretaryId]);
GO

CREATE INDEX [IX_Boards_BoardStatusId] ON [Dbo].[Boards] ([BoardStatusId]);
GO

CREATE INDEX [IX_Boards_EstablishedByUnderId] ON [Dbo].[Boards] ([EstablishedByUnderId]);
GO

CREATE INDEX [IX_Boards_OwnerDivisionId] ON [Dbo].[Boards] ([OwnerDivisionId]);
GO

CREATE INDEX [IX_Boards_OwnerPositionId] ON [Dbo].[Boards] ([OwnerPositionId]);
GO

CREATE INDEX [IX_Boards_PortfolioId] ON [Dbo].[Boards] ([PortfolioId]);
GO

CREATE INDEX [IX_Boards_ResponsibleUserId] ON [Dbo].[Boards] ([ResponsibleUserId]);
GO

CREATE INDEX [IX_MinisterTerms_MinisterId] ON [Dbo].[MinisterTerms] ([MinisterId]);
GO

CREATE INDEX [IX_MinisterTerms_PortfolioId] ON [Dbo].[MinisterTerms] ([PortfolioId]);
GO

CREATE UNIQUE INDEX [IX_OptionKeys_Code] ON [Dbo].[OptionKeys] ([Code]);
GO

CREATE INDEX [IX_OptionSet_OptionKeyId] ON [Dbo].[OptionSet] ([OptionKeyId]);
GO

CREATE INDEX [IX_Permissions_AppResourceId] ON [acl].[Permissions] ([AppResourceId]);
GO

CREATE INDEX [IX_Permissions_AppRoleId] ON [acl].[Permissions] ([AppRoleId]);
GO

CREATE UNIQUE INDEX [IX_Roles_Code] ON [acl].[Roles] ([Code]);
GO

CREATE INDEX [IX_Skill_SkillTypeId] ON [Dbo].[Skill] ([SkillTypeId]);
GO

CREATE INDEX [IX_TeamRoles_AppRoleId] ON [acl].[TeamRoles] ([AppRoleId]);
GO

CREATE INDEX [IX_TeamRoles_AppTeamId] ON [acl].[TeamRoles] ([AppTeamId]);
GO

CREATE INDEX [IX_TeamUsers_AppTeamId] ON [acl].[TeamUsers] ([AppTeamId]);
GO

CREATE INDEX [IX_TeamUsers_AppUserId] ON [acl].[TeamUsers] ([AppUserId]);
GO

CREATE INDEX [IX_UserRoles_AppRoleId] ON [acl].[UserRoles] ([AppRoleId]);
GO

CREATE INDEX [IX_UserRoles_AppUserId] ON [acl].[UserRoles] ([AppUserId]);
GO

CREATE UNIQUE INDEX [IX_Users_UserId] ON [acl].[Users] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220901044255_Initial_Migration', N'5.0.16');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'CustomMigration_BoardDbViews', N'5.0.16');
GO

COMMIT;
GO

