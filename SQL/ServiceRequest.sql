IF OBJECT_ID('ServiceRequest') IS NOT NULL
	DROP TABLE ServiceRequest
GO
CREATE TABLE ServiceRequest(
	 ID INT IDENTITY(1,1) PRIMARY KEY
	,ServiceName nvarchar(200) NOT NULL
	,RequesterTeamID int NOT NULL
	,ProviderTeamID int NOT NULL
	,ServiceID int null
	,IsApproved bit NOT NULL DEFAULT 0
	,RequestType int NOT NULL
	,RequestState int NOT NULL
	,RequestDescription nvarchar(600)
	,Deleted bit default 0 NOT NULL
)


CREATE INDEX IX_ServiceRequest_RequesterTeamID ON ServiceRequest(RequesterTeamID)
CREATE INDEX IX_ServiceRequest_ProviderTeamID ON ServiceRequest(ProviderTeamID)
CREATE INDEX IX_ServiceRequest_ServiceID ON ServiceRequest(ServiceID)