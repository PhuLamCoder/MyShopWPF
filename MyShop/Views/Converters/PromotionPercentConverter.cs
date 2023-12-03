using System.Globalization;
using System.Windows.Data;

namespace MyShop.Views.Converters
{
	internal class PromotionPercentConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			int percent = (int)value;

			string result = $"-{percent} %";
			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
