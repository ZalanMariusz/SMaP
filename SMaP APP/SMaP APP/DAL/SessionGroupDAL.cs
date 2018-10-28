using SMaP_APP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMaP_APP.DAL
{
    class SessionGroupDAL : GenericDAL<SessionGroup>
    {
        public List<Teacher> TeacherList { get; private set; }
        public List<Semester> SemesterList { get; private set; }

        public SessionGroupDAL()
        {
            TeacherList = applicationDbContext.Set<Teacher>().Where(x => !x.Deleted).ToList();
            SemesterList = applicationDbContext.Set<Semester>().Where(x => !x.Deleted && x.IsActive).ToList();
        }

        public void AddTeams(int sessionGroupID)
        {
            TeamDAL TeamDal = new TeamDAL();
            DictionaryDAL dd = new DictionaryDAL();

            List<Dictionary> teams= dd.FindAll(x => !x.Deleted && x.DictionaryType.TypeName == "Csapatok");
            foreach (Dictionary item in teams)
            {
                TeamDal.Create(new Team { SessionGroupID = sessionGroupID, TeamName = item.ItemName });
            }
        }
    }
}
