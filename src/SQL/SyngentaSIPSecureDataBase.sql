IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ApplicationSetting]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ApplicationSetting](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[KeyName] [varchar](100) NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_ApplicationSetting] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
End
GO