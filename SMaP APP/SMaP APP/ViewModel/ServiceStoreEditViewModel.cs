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
    class ServiceStoreEditViewModel : BaseViewModel<ServiceStore>
    {
        public ServiceStore SelectedServiceStore { get; set; }
        public ObservableCollection<Team> TeamList { get; set; }

        private TeamDAL TeamDal { get; set; }
        private SessionGroupDAL SessionGroupDal { get; set; }
        private StudentDAL StudentDal {get;set;}

        public RelayCommand SaveCommand { get; set; }
        public ServiceStoreEditViewModel(ServiceStoreEditWindow serviceStoreEditWindow, ServiceStore selectedServiceStore)
        {
            this.SelectedServiceStore = selectedServiceStore;
            this.SourceWindow = serviceStoreEditWindow;
            this.SaveCommand = new RelayCommand(SaveServiceStore);

            this._contextDal = new ServiceStoreDAL();
            this.TeamDal = new TeamDAL();
            this.SessionGroupDal = new SessionGroupDAL();
            this.StudentDal = new StudentDAL();
            int TeamID = StudentDal.FindAll().FirstOrDefault(x => x.ID == SelectedServiceStore.CreatorID).TeamID;
            int SessionGroupID = TeamDal.FindAll().FirstOrDefault(x => x.ID == TeamID).SessionGroupID;
            this.TeamList = new ObservableCollection<Team>(TeamDal.FindAll(x=>x.SessionGroupID==SessionGroupID));
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
                    this._contextDal.Create(SelectedServiceStore);
                }
                else
                {
                    this._contextDal.Update(SelectedServiceStore);
                }
                this.SourceWindow.Close();
            }
        }
    }
}
