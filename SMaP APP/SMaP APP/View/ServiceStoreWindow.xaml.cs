﻿using SMaP_APP.Model;
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
    /// Interaction logic for ServiceStoreWindow.xaml
    /// </summary>
    public partial class ServiceStoreWindow : Window
    {
        public ServiceStoreWindow(Student contextStudent,Teacher ContextTeacher)
        {
            InitializeComponent();
            this.DataContext = new ServiceStoreViewModel(this, contextStudent,ContextTeacher);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }

        public static bool IsValid(string str)
        {
            return int.TryParse(str, out int i);
        }
    }
}
