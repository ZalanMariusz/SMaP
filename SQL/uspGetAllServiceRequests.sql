IF OBJECT_ID('uspGetAllServiceRequests') IS NOT NULL
	DROP PROCEDURE uspGetAllServiceRequests
GO

CREATE PROCEDURE uspGetAllServiceRequests
	 @SessionGroupID int
	,@rowNumber int=null
	,@requesterTeamID int=null
	,@providerTeamID int=null
	,@requestState int=null
	,@requestType int =null
	,@creatorId int=null
	,@assigneeId int=null
AS BEGIN

DECLARE @sql nvarchar(max)=N'
SELECT sr.* FROM ServiceRequest sr
	INNER JOIN Team pt ON sr.ProviderTeamID=pt.ID AND pt.SessionGroupID = '+CAST(@SessionGroupID AS VARCHAR)+' AND pt.Deleted=0'+ISNULL(' AND pt.ID='+CAST(@ProviderTeamID as varchar),'')+'
	INNER JOIN Team rt ON sr.RequesterTeamID=rt.ID AND rt.Deleted=0'+ISNULL(' AND rt.ID='+CAST(@requesterTeamID as varchar),'')+'
	INNER JOIN Dictionary ds ON ds.ID=sr.RequestState AND ds.Deleted=0'+ISNULL(' AND ds.ID='+CAST(@requestState as varchar),'')+'
	INNER JOIN Dictionary dt ON dt.ID=sr.RequestType AND dt.Deleted=0'+ISNULL(' AND dt.ID='+CAST(@requestType as varchar),'')+'
	INNER JOIN Student tCreator ON tCreator.ID=sr.CreatorID AND tCreator.Deleted=0'+ISNULL(' AND tCreator.ID='+CAST(@creatorId as varchar),'')+'
	LEFT JOIN Student tProvider ON tProvider.ID=sr.AssigneeID AND tProvider.Deleted=0 
WHERE sr.Deleted=0
	 '+ISNULL(' AND sr.RequestNumber='+CAST(@rowNumber as varchar),'')+''+ISNULL(' AND tProvider.ID='+CAST(@assigneeId as varchar),'')+'
ORDER BY sr.RequestState'
print @sql

EXEC (@sql)
END