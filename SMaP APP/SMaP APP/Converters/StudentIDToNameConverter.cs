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
    class StudentIDToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int? StudentID = (int?)value;
            if (StudentID!=null)
            {
                UsersDAL ud = new UsersDAL();
                StudentDAL sd = new StudentDAL();
                Student s = sd.FindById((int)StudentID);
                return ud.FindById(s.UserID).FullName;
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
