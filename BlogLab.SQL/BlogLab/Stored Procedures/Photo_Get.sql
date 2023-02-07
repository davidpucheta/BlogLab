CREATE PROCEDURE [dbo].[Photo_Get]
	@PhotoId INT
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
		P.[PhotoId] = @PhotoId
RETURN 0
