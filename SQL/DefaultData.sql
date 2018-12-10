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
	 (1,'F�l�v t�pusok',0)
	,(2,'Csapatok',1)
	,(3,'Adatt�pusok',0)
	,(4,'Ig�ny t�pus',0)
	,(5,'Ig�ny �llapota',0)
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
	 (1,'�szi','�szi',1,1)
	,(2,'Tavaszi','Tavaszi',1,1)
	,(3,'Cikk �s k�szlet','C&K',2,1)
	,(4,'�rt�kes�t�s','�rt',2,1)
	,(5,'Project Management','PM',2,1)
	,(6,'Beszerz�s','Besz',2,1)
	,(7,'Gy�rt�s','Gy�rt',2,1)
	,(8,'P�nz�gy','P�',2,1)
	,(9,'Sz�ml�z�s','Sz�m',2,1)
	,(10,'int','int',3,1)
	,(11,'bool','bool',3,1)
	,(12,'string','string',3,1)
	,(13,'float','float',3,1)
	,(14,'double','double',3,1)
	,(15,'char','char',3,1)
	,(16,'date','date',3,1)
	,(17,'datetime','datetime',3,1)
	,(18,'time','time',3,1)
	,(19,'M�dos�t�s','M�dos�t�s',4,1)
	,(20,'�j szolg�ltat�s','�j szolg�ltat�s',4,1)
	,(21,'�j','�j',5,1)
	,(22,'M�dos�t�s alatt','M�dos�t�s alatt',5,1)
	,(23,'J�v�hagy�sra v�r','J�v�hagy�sra v�r',5,1)
	,(24,'J�v�hagyva','J�v�hagyva',5,1)
	,(25,'Visszautas�tva','Visszautas�tva',5,1)

SET IDENTITY_INSERT Dictionary OFF