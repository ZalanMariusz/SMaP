IF OBJECT_ID('ServiceStoreParams') IS NOT NULL
	DROP TABLE ServiceStoreParams
GO
CREATE TABLE ServiceStoreParams(
	 ID INT IDENTITY(1,1) PRIMARY KEY
	,ServiceID int NOT NULL
	,InOut bit not null
	,ServiceTableFieldID INT not null
	,Deleted bit default 0 NOT NULL
)

CREATE INDEX IX_ServiceStoreParam_ServiceID ON ServiceStoreParams(ServiceID)
CREATE INDEX IX_ServiceStoreParam_ServiceTableFieldID ON ServiceStoreParams(ServiceTableFieldID)