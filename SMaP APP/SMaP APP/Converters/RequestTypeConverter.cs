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
    class RequestTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ServiceRequest sr = (ServiceRequest)value;
            ServiceStoreDAL srd = new ServiceStoreDAL();
            DictionaryDAL dd = new DictionaryDAL();
            if (sr.RequestType== 19)
            {
                return String.Format("{0}({1})", dd.FindById(sr.RequestType).ItemName,srd.FindById((int)sr.ServiceID).ServiceNumber);
            }
            else
            {
                return dd.FindById(sr.RequestType).ItemName;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
