CREATE PROCEDURE [dbo].[BlogComment_Delete]
	@BlogCommentId INT
AS
	
	DROP TABLE IF EXISTS #BlogCommentsToBeDeleted;

	WITH cte_Blog_Comments As (
		SELECT 
			BC1.[BlogCommentId],
			BC1.[ParentBlogCommentId]
		FROM
			[dbo].[BlogComment] BC1
		WHERE
			BC1.[BlogCommentId] = @BlogCommentId
		UNION ALL
		SELECT 
			BC2.[BlogCommentId],
			BC2.[ParentBlogCommentId]
		FROM
			[dbo].[BlogComment] BC2
		INNER JOIN 
			cte_Blog_Comments BC3
		ON 
			BC3.[BlogCommentId] = BC2.[BlogCommentId]
	)

	SELECT
		[BlogCommentId],
		[ParentBlogCommentId]
	INTO
		#BlogCommentsToBeDeleted
	FROM
		cte_Blog_Comments;

	UPDATE BC
	SET 
		BC.[ActiveInd] = CONVERT(BIT, 0),
		BC.[UpdateDate] = GETDATE()
	FROM
		[dbo].[BlogComment] BC 
	INNER JOIN
		#BlogCommentsToBeDeleted BCD
	ON
		BC.[BlogCommentId] = BCD.[BlogCommentId]
	
RETURN 0
