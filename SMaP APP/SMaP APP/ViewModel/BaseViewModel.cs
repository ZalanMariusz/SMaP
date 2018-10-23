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

namespace SMaP_APP.ViewModel
{
    class BaseViewModel<TEntity> : INotifyPropertyChanged where TEntity : class, IBaseModel
    {
        internal Window SourceWindow { get; set; }
        internal GenericDAL<TEntity> _contextDal { get; set; }
        private static SMaPEntities _dbContext = null;

        public event PropertyChangedEventHandler PropertyChanged;

        internal void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RelayCommand CloseCommand { get; set; }
        public static SMaPEntities DbContext
        {
            get
            {
                _dbContext = new SMaPEntities();
                return _dbContext;
            }
        }

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
        internal void Close()
        {
            this.SourceWindow.Close();
        }
    }
}
