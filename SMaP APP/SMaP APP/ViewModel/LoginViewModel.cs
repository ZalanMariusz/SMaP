using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SMaP_APP.ViewModel
{
    class LoginViewModel
    {
        private DAL.UsersDAL UsersDAL { get; set; }
        private Model.Users user { get; set; }
        public Commands.RelayCommand LoginCommand { get; set; }
        public string userName { get; set; }
        public string password { get; set; }


        public LoginViewModel()
        {
            this.UsersDAL = new DAL.UsersDAL();
            LoginCommand = new Commands.RelayCommand(LoginUser);
        }

        public void LoginUser(object NA)
        {
            string passwordHash = ComputeSha256Hash(password);
            if (UsersDAL != null)
            {
                user = UsersDAL.GetUserAuthenticate(userName, passwordHash);
            }
            else
            {
                throw new NullReferenceException("No UsersDAL Initialized!");
            }
        }

        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
