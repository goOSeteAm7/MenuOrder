using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MenuOrder
{
    // Класс конвертера для преобразования значения в Visibility на основе строки TrueValue
    public class VisibilityConverter : IValueConverter
    {
        // Свойство для хранения значения, которое приведет к Visibility.Visible
        public string TrueValue { get; set; }

        // Метод Convert для преобразования значения в Visibility
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Возвращает Visibility.Visible, если значение совпадает с TrueValue, иначе возвращает Visibility.Collapsed
            return value?.ToString() == TrueValue ? Visibility.Visible : Visibility.Collapsed;
        }

        // Метод ConvertBack не реализован и вызовет исключение, если будет вызван
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
