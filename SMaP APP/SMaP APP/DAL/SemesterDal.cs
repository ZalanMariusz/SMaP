using SMaP_APP.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMaP_APP.DAL
{
    class SemesterDal : GenericDAL<Semester>
    {
        public List<Dictionary> SemesterTypes { get; private set; }
        //public List<Semester> AllSemesters { get; set; }
        public SemesterDal(SMaPEntities applicationDbContext) : base(applicationDbContext)
        {
            this.SemesterTypes = applicationDbContext.Set<Dictionary>().Where(x => !x.Deleted && x.ItemType == "SemesterType").ToList();
        }
    }
}
