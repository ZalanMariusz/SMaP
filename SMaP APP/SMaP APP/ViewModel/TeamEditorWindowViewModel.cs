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
    class TeamEditorWindowViewModel : BaseViewModel<Team>
    {
        private Team selectedTeam;
        public RelayCommand SaveCommand { get; set; }

        public Team SelectedTeam
        {
            get { return selectedTeam; }
            set { selectedTeam = value; NotifyPropertyChanged(); }
        }
        public ObservableCollection<Student> StudentList { get; set; }
        public ObservableCollection<SessionGroup> SessionGroupList { get; set; }

        public TeamEditorWindowViewModel(TeamEditorWindow teamEditWindow, Team selectedTeam)
        {
            this.SourceWindow = teamEditWindow;
            this._contextDal = new TeamDAL(DbContext);
            this.SelectedTeam = selectedTeam;
            this.SaveCommand = new RelayCommand(SaveTeam, CanSaveTeam);
            this.StudentList = new ObservableCollection<Student>(((TeamDAL)_contextDal).StudentList);
            this.SessionGroupList = new ObservableCollection<SessionGroup>(((TeamDAL)_contextDal).SessionGroupList);
        }
        private bool CanSaveTeam()
        {
            return (!string.IsNullOrEmpty(SelectedTeam.TeamName) && SelectedTeam.SessionGroupID!=0);
        }

        private void SaveTeam()
        {
            if (this._contextDal.FindAll().Exists(x => x.ID != SelectedTeam.ID && x.SessionGroupID == SelectedTeam.SessionGroupID && x.TeamName == SelectedTeam.TeamName))
            {
                MessageBox.Show("Az adott csoporthoz már létezik ilyen nevű csapat!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (this._contextDal.FindById(SelectedTeam.ID) == null)
                {
                    this._contextDal.Create(SelectedTeam);
                }
                else
                {
                    this._contextDal.Update(SelectedTeam);
                }
                this.SourceWindow.Close();
            }
        }


    }
}
