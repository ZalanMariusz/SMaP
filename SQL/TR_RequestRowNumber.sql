use [SMaP]
IF OBJECT_ID('TR_RequestRowNumber') IS NOT NULL
	DROP TRIGGER TR_RequestRowNumber
GO
CREATE TRIGGER TR_RequestRowNumber ON ServiceRequest AFTER INSERT
AS
BEGIN
	DECLARE  @id INT=(SELECT ID FROM inserted)
	DECLARE @SessionGroupID INT
	SELECT @SessionGroupID=t.SessionGroupID FROM ServiceRequest sr
		INNER JOIN Team t ON t.ID=sr.RequesterTeamID
	WHERE sr.ID=@id
	
	UPDATE ServiceRequest SET RequestNumber=
		(SELECT count(*)+1 from ServiceRequest sr
			INNER JOIN Team t ON t.ID=sr.RequesterTeamID
		WHERE t.SessionGroupID=@SessionGroupID
		GROUP BY t.SessionGroupID)
	WHERE ID = @id
END