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
    class SessionGroupNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                SessionGroup sessionGroup = (SessionGroup)value;
                Teacher teacher;
                using (var dbContext=new SMaPEntities())
                {
                    teacher = dbContext.Teacher.First(x => x.ID == sessionGroup.TeacherID);
                    return String.Format("{0} ({1} - {2})", sessionGroup.SessionGroupName, sessionGroup.Semester.SemesterName, teacher.Users.FullName);
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
