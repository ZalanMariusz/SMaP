--DROP
IF OBJECT_ID('FK_Semester_SemesterType_Dictionary') IS NOT NULL ALTER TABLE Semester DROP CONSTRAINT FK_Semester_SemesterType_Dictionary
IF OBJECT_ID('FK_SessionGroup_SemesterID_Semester') IS NOT NULL ALTER TABLE SessionGroup DROP CONSTRAINT FK_SessionGroup_SemesterID_Semester
IF OBJECT_ID('FK_Teacher_UserID_User') IS NOT NULL ALTER TABLE Teacher DROP CONSTRAINT FK_Teacher_UserID_User 
IF OBJECT_ID('FK_Student_UserID_User') IS NOT NULL ALTER TABLE Student DROP CONSTRAINT FK_Student_UserID_User 
IF OBJECT_ID('FK_Team_TeamCaption_Student') IS NOT NULL ALTER TABLE Team DROP CONSTRAINT FK_Team_TeamCaption_Student 
IF OBJECT_ID('FK_Team_SessionGroupID_SessionGroup') IS NOT NULL ALTER TABLE Team DROP CONSTRAINT FK_Team_SessionGroupID_SessionGroup
IF OBJECT_ID('FK_SessionGroup_TeacherID_Teacher') IS NOT NULL ALTER TABLE SessionGroup DROP CONSTRAINT FK_SessionGroup_TeacherID_Teacher
IF OBJECT_ID('FK_Dictionary_DictionaryTypeID_DictionaryType') IS NOT NULL ALTER TABLE Dictionary DROP CONSTRAINT FK_Dictionary_DictionaryTypeID_DictionaryType
