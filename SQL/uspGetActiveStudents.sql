IF OBJECT_ID('uspGetActiveStudents') IS NOT NULL
	DROP PROCEDURE uspGetActiveStudents
GO

CREATE PROCEDURE uspGetActiveStudents
	 @SessionGroupID int=NULL
	,@TeamID int=NULL
	,@TeacherID int=NULL
AS BEGIN
DECLARE @sql NVARCHAR(max)=N'
SELECT s.* FROM Student s
	INNER JOIN Team t ON t.ID=s.TeamID AND t.Deleted=0 '+CASE WHEN @TeamID IS NOT NULL THEN 'AND t.ID = '+CAST(@TeamID as VARCHAR) ELSE '' END+'
	INNER JOIN SessionGroup sg ON sg.ID=t.SessionGroupID AND sg.Deleted=0 '+CASE WHEN @SessionGroupID IS NOT NULL THEN 'AND sg.ID = '+CAST(@SessionGroupID as VARCHAR) ELSE '' END+' ' +CASE WHEN @TeacherID IS NOT NULL THEN 'AND sg.TeacherID = '+CAST(@TeacherID as VARCHAR) ELSE '' END+'
	INNER JOIN Semester se ON se.ID=sg.SemesterID AND se.Deleted=0 AND se.IsActive=1
WHERE s.Deleted=0'
print @sql
exec (@sql)
END