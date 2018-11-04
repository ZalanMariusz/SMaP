--CREATE
ALTER TABLE Semester ADD CONSTRAINT FK_Semester_SemesterType_Dictionary FOREIGN KEY (SemesterType) REFERENCES Dictionary(ID)
ALTER TABLE SessionGroup ADD CONSTRAINT FK_SessionGroup_SemesterID_Semester FOREIGN KEY(SemesterID) REFERENCES Semester(ID)
ALTER TABLE Teacher ADD CONSTRAINT FK_Teacher_UserID_User FOREIGN KEY (UserID) References Users(ID)
ALTER TABLE Student ADD CONSTRAINT FK_Student_UserID_User FOREIGN KEY (UserID) References Users(ID)
ALTER TABLE Team ADD CONSTRAINT	 FK_Team_TeamCaption_Student FOREIGN KEY (TeamCaptain) References Student(ID)
ALTER TABLE Team ADD CONSTRAINT FK_Team_SessionGroupID_SessionGroup FOREIGN KEY (SessionGroupID) References SessionGroup(ID)
ALTER TABLE SessionGroup ADD CONSTRAINT FK_SessionGroup_TeacherID_Teacher FOREIGN KEY (TeacherID) References Teacher(ID)
ALTER TABLE Dictionary ADD CONSTRAINT FK_Dictionary_DictionaryTypeID_DictionaryType FOREIGN KEY (DictionaryTypeID) References DictionaryType(ID)
ALTER TABLE ServiceStore ADD CONSTRAINT FK_ServiceStore_CreatorID_User FOREIGN KEY (CreatorID) References Student(ID)
ALTER TABLE ServiceStore ADD CONSTRAINT FK_ServiceStore_RequesterTeamID_Team FOREIGN KEY (RequesterTeamID) References Team(ID)
ALTER TABLE ServiceStore ADD CONSTRAINT FK_ServiceStore_ProviderTeamID_Team FOREIGN KEY (ProviderTeamID) References Team(ID)
ALTER TABLE ServiceRequest ADD CONSTRAINT FK_ServiceRequest_RequesterTeamID_Team FOREIGN KEY (RequesterTeamID) References Team(ID)
ALTER TABLE ServiceRequest ADD CONSTRAINT FK_ServiceRequest_ProviderTeamID_Team FOREIGN KEY (ProviderTeamID) References Team(ID)
ALTER TABLE ServiceRequest ADD CONSTRAINT FK_ServiceRequest_ServiceID_Service FOREIGN KEY (ServiceID) References ServiceStore(ID)
ALTER TABLE ServiceTable ADD CONSTRAINT FK_ServiceTable_TeamID_Team FOREIGN KEY (TeamID) References Team(ID)
ALTER TABLE ServiceTableField ADD CONSTRAINT FK_ServiceTableField_TableID_ServiceTable FOREIGN KEY (TableID) References ServiceTable(ID)
ALTER TABLE ServiceTableField ADD CONSTRAINT FK_ServiceTableField_FieldTypeID_Dictionary FOREIGN KEY (FieldTypeID) References Dictionary(ID)
ALTER TABLE ServiceStoreParams ADD CONSTRAINT FK_ServiceStoreParams_ServiceTableFieldID_ServiceTableField FOREIGN KEY (ServiceTableFieldID) References ServiceTableField(ID)
ALTER TABLE ServiceStoreParams ADD CONSTRAINT FK_ServiceStoreParams_ServiceID_ServiceStore FOREIGN KEY (ServiceID) References ServiceStore(ID)

