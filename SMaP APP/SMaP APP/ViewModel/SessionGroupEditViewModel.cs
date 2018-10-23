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
    class SessionGroupEditViewModel : BaseViewModel<SessionGroup>
    {
        private SessionGroup selectedSessionGroup;
        public SessionGroup SelectedSessionGroup
        {
            get { return selectedSessionGroup; }
            set { selectedSessionGroup = value; NotifyPropertyChanged(); }
        }
        public RelayCommand SaveCommand { get; set; }
        public ObservableCollection<Teacher> TeacherList { get; set; }
        public ObservableCollection<Semester> SemesterList { get; set; }

        public SessionGroupEditViewModel(SessionGroupEditWindow sessionGroupEditWindow, SessionGroup selectedSessionGroup)
        {
            this.SourceWindow = sessionGroupEditWindow;
            this._contextDal = new SessionGroupDAL(DbContext);
            this.SelectedSessionGroup = selectedSessionGroup;
            this.SaveCommand = new RelayCommand(SaveSessionGroup, CanSaveSessionGroup);
            this.TeacherList = new ObservableCollection<Teacher>(((SessionGroupDAL)_contextDal).TeacherList);
            this.SemesterList = new ObservableCollection<Semester>(((SessionGroupDAL)_contextDal).SemesterList);
            if (SelectedSessionGroup.Semester == null)
            {
                this.SelectedSessionGroup.SemesterID = SemesterList.Where(x => x.IsActive).FirstOrDefault().ID;
            }
        }

        private void SaveSessionGroup()
        {
            if (this._contextDal.FindAll().Exists(x => x.ID != SelectedSessionGroup.ID && x.SemesterID == SelectedSessionGroup.SemesterID && x.SessionGroupName == SelectedSessionGroup.SessionGroupName))
            {
                MessageBox.Show("Az adott félévben már létezik ilyen nevű csoport!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (this._contextDal.FindById(SelectedSessionGroup.ID) == null)
                {
                    this._contextDal.Create(SelectedSessionGroup);
                }
                else
                {
                    this._contextDal.Update(SelectedSessionGroup);
                }
                this.SourceWindow.Close();
            }
        }
        private bool CanSaveSessionGroup()
        {
            return !string.IsNullOrEmpty(this.SelectedSessionGroup.SessionGroupName) 
                && !(this.SelectedSessionGroup.TeacherID==null || this.SelectedSessionGroup.TeacherID == 0)
                && !(this.SelectedSessionGroup.SemesterID==null || this.SelectedSessionGroup.SemesterID == 0);
        }
    }
}
