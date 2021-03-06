
declare @procName varchar(500)
declare cur cursor for select [name] from sys.objects where type = 'p' 
open cur fetch next from cur into @procName while @@fetch_status = 0 begin exec('drop procedure [' + @procName + ']') 
fetch next from cur into @procName end close cur deallocate cur

Declare @sql NVARCHAR(MAX) = N'';
SELECT @sql = @sql + N' DROP type ' 
                   + QUOTENAME(SCHEMA_NAME(schema_id)) 
                   + N'.' + QUOTENAME(name)
FROM sys.types
WHERE is_user_defined = 1
Exec sp_executesql @sql

/****** Object:  UserDefinedTableType [dbo].[TYPE_COUNTRY_IMPORT]    Script Date: 02/02/2018 12:24:22 ******/
CREATE TYPE [dbo].[TYPE_COUNTRY_IMPORT] AS TABLE(
	[RegionName] [varchar](max) NULL,
	[TerritoryName] [varchar](max) NULL,
	[CountryName] [varchar](max) NULL,
	[DefaultLanguange] [varchar](max) NULL,
	[Currency] [varchar](max) NULL
)
GO
----------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE TYPE TYPE_PAYOUTCURVE_IMPORT AS TABLE 
(
	PayoutCurve varchar(Max),
	SlabId int,
	 Min   decimal(18,3),
	Max   decimal(18,3) ,
	Multiplier decimal(18,12),
	MultiplierType Varchar(100)
)
GO
------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE TYPE TYPE_INCENTIVEPERCENTAGE_IMPORT AS TABLE 
(
	PayoutPercentage varchar(Max),
	FrequencyDetails varchar(MAX),
	Percentage decimal(18,6)
)
GO
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE TYPE [dbo].[TYPE_ROLE_IMPORT] AS TABLE(
	[Country] [varchar](max) NULL,
	[BusinessUnit] [varchar](max) NULL,
	[Role] [varchar](max) NULL,
	[RoleTargetIncentive] [decimal](18, 6) NULL,
	[Year] [int] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL
)
GO
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/****** Object:  UserDefinedTableType [dbo].[TYPE_ROLEMEASURE_IMPORT]    Script Date: 1/7/2019 1:25:45 PM ******/
CREATE TYPE [dbo].[TYPE_ROLEMEASURE_IMPORT] AS TABLE(
	[Country] [varchar](max) NULL,
	[BusinessUnit] [varchar](max) NULL,
	[Role] [varchar](max) NULL,
	[Measure] [varchar](max) NULL,
	[Frequency] [varchar](max) NULL,
	[MeasureWeightage] [int] NULL,
	[PayoutCurve] [varchar](max) NULL,
	[PayoutType] [varchar](max) NULL,
	[MeasureSequence] [int] NULL,
	[IncentivePayout] [varchar](max) NULL,
	[Modifier] [varchar](100) NULL,
	[Goal] [varchar](10) NULL,
	[KPIModifierName] [varchar](100) NULL,
	[KPIModifierValue] [varchar](100) NULL,
	[IsKPI] [varchar](10) NULL,
	[Year] [int] NULL
)
GO



------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/****** Object:  UserDefinedTableType [dbo].[TYPE_USERDATAIMPORT]    Script Date: 22-02-2018 16:51:20 ******/
CREATE TYPE [dbo].[TYPE_USERDATAIMPORT] AS TABLE(
	[Country] [varchar](max) NULL,
	[BusinessUnit] [varchar](max) NULL,
	[Role] [varchar](max) NULL,
	[UserADID] [varchar](max) NULL,
	[UserName] [varchar](max) NULL,
	[EmployeeID] [varchar](max) NULL,
	[SIPPlan] [varchar](max) NULL
)
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE TYPE [dbo].[TYPE_SALARY_IMPORT] AS TABLE(
	[UserId] [nvarchar](200) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date]  NULL,
	[Salary] [nvarchar](200) NOT NULL
)
GO
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/****** Object:  UserDefinedTableType [dbo].[TYPE_DATA_IMPORT_PAYOUTHISTORY]    Script Date: 26-02-2018 17:11:38 ******/
CREATE TYPE [dbo].[TYPE_DATA_IMPORT_PAYOUTHISTORY] AS TABLE(
	[Year] [nvarchar](100) NULL,
	[Plan] [nvarchar](100) NULL,
	[UserADID] [nvarchar](100) NULL,
	[MeasureName] [nvarchar](100) NULL,
	[Freq] [varchar](100) NULL,
	[Q1] [nvarchar](max) NULL,
	[Q2] [nvarchar](max) NULL,
	[Q3] [nvarchar](max) NULL,
	[Q4] [nvarchar](max) NULL
)
GO
------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/****** Object:  UserDefinedTableType [dbo].[TYPE_DATA_IMPORT_GOALAMOUNT]    Script Date: 16-03-2018 11:59:19 ******/
CREATE TYPE [dbo].[TYPE_DATA_IMPORT_GOALAMOUNT] AS TABLE(
	[Country] [nvarchar](max) NOT NULL,
	[BusinessUnit] [nvarchar](max) NOT NULL,
	[Role] [nvarchar](max) NOT NULL,
	[UserADID] [nvarchar](max) NOT NULL,
	[MeasureSequence] [int] NOT NULL,
	[Q1] [nvarchar](max) NOT NULL,
	[Q2] [nvarchar](max) NOT NULL,
	[Q3] [nvarchar](max) NOT NULL,
	[Q4] [nvarchar](max) NOT NULL,
	[H1] [nvarchar](max) NOT NULL,
	[H2] [nvarchar](max) NOT NULL,
	[Annual] [nvarchar](max) NOT NULL
)
GO
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


