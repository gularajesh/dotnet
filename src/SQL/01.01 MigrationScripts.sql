/****** Object:  TABLE [dbo].[PayoutCurveDetail]    Script Date: 14-02-2018 ******/
IF NOT EXISTS  (select * from sys.columns
       Where NAme =N'Max'
	   AND Object_ID = object_ID(N'PayoutCurveDetail'))
BEGIN
Alter table PayoutCurveDetail alter column [Max]  decimal(18,2)
END
IF NOT EXISTS  (select * from sys.columns
       Where NAme =N'Min'
	   AND Object_ID = object_ID(N'PayoutCurveDetail'))
BEGIN
Alter table PayoutCurveDetail alter column [Min]  decimal(18,2)
END
IF NOT  EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'EmployeeID'
          AND Object_ID = Object_ID(N'[dbo].[User]'))
		 
BEGIN
     ALTER TABLE [user]   ADD  EmployeeID Nvarchar(MAX) NULL
END

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserTargetSimulationModifierDetail_UserTargetSimulation]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserTargetSimulationModifierDetail]'))
ALTER TABLE [dbo].[UserTargetSimulationModifierDetail]  DROP CONSTRAINT [FK_UserTargetSimulationModifierDetail_UserTargetSimulation]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserTargetSimulationModifierDetail_UserTargetSimulation]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserTargetSimulationModifierDetail]'))
ALTER TABLE [dbo].[UserTargetSimulationModifierDetail] DROP CONSTRAINT [FK_UserTargetSimulationModifierDetail_UserTargetSimulation]
GO
IF EXISTS (SELECT * FROM sys.tables 
          WHERE name = N'UserTargetSimulationModifierDetail'
        )
		BEGIN
DROP TABLE [dbo].[UserTargetSimulationModifierDetail]
END


/****** Object:  Table [dbo].[UserTargetSimulationDetail]    Script Date: 19-02-2018 15:11:06 ******/
IF EXISTS (SELECT * FROM sys.tables 
          WHERE name = N'UserTargetSimulationDetail'
        )
		BEGIN
DROP TABLE [dbo].[UserTargetSimulationDetail]
END
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserTargetSimulation_UserTarget]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserTargetSimulation]'))
ALTER TABLE [dbo].[UserTargetSimulation] DROP CONSTRAINT [FK_UserTargetSimulation_UserTarget]
GO




/****** Object:  Table [dbo].[UserTargetSimulation]    Script Date: 19-02-2018 15:10:11 ******/
IF EXISTS (SELECT * FROM sys.tables 
          WHERE name = N'UserTargetSimulation'
         )
		 BEGIN

DROP TABLE [dbo].[UserTargetSimulation]
END
GO

/****** Object:  Table [dbo].[UserSimulation]    Script Date: 19-02-2018 17:54:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserSimulation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserSimulation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserTargetId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[TotalPayout] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_UserSimulation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserSimulationMeasureDetail]    Script Date: 19-02-2018 17:54:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserSimulationMeasureDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserSimulationMeasureDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserSimulationId] [int] NOT NULL,
	[PlanMeasureId] [int] NOT NULL,
	[Achievement] [decimal](18, 2) NOT NULL,
	[AchievementPercentage] [decimal](18, 2) NOT NULL,
	[PayoutAmount] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_UserSimulationMeasureDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserSimulationMeasureFrequencyDetail]    Script Date: 19-02-2018 17:54:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserSimulationMeasureFrequencyDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserSimulationMeasureFrequencyDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserSimulationMeasureDetailId] [int] NOT NULL,
	[FrequencyDetailId] [int] NOT NULL,
	[Achievement] [decimal](18, 2) NOT NULL,
	[AchievementPercentage] [decimal](18, 2) NOT NULL,
	[AdditionalFields] [xml] NULL,
	[PayoutAmount] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_UserSimulationMeasureFrequencyDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserSimulationModifierDetail]    Script Date: 19-02-2018 17:54:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserSimulationModifierDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserSimulationModifierDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserSimulationId] [int] NOT NULL,
	[PlanModifierId] [int] NOT NULL,
	[ModifierFieldValue] [int] NOT NULL,
	[ModifierPayout] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_UserSimulationModifierDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserSimulation_UserTarget]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserSimulation]'))
ALTER TABLE [dbo].[UserSimulation]  WITH CHECK ADD  CONSTRAINT [FK_UserSimulation_UserTarget] FOREIGN KEY([UserTargetId])
REFERENCES [dbo].[UserTarget] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserSimulation_UserTarget]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserSimulation]'))
ALTER TABLE [dbo].[UserSimulation] CHECK CONSTRAINT [FK_UserSimulation_UserTarget]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserSimulationMeasureDetail_PlanMeasure]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserSimulationMeasureDetail]'))
ALTER TABLE [dbo].[UserSimulationMeasureDetail]  WITH CHECK ADD  CONSTRAINT [FK_UserSimulationMeasureDetail_PlanMeasure] FOREIGN KEY([PlanMeasureId])
REFERENCES [dbo].[PlanMeasure] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserSimulationMeasureDetail_PlanMeasure]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserSimulationMeasureDetail]'))
ALTER TABLE [dbo].[UserSimulationMeasureDetail] CHECK CONSTRAINT [FK_UserSimulationMeasureDetail_PlanMeasure]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserSimulationMeasureDetail_UserSimulation]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserSimulationMeasureDetail]'))
ALTER TABLE [dbo].[UserSimulationMeasureDetail]  WITH CHECK ADD  CONSTRAINT [FK_UserSimulationMeasureDetail_UserSimulation] FOREIGN KEY([UserSimulationId])
REFERENCES [dbo].[UserSimulation] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserSimulationMeasureDetail_UserSimulation]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserSimulationMeasureDetail]'))
ALTER TABLE [dbo].[UserSimulationMeasureDetail] CHECK CONSTRAINT [FK_UserSimulationMeasureDetail_UserSimulation]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserSimulationMeasureFrequencyDetail_FrequencyDetail]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserSimulationMeasureFrequencyDetail]'))
ALTER TABLE [dbo].[UserSimulationMeasureFrequencyDetail]  WITH CHECK ADD  CONSTRAINT [FK_UserSimulationMeasureFrequencyDetail_FrequencyDetail] FOREIGN KEY([FrequencyDetailId])
REFERENCES [dbo].[FrequencyDetail] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserSimulationMeasureFrequencyDetail_FrequencyDetail]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserSimulationMeasureFrequencyDetail]'))
ALTER TABLE [dbo].[UserSimulationMeasureFrequencyDetail] CHECK CONSTRAINT [FK_UserSimulationMeasureFrequencyDetail_FrequencyDetail]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserSimulationMeasureFrequencyDetail_UserSimulationMeasureDetail]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserSimulationMeasureFrequencyDetail]'))
ALTER TABLE [dbo].[UserSimulationMeasureFrequencyDetail]  WITH CHECK ADD  CONSTRAINT [FK_UserSimulationMeasureFrequencyDetail_UserSimulationMeasureDetail] FOREIGN KEY([UserSimulationMeasureDetailId])
REFERENCES [dbo].[UserSimulationMeasureDetail] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserSimulationMeasureFrequencyDetail_UserSimulationMeasureDetail]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserSimulationMeasureFrequencyDetail]'))
ALTER TABLE [dbo].[UserSimulationMeasureFrequencyDetail] CHECK CONSTRAINT [FK_UserSimulationMeasureFrequencyDetail_UserSimulationMeasureDetail]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserSimulationModifierDetail_PlanModifier]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserSimulationModifierDetail]'))
ALTER TABLE [dbo].[UserSimulationModifierDetail]  WITH CHECK ADD  CONSTRAINT [FK_UserSimulationModifierDetail_PlanModifier] FOREIGN KEY([PlanModifierId])
REFERENCES [dbo].[PlanModifier] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserSimulationModifierDetail_PlanModifier]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserSimulationModifierDetail]'))
ALTER TABLE [dbo].[UserSimulationModifierDetail] CHECK CONSTRAINT [FK_UserSimulationModifierDetail_PlanModifier]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserSimulationModifierDetail_UserSimulation]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserSimulationModifierDetail]'))
ALTER TABLE [dbo].[UserSimulationModifierDetail]  WITH CHECK ADD  CONSTRAINT [FK_UserSimulationModifierDetail_UserSimulation] FOREIGN KEY([UserSimulationId])
REFERENCES [dbo].[UserSimulation] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserSimulationModifierDetail_UserSimulation]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserSimulationModifierDetail]'))
ALTER TABLE [dbo].[UserSimulationModifierDetail] CHECK CONSTRAINT [FK_UserSimulationModifierDetail_UserSimulation]
GO
IF NOT  EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'HasGoal'
          AND Object_ID = Object_ID(N'[dbo].[PlanMeasure]'))
		 
BEGIN
     ALTER TABLE [PlanMeasure]   ADD  HasGoal bit NULL
END

IF EXISTS(Select * from Sys.default_constraints
          Where Name =N'HAS_GOAL'
		  AND Object_ID = Object_ID(N'[dbo].[PlanMeasure]'))
BEGIN 
      ALTER TABLE PlanMeasure ADD  CONSTRAINT HAS_GOAL  
   DEFAULT 0 FOR [HasGoal] ;
END
/****** Object:  TABLE [dbo].[PayoutCurveDetail]    Script Date: 21-02-2018 ******/

IF EXISTS(Select * from Sys.columns
          Where Name =N'Multiplier'
		  AND Object_ID = Object_ID(N'[dbo].[PayoutCurveDetail]'))
BEGIN 
     Alter Table PayoutCurveDetail alter column Multiplier decimal(18,10);
END



IF NOT  EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'DataType'
          AND Object_ID = Object_ID(N'[dbo].[PlanMeasure]'))
		 
BEGIN
     ALTER TABLE PlanMeasure   ADD  DataType nvarchar(100)
END

IF NOT  EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'ValueType'
          AND Object_ID = Object_ID(N'[dbo].[PlanMeasure]'))
		 
BEGIN
     ALTER TABLE PlanMeasure   ADD  ValueType nvarchar(100)
END
IF NOT  EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'DataType'
          AND Object_ID = Object_ID(N'[dbo].[PlanModifier]'))
		 
BEGIN
     ALTER TABLE PlanModifier   ADD  DataType nvarchar(100)
END

IF NOT  EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'ValueType'
          AND Object_ID = Object_ID(N'[dbo].[PlanModifier]'))
		 
BEGIN
     ALTER TABLE PlanModifier   ADD  ValueType nvarchar(100)
END
/****** Object:  TABLE [dbo].[ServerLog]    Script Date: 21-02-2018 ******/
IF NOT  EXISTS(SELECT * FROM sys.columns 
         WHERE Name = N'ReferenceId'
         AND Object_ID = Object_ID(N'[dbo].[ServerLog]'))

BEGIN
    ALTER TABLE ServerLog   ADD  ReferenceId varchar(MAX) NULL
END

IF NOT  EXISTS(SELECT * FROM sys.columns 
         WHERE Name = N'API'
         AND Object_ID = Object_ID(N'[dbo].[ServerLog]'))

BEGIN
    ALTER TABLE ServerLog   ADD  API varchar(MAX) NULL
END

IF NOT  EXISTS(SELECT * FROM sys.columns 
         WHERE Name = N'Method'
         AND Object_ID = Object_ID(N'[dbo].[ServerLog]'))

BEGIN
    ALTER TABLE ServerLog   ADD  Method varchar(MAX) NULL
END

IF EXISTS(Select * from Sys.Columns
          Where Name =N'Multiplier'
		  AND Object_ID = Object_ID(N'[dbo].[PayoutCurveDetail]'))

BEGIN
      Alter table PayoutCurveDetail alter column Multiplier  decimal(18,12)
END
GO


IF EXISTS(Select * from Sys.Columns
          Where Name =N'HasGoal'
		  AND Object_ID = Object_ID(N'[dbo].[PlanMeasure]'))
BEGIN 
       ALTER TABLE [PlanMeasure]   ALter COLUMN  HasGoal bit NOT  NULL
END
Go
/****** Object:  Table [dbo].[UserPayoutHistory]    Script Date: 27-02-2018 11:05:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS(select * from Sys.objects where OBJECT_ID=object_Id(N'dbo.[UserPayoutHistory]') And TYPE in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserPayoutHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[PlanName] [nvarchar](max) NOT NULL,
	[MeasureName] [nvarchar](max) NOT NULL,
	[FrequencyId] [int] NOT NULL,
	[Quarter1] [decimal](30,18) NOT NULL,
	[Quarter2] [decimal](30,18) NOT NULL,
	[Quarter3] [decimal](30,18) NOT NULL,
	[Quarter4] [decimal](30,18) NOT NULL,
 CONSTRAINT [PK_UserPayoutHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------
IF NOT EXISTS (select * from Sys.foreign_Keys where object_Id=OBJECT_ID(N'[dbo].[FK_UserPayoutHistory_Frequency]') AND parent_object_id=OBJECT_ID(N'[UserPayoutHistory]'))
ALTER TABLE [dbo].[UserPayoutHistory]  WITH CHECK ADD  CONSTRAINT [FK_UserPayoutHistory_Frequency] FOREIGN KEY([FrequencyId])
REFERENCES [dbo].[Frequency] ([Id])
GO

IF EXISTS (Select * from Sys.foreign_Keys Where object_Id=OBJECT_ID(N'[dbo].[FK_UserPayoutHistory_Frequency]') AND parent_object_id=OBJECT_ID(N'[UserPayoutHistory]'))
ALTER TABLE [dbo].[UserPayoutHistory] CHECK CONSTRAINT [FK_UserPayoutHistory_Frequency]
GO
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/****** Object:  Table [dbo].[UserSalaryDetail]    Script Date: 2/22/2018 8:08:32 PM ******/
IF  EXISTS  (select * from sys.columns
       Where NAme =N'Salary'
	   AND Object_ID = object_ID(N'UserSalaryDetail'))
BEGIN
Alter Table UserSalaryDetail alter column Salary Nvarchar(MAX) NULL
END
GO
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/****** Object:  Table [dbo].[UserSimulationMeasureFrequencyDetail]    Script Date: 2/22/2018 8:08:32 PM ******/

IF NOT  EXISTS  (select * from sys.columns
       Where NAme =N'CumulativePercentage'
	   AND Object_ID = object_ID(N'UserSimulationMeasureFrequencyDetail'))
BEGIN
Alter Table UserSimulationMeasureFrequencyDetail  ADD CumulativePercentage Decimal(18,2) NULL
END
GO
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

IF NOT  EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'IsValue'
          AND Object_ID = Object_ID(N'[dbo].[UserSimulationMeasureFrequencyDetail]'))
		 
BEGIN
     ALTER TABLE [UserSimulationMeasureFrequencyDetail]   Add  IsValue bit NULL
END
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
IF  EXISTS  (select * from sys.columns
       Where NAme =N'Incentive'
	   AND Object_ID = object_ID(N'UserTargetDetail'))
BEGIN
Alter Table UserTargetDetail alter column Incentive Nvarchar(MAX) NULL
END
GO

IF  EXISTS  (select * from sys.columns
       Where NAme =N'Goal'
	   AND Object_ID = object_ID(N'UserTargetDetail'))
BEGIN
Alter Table UserTargetDetail alter column Goal Nvarchar(MAX) NULL
END
GO

IF  EXISTS  (select * from sys.columns
       Where NAme =N'TotalPayout'
	   AND Object_ID = object_ID(N'UserSimulation'))
BEGIN
Alter Table UserSimulation alter column TotalPayout Nvarchar(MAX) NULL
END
GO

IF  EXISTS  (select * from sys.columns
       Where NAme =N'Achievement'
	   AND Object_ID = object_ID(N'UserSimulationMeasureDetail'))
BEGIN
Alter Table UserSimulationMeasureDetail alter column Achievement Nvarchar(MAX) NULL
END
GO

IF  EXISTS  (select * from sys.columns
       Where NAme =N'PayoutAmount'
	   AND Object_ID = object_ID(N'UserSimulationMeasureDetail'))
BEGIN
Alter Table UserSimulationMeasureDetail alter column PayoutAmount Nvarchar(MAX) NULL
END
GO

IF  EXISTS  (select * from sys.columns
       Where NAme =N'Achievement'
	   AND Object_ID = object_ID(N'UserSimulationMeasureFrequencyDetail'))
BEGIN
Alter Table UserSimulationMeasureFrequencyDetail alter column Achievement Nvarchar(MAX) NULL
END
GO

IF  EXISTS  (select * from sys.columns
       Where NAme =N'PayoutAmount'
	   AND Object_ID = object_ID(N'UserSimulationMeasureFrequencyDetail'))
BEGIN
Alter Table UserSimulationMeasureFrequencyDetail alter column PayoutAmount Nvarchar(MAX) NULL
END
GO

IF  EXISTS  (select * from sys.columns
       Where NAme =N'ModifierPayout'
	   AND Object_ID = object_ID(N'UserSimulationModifierDetail'))
BEGIN
Alter Table UserSimulationModifierDetail alter column ModifierPayout Nvarchar(MAX) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Permission]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Permission](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserPermission]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserPermission](
	[UserID] [int] NOT NULL,
	[PermissionID] [int] NOT NULL,
 CONSTRAINT [PK_UserPermission] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[PermissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserPermission_Permission]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPermission]'))
ALTER TABLE [dbo].[UserPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserPermission_Permission] FOREIGN KEY([PermissionID])
REFERENCES [dbo].[Permission] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserPermission_Permission]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPermission]'))
ALTER TABLE [dbo].[UserPermission] CHECK CONSTRAINT [FK_UserPermission_Permission]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserPermission_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPermission]'))
ALTER TABLE [dbo].[UserPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserPermission_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserPermission_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPermission]'))
ALTER TABLE [dbo].[UserPermission] CHECK CONSTRAINT [FK_UserPermission_User]
GO

-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
IF NOT EXISTS (select * from sys.columns
                where NAME='IsKPI'
				and OBJECT_id=OBJECT_ID(N'[dbo].[PlanMeasure]'))
BEGIN
ALTER TABLE [dbo].[PlanMeasure] ADD IsKPI bit not null DEFAULT 0
END
GO
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
IF  EXISTS  (select * from sys.columns
       Where NAme =N'Quarter1'
	   AND Object_ID = object_ID(N'UserPayoutHistory'))
BEGIN
Alter Table UserPayoutHistory alter column Quarter1 Nvarchar(MAX) NULL
END
GO
IF  EXISTS  (select * from sys.columns
       Where NAme =N'Quarter2'
	   AND Object_ID = object_ID(N'UserPayoutHistory'))
BEGIN
Alter Table UserPayoutHistory alter column Quarter2 Nvarchar(MAX) NULL
END
GO
IF  EXISTS  (select * from sys.columns
       Where NAme =N'Quarter3'
	   AND Object_ID = object_ID(N'UserPayoutHistory'))
BEGIN
Alter Table UserPayoutHistory alter column Quarter3 Nvarchar(MAX) NULL
END
GO
IF  EXISTS  (select * from sys.columns
       Where NAme =N'Quarter4'
	   AND Object_ID = object_ID(N'UserPayoutHistory'))
BEGIN
Alter Table UserPayoutHistory alter column Quarter4 Nvarchar(MAX) NULL
END
GO