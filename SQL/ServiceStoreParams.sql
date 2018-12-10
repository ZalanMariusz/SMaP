IF OBJECT_ID('ServiceStoreParams') IS NOT NULL
	DROP TABLE ServiceStoreParams
GO
CREATE TABLE ServiceStoreParams(
	 ID INT IDENTITY(1,1) PRIMARY KEY
	,ServiceID int NOT NULL
	,InOut bit not null
	,ServiceTableFieldID INT null
	,ParamName NVARCHAR(100) NOT NULL
	,IsCustom BIT DEFAULT 0 NOT NULL
	,CustomParamTypeID INT NULL
	,Deleted bit default 0 NOT NULL
)

CREATE INDEX IX_ServiceStoreParam_ServiceID ON ServiceStoreParams(ServiceID)
CREATE INDEX IX_ServiceStoreParam_ServiceTableFieldID ON ServiceStoreParams(ServiceTableFieldID)