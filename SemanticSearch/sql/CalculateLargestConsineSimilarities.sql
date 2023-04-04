USE [SemanticSearchDb]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_CalculateLargestCosineSimilarities]
	@csvList varchar(max),
	@maxResults int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @rec as table(id int)
	declare @result as table(id int, similarity float)

	insert into @rec
	select distinct [Id]
	from [dbo].[KnowledgeRecords]

	declare @id as int 

	create table #tempa(row_num int, val float)
	create table #tempb(row_num int, val float)
	CREATE CLUSTERED INDEX temp_indexa ON #tempa (row_num)
	CREATE CLUSTERED INDEX temp_indexb ON #tempb (row_num)

	insert into #tempb
	SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS row_num,
		   CAST(value AS float) AS val
	FROM STRING_SPLIT(@csvList, ',')

	DECLARE allRecordsCursor CURSOR FOR
	SELECT id FROM @rec
	OPEN allRecordsCursor

	FETCH NEXT FROM allRecordsCursor INTO @id

	WHILE @@FETCH_STATUS = 0
	BEGIN

		truncate TABLE #tempa

		insert into #tempa
		SELECT ROW_NUMBER() OVER (ORDER BY [Id]) as row_num, [VectorValue] as val
		FROM [dbo].[KnowledgeVectorItems]
		where KnowledgeRecordId = @id

		DECLARE @dotProduct float, @aMagnitude float, @bMagnitude float, @similarity float

		-- ChatGPT Embeddings are normalized, so the similarity is just the dot product of the 
		-- two vectors. If you are using a non-normalized embedding, then uncomment these lines
		-- to use cosine similarity.

		--SELECT @dotProduct = SUM(a.val * b.val),
		--	@aMagnitude = SQRT(SUM(a.val * a.val)),
		--	@bMagnitude = SQRT(SUM(b.val * b.val)),
		--	@similarity = @dotProduct / (@aMagnitude * @bMagnitude)
		--FROM #tempa a
		--JOIN #tempb b ON a.row_num = b.row_num

		SELECT @dotProduct = SUM(a.val * b.val),
			@similarity = @dotProduct
		FROM #tempa a
		JOIN #tempb b ON a.row_num = b.row_num

		insert into @result (id, similarity) values(@id, @similarity)

		FETCH NEXT FROM allRecordsCursor INTO @id
	END

	CLOSE allRecordsCursor
	DEALLOCATE allRecordsCursor

	drop table #tempa
	drop table #tempb

	select top(@maxResults) id, similarity from @result order by similarity desc

END
GO


