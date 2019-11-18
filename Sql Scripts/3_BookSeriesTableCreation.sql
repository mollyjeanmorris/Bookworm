USE [BookWorm]
GO

/****** Object:  Table [dbo].[BookSeries]    Script Date: 11/18/2019 9:39:56 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BookSeries](
	[Id] [int] NOT NULL,
	[AuthorId] [int] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_BookSeries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[BookSeries]  WITH CHECK ADD  CONSTRAINT [FK_BookSeries_Authors] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Authors] ([Id])
GO

ALTER TABLE [dbo].[BookSeries] CHECK CONSTRAINT [FK_BookSeries_Authors]
GO


