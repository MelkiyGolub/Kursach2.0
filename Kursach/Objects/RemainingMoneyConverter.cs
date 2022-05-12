using System;
using System.Globalization;
using System.Windows.Data;

namespace Kursach.Objects;

internal class RemainingMoneyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => $"Остаток денег в кассе: {value} ₽";

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
}