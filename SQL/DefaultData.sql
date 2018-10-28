INSERT INTO Users(
	 NEPTUN
	,FirstName
	,LastName
	,FullName
	,Email
	,UserName
	,UserPassword
	,Deleted
	) VALUES (
	 NULL
	,'Admin'
	,'Admin'
	,'Admin'
	,NULL
	,'admin'
	,'8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918'
	,0)

DECLARE @id INT = SCOPE_IDENTITY();

INSERT INTO Teacher(
	 UserID
) VALUES(@id)

INSERT INTO DictionaryType(
	 TypeName)
VALUES ('Félév típusok')

set @id=SCOPE_IDENTITY()

INSERT INTO Dictionary(
	 ItemName
	,ShortItemName 
	,DictionaryTypeID
	,IsProtected
)
VALUES (N'Õszi',N'Õszi',@id,1)
	  ,(N'Tavaszi',N'Tavaszi',@id,1)

INSERT INTO DictionaryType(
	 TypeName)
VALUES ('Csapatok')
set @id=SCOPE_IDENTITY()
INSERT INTO Dictionary(
	 ItemName
	,ShortItemName 
	,DictionaryTypeID
	,IsProtected
)
VALUES (N'Cikk és készlet',N'C&K',@id,1)
	  ,(N'Értékesítés',N'Ért',@id,1)
	  ,(N'Project Management',N'PM',@id,1)
	  ,(N'Beszerzés',N'Besz',@id,1)
	  ,(N'Gyártás',N'Gyárt',@id,1)
	  ,(N'Pénzügy',N'PÜ',@id,1)
	  ,(N'Számlázás',N'Szám',@id,1)

