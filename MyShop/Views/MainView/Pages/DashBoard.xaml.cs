using MyShop.BUS;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyShop.Views.MainView.Pages
{
	class Resources
	{
		public string ProductTotalBG { get; set; }
		public string OrderTotalBG { get; set; }
		public int TotalProduct { get; set; }
		public int TotalOrder { get; set; }
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

			int totalOrder = _orderBUS.countTotalOrderbyLastWeek();

			var top5Product = await _productBUS.getTop5Product();

			this.DataContext = new Resources()
			{
				ProductTotalBG = "Assets/Images/item1-bg.jpg",
				OrderTotalBG = "Assets/Images/item2-bg.jpg",
				TotalProduct = totalProduct,
				TotalOrder = totalOrder
			};

			productsListView.ItemsSource = top5Product;
		}

		private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			// TODO
		}
	}
}
