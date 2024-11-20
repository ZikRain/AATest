using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPF.Converters
{
    [ValueConversion(typeof(Sex), typeof(string))]
    public class SexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
          System.Globalization.CultureInfo culture)
        {
            value = value ?? Sex.DefaultSex;
            var s = (Sex)value;
            switch (s)
            {
                case Sex.Male: return "М";
                case Sex.Female: return "Ж";
                case Sex.DefaultSex: return "-";
                    default: return s;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            try
            {
                return (Sex)value;
            }
            catch
            {
                return value;
            }
        }
    }
}
