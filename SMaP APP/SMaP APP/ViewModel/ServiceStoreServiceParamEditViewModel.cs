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
using System.Windows.Controls;

namespace SMaP_APP.ViewModel
{
    class ServiceStoreServiceParamEditViewModel : BaseViewModel<ServiceStoreServiceParams>
    {

        private ServiceStore SelectedServiceStore { get; set; }
        public ServiceStoreServiceParams SelectedParam { get; set; }
        public ObservableCollection<Team> TeamList { get; set; }
        public ObservableCollection<ServiceStore> serviceStoreList;
        public ObservableCollection<ServiceStore> ServiceStoreList
        {
            get { return serviceStoreList; }
            set { serviceStoreList = value; NotifyPropertyChanged(); }
        }
        private Team teamFilter;
        public Team TeamFilter
        {
            get { return teamFilter; }
            set { teamFilter = value; NotifyPropertyChanged(); ServiceStoreList = ReloadServiceStoreList(); }
        }
        private TeamDAL TeamDal { get; set; }
        private ServiceStoreDAL ServiceStoreDal { get; set; }
        private string serviceTeamFilter = "";
        private int SessionGroupID { get; set; }
        public string ServiceTeamFilter
        {
            get { return serviceTeamFilter; }
            set { serviceTeamFilter = value; NotifyPropertyChanged(); ServiceStoreList = ReloadServiceStoreList(); }
        }
        public RelayCommand SaveCommand { get; set; }
        public ServiceStoreServiceParamEditViewModel(ServiceStoreServiceParamEditWindow window, ServiceStore selectedServiceStore, ServiceStoreServiceParams selectedParam)
        {

            this._contextDal = new ServiceStoreServiceParamsDAL();
            this.SourceWindow = window;
            this.SelectedServiceStore = selectedServiceStore;
            this.TeamDal = new TeamDAL();
            this.ServiceStoreDal = new ServiceStoreDAL();
            this.TeamFilter = TeamDal.FindById(SelectedServiceStore.ProviderTeamID);
            this.SelectedParam = selectedParam;
            this.SessionGroupID = TeamDal.FindById(SelectedServiceStore.ProviderTeamID).SessionGroupID;
            this.TeamList = new ObservableCollection<Team>(TeamDal.FindAll(x => x.SessionGroupID == SessionGroupID));
            this.ServiceStoreList = ReloadServiceStoreList();
            this.SaveCommand = new RelayCommand(SaveParameter);

        }

        private ObservableCollection<ServiceStore> ReloadServiceStoreList()
        {
            if (TeamFilter != null)
            {
                return new ObservableCollection<ServiceStore>(ServiceStoreDal.FindAll(x => x.Team.SessionGroupID == SessionGroupID && x.ProviderTeamID == TeamFilter.ID && x.ServiceName.Contains(ServiceTeamFilter)));
            }
            return new ObservableCollection<ServiceStore>(ServiceStoreDal.FindAll(x => x.Team.SessionGroupID == SessionGroupID && x.ServiceName.Contains(ServiceTeamFilter)));
        }

        private void SaveParameter(object param)
        {
            SelectedParam.ParamServiceStoreID = ((ServiceStore)((DataGrid)param).SelectedItem).ID;
            this.SourceWindow.Close();
        }

        public override void Close()
        {
            SelectedServiceStore.ServiceStoreServiceParams1.Remove(SelectedParam);
            this.SourceWindow.Close();
        }
    }
}
