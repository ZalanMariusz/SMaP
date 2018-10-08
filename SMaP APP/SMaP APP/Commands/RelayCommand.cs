using System;
using System.Windows.Input;

namespace SMaP_APP.Commands
{
    class RelayCommand : ICommand
    {
        private Action<object> executeWithParam;
        private Func<object, bool> canExecuteWithParam;
        private Action executeWithoutParam;
        private Func<bool> canExecuteWithoutParam;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.executeWithParam = execute;
            this.canExecuteWithParam = canExecute;
        }

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            this.executeWithoutParam = execute;
            this.canExecuteWithoutParam = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (parameter==null)
            {
                return this.canExecuteWithoutParam == null || this.canExecuteWithoutParam.Invoke();
            }
            else
            {
                return this.canExecuteWithParam == null || this.canExecuteWithParam(parameter);
            }
            
        }

        public void Execute(object parameter)
        {
            if (parameter==null)
            {
                this.executeWithoutParam();
            }
            else
            {
                this.executeWithParam(parameter);
            }
            
        }
    }
}
