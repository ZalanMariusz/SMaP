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

INSERT INTO DictionaryType(
	 TypeName)
VALUES ('Adattípusok')
set @id=SCOPE_IDENTITY()
INSERT INTO Dictionary(
	 ItemName
	,ShortItemName 
	,DictionaryTypeID
	,IsProtected
)
VALUES (N'int',N'int',@id,1)
	  ,(N'bool',N'bool',@id,1)
	  ,(N'string',N'string',@id,1)
	  ,(N'float',N'float',@id,1)
	  ,(N'double',N'double',@id,1)
	  ,(N'char',N'char',@id,1)
	  ,(N'date',N'date',@id,1)
	  ,(N'datetime',N'datetime',@id,1)
	  ,(N'time',N'time',@id,1)

INSERT INTO DictionaryType(
	 TypeName)
VALUES ('Igény típus')
SET @id = SCOPE_IDENTITY()
INSERT INTO Dictionary(
	 ItemName
	,ShortItemName 
	,DictionaryTypeID
	,IsProtected
)
VALUES (N'Módosítás',N'Módosítás',@id,1)
	  ,(N'Új szolgáltatás',N'Új szolgáltatás',@id,1)
	  

INSERT INTO DictionaryType(
	 TypeName)
VALUES ('Igény állapota')
SET @id = SCOPE_IDENTITY()
INSERT INTO Dictionary(
	 ItemName
	,ShortItemName 
	,DictionaryTypeID
	,IsProtected
)
VALUES (N'Új',N'Új',@id,1)
	  ,(N'Folyamatban',N'Folyamatban',@id,1)
	  ,(N'Jóváhagyásra vár',N'Jóváhagyásra vár',@id,1)
	  ,(N'Visszautasítva',N'Visszautasítva',@id,1)
	  ,(N'Jóváhagyva',N'Jóváhagyva',@id,1)
	  ,(N'Visszautasítva',N'Visszautasítva',@id,1)

	  select * from DictionaryType WHERE DictionaryTypeID=5