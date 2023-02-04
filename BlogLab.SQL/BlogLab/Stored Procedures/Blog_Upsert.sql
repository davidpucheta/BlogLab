CREATE PROCEDURE [dbo].[Blog_Upsert]
	@Blog BlogType READONLY,
	@ApplicationUserId INT
AS
	MERGE INTO [dbo].[Blog] TARGET
	USING
	(
		SELECT 
			BlogId,
			@ApplicationUserId [ApplicationUserId],
			Title,
			Content,
			PhotoId
		FROM 
			@Blog
	) AS SOURCE
	ON
	(
		TARGET.BlogId = SOURCE.BlogId
		AND
		TARGET.ApplicationUserId = SOURCE.ApplicationUserId
	)
	WHEN MATCHED THEN 
		UPDATE SET
			TARGET.[Title] = SOURCE.[Title],
			TARGET.[Content] = SOURCE.[Content],
			TARGET.[PhotoId] = SOURCE.[PhotoId],
			TARGET.[UpdateDate] = GETDATE()
	WHEN NOT MATCHED THEN
		INSERT
		(
			[ApplicationUserId],
			[Title],
			[Content],
			[PhotoId]
		)
		VALUES
		(
			SOURCE.[ApplicationUserId],
			SOURCE.[Title],
			SOURCE.[Content],
			SOURCE.[PhotoID]
		);

		SELECT CAST(SCOPE_IDENTITY() AS INT);

RETURN 0
