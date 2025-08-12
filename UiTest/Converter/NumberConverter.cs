using System;
using System.Globalization;
using System.Windows.Data;
namespace UiTest.Converter
{
    public class NumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value?.ToString();

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(value?.ToString()))
                    return null;

                return System.Convert.ChangeType(value, Nullable.GetUnderlyingType(targetType) ?? targetType);
            }
            catch
            {
                return Binding.DoNothing;
            }
        }
    }
}
