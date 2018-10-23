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
        public RelayCommand TeamCreateCommand { get; set; }
        public RelayCommand TeamEditCommand { get; set; }
        public RelayCommand TeamDeleteCommand { get; set; }
        public RelayCommand DeleteFilter { get; set; }

        public RelayCommand StudentCreateCommand { get; set; }
        public RelayCommand StudentEditCommand { get; set; }
        public RelayCommand StudentDeleteCommand { get; set; }

        public RelayCommand LogoutCommand { get; set; }
        #endregion Commands

        #region Properties and Fields
        private ObservableCollection<Semester> semesterList;
        private ObservableCollection<Teacher> teacherList;
        private ObservableCollection<SessionGroup> sessionGroupList;
        private ObservableCollection<Team> teamList;
        private ObservableCollection<Student> studentList;
        public Teacher ContextTeacher { get; set; }
        private Teacher teacherFilter;
        private SessionGroup sessionGroupFilter;
        private Team teamFilter;

        public Teacher TeacherFilter {
            get { return teacherFilter; }
            set { teacherFilter = value; NotifyPropertyChanged("StudentList"); }
        }

        public SessionGroup SessionGroupFilter
        {
            get { return sessionGroupFilter; }
            set { sessionGroupFilter = value; NotifyPropertyChanged("StudentList"); }
        }
        public Team TeamFilter
        {
            get { return teamFilter; }
            set { teamFilter = value; NotifyPropertyChanged("StudentList"); }
        }

        public ObservableCollection<Semester> SemesterList
        {
            get { return semesterList; }
            set { this.semesterList = value; NotifyPropertyChanged(); NotifyPropertyChanged("SessionGroupList"); NotifyPropertyChanged("TeamList"); }
        }
        public ObservableCollection<Teacher> TeacherList
        {
            get { return teacherList; }
            set { teacherList = value; NotifyPropertyChanged(); NotifyPropertyChanged("SessionGroupList"); NotifyPropertyChanged("StudentList"); }
        }
        public ObservableCollection<SessionGroup> SessionGroupList
        {
            get { return sessionGroupList; }
            set { sessionGroupList = value; NotifyPropertyChanged(); NotifyPropertyChanged("TeamList"); }
        }
        public ObservableCollection<Team> TeamList
        {
            get { return teamList; }
            set { teamList = value; NotifyPropertyChanged(); }
        }
        public ObservableCollection<Student> StudentList
        {
            get { return ReloadStudentList(); }
            set { studentList = value; NotifyPropertyChanged(); }
        }
        #endregion Properties and Fields

        #region DALs
        #endregion DALs

        private ObservableCollection<SessionGroup> ReloadActiveSessionGroupList()
        {
            return new ObservableCollection<SessionGroup>(((TeacherDAL)_contextDal).ActiveSessionGroupList());
        }

        private ObservableCollection<Team> ReloadActiveTeamList()
        {
            return new ObservableCollection<Team>(((TeacherDAL)_contextDal).ActiveTeamList());
        }

        private ObservableCollection<Semester> ReloadSemesterList()
        {
            return new ObservableCollection<Semester>(((TeacherDAL)_contextDal).SemesterList());
        }

        private ObservableCollection<Student> ReloadStudentList()
        {
            int? SessionGroupFilterID = SessionGroupFilter == null ? null : (int?)SessionGroupFilter.ID;
            int? TeamFilterID= TeamFilter == null ? null : (int?)TeamFilter.ID;
            int? TeacherFilterID= TeacherFilter == null ? null : (int?)TeacherFilter.ID;
            return new ObservableCollection<Student>(((TeacherDAL)_contextDal).StudentList(SessionGroupFilterID, TeamFilterID, TeacherFilterID));
        }

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

            this.TeamCreateCommand = new RelayCommand(CreateTeam);
            this.TeamEditCommand = new RelayCommand(EditTeam, CanEditTeam);
            this.TeamDeleteCommand = new RelayCommand(DeleteTeam, CanDeleteTeam);

            this.StudentCreateCommand = new RelayCommand(CreateStudent);
            this.StudentEditCommand = new RelayCommand(EditStudent, CanEditStudent);
            this.StudentDeleteCommand = new RelayCommand(DeleteStudent, CanDeleteStudent);

            this.DeleteFilter = new RelayCommand(DeleteSelectedFilter);
            this._contextDal = new TeacherDAL(DbContext);

            this.SemesterList = ReloadSemesterList();
            this.TeacherList = new ObservableCollection<Teacher>(_contextDal.FindAll());
            this.SessionGroupList = ReloadActiveSessionGroupList();
            this.TeamList = ReloadActiveTeamList();
            this.StudentList = ReloadStudentList();

            this.LogoutCommand = new RelayCommand(Logout);
        }

        private void DeleteSelectedFilter(object param)
        {
            ComboBox cb = (ComboBox)param;
            switch (cb.Name)
            {
                case "TeacherCB":
                    TeacherFilter = null;
                    break;
                case "SessionGroupCB":
                    SessionGroupFilter = null;
                    break;
                case "TeamCB":
                    TeamFilter = null;
                    break;
            }
            ((ComboBox)param).SelectedItem = null;
        }

        private void Logout()
        {
            SwitchWindows(new LoginWindow());
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
            this.SemesterList = ReloadSemesterList();
        }
        private void EditSemester(object param)
        {
            SemesterWindow target = new SemesterWindow((Semester)((ListBox)param).SelectedItem)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.SemesterList = ReloadSemesterList();
            this.SessionGroupList = ReloadActiveSessionGroupList();
            this.TeamList = ReloadActiveTeamList();
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
            this.SessionGroupList = ReloadActiveSessionGroupList();
            this.TeamList = ReloadActiveTeamList();
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
            SessionGroup sessionGroup = new SessionGroup
            {
                TeacherID = ContextTeacher.ID
            };
            SessionGroupEditWindow target = new SessionGroupEditWindow(sessionGroup)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.SessionGroupList = ReloadActiveSessionGroupList();
        }
        private void EditSessionGroup(object param)
        {
            SessionGroupEditWindow target = new SessionGroupEditWindow((SessionGroup)((ListBox)param).SelectedItem)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.SessionGroupList = ReloadActiveSessionGroupList();
            this.TeamList = ReloadActiveTeamList();
        }
        private void DeleteSessionGroup(object param)
        {
            SessionGroup selectedSessionGroup = (SessionGroup)((ListBox)param).SelectedItem;
            MessageBoxResult messageBoxResult = MessageBox.Show("Valóban törli?", "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                SessionGroupList.Remove(selectedSessionGroup);
                ((TeacherDAL)_contextDal).DeleteSessionGroup(selectedSessionGroup);
            }
        }

        private void CreateTeam()
        {
            Team team = new Team();
            TeamEditorWindow target = new TeamEditorWindow(team)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.TeamList = ReloadActiveTeamList();
        }
        private void EditTeam(object param)
        {
            TeamEditorWindow target = new TeamEditorWindow((Team)((ListBox)param).SelectedItem)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.TeamList = ReloadActiveTeamList();
        }
        private void DeleteTeam(object param)
        {
            Team selectedTeam = (Team)((ListBox)param).SelectedItem;
            MessageBoxResult messageBoxResult = MessageBox.Show("Valóban törli?", "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                TeamList.Remove(selectedTeam);
                ((TeacherDAL)_contextDal).DeleteTeam(selectedTeam);
            }
        }

        private void CreateStudent()
        {
            Student student = new Student();
            StudentEditorWindow target = new StudentEditorWindow(student)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.StudentList = ReloadStudentList();
        }
        private void EditStudent(object param)
        {
            StudentEditorWindow target = new StudentEditorWindow((Student)((DataGrid)param).SelectedItem)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.StudentList = ReloadStudentList();
        }
        private void DeleteStudent(object param)
        {
            Student selectedStudent= (Student)((DataGrid)param).SelectedItem;
            MessageBoxResult messageBoxResult = MessageBox.Show("Valóban törli?", "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                StudentList.Remove(selectedStudent);
                ((TeacherDAL)_contextDal).DeleteStudent(selectedStudent);
            }
        }
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
        {
            return (SessionGroup)((ListBox)param).SelectedItem != null;
        }
        private bool CanDeleteSessionGroup(object param)
        {
            return (SessionGroup)((ListBox)param).SelectedItem != null;
        }

        private bool CanEditTeam(object param)
        {
            return (Team)((ListBox)param).SelectedItem != null;
        }
        private bool CanDeleteTeam(object param)
        {
            return (Team)((ListBox)param).SelectedItem != null;
        }

        private bool CanDeleteStudent(object param)
        {
            return (Student)((DataGrid)param).SelectedItem != null;
        }

        private bool CanEditStudent(object param)
        {
            return (Student)((DataGrid)param).SelectedItem != null;
        }
        #endregion CanExecutes
    }
}
