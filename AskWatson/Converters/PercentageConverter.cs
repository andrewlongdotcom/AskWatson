using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace AskWatson.Converters
{
    public class PercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null ||
                value.GetType() != typeof(float))
            {
                return "n/a";
            }
            else
            {
                return string.Format(
                    "{0}%",
                    System.Convert.ToInt16(((float)value) * 100, System.Globalization.CultureInfo.CurrentCulture));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
