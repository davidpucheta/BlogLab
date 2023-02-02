CREATE TABLE [dbo].[ApplicationUser]
(
	[ApplicationUserId] INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	[Username] VARCHAR(20) NOT NULL,
	[NormalizedUsername] VARCHAR(20) NOT NULL,
	[Email] VARCHAR(30) NOT NULL,
	[NormalizedEmail] VARCHAR(30) NOT NULL,
	[Fullname] VARCHAR(30) NULL,
	[PasswordHash] VARCHAR(MAX) NOT NULL, 

	INDEX [IX_ApplicationUser_NormalizedUsername] ([NormalizedUsername]),
	INDEX [IX_ApplicationUser_NormalizedEmail] ([NormalizedEmail])
);

