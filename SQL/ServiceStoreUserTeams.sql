IF OBJECT_ID('ServiceStoreUserTeams') IS NOT NULL
	DROP TABLE ServiceStoreUserTeams
GO

CREATE TABLE ServiceStoreUserTeams(
	  ID int Identity(1,1) PRIMARY KEY CLUSTERED
	 ,RequesterTeamID INT NOT NULL
	 ,ServiceID INT NOT NULL
	 ,Deleted bit default 0 NOT NULL
)

CREATE INDEX IX_ServiceStoreUserTeams_RequesterTeamID ON ServiceStoreUserTeams(RequesterTeamID)
CREATE INDEX IX_ServiceStoreUserTeams_ServiceID ON ServiceStoreUserTeams(ServiceID)
