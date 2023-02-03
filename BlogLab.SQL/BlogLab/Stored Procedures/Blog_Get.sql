CREATE PROCEDURE [dbo].[Blog_Get]
	@BlogId INT
AS
    SELECT
        [BlogId]
        ,[ApplicationUserId]
        ,[Username]
        ,[Title]
        ,[Content]
        ,[PhotoId]
        ,[PublishDate]
        ,[UpdateDate]
        ,[ActiveInd]
    FROM 
        [aggregate].[Blog] B 
    WHERE
       B.BlogId = @BlogId 
    AND
        B.ActiveInd = CONVERT(BIT, 1);
RETURN 0
