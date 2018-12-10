INSERT INTO Users(
	 NEPTUN
	,FirstName
	,LastName
	,FullName
	,Email
	,UserName
	,UserPassword
	,IsPasswordChangeRequired
	,Deleted
	) VALUES (
	 NULL
	,'Admin'
	,'Admin'
	,'Admin'
	,NULL
	,'admin'
	,'8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918'
	,0
	,0)

DECLARE @id INT = SCOPE_IDENTITY();

INSERT INTO Teacher(
	 UserID
) VALUES(@id)

SET IDENTITY_INSERT DictionaryType ON
INSERT INTO DictionaryType(
	 ID
	,TypeName
	,IsSessionGroup
)
VALUES 
	 (1,'Félév típusok',0)
	,(2,'Csapatok',1)
	,(3,'Adattípusok',0)
	,(4,'Igény típus',0)
	,(5,'Igény állapota',0)
SET IDENTITY_INSERT DictionaryType OFF

SET IDENTITY_INSERT Dictionary ON
INSERT INTO Dictionary(
	 ID
	,ItemName
	,ShortItemName
	,DictionaryTypeID
	,IsProtected
)
VALUES
	 (1,'Õszi','Õszi',1,1)
	,(2,'Tavaszi','Tavaszi',1,1)
	,(3,'Cikk és készlet','C&K',2,1)
	,(4,'Értékesítés','Ért',2,1)
	,(5,'Project Management','PM',2,1)
	,(6,'Beszerzés','Besz',2,1)
	,(7,'Gyártás','Gyárt',2,1)
	,(8,'Pénzügy','PÜ',2,1)
	,(9,'Számlázás','Szám',2,1)
	,(10,'int','int',3,1)
	,(11,'bool','bool',3,1)
	,(12,'string','string',3,1)
	,(13,'float','float',3,1)
	,(14,'double','double',3,1)
	,(15,'char','char',3,1)
	,(16,'date','date',3,1)
	,(17,'datetime','datetime',3,1)
	,(18,'time','time',3,1)
	,(19,'Módosítás','Módosítás',4,1)
	,(20,'Új szolgáltatás','Új szolgáltatás',4,1)
	,(21,'Új','Új',5,1)
	,(22,'Módosítás alatt','Módosítás alatt',5,1)
	,(23,'Jóváhagyásra vár','Jóváhagyásra vár',5,1)
	,(24,'Jóváhagyva','Jóváhagyva',5,1)
	,(25,'Visszautasítva','Visszautasítva',5,1)

SET IDENTITY_INSERT Dictionary OFF