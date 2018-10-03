using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SMaP_APP.Commands.InterFaces
{
    interface NavigationCommand
    {
        void Navigate(Window source, Window target);
    }
}
