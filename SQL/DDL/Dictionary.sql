USE [SMap]
IF OBJECT_ID('Dictionary') IS NOT NULL
	DROP TABLE Dictionary
GO
CREATE TABLE Dictionary(
	 ID int Identity(1,1) PRIMARY KEY CLUSTERED
	,ItemName nvarchar(100) NOT NULL
	,ShortItemName nvarchar(100) NULL
	,ValidFrom datetime NULL
	,ValidTo datetime NULL
	,ItemType nvarchar(100) NOT NULL
	,Deleted bit default 0 NOT NULL
)

INSERT INTO Dictionary(
	 ItemName
	,ShortItemName 
	,ValidFrom
	,ValidTo 
	,ItemType
	,Deleted
)
VALUES(N'Õszi',N'Õszi',null,null,'SemesterType',0)
,	  (N'Tavaszi',N'Tavaszi',null,null,'SemesterType',0)