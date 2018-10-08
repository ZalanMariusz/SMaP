USE [SMap]
IF OBJECT_ID('Teacher') IS NOT NULL
	DROP TABLE Teacher
GO
CREATE TABLE Teacher(
	 ID int Identity(1,1) PRIMARY KEY CLUSTERED
	,UserID int NOT NULL
	,DefaultSessionGroupID int NULL
	,Deleted bit default 0
)

