CREATE TABLE [dbo].[Photo]
(
	[PhotoId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [ApplicationUserId] INT NOT NULL, 
    [PublicId] VARCHAR(50) NOT NULL, 
    [ImageUrl] VARCHAR(250) NOT NULL, 
    [Description] VARCHAR(30) NOT NULL, 
    [PublishDate] DATETIME NOT NULL DEFAULT GETDATE(), 
    [UpdateDate] DATETIME NOT NULL DEFAULT GETDATE(), 

    CONSTRAINT [FK_Photo_ApplicationUser] FOREIGN KEY ([ApplicationUserId]) REFERENCES [ApplicationUser]([ApplicationUserId])
)
