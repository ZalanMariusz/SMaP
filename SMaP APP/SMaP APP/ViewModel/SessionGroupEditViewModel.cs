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

namespace SMaP_APP.ViewModel
{
    class SessionGroupEditViewModel : BaseViewModel<SessionGroup>
    {
        public SessionGroup SelectedSessionGroup { get; set; }
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
        }

        private void SaveSessionGroup()
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
        private bool CanSaveSessionGroup()
        {
            return true;
        }
    }
}
