using SMaP_APP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SMaP_APP.Converters
{
    class StudentImportDataTableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Users u = (Users)value;
            switch (parameter)
            {
                case "Name":
                    return u.FullName;
                case "Neptun":
                    return u.NEPTUN;
                case "Email":
                    return u.Email;
            }
            return "";
            
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
