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
    class ServiceTableFieldViewModel : BaseViewModel<ServiceTableField>
    {
        public ServiceTableField SelectedServiceTableField { get; set; }
        public ObservableCollection<Dictionary> TableFieldList { get; set; }
        public ObservableCollection<ServiceTable> TableList { get; set; }
        public RelayCommand SaveCommand { get; set; }
        private DictionaryDAL DictionaryDal { get; set; }
        private ServiceTableDAL ServiceTableDal { get; set; }
        public ServiceTableFieldViewModel(ServiceTableFieldWindow sourceWindow, ServiceTableField selectedServiceTableField)
        {
            this._contextDal = new ServiceTableFieldDAL();
            this.SourceWindow = sourceWindow;
            this.SelectedServiceTableField = selectedServiceTableField;
            this.SaveCommand = new RelayCommand(SaveServiceTableField, CanExecute);
            this.DictionaryDal = new DictionaryDAL();
            this.ServiceTableDal = new ServiceTableDAL();
            this.TableFieldList = new ObservableCollection<Dictionary>(DictionaryDal.DictionaryListByType(3));
            ServiceTable st = ServiceTableDal.FindById((int)selectedServiceTableField.TableID);
            int contextSessionGroupID= st.Team.SessionGroupID;

            this.TableList = new ObservableCollection<ServiceTable>(ServiceTableDal.FindAll(x => x.Team.SessionGroupID == contextSessionGroupID));
        }

        private void SaveServiceTableField()
        {
            if (this._contextDal.FindAll().Exists(x => x.ID != SelectedServiceTableField.ID && x.TableID == SelectedServiceTableField.TableID && x.FieldName== SelectedServiceTableField.FieldName))
            {
                MessageBox.Show("Az adott mező már létezik a táblán!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (this._contextDal.FindById(SelectedServiceTableField.ID) == null)
                {
                    this._contextDal.Create(SelectedServiceTableField);
                }
                else
                {
                    this._contextDal.Update(SelectedServiceTableField);
                }
                this.SourceWindow.Close();
            }
        }
    }
}
