USE [SMap]
IF OBJECT_ID('Semester') IS NOT NULL
	DROP TABLE Semester
GO
CREATE TABLE Semester(
	 ID int Identity(1,1) PRIMARY KEY CLUSTERED
	,SemesterName nvarchar(30) NOT NULL
	,SemesterType int NOT NULL 
	,IsActive bit default 0 NOT NULL
	,Deleted bit default 0 NOT NULL
)

CREATE INDEX IX_Semester_SemesterType ON Semester(SemesterType)