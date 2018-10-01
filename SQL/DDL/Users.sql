IF OBJECT_ID('Users') IS NOT NULL
	DROP TABLE Users
GO
CREATE TABLE Users(
	 ID int Identity(1,1) PRIMARY KEY CLUSTERED
	,NEPTUN nvarchar(6)
	,FirstName nvarchar(120) -- keresznév
	,LastName nvarchar(120) -- vezetéknév
	,FullName nvarchar(240) 
	,Email nvarchar(120)
	,UserName nvarchar(120)
	,UserPassword nvarchar(200)
	--,UserPasswordSalt float
	,Deleted bit
	INDEX NCX_FullName(FullName)
)