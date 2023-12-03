using System.Globalization;
using System.Windows.Data;

namespace MyShop.Views.Converters
{
	public class DecimalToCurrencyConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			decimal currency = (decimal)value;

			string result = string.Format("{0:N0} đ", currency);
			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
