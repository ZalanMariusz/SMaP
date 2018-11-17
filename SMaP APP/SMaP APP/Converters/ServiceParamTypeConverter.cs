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
    class ServiceParamTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ServiceStoreParams param = (ServiceStoreParams)value;
            DictionaryDAL DictionaryDal = new DictionaryDAL();
            ServiceTableFieldDAL serviceTableFieldDal = new ServiceTableFieldDAL();
            ServiceTableField field = serviceTableFieldDal.FindById(param.ServiceTableFieldID);
            return DictionaryDal.FindById(field.FieldTypeID).ItemName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
