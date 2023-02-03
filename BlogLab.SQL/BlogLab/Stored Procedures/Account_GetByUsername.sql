CREATE PROCEDURE [dbo].[Account_GetByUsername]
	@NormalizedUsername VARCHAR(20)
AS
	SELECT 
		 [ApplicationUserId]
		,[Username]
		,[NormalizedUsername]
		,[Email]
		,[NormalizedEmail]
		,[Fullname]
		,[PasswordHash]
	FROM 
		[dbo].[ApplicationUser] AU
	WHERE
		AU.[NormalizedUsername] = @NormalizedUsername
RETURN 0
