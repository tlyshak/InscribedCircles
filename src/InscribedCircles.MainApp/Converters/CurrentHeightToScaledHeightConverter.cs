﻿using System;
using System.Globalization;
using System.Windows.Data;
using InscribedCircles.MainApp.ViewModels;

namespace InscribedCircles.MainApp.Converters
{
    public class CurrentHeightToScaledHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            StaticData.Scale = (double)value / StaticData.DefinedWidth;
            return StaticData.DefinedHeight * StaticData.Scale;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}