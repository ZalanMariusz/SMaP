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
    class DictionaryViewModel : BaseViewModel<Dictionary>
    {
        public Dictionary SelectedDictionary { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public ObservableCollection<DictionaryType> DictionaryTypeList { get; set; }
        public bool IsProtected { get; set; }

        public DictionaryViewModel(DictionaryEditWindow dictionaryEditWindow,Dictionary SelectedDictionary)
        {
            this.SelectedDictionary = SelectedDictionary;
            this.SourceWindow = dictionaryEditWindow;
            this.SaveCommand = new RelayCommand(SaveDictionary, CanExecute);
            this._contextDal = new DictionaryDAL();
            this.DictionaryTypeList = new ObservableCollection<DictionaryType>(((DictionaryDAL)_contextDal).DictionaryTypeList());
            IsProtected = SelectedDictionary.IsProtected;
        }
        public void SaveDictionary()
        {
            if (this._contextDal.FindAll().Exists(x => x.ID != SelectedDictionary.ID && x.DictionaryTypeID == SelectedDictionary.DictionaryTypeID && x.ItemName == SelectedDictionary.ItemName))
            {
                MessageBox.Show("Az adott típushoz már létezik ilyen nevű elem!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                SelectedDictionary.IsProtected = IsProtected;
                if (this._contextDal.FindById(SelectedDictionary.ID) == null)
                {
                    this._contextDal.Create(SelectedDictionary);
                }
                else
                {
                    this._contextDal.Update(SelectedDictionary);
                }
                this.SourceWindow.Close();
            }
        }
        
    }
}
