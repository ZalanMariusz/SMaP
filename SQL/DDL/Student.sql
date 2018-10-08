USE [SMap]
IF OBJECT_ID('Student') IS NOT NULL
	DROP TABLE Student
GO
CREATE TABLE Student(
	 ID int Identity(1,1) PRIMARY KEY CLUSTERED
	,UserID int NOT NULL
	,TeamID int NOT NULL
	,Deleted bit NOT NULL
)