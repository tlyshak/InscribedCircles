using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using WpfShell.ViewModels;

namespace WpfShell.Converters
{
    public class SizeToScalableSizeMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var firstValue = values.FirstOrDefault();
            if (firstValue == null) return 0;
            var value = (double)firstValue;
            return value * StaticData.Scale;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
