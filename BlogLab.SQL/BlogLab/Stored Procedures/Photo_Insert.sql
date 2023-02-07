CREATE PROCEDURE [dbo].[Photo_Insert]
	@Photo PhotoType READONLY,
	@ApplicationUserId INT
AS
	INSERT INTO [dbo].[Photo]
	(
			 [ApplicationUserId]
			,[PublicId]
			,[ImageUrl]
			,[Description]
	)
	SELECT
		@ApplicationUserId
		,[PublicId]
		,[ImageUrl]
		,[Description]
	FROM
		@Photo;

	SELECt CAST(SCOPE_IDENTITY() AS INT);
	
RETURN 0
