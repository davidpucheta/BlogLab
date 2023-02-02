CREATE TABLE [dbo].[BlogComment]
(
	[BlogCommentId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ParentBlogCommentId] INT NOT NULL, 
    [BlogId] INT NOT NULL, 
    [ApplicationUserId] INT NOT NULL, 
    [Content] VARCHAR(300) NOT NULL, 
    [PublishDate] DATETIME NOT NULL DEFAULT GETDATE(), 
    [UpdateDate] DATETIME NOT NULL DEFAULT GETDATE(), 
    [ActiveInd] BIT NOT NULL DEFAULT CONVERT(BIT, 1), 
    CONSTRAINT [FK_BlogComment_Blog] FOREIGN KEY ([BlogId]) REFERENCES [Blog]([BlogId]), 
    CONSTRAINT [FK_BlogComment_ApplicationUser] FOREIGN KEY ([ApplicationUserId]) REFERENCES [ApplicationUser]([ApplicationUserId])
)
