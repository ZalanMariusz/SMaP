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
        //private GenericDAL<Teacher> _teacherDal;
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

        public RelayCommand StudentCreateCommand { get; set; }
        public RelayCommand StudentEditCommand { get; set; }
        public RelayCommand StudentDeleteCommand { get; set; }

        public RelayCommand DictionaryCreateCommand { get; set; }
        public RelayCommand DictionaryEditCommand { get; set; }
        public RelayCommand DictionaryDeleteCommand { get; set; }

        public RelayCommand DictionaryTypeCreateCommand { get; set; }
        public RelayCommand DictionaryTypeEditCommand { get; set; }
        public RelayCommand DictionaryTypeDeleteCommand { get; set; }

        public RelayCommand CopySemesterCommand { get; set; }
        public RelayCommand DeleteFilter { get; set; }
        public RelayCommand LogoutCommand { get; set; }
        #endregion Commands

        #region Properties and Fields
        private ObservableCollection<Semester> semesterList;
        private ObservableCollection<Teacher> teacherList;
        private ObservableCollection<SessionGroup> sessionGroupList;
        private ObservableCollection<Team> teamList;
        private ObservableCollection<Team> teamFilterList;
        private ObservableCollection<Student> studentList;
        private ObservableCollection<Dictionary> dictionaryList;
        private ObservableCollection<DictionaryType> dictionaryTypeList;

        public Teacher ContextTeacher { get; set; }

        private Teacher teacherFilter;
        private SessionGroup sessionGroupFilter;
        private SessionGroup sessionGroupTeamFilter;
        private Team teamFilter;
        private DictionaryType dictionaryTypeFilter;

        public DictionaryType DictionaryTypeFilter
        {
            get { return dictionaryTypeFilter; }
            set { dictionaryTypeFilter = value; NotifyPropertyChanged(); DictionaryList = ReloadDictionaryList(); }
        }
        public SessionGroup SessionGroupTeamFilter
        {
            get { return sessionGroupTeamFilter; }
            set { sessionGroupTeamFilter = value; NotifyPropertyChanged(); TeamList = ReloadActiveTeamList(); }
        }

        public Teacher TeacherFilter
        {
            get { return teacherFilter; }
            set { teacherFilter = value; this.StudentList = ReloadStudentList(); }
        }

        public SessionGroup SessionGroupFilter
        {
            get { return sessionGroupFilter; }
            set { sessionGroupFilter = value; this.StudentList = ReloadStudentList(); }
        }
        public Team TeamFilter
        {
            get { return teamFilter; }
            set { teamFilter = value; this.StudentList = ReloadStudentList(); }
        }

        public ObservableCollection<Dictionary> DictionaryList
        {
            get { return dictionaryList; }
            set { this.dictionaryList = value; NotifyPropertyChanged(); }
        }

        public ObservableCollection<DictionaryType> DictionaryTypeList
        {
            get { return dictionaryTypeList; }
            set { this.dictionaryTypeList = value; NotifyPropertyChanged(); }
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
        public ObservableCollection<Team> TeamFilterList
        {
            get { return teamFilterList; }
            set { teamFilterList = value; NotifyPropertyChanged(); }
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

        private ObservableCollection<SessionGroup> ReloadActiveSessionGroupList()
        {
            return new ObservableCollection<SessionGroup>(((TeacherDAL)_contextDal).ActiveSessionGroupList());
        }

        private ObservableCollection<Team> ReloadActiveTeamList()
        {
            if (SessionGroupTeamFilter != null)
            {
                return new ObservableCollection<Team>(((TeacherDAL)_contextDal).ActiveTeamList().Where(x => x.SessionGroupID == SessionGroupTeamFilter.ID));
            }
            return new ObservableCollection<Team>(((TeacherDAL)_contextDal).ActiveTeamList());
        }

        private ObservableCollection<Team> ReloadTeamFilterList()
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
            int? TeamFilterID = TeamFilter == null ? null : (int?)TeamFilter.ID;
            int? TeacherFilterID = TeacherFilter == null ? null : (int?)TeacherFilter.ID;
            return new ObservableCollection<Student>(((TeacherDAL)_contextDal).StudentList(SessionGroupFilterID, TeamFilterID, TeacherFilterID));
        }

        private ObservableCollection<Dictionary> ReloadDictionaryList()
        {
            if (DictionaryTypeFilter != null)
            {
                return new ObservableCollection<Dictionary>(((TeacherDAL)_contextDal).DictionaryList().Where(x=>x.DictionaryTypeID==dictionaryTypeFilter.ID));
            }
            return new ObservableCollection<Dictionary>(((TeacherDAL)_contextDal).DictionaryList());
        }

        private ObservableCollection<DictionaryType> ReloadDictionaryTypeList()
        {
            return new ObservableCollection<DictionaryType>(((TeacherDAL)_contextDal).DictionaryTypeList());
        }


        public TeacherWindowViewModel(TeacherWindow teacherWindow, Teacher ContextTeacher)
        {
            this.SourceWindow = teacherWindow;
            this.ContextTeacher = ContextTeacher;

            this.SemesterCreateCommand = new RelayCommand(CreateSemester);
            this.SemesterEditCommand = new RelayCommand(EditSemester, CanEditOrDeleteSelectedItem);
            this.SemesterDeleteCommand = new RelayCommand(DeleteSemester, CanEditOrDeleteSelectedItem);

            this.TeacherCreateCommand = new RelayCommand(CreateTeacher);
            this.TeacherEditCommand = new RelayCommand(EditTeacher, CanEditOrDeleteSelectedItem);
            this.TeacherDeleteCommand = new RelayCommand(DeleteTeacher, CanEditOrDeleteSelectedItem);

            this.SessionGroupCreateCommand = new RelayCommand(CreateSessionGroup);
            this.SessionGroupEditCommand = new RelayCommand(EditSessionGroup, CanEditOrDeleteSelectedItem);
            this.SessionGroupDeleteCommand = new RelayCommand(DeleteSessionGroup, CanEditOrDeleteSelectedItem);

            this.TeamCreateCommand = new RelayCommand(CreateTeam);
            this.TeamEditCommand = new RelayCommand(EditTeam, CanEditOrDeleteSelectedItem);
            this.TeamDeleteCommand = new RelayCommand(DeleteTeam, CanEditOrDeleteSelectedItem);

            this.StudentCreateCommand = new RelayCommand(CreateStudent);
            this.StudentEditCommand = new RelayCommand(EditStudent, CanEditOrDeleteSelectedItem);
            this.StudentDeleteCommand = new RelayCommand(DeleteStudent, CanEditOrDeleteSelectedItem);

            this.DictionaryCreateCommand = new RelayCommand(CreateDictionary);
            this.DictionaryEditCommand = new RelayCommand(EditDictionary, CanEditOrDeleteSelectedItem);
            this.DictionaryDeleteCommand = new RelayCommand(DeleteDictionary, CanDeleteDictionary);

            this.DictionaryTypeCreateCommand = new RelayCommand(CreateDictionaryType);
            this.DictionaryTypeEditCommand = new RelayCommand(EditDictionaryType, CanEditOrDeleteSelectedItem);
            this.DictionaryTypeDeleteCommand = new RelayCommand(DeleteDictionaryType, CanEditOrDeleteSelectedItem);

            this.CopySemesterCommand = new RelayCommand(CopySelectedSemester, CanCopySemester);
            this.DeleteFilter = new RelayCommand(DeleteSelectedFilter);
            this._contextDal = new TeacherDAL();

            this.SemesterList = ReloadSemesterList();
            this.TeacherList = new ObservableCollection<Teacher>(_contextDal.FindAll());
            this.SessionGroupList = ReloadActiveSessionGroupList();
            this.TeamList = ReloadActiveTeamList();
            this.StudentList = ReloadStudentList();
            this.DictionaryList = ReloadDictionaryList();
            this.DictionaryTypeList = ReloadDictionaryTypeList();
            this.TeamFilterList = ReloadTeamFilterList();

            this.LogoutCommand = new RelayCommand(Logout);
            this.TeacherFilter = _contextDal.FindById(ContextTeacher.ID);
            this.DictionaryTypeFilter = DictionaryTypeList[0];
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
                case "SessionGroupTeamFilterCB":
                    SessionGroupTeamFilter = null;
                    break;
                case "DictionaryTypeFilterCB":
                    DictionaryTypeFilter = null;
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
            SemesterWindow target = new SemesterWindow((Semester)((DataGrid)param).SelectedItem)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.SemesterList = ReloadSemesterList();
            this.SessionGroupList = ReloadActiveSessionGroupList();
            this.TeamList = ReloadActiveTeamList();
            this.TeamFilterList = ReloadTeamFilterList();
            this.StudentList = ReloadStudentList();
        }
        private void DeleteSemester(object param)
        {
            Semester selectedSemester = (Semester)((DataGrid)param).SelectedItem;
            SessionGroupDAL sessionGroupDal=new SessionGroupDAL();
            if (selectedSemester.IsActive == true)
            {
                MessageBox.Show("Aktív szemeszter nem törölhető!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (sessionGroupDal.FindAll(x=>x.SemesterID==selectedSemester.ID).FirstOrDefault()!=null)
            {
                MessageBox.Show("A félévhez tartozik csoport, ezért nem törölhető!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
            TeacherEditorWindow target = new TeacherEditorWindow((Teacher)((DataGrid)param).SelectedItem)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.TeacherList = new ObservableCollection<Teacher>(_contextDal.FindAll());
            this.SessionGroupList = ReloadActiveSessionGroupList();
            this.TeamList = ReloadActiveTeamList();
        }
        private void DeleteTeacher(object param)
        {
            Teacher selectedTeacher = (Teacher)((DataGrid)param).SelectedItem;
            if (selectedTeacher.ID == ContextTeacher.ID)
            {
                MessageBox.Show("Bejelentkezett felhasználó nem törölhető!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (this.SessionGroupList.Where(x=>x.TeacherID==selectedTeacher.ID).FirstOrDefault() != null)
            {
                MessageBox.Show("Az otkatóhoz tartozik csoport, ezért nem törölhető!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
            this.TeamList = ReloadActiveTeamList();
            this.TeamFilterList = ReloadTeamFilterList();
        }
        private void EditSessionGroup(object param)
        {
            SessionGroupEditWindow target = new SessionGroupEditWindow((SessionGroup)((DataGrid)param).SelectedItem)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.SessionGroupList = ReloadActiveSessionGroupList();
            this.TeamList = ReloadActiveTeamList();
            this.StudentList = ReloadStudentList();
            this.TeamFilterList = ReloadTeamFilterList();
        }
        private void DeleteSessionGroup(object param)
        {
            
            SessionGroup selectedSessionGroup = (SessionGroup)((DataGrid)param).SelectedItem;
            if (this.TeamList.FirstOrDefault(x => x.SessionGroupID==selectedSessionGroup.ID)!=null)
            {
                MessageBox.Show("A csoporthoz tartozik csapat, ezért nem törölhető!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Valóban törli?", "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    SessionGroupList.Remove(selectedSessionGroup);
                    ((TeacherDAL)_contextDal).DeleteSessionGroup(selectedSessionGroup);
                }
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
            this.TeamFilterList = ReloadTeamFilterList();
        }
        private void EditTeam(object param)
        {
            TeamEditorWindow target = new TeamEditorWindow((Team)((DataGrid)param).SelectedItem)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.TeamList = ReloadActiveTeamList();
            this.StudentList = ReloadStudentList();
            this.TeamFilterList = ReloadTeamFilterList();
        }
        private void DeleteTeam(object param)
        {
            Team selectedTeam = (Team)((DataGrid)param).SelectedItem;
            if (this.StudentList.FirstOrDefault(x => x.TeamID == selectedTeam.ID) != null)
            {
                MessageBox.Show("A csapathoz tartozik hallgató, ezért nem törölhető!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Valóban törli?", "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    TeamList.Remove(selectedTeam);
                    ((TeacherDAL)_contextDal).DeleteTeam(selectedTeam);
                }
            }
            this.TeamFilterList = ReloadTeamFilterList();
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
            this.TeamList = ReloadActiveTeamList();
            this.TeamFilterList = ReloadTeamFilterList();
        }
        private void DeleteStudent(object param)
        {
            Student selectedStudent = (Student)((DataGrid)param).SelectedItem;
            Team studentTeam = _contextDal.applicationDbContext.Team.FirstOrDefault(x => x.TeamCaptain == selectedStudent.ID);
            if (studentTeam != null && selectedStudent.Team.First().TeamCaptain ==selectedStudent.ID)
            {
                MessageBox.Show("Csapatkapitány nem törölhető!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Valóban törli?", "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    ((TeacherDAL)_contextDal).DeleteStudentUser(selectedStudent);
                    StudentList.Remove(selectedStudent);
                    ((TeacherDAL)_contextDal).DeleteStudent(selectedStudent);
                }
            }
            this.StudentList = ReloadStudentList();
        }

        private void CreateDictionary()
        {
            Dictionary dictionary = new Dictionary();
            DictionaryEditWindow target = new DictionaryEditWindow(dictionary)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.DictionaryList = ReloadDictionaryList();
        }
        private void EditDictionary(object param)
        {
            DictionaryEditWindow target = new DictionaryEditWindow((Dictionary)((DataGrid)param).SelectedItem)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.DictionaryList = ReloadDictionaryList();
        }
        private void DeleteDictionary(object param)
        {
            Dictionary selectedDictionary = (Dictionary)((DataGrid)param).SelectedItem;
            MessageBoxResult messageBoxResult = MessageBox.Show("Valóban törli?", "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                DictionaryList.Remove(selectedDictionary);
                ((TeacherDAL)_contextDal).DeleteDictionary(selectedDictionary);
            }
        }

        private void CreateDictionaryType()
        {
            DictionaryType dictionaryType = new DictionaryType();
            DictionaryTypeEditWindow target = new DictionaryTypeEditWindow(dictionaryType)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.DictionaryTypeList = ReloadDictionaryTypeList();
        }
        private void EditDictionaryType(object param)
        {
            DictionaryTypeEditWindow target = new DictionaryTypeEditWindow((DictionaryType)((DataGrid)param).SelectedItem)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.DictionaryList = ReloadDictionaryList();
        }
        private void DeleteDictionaryType(object param)
        {
            DictionaryType selectedDictionaryType = (DictionaryType)((DataGrid)param).SelectedItem;
            if (this.DictionaryList.FirstOrDefault(x => x.DictionaryTypeID==selectedDictionaryType.ID)!=null)
            {
                MessageBox.Show("A szótár típushoz tartozik szótár elem, ezért nem törölhető!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Valóban törli?", "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    DictionaryTypeList.Remove(selectedDictionaryType);
                    ((TeacherDAL)_contextDal).DeleteDictionaryType(selectedDictionaryType);
                }
            }
        }

        private void CopySelectedSemester()
        {
            Semester activeSemester = SemesterList.Where(x => x.IsActive).FirstOrDefault();
            CopySemesterWindow target = new CopySemesterWindow(activeSemester)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.SemesterList = ReloadSemesterList();
            this.SessionGroupList = ReloadActiveSessionGroupList();
            this.TeamList = ReloadActiveTeamList();
            this.TeamFilterList = ReloadTeamFilterList();
            this.StudentList = ReloadStudentList();
        }

        #endregion CommandMethods
        #region CanExecutes
        private bool CanDeleteDictionary(object param)
        {
            Dictionary item = (Dictionary)((DataGrid)param).SelectedItem;
            return item != null && !item.IsProtected;
        }

        private bool CanEditOrDeleteSelectedItem(object param)
        {
            return ((DataGrid)param).SelectedItem != null;
        }

        private bool CanCopySemester()
        {
            return SemesterList.Where(x => x.IsActive).FirstOrDefault() != null;
        }
        #endregion CanExecutes
    }
}
