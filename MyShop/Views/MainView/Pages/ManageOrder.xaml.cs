using MyShop.BUS;
using MyShop.DTO;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyShop.Views.MainView.Pages
{
    /// <summary>
    /// Interaction logic for ManageOrder.xaml
    /// </summary>
    public partial class ManageOrder : Page
    {
		Frame _pageNavigation;
		private ShopOrderBUS _orderBUS;

		private List<ShopOrderDTO>? _orders = null;
		private ObservableCollection<Data>? _list = null;

		private int _currentPage = 1;
		private int _rowsPerPage = 8;
		private int _totalItems = 0;
		private int _totalPages = 0;
		private DateTime? _currentStartDate = null;
		private DateTime? _currentEndDate = null;

		class OrderDetailResources
		{
			public string FirstIcon { get; set; }
			public string LastIcon { get; set; }
			public string NextIcon { get; set; }
			public string PrevIcon { get; set; }
		}

		public class Data
		{
			public int OrderID { get; set; }
			public DateTime CreateAt { get; set; }
			public string CusName { get; set; }
			public string FinalTotal { get; set; }

			public Data(ShopOrderDTO order)
			{
				var customerBUS = new CustomerBUS();
				OrderID = order.OrderID;
				CreateAt = order.CreateAt.Date;
				CusName = customerBUS.findCustomerById(order.CusID).CusName;
				FinalTotal = string.Format("{0:N0} đ", order.FinalTotal);
			}
		}

		public ManageOrder(Frame pageNavigation)
		{
			_pageNavigation = pageNavigation;
			_orderBUS = new ShopOrderBUS();
			InitializeComponent();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			updateDataSource();
			this.DataContext = new OrderDetailResources()
			{
				FirstIcon = "Assets/Images/ic-first.png",
				LastIcon = "Assets/Images/ic-last.png",
				NextIcon = "Assets/Images/ic-next.png",
				PrevIcon = "Assets/Images/ic-prev.png"
			};
		}

		private void updateDataSource()
		{
			_list = new ObservableCollection<Data>();
			(_orders, _totalItems) = _orderBUS.findOrderBySearch(_currentPage, 
				_rowsPerPage, _currentStartDate, _currentEndDate);

			foreach (var order in _orders)
			{
				_list.Add(new Data(order));
			}
			ordersListView.ItemsSource = _list;

			infoTextBlock.Text = $"Đang hiển thị {_orders.Count} trên tổng số {_totalItems} hóa đơn";
			updatePagingInfo();
		}

		private void updatePagingInfo()
		{
			_totalPages = _totalItems / _rowsPerPage + (_totalItems % _rowsPerPage == 0 ? 0 : 1);
			if (_totalPages == 0) _totalPages = 1;

			pageInfoTextBlock.Text = $"{_currentPage}/{_totalPages}";
		}


		private void AddOrder_Click(object sender, RoutedEventArgs e)
		{
			_pageNavigation.NavigationService.Navigate(new AddOrder(_pageNavigation, _list));
		}

		private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			int i = ordersListView.SelectedIndex;
			var order = _orders[i];

			if (order != null)
			{
				//_pageNavigation.NavigationService.Navigate(new SuperOrderDetail(order, _pageNavigation));
			}
		}

		private void FirstButton_Click(object sender, RoutedEventArgs e)
		{
			_currentPage = 1;
			updateDataSource();
		}

		private void PrevButton_Click(object sender, RoutedEventArgs e)
		{
			if (_currentPage > 1)
			{
				_currentPage--;
				updateDataSource();
			}
		}

		private void NextButton_Click(object sender, RoutedEventArgs e)
		{
			if (_currentPage < _totalPages)
			{
				_currentPage++;
				updateDataSource();
			}
		}

		private void LastButton_Click(object sender, RoutedEventArgs e)
		{
			_currentPage = _totalPages;
			updateDataSource();
		}

		private void Search_Click(object sender, RoutedEventArgs e)
		{
			_currentPage = 1;
			_currentStartDate = StartDate.SelectedDate;
			_currentEndDate = EndDate.SelectedDate;
			updateDataSource();
			updatePagingInfo();
		}
	}
}
