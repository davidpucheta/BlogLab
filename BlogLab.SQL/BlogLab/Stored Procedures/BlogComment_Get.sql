CREATE PROCEDURE [dbo].[BlogComment_Get]
	@BlogCommentId INT
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
		BC.[BlogCommentId] = @BlogCommentId
	AND
		BC.[ActiveInd] = CONVERT(BIT, 1)

RETURN 0
