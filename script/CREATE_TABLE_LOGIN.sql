SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Login](
	[USER_ID] [nvarchar](50) NOT NULL,
	[USERNAME] [nvarchar](100) NOT NULL,
	[FIRST_NAME] [nvarchar](255) NULL,
	[LAST_NAME] [nvarchar](255) NULL,
	[STATUS] [bit] NOT NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[USER_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


