using SMaP_APP.Commands;
using SMaP_APP.DAL;
using SMaP_APP.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SMaP_APP.ViewModel
{
    class BaseViewModel<TEntity> : INotifyPropertyChanged where TEntity : class, IBaseModel
    {
        internal Window SourceWindow { get; set; }
        internal GenericDAL<TEntity> _contextDal { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RelayCommand CloseCommand { get; set; }
       

        internal void SwitchWindows(Window target, bool toModal = false)
        {
            if (!toModal)
            {
                target.Show();
                SourceWindow.Close();
            }
            else
            {
                target.ShowDialog();
            }

        }
        public BaseViewModel()
        {
            this.CloseCommand = new RelayCommand(Close);
        }

        public virtual bool CanExecute()
        {
            return IsValid(SourceWindow);
        }

        public virtual bool IsValid(DependencyObject obj)
        {
            // The dependency object is valid if it has no errors and all
            // of its children (that are dependency objects) are error-free.
            return !Validation.GetHasError(obj) &&
            LogicalTreeHelper.GetChildren(obj)
            .OfType<DependencyObject>()
            .All(IsValid);
        }

        public virtual void Close()
        {
            _contextDal.CleanUp();
            this.SourceWindow.Close();
        }
    }
}
