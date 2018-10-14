using SMaP_APP.Commands;
using SMaP_APP.DAL;
using SMaP_APP.Model;
using SMaP_APP.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SMaP_APP.ViewModel
{
    class TeacherEditorWindowViewModel : BaseViewModel<Teacher>
    {
        public Teacher SelectedTeacher { get; set; }
        public RelayCommand SaveCommand { get; set; }
        private UsersDAL UsersDAL { get; set; }
        public TeacherEditorWindowViewModel(TeacherEditorWindow teacherEditorWindow, Teacher teacher)
        {
            this.SourceWindow = teacherEditorWindow;
            this._contextDal = new TeacherDAL(DbContext);
            UsersDAL = new UsersDAL(DbContext);
            this.SelectedTeacher = teacher;
            this.SaveCommand = new RelayCommand(SaveTeacher, CanSaveTeacher);
            if (teacher.Users == null)
            {
                teacher.Users = new Users();
            }
        }
        private void SaveTeacher()
        {
            Users existingUser = UsersDAL.FindAll(x => (x.Email == SelectedTeacher.Users.Email || x.NEPTUN == SelectedTeacher.Users.NEPTUN) && x.ID != SelectedTeacher.UserID).FirstOrDefault();
            if (existingUser != null)
            {
                MessageBox.Show("Létezik már felhasználó ezzel a Neptun-kóddal vagy E-mail címmel!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (this._contextDal.FindById(SelectedTeacher.ID) == null)
                {
                    this.SelectedTeacher.Users.FullName = String.Format("{0} {1}", SelectedTeacher.Users.LastName, SelectedTeacher.Users.FirstName);
                    this.SelectedTeacher.Users.UserName = SelectedTeacher.Users.Email;
                    this.SelectedTeacher.Users.UserPassword = UsersDAL.ComputeSha256Hash(SelectedTeacher.Users.Email);
                    this.UsersDAL.Create(SelectedTeacher.Users);
                    SelectedTeacher.UserID = SelectedTeacher.Users.ID;
                    this._contextDal.Create(SelectedTeacher);
                }
                else
                {
                    this.SelectedTeacher.Users.FullName = String.Format("{0} {1}", SelectedTeacher.Users.LastName, SelectedTeacher.Users.FirstName);
                    this.UsersDAL.Update(SelectedTeacher.Users);
                    this._contextDal.Update(SelectedTeacher);
                }
                this.SourceWindow.Close();
            }
        }
        private bool CanSaveTeacher()
        {
            if (!String.IsNullOrEmpty(SelectedTeacher.Users.LastName) && !String.IsNullOrEmpty(SelectedTeacher.Users.FirstName) &&
                !String.IsNullOrEmpty(SelectedTeacher.Users.NEPTUN) && !String.IsNullOrEmpty(SelectedTeacher.Users.Email)
                )
            {
                return true;
            }
            return false;
        }
    }
}
