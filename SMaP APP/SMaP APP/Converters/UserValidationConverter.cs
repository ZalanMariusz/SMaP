using SMaP_APP.DAL;
using SMaP_APP.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace SMaP_APP.Converters
{
    class UserValidationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            UsersDAL ud = new UsersDAL();
            if (value.GetType() == typeof(Users))
            {
                Users val = (Users)value;
                Users existingUser = new Users();
                switch (parameter)
                {
                    case "Email":
                        existingUser = ud.FindAll(x => x.Email == val.Email).FirstOrDefault();
                        break;
                    case "Neptun":
                        existingUser = ud.FindAll(x => x.NEPTUN == val.NEPTUN).FirstOrDefault();
                        break;
                }
                if (existingUser == null)
                {
                    return new SolidColorBrush(Colors.Black);
                }
                return new SolidColorBrush(Colors.Red);
            }
            else
            {
                string paramString = parameter as string;
                string SessionGroupName = (string)value;
                SessionGroupDAL sgd = new SessionGroupDAL();
                SessionGroup sg = sgd.FindAll(x => x.Semester.IsActive && x.SessionGroupName == SessionGroupName).FirstOrDefault();
                if (sg == null)
                {
                    return new SolidColorBrush(Colors.Red);
                }
                return new SolidColorBrush(Colors.Black);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
