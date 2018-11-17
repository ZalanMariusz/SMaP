USE [SMap]
IF OBJECT_ID('Teacher') IS NOT NULL
	DROP TABLE Teacher
GO
CREATE TABLE Teacher(
	 ID int Identity(1,1) PRIMARY KEY CLUSTERED
	,UserID int NOT NULL
	,Deleted bit default 0 NOT NULL
)

CREATE INDEX IX_Teacher_UserID ON Teacher (UserID)