using SMaP_APP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMaP_APP.DAL
{
    class TeacherDAL:GenericDAL<Teacher>
    {
        public TeacherDAL(SMaPEntities applicationDbContext) : base(applicationDbContext)
        {
            
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
    }
}
