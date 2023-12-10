using MyShop.BUS;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyShop.Views.MainView.Pages
{
	class Resources
	{
		public int TotalProduct { get; set; }
		public int TotalOrderByWeek { get; set; }
		public int TotalOrderByMonth { get; set; }
	}

	/// <summary>
	/// Interaction logic for DashBoard.xaml
	/// </summary>
	public partial class DashBoard : Page
	{
		ProductBUS _productBUS;
		ShopOrderBUS _orderBUS;

		public DashBoard()
		{
			_productBUS = new ProductBUS();
			_orderBUS = new ShopOrderBUS();
			InitializeComponent();
		}

		private async void Page_Loaded(object sender, RoutedEventArgs e)
		{
			int totalProduct = await _productBUS.countTotalProduct();

			int totalOrderByWeek = _orderBUS.countTotalOrderbyLastWeek();
			int totalOrderByMonth = _orderBUS.countTotalOrderbyLastMonth();
			var top5Product = await _productBUS.getTop5Product();

			this.DataContext = new Resources()
			{
				TotalProduct = totalProduct,
				TotalOrderByWeek = totalOrderByWeek,
				TotalOrderByMonth = totalOrderByMonth,
			};

			productsListView.ItemsSource = top5Product;
		}

		private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			// TODO
		}
	}
}
