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
using SMaP_APP.ViewModel;
using System.Data;

namespace SMaP_APP.View
{
    /// <summary>
    /// Interaction logic for StudentImportWindow.xaml
    /// </summary>
    public partial class StudentImportWindow : Window
    {
        public StudentImportWindow(DataTable dt)
        {
            InitializeComponent();
            this.DataContext = new StudentImportViewModel(dt,this);
        }
    }
}
