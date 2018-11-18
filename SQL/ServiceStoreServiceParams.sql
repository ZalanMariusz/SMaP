IF OBJECT_ID('ServiceStoreServiceParams') IS NOT NULL
	DROP TABLE ServiceStoreServiceParams
GO

CREATE TABLE ServiceStoreServiceParams(
	 ID int Identity(1,1) PRIMARY KEY CLUSTERED
	,ParentServiceStoreID INT NOT NULL
	,ParamServiceStoreID INT NOT NULL
	,Deleted bit default 0 not null
)

CREATE INDEX IX_ServiceStoreServiceParams_ParentServiceStoreID ON ServiceStoreServiceParams(ParentServiceStoreID)
CREATE INDEX IX_ServiceStoreServiceParams_ParamServiceStoreID ON ServiceStoreServiceParams(ParamServiceStoreID)
