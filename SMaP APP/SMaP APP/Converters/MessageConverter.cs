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
    class MessageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RequestMessage rm = (RequestMessage)value;
            StudentDAL sd = new StudentDAL();
            Student st = sd.FindById(rm.SenderID);
            //,"yyyy-MM-dd HH:mm:ss"
            return String.Format("{0} - {1}\n{2}",st.Users.FullName,((DateTime)rm.Created).ToString("yyyy-MM-dd HH:mm:ss"),rm.RequestMessage1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
