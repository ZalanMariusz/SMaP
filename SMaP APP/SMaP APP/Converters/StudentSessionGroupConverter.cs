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
    class StudentSessionGroupConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                int TeamID = (int)value;
                using (var dbcontext = new SMaPEntities())
                {
                    Team team= dbcontext.Team.Single(x => x.ID == TeamID);
                    return dbcontext.SessionGroup.Single(x => x.ID == team.SessionGroupID).SessionGroupName;
                }
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
