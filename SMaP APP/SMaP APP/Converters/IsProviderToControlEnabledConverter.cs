using SMaP_APP.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SMaP_APP.Converters
{
    class IsProviderToControlEnabledConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            ServiceStore serviceStore = (ServiceStore)values[0];
            Student contextStudent = (Student)values[1];
            if (contextStudent==null)
            {
                return Visibility.Hidden;
            }
            if (parameter!=null && parameter.ToString()=="V" && (contextStudent==null || serviceStore.ProviderTeamID != contextStudent.TeamID))
            {
                return Visibility.Hidden;
            }
            else if (parameter != null && parameter.ToString() == "V" && serviceStore.ProviderTeamID == contextStudent.TeamID)
            {
                return Visibility.Visible;
            }
            else
            {
                return serviceStore.ProviderTeamID == contextStudent.TeamID;
            }

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
