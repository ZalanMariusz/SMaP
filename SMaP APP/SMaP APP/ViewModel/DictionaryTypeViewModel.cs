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
    class DictionaryTypeViewModel : BaseViewModel<DictionaryType>
    {
        public DictionaryType SelectedDictionaryType { get; set; }
        public RelayCommand SaveCommand { get; set; }

        public DictionaryTypeViewModel(DictionaryTypeEditWindow dictionaryEditWindow, DictionaryType SelectedDictionaryType)
        {
            this.SelectedDictionaryType = SelectedDictionaryType;
            SelectedDictionaryType.IsSessionGroup = true;
            this.SourceWindow = dictionaryEditWindow;
            this.SaveCommand = new RelayCommand(SaveDictionaryType, CanExecute);
            this._contextDal = new DictionaryTypeDAL();
        }
        public void SaveDictionaryType()
        {
            if (this._contextDal.FindAll().Exists(x => x.ID != SelectedDictionaryType.ID && x.TypeName == SelectedDictionaryType.TypeName))
            {
                MessageBox.Show("Az adott típushoz már létezik ilyen nevű elem!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (this._contextDal.FindById(SelectedDictionaryType.ID) == null)
                {
                    this._contextDal.Create(SelectedDictionaryType);
                }
                else
                {
                    this._contextDal.Update(SelectedDictionaryType);
                }
                this.SourceWindow.Close();
            }
        }
    }
}
