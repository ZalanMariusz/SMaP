--DROP
IF OBJECT_ID('FK_Semester_SemesterType_Dictionary') IS NOT NULL ALTER TABLE Semester DROP CONSTRAINT FK_Semester_SemesterType_Dictionary
IF OBJECT_ID('FK_SessionGroup_SemesterID_Semester') IS NOT NULL ALTER TABLE SessionGroup DROP CONSTRAINT FK_SessionGroup_SemesterID_Semester
IF OBJECT_ID('FK_Teacher_UserID_User') IS NOT NULL ALTER TABLE Teacher DROP CONSTRAINT FK_Teacher_UserID_User 
IF OBJECT_ID('FK_Student_UserID_User') IS NOT NULL ALTER TABLE Student DROP CONSTRAINT FK_Student_UserID_User 
IF OBJECT_ID('FK_Team_TeamCaption_Student') IS NOT NULL ALTER TABLE Team DROP CONSTRAINT FK_Team_TeamCaption_Student 
IF OBJECT_ID('FK_Team_SessionGroupID_SessionGroup') IS NOT NULL ALTER TABLE Team DROP CONSTRAINT FK_Team_SessionGroupID_SessionGroup
IF OBJECT_ID('FK_SessionGroup_TeacherID_Teacher') IS NOT NULL ALTER TABLE SessionGroup DROP CONSTRAINT FK_SessionGroup_TeacherID_Teacher
IF OBJECT_ID('FK_Dictionary_DictionaryTypeID_DictionaryType') IS NOT NULL ALTER TABLE Dictionary DROP CONSTRAINT FK_Dictionary_DictionaryTypeID_DictionaryType
IF OBJECT_ID('FK_ServiceStore_CreatorID_User') IS NOT NULL ALTER TABLE  ServiceStore DROP CONSTRAINT FK_ServiceStore_CreatorID_User
IF OBJECT_ID('FK_ServiceStore_ProviderTeamID_Team') IS NOT NULL ALTER TABLE ServiceStore DROP CONSTRAINT FK_ServiceStore_ProviderTeamID_Team
IF OBJECT_ID('FK_ServiceRequest_RequesterTeamID_Team') IS NOT NULL ALTER TABLE ServiceRequest DROP CONSTRAINT FK_ServiceRequest_RequesterTeamID_Team
IF OBJECT_ID('FK_ServiceRequest_ProviderTeamID_Team') IS NOT NULL ALTER TABLE ServiceRequest DROP CONSTRAINT FK_ServiceRequest_ProviderTeamID_Team
IF OBJECT_ID('FK_ServiceRequest_ServiceID_Service') IS NOT NULL ALTER TABLE ServiceRequest DROP CONSTRAINT FK_ServiceRequest_ServiceID_Service
IF OBJECT_ID('FK_ServiceRequest_RequestState_Dictionary') IS NOT NULL ALTER TABLE ServiceRequest DROP CONSTRAINT FK_ServiceRequest_RequestState_Dictionary
IF OBJECT_ID('FK_ServiceRequest_RequestType_Dictionary') IS NOT NULL ALTER TABLE ServiceRequest DROP CONSTRAINT FK_ServiceRequest_RequestType_Dictionary 
IF OBJECT_ID('FK_ServiceRequest_CreatorID_Student') IS NOT NULL ALTER TABLE ServiceRequest DROP CONSTRAINT FK_ServiceRequest_CreatorID_Student
IF OBJECT_ID('FK_ServiceRequest_AssigneeID_Student') IS NOT NULL ALTER TABLE ServiceRequest DROP CONSTRAINT FK_ServiceRequest_AssigneeID_Student
IF OBJECT_ID('FK_ServiceTable_TeamID_Team') IS NOT NULL ALTER TABLE ServiceTable DROP CONSTRAINT FK_ServiceTable_TeamID_Team
IF OBJECT_ID('FK_ServiceTableField_TableID_ServiceTable') IS NOT NULL ALTER TABLE ServiceTableField DROP CONSTRAINT FK_ServiceTableField_TableID_ServiceTable
IF OBJECT_ID('FK_ServiceTableField_FieldTypeID_Dictionary') IS NOT NULL ALTER TABLE ServiceTableField DROP CONSTRAINT FK_ServiceTableField_FieldTypeID_Dictionary
IF OBJECT_ID('FK_ServiceStoreParams_ServiceTableFieldID_ServiceTableField') IS NOT NULL ALTER TABLE ServiceStoreParams DROP CONSTRAINT FK_ServiceStoreParams_ServiceTableFieldID_ServiceTableField
IF OBJECT_ID('FK_ServiceStoreParams_ServiceID_ServiceStore') IS NOT NULL ALTER TABLE ServiceStoreParams DROP CONSTRAINT FK_ServiceStoreParams_ServiceID_ServiceStore
IF OBJECT_ID('FK_ServiceStoreParams_CustomParamTypeID_Dictionary') IS NOT NULL ALTER TABLE ServiceStoreParams DROP CONSTRAINT FK_ServiceStoreParams_CustomParamTypeID_Dictionary
IF OBJECT_ID('FK_ServiceStoreUserTeams_ServiceID_ServiceStore') IS NOT NULL ALTER TABLE ServiceStoreUserTeams DROP CONSTRAINT FK_ServiceStoreUserTeams_ServiceID_ServiceStore
IF OBJECT_ID('FK_ServiceStoreUserTeams_RequesterTeamID_Team') IS NOT NULL ALTER TABLE ServiceStoreUserTeams DROP CONSTRAINT FK_ServiceStoreUserTeams_RequesterTeamID_Team
IF OBJECT_ID('FK_ServiceStoreServiceParams_ParentServiceStoreID_ServiceStore') IS NOT NULL ALTER TABLE ServiceStoreServiceParams DROP CONSTRAINT FK_ServiceStoreServiceParams_ParentServiceStoreID_ServiceStore
IF OBJECT_ID('FK_ServiceStoreServiceParams_ParamServiceStoreID_ServiceStore') IS NOT NULL ALTER TABLE ServiceStoreServiceParams DROP CONSTRAINT FK_ServiceStoreServiceParams_ParamServiceStoreID_ServiceStore
IF OBJECT_ID('FK_RequestMessage_SenderID_Student') IS NOT NULL ALTER TABLE RequestMessage DROP CONSTRAINT FK_RequestMessage_SenderID_Student
IF OBJECT_ID('FK_RequestMessage_RequestID_ServiceRequest') IS NOT NULL  ALTER TABLE RequestMessage DROP CONSTRAINT FK_RequestMessage_RequestID_ServiceRequest
