using System.Globalization;
using System.Windows.Data;

namespace MyShop.Views.Converters
{
	public class RelativeToAbsoluteConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string relative = (string)value;
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			string absolute = $"{baseDirectory}{relative}";
			return absolute;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
