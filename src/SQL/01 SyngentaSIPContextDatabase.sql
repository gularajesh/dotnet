
/****** Object:  Table [dbo].[BusinessUnit]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BusinessUnit]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BusinessUnit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_BusinessUnit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[City]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[City]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[City](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Country]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Country]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Country](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[TerritoryId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[CurrencyId] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedBy] [int] NOT NULL,
	[UpdatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Currency]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Currency]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Currency](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Frequency]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Frequency]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Frequency](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Frequency] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[FrequencyDetail]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FrequencyDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FrequencyDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[FrequencyId] [int] NOT NULL,
 CONSTRAINT [PK_FrequencyDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Language]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Language]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Language](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PayoutCurve]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayoutCurve]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PayoutCurve](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[PayoutCurveTypeId] [int] NOT NULL,
 CONSTRAINT [PK_PayoutCurve] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PayoutCurveDetail]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayoutCurveDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PayoutCurveDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PayoutCurveId] [int] NOT NULL,
	[SlabId] [int] NOT NULL,
	[Min] [decimal](18, 2) NULL,
	[Max] [decimal](18, 2) NULL,
	[Multiplier] [decimal](18, 12) NULL,
 CONSTRAINT [PK_PayoutCurveDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PayoutCurveType]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayoutCurveType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PayoutCurveType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_PayoutCurveType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PayoutPercentage]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayoutPercentage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PayoutPercentage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PayoutPercentage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PayoutPercentageDetail]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayoutPercentageDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PayoutPercentageDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PayoutPercentageId] [int] NOT NULL,
	[FrequencyDetailId] [int] NOT NULL,
	[PayoutPercentage] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_PayoutPercentageDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PayoutType]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayoutType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PayoutType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PayoutType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Plan]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Plan]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Plan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[RoleId] [int] NOT NULL,
	[TargetIncentivePercentage] [decimal](18, 5) NOT NULL,
	[Status] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Plan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PlanMeasure]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PlanMeasure]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PlanMeasure](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PlanId] [int] NOT NULL,
	[Sequence] [int] NOT NULL,
	[MeasureName] [varchar](100) NOT NULL,
	[FrequencyId] [int] NOT NULL,
	[PayoutTypeId] [int] NOT NULL,
	[PayoutPercentageId] [int] NOT NULL,
	[PayoutCurveId] [int] NULL,
	[MeasureWeightage] [int] NOT NULL,
	[HasGoal] [bit] NOT NULL,
	[DataType] [nvarchar](100) NULL,
	[ValueType] [nvarchar](100) NULL,
 CONSTRAINT [PK_PlanMeasure] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PlanModifier]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PlanModifier]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PlanModifier](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PlanId] [int] NOT NULL,
	[ModifierName] [varchar](500) NOT NULL,
	[PayoutCurveId] [int] NOT NULL,
	[DataType] [nvarchar](100) NULL,
	[ValueType] [nvarchar](100) NULL,
 CONSTRAINT [PK_PlanModifier] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Region]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Region]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Region](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Region] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Role]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[CountryId] [int] NOT NULL,
	[BusinessUnitId] [int] NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ServerLog]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServerLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ServerLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Thread] [nvarchar](max) NULL,
	[Level] [nvarchar](max) NULL,
	[Logger] [nvarchar](max) NULL,
	[Message] [nvarchar](max) NULL,
	[Exception] [nvarchar](max) NULL,
	[ReferenceId] [varchar](max) NULL,
	[API] [varchar](max) NULL,
	[Method] [varchar](max) NULL,
 CONSTRAINT [PK_dbo.ServerLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Territory]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Territory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Territory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[RegionId] [int] NOT NULL,
 CONSTRAINT [PK_Territory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[User]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoginID] [nvarchar](200) NOT NULL,
	[UserName] [nvarchar](200) NOT NULL,
	[CountryId] [int] NOT NULL,
	[BusinessUnitId] [int] NOT NULL,
	[CityId] [int] NULL,
	[SaveSimulation] [bit] NOT NULL,
	[LanguageId] [int] NULL,
	[LastLogin] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[EmployeeID] [nvarchar](max) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserPayoutHistory]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserPayoutHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserPayoutHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[PlanName] [nvarchar](max) NOT NULL,
	[MeasureName] [nvarchar](max) NOT NULL,
	[FrequencyId] [int] NOT NULL,
	[Quarter1] [decimal](30, 18) NOT NULL,
	[Quarter2] [decimal](30, 18) NOT NULL,
	[Quarter3] [decimal](30, 18) NOT NULL,
	[Quarter4] [decimal](30, 18) NOT NULL,
 CONSTRAINT [PK_UserPayoutHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserRoleDetail]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRoleDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserRoleDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NULL,
 CONSTRAINT [PK_UserRoleDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserSalaryDetail]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserSalaryDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserSalaryDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NULL,
	[Salary] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserSalaryDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserSimulation]    Script Date: 08-03-2018 15:12:51 ******/
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
/****** Object:  Table [dbo].[UserSimulationMeasureDetail]    Script Date: 08-03-2018 15:12:51 ******/
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
/****** Object:  Table [dbo].[UserSimulationMeasureFrequencyDetail]    Script Date: 08-03-2018 15:12:51 ******/
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
	[CumulativePercentage] [decimal](18, 2) NULL,
 CONSTRAINT [PK_UserSimulationMeasureFrequencyDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserSimulationModifierDetail]    Script Date: 08-03-2018 15:12:51 ******/
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
/****** Object:  Table [dbo].[UserTarget]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserTarget]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserTarget](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[PlanId] [int] NOT NULL,
 CONSTRAINT [PK_UserTarget] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserTargetDetail]    Script Date: 08-03-2018 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserTargetDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserTargetDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserTargetId] [int] NOT NULL,
	[PlanMeasureId] [int] NOT NULL,
	[FrequencyDetailId] [int] NOT NULL,
	[Incentive] [decimal](18, 2) NOT NULL,
	[Goal] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_UserTargetDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Country_Currency]') AND parent_object_id = OBJECT_ID(N'[dbo].[Country]'))
ALTER TABLE [dbo].[Country]  WITH CHECK ADD  CONSTRAINT [FK_Country_Currency] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Currency] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Country_Currency]') AND parent_object_id = OBJECT_ID(N'[dbo].[Country]'))
ALTER TABLE [dbo].[Country] CHECK CONSTRAINT [FK_Country_Currency]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Country_Language]') AND parent_object_id = OBJECT_ID(N'[dbo].[Country]'))
ALTER TABLE [dbo].[Country]  WITH CHECK ADD  CONSTRAINT [FK_Country_Language] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Country_Language]') AND parent_object_id = OBJECT_ID(N'[dbo].[Country]'))
ALTER TABLE [dbo].[Country] CHECK CONSTRAINT [FK_Country_Language]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Country_Territory]') AND parent_object_id = OBJECT_ID(N'[dbo].[Country]'))
ALTER TABLE [dbo].[Country]  WITH CHECK ADD  CONSTRAINT [FK_Country_Territory] FOREIGN KEY([TerritoryId])
REFERENCES [dbo].[Territory] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Country_Territory]') AND parent_object_id = OBJECT_ID(N'[dbo].[Country]'))
ALTER TABLE [dbo].[Country] CHECK CONSTRAINT [FK_Country_Territory]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FrequencyDetail_Frequency]') AND parent_object_id = OBJECT_ID(N'[dbo].[FrequencyDetail]'))
ALTER TABLE [dbo].[FrequencyDetail]  WITH CHECK ADD  CONSTRAINT [FK_FrequencyDetail_Frequency] FOREIGN KEY([FrequencyId])
REFERENCES [dbo].[Frequency] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FrequencyDetail_Frequency]') AND parent_object_id = OBJECT_ID(N'[dbo].[FrequencyDetail]'))
ALTER TABLE [dbo].[FrequencyDetail] CHECK CONSTRAINT [FK_FrequencyDetail_Frequency]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PayoutCurveDetail_PayoutCurve]') AND parent_object_id = OBJECT_ID(N'[dbo].[PayoutCurveDetail]'))
ALTER TABLE [dbo].[PayoutCurveDetail]  WITH CHECK ADD  CONSTRAINT [FK_PayoutCurveDetail_PayoutCurve] FOREIGN KEY([PayoutCurveId])
REFERENCES [dbo].[PayoutCurve] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PayoutCurveDetail_PayoutCurve]') AND parent_object_id = OBJECT_ID(N'[dbo].[PayoutCurveDetail]'))
ALTER TABLE [dbo].[PayoutCurveDetail] CHECK CONSTRAINT [FK_PayoutCurveDetail_PayoutCurve]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PayoutPercentageDetail_FrequencyDetail]') AND parent_object_id = OBJECT_ID(N'[dbo].[PayoutPercentageDetail]'))
ALTER TABLE [dbo].[PayoutPercentageDetail]  WITH CHECK ADD  CONSTRAINT [FK_PayoutPercentageDetail_FrequencyDetail] FOREIGN KEY([FrequencyDetailId])
REFERENCES [dbo].[FrequencyDetail] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PayoutPercentageDetail_FrequencyDetail]') AND parent_object_id = OBJECT_ID(N'[dbo].[PayoutPercentageDetail]'))
ALTER TABLE [dbo].[PayoutPercentageDetail] CHECK CONSTRAINT [FK_PayoutPercentageDetail_FrequencyDetail]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PayoutPercentageDetail_PayoutPercentage]') AND parent_object_id = OBJECT_ID(N'[dbo].[PayoutPercentageDetail]'))
ALTER TABLE [dbo].[PayoutPercentageDetail]  WITH CHECK ADD  CONSTRAINT [FK_PayoutPercentageDetail_PayoutPercentage] FOREIGN KEY([PayoutPercentageId])
REFERENCES [dbo].[PayoutPercentage] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PayoutPercentageDetail_PayoutPercentage]') AND parent_object_id = OBJECT_ID(N'[dbo].[PayoutPercentageDetail]'))
ALTER TABLE [dbo].[PayoutPercentageDetail] CHECK CONSTRAINT [FK_PayoutPercentageDetail_PayoutPercentage]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Plan_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[Plan]'))
ALTER TABLE [dbo].[Plan]  WITH CHECK ADD  CONSTRAINT [FK_Plan_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Plan_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[Plan]'))
ALTER TABLE [dbo].[Plan] CHECK CONSTRAINT [FK_Plan_Role]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanMeasure_Frequency]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanMeasure]'))
ALTER TABLE [dbo].[PlanMeasure]  WITH CHECK ADD  CONSTRAINT [FK_PlanMeasure_Frequency] FOREIGN KEY([FrequencyId])
REFERENCES [dbo].[Frequency] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanMeasure_Frequency]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanMeasure]'))
ALTER TABLE [dbo].[PlanMeasure] CHECK CONSTRAINT [FK_PlanMeasure_Frequency]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanMeasure_PayoutCurve]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanMeasure]'))
ALTER TABLE [dbo].[PlanMeasure]  WITH CHECK ADD  CONSTRAINT [FK_PlanMeasure_PayoutCurve] FOREIGN KEY([PayoutCurveId])
REFERENCES [dbo].[PayoutCurve] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanMeasure_PayoutCurve]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanMeasure]'))
ALTER TABLE [dbo].[PlanMeasure] CHECK CONSTRAINT [FK_PlanMeasure_PayoutCurve]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanMeasure_PayoutPercentage]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanMeasure]'))
ALTER TABLE [dbo].[PlanMeasure]  WITH CHECK ADD  CONSTRAINT [FK_PlanMeasure_PayoutPercentage] FOREIGN KEY([PayoutPercentageId])
REFERENCES [dbo].[PayoutPercentage] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanMeasure_PayoutPercentage]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanMeasure]'))
ALTER TABLE [dbo].[PlanMeasure] CHECK CONSTRAINT [FK_PlanMeasure_PayoutPercentage]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanMeasure_PayoutType]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanMeasure]'))
ALTER TABLE [dbo].[PlanMeasure]  WITH CHECK ADD  CONSTRAINT [FK_PlanMeasure_PayoutType] FOREIGN KEY([PayoutTypeId])
REFERENCES [dbo].[PayoutType] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanMeasure_PayoutType]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanMeasure]'))
ALTER TABLE [dbo].[PlanMeasure] CHECK CONSTRAINT [FK_PlanMeasure_PayoutType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanMeasure_Plan]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanMeasure]'))
ALTER TABLE [dbo].[PlanMeasure]  WITH CHECK ADD  CONSTRAINT [FK_PlanMeasure_Plan] FOREIGN KEY([PlanId])
REFERENCES [dbo].[Plan] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanMeasure_Plan]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanMeasure]'))
ALTER TABLE [dbo].[PlanMeasure] CHECK CONSTRAINT [FK_PlanMeasure_Plan]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanModifier_PayoutCurve]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanModifier]'))
ALTER TABLE [dbo].[PlanModifier]  WITH CHECK ADD  CONSTRAINT [FK_PlanModifier_PayoutCurve] FOREIGN KEY([PayoutCurveId])
REFERENCES [dbo].[PayoutCurve] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanModifier_PayoutCurve]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanModifier]'))
ALTER TABLE [dbo].[PlanModifier] CHECK CONSTRAINT [FK_PlanModifier_PayoutCurve]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanModifier_Plan]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanModifier]'))
ALTER TABLE [dbo].[PlanModifier]  WITH CHECK ADD  CONSTRAINT [FK_PlanModifier_Plan] FOREIGN KEY([PlanId])
REFERENCES [dbo].[Plan] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanModifier_Plan]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanModifier]'))
ALTER TABLE [dbo].[PlanModifier] CHECK CONSTRAINT [FK_PlanModifier_Plan]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Role_BusinessUnit]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
ALTER TABLE [dbo].[Role]  WITH CHECK ADD  CONSTRAINT [FK_Role_BusinessUnit] FOREIGN KEY([BusinessUnitId])
REFERENCES [dbo].[BusinessUnit] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Role_BusinessUnit]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
ALTER TABLE [dbo].[Role] CHECK CONSTRAINT [FK_Role_BusinessUnit]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Territory_Region]') AND parent_object_id = OBJECT_ID(N'[dbo].[Territory]'))
ALTER TABLE [dbo].[Territory]  WITH CHECK ADD  CONSTRAINT [FK_Territory_Region] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Region] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Territory_Region]') AND parent_object_id = OBJECT_ID(N'[dbo].[Territory]'))
ALTER TABLE [dbo].[Territory] CHECK CONSTRAINT [FK_Territory_Region]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_User_BusinessUnit]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_BusinessUnit] FOREIGN KEY([BusinessUnitId])
REFERENCES [dbo].[BusinessUnit] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_User_BusinessUnit]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_BusinessUnit]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_User_City]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_City] FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_User_City]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_City]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserPayoutHistory_Frequency]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPayoutHistory]'))
ALTER TABLE [dbo].[UserPayoutHistory]  WITH CHECK ADD  CONSTRAINT [FK_UserPayoutHistory_Frequency] FOREIGN KEY([FrequencyId])
REFERENCES [dbo].[Frequency] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserPayoutHistory_Frequency]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPayoutHistory]'))
ALTER TABLE [dbo].[UserPayoutHistory] CHECK CONSTRAINT [FK_UserPayoutHistory_Frequency]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRoleDetail_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRoleDetail]'))
ALTER TABLE [dbo].[UserRoleDetail]  WITH CHECK ADD  CONSTRAINT [FK_UserRoleDetail_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRoleDetail_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRoleDetail]'))
ALTER TABLE [dbo].[UserRoleDetail] CHECK CONSTRAINT [FK_UserRoleDetail_Role]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRoleDetail_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRoleDetail]'))
ALTER TABLE [dbo].[UserRoleDetail]  WITH CHECK ADD  CONSTRAINT [FK_UserRoleDetail_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRoleDetail_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRoleDetail]'))
ALTER TABLE [dbo].[UserRoleDetail] CHECK CONSTRAINT [FK_UserRoleDetail_User]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserSalaryDetail_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserSalaryDetail]'))
ALTER TABLE [dbo].[UserSalaryDetail]  WITH CHECK ADD  CONSTRAINT [FK_UserSalaryDetail_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserSalaryDetail_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserSalaryDetail]'))
ALTER TABLE [dbo].[UserSalaryDetail] CHECK CONSTRAINT [FK_UserSalaryDetail_User]
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
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserTarget_Plan]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserTarget]'))
ALTER TABLE [dbo].[UserTarget]  WITH CHECK ADD  CONSTRAINT [FK_UserTarget_Plan] FOREIGN KEY([PlanId])
REFERENCES [dbo].[Plan] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserTarget_Plan]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserTarget]'))
ALTER TABLE [dbo].[UserTarget] CHECK CONSTRAINT [FK_UserTarget_Plan]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserTarget_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserTarget]'))
ALTER TABLE [dbo].[UserTarget]  WITH CHECK ADD  CONSTRAINT [FK_UserTarget_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserTarget_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserTarget]'))
ALTER TABLE [dbo].[UserTarget] CHECK CONSTRAINT [FK_UserTarget_User]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserTargetDetail_FrequencyDetail]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserTargetDetail]'))
ALTER TABLE [dbo].[UserTargetDetail]  WITH CHECK ADD  CONSTRAINT [FK_UserTargetDetail_FrequencyDetail] FOREIGN KEY([FrequencyDetailId])
REFERENCES [dbo].[FrequencyDetail] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserTargetDetail_FrequencyDetail]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserTargetDetail]'))
ALTER TABLE [dbo].[UserTargetDetail] CHECK CONSTRAINT [FK_UserTargetDetail_FrequencyDetail]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserTargetDetail_PlanMeasure]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserTargetDetail]'))
ALTER TABLE [dbo].[UserTargetDetail]  WITH CHECK ADD  CONSTRAINT [FK_UserTargetDetail_PlanMeasure] FOREIGN KEY([PlanMeasureId])
REFERENCES [dbo].[PlanMeasure] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserTargetDetail_PlanMeasure]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserTargetDetail]'))
ALTER TABLE [dbo].[UserTargetDetail] CHECK CONSTRAINT [FK_UserTargetDetail_PlanMeasure]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserTargetDetail_UserTarget]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserTargetDetail]'))
ALTER TABLE [dbo].[UserTargetDetail]  WITH CHECK ADD  CONSTRAINT [FK_UserTargetDetail_UserTarget] FOREIGN KEY([UserTargetId])
REFERENCES [dbo].[UserTarget] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserTargetDetail_UserTarget]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserTargetDetail]'))
ALTER TABLE [dbo].[UserTargetDetail] CHECK CONSTRAINT [FK_UserTargetDetail_UserTarget]
GO
