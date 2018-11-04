IF OBJECT_ID('uspCopySemester') IS NOT NULL
	DROP PROCEDURE uspCopySemester
GO

CREATE PROCEDURE uspCopySemester
	 @sourceId int
	,@newSemesterName nvarchar(60)
	,@newSemesterTypeID int
	,@SessionGroups nvarchar(2000)
	,@Teams nvarchar(2000)
	,@Students nvarchar(2000)
AS BEGIN
	CREATE TABLE #tmpIDs (ID int)
	
	INSERT INTO #tmpIDs (ID)
		SELECT * FROM string_split(@SessionGroups,',')
	UPDATE SessionGroup SET
		Deleted=1 WHERE ID in (SELECT * FROM #tmpIDs)
	TRUNCATE TABLE #tmpIDs

	INSERT INTO #tmpIDs (ID)
		SELECT * FROM string_split(@Teams,',')
	UPDATE Team SET
		Deleted=1 WHERE ID in (SELECT * FROM #tmpIDs)
	TRUNCATE TABLE #tmpIDs

	INSERT INTO #tmpIDs (ID)
		SELECT * FROM string_split(@Students,',')
	UPDATE u SET
		Deleted=1 
	FROM Student s
		INNER JOIN Users u ON u.ID=s.UserID
	WHERE s.ID in (SELECT * FROM #tmpIDs)
	UPDATE Student SET
		Deleted=1 WHERE ID in (SELECT * FROM #tmpIDs)
	DROP TABLE #tmpIDs

	INSERT INTO Semester(
		 SemesterName
		,SemesterType
		,IsActive
	) VALUES(@newSemesterName,@newSemesterTypeID,0)

	DECLARE @newSemesterID int=SCOPE_IDENTITY()

	UPDATE SessionGroup SET SemesterID=@newSemesterID WHERE Deleted=0 AND SemesterID=@sourceId
END