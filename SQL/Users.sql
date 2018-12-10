USE [SMap]
IF OBJECT_ID('Users') IS NOT NULL
	DROP TABLE Users
GO
CREATE TABLE Users(
	 ID int Identity(1,1) PRIMARY KEY CLUSTERED
	,NEPTUN nvarchar(6) NULL
	,FirstName nvarchar(120) NOT NULL -- keresztnév
	,LastName nvarchar(120) NOT NULL -- vezetéknév
	,FullName nvarchar(240) NOT NULL 
	,Email nvarchar(120) NULL
	,IsPasswordChangeRequired bit default 1 not null
	,UserName nvarchar(120) COLLATE HUNGARIAN_CS_AI NOT NULL
	,UserPassword nvarchar(200) NOT NULL
	,Deleted bit DEFAULT 0 NOT NULL
)

CREATE INDEX IX_Users_UserName ON Users(UserName) INCLUDE (UserPassword)
	