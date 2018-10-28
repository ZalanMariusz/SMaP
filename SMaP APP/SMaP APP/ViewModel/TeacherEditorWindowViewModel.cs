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
        private UsersDAL UsersDal { get; set; }
        public TeacherEditorWindowViewModel(TeacherEditorWindow teacherEditorWindow, Teacher teacher)
        {
            this.SourceWindow = teacherEditorWindow;
            this._contextDal = new TeacherDAL();
            UsersDal = new UsersDAL();
            this.SelectedTeacher = teacher;
            this.SaveCommand = new RelayCommand(SaveTeacher, CanExecute);
            if (teacher.Users == null)
            {
                teacher.Users = new Users();
            }
        }
        private void SaveTeacher()
        {
            Users existingUser = UsersDal.FindAll(x => (x.Email == SelectedTeacher.Users.Email || x.NEPTUN == SelectedTeacher.Users.NEPTUN) && x.ID != SelectedTeacher.UserID).FirstOrDefault();
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

                    Users u = SelectedTeacher.Users;
                    this.UsersDal.Create(u);
                    SelectedTeacher.UserID = u.ID;
                    u.Teacher.Add(SelectedTeacher);
                    this.UsersDal.Update(u);

                    //a = TeacherDAL.applicationDbContext.Teacher.Count(x=>!x.Deleted);
                    //SelectedTeacher.UserID = SelectedTeacher.Users.ID;
                    //this._contextDal.Create(SelectedTeacher);
                    //a = TeacherDAL.applicationDbContext.Teacher.Count(x => !x.Deleted);
                }
                else
                {
                    this.SelectedTeacher.Users.FullName = String.Format("{0} {1}", SelectedTeacher.Users.LastName, SelectedTeacher.Users.FirstName);
                    this.UsersDal.Update(SelectedTeacher.Users);
                    this._contextDal.Update(SelectedTeacher);
                }
                this.SourceWindow.Close();
            }
        }
    }
}
