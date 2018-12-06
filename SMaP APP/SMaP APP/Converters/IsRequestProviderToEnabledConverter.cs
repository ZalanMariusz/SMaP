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
    class IsRequestProviderToEnabledConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            ServiceRequest val = (ServiceRequest)values[0];
            DictionaryDAL dd = new DictionaryDAL();
            int ApprovedID = dd.DictionaryListByType("Igény állapota").Where(x => x.ItemName == "Jóváhagyva").FirstOrDefault().ID;
            if ((parameter.ToString() == "NewNotEditable" && val.ID == 0) || ApprovedID == val.RequestState || (bool)values[2])
            {
                return false;
            }
            return val.ProviderTeamID == (int)values[1];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
