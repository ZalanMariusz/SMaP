using SMaP_APP.Commands;
using SMaP_APP.DAL;
using SMaP_APP.Model;
using SMaP_APP.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SMaP_APP.ViewModel
{
    class StudentEditorWindowViewModel : BaseViewModel<Student>
    {
        public Student SelectedStudent { get; set; }
        public RelayCommand SaveCommand { get; set; }
        private StudentDAL StudentDal { get; set; }
        private UsersDAL UsersDal { get; set; }
        public ObservableCollection<Team> TeamList { get; set; }
        public StudentEditorWindowViewModel(StudentEditorWindow teacherEditorWindow, Student student)
        {
            this.SourceWindow = teacherEditorWindow;
            this._contextDal = new StudentDAL(DbContext);
            this.UsersDal = new UsersDAL(DbContext);
            this.SelectedStudent = student;
            this.SaveCommand = new RelayCommand(SaveStudent, CanSaveStudent);
            this.TeamList = new ObservableCollection<Team>(((StudentDAL)_contextDal).TeamList());
            if (SelectedStudent.Users == null)
            {
                SelectedStudent.Users = new Users();
            }
        }

        private bool CanSaveStudent()
        {
            if (!String.IsNullOrEmpty(SelectedStudent.Users.LastName) && !String.IsNullOrEmpty(SelectedStudent.Users.FirstName) &&
                !String.IsNullOrEmpty(SelectedStudent.Users.NEPTUN) && !String.IsNullOrEmpty(SelectedStudent.Users.Email)
                )
            {
                return true;
            }
            return false;
        }

        private void SaveStudent()
        {
            Users existingUser = UsersDal.FindAll(x => (x.Email == SelectedStudent.Users.Email || x.NEPTUN == SelectedStudent.Users.NEPTUN) && x.ID != SelectedStudent.UserID).FirstOrDefault();
            if (existingUser != null)
            {
                MessageBox.Show("Létezik már felhasználó ezzel a Neptun-kóddal vagy E-mail címmel!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (this._contextDal.FindById(SelectedStudent.ID) == null)
                {
                    this.SelectedStudent.Users.FullName = String.Format("{0} {1}", SelectedStudent.Users.LastName, SelectedStudent.Users.FirstName);
                    this.SelectedStudent.Users.UserName = SelectedStudent.Users.Email;
                    this.SelectedStudent.Users.UserPassword = UsersDAL.ComputeSha256Hash(SelectedStudent.Users.Email);
                    this.UsersDal.Create(SelectedStudent.Users);
                    SelectedStudent.UserID = SelectedStudent.Users.ID;
                    this._contextDal.Create(SelectedStudent);
                }
                else
                {
                    this.SelectedStudent.Users.FullName = String.Format("{0} {1}", SelectedStudent.Users.LastName, SelectedStudent.Users.FirstName);
                    this.UsersDal.Update(SelectedStudent.Users);
                    this._contextDal.Update(SelectedStudent);
                }
                this.SourceWindow.Close();
            }
        }
    }
}
