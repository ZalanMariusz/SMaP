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
    class TeamCaptainNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value==null)
            {
                return "";
            }
            else
            {
                StudentDAL sd = new StudentDAL();
                int filterId = ((Team)value).TeamCaptain == null ? 0 : (int)((Team)value).TeamCaptain;
                Student s = sd.FindAll().FirstOrDefault(x => x.ID == filterId);
                if (s != null)
                {
                    return s.Users.FullName;
                }
                else
                {
                    return "";
                }
                    
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
