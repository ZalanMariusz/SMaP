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

