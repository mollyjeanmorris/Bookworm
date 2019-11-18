USE [BookWorm]
GO

/****** Object:  Table [dbo].[Books]    Script Date: 11/18/2019 9:40:33 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Books](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AuthorId] [int] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[StarRating] [char](1) NOT NULL,
	[BookSeriesId] [int] NULL,
	[CoverArt] [nvarchar](max) NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Authors] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Authors] ([Id])
GO

ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Authors]
GO

ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_BookSeries] FOREIGN KEY([BookSeriesId])
REFERENCES [dbo].[BookSeries] ([Id])
GO

ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_BookSeries]
GO


