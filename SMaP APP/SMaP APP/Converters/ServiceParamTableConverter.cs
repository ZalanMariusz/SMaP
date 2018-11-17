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
    class ServiceParamTableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ServiceStoreParams param = (ServiceStoreParams)value;
            ServiceTableFieldDAL ServiceTableFieldDal = new ServiceTableFieldDAL();
            ServiceTableDAL ServiceTableDal = new ServiceTableDAL();
            ServiceTableField sf = ServiceTableFieldDal.FindById(param.ServiceTableFieldID);
            return ServiceTableDal.FindAll(x => x.ID == sf.ID).FirstOrDefault().TableName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
