using MyShop.BUS;
using MyShop.DTO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyShop.Views.MainView.Pages
{
	/// <summary>
	/// Interaction logic for OrderDetail.xaml
	/// </summary>
	public partial class OrderDetail : Page
	{
		public class Data : INotifyPropertyChanged
		{
			public ProductDTO Product { get; set; }
			public PurchaseDTO Purchase { get; set; }

			public event PropertyChangedEventHandler? PropertyChanged;
		}

		public class OrderInfo : INotifyPropertyChanged
		{
			public CustomerDTO Customer { get; set; }
			public ShopOrderDTO Order { get; set; }

			public event PropertyChangedEventHandler? PropertyChanged;
		}

		private ShopOrderDTO _order;
		private ObservableCollection<Data>? _dataList;
		private OrderInfo? _orderInfo;

		private Frame _pageNavigation;

		public OrderDetail(ShopOrderDTO order, Frame pageNavigation)
		{
			InitializeComponent();
			_pageNavigation = pageNavigation;
			_order = order;

			var productBUS = new ProductBUS();
			var orderBUS = new ShopOrderBUS();
			var customerBUS = new CustomerBUS();


			List<PurchaseDTO> _purchases = orderBUS.findPurchases(_order.OrderID);
			_dataList = new ObservableCollection<Data>();

			foreach (var purchase in _purchases)
			{
				var product = productBUS.findProductById(purchase.ProID);
				Data item = new Data();
				item.Product = product;
				item.Purchase = purchase;
				_dataList.Add(item);
			}
			productsListView.ItemsSource = _dataList;

			CustomerDTO _customer = customerBUS.findCustomerById(_order.CusID);
			_orderInfo = new OrderInfo();
			_orderInfo.Customer = _customer;
			_orderInfo.Order = _order;
			DataContext = _orderInfo;
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			_pageNavigation.NavigationService.GoBack();
		}

		private void DelOrder_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult choice = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông Báo", 
				MessageBoxButton.OKCancel, MessageBoxImage.Question);

			if (choice == MessageBoxResult.OK)
			{
				var orderBUS = new ShopOrderBUS();
				orderBUS.delOrderById(_order.OrderID);
				_pageNavigation.NavigationService.GoBack();
			}
		}

		private void UpdateOrder_Click(object sender, RoutedEventArgs e)
		{
			_pageNavigation.NavigationService.Navigate(new UpdateOrder(_pageNavigation, _orderInfo, _dataList));
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{

		}
	}
}
