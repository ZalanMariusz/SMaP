using SMaP_APP.Commands;
using SMaP_APP.DAL;
using SMaP_APP.Model;
using SMaP_APP.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SMaP_APP.ViewModel
{
    class PasswordChangeViewModel : BaseViewModel<Users>
    {
        public RelayCommand BackToLogin { get; set; }
        public RelayCommand ChangePassword { get; set; }
        public Users ContextUser { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string NEPTUN { get; set; }
        public PasswordChangeViewModel(PasswordChangeWindow sourceWindow)
        {
            this.SourceWindow = sourceWindow;
            this.BackToLogin = new RelayCommand(GoBack);
            this.ChangePassword = new RelayCommand(SavePassword, CanChange);
            _contextDal = new UsersDAL();
        }

        private bool CanChange()
        {

            return !(string.IsNullOrEmpty(NEPTUN) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(PasswordConfirm));

        }

        private void SavePassword()
        {
            Users userToMod = _contextDal.FindAll(x => x.NEPTUN == NEPTUN).FirstOrDefault();
            if (userToMod == null)
            {
                MessageBox.Show("Ehhez a NEPTUN kódhoz nem tartozik felhasználó!", "Ismeretlen NEPTUN", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (string.Compare(Password,PasswordConfirm, false) !=0)
            {
                MessageBox.Show("A két jelszó nem egyezik!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                userToMod.UserPassword = ComputeSha256Hash(Password);
                userToMod.IsPasswordChangeRequired = false;
                _contextDal.Update(userToMod);
                SwitchWindows(new LoginWindow());
            }


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
        private void GoBack()
        {
            SwitchWindows(new LoginWindow());
        }
    }
}
