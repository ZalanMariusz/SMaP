--CREATE
ALTER TABLE Semester ADD CONSTRAINT FK_Semester_SemesterType_Dictionary FOREIGN KEY (SemesterType) REFERENCES Dictionary(ID)
ALTER TABLE SessionGroup ADD CONSTRAINT FK_SessionGroup_SemesterID_Semester FOREIGN KEY(SemesterID) REFERENCES Semester(ID)
ALTER TABLE Teacher ADD CONSTRAINT FK_Teacher_UserID_User FOREIGN KEY (UserID) References Users(ID)
ALTER TABLE Student ADD CONSTRAINT FK_Student_UserID_User FOREIGN KEY (UserID) References Users(ID)
ALTER TABLE Team ADD CONSTRAINT	 FK_Team_TeamCaption_Student FOREIGN KEY (TeamCaptain) References Student(ID)
ALTER TABLE Team ADD CONSTRAINT FK_Team_SessionGroupID_SessionGroup FOREIGN KEY (SessionGroupID) References SessionGroup(ID)
ALTER TABLE SessionGroup ADD CONSTRAINT FK_SessionGroup_TeacherID_Teacher FOREIGN KEY (TeacherID) References Teacher(ID)
ALTER TABLE Dictionary ADD CONSTRAINT FK_Dictionary_DictionaryTypeID_DictionaryType FOREIGN KEY (DictionaryTypeID) References DictionaryType(ID)


