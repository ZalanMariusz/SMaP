using SMaP_APP.Model;
using SMaP_APP.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SMaP_APP.View
{
    /// <summary>
    /// Interaction logic for TeacherEditorWindow.xaml
    /// </summary>
    public partial class TeacherEditorWindow : Window
    {
        public TeacherEditorWindow(Teacher teacher)
        {
            InitializeComponent();
            this.DataContext = new TeacherEditorWindowViewModel(this,teacher);
        }
    }
}
