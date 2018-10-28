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
    class SemesterCopyViewModel : BaseViewModel<Semester>
    {
        public RelayCommand SaveCommand { get; set; }
        public int SourceId { get; set; }
        public int NewSemesterTypeId { get; set; }
        public string NewSemesterName { get; set; }
        public ObservableCollection<Dictionary> SemesterTypes { get; set; }

        public SemesterCopyViewModel(CopySemesterWindow copySemesterWindow,Semester sourceSemester)
        {
            this.SourceId = sourceSemester.ID;
            this.SourceWindow = copySemesterWindow;
            this.SaveCommand = new RelayCommand(CopySemester, CanExecute);
            this._contextDal = new SemesterDAL();
            this.SemesterTypes = new ObservableCollection<Dictionary>(((SemesterDAL)_contextDal).SemesterTypes);
        }

        public void CopySemester()
        {
            if (((SemesterDAL)this._contextDal).FindAll(x => x.SemesterName == this.NewSemesterName).FirstOrDefault() != null)
            {
                MessageBox.Show("Szemeszter ezzel a névvel már létezik!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                ((SemesterDAL)_contextDal).CopySemester(SourceId, NewSemesterName, NewSemesterTypeId);
                this.SourceWindow.Close();
            }
            
        }
    }
}
