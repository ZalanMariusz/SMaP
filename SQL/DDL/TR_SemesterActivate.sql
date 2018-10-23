use [SMaP]
IF OBJECT_ID('TR_SemesterActivate') IS NOT NULL
	DROP TRIGGER TR_SemesterActivate
GO
CREATE TRIGGER TR_SemesterActivate ON Semester AFTER INSERT, UPDATE
AS
BEGIN
	DECLARE  @id INT=(SELECT ID FROM inserted)
			,@isActive bit=(SELECT IsActive FROM inserted)
	IF @isActive = 1
	UPDATE Semester SET
		IsActive = 0
	WHERE Deleted=0 AND ID<>@id
END

