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
    class ServiceStoreViewModel : BaseViewModel<ServiceStore>
    {
        public Student ContextStudent { get; set; }
        private ServiceRequestDAL ServiceRequestDal { get; set; }
        private ServiceTableDAL ServiceTableDal { get; set; }
        private ServiceTableFieldDAL ServiceTableFieldDal { get; set; }
        private TeamDAL TeamDal { get; set; }

        public RelayCommand LogoutCommand { get; set; }
        public RelayCommand DeleteFilter { get; set; }


        public RelayCommand ServiceStoreCreate { get; set; }
        public RelayCommand ServiceStoreEdit { get; set; }
        public RelayCommand ServiceStoreDelete { get; set; }

        public RelayCommand ServiceRequestCreate { get; set; }
        public RelayCommand ServiceRequestEdit { get; set; }
        public RelayCommand ServiceRequestDelete { get; set; }

        public RelayCommand ServiceTableCreate { get; set; }
        public RelayCommand ServiceTableEdit { get; set; }
        public RelayCommand ServiceTableDelete { get; set; }

        public RelayCommand ServiceTableFieldCreate { get; set; }
        public RelayCommand ServiceTableFieldEdit { get; set; }
        public RelayCommand ServiceTableFieldDelete { get; set; }

        private ObservableCollection<ServiceStore> serviceStoreList;
        private ObservableCollection<ServiceRequest> serviceRequestList;
        private ObservableCollection<ServiceTable> serviceTableList;
        private ObservableCollection<ServiceTableField> serviceTableFieldList;
        private ObservableCollection<Team> teamList;

        public ObservableCollection<ServiceStore> ServiceStoreList
        {
            get { return serviceStoreList; }
            set { serviceStoreList = value; NotifyPropertyChanged(); }
        }
        public ObservableCollection<ServiceRequest> ServiceRequestList
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

        public Team teamIDFilterForTables;
        public Team TeamIDFilterForTables
        {
            get { return teamIDFilterForTables; }
            set { teamIDFilterForTables = value; NotifyPropertyChanged(); ServiceTableList= ReloadServiceTableList(); }
        }

        private int contextSessionGroupID { get; set; }
        public ServiceTable selectedServiceTable;
        public ServiceTable SelectedServiceTable
        {
            get { return selectedServiceTable; }
            set { selectedServiceTable = value; NotifyPropertyChanged(); ServiceTableFieldList = ReloadServiceTableFieldList(); }
        }

        public ServiceStoreViewModel(Window SourceWindow, Student contextStudent)
        {
            this.ContextStudent = contextStudent;
            this.SourceWindow = SourceWindow;

            this._contextDal = new ServiceStoreDAL();
            this.ServiceRequestDal = new ServiceRequestDAL();
            this.ServiceTableDal = new ServiceTableDAL();
            this.ServiceTableFieldDal = new ServiceTableFieldDAL();
            this.TeamDal = new TeamDAL();

            this.ServiceStoreList = ReloadServiceStoreList();
            this.ServiceRequestList = ReloadServiceRequestList();

            this.ServiceStoreCreate = new RelayCommand(CreateServiceStore);
            this.ServiceStoreEdit = new RelayCommand(EditServiceStore, CanEditOrDeleteSelectedItem);

            this.ServiceTableCreate = new RelayCommand(CreateServiceTable);
            this.ServiceTableEdit = new RelayCommand(EditServiceTable, CanEditOrDeleteSelectedItem);
            this.ServiceTableDelete = new RelayCommand(DeleteServiceTable, CanEditOrDeleteSelectedItem);

            this.ServiceTableFieldCreate = new RelayCommand(CreateServiceTableField, CanCreateServiceTableField);
            this.ServiceTableFieldEdit = new RelayCommand(EditServiceTableField, CanEditOrDeleteSelectedItem);
            this.ServiceTableFieldDelete = new RelayCommand(DeleteServiceTableField, CanEditOrDeleteSelectedItem);

            this.LogoutCommand = new RelayCommand(Logout);
            this.DeleteFilter = new RelayCommand(DeleteSelectedFilter);
            contextSessionGroupID = TeamDal.FindById(ContextStudent.TeamID).SessionGroupID;

            this.TeamList = new ObservableCollection<Team>(TeamDal.FindAll(x => x.SessionGroupID == contextSessionGroupID));
            this.ServiceTableList = ReloadServiceTableList();
            this.ServiceTableFieldList = ReloadServiceTableFieldList();

        }

        private void DeleteServiceTableField(object param)
        {
            ServiceTableField selectedServiceTable = (ServiceTableField)((DataGrid)param).SelectedItem;
            MessageBoxResult messageBoxResult = MessageBox.Show("Valóban törli?", "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                ServiceTableFieldList.Remove(selectedServiceTable);
                ServiceTableFieldDal.LogicalDelete(selectedServiceTable);
            }
            this.ServiceTableFieldList = ReloadServiceTableFieldList();
        }
        private void EditServiceTableField(object param)
        {
            ServiceTableFieldWindow target = new ServiceTableFieldWindow((ServiceTableField)((DataGrid)param).SelectedItem)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.ServiceTableFieldList = ReloadServiceTableFieldList();
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
        }
        public void DeleteServiceTable(object param)
        {
            ServiceTable selectedServiceTable = (ServiceTable)((DataGrid)param).SelectedItem;
            MessageBoxResult messageBoxResult = MessageBox.Show("Valóban törli?", "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                ServiceTableList.Remove(selectedServiceTable);
                ServiceTableDal.LogicalDelete(selectedServiceTable);
            }
            ServiceTableList = ReloadServiceTableList();
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
                return new ObservableCollection<ServiceTable>(ServiceTableDal.FindAll(x => x.Team.SessionGroupID == contextSessionGroupID));
            }
            return new ObservableCollection<ServiceTable>(ServiceTableDal.FindAll(x => x.Team.SessionGroupID == contextSessionGroupID && x.TeamID == TeamIDFilterForTables.ID));
        }
        private ObservableCollection<ServiceStore> ReloadServiceStoreList()
        {
            return new ObservableCollection<ServiceStore>(((ServiceStoreDAL)_contextDal).ProvidedServices(ContextStudent.TeamID));
        }
        private ObservableCollection<ServiceRequest> ReloadServiceRequestList()
        {
            return new ObservableCollection<ServiceRequest>(ServiceRequestDal.FindAll());
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
            ServiceStoreEditWindow target = new ServiceStoreEditWindow(serviceStore)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.ServiceStoreList = ReloadServiceStoreList();
        }
        private void EditServiceStore(object param)
        {
            ServiceStoreEditWindow target = new ServiceStoreEditWindow((ServiceStore)((DataGrid)param).SelectedItem)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(target, true);
            this.ServiceStoreList = ReloadServiceStoreList();
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
        //private void EditDictionaryType(object param)
        //{
        //    DictionaryTypeEditWindow target = new DictionaryTypeEditWindow((DictionaryType)((DataGrid)param).SelectedItem)
        //    {
        //        Owner = this.SourceWindow
        //    };
        //    SwitchWindows(target, true);
        //    this.DictionaryList = ReloadDictionaryList();
        //}
        //private void DeleteDictionaryType(object param)
        //{
        //    DictionaryType selectedDictionaryType = (DictionaryType)((DataGrid)param).SelectedItem;
        //    if (this.DictionaryList.FirstOrDefault(x => x.DictionaryTypeID == selectedDictionaryType.ID) != null)
        //    {
        //        MessageBox.Show("A szótár típushoz tartozik szótár elem, ezért nem törölhető!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //    }
        //    else
        //    {
        //        MessageBoxResult messageBoxResult = MessageBox.Show("Valóban törli?", "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question);
        //        if (messageBoxResult == MessageBoxResult.Yes)
        //        {
        //            DictionaryTypeList.Remove(selectedDictionaryType);
        //            ((TeacherDAL)_contextDal).DeleteDictionaryType(selectedDictionaryType);
        //        }
        //    }
        //}

    }
}
