using SMaP_APP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace SMaP_APP.DAL
{
    class StudentDAL : GenericDAL<Student>
    {
        public StudentDAL(SMaPEntities applicationDbContext): base(applicationDbContext)
        {
        }

        internal List<Team> TeamList()
        {
            return applicationDbContext.Team.Where(x => !x.Deleted && x.SessionGroup.Semester.IsActive).ToList();
        }
    }
}
