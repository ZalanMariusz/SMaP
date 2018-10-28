using SMaP_APP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMaP_APP.DAL
{
    class TeamDAL: GenericDAL<Team> 
    {
        public List<Student> StudentList { get; private set; }
        public List<SessionGroup> SessionGroupList { get; private set; }
        
        public TeamDAL()
        {
            this.StudentList= applicationDbContext.Set<Student>().Where(x => !x.Deleted).ToList();
            this.SessionGroupList = applicationDbContext.Set<SessionGroup>().Where(x => !x.Deleted && x.Semester.IsActive).ToList();
        }
    }
}
