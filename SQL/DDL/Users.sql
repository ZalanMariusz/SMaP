USE [SMap]
IF OBJECT_ID('Users') IS NOT NULL
	DROP TABLE Users
GO
CREATE TABLE Users(
	 ID int Identity(1,1) PRIMARY KEY CLUSTERED
	,NEPTUN nvarchar(6) NULL
	,FirstName nvarchar(120) NULL -- keresznév
	,LastName nvarchar(120) NULL -- vezetéknév
	,FullName nvarchar(240) NULL 
	,Email nvarchar(120) NULL
	,UserName nvarchar(120) NULL
	,UserPassword nvarchar(200) NULL
	,Deleted bit
	INDEX NCX_FullName(FullName)
)

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
