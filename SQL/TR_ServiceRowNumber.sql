use [SMaP]
IF OBJECT_ID('TR_ServiceRowNumber') IS NOT NULL
	DROP TRIGGER TR_ServiceRowNumber
GO
CREATE TRIGGER TR_ServiceRowNumber ON ServiceStore AFTER INSERT
AS
BEGIN
	DECLARE  @id INT=(SELECT ID FROM inserted)
	DECLARE @SessionGroupID INT
	SELECT @SessionGroupID=t.SessionGroupID FROM ServiceStore ss
		INNER JOIN Team t ON t.ID=ss.ProviderTeamID
	WHERE ss.ID=@id
	
	UPDATE ServiceStore SET ServiceNumber=
		(SELECT count(*)+1 from ServiceStore ss
			INNER JOIN Team t ON t.ID=ss.ProviderTeamID
		WHERE t.SessionGroupID=@SessionGroupID
		GROUP BY t.SessionGroupID)
	WHERE ID = @id
END