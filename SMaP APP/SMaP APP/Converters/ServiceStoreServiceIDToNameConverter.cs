using SMaP_APP.DAL;
using SMaP_APP.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SMaP_APP.Converters
{
    class ServiceStoreServiceIDToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ServiceStoreServiceParamsDAL dal = new ServiceStoreServiceParamsDAL();
            var items=dal.FindAll(x => x.ParentServiceStoreID == ((int)value));
            string retval = "";
            foreach (var item in items)
            {
                string TeamName = String.IsNullOrEmpty(item.ServiceStore.Team.ShortTeamName) ? item.ServiceStore.Team.TeamName : item.ServiceStore.Team.ShortTeamName;
                retval += TeamName + "(" + item.ServiceStore.ServiceNumber + "), ";
            }
            if (!string.IsNullOrEmpty(retval))
            {
                return retval.Substring(0, retval.Length - 2);
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
