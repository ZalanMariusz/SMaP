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
using System.Windows.Controls;

namespace SMaP_APP.ViewModel
{
    class ServiceStoreEditViewModel : BaseViewModel<ServiceStore>
    {
        public ServiceStore SelectedServiceStore { get; set; }
        public ObservableCollection<TeamIsSelected> RequesterTeamList { get; set; }
        public ObservableCollection<Team> TeamList { get; set; }
        public ObservableCollection<ServiceStoreParams> InputFieldList { get; set; }
        public ObservableCollection<ServiceStoreParams> OutputFieldList { get; set; }
        public ObservableCollection<ServiceStoreServiceParams> ServiceStoreServiceParamsList { get; set; }

        private TeamDAL TeamDal { get; set; }
        private SessionGroupDAL SessionGroupDal { get; set; }
        private StudentDAL StudentDal { get; set; }
        private ServiceStoreParamsDAL ServiceStoreParamsDal { get; set; }
        private ServiceStoreUserTeamsDAL ServiceStoreUserTeamsDal { get; set; }
        private ServiceStoreServiceParamsDAL ServiceStoreServiceParamsDal { get; set; }

        public RelayCommand SaveCommand { get; set; }

        public RelayCommand InputFieldCreateCommand { get; set; }
        public RelayCommand InputFieldDeleteCommand { get; set; }

        public RelayCommand OutputFieldCreateCommand { get; set; }
        public RelayCommand OutputFieldDeleteCommand { get; set; }

        public RelayCommand ServiceStoreServiceAddCommand { get; set; }
        public RelayCommand ServiceStoreServiceDeleteCommand { get; set; }

        public Student ContextStudent { get; set; }

        public ServiceStoreEditViewModel(ServiceStoreEditWindow serviceStoreEditWindow, ServiceStore selectedServiceStore,Student contextStudent)
        {

            this.ContextStudent = contextStudent;
            this.SelectedServiceStore = selectedServiceStore;
            this.SourceWindow = serviceStoreEditWindow;

            this.SaveCommand = new RelayCommand(SaveServiceStore,CanExecute);
            this.InputFieldCreateCommand = new RelayCommand(AddInputParam);
            this.InputFieldDeleteCommand = new RelayCommand(DeleteSelectedInputField, CanEditOrDeleteSelectedItem);

            this.OutputFieldCreateCommand = new RelayCommand(AddOutputParam);
            this.OutputFieldDeleteCommand = new RelayCommand(DeleteSelectedOutputField, CanEditOrDeleteSelectedItem);

            this.ServiceStoreServiceAddCommand = new RelayCommand(AddServiceParam);
            this.ServiceStoreServiceDeleteCommand = new RelayCommand(DeleteServiceParam, CanEditOrDeleteSelectedItem);

            this._contextDal = new ServiceStoreDAL();
            this.TeamDal = new TeamDAL();
            this.SessionGroupDal = new SessionGroupDAL();
            this.StudentDal = new StudentDAL();
            this.ServiceStoreParamsDal = new ServiceStoreParamsDAL();
            this.ServiceStoreUserTeamsDal = new ServiceStoreUserTeamsDAL();
            this.ServiceStoreServiceParamsDal = new ServiceStoreServiceParamsDAL();

            int SessionGroupID = TeamDal.FindAll().FirstOrDefault(x => x.ID == SelectedServiceStore.ProviderTeamID).SessionGroupID;

            this.RequesterTeamList = LoadTeamList();
            this.TeamList= new ObservableCollection<Team>(TeamDal.FindAll(x => x.SessionGroupID == SessionGroupID));
            this.ServiceStoreServiceParamsList = ReloadServiceParams();

            this.InputFieldList = ReloadInputFieldList();   
            this.OutputFieldList = ReloadOutputFieldList();
        }

        private void DeleteServiceParam(object param)
        {
            ServiceStoreServiceParams selectedItem = (ServiceStoreServiceParams)((DataGrid)param).SelectedItem;
            MessageBoxResult messageBoxResult = MessageBox.Show("Valóban törli?", "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                ServiceStoreServiceParamsList.Remove(selectedItem);
                ServiceStoreServiceParamsDal.LogicalDelete(selectedItem);
            }
        }

        private ObservableCollection<TeamIsSelected> LoadTeamList()
        {
            int SessionGroupID = TeamDal.FindAll().FirstOrDefault(x => x.ID == SelectedServiceStore.ProviderTeamID).SessionGroupID;
            List<Team> Teams = TeamDal.FindAll(x => x.SessionGroupID == SessionGroupID);
            ObservableCollection<TeamIsSelected> retval = new ObservableCollection<TeamIsSelected>();
            foreach (var item in Teams)
            {
                retval.Add(new TeamIsSelected(SelectedServiceStore.ID, item));
            }
            return retval;
        }

        private void DeleteSelectedOutputField(object param)
        {
            ServiceStoreParams selectedItem = (ServiceStoreParams)((DataGrid)param).SelectedItem;
            MessageBoxResult messageBoxResult = MessageBox.Show("Valóban törli?", "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                OutputFieldList.Remove(selectedItem);
                ServiceStoreParamsDal.LogicalDelete(selectedItem);
            }
        }

        private void DeleteSelectedInputField(object param)
        {
            ServiceStoreParams selectedItem = (ServiceStoreParams)((DataGrid)param).SelectedItem;
            MessageBoxResult messageBoxResult = MessageBox.Show("Valóban törli?", "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                InputFieldList.Remove(selectedItem);
                ServiceStoreParamsDal.LogicalDelete(selectedItem);
            }
        }


        private ObservableCollection<ServiceStoreParams> ReloadInputFieldList()
        {
            return new ObservableCollection<ServiceStoreParams>(SelectedServiceStore.ServiceStoreParams.Where(x => !x.Deleted && x.InOut));
        }
        private ObservableCollection<ServiceStoreParams> ReloadOutputFieldList()
        {
            return new ObservableCollection<ServiceStoreParams>(SelectedServiceStore.ServiceStoreParams.Where(x => !x.Deleted && !x.InOut));
        }
        private ObservableCollection<ServiceStoreServiceParams> ReloadServiceParams()
        {
            return new ObservableCollection<ServiceStoreServiceParams>(SelectedServiceStore.ServiceStoreServiceParams1.Where(x=>!x.Deleted));
        }

        private void HandleTeamList()
        {
            foreach (var item in RequesterTeamList)
            {
                if (!item.IsSelected)
                {
                    ServiceStoreUserTeams team = ServiceStoreUserTeamsDal.FindAll(x => x.ServiceID == SelectedServiceStore.ID && x.RequesterTeamID == item.SelectedTeam.ID).FirstOrDefault();
                    if (team != null)
                    {
                        ServiceStoreUserTeamsDal.LogicalDelete(team);
                    }
                }
                else
                {
                    if (!ServiceStoreUserTeamsDal.FindAll().Exists(x => x.ServiceID == SelectedServiceStore.ID && x.RequesterTeamID == item.SelectedTeam.ID))
                    {
                        ServiceStoreUserTeams team = new ServiceStoreUserTeams()
                        {
                            RequesterTeamID = item.SelectedTeam.ID,
                            ServiceID = SelectedServiceStore.ID
                        };
                        ServiceStoreUserTeamsDal.Create(team);
                    }
                }
            }
        }
        private void SaveServiceStore()
        {
            if (this._contextDal.FindAll().Exists(x => x.ID != SelectedServiceStore.ID && x.ProviderTeamID == SelectedServiceStore.ProviderTeamID && x.ServiceName == SelectedServiceStore.ServiceName))
            {
                MessageBox.Show("Az adott csapatban már létezik ilyen nevű szolgáltatás!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (this._contextDal.FindById(SelectedServiceStore.ID) == null)
                {
                    List<ServiceStoreParams> tempFieldParamList = new List<ServiceStoreParams>();
                    List<ServiceStoreServiceParams> tempServiceParamList = new List<ServiceStoreServiceParams>();
                    foreach (var item in SelectedServiceStore.ServiceStoreParams)
                    {
                        tempFieldParamList.Add(item);
                    }
                    foreach (var item in SelectedServiceStore.ServiceStoreServiceParams1)
                    {
                        tempServiceParamList.Add(item);
                    }

                    SelectedServiceStore.ServiceStoreParams.Clear();
                    SelectedServiceStore.ServiceStoreServiceParams1.Clear();

                    this._contextDal.Create(SelectedServiceStore);
                    SelectedServiceStore.ServiceStoreParams = tempFieldParamList;
                    SelectedServiceStore.ServiceStoreServiceParams1 = tempServiceParamList;
                    HandleTeamList();
                    this._contextDal.Update(SelectedServiceStore);
                }
                else
                {
                    HandleTeamList();
                    this._contextDal.Update(SelectedServiceStore);
                }
                this.SourceWindow.Close();
            }
        }
        private void AddInputParam()
        {
            ServiceStoreParams newParam = new ServiceStoreParams()
            {
                ServiceStore = SelectedServiceStore,
                InOut = true,
            };
            SelectedServiceStore.ServiceStoreParams.Add(newParam);
            ServiceStoreParamAddWindow editWindow = new ServiceStoreParamAddWindow(SelectedServiceStore, newParam)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(editWindow, true);
            this.InputFieldList = ReloadInputFieldList(); NotifyPropertyChanged("InputFieldList");
        }
        private void AddOutputParam()
        {
            ServiceStoreParams newParam = new ServiceStoreParams()
            {
                ServiceStore = SelectedServiceStore,
                InOut = false,
            };
            SelectedServiceStore.ServiceStoreParams.Add(newParam);
            ServiceStoreParamAddWindow editWindow = new ServiceStoreParamAddWindow(SelectedServiceStore, newParam)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(editWindow, true);
            this.OutputFieldList = ReloadOutputFieldList(); NotifyPropertyChanged("OutputFieldList");
        }

        private void AddServiceParam()
        {
            ServiceStoreServiceParams newParam = new ServiceStoreServiceParams()
            {
                ServiceStore1 = SelectedServiceStore
            };
            SelectedServiceStore.ServiceStoreServiceParams1.Add(newParam);
            ServiceStoreServiceParamEditWindow editWindow = new ServiceStoreServiceParamEditWindow(SelectedServiceStore, newParam)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(editWindow, true);
            this.ServiceStoreServiceParamsList = ReloadServiceParams(); NotifyPropertyChanged("ServiceStoreServiceParamsList");
        }

        private bool CanEditOrDeleteSelectedItem(object param)
        {
            return ((DataGrid)param).SelectedItem != null;
        }
    }

    class TeamIsSelected
    {
        public int ServiceStoreID { get; set; }
        public Team SelectedTeam { get; set; }
        public bool IsSelected { get; set; }
        private ServiceStoreUserTeamsDAL ServiceStoreUserTeamsDal { get; set; }

        public TeamIsSelected(int ServiceStoreID,Team SelectedTeam)
        {
            this.ServiceStoreID = ServiceStoreID;
            this.SelectedTeam = SelectedTeam;
            this.ServiceStoreUserTeamsDal = new ServiceStoreUserTeamsDAL();
            this.IsSelected = ServiceStoreUserTeamsDal.FindAll().Exists(x=>x.ServiceID==ServiceStoreID && x.RequesterTeamID==SelectedTeam.ID);
        }
    }
}
