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
    class ServiceStoreParamAddViewModel : BaseViewModel<ServiceStoreParams>
    {
        public ServiceStore SelectedServiceStore { get; set; }
        public TeamDAL TeamDal { get; set; }
        public ServiceTableDAL ServiceTableDal { get; set; }
        public ServiceTableFieldDAL ServiceTableFieldDal { get; set; }
        public ServiceStoreParamsDAL ServiceStoreParamsDal { get; set; }
        public ServiceStoreDAL ServiceStoreDal { get; set; }

        public RelayCommand SaveCommand { get; set; }
        private Team teamFilter;
        public Team TeamFilter
        {
            get { return teamFilter; }
            set { teamFilter = value; NotifyPropertyChanged(); this.TableList = ReloadServiceTableList(); }
        }

        private string tableNameFilter = "";
        public string TableNameFilter
        {
            get { return tableNameFilter; }
            set { tableNameFilter = value; NotifyPropertyChanged(); TableList = ReloadServiceTableList(); }
        }

        public ObservableCollection<Team> SessionGroupTeamList { get; set; }
        public ObservableCollection<ServiceTable> tableList;
        public ObservableCollection<ServiceTable> TableList
        {
            get { return tableList; }
            set { tableList = value; NotifyPropertyChanged(); }
        }
        public ObservableCollection<ServiceTableField> tableFieldList;
        public ObservableCollection<ServiceTableField> TableFieldList { get { return tableFieldList; } set { tableFieldList = value; NotifyPropertyChanged(); } }
        public ServiceTable selectedTable;
        public ServiceTable SelectedTable
        {
            get { return selectedTable; }
            set { selectedTable = value; NotifyPropertyChanged(); this.TableFieldList = ReloadServiceTableFieldList(); }
        }
        private int SessionGroupID { get; set; }

        public ServiceStoreParams SelectedServiceStoreParam { get; set; }
        private DictionaryDAL DictionaryDal { get; set; }
        public ObservableCollection<Dictionary> CustomFieldTypeList { get; set; }
        public ServiceStoreParamAddViewModel(ServiceStoreParamAddWindow sourceWindow, ServiceStore selectedServiceStore, ServiceStoreParams serviceStoreParam)
        {
            this.SourceWindow = sourceWindow;
            this.SelectedServiceStore = selectedServiceStore;
            this._contextDal = new ServiceStoreParamsDAL();
            this.TeamDal = new TeamDAL();
            this.ServiceTableDal = new ServiceTableDAL();
            this.ServiceTableFieldDal = new ServiceTableFieldDAL();
            this.ServiceStoreParamsDal = new ServiceStoreParamsDAL();
            this.ServiceStoreDal = new ServiceStoreDAL();
            this.DictionaryDal = new DictionaryDAL();

            this.SessionGroupID = TeamDal.FindById(selectedServiceStore.ProviderTeamID).SessionGroupID;
            this.TeamFilter = TeamDal.FindById(SelectedServiceStore.ProviderTeamID);
            this.SessionGroupTeamList = new ObservableCollection<Team>(TeamDal.FindAll(x => x.SessionGroupID == SessionGroupID));
            this.TableList = ReloadServiceTableList();
            this.TableFieldList = ReloadServiceTableFieldList();
            this.SelectedServiceStoreParam = serviceStoreParam;
            SelectedServiceStoreParam.ServiceStore = SelectedServiceStore;
            CustomFieldTypeList = new ObservableCollection<Dictionary>(DictionaryDal.DictionaryListByType(3));
            this.SaveCommand = new RelayCommand(SaveParameter, CanSaveParameter);
        }

        private bool CanSaveParameter(object param)
        {
            return this.CanExecute() && ((SelectedServiceStoreParam.IsCustom && SelectedServiceStoreParam.CustomParamTypeID!=null)
                || (!SelectedServiceStoreParam.IsCustom && ((DataGrid)param).SelectedItem != null));
        }

        private void SaveParameter(object param)
        {
            if (ServiceStoreParamsDal.FindAll().Exists(x=>
                x.ID!= SelectedServiceStoreParam.ID 
                && x.ServiceID== SelectedServiceStore.ID
                && x.ParamName== SelectedServiceStoreParam.ParamName))
            {
                MessageBox.Show("A szolgáltatáshoz már tartozik ilyen nevű paraméter!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (!SelectedServiceStoreParam.IsCustom)
                {
                    SelectedServiceStoreParam.ServiceTableFieldID = ((ServiceTableField)((DataGrid)param).SelectedItem).ID;
                    SelectedServiceStoreParam.CustomParamTypeID = null;
                }
                this.SourceWindow.Close();
            }
        }

        public override void Close()
        {
            SelectedServiceStore.ServiceStoreParams.Remove(SelectedServiceStoreParam);
            this.SourceWindow.Close();
        }

        public ObservableCollection<ServiceTableField> ReloadServiceTableFieldList()
        {
            if (SelectedTable == null)
            {
                return new ObservableCollection<ServiceTableField>();
            }
            return new ObservableCollection<ServiceTableField>(ServiceTableFieldDal.FindAll(x => x.TableID == SelectedTable.ID));
        }

        private ObservableCollection<ServiceTable> ReloadServiceTableList()
        {
            if (TeamFilter == null)
            {
                return new ObservableCollection<ServiceTable>(ServiceTableDal.FindAll(x => x.Team.SessionGroupID == SessionGroupID));
            }
            return new ObservableCollection<ServiceTable>(ServiceTableDal.FindAll(x => x.Team.SessionGroupID == SessionGroupID && x.TeamID == TeamFilter.ID && x.TableName.Contains(TableNameFilter)));
        }

        public void DeleteFilter()
        {
            this.TeamFilter = null;
        }
    }
}
