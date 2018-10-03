using SMaP_APP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SMaP_APP.Model;
using System.Windows;
using SMaP_APP.Commands;
using SMaP_APP.View;
using System.Windows.Input;

namespace SMaP_APP.ViewModel
{
    class LoginWindowViewModel:BaseViewModel
    {
        private UsersDAL UsersDAL { get; set; }
        private Users User { get; set; }
        private LoginWindow LoginWindow { get; set; }
        public RelayCommand LoginCommand { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        private static SMaPEntities _dbContext = null;
        public static SMaPEntities DbContext
        {
            get
            {
                if (_dbContext == null)
                    _dbContext = new SMaPEntities();
                return _dbContext;
            }
        }

        public LoginWindowViewModel(LoginWindow loginWindow)
        {
            this.UsersDAL = new UsersDAL(DbContext);
            this.LoginCommand = new RelayCommand(LoginUser, CanLogin);
            this.SourceWindow = loginWindow;
        }

        public void LoginUser(object NA)
        {
            string passwordHash = ComputeSha256Hash(Password);
            User = UsersDAL.FindAll(x => String.Equals(x.UserName,UserName) && x.UserPassword == passwordHash).FirstOrDefault();
            if (User == null)
                MessageBox.Show("Ismeretlen felhasználónév vagy jelszó!", "Sikertelen belépés", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                SwitchWindows(new TeacherWindow());
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
        private bool CanLogin(object NA)
        {
            return !(String.IsNullOrEmpty(UserName) || String.IsNullOrEmpty(Password));
        }
    }
}
