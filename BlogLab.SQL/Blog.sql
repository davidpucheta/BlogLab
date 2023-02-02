CREATE TABLE [dbo].[Blog]
(
	[BlogId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ApplicationUserId] INT NOT NULL, 
    [PhotoId] INT NULL, 
    [Title] VARCHAR(50) NOT NULL, 
    [Content] VARCHAR(MAX) NOT NULL, 
    [PublishDate] DATETIME NOT NULL DEFAULT GETDATE(), 
    [UpdateDate] DATETIME NOT NULL DEFAULT GETDATE(), 
    [ActiveInd] BIT NOT NULL DEFAULT CONVERT(BIT, 1), 
    
    CONSTRAINT [FK_Blog_ApplicationUser] FOREIGN KEY ([ApplicationUserId]) REFERENCES [ApplicationUser]([ApplicationUserId]), 
    CONSTRAINT [FK_Blog_Photo] FOREIGN KEY ([PhotoId]) REFERENCES [Photo]([PhotoId])
)
