using System;
using System.Globalization;
using System.Windows.Data;
using WpfShell.ViewModels;

namespace WpfShell.Converters
{
    public class WidthToScaledWidthMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var columnWidth = (double) values[0];
            var rectangleWidth = StaticData.DefinedWidth;
            StaticData.Scale = columnWidth/rectangleWidth;
            return StaticData.DefinedHeight*StaticData.Scale;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
