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
    class ServiceStoreParamConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ServiceStore param = (ServiceStore)value;
            string retval = "";
            bool inout = false;
            if ((string)parameter=="IN")
            {
                inout = true;
            }
            foreach (var item in param.ServiceStoreParams.Where(x=>!x.Deleted && x.InOut==inout))
            {
                retval += item.ServiceTableField.ServiceTable.TableName+"."+item.ServiceTableField.FieldName+", ";
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
