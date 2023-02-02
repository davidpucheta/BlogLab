CREATE VIEW [aggregate].[Blog]
	AS 
		SELECT
			B.BlogId,
			B.ApplicationUserId,
			AU.Username,
			B.Title,
			B.Content,
			B.PhotoId,
			B.PublishDate,
			B.UpdateDate,
			B.ActiveInd
		FROM 
			[dbo].[Blog] B
		INNER JOIN 
			dbo.ApplicationUser AU
		ON
			B.ApplicationUserId = AU.ApplicationUserId
