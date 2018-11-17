USE [SMap]
IF OBJECT_ID('Team') IS NOT NULL
	DROP TABLE Team
GO
CREATE TABLE Team(
	 ID int Identity(1,1) PRIMARY KEY CLUSTERED
	,SessionGroupID int NOT NULL
	,TeamName nvarchar(100) NOT NULL
	,TeamCaptain int NULL
	,Deleted bit default 0 NOT NULL
)

CREATE INDEX IX_Team_SessionGroupID ON Team(SessionGroupID)
CREATE INDEX IX_Team_TeamCaptain ON Team(TeamCaptain)
CREATE INDEX IX_Team_TeamName ON Team(TeamName)
