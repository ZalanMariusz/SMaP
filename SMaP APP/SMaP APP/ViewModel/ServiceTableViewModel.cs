using SMaP_APP.Commands;
using SMaP_APP.DAL;
using SMaP_APP.Model;
using SMaP_APP.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SMaP_APP.ViewModel
{
    class ServiceTableViewModel : BaseViewModel<ServiceTable>
    {
        public ServiceTable SelectedServiceTable { get; set; }
        public RelayCommand SaveCommand { get; set; }
        private TeamDAL TeamDal { get; set; }
        public ServiceTableViewModel(ServiceTableWindow SourceWindow, ServiceTable selectedServiceTable)
        {
            this._contextDal = new ServiceTableDAL();
            this.SourceWindow = SourceWindow;
            this.SelectedServiceTable = selectedServiceTable;
            this.SaveCommand = new RelayCommand(SaveServiceTable, CanExecute);
            this.TeamDal = new TeamDAL();
        }

        private void SaveServiceTable()
        {
            int contextSessionGroupID = TeamDal.FindById((int)SelectedServiceTable.TeamID).SessionGroupID;
            if (this._contextDal.FindAll().Exists(x => x.ID != SelectedServiceTable.ID && x.Team.SessionGroupID == contextSessionGroupID && x.TableName == SelectedServiceTable.TableName))
            {
                MessageBox.Show("Az adott tábla már létezik!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (this._contextDal.FindById(SelectedServiceTable.ID) == null)
                {
                    this._contextDal.Create(SelectedServiceTable);
                }
                else
                {
                    this._contextDal.Update(SelectedServiceTable);
                }
                this.SourceWindow.Close();
            }
        }
    }
}
