using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMaP_APP.ViewModel
{
    class UsersViewModel
    {
        private DAL.UsersDAL UsersDAL;
        public UsersViewModel()
        {
            this.UsersDAL = new DAL.UsersDAL();
        }
        public Model.Users Login(string UserName,string UserPasswordHash)
        {
            if (UsersDAL != null)
            {
                return UsersDAL.GetUsers().Where(x => x.UserName == UserName && x.UserPassword == UserPasswordHash).Single();
            }
            else
            {
                throw new NullReferenceException("No UsersDAL Initialized!");
            }
        }
    }
}
