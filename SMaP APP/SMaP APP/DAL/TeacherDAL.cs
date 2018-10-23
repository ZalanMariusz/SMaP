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
        public TeacherDAL(SMaPEntities applicationDbContext) : base(applicationDbContext)
        {

        }

        public List<SessionGroup> ActiveSessionGroupList()
        {
            return applicationDbContext.SessionGroup.Where(x => !x.Deleted && x.Semester.IsActive).ToList();
        }
        public List<Team> ActiveTeamList()
        {
            return applicationDbContext.Team.Where(x => !x.Deleted && x.SessionGroup.Semester.IsActive).ToList();
        }
        public List<Semester> SemesterList()
        {
            RefreshContext();
            var a = applicationDbContext.Semester.Where(x => !x.Deleted).ToList();
            return applicationDbContext.Semester.Where(x => !x.Deleted).ToList();
        }

        public void DeleteSemester(Semester param)
        {
            SemesterDAL sd = new SemesterDAL(this.applicationDbContext);
            sd.LogicalDelete(param);
        }
        public void DeleteTeacherUser(Teacher param)
        {
            UsersDAL ud = new UsersDAL(this.applicationDbContext);
            ud.LogicalDelete(param.Users);
        }
        public void DeleteSessionGroup(SessionGroup param)
        {
            SessionGroupDAL sgd = new SessionGroupDAL(this.applicationDbContext);
            sgd.LogicalDelete(param);
        }
        public void DeleteTeam(Team param)
        {
            TeamDAL td = new TeamDAL(this.applicationDbContext);
            td.LogicalDelete(param);
        }

        public List<Student> StudentList(int? sessionGroupID,int? teamID, int? TeacherID)
        {
            return applicationDbContext.uspGetActiveStudents(sessionGroupID, teamID, TeacherID).ToList();
        }

        internal void DeleteStudent(Student param)
        {
            StudentDAL sd = new StudentDAL(this.applicationDbContext);
            sd.LogicalDelete(param);
        }
    }
}
