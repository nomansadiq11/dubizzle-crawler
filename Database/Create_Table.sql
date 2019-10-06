USE [dubizzleCrawler]
GO

/****** Object:  Table [dbo].[data]    Script Date: 10/6/2019 12:15:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[data](
	[Title] [varchar](2000) NULL,
	[Price] [varchar](2000) NULL,
	[Location] [varchar](2000) NULL,
	[link] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


