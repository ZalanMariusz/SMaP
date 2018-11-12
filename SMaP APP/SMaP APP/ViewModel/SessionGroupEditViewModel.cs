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
        public bool GenerateTeams { get; set; }

        public SessionGroupEditViewModel(SessionGroupEditWindow sessionGroupEditWindow, SessionGroup selectedSessionGroup)
        {
            this.SourceWindow = sessionGroupEditWindow;
            this._contextDal = new SessionGroupDAL();
            this.SelectedSessionGroup = selectedSessionGroup;
            this.SaveCommand = new RelayCommand(SaveSessionGroup, CanExecute);
            this.TeacherList = new ObservableCollection<Teacher>(((SessionGroupDAL)_contextDal).TeacherList);
            this.SemesterList = new ObservableCollection<Semester>(((SessionGroupDAL)_contextDal).SemesterList);
            if (SelectedSessionGroup.Semester == null)
            {
                if (SemesterList.Where(x => x.IsActive).SingleOrDefault() != null)
                {
                    this.SelectedSessionGroup.SemesterID = SemesterList.Where(x => x.IsActive).FirstOrDefault().ID;
                    GenerateTeams = true;
                }
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
                if (GenerateTeams)
                {
                    ((SessionGroupDAL)_contextDal).AddTeams(SelectedSessionGroup.ID);
                }
                this.SourceWindow.Close();
            }
        }
    }
}
