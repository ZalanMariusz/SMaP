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
        public ObservableCollection<Team> TeamList { get; set; }
        public ObservableCollection<ServiceStoreParams> InputFieldList { get; set; }
        public ObservableCollection<ServiceStoreParams> OutputFieldList { get; set; }

        private TeamDAL TeamDal { get; set; }
        private SessionGroupDAL SessionGroupDal { get; set; }
        private StudentDAL StudentDal { get; set; }
        private ServiceStoreParamsDAL ServiceStoreParamsDal { get; set; }
        //private ServiceStoreDAL ServiceStoreDal { get; set; }

        public RelayCommand SaveCommand { get; set; }

        public RelayCommand InputFieldCreateCommand { get; set; }
        public RelayCommand InputFieldDeleteCommand { get; set; }

        public RelayCommand OutputFieldCreateCommand { get; set; }
        public RelayCommand OutputFieldDeleteCommand { get; set; }

        public ServiceStoreEditViewModel(ServiceStoreEditWindow serviceStoreEditWindow, ServiceStore selectedServiceStore)
        {
            this.SelectedServiceStore = selectedServiceStore;
            this.SourceWindow = serviceStoreEditWindow;

            this.SaveCommand = new RelayCommand(SaveServiceStore);
            this.InputFieldCreateCommand = new RelayCommand(AddInputParam);
            this.InputFieldDeleteCommand = new RelayCommand(DeleteSelectedInputField, CanEditOrDeleteSelectedItem);

            this.OutputFieldCreateCommand = new RelayCommand(AddOutputParam);
            this.OutputFieldDeleteCommand = new RelayCommand(DeleteSelectedOutputField, CanEditOrDeleteSelectedItem);

            this._contextDal = new ServiceStoreDAL();
            this.TeamDal = new TeamDAL();
            this.SessionGroupDal = new SessionGroupDAL();
            this.StudentDal = new StudentDAL();
            this.ServiceStoreParamsDal = new ServiceStoreParamsDAL();

            int TeamID = StudentDal.FindAll().FirstOrDefault(x => x.ID == SelectedServiceStore.CreatorID).TeamID;
            int SessionGroupID = TeamDal.FindAll().FirstOrDefault(x => x.ID == TeamID).SessionGroupID;
            this.TeamList = new ObservableCollection<Team>(TeamDal.FindAll(x => x.SessionGroupID == SessionGroupID));

            this.InputFieldList = ReloadInputFieldList();
            this.OutputFieldList = ReloadOutputFieldList();
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
                    List<ServiceStoreParams> templist = new List<ServiceStoreParams>();
                    foreach (var item in SelectedServiceStore.ServiceStoreParams)
                    {
                        templist.Add(item);
                    }
                    SelectedServiceStore.ServiceStoreParams.Clear();
                    this._contextDal.Create(SelectedServiceStore);
                    SelectedServiceStore.ServiceStoreParams = templist;
                    this._contextDal.Update(SelectedServiceStore);
                }
                else
                {
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
        private bool CanEditOrDeleteSelectedItem(object param)
        {
            return ((DataGrid)param).SelectedItem != null;
        }
    }
}
