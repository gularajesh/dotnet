---------------------------------------------------------------------------------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[Region] ON 
GO
;merge region t
using 
(
	select 1 as Id, N'APAC' as [Name]
)s on t.Id = s.Id
when not matched then 
	insert (Id,Name) 
	values(s.Id,s.Name)
when matched and t.Name != s.Name then update set t.Name= s.Name
;
SET IDENTITY_INSERT [dbo].[Region] OFF
-----------------------------------------------------------------------------------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[Frequency] ON 
GO
;merge frequency t
using 
(
	select 1 as Id, 'Annual' as [Name]
	Union all select 2, 'HalfYearly'
	Union all select 3, 'Quarterly'
)s on t.Id = s.Id
when not matched then 
	insert (Id,Name) 
	values(s.Id,s.Name)
when matched and t.Name != s.Name then update set t.Name= s.Name
;
SET IDENTITY_INSERT [dbo].[Frequency] OFF
GO
-----------------------------------------------------------------------------------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[FrequencyDetail] ON 
GO
;merge frequencydetail t
using 
(
	select 1 as Id, N'Annual' as [Name],1 as FrequencyId
	Union all select 2, N'H1',2
	Union all select 3, N'H2',2
	Union all select 4, N'Q1',3
	Union all select 5, N'Q2',3
	Union all select 6, N'Q3',3
	Union all select 7, N'Q4',3
)s on t.Id = s.Id
when not matched then 
	insert (Id,Name,FrequencyId) 
	values(s.Id,s.Name,s.FrequencyId)
when matched and (t.Name != s.Name  or t.FrequencyId != s.FrequencyId) 
	then update set t.Name= s.Name,
					t.FrequencyId = s.FrequencyId
;
SET IDENTITY_INSERT [dbo].[FrequencyDetail] OFF
GO
-----------------------------------------------------------------------------------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[PayoutType] ON 
GO
;merge payouttype t
using 
(
	select 1 as Id, N'Discrete' as [Name]
	Union all select 2,  N'Cumulative'
)s on t.Id = s.Id
when not matched then 
	insert (Id,Name) 
	values(s.Id,s.Name)
when matched and t.Name != s.Name then update set t.Name= s.Name
;
SET IDENTITY_INSERT [dbo].[PayoutType] OFF
GO
------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[PayoutCurveType] ON 
GO
;merge payoutcurvetype t
using 
(
	select 1 as Id,  N'Multiplier' as [Name]
	Union all select 2, N'Direct'
	Union all select 3, N'Linear'
	Union all select 4, N'Flat'
)s on t.Id = s.Id
when not matched then 
	insert (Id,Name) 
	values(s.Id,s.Name)
when matched and t.Name != s.Name then update set t.Name= s.Name
;
SET IDENTITY_INSERT [dbo].[PayoutCurveType] OFF
GO
------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[City] ON 
GO
;merge city t
using 
(
	select 1 as Id,   N'All' as [Name]
)s on t.Id = s.Id
when not matched then 
	insert (Id,Name) 
	values(s.Id,s.Name)
when matched and t.Name != s.Name then update set t.Name= s.Name
;
SET IDENTITY_INSERT [dbo].[City] OFF
GO
------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].Permission ON 
GO
;merge Permission t
using 
(
	select 1 as Id,   N'Edit User' as [Name]
	union all select 2,   N'Read User'
	union all select 3,   N'Edit Plan'
	union all select 4,   N'Read Plan'
	union all select 5,   N'Edit Goal'
	union all select 6,   N'Read Goal'
	union all select 7,   N'Edit Salary'
	union all select 8,   N'Read Salary'
	union all select 9,   N'Generate Key'
	union all select 10,  N'Upload Document'
	
)s on t.Id = s.Id
when not matched then 
	insert (Id,Name) 
	values(s.Id,s.Name)
when matched and t.Name != s.Name then update set t.Name= s.Name
;
SET IDENTITY_INSERT [dbo].Permission OFF
GO
------------------------------------------------------------------------------------------------------------------------------------------------------------------------
