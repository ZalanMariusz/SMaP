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
VALUES ('F�l�v t�pusok')

set @id=SCOPE_IDENTITY()

INSERT INTO Dictionary(
	 ItemName
	,ShortItemName 
	,DictionaryTypeID
	,IsProtected
)
VALUES (N'�szi',N'�szi',@id,1)
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
VALUES (N'Cikk �s k�szlet',N'C&K',@id,1)
	  ,(N'�rt�kes�t�s',N'�rt',@id,1)
	  ,(N'Project Management',N'PM',@id,1)
	  ,(N'Beszerz�s',N'Besz',@id,1)
	  ,(N'Gy�rt�s',N'Gy�rt',@id,1)
	  ,(N'P�nz�gy',N'P�',@id,1)
	  ,(N'Sz�ml�z�s',N'Sz�m',@id,1)

INSERT INTO DictionaryType(
	 TypeName)
VALUES ('Adatt�pusok')
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
VALUES ('Ig�ny t�pus')
SET @id = SCOPE_IDENTITY()
INSERT INTO Dictionary(
	 ItemName
	,ShortItemName 
	,DictionaryTypeID
	,IsProtected
)
VALUES (N'M�dos�t�s',N'M�dos�t�s',@id,1)
	  ,(N'�j szolg�ltat�s',N'�j szolg�ltat�s',@id,1)
	  

INSERT INTO DictionaryType(
	 TypeName)
VALUES ('Ig�ny �llapota')
SET @id = SCOPE_IDENTITY()
INSERT INTO Dictionary(
	 ItemName
	,ShortItemName 
	,DictionaryTypeID
	,IsProtected
)
VALUES (N'�j',N'�j',@id,1)
	  ,(N'Folyamatban',N'Folyamatban',@id,1)
	  ,(N'J�v�hagy�sra v�r',N'J�v�hagy�sra v�r',@id,1)
	  ,(N'Visszautas�tva',N'Visszautas�tva',@id,1)
	  ,(N'J�v�hagyva',N'J�v�hagyva',@id,1)
	  ,(N'Visszautas�tva',N'Visszautas�tva',@id,1)

	  select * from DictionaryType WHERE DictionaryTypeID=5