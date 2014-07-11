using System;
using System.Globalization;
using System.Windows.Data;
using InscribedCircles.MainApp.ViewModels;

namespace InscribedCircles.MainApp.Converters
{
    public class CoordinatesToScalableCoordinatesMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)values[0] * StaticData.Scale;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
