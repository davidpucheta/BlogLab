CREATE TYPE [dbo].[BlogCommentType] AS TABLE
(
	[BlogCommentId] INT NOT NULL,
	[ParentBlogCommentId] INT NULL,
	[BlogId] INT NOT NULL,
	[Content] VARCHAR(300) NOT NULL
)
