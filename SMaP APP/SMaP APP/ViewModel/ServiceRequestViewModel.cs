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
        private ObservableCollection<Student> assigneeList;
        private int selectedProviderTeamID;
        private ServiceStore selectedServiceStore;
        private int selectedServiceRequestTypeID;


        public int SelectedServiceRequestTypeID
        {
            get { return selectedServiceRequestTypeID; }
            set { selectedServiceRequestTypeID = value; NotifyPropertyChanged(); }
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

        public ObservableCollection<Student> AssigneeList
        {
            get { return assigneeList; }
            set { assigneeList = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<RequestMessage> requestMessageList;
        public ObservableCollection<RequestMessage> RequestMessageList
        {
            get { return requestMessageList; }
            set { requestMessageList = value; NotifyPropertyChanged(); }
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
            set { selectedProviderTeamID = value; NotifyPropertyChanged(); AvailableServiceStoreList = ReloadServiceStoreList(); AssigneeList = ReloadAssigneeList(); }
        }

        public bool CanEditRequestDescription
        {
            get { return SelectedServiceRequest.ID == 0 || (ContextStudent.TeamID==SelectedServiceRequest.RequesterTeamID && SemiReadOnly) && !IsAcceptedOrDeclined(); }
            set { }
        }

        public bool CanPickAssignee
        {
            get { return SelectedServiceRequest.ID != 0 && ContextStudent.TeamID == SelectedServiceRequest.ProviderTeamID && !SemiReadOnly; }
            set { }
        }
        public bool CanChangeState
        {
            get { return SelectedServiceRequest.ID != 0 && ContextStudent.TeamID == SelectedServiceRequest.ProviderTeamID && !SemiReadOnly && SelectedServiceRequest.AssigneeID==ContextStudent.ID; }
            set { }
        }

        private bool IsAcceptedOrDeclined()
        {
            int ApprovedID = ((ServiceRequestDAL)_contextDal).StateListByName().FirstOrDefault(y => y.ItemName == "Jóváhagyva").ID;
            int DeclineID = ((ServiceRequestDAL)_contextDal).StateListByName().FirstOrDefault(y => y.ItemName == "Visszautasítva").ID;
            return SelectedServiceRequest.RequestState == ApprovedID || SelectedServiceRequest.RequestState == DeclineID;
        }

        private DictionaryDAL DictionaryDal { get; set; }
        private TeamDAL TeamDal { get; set; }
        private ServiceStoreDAL ServiceStoreDal { get; set; }
        private StudentDAL StudentDal { get; set; }
        private RequestMessageDAL RequestMessageDal { get; set; }
        public bool SemiReadOnly { get; set; }

        public RelayCommand CreateRequestMessage { get; set; }
        public ServiceRequestViewModel(ServiceRequest selectedServiceRequest, ServiceRequestWindow sourceWindow, Student contextStudent, string semiReadOnly)
        {
            _contextDal = new ServiceRequestDAL();
            this.DictionaryDal = new DictionaryDAL();
            this.TeamDal = new TeamDAL();
            this.ServiceStoreDal = new ServiceStoreDAL();
            this.StudentDal = new StudentDAL();
            this.RequestMessageDal = new RequestMessageDAL();

            this.SemiReadOnly = semiReadOnly == "ServiceRequestsGrid";
            this.SourceWindow = sourceWindow;
            this.SelectedServiceRequest = selectedServiceRequest;

            this.SelectedProviderTeamID = SelectedServiceRequest.ProviderTeamID;
            this.SelectedServiceStore = ServiceStoreDal.FindAll(x => x.ID == SelectedServiceRequest.ServiceID).FirstOrDefault();
            this.SelectedServiceRequestTypeID = SelectedServiceRequest.RequestType;

            
            this.AssigneeList = ReloadAssigneeList();

            RequestMessageList = ReloadRequestMessageList();
            this.CreateRequestMessage = new RelayCommand(RequestMessageCreate,CanCreateRequestMessage);
            this.ContextStudent = contextStudent;
            this.SaveCommand = new RelayCommand(SaveRequest, CanSave);
            if (DictionaryDal.DictionaryListByType("Igény állapota").Where(x => x.ItemName == "Jóváhagyva").FirstOrDefault().ID == SelectedServiceRequest.RequestState)
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

        private void RequestMessageCreate()
        {
            RequestMessage message = new RequestMessage();
            message.SenderID = ContextStudent.ID;
            message.RequestID = SelectedServiceRequest.ID;
            RequestMessageWindow window = new RequestMessageWindow(message)
            {
                Owner = this.SourceWindow
            };
            SwitchWindows(window, true);
            this.RequestMessageList = ReloadRequestMessageList();
        }

        private bool CanCreateRequestMessage()
        {
            return SelectedServiceRequest.ProviderTeamID == ContextStudent.TeamID || SelectedServiceRequest.RequesterTeamID == ContextStudent.TeamID;
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

                if (SelectedServiceRequest.AssigneeID!=null && SelectedServiceRequest.RequestState== DictionaryDal.DictionaryListByType("Igény állapota").Where(x => x.ItemName == "Új").FirstOrDefault().ID)
                {
                    SelectedServiceRequest.RequestState = DictionaryDal.DictionaryListByType("Igény állapota").Where(x => x.ItemName == "Módosítás alatt").FirstOrDefault().ID;
                }
                if (SelectedServiceRequest.ID == 0)
                {
                    SelectedServiceRequest.CreatorID = ContextStudent.ID;
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
            if (SelectedServiceRequest.ServiceID == null && SelectedServiceRequest.ID != 0)
            {
                return new ObservableCollection<ServiceStore>();
            }
            return new ObservableCollection<ServiceStore>(ServiceStoreDal.FindAll(x => x.ProviderTeamID == SelectedProviderTeamID));
        }

        private ObservableCollection<Student> ReloadAssigneeList()
        {
            return new ObservableCollection<Student>(StudentDal.FindAll(x => x.TeamID == SelectedProviderTeamID));
        }

        private ObservableCollection<RequestMessage> ReloadRequestMessageList()
        {
            return new ObservableCollection<RequestMessage>(RequestMessageDal.MessagesByRequest(SelectedServiceRequest.ID));
        }
        
    }
}
