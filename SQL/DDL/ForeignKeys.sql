--DROP
IF OBJECT_ID('FK_Semester_SemesterType_Dictionary') IS NOT NULL ALTER TABLE Semester DROP CONSTRAINT FK_Semester_SemesterType_Dictionary
IF OBJECT_ID('FK_SessionGroup_SemesterID_Semester') IS NOT NULL ALTER TABLE SessionGroup DROP CONSTRAINT FK_SessionGroup_SemesterID_Semester
IF OBJECT_ID('FK_Teacher_UserID_User') IS NOT NULL ALTER TABLE Teacher DROP CONSTRAINT FK_Teacher_UserID_User 
IF OBJECT_ID('FK_Teacher_DefaultSessionGroupID_SessionGroup') IS NOT NULL ALTER TABLE Teacher DROP CONSTRAINT FK_Teacher_DefaultSessionGroupID_SessionGroup
IF OBJECT_ID('FK_Student_UserID_User') IS NOT NULL ALTER TABLE Student DROP CONSTRAINT FK_Student_UserID_User 
IF OBJECT_ID('FK_Student_DefaultSessionGroupID_SessionGroup') IS NOT NULL ALTER TABLE Student DROP CONSTRAINT FK_Student_DefaultSessionGroupID_SessionGroup
IF OBJECT_ID('FK_Team_TeamCaption_Student') IS NOT NULL ALTER TABLE Team DROP CONSTRAINT FK_Team_TeamCaption_Student 
IF OBJECT_ID('FK_Team_SessionGroupID_SessionGroup') IS NOT NULL ALTER TABLE Team DROP CONSTRAINT FK_Team_SessionGroupID_SessionGroup
--CREATE
ALTER TABLE Semester ADD CONSTRAINT FK_Semester_SemesterType_Dictionary FOREIGN KEY (SemesterType) REFERENCES Dictionary(ID)
ALTER TABLE SessionGroup ADD CONSTRAINT FK_SessionGroup_SemesterID_Semester FOREIGN KEY(SemesterID) REFERENCES Semester(ID)
ALTER TABLE Teacher ADD CONSTRAINT FK_Teacher_UserID_User FOREIGN KEY (UserID) References Users(ID)
ALTER TABLE Teacher ADD CONSTRAINT FK_Teacher_DefaultSessionGroupID_SessionGroup FOREIGN KEY (DefaultSessionGroupID) References SessionGroup(ID)
ALTER TABLE Student ADD CONSTRAINT FK_Student_UserID_User FOREIGN KEY (UserID) References Users(ID)
ALTER TABLE Student ADD CONSTRAINT FK_Student_DefaultSessionGroupID_SessionGroup FOREIGN KEY (UserID) references SessionGroup(ID)
ALTER TABLE Team ADD CONSTRAINT	 FK_Team_TeamCaption_Student FOREIGN KEY (TeamCaptain) References Student(ID)
ALTER TABLE Team ADD CONSTRAINT FK_Team_SessionGroupID_SessionGroup FOREIGN KEY (SessionGroupID) References SessionGroup(ID)


