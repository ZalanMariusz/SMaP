USE [SMap]
IF OBJECT_ID('DictionaryType') IS NOT NULL
	DROP TABLE DictionaryType
GO
CREATE TABLE DictionaryType(
	 ID int Identity(1,1) PRIMARY KEY CLUSTERED
	,TypeName nvarchar(100) NOT NULL
	,IsSessionGroup bit NULL
	,Deleted bit default 0 NOT NULL
)

CREATE INDEX IX_DictionaryType_TypeName ON DictionaryType(TypeName)