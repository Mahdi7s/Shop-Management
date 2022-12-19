using System;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace StoreManager.Presenters.Converters
{
    public sealed class NumberConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null) return value.ToString();
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.FilterNonNumeric(value as string);
        }

        #endregion

        private string FilterNonNumeric(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var result = from c in value where char.IsDigit(c) select c;
                return new string(result.ToArray());
            }
            return string.Empty;
        }
    }
}
