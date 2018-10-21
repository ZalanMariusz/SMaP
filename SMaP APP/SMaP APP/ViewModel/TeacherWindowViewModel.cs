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
        public RelayCommand LogoutCommand { get; set; }
        #endregion Commands

        #region Properties and Fields
        private ObservableCollection<Semester> semesterList;
        private ObservableCollection<Teacher> teacherList;
        private ObservableCollection<SessionGroup> sessionGroupList;
        private ObservableCollection<Team> teamList;
        public Teacher ContextTeacher { get; set; }
        private bool onlyActiveData;

        public bool OnlyActiveData
        {
            get { return onlyActiveData;}
            set { onlyActiveData = value;NotifyPropertyChanged(); }
        }
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
        public ObservableCollection<Team> TeamList
        {
            get { return teamList; }
            set { teamList = value; NotifyPropertyChanged(); }
        }
        #endregion Properties and Fields

        #region DALs
        public SemesterDAL SemesterDal { get; set; }
        public SessionGroupDAL SessionGroupDal { get; set; }
        public TeamDAL TeamDal { get; set; }
        #endregion DALs

        public TeacherWindowViewModel(TeacherWindow teacherWindow, Teacher ContextTeacher)
        {
            OnlyActiveData = true;
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

            this._contextDal = new TeacherDAL(DbContext);
            this.SessionGroupDal = new SessionGroupDAL(DbContext);
            this.SemesterDal = new SemesterDAL(DbContext);
            this.TeamDal = new TeamDAL(DbContext);

            this.SemesterList = new ObservableCollection<Semester>(SemesterDal.FindAll());
            this.TeacherList = new ObservableCollection<Teacher>(_contextDal.FindAll());
            this.SessionGroupList = new ObservableCollection<SessionGroup>(SessionGroupDal.FindAll());
            this.TeamList = new ObservableCollection<Team>(TeamDal.FindAll());

            this.LogoutCommand = new RelayCommand(Logout);
        }
        //private Expression onlyActiveFilter()
        //{
        //    if (OnlyActiveData)
        //    {
        //        return
        //    }
        //}
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
            this.SemesterList = new ObservableCollection<Semester>(SemesterDal.FindAll());
        }
        private void EditSemester(object param)
        {
            SemesterWindow target = new SemesterWindow((Semester)((ListBox)param).SelectedItem)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.SemesterList = new ObservableCollection<Semester>(DbContext.Set<Semester>().Where(x => !x.Deleted));
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
        {
            SessionGroupEditWindow target = new SessionGroupEditWindow((SessionGroup)((ListBox)param).SelectedItem)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.SessionGroupList = new ObservableCollection<SessionGroup>(new ObservableCollection<SessionGroup>(_contextDal.applicationDbContext.Set<SessionGroup>().Where(x => !x.Deleted)));
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
            this.TeamList = new ObservableCollection<Team>(TeamDal.FindAll());
        }
        private void EditTeam(object param)
        {
            TeamEditorWindow target = new TeamEditorWindow((Team)((ListBox)param).SelectedItem)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.TeamList = new ObservableCollection<Team>(new ObservableCollection<Team>(_contextDal.applicationDbContext.Set<Team>().Where(x => !x.Deleted)));
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
        #endregion CanExecutes
    }
}
