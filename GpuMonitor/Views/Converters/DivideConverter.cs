using System;
using System.Globalization;
using System.Windows.Data;

namespace GpuMonitor.Views.Converters
{
    public class DivideConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return 0;
            }

            return (int)value / 4;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}