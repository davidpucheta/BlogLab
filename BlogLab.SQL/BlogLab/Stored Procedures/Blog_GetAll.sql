CREATE PROCEDURE [dbo].[Blog_GetAll] 
    @Offset INT,
    @PageSize INT 
AS
    SELECT
        [BlogId],
        [ApplicationUserId],
        [Username],
        [Title],
        [Content],
        [PhotoId],
        [PublishDate],
        [UpdateDate]
    FROM
        [aggregate].[Blog] B
    WHERE
        B.ActiveInd = CONVERT(BIT, 1)
    ORDER BY
        B.[BlogId] OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

    SELECT
        COUNT(*)
    FROM
        [aggregate].[Blog] B
    WHERE
        B.ActiveInd = CONVERT(BIT, 1);

RETURN 0