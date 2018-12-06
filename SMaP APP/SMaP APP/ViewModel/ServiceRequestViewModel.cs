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
    class ServiceRequestViewModel : BaseViewModel<ServiceRequest>
    {
        private ServiceRequest selectedServiceRequest;
        private ObservableCollection<ServiceStore> availableServiceStoreList;
        private int selectedProviderTeamID;
        private ServiceStore selectedServiceStore;
        private int selectedServiceRequestTypeID;

        public int SelectedServiceRequestTypeID
        {
            get { return selectedServiceRequestTypeID; }
            set { selectedServiceRequestTypeID = value;NotifyPropertyChanged(); }
        }
        public ServiceStore SelectedServiceStore
        {
            get { return selectedServiceStore; }
            set { selectedServiceStore = value; NotifyPropertyChanged(); }
        }
        public ServiceRequest SelectedServiceRequest
        {
            get { return selectedServiceRequest; }
            set { selectedServiceRequest = value; NotifyPropertyChanged(); }
        }
        public Student ContextStudent { get; set; }
        public RelayCommand SaveCommand { get; set; }

        public ObservableCollection<Dictionary> RequestStateList { get; set; }
        public ObservableCollection<Dictionary> RequestTypeList { get; set; }
        public ObservableCollection<Team> ProviderTeamList { get; set; }
        public ObservableCollection<ServiceStore> AvailableServiceStoreList
        {
            get { return availableServiceStoreList; }
            set { availableServiceStoreList = value; NotifyPropertyChanged(); }
        }
        public int SelectedProviderTeamID
        {
            get { return selectedProviderTeamID; }
            set { selectedProviderTeamID = value; NotifyPropertyChanged(); AvailableServiceStoreList = ReloadServiceStoreList(); }
        }

        private DictionaryDAL DictionaryDal { get; set; }
        private TeamDAL TeamDal { get; set; }
        private ServiceStoreDAL ServiceStoreDal { get; set; }
        public bool SemiReadOnly { get; set; }
        public ServiceRequestViewModel(ServiceRequest selectedServiceRequest, ServiceRequestWindow sourceWindow, Student contextStudent, string semiReadOnly)
        {
            _contextDal = new ServiceRequestDAL();
            this.DictionaryDal = new DictionaryDAL();
            this.TeamDal = new TeamDAL();
            this.ServiceStoreDal = new ServiceStoreDAL();

            this.SemiReadOnly = semiReadOnly == "ServiceRequestsGrid";
            this.SourceWindow = sourceWindow;
            this.SelectedServiceRequest = selectedServiceRequest;
            
            this.SelectedProviderTeamID = SelectedServiceRequest.ProviderTeamID;
            this.SelectedServiceStore = ServiceStoreDal.FindAll(x => x.ID == SelectedServiceRequest.ServiceID).FirstOrDefault();
            this.SelectedServiceRequestTypeID = SelectedServiceRequest.RequestType;

            this.ContextStudent = contextStudent;
            this.SaveCommand = new RelayCommand(SaveRequest, CanSave);
            if (DictionaryDal.DictionaryListByType("Igény állapota").Where(x => x.ItemName == "Jóváhagyva").FirstOrDefault().ID==SelectedServiceRequest.RequestState)
            {
                this.RequestStateList = new ObservableCollection<Dictionary>(DictionaryDal.DictionaryListByType("Igény állapota").Where(x => x.ItemName == "Jóváhagyva"));
            }
            else
            {
                this.RequestStateList = new ObservableCollection<Dictionary>(DictionaryDal.DictionaryListByType("Igény állapota").Where(x => x.ItemName != "Jóváhagyva"));
            }
            this.RequestTypeList = new ObservableCollection<Dictionary>(DictionaryDal.DictionaryListByType("Igény típus"));
            int SessionGroupID = TeamDal.FindById(ContextStudent.TeamID).SessionGroupID;
            this.ProviderTeamList = new ObservableCollection<Team>(TeamDal.FindAll(x => x.SessionGroupID == SessionGroupID));
            this.AvailableServiceStoreList = ReloadServiceStoreList();
        }

        private bool CanSave(object param)
        {
            return base.CanExecute();
        }

        private void SaveRequest(object param)
        {
            ServiceStore selectedServiceStore = (ServiceStore)((DataGrid)param).SelectedItem;            
            SelectedServiceRequest.RequestType = SelectedServiceRequestTypeID;
            if (SelectedServiceRequest.RequestType == DictionaryDal.DictionaryListByType("Igény típus").Where(x => x.ItemName == "Módosítás").FirstOrDefault().ID && selectedServiceStore == null)
            {
                MessageBox.Show("Módosítási igényhez szolgáltatás kijelölése szükséges!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (SelectedServiceRequest.RequestType == DictionaryDal.DictionaryListByType("Igény típus").Where(x => x.ItemName == "Módosítás").FirstOrDefault().ID && selectedServiceStore != null)
                {
                    SelectedServiceRequest.ServiceID = selectedServiceStore.ID;
                }
                else
                {
                    SelectedServiceRequest.ServiceID = null;
                }
                SelectedServiceRequest.ProviderTeamID = SelectedProviderTeamID;
                if (SelectedServiceRequest.ID == 0)
                {
                    _contextDal.Create(SelectedServiceRequest);
                }
                else
                {
                    _contextDal.Update(SelectedServiceRequest);
                }
                this.SourceWindow.Close();
            }
            
        }

        private ObservableCollection<ServiceStore> ReloadServiceStoreList()
        {
            return new ObservableCollection<ServiceStore>(ServiceStoreDal.FindAll(x => x.ProviderTeamID == SelectedProviderTeamID));
        }
    }
}
