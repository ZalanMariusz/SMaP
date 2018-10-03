using SMaP_APP.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMaP_APP.ViewModel
{
    class TeacherWindowViewModel:BaseViewModel
    {
        public TeacherWindowViewModel(TeacherWindow teacherWindow)
        {
            this.SourceWindow = teacherWindow;
        }
    }
}
