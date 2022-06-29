USE [DI_Boards]
GO
SET IDENTITY_INSERT [acl].[Roles] ON 
GO
INSERT [acl].[Roles] ([Id], [Code], [Locked], [Disabled], [Deleted], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [Name], [Description]) VALUES (1, N'8', 1, 0, 0, CAST(N'2022-06-24T03:06:04.4090142' AS DateTime2), N'|SYSTEM', CAST(N'2022-06-24T03:06:04.4090143' AS DateTime2), N'|SYSTEM', N'Viewer', N'Readonly User')
GO
INSERT [acl].[Roles] ([Id], [Code], [Locked], [Disabled], [Deleted], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [Name], [Description]) VALUES (2, N'4', 1, 0, 0, CAST(N'2022-06-24T03:06:04.4090134' AS DateTime2), N'|SYSTEM', CAST(N'2022-06-24T03:06:04.4090135' AS DateTime2), N'|SYSTEM', N'Contributor', N'Contributor')
GO
INSERT [acl].[Roles] ([Id], [Code], [Locked], [Disabled], [Deleted], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [Name], [Description]) VALUES (3, N'2', 1, 0, 0, CAST(N'2022-06-24T03:06:04.4090120' AS DateTime2), N'|SYSTEM', CAST(N'2022-06-24T03:06:04.4090123' AS DateTime2), N'|SYSTEM', N'Admin', N'Business Administrator')
GO
INSERT [acl].[Roles] ([Id], [Code], [Locked], [Disabled], [Deleted], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [Name], [Description]) VALUES (4, N'1', 1, 0, 0, CAST(N'2022-06-24T03:06:04.4089725' AS DateTime2), N'|SYSTEM', CAST(N'2022-06-24T03:06:04.4089939' AS DateTime2), N'|SYSTEM', N'SysAdmin', N'System Administrator')
GO
INSERT [acl].[Roles] ([Id], [Code], [Locked], [Disabled], [Deleted], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [Name], [Description]) VALUES (5, N'0', 1, 0, 0, CAST(N'2022-06-24T03:06:04.4085818' AS DateTime2), N'|SYSTEM', CAST(N'2022-06-24T03:06:04.4087332' AS DateTime2), N'|SYSTEM', N'None', N'None')
GO
SET IDENTITY_INSERT [acl].[Roles] OFF
GO

-- user role

INSERT INTO [acl].[UserRoles] ([AppUserId] ,[AppRoleId] ,[Locked] ,[Disabled] ,[Deleted])
     VALUES (1,4,0,0,0)
GO


