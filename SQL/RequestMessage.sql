IF OBJECT_ID('RequestMessage') IS NOT NULL
	DROP TABLE RequestMessage
GO
CREATE TABLE RequestMessage(
	 ID INT IDENTITY(1,1) PRIMARY KEY
	,Created DATETIME NOT NULL
	,SenderID INT NOT NULL
	,RequestMessage nvarchar(1000) NOT NULL
	,RequestID int NOT NULL
	,Deleted bit default 0 NOT NULL
)

CREATE INDEX IX_RequestMessage_RequestID ON RequestMessage(RequestID)
