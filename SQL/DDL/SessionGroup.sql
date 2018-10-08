USE [SMap]
IF OBJECT_ID('SessionGroup') IS NOT NULL
	DROP TABLE SessionGroup
GO
CREATE TABLE SessionGroup(
	 ID int Identity(1,1) PRIMARY KEY CLUSTERED
	,SessionGroupName nvarchar(100)
	,SemesterID int
	,Teacher int
	,Deleted bit
)