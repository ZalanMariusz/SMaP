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
    class ServiceStoreIDProviderTeamNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ServiceStoreDAL serviceStoreDal = new ServiceStoreDAL();
            ServiceStore providedServiceStore = serviceStoreDal.FindById((int)value);
            TeamDAL teamDal = new TeamDAL();
            return teamDal.FindById(providedServiceStore.ProviderTeamID).TeamName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
