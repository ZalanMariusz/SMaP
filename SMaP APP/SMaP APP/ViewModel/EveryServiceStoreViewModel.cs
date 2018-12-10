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
    class EveryServiceStoreViewModel : BaseViewModel<ServiceStore>
    {
        #region filters
        public ObservableCollection<Team> RequesterTeamList
        {
            get { return requesterTeamList; }
            set { requesterTeamList = value; NotifyPropertyChanged(); }
        }
        public ObservableCollection<Team> ProviderTeamList
        {
            get { return providerTeamList; }
            set { providerTeamList = value; NotifyPropertyChanged();  }
        }
        public string RequestNrFilter
        {
            get { return requestNrFilter; }
            set { requestNrFilter = value; NotifyPropertyChanged(); ServiceRequestList = ReloadServiceRequestList(); }
        }
        public ObservableCollection<Dictionary> RequestStateList
        {
            get { return requestStateList; }
            set { requestStateList = value; NotifyPropertyChanged(); }
        }

        public ObservableCollection<Dictionary> RequestTypeList
        {
            get { return requestTypeList; }
            set { requestTypeList = value; NotifyPropertyChanged(); }
        }

        public ObservableCollection<Student> RequestCreatorList
        {
            get { return requestCreatorList; }
            set { requestCreatorList = value; NotifyPropertyChanged(); }
        }

        public ObservableCollection<Student> RequestAssigneeList
        {
            get { return requestAssigneeList; }
            set { requestAssigneeList = value; NotifyPropertyChanged(); }
        }

        public ObservableCollection<Team> requesterTeamList;
        public ObservableCollection<Team> providerTeamList;
        public string requestNrFilter;
        public ObservableCollection<Dictionary> requestStateList;
        public ObservableCollection<Dictionary> requestTypeList;
        public ObservableCollection<Student> requestCreatorList;
        public ObservableCollection<Student> requestAssigneeList;


        private Team requesterTeamFilter;
        private Team providerTeamFilter;
        private Dictionary requestStateFilter;
        private Dictionary requestTypeFilter;
        private Student requestCreatorFilter;
        private Student requestAssigneeFilter;

        public Team RequesterTeamFilter
        {
            get { return requesterTeamFilter; }
            set { requesterTeamFilter = value; NotifyPropertyChanged(); ServiceRequestList = ReloadServiceRequestList(); FillFilters(true); }
        }
        public Team ProviderTeamFilter
        {
            get { return providerTeamFilter; }
            set { providerTeamFilter = value; NotifyPropertyChanged(); ServiceRequestList = ReloadServiceRequestList(); FillFilters(true); }
        }

        public Dictionary RequestStateFilter
        {
            get { return requestStateFilter; }
            set { requestStateFilter = value; NotifyPropertyChanged(); ServiceRequestList = ReloadServiceRequestList(); }
        }
        public Dictionary RequestTypeFilter
        {
            get { return requestTypeFilter; }
            set { requestTypeFilter = value; NotifyPropertyChanged(); ServiceRequestList = ReloadServiceRequestList(); }
        }
        public Student RequestCreatorFilter
        {
            get { return requestCreatorFilter; }
            set { requestCreatorFilter = value; NotifyPropertyChanged(); ServiceRequestList = ReloadServiceRequestList(); }
        }
        public Student RequestAssigneeFilter
        {
            get { return requestAssigneeFilter; }
            set { requestAssigneeFilter = value; NotifyPropertyChanged(); ServiceRequestList = ReloadServiceRequestList(); }
        }

        #endregion filters
        private int SessionGroupID { get; set; }
        public Teacher ContextTeacher { get; set; }
        public ObservableCollection<ServiceStore> ServiceStoreList { get; set; }
        private ServiceTable selectedServiceTable;
        public ServiceTable SelectedServiceTable
        {
            get { return selectedServiceTable; }
            set { selectedServiceTable = value; NotifyPropertyChanged(); ServiceTableFieldList = ReloadServiceTableFieldList(); }
        }
        private Team teamIDFilterForTables;
        public Team TeamIDFilterForTables
        {
            get { return teamIDFilterForTables; }
            set { teamIDFilterForTables = value; NotifyPropertyChanged(); ServiceTableList = ReloadServiceTableList(); }
        }




        public RelayCommand ServiceStoreEdit { get; set; }

        private ObservableCollection<Team> teamList;
        public ObservableCollection<Team> TeamList
        {
            get { return teamList; }
            set { teamList = value; NotifyPropertyChanged(); }
        }

        private TeamDAL TeamDal { get; set; }
        private ServiceTableDAL ServiceTableDal { get; set; }
        private ServiceTableFieldDAL ServiceTableFieldDal { get; set; }

        private ObservableCollection<ServiceTable> serviceTableList;
        public ObservableCollection<ServiceTable> ServiceTableList
        {
            get { return serviceTableList; }
            set { serviceTableList = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<ServiceTableField> serviceTableFieldList;
        public ObservableCollection<ServiceTableField> ServiceTableFieldList
        {
            get { return serviceTableFieldList; }
            set { serviceTableFieldList = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<ServiceRequest> serviceRequestList;
        public ObservableCollection<ServiceRequest> ServiceRequestList
        {
            get { return serviceRequestList; }
            set { serviceRequestList = value; NotifyPropertyChanged(); }
        }

        public RelayCommand ServiceRequestEdit { get; set; }
        public RelayCommand DeleteFilter { get; set; }
        public RelayCommand LogoutCommand { get; set; }
        public RelayCommand BackToStartWindow { get; set; }
        private ServiceRequestDAL ServiceRequestDal { get; set; }
        private DictionaryDAL DictionaryDal { get; set; }
        public RelayCommand DeleteRequestFilters { get; set; }
        public EveryServiceStoreViewModel(EveryServiceStoreWindow sourceWindow, int SessionGroupID, Teacher ContextTeacher)
        {
            this._contextDal = new ServiceStoreDAL();
            this.TeamDal = new TeamDAL();
            this.ServiceTableDal = new ServiceTableDAL();
            this.ServiceTableFieldDal = new ServiceTableFieldDAL();
            this.ServiceRequestDal = new ServiceRequestDAL();
            this.DictionaryDal = new DictionaryDAL();

            this.SessionGroupID = SessionGroupID;
            this.SourceWindow = sourceWindow;
            this.ContextTeacher = ContextTeacher;
            this.DeleteFilter = new RelayCommand(DeleteSelectedFilter);

            this.ServiceStoreList = ReloadServiceStoreList();
            this.TeamList = new ObservableCollection<Team>(TeamDal.FindAll(x => x.SessionGroupID == SessionGroupID));
            this.ServiceStoreEdit = new RelayCommand(EditServiceStore, CanEditOrDeleteSelectedItem);
            this.ServiceTableList = ReloadServiceTableList();
            this.LogoutCommand = new RelayCommand(Logout);
            this.BackToStartWindow = new RelayCommand(NavigateBack);
            this.ServiceRequestList = ReloadServiceRequestList();
            this.DeleteRequestFilters = new RelayCommand(RequestFilterClear);

            this.ServiceRequestEdit = new RelayCommand(OpenSelectedServiceRequest, CanEditOrDeleteSelectedItem);
            FillFilters(false);
        }

        private void RequestFilterClear()
        {
            RequestNrFilter = null;
            RequesterTeamFilter = null;
            ProviderTeamFilter = null;
            RequestStateFilter = null;
            RequestTypeFilter = null;
            RequestCreatorFilter = null;
            RequestAssigneeFilter = null;
        }

        private void OpenSelectedServiceRequest(object param)
        {
            ServiceRequest SelectedRequest = (ServiceRequest)((DataGrid)param).SelectedItem;
            ServiceRequestWindow target = new ServiceRequestWindow(SessionGroupID, SelectedRequest, null, ((DataGrid)param).Name)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.ServiceRequestList = ReloadServiceRequestList();
        }

        private ObservableCollection<ServiceRequest> ReloadServiceRequestList()
        {
            int? requesterTeamFilterID = RequesterTeamFilter?.ID;
            int? providerTeamFilterID = ProviderTeamFilter?.ID;
            int? requestStateFilterID = RequestStateFilter?.ID;
            int? requestTypeFilterID = RequestTypeFilter?.ID;
            int? requestCreatorFilterID = RequestCreatorFilter?.ID;
            int? requestAssigneeFilterID = RequestAssigneeFilter?.ID;
            return new ObservableCollection<ServiceRequest>
                (ServiceRequestDal.ReloadAllServiceRequests(SessionGroupID, ToNullableInt(RequestNrFilter), requesterTeamFilterID, providerTeamFilterID, requestStateFilterID,
                requestTypeFilterID, requestCreatorFilterID, requestAssigneeFilterID));
        }

        private void NavigateBack()
        {
            SwitchWindows(new TeacherWindow(ContextTeacher));
        }

        private void Logout()
        {
            SwitchWindows(new LoginWindow());
        }

        private ObservableCollection<ServiceStore> ReloadServiceStoreList()
        {
            return new ObservableCollection<ServiceStore>(((ServiceStoreDAL)_contextDal).AllServicesForSessionGroup(SessionGroupID));
        }

        private void EditServiceStore(object param)
        {
            ServiceStoreEditWindow target = new ServiceStoreEditWindow((ServiceStore)((DataGrid)param).SelectedItem, null)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.ServiceStoreList = ReloadServiceStoreList();
        }

        private bool CanEditOrDeleteSelectedItem(object param)
        {
            return ((DataGrid)param).SelectedItem != null;
        }

        private ObservableCollection<ServiceTable> ReloadServiceTableList()
        {
            if (TeamIDFilterForTables == null)
            {
                return new ObservableCollection<ServiceTable>(ServiceTableDal.FindAll(x => x.Team.SessionGroupID == SessionGroupID));
            }
            return new ObservableCollection<ServiceTable>(ServiceTableDal.FindAll(x => x.Team.SessionGroupID == SessionGroupID && x.TeamID == TeamIDFilterForTables.ID));
        }
        private ObservableCollection<ServiceTableField> ReloadServiceTableFieldList()
        {
            if (SelectedServiceTable == null)
            {
                return new ObservableCollection<ServiceTableField>();
            }
            return new ObservableCollection<ServiceTableField>(ServiceTableFieldDal.FindAll(x => x.TableID == SelectedServiceTable.ID));
        }

        private void DeleteSelectedFilter(object param)
        {
            ComboBox cb = (ComboBox)param;
            switch (cb.Name)
            {
                case "TeamCB":
                    TeamIDFilterForTables = null;
                    break;
            }
          ((ComboBox)param).SelectedItem = null;
        }
        public static int? ToNullableInt(string s)
        {
            if (int.TryParse(s, out int i)) return i;
            return null;
        }
        private void FillFilters(bool filteredFilter)
        {
            if (!filteredFilter)
            {
                RequesterTeamList = new ObservableCollection<Team>(TeamList);
                ProviderTeamList = new ObservableCollection<Team>(TeamList);
                RequestStateList = new ObservableCollection<Dictionary>(DictionaryDal.DictionaryListByType(5));
                RequestTypeList = new ObservableCollection<Dictionary>(DictionaryDal.DictionaryListByType(4));
            }
            RequestCreatorList=new ObservableCollection<Student>();
            RequestAssigneeList= new ObservableCollection<Student>();
            int? RequesterTeamFilterID = RequesterTeamFilter?.ID;
            if (RequesterTeamFilterID != null)
            {
                RequestCreatorList = new ObservableCollection<Student>(new StudentDAL().FindAll(x => x.TeamID == RequesterTeamFilter.ID));
            }
            int? ProviderTeamFilterID = ProviderTeamFilter?.ID;
            if (ProviderTeamFilterID != null)
            {
                RequestAssigneeList = new ObservableCollection<Student>(new StudentDAL().FindAll(x => x.TeamID == ProviderTeamFilter.ID));
            }
        }
    }
}
