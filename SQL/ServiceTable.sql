IF OBJECT_ID('ServiceTable') IS NOT NULL
	DROP TABLE ServiceTable
GO
CREATE TABLE ServiceTable(
	 ID INT IDENTITY(1,1) PRIMARY KEY
	,TableName nvarchar(200) NOT NULL
	,TeamID int
	,Deleted bit not null default 0
)

CREATE INDEX IX_ServiceTable_Deleted ON ServiceTable(Deleted)

