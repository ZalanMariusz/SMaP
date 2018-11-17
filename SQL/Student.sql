USE [SMap]
IF OBJECT_ID('Student') IS NOT NULL
	DROP TABLE Student
GO
CREATE TABLE Student(
	 ID int Identity(1,1) PRIMARY KEY CLUSTERED
	,UserID int NOT NULL
	,TeamID int NOT NULL
	,Deleted bit DEFAULT 0 NOT NULL
)


CREATE INDEX IX_Student_UserID ON Student(UserID)
CREATE INDEX IX_Student_TeamID ON Student(TeamID)
