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
    class StudentNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                Student student = (Student)value;
                using (var context = new SMaPEntities())
                {
                    Team studentTeam = context.Team.Where(x => x.ID == student.TeamID).FirstOrDefault();
                    return String.Format("{0} ({1})", student.Users.FullName, studentTeam.TeamName);
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
