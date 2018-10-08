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
            SemesterDal sa = new SemesterDal(this.applicationDbContext);
            sa.LogicalDelete(param);
        }
    }
}
