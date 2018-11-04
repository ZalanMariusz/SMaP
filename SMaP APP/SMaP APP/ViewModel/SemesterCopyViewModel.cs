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
    class SemesterCopyViewModel : BaseViewModel<Semester>
    {
        public RelayCommand SaveCommand { get; set; }
        public int SourceId { get; set; }
        public int NewSemesterTypeId { get; set; }
        public string NewSemesterName { get; set; }
        public ObservableCollection<Dictionary> SemesterTypes { get; set; }
        public SemesterDataTree DataTree { get; set; }
        public ObservableCollection<SessionGroupTreeViewItem> SessionGroupList { get; set; }

        public SemesterCopyViewModel(CopySemesterWindow copySemesterWindow, Semester sourceSemester)
        {
            this.SourceId = sourceSemester.ID;
            this.SourceWindow = copySemesterWindow;
            this.SaveCommand = new RelayCommand(CopySemester, CanExecute);
            this._contextDal = new SemesterDAL();
            this.SemesterTypes = new ObservableCollection<Dictionary>(((SemesterDAL)_contextDal).SemesterTypes);
            this.DataTree = new SemesterDataTree();
            SessionGroupList = DataTree.SessionGroupList;
        }

        public void CopySemester()
        {
            if (((SemesterDAL)this._contextDal).FindAll(x => x.SemesterName == this.NewSemesterName).FirstOrDefault() != null)
            {
                MessageBox.Show("Szemeszter ezzel a névvel már létezik!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                string SessionGroups = getSessionGroupsToDelete();
                string Teams = getTeamsToDelete();
                string Students = getStudentsToDelete();
                ((SemesterDAL)_contextDal).CopySemester(SourceId, NewSemesterName, NewSemesterTypeId,SessionGroups,Teams,Students);
                this.SourceWindow.Close();
            }

        }

        private string getSessionGroupsToDelete()
        {
            string retval = "";
            foreach (var SessionGroupItem in DataTree.SessionGroupList)
            {
                if (SessionGroupItem.IsCheked == false)
                {
                    retval += SessionGroupItem.ID + ",";
                }
            }
            if (!string.IsNullOrEmpty(retval))
            {
                retval = retval.Substring(0, retval.Length - 1);
            }
            return retval;
        }

        private string getTeamsToDelete()
        {
            string retval = "";
            foreach (var SessionGroupItem in DataTree.SessionGroupList)
            {
                foreach (var TeamItem in SessionGroupItem.ActiveTeamList)
                {
                    if (TeamItem.IsCheked == false)
                    {
                        retval += TeamItem.ID + ",";
                    }
                }
            }
            if (!string.IsNullOrEmpty(retval))
            {
                retval = retval.Substring(0, retval.Length - 1);
            }
            return retval;
        }

        private string getStudentsToDelete()
        {
            string retval = "";
            foreach (var SessionGroupItem in DataTree.SessionGroupList)
            {
                foreach (var TeamItem in SessionGroupItem.ActiveTeamList)
                {
                    foreach (var StudentItem in TeamItem.ActiveStudentList)
                    {
                        if (StudentItem.IsCheked==false)
                        {
                            retval += StudentItem.ID + ",";
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(retval))
            {
                retval = retval.Substring(0, retval.Length - 1);
            }
            return retval;
        }
    }



    class SemesterDataTree
    {
        private ObservableCollection<SessionGroup> ActiveSessionGroupList;
        private ObservableCollection<Team> ActiveTeamList;
        private ObservableCollection<Student> ActiveStudentList;

        public ObservableCollection<SessionGroupTreeViewItem> SessionGroupList { get; set; }
        public SemesterDataTree()
        {
            SessionGroupList = new ObservableCollection<SessionGroupTreeViewItem>();
            this.ActiveSessionGroupList = new ObservableCollection<SessionGroup>(new TeacherDAL().ActiveSessionGroupList());
            foreach (var sessiongroup in ActiveSessionGroupList)
            {
                SessionGroupTreeViewItem newItem = new SessionGroupTreeViewItem { ID = sessiongroup.ID, IsCheked = true, SessionGroupName = sessiongroup.SessionGroupName };
                SessionGroupList.Add(newItem);
                ActiveTeamList = new ObservableCollection<Team>(new TeacherDAL().ActiveTeamList().Where(x => x.SessionGroupID == newItem.ID));
                foreach (var team in ActiveTeamList)
                {
                    TeamTreeViewItem newTeam = new TeamTreeViewItem() { ID = team.ID, Parent = newItem, TeamName = team.TeamName, IsCheked = true };
                    newItem.ActiveTeamList.Add(newTeam);
                    ActiveStudentList = new ObservableCollection<Student>(new TeacherDAL().StudentList(null, newTeam.ID, null));
                    foreach (var student in ActiveStudentList)
                    {
                        StudentTreeViewItem newStudent = new StudentTreeViewItem() { ID = student.ID, Parent = newTeam, StudentName = student.Users.FullName, IsCheked = true };
                        newTeam.ActiveStudentList.Add(newStudent);
                    }
                }

            }
        }

    }

    class SessionGroupTreeViewItem : BaseViewModel<SessionGroup>
    {
        private bool? isChecked;
        private ObservableCollection<TeamTreeViewItem> activeTeamList;
        public bool? IsCheked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                foreach (var item in ActiveTeamList)
                {
                    if (isChecked != null && item.IsCheked != isChecked)
                    {
                        item.IsCheked = value;
                    }
                }
                NotifyPropertyChanged();
                NotifyPropertyChanged("ActiveTeamList");
            }
        }

        public int ID { get; set; }
        public string SessionGroupName { get; set; }
        public ObservableCollection<TeamTreeViewItem> ActiveTeamList
        {
            get { return activeTeamList; }
            set { activeTeamList = value; NotifyPropertyChanged(); }
        }
        public SessionGroupTreeViewItem()
        {
            ActiveTeamList = new ObservableCollection<TeamTreeViewItem>();
        }
    }

    class TeamTreeViewItem : BaseViewModel<Team>
    {
        public SessionGroupTreeViewItem Parent { get; set; }
        private bool? isChecked;
        public int ID { get; set; }
        public bool? IsCheked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                foreach (var item in ActiveStudentList)
                {
                    if (isChecked != null && item.IsCheked != isChecked)
                    {
                        item.IsCheked = value;
                    }
                }
                if (isChecked != Parent.IsCheked)
                {
                    Parent.IsCheked = null;
                }
                NotifyPropertyChanged();
                NotifyPropertyChanged("ActiveStudentList");
            }
        }
        public string TeamName { get; set; }
        public ObservableCollection<StudentTreeViewItem> ActiveStudentList { get; set; }
        public TeamTreeViewItem()
        {
            ActiveStudentList = new ObservableCollection<StudentTreeViewItem>();
        }
    }

    class StudentTreeViewItem : BaseViewModel<Student>
    {
        private bool? isChecked;
        public TeamTreeViewItem Parent { get; set; }
        public int ID { get; set; }
        public bool? IsCheked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                if (isChecked != Parent.IsCheked)
                {
                    Parent.IsCheked = null;
                }
                NotifyPropertyChanged();
            }
        }
        public string StudentName { get; set; }
    }
}
