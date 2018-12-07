using SMaP_APP.DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace SMaP_APP.Converters
{
    class RequestStateToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DictionaryDAL dd = new DictionaryDAL();
            string state=dd.FindById((int)value).ItemName;
            Color secondColor=new Color();
            switch (state)
            {
                case "Új":
                    secondColor = Colors.AliceBlue;
                    break;
                case "Módosítás alatt":
                    secondColor = Colors.Aquamarine;
                    break;
                case "Jóváhagyásra vár":
                    secondColor = Colors.Goldenrod;
                    break;
                case "Jóváhagyva":
                    secondColor = Colors.LightGreen;
                    break;
                case "Visszautasítva":
                    secondColor = Colors.Red;
                    secondColor.A = 100;
                    break;
            }
            //LinearGradientBrush rowColor = new LinearGradientBrush();
            //rowColor.StartPoint= new Point(0, 0.5);
            //rowColor.StartPoint = new Point(1, 0.5);
            //rowColor.GradientStops.Add(new GradientStop(Colors.LightBlue, 0.0));
            //rowColor.GradientStops.Add(new GradientStop(secondColor, 0.7));
            return new SolidColorBrush(secondColor);
            //return rowColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
