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
    class RequesterTeamsNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ServiceStore serviceStore = (ServiceStore)value;
            string retval="";
            foreach (var item in serviceStore.ServiceStoreUserTeams.Where(x=>!x.Deleted))
            {
                if (!item.Team.Deleted)
                {
                    string shortTeamName = String.IsNullOrEmpty(item.Team.ShortTeamName) ? item.Team.TeamName : item.Team.ShortTeamName;
                    retval += shortTeamName + ", ";
                }    
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
