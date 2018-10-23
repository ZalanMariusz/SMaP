﻿using SMaP_APP.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SMaP_APP.Converters
{
    class TeamNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                Team item = (Team)value;
                SessionGroup sg;
                using (var DbContext = new SMaPEntities())
                {
                    sg = DbContext.SessionGroup.Where(x => x.ID == item.SessionGroupID).FirstOrDefault();
                }
                return String.Format("{0} - {1}", item.TeamName, sg.SessionGroupName);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
