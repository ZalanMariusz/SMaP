USE [SMap]
IF OBJECT_ID('Dictionary') IS NOT NULL
	DROP TABLE Dictionary
GO
CREATE TABLE Dictionary(
	 ID int Identity(1,1) PRIMARY KEY CLUSTERED
	,ItemName nvarchar(100) NOT NULL
	,ShortItemName nvarchar(100) NULL
	,DictionaryTypeID INT NOT NULL
	,IsProtected BIT DEFAULT 0 NOT NULL
	,Deleted bit default 0 NOT NULL
)

CREATE INDEX IX_Dictionary_DictionaryTypeID ON Dictionary(DictionaryTypeID)