using SMaP_APP.Model;

namespace SMaP_APP.DAL
{
    public class UsersDAL : GenericDAL<Users>
    {
        public UsersDAL(SMaPEntities applicationDbContext): base(applicationDbContext)
        {
            
        }
    }
}
