using System.Globalization;
using System.Windows.Data;

namespace FastHotKeyForWPF
{
    internal class DoubleConvertor : IValueConverter
    {
        private double _rate = 0.8;
        public double ConvertRate
        {
            get => _rate;
            set
            {
                if (value > 0)
                {
                    _rate = value;
                }
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double temp = double.IsNaN((double)value) ? 1 : (double)value * ConvertRate;
            double result = temp > 0 ? temp : 1;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double temp = double.IsNaN((double)value) ? 1 : (double)value / ConvertRate;
            double result = temp > 0 ? temp : 1;
            return result;
        }
    }
}
