using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MenuOrder
{
    // Класс конвертера для преобразования значения в Visibility на основе строки "Menu"
    public class MenuVisibilityConverter : IValueConverter
    {
        // Метод Convert для преобразования значения в Visibility
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Возвращает Visibility.Visible, если значение совпадает с "Menu", иначе возвращает Visibility.Collapsed
            return value?.ToString() == "Menu" ? Visibility.Visible : Visibility.Collapsed;
        }

        // Метод ConvertBack не реализован и вызовет исключение, если будет вызван
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
