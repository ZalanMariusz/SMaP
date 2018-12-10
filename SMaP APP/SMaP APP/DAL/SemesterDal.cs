using SMaP_APP.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMaP_APP.DAL
{
    class SemesterDAL : GenericDAL<Semester>
    {
        public List<Dictionary> SemesterTypes { get; private set; }
        public SemesterDAL()
        {
            this.SemesterTypes = applicationDbContext.Set<Dictionary>().Where(x => !x.Deleted && x.DictionaryTypeID==1).ToList();
        }
        public void CopySemester(int sourceId,string newSemesterName, int newSemesterTypedId,string SessionGroups,string Teams,string Students)
        {
            RefreshContext();
            applicationDbContext.uspCopySemester(sourceId, newSemesterName, newSemesterTypedId,SessionGroups,Teams,Students);
        }
    }
}
