CREATE PROCEDURE [dbo].[Photo_GetByUserId]
	@ApplicationUserId INT
AS
	SELECT 
		 P.[PhotoId]
		,P.[ApplicationUserId]
		,P.[PublicId]
		,P.[ImageUrl]
		,P.[Description]
		,P.[PublishDate]
		,P.[UpdateDate]
	FROM
		[dbo].[Photo] P
	WHERE
		P.[ApplicationUserId] = @ApplicationUserId

RETURN 0
