using System;
using System.Data.Entity;
using System.Linq;

namespace SMaP_APP.DAL
{
    public class UsersDAL
    {
        public UsersDAL()
        {
               
        }
        public DbSet<Model.Users> GetUsers()
        {
            using (var Context = new Model.SMaPEntities())
            {
                return Context.Users;
            }
        }
        public Model.Users GetUserById(int ID)
        {
            using (var Context = new Model.SMaPEntities())
            {
                return Context.Users.Where(x=>x.ID==ID).FirstOrDefault();
            }
        }

        public Model.Users GetUserAuthenticate(string userName, string passwordHash)
        {
            using (var Context = new Model.SMaPEntities())
            {
                return Context.Users.Where(x => x.UserName == userName && x.UserPassword==passwordHash).FirstOrDefault();
            }
        }
    }
}
