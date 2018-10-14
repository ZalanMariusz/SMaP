using SMaP_APP.Commands;
using SMaP_APP.DAL;
using SMaP_APP.Model;
using SMaP_APP.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SMaP_APP.ViewModel
{
    class TeacherWindowViewModel : BaseViewModel<Teacher>
    {
        #region Commands
        public RelayCommand SemesterEditCommand { get; set; }
        public RelayCommand SemesterCreateCommand { get; set; }
        public RelayCommand SemesterDeleteCommand { get; set; }
        public RelayCommand TeacherEditCommand { get; set; }
        public RelayCommand TeacherCreateCommand { get; set; }
        public RelayCommand TeacherDeleteCommand { get; set; }
        public RelayCommand SessionGroupCreateCommand { get; set; }
        public RelayCommand SessionGroupEditCommand { get; set; }
        public RelayCommand SessionGroupDeleteCommand { get; set; }
        #endregion Commands

        #region Properties and Fields
        private ObservableCollection<Semester> semesterList;
        private ObservableCollection<Teacher> teacherList;
        private ObservableCollection<SessionGroup> sessionGroupList;
        public Teacher ContextTeacher { get; set; }
        public ObservableCollection<Semester> SemesterList
        {
            get { return semesterList; }
            set { this.semesterList = value; NotifyPropertyChanged(); }
        }
        public ObservableCollection<Teacher> TeacherList
        {
            get { return teacherList; }
            set { teacherList = value; NotifyPropertyChanged(); }
        }
        public ObservableCollection<SessionGroup> SessionGroupList
        {
            get { return sessionGroupList; }
            set { sessionGroupList = value; NotifyPropertyChanged(); }
        }
        #endregion Properties and Fields

        #region DALs
        public SemesterDal SemesterDal { get; set; }
        public SessionGroupDAL SessionGroupDal { get; set; }
        #endregion DALs

        public TeacherWindowViewModel(TeacherWindow teacherWindow, Teacher ContextTeacher)
        {
            this.SourceWindow = teacherWindow;
            this.ContextTeacher = ContextTeacher;

            this.SemesterCreateCommand = new RelayCommand(CreateSemester);
            this.SemesterEditCommand = new RelayCommand(EditSemester, CanEditSemester);
            this.SemesterDeleteCommand = new RelayCommand(DeleteSemester, CanDeleteSemester);

            this.TeacherCreateCommand = new RelayCommand(CreateTeacher);
            this.TeacherEditCommand = new RelayCommand(EditTeacher, CanEditTeacher);
            this.TeacherDeleteCommand = new RelayCommand(DeleteTeacher, CanDeleteTeacher);

            this.SessionGroupCreateCommand = new RelayCommand(CreateSessionGroup);
            this.SessionGroupEditCommand = new RelayCommand(EditSessionGroup, CanEditSessionGroup);
            this.SessionGroupDeleteCommand = new RelayCommand(DeleteSessionGroup, CanDeleteSessionGroup);

            this._contextDal = new TeacherDAL(DbContext);
            this.SessionGroupDal = new SessionGroupDAL(DbContext);
            this.SemesterDal = new SemesterDal(DbContext);

            this.SemesterList = new ObservableCollection<Semester>(SemesterDal.FindAll());
            this.TeacherList = new ObservableCollection<Teacher>(_contextDal.FindAll());
            this.SessionGroupList = new ObservableCollection<SessionGroup>(SessionGroupDal.FindAll());
        }

        #region CommandMethods
        private void CreateSemester()
        {
            Semester newSemester = new Semester();
            SemesterWindow target = new SemesterWindow(newSemester)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.SemesterList = new ObservableCollection<Semester>(SemesterDal.FindAll());
        }
        private void EditSemester(object param)
        {
            SemesterWindow target = new SemesterWindow((Semester)((ListBox)param).SelectedItem)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.SemesterList = new ObservableCollection<Semester>(SemesterDal.FindAll());
        }
        private void DeleteSemester(object param)
        {
            Semester selectedSemester = (Semester)((ListBox)param).SelectedItem;
            if (selectedSemester.IsActive == true)
            {
                MessageBox.Show("Aktív szemeszter nem törölhető!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Valóban törli?", "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    SemesterList.Remove(selectedSemester);
                    ((TeacherDAL)_contextDal).DeleteSemester(selectedSemester);
                }
            }
        }

        private void CreateTeacher()
        {
            Teacher newTeacher = new Teacher();
            TeacherEditorWindow target = new TeacherEditorWindow(newTeacher)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.TeacherList = new ObservableCollection<Teacher>(_contextDal.FindAll());
        }
        private void EditTeacher(object param)
        {
            TeacherEditorWindow target = new TeacherEditorWindow((Teacher)((ListBox)param).SelectedItem)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.TeacherList = new ObservableCollection<Teacher>(
               _contextDal.FindAll());
        }
        private void DeleteTeacher(object param)
        {
            Teacher selectedTeacher = (Teacher)((ListBox)param).SelectedItem;
            if (selectedTeacher.ID == ContextTeacher.ID)
            {
                MessageBox.Show("Bejelentkezett felhasználó nem törölhető!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Valóban törli?", "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    TeacherList.Remove(selectedTeacher);
                    ((TeacherDAL)_contextDal).DeleteTeacherUser(selectedTeacher);
                    _contextDal.LogicalDelete(selectedTeacher);
                }
            }
        }

        private void CreateSessionGroup()
        {
            SessionGroup sessionGroup = new SessionGroup();
            SessionGroupEditWindow target = new SessionGroupEditWindow(sessionGroup)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.SessionGroupList = new ObservableCollection<SessionGroup>(SessionGroupDal.FindAll());
        }
        private void EditSessionGroup(object param)
        { }
        private void DeleteSessionGroup(object param)
        { }
        #endregion CommandMethods
        #region CanExecutes
        private bool CanEditSemester(object param)
        {
            return (Semester)((ListBox)param).SelectedItem != null;
        }
        private bool CanDeleteSemester(object param)
        {
            return (Semester)((ListBox)param).SelectedItem != null;
        }

        private bool CanEditTeacher(object param)
        {
            return (Teacher)((ListBox)param).SelectedItem != null;
        }
        private bool CanDeleteTeacher(object param)
        {
            return (Teacher)((ListBox)param).SelectedItem != null;
        }

        private bool CanEditSessionGroup(object param)
        { return true; }
        private bool CanDeleteSessionGroup(object param)
        { return true; }
        #endregion CanExecutes
    }
}
