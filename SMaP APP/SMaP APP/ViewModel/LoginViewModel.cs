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
    class LoginWindowViewModel:BaseViewModel<Users>
    {
        //private UsersDAL UsersDAL { get; set; }
        private Users User { get; set; }
        private LoginWindow LoginWindow { get; set; }
        public RelayCommand LoginCommand { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public LoginWindowViewModel(LoginWindow loginWindow)
        {
            this._contextDal = new UsersDAL();
            this.LoginCommand = new RelayCommand(LoginUser, CanLogin);
            this.SourceWindow = loginWindow;
        }

        public void LoginUser()
        {
            string passwordHash = UsersDAL.ComputeSha256Hash(Password);
            User = _contextDal.FindAll(x => x.UserName==UserName && x.UserPassword == passwordHash).FirstOrDefault();
            if (User == null)
            {
                MessageBox.Show("Ismeretlen felhasználónév vagy jelszó!", "Sikertelen belépés", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var contextTeacher = _contextDal.applicationDbContext.Teacher.Where(x => x.UserID == User.ID && !x.Deleted).FirstOrDefault();
                if (contextTeacher == null)
                {
                    var contextStudent= _contextDal.applicationDbContext.Student.Where(x => x.UserID == User.ID && !x.Deleted
                        && _contextDal.applicationDbContext.Team.FirstOrDefault(y=>y.ID==x.TeamID).SessionGroup.Semester.IsActive
                    ).FirstOrDefault();
                    if (contextStudent == null)
                    {
                        MessageBox.Show("Érvénytelen jogosultság!", "Érvénytelen jogosultság!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        SwitchWindows(new ServiceStoreWindow(contextStudent));
                    }
                }
                else
                {
                    SwitchWindows(new TeacherWindow(contextTeacher));
                }
            }
        }

        public bool CanLogin()
        {
            return !(String.IsNullOrEmpty(UserName) || String.IsNullOrEmpty(Password));
        }

        static string ComputeSha256Hash(string rawPassword)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawPassword));

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
