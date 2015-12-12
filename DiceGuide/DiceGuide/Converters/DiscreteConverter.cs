﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace DiceGuide.Converters
{
    class DiscreteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is uint)
                return System.Convert.ToDouble((uint)value);

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is double)
                return System.Convert.ToUInt32((double)value);

            return null;
        }
    }
}
