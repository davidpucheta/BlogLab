CREATE PROCEDURE [dbo].[BlogComment_GetAll]
	@BlogId INT
AS
	SELECT 
		 BC.[BlogCommentId]
		,BC.[ParentBlogCommentId]
		,BC.[BlogId]
		,BC.[ApplicationUserId]
		,BC.[Username]
		,BC.[Content]
		,BC.[PublishDate]
		,BC.[UpdateDate]
	FROM
		[aggregate].[BlogComment] BC
		WHERE
		BC.[BlogId] = @BlogId
	AND
		BC.[ActiveInd] = CONVERT(BIT, 1)
	ORDER BY
		BC.[UpdateDate] DESC

RETURN 0
