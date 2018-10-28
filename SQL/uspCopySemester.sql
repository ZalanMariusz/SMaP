IF OBJECT_ID('uspCopySemester') IS NOT NULL
	DROP PROCEDURE uspCopySemester
GO

CREATE PROCEDURE uspCopySemester
	 @sourceId int
	,@newSemesterName nvarchar(60)
	,@newSemesterTypeID int
AS BEGIN
	INSERT INTO Semester(
		 SemesterName
		,SemesterType
		,IsActive
	) VALUES(@newSemesterName,@newSemesterTypeID,0)

	DECLARE @newSemesterID int=SCOPE_IDENTITY()

	INSERT INTO SessionGroup(
		 SessionGroupName
		,SemesterID
		,TeacherID
	)
	SELECT 
		 SessionGroupName
		,@newSemesterID
		,TeacherID
	FROM SessionGroup
	WHERE Deleted=0 AND SemesterID=@sourceId

	INSERT INTO Team(
		 t.SessionGroupID
		,t.TeamName
		,t.TeamCaptain
	)
	SELECT 
		 sgNew.ID
		,t.TeamName
		,NULL
	FROM Team t
		INNER JOIN SessionGroup sg ON sg.ID=t.SessionGroupID AND sg.Deleted=0 AND sg.SemesterID=@sourceId
		INNER JOIN SessionGroup sgNew ON sgNew.SessionGroupName=sg.SessionGroupName AND sgNew.SemesterID=@newSemesterID AND sgNew.Deleted=0
	WHERE t.Deleted=0

	INSERT INTO Student(
		 UserID
		,TeamID
	) 
	SELECT 
		 u.UserID
		,tNew.ID
	FROM Student u
		INNER JOIN Team t ON t.ID=u.TeamID AND t.Deleted=0
		INNER JOIN SessionGroup sg ON sg.ID=t.SessionGroupID AND sg.SemesterID=@sourceId AND sg.Deleted=0
		INNER JOIN SessionGroup sgNew ON sgNew.SessionGroupName=sg.SessionGroupName AND sgNew.Deleted=0 AND sgNew.SemesterID=@newSemesterID
		INNER JOIN Team tNew ON tNew.SessionGroupID=sgNew.ID AND tNew.Deleted=0 AND tNew.TeamName=t.TeamName
	WHERE u.Deleted=0

	UPDATE tNew set TeamCaptain=sNew.ID
	FROM Student s
		INNER JOIN Team t ON t.TeamCaptain=s.ID AND t.Deleted=0
		INNER JOIN SessionGroup sg ON sg.ID=t.SessionGroupID AND sg.SemesterID=@sourceId AND sg.Deleted=0
		INNER JOIN Student sNew ON sNew.UserID=s.UserID AND sNew.Deleted=0 AND sNew.ID<>s.ID
		INNER JOIN Team tNew on tNew.ID=sNew.TeamID AND tNew.Deleted=0
		INNER JOIN SessionGroup sgNew ON sgNew.SemesterID=@newSemesterID AND sgNew.ID=tNew.SessionGroupID
	WHERE s.Deleted=0


END