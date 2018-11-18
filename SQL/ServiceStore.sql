IF OBJECT_ID('ServiceStore') IS NOT NULL
	DROP TABLE ServiceStore
GO
CREATE TABLE ServiceStore(
	 ID INT IDENTITY(1,1) PRIMARY KEY
	,ServiceName nvarchar(200) NOT NULL
	,ServiceNumber int NOT NULL
	,CreatorID int NOT NULL
	,ProviderTeamID int NOT NULL
	,ServiceDescription nvarchar(1000) null
	,Deleted bit default 0 NOT NULL
)

CREATE INDEX IX_ServiceStore_ProviderTeamID ON ServiceStore(ProviderTeamID)