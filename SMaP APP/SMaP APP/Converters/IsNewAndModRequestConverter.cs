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
    class IsNewAndModRequestConverter : IMultiValueConverter
    {
       

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int type = (int)values[0];
            ServiceRequest val = (ServiceRequest)values[1];
            Student contextStudent = (Student)values[2];
            DictionaryDAL dd = new DictionaryDAL();
            int ModID = dd.DictionaryListByType("Igény típus").Where(x => x.ItemName == "Módosítás").FirstOrDefault().ID;
            if (val.ID == 0 && contextStudent.TeamID == val.RequesterTeamID && type == ModID)
            {
                return true;
            }
            return false;
            
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
