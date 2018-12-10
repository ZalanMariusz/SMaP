using Microsoft.Win32;
using SMaP_APP.Commands;
using SMaP_APP.DAL;
using SMaP_APP.Model;
using SMaP_APP.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SMaP_APP.ViewModel
{
    class ServiceStoreViewModel : BaseViewModel<ServiceStore>
    {
        public Student ContextStudent { get; set; }
        private ServiceRequestDAL ServiceRequestDal { get; set; }
        private ServiceTableDAL ServiceTableDal { get; set; }
        private ServiceTableFieldDAL ServiceTableFieldDal { get; set; }
        private TeamDAL TeamDal { get; set; }
        private ServiceStoreParamsDAL ServiceStoreParamsDal { get; set; }
        private DictionaryDAL DictionaryDal { get; set; }

        public RelayCommand LogoutCommand { get; set; }
        public RelayCommand DeleteFilter { get; set; }

        public RelayCommand ServiceStoreCreate { get; set; }
        public RelayCommand ServiceStoreEdit { get; set; }
        public RelayCommand ServiceStoreDelete { get; set; }

        public RelayCommand ServiceRequestCreate { get; set; }
        public RelayCommand ServiceRequestEdit { get; set; }
        public RelayCommand ServiceRequestDelete { get; set; }
        public RelayCommand ApproveRequest { get; set; }

        public RelayCommand ServiceTableCreate { get; set; }
        public RelayCommand ServiceTableEdit { get; set; }
        public RelayCommand ServiceTableDelete { get; set; }

        public RelayCommand ServiceTableFieldCreate { get; set; }
        public RelayCommand ServiceTableFieldEdit { get; set; }
        public RelayCommand ServiceTableFieldDelete { get; set; }

        public RelayCommand DeleteCompleteFilters { get; set; }
        public RelayCommand DeleteRequestedFilters { get; set; }
        public RelayCommand GenerateSummary { get; set; }

        private ObservableCollection<ServiceStore> serviceStoreList;
        private ObservableCollection<ServiceStore> serviceRequestList;
        private ObservableCollection<ServiceTable> serviceTableList;
        private ObservableCollection<ServiceTableField> serviceTableFieldList;
        private ObservableCollection<Team> teamList;
        private ObservableCollection<ServiceRequest> providedRequestsList;
        private ObservableCollection<ServiceRequest> requestedRequestsList;
        private ObservableCollection<ServiceStore> allServices;

        #region filters
        private string toCompleteNrFilter;
        public string ToCompleteNrFilter
        {
            get { return toCompleteNrFilter; }
            set { toCompleteNrFilter = value; NotifyPropertyChanged(); ProvidedRequestsList = ReloadProvidedRequestsList(); }
        }

        private string providedNrFilter;
        public string ProvidedNrFilter
        {
            get { return providedNrFilter; }
            set { providedNrFilter = value; NotifyPropertyChanged(); RequestedRequestsList = ReloadRequestedRequests(); }
        }

        private ObservableCollection<Team> completeRequesterTeamList;
        public ObservableCollection<Team> CompleteRequesterTeamList
        {
            get { return completeRequesterTeamList; }
            set { completeRequesterTeamList = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<Team> requestProviderTeamList;
        public ObservableCollection<Team> RequestProviderTeamList
        {
            get { return requestProviderTeamList; }
            set { requestProviderTeamList = value; NotifyPropertyChanged(); }
        }

        private Team requesterTeamFilter;
        public Team RequesterTeamFilter
        {
            get { return requesterTeamFilter; }
            set { requesterTeamFilter = value; NotifyPropertyChanged(); ProvidedRequestsList = ReloadProvidedRequestsList(); FillFilters(true); }
        }

        private Team providerTeamFilter;
        public Team ProviderTeamFilter
        {
            get { return providerTeamFilter; }
            set { providerTeamFilter = value; NotifyPropertyChanged(); RequestedRequestsList = ReloadRequestedRequests(); FillFilters(true); }
        }

        private ObservableCollection<Dictionary> completeRequestStateList;
        public ObservableCollection<Dictionary> CompleteRequestStateList
        {
            get { return completeRequestStateList; }
            set { completeRequestStateList = value; NotifyPropertyChanged(); }
        }
        private Dictionary completeRequestStateFilter;
        public Dictionary CompleteRequestStateFilter
        {
            get { return completeRequestStateFilter; }
            set { completeRequestStateFilter = value; NotifyPropertyChanged(); ProvidedRequestsList = ReloadProvidedRequestsList(); }
        }

        private ObservableCollection<Dictionary> requestedStateList;
        public ObservableCollection<Dictionary> RequestedStateList
        {
            get { return requestedStateList; }
            set { requestedStateList = value; NotifyPropertyChanged(); }
        }
        private Dictionary requestedStateFilter;
        public Dictionary RequestedStateFilter
        {
            get { return requestedStateFilter; }
            set { requestedStateFilter = value; NotifyPropertyChanged(); RequestedRequestsList = ReloadRequestedRequests(); }
        }

        private ObservableCollection<Dictionary> completeRequestTypeList;
        public ObservableCollection<Dictionary> CompleteRequestTypeList
        {
            get { return completeRequestTypeList; }
            set { completeRequestTypeList = value; NotifyPropertyChanged(); }
        }
        private Dictionary completeRequestTypeFilter;
        public Dictionary CompleteRequestTypeFilter
        {
            get { return completeRequestTypeFilter; }
            set { completeRequestTypeFilter = value; NotifyPropertyChanged(); ProvidedRequestsList = ReloadProvidedRequestsList(); }
        }

        private ObservableCollection<Dictionary> requestedTypeList;
        public ObservableCollection<Dictionary> RequestedTypeList
        {
            get { return requestedTypeList; }
            set { requestedTypeList = value; NotifyPropertyChanged(); }
        }
        private Dictionary requestedTypeFilter;
        public Dictionary RequestedTypeFilter
        {
            get { return requestedTypeFilter; }
            set { requestedTypeFilter = value; NotifyPropertyChanged(); RequestedRequestsList = ReloadRequestedRequests(); }
        }

        private ObservableCollection<Student> completeRequestCreatorList;
        public ObservableCollection<Student> CompleteRequestCreatorList
        {
            get { return completeRequestCreatorList; }
            set { completeRequestCreatorList = value; NotifyPropertyChanged(); }
        }
        private Student completeCreatorFilter;
        public Student CompleteCreatorFilter
        {
            get { return completeCreatorFilter; }
            set { completeCreatorFilter = value; NotifyPropertyChanged(); ProvidedRequestsList = ReloadProvidedRequestsList(); }
        }
        private ObservableCollection<Student> myTeamMembersList;
        public ObservableCollection<Student> MyTeamMembersList
        {
            get { return myTeamMembersList; }
            set { myTeamMembersList = value; NotifyPropertyChanged(); }
        }
        private Student ourAssignee;
        public Student OurAssignee
        {
            get { return ourAssignee; }
            set { ourAssignee = value; NotifyPropertyChanged(); ProvidedRequestsList = ReloadProvidedRequestsList(); }
        }

        private ObservableCollection<Student> requestedCreatorList;
        public ObservableCollection<Student> RequestedCreatorList
        {
            get { return requestedCreatorList; }
            set { requestedCreatorList = value; NotifyPropertyChanged(); }
        }
        private Student requestedCreatorFilter;
        public Student RequestedCreatorFilter
        {
            get { return requestedCreatorFilter; }
            set { requestedCreatorFilter = value; NotifyPropertyChanged(); RequestedRequestsList = ReloadRequestedRequests(); }
        }
        private ObservableCollection<Student> requestedAssigneeList;
        public ObservableCollection<Student> RequestedAssigneeList
        {
            get { return requestedAssigneeList; }
            set { requestedAssigneeList = value; NotifyPropertyChanged(); }
        }
        private Student requestedAssigneeFilter;
        public Student RequestedAssigneeFilter
        {
            get { return requestedAssigneeFilter; }
            set { requestedAssigneeFilter = value; NotifyPropertyChanged(); RequestedRequestsList = ReloadRequestedRequests(); ; }
        }
        #endregion filters

        private Team teamIDFilterForTables;

        public ObservableCollection<ServiceStore> AllServices
        {
            get { return allServices; }
            set { allServices = value; NotifyPropertyChanged(); }
        }
        public ObservableCollection<ServiceStore> ServiceStoreList
        {
            get { return serviceStoreList; }
            set { serviceStoreList = value; NotifyPropertyChanged(); }
        }
        public ObservableCollection<ServiceStore> ServiceRequestList
        {
            get { return serviceRequestList; }
            set { serviceRequestList = value; NotifyPropertyChanged(); }
        }
        public ObservableCollection<ServiceTable> ServiceTableList
        {
            get { return serviceTableList; }
            set { serviceTableList = value; NotifyPropertyChanged(); }
        }
        public ObservableCollection<ServiceTableField> ServiceTableFieldList
        {
            get { return serviceTableFieldList; }
            set { serviceTableFieldList = value; NotifyPropertyChanged(); }
        }
        public ObservableCollection<Team> TeamList
        {
            get { return teamList; }
            set { teamList = value; NotifyPropertyChanged(); }
        }

        public ObservableCollection<ServiceRequest> ProvidedRequestsList
        {
            get { return providedRequestsList; }
            set { providedRequestsList = value; NotifyPropertyChanged(); }
        }

        public ObservableCollection<ServiceRequest> RequestedRequestsList
        {
            get { return requestedRequestsList; }
            set { requestedRequestsList = value; NotifyPropertyChanged(); }
        }

        public Team TeamIDFilterForTables
        {
            get { return teamIDFilterForTables; }
            set { teamIDFilterForTables = value; NotifyPropertyChanged(); ServiceTableList = ReloadServiceTableList(); }
        }

        private int ContextSessionGroupID { get; set; }
        public ServiceTable selectedServiceTable;

        public ServiceTable SelectedServiceTable
        {
            get { return selectedServiceTable; }
            set { selectedServiceTable = value; NotifyPropertyChanged(); ServiceTableFieldList = ReloadServiceTableFieldList(); }
        }

        public Teacher ContextTeacher { get; set; }

        public ServiceStoreViewModel(Window sourceWindow, Student contextStudent, Teacher ContextTeacher)
        {
            this.ContextStudent = contextStudent;
            this.SourceWindow = sourceWindow;
            
            this._contextDal = new ServiceStoreDAL();
            this.ServiceRequestDal = new ServiceRequestDAL();
            this.ServiceTableDal = new ServiceTableDAL();
            this.ServiceTableFieldDal = new ServiceTableFieldDAL();
            this.TeamDal = new TeamDAL();
            this.ServiceStoreParamsDal = new ServiceStoreParamsDAL();
            this.DictionaryDal = new DictionaryDAL();
            this.ContextTeacher = ContextTeacher;

            this.ServiceStoreCreate = new RelayCommand(CreateServiceStore);
            this.ServiceStoreEdit = new RelayCommand(EditServiceStore, CanEditOrDeleteSelectedItem);
            this.ServiceStoreDelete = new RelayCommand(DeleteServiceStore, CanEditOrDeleteSelectedItem);

            this.ServiceTableCreate = new RelayCommand(CreateServiceTable);
            this.ServiceTableEdit = new RelayCommand(EditServiceTable, CanEditOrDeleteSelectedItem);
            this.ServiceTableDelete = new RelayCommand(DeleteServiceTable, CanEditOrDeleteSelectedItem);


            this.ServiceTableFieldCreate = new RelayCommand(CreateServiceTableField, CanCreateServiceTableField);
            this.ServiceTableFieldEdit = new RelayCommand(EditServiceTableField, CanEditOrDeleteSelectedItem);
            this.ServiceTableFieldDelete = new RelayCommand(DeleteServiceTableField, CanEditOrDeleteSelectedItem);

            this.ServiceRequestCreate = new RelayCommand(CreateServiceRequest);
            this.ServiceRequestEdit = new RelayCommand(EditServiceRequest, CanEditOrDeleteSelectedItem);
            this.ServiceRequestDelete = new RelayCommand(DeleteServiceRequest, CanDeleteServiceRequest);
            this.ApproveRequest = new RelayCommand(RequestApprove, CanApproveRequest);

            this.LogoutCommand = new RelayCommand(Logout);
            this.DeleteFilter = new RelayCommand(DeleteSelectedFilter);
            this.DeleteCompleteFilters = new RelayCommand(CompleteFiltersDelete);
            this.DeleteRequestedFilters = new RelayCommand(RequestFiltersDelete);
            this.GenerateSummary = new RelayCommand(SummaryGeneration);
            this.ContextSessionGroupID = TeamDal.FindById(ContextStudent.TeamID).SessionGroupID;

            this.TeamList = new ObservableCollection<Team>(TeamDal.FindAll(x => x.SessionGroupID == ContextSessionGroupID));
            this.ServiceTableList = ReloadServiceTableList();
            this.ServiceTableFieldList = ReloadServiceTableFieldList();
            this.ServiceStoreList = ReloadServiceStoreList();
            this.ServiceRequestList = ReloadServiceRequestList();
            this.RequestedRequestsList = ReloadRequestedRequests();
            this.ProvidedRequestsList = ReloadProvidedRequestsList();
            this.AllServices = ReloadAllServices();
            this.FillFilters(false);

        }

        private void SummaryGeneration()
        {
            this.ServiceStoreList = ReloadServiceStoreList();
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "Szolgáltatások",
                Filter = "Szöveges fájlok (*.txt)|*.txt"
            };
            saveFileDialog.ShowDialog();

            if (!string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                StreamWriter sw = new StreamWriter(saveFileDialog.FileName)
                {
                    AutoFlush = true
                };
                foreach (var item in ServiceStoreList)
                {
                    sw.WriteLine(@"" + item.ServiceName);
                    sw.WriteLine("SSz: " + item.ServiceNumber);
                    sw.WriteLine("Nyújtó: " + TeamDal.FindById(item.ProviderTeamID).TeamName);
                    sw.Write("Igénylő(k): ");
                    string requesterList="";
                    foreach (var Requesters in item.ServiceStoreUserTeams)
                    {
                        if (!Requesters.Deleted)
                        {
                            requesterList += Requesters.Team.TeamName + ", ";
                        }
                    }
                    if (!string.IsNullOrEmpty(requesterList))
                    {
                        requesterList=requesterList.Substring(0, requesterList.Length - 2); ;
                        sw.Write(requesterList);
                    }
                    sw.WriteLine();
                    sw.Write("Átadott információk: ");
                        
                    string paramList = "";
                    foreach (var inputs in item.ServiceStoreParams)
                    {
                        if (inputs.InOut && !inputs.Deleted)
                        {
                            paramList += inputs.ParamName + ", ";
                        }
                        
                    }
                    if (!string.IsNullOrEmpty(paramList))
                    {
                        paramList = paramList.Substring(0, paramList.Length - 2);
                        sw.Write(paramList);
                    }
                    paramList = "";
                    sw.WriteLine();
                    sw.Write("Visszaadott információk: ");
                    foreach (var inputs in item.ServiceStoreParams)
                    {
                        if (!inputs.InOut && !inputs.Deleted)
                        {
                            paramList += inputs.ParamName + ", ";
                        }
                    }
                    if (!string.IsNullOrEmpty(paramList))
                    {
                        paramList = paramList.Substring(0, paramList.Length - 2);
                        sw.Write(paramList);
                    }
                    sw.WriteLine();
                    sw.WriteLine("Leírás: " + item.ServiceDescription);
                    sw.WriteLine();
                }
                sw.Close();
            }
        }

        private ObservableCollection<ServiceStore> ReloadAllServices()
        {
            return new ObservableCollection<ServiceStore>(((ServiceStoreDAL)_contextDal).AllServicesForSessionGroup(ContextSessionGroupID));
        }

        private void RequestFiltersDelete()
        {
            ProvidedNrFilter = null;
            ProviderTeamFilter = null;
            RequestedTypeFilter = null;
            RequestedStateFilter = null;
            RequestedCreatorFilter = null;
            RequestedAssigneeFilter = null;
        }

        private void CompleteFiltersDelete()
        {
            ToCompleteNrFilter = null;
            RequesterTeamFilter = null;
            CompleteRequestTypeFilter = null;
            CompleteRequestStateFilter = null;
            CompleteCreatorFilter = null;
            OurAssignee = null;
        }

        private void FillFilters(bool filteredFilter)
        {
            if (!filteredFilter)
            {
                CompleteRequesterTeamList = new ObservableCollection<Team>(TeamList);
                CompleteRequestStateList = new ObservableCollection<Dictionary>(DictionaryDal.DictionaryListByType(5).Where(x => x.ID != 42 && x.ID != 25));
                CompleteRequestTypeList = new ObservableCollection<Dictionary>(DictionaryDal.DictionaryListByType(4));
                MyTeamMembersList = new ObservableCollection<Student>(new StudentDAL().FindAll(x => x.TeamID == ContextStudent.ID));

                RequestProviderTeamList = new ObservableCollection<Team>(TeamList);
                RequestedStateList = new ObservableCollection<Dictionary>(DictionaryDal.DictionaryListByType(5));
                RequestedTypeList = new ObservableCollection<Dictionary>(DictionaryDal.DictionaryListByType(4));
                RequestedCreatorList = new ObservableCollection<Student>(new StudentDAL().FindAll(x => x.TeamID == ContextStudent.ID));

            }
            CompleteRequestCreatorList = new ObservableCollection<Student>();
            RequestedAssigneeList = new ObservableCollection<Student>();

            int? RequesterTeamFilterID = RequesterTeamFilter?.ID;
            if (RequesterTeamFilterID != null)
            {
                CompleteRequestCreatorList = new ObservableCollection<Student>(new StudentDAL().FindAll(x => x.TeamID == requesterTeamFilter.ID));
            }
            int? ProviderTeamFilterID = ProviderTeamFilter?.ID;
            if (ProviderTeamFilterID != null)
            {
                RequestedAssigneeList = new ObservableCollection<Student>(new StudentDAL().FindAll(x => x.TeamID == ProviderTeamFilter.ID));
            }
        }



        private bool CanApproveRequest(object param)
        {
            return ((DataGrid)param).SelectedItem != null && DictionaryDal.FindById(((ServiceRequest)((DataGrid)param).SelectedItem).RequestState).ItemName == "Jóváhagyásra vár";
        }

        private void RequestApprove(object param)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Jóváhagyás után az igénylés nem szerkeszthető a nyújtó csapat által! Folytatja?", "Megerősítés", MessageBoxButton.YesNo, MessageBoxImage.Question);
            ServiceRequest requestToApprove = ServiceRequestDal.FindById(((ServiceRequest)((DataGrid)param).SelectedItem).ID);
            requestToApprove.RequestState = DictionaryDal.DictionaryListByType(5).First(x => x.ID == 24).ID;
            ServiceRequestDal.Update(requestToApprove);
            this.RequestedRequestsList = ReloadRequestedRequests();
            this.ProvidedRequestsList = ReloadProvidedRequestsList();
        }

        private bool CanDeleteServiceRequest(object param)
        {
            if (((DataGrid)param).SelectedItem == null)
            {
                return false;
            }
            ServiceRequest selectedServiceRequest = (ServiceRequest)((DataGrid)param).SelectedItem;
            int NewID = DictionaryDal.DictionaryListByType(5).Where(x => x.ID == 21).FirstOrDefault().ID;
            int DeniedID = DictionaryDal.DictionaryListByType(5).Where(x => x.ID == 25).FirstOrDefault().ID;
            return selectedServiceRequest.RequestState == NewID || selectedServiceRequest.RequestState == DeniedID;
        }

        private void DeleteServiceRequest(object param)
        {
            ServiceRequest selectedServiceRequest = (ServiceRequest)((DataGrid)param).SelectedItem;
            MessageBoxResult messageBoxResult = MessageBox.Show("Valóban törli?", "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                (ServiceRequestDal).LogicalDelete(selectedServiceRequest);
            }
            this.RequestedRequestsList = ReloadRequestedRequests();
            this.ProvidedRequestsList = ReloadProvidedRequestsList();
        }

        private ObservableCollection<ServiceRequest> ReloadProvidedRequestsList()
        {
            int? RequesterTeamFilterID = RequesterTeamFilter?.ID;
            int? RequestTypeID = CompleteRequestTypeFilter?.ID;
            int? RequestStateID = CompleteRequestStateFilter?.ID;
            int? CreatorID = CompleteCreatorFilter?.ID;
            int? AssigneeID = OurAssignee?.ID;
            return new ObservableCollection<ServiceRequest>(ServiceRequestDal.ReloadProvidedRequestsList(ContextStudent, ToNullableInt(ToCompleteNrFilter),
                RequesterTeamFilterID, RequestStateID, RequestTypeID, CreatorID, AssigneeID));
        }

        public static int? ToNullableInt(string s)
        {
            if (int.TryParse(s, out int i)) return i;
            return null;
        }

        private ObservableCollection<ServiceRequest> ReloadRequestedRequests()
        {
            int? ProviderTeamFilterID = ProviderTeamFilter?.ID;
            int? RequestTypeID = RequestedTypeFilter?.ID;
            int? RequestStateID = RequestedStateFilter?.ID;
            int? CreatorID = RequestedCreatorFilter?.ID;
            int? AssigneeID = RequestedAssigneeFilter?.ID;

            return new ObservableCollection<ServiceRequest>(ServiceRequestDal.ReloadRequestedRequests(ContextStudent, ToNullableInt(ProvidedNrFilter),
                ProviderTeamFilterID, RequestStateID, RequestTypeID, CreatorID, AssigneeID));
        }
        private void EditServiceRequest(object param)
        {
            ServiceRequest SelectedRequest = (ServiceRequest)((DataGrid)param).SelectedItem;
            ServiceRequestWindow target = new ServiceRequestWindow(0,SelectedRequest, ContextStudent, ((DataGrid)param).Name)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.RequestedRequestsList = ReloadRequestedRequests();
            this.ProvidedRequestsList = ReloadProvidedRequestsList();
        }
        private void CreateServiceRequest()
        {
            ServiceRequest newRequest = new ServiceRequest
            {
                RequesterTeamID = ContextStudent.TeamID,
                RequestState = DictionaryDal.DictionaryListByType(5).Where(x => x.ID == 21).FirstOrDefault().ID
            };
            ServiceRequestWindow target = new ServiceRequestWindow(0,newRequest, ContextStudent, "")
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.RequestedRequestsList = ReloadRequestedRequests();
            this.ProvidedRequestsList = ReloadProvidedRequestsList();
        }

        private void DeleteServiceStore(object param)
        {
            ServiceStore selectedServiceStore = (ServiceStore)((DataGrid)param).SelectedItem;
            MessageBoxResult messageBoxResult = MessageBox.Show("Valóban törli?", "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                ((ServiceStoreDAL)_contextDal).LogicalDelete(selectedServiceStore);
            }
            this.ServiceStoreList = ReloadServiceStoreList();
            this.AllServices = ReloadAllServices();
        }

        private void DeleteServiceTableField(object param)
        {
            ServiceTableField selectedServiceTableField = (ServiceTableField)((DataGrid)param).SelectedItem;
            if (ServiceStoreParamsDal.FindAll().Exists(x => x.ServiceTableFieldID == selectedServiceTableField.ID && !x.ServiceStore.Deleted))
            {
                MessageBox.Show("A mezőre hivatkozik szolgáltatás!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Valóban törli?", "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    ServiceTableFieldList.Remove(selectedServiceTableField);
                    ServiceTableFieldDal.LogicalDelete(selectedServiceTableField);
                }
                this.ServiceTableFieldList = ReloadServiceTableFieldList();
            }
        }
        private void EditServiceTableField(object param)
        {
            ServiceTableFieldWindow target = new ServiceTableFieldWindow((ServiceTableField)((DataGrid)param).SelectedItem)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.ServiceTableFieldList = ReloadServiceTableFieldList();
            this.ServiceStoreList = ReloadServiceStoreList();
        }
        private void CreateServiceTableField()
        {
            ServiceTableField serviceTableField = new ServiceTableField()
            {
                TableID = SelectedServiceTable.ID
            };
            ServiceTableFieldWindow target = new ServiceTableFieldWindow(serviceTableField)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.ServiceTableFieldList = ReloadServiceTableFieldList();
        }

        private bool CanCreateServiceTableField()
        {
            return SelectedServiceTable != null;
        }

        public void CreateServiceTable()
        {
            ServiceTable serviceTable = new ServiceTable()
            {
                TeamID = ContextStudent.TeamID
            };
            ServiceTableWindow target = new ServiceTableWindow(serviceTable)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.ServiceTableList = ReloadServiceTableList();
        }
        public void EditServiceTable(object param)
        {
            ServiceTableWindow target = new ServiceTableWindow((ServiceTable)((DataGrid)param).SelectedItem)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.ServiceTableList = ReloadServiceTableList();
            this.ServiceStoreList = ReloadServiceStoreList();
        }
        public void DeleteServiceTable(object param)
        {

            ServiceTable selectedServiceTable = (ServiceTable)((DataGrid)param).SelectedItem;
            if (ServiceTableFieldDal.FindAll().Exists(x => x.TableID == selectedServiceTable.ID))
            {
                MessageBox.Show("A táblához tartozik mező, ezért nem törölhető!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Valóban törli?", "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    ServiceTableList.Remove(selectedServiceTable);
                    ServiceTableDal.LogicalDelete(selectedServiceTable);
                }
                ServiceTableList = ReloadServiceTableList();
            }
        }

        private ObservableCollection<ServiceTableField> ReloadServiceTableFieldList()
        {
            if (SelectedServiceTable == null)
            {
                return new ObservableCollection<ServiceTableField>();
            }
            return new ObservableCollection<ServiceTableField>(ServiceTableFieldDal.FindAll(x => x.TableID == SelectedServiceTable.ID));
        }
        private ObservableCollection<ServiceTable> ReloadServiceTableList()
        {
            if (TeamIDFilterForTables == null)
            {
                return new ObservableCollection<ServiceTable>(ServiceTableDal.FindAll(x => x.Team.SessionGroupID == ContextSessionGroupID));
            }
            return new ObservableCollection<ServiceTable>(ServiceTableDal.FindAll(x => x.Team.SessionGroupID == ContextSessionGroupID && x.TeamID == TeamIDFilterForTables.ID));
        }
        private ObservableCollection<ServiceStore> ReloadServiceStoreList()
        {
            return new ObservableCollection<ServiceStore>(((ServiceStoreDAL)_contextDal).ProvidedServices(ContextStudent.TeamID));
        }
        private ObservableCollection<ServiceStore> ReloadServiceRequestList()
        {
            return new ObservableCollection<ServiceStore>(((ServiceStoreDAL)_contextDal).RequestedServices(ContextStudent.TeamID));
        }

        private void Logout()
        {
            SwitchWindows(new LoginWindow());
        }

        private void CreateServiceStore()
        {
            ServiceStore serviceStore = new ServiceStore()
            {
                ProviderTeamID = ContextStudent.TeamID,
                CreatorID = ContextStudent.ID
            };
            ServiceStoreEditWindow target = new ServiceStoreEditWindow(serviceStore, ContextStudent)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.ServiceStoreList = ReloadServiceStoreList();
            this.AllServices = ReloadAllServices();
        }
        private void EditServiceStore(object param)
        {
            ServiceStoreEditWindow target = new ServiceStoreEditWindow((ServiceStore)((DataGrid)param).SelectedItem, ContextStudent)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.ServiceStoreList = ReloadServiceStoreList();
            this.AllServices = ReloadAllServices();
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

        private bool CanEditOrDeleteSelectedItem(object param)
        {
            return ((DataGrid)param).SelectedItem != null;
        }
    }
}
