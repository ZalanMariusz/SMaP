IF OBJECT_ID('ServiceTableField') IS NOT NULL
	DROP TABLE ServiceTableField
GO
CREATE TABLE ServiceTableField(
	 ID INT IDENTITY(1,1) PRIMARY KEY
	,TableID int NOT NULL
	,FieldName nvarchar(200)
	,FieldTypeID int NOT NULL
	,Deleted bit not null default 0
)

