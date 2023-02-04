CREATE PROCEDURE [dbo].[Blog_GetByUserId]
	@ApplicationUserId INT
AS
	SELECT
		B.[BlogId]
		,B.[ApplicationUserId]
		,B.[Username]
		,B.[PhotoId]
		,B.[Title]
		,B.[Content]
		,B.[PublishDate]
		,B.[UpdateDate]
	FROM
		[aggregate].[Blog] B
	WHERE
		B.[ApplicationUserId] = @ApplicationUserId
	AND
		B.[ActiveInd] = CONVERT(BIT, 1);
RETURN 0
