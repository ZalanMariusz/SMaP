USE [SMap]
IF OBJECT_ID('SessionGroup') IS NOT NULL
	DROP TABLE SessionGroup
GO
CREATE TABLE SessionGroup(
	 ID int Identity(1,1) PRIMARY KEY CLUSTERED
	,SessionGroupName nvarchar(100)
	,SemesterID int
	,TeacherID int
	,Deleted bit DEFAULT 0 NOT NULL
)

CREATE INDEX IX_SessionGroup_SemesterID ON SessionGroup(SemesterID)
CREATE INDEX IX_SessionGroup_TeacherID ON SessionGroup(TeacherID)
