using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SMaP_APP.ViewModel
{
    class BaseViewModel
    {
        internal Window SourceWindow { get; set; }
        internal void SwitchWindows(Window target)
        {
            target.Show();
            SourceWindow.Close();
        }
    }
}
