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
    class StudentImportTeamValidationConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string SessionGroupName = (string)values[0];
            string TeamName = (string)values[1];
            SessionGroupDAL sgd = new SessionGroupDAL();
            SessionGroup sg = sgd.FindAll(x=>x.Semester.IsActive && x.SessionGroupName== SessionGroupName).FirstOrDefault();
            if (sg==null)
            {
                return new SolidColorBrush(Colors.Red);
            }
            TeamDAL td = new TeamDAL();
            Team t = td.FindAll(x=>x.TeamName== TeamName && x.SessionGroupID==sg.ID).FirstOrDefault();
            if (t==null)
            {
                return new SolidColorBrush(Colors.Red);
            }
            return new SolidColorBrush(Colors.Black);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
