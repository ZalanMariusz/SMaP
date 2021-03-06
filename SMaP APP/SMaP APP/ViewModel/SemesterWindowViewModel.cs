﻿using SMaP_APP.Commands;
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
    class SemesterWindowViewModel : BaseViewModel<Semester>
    {
        public RelayCommand SaveCommand { get; set; }
        private Semester selectedSemester;
        public Semester SelectedSemester
        {
            get { return selectedSemester; }
            set { selectedSemester = value; NotifyPropertyChanged(); }
        }
        public ObservableCollection<Dictionary> SemesterTypes { get; set; }

        public SemesterWindowViewModel(SemesterWindow semesterWindow, Semester selectedSemester)
        {
            this.SourceWindow = semesterWindow;
            this._contextDal = new SemesterDAL();
            this.SelectedSemester = selectedSemester;
            this.SaveCommand = new RelayCommand(SaveSemester, CanExecute);
            SemesterTypes = new ObservableCollection<Dictionary>(((SemesterDAL)_contextDal).SemesterTypes);
        }
        public void SaveSemester()
        {
            if (((SemesterDAL)this._contextDal).FindAll(x => x.SemesterName == this.selectedSemester.SemesterName && x.ID != selectedSemester.ID).FirstOrDefault() != null)
            {
                MessageBox.Show("Szemeszter ezzel a névvel már létezik!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (this._contextDal.FindById(SelectedSemester.ID) == null)
                {
                    this._contextDal.Create(SelectedSemester);
                }
                else
                {
                    this._contextDal.Update(SelectedSemester);
                }
                this.SourceWindow.Close();
            }
        }
    }
}
