using SMaP_APP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMaP_APP.DAL
{
    class TeacherDAL : GenericDAL<Teacher>
    {
        public TeacherDAL()
        {

        }

        public List<SessionGroup> ActiveSessionGroupList()
        {
            RefreshContext();
            return applicationDbContext.SessionGroup.Where(x => !x.Deleted && x.Semester.IsActive).ToList();
        }
        public List<Team> ActiveTeamList()
        {
            RefreshContext();
            return applicationDbContext.Team.Where(x => !x.Deleted && x.SessionGroup.Semester.IsActive).ToList();
        }
        public List<Semester> SemesterList()
        {
            RefreshContext();
            return applicationDbContext.Semester.Where(x => !x.Deleted).ToList();
        }

        public void DeleteSemester(Semester param)
        {
            RefreshContext();
            SemesterDAL sd = new SemesterDAL();
            sd.LogicalDelete(param);
        }
        public void DeleteTeacherUser(Teacher param)
        {
            RefreshContext();
            UsersDAL ud = new UsersDAL();
            ud.LogicalDelete(ud.FindAll(x=>x.ID==param.UserID).FirstOrDefault());
        }
        public void DeleteSessionGroup(SessionGroup param)
        {
            RefreshContext();
            SessionGroupDAL sgd = new SessionGroupDAL();
            sgd.LogicalDelete(param);
        }
        public void DeleteTeam(Team param)
        {
            RefreshContext();
            TeamDAL td = new TeamDAL();
            td.LogicalDelete(param);
        }

        public List<Student> StudentList(int? sessionGroupID, int? teamID, int? TeacherID)
        {
            RefreshContext();
            return applicationDbContext.uspGetActiveStudents(sessionGroupID, teamID, TeacherID).ToList();
        }

        public void DeleteStudent(Student param)
        {
            RefreshContext();
            StudentDAL sd = new StudentDAL();
            sd.LogicalDelete(param);
        }
        public void DeleteStudentUser(Student param)
        {
            RefreshContext();
            UsersDAL ud = new UsersDAL();
            ud.LogicalDelete(ud.FindAll(x => x.ID == param.UserID).FirstOrDefault());
        }

        public List<Dictionary> DictionaryList()
        {
            RefreshContext();
            return applicationDbContext.Dictionary.Where(x => !x.Deleted).ToList();
        }

        public List<DictionaryType> DictionaryTypeList()
        {
            RefreshContext();
            return applicationDbContext.DictionaryType.Where(x => !x.Deleted).ToList();
        }

        public void DeleteDictionary(Dictionary param)
        {
            RefreshContext();
            DictionaryDAL dd = new DictionaryDAL();
            dd.LogicalDelete(param);
        }

        public void DeleteDictionaryType(DictionaryType param)
        {
            RefreshContext();
            DictionaryTypeDAL dtd = new DictionaryTypeDAL();
            dtd.LogicalDelete(param);
        }
    }
}
