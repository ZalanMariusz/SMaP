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
using SMaP_APP.Model;

namespace SMaP_APP.View
{
    /// <summary>
    /// Interaction logic for PasswordChangeWindow.xaml
    /// </summary>
    public partial class PasswordChangeWindow : Window
    {
        public PasswordChangeWindow()
        {
            InitializeComponent();
            this.DataContext = new PasswordChangeViewModel(this);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((PasswordChangeViewModel)this.DataContext).Password = PasswordBox.Password;
        }

        private void PasswordBoxConfirm_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((PasswordChangeViewModel)this.DataContext).PasswordConfirm = PasswordBoxConfirm.Password;
        }
    }
}
