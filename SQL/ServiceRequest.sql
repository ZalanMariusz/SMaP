IF OBJECT_ID('ServiceRequest') IS NOT NULL
	DROP TABLE ServiceRequest
GO
CREATE TABLE ServiceRequest(
	 ID INT IDENTITY(1,1) PRIMARY KEY
	,ServiceName nvarchar(200) NOT NULL
	,RequesterTeamID int NOT NULL
	,ProviderTeamID int NOT NULL
	,ServiceID int null
	,Deleted bit default 0 NOT NULL
)