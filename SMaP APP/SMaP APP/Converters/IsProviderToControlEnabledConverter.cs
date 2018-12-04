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
            Student contextStudend = (Student)values[1];
            if (parameter!=null && parameter.ToString()=="V" && serviceStore.ProviderTeamID != contextStudend.TeamID)
            {
                return Visibility.Hidden;
            }
            else if (parameter != null && parameter.ToString() == "V" && serviceStore.ProviderTeamID == contextStudend.TeamID)
            {
                return Visibility.Visible;
            }
            else
            {
                return serviceStore.ProviderTeamID == contextStudend.TeamID;
            }

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
