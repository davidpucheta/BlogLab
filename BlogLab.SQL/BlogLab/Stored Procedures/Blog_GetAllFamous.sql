CREATE PROCEDURE [dbo].[Blog_GetAllFamous]
AS
	SELECT TOP (6)
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
	INNER JOIN 
		[dbo].[BlogComment] BC
	ON
		B.BlogId = BC.BlogId
	WHERE
		B.ActiveInd = CONVERT(BIT, 1)
	AND
		BC.ActiveInd = CONVERT(BIT, 1)
	GROUP BY
		B.[BlogId]
		,B.[ApplicationUserId]
		,B.[Username]
		,B.[PhotoId]
		,B.[Title]
		,B.[Content]
		,B.[PublishDate]
		,B.[UpdateDate]
	ORDER BY
		COUNT(BC.BlogCommentId)
	DESC

RETURN 0
