using MyShop.BUS;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyShop.Views.MainView.Pages
{
	/// <summary>
	/// Interaction logic for TopSalesReport.xaml
	/// </summary>
	public partial class TopSalesReport : Page
	{
		class Resources
		{
			public int TotalProduct { get; set; }
			public int TotalOrderByWeek { get; set; }
			public int TotalOrderByMonth { get; set; }
		}

		Frame _pageNavigation;
		ProductBUS _productBUS;
		ShopOrderBUS _orderBUS;
		ReportBUS _reportBUS;

		public TopSalesReport(Frame pageNavigation)
		{
			_productBUS = new ProductBUS();
			_orderBUS = new ShopOrderBUS();
			_reportBUS = new ReportBUS();

			InitializeComponent();
			_pageNavigation = pageNavigation;
		}

		private async void Page_Loaded(object sender, RoutedEventArgs e)
		{
			int totalProduct = await _productBUS.countTotalProduct();
			int totalOrderByWeek = _orderBUS.countTotalOrderbyLastWeek();
			int totalOrderByMonth = _orderBUS.countTotalOrderbyLastMonth();
			var top5Product = _reportBUS.groupTopSalesByYear(DateTime.Now.Year);

			DataContext = new Resources()
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
			MessageBox.Show(((ReportBUS.ProductSales)productsListView.SelectedItem).Quantity.ToString());
		}

		private void GoBack_Click(object sender, RoutedEventArgs e)
		{
			_pageNavigation.NavigationService.GoBack();
		}

		private void TimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (productsListView != null)
			{
				if (TimeComboBox.SelectedIndex == 0)
				{
					productsListView.ItemsSource = _reportBUS.groupTopSalesByYear(DateTime.Now.Year);
				}
				else if (TimeComboBox.SelectedIndex == 1)
				{
					productsListView.ItemsSource = _reportBUS.groupTopSalesByMonth(DateTime.Now.Year, DateTime.Now.Month);
				}
				else
				{
					productsListView.ItemsSource = _reportBUS.groupTopSalesByCurrentWeek();
				}
			}
		}
	}
}
