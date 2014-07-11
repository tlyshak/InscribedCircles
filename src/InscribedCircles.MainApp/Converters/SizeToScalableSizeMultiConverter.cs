using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using InscribedCircles.MainApp.ViewModels;

namespace InscribedCircles.MainApp.Converters
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
