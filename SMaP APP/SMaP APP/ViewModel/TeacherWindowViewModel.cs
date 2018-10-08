using SMaP_APP.Commands;
using SMaP_APP.DAL;
using SMaP_APP.Model;
using SMaP_APP.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SMaP_APP.ViewModel
{
    class TeacherWindowViewModel : BaseViewModel<Teacher>
    {
        public RelayCommand SemesterEditCommand { get; set; }
        public RelayCommand SemesterCreateCommand { get; set; }
        public RelayCommand SemesterDeleteCommand { get; set; }
        private ObservableCollection<Semester> semesterList;
        public ObservableCollection<Semester> SemesterList
        {
            get { return semesterList; }
            set { this.semesterList = value; NotifyPropertyChanged(); }
        }
        public SemesterDal SemesterDal { get; set; }

        public TeacherWindowViewModel(TeacherWindow teacherWindow)
        {
            this.SourceWindow = teacherWindow;
            this._contextDal = new TeacherDAL(DbContext);


            this.SemesterCreateCommand = new RelayCommand(CreateSemester);
            this.SemesterEditCommand = new RelayCommand(EditSemester, CanEditSemester);
            this.SemesterDeleteCommand = new RelayCommand(DeleteSemester, CanDeleteSemester);

            this.SemesterList = new ObservableCollection<Semester>(
                DbContext.Semester.Where(x => !x.Deleted).ToList());
        }


        private void EditSemester(object param)
        {
            SemesterWindow target = new SemesterWindow((Semester)((ListBox)param).SelectedItem);
            target.Owner = this.SourceWindow;
            SwitchWindows(target, true);
        }
        private void DeleteSemester(object param)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Valóban törli?", "Törlés megerősítése", MessageBoxButton.YesNo,MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Semester selectedSemester = (Semester)((ListBox)param).SelectedItem;
                semesterList.Remove(selectedSemester);
                ((TeacherDAL)_contextDal).DeleteSemester(selectedSemester);
            }
        }
        private void CreateSemester()
        {
            Semester newSemester = new Semester() { IsActive = false };
            SemesterWindow target = new SemesterWindow(newSemester);
            target.Owner = this.SourceWindow;
            SwitchWindows(target, true);
            this.SemesterList = new ObservableCollection<Semester>(
               DbContext.Semester.Where(x => !x.Deleted).ToList());
        }

        private bool CanDeleteSemester(object param)
        {
            return (Semester)((ListBox)param).SelectedItem != null;
        }
        private bool CanEditSemester(object param)
        {
            return (Semester)((ListBox)param).SelectedItem != null;
        }
    }
}
