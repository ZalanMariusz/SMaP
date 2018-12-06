using SMaP_APP.DAL;
using SMaP_APP.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SMaP_APP.Converters
{
    class StudentIsTeamCaptainToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Student val = (Student)value;
            TeamDAL td = new TeamDAL();
            Team StudentTeam = td.FindById(val.TeamID);
            if (StudentTeam.TeamCaptain != null && StudentTeam.TeamCaptain==val.ID)
            {
                return Visibility.Visible;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
