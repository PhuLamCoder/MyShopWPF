using MyShop.BUS;
using MyShop.DTO;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyShop.Views.MainView.Pages
{
	/// <summary>
	/// Interaction logic for ManageCustomer.xaml
	/// </summary>
	public partial class ManageCustomer : Page
	{
		Frame _pageNavigation;
		private CustomerBUS _customerBUS;

		private ObservableCollection<CustomerDTO>? _customers = null;

		private int _currentPage = 1;
		private int _rowsPerPage = 8;
		private int _totalItems = 0;
		private int _totalPages = 0;
		private string _currentKey = "";

		public ManageCustomer(Frame pageNavigation)
		{
			_pageNavigation = pageNavigation;
			_customerBUS = new CustomerBUS();
			InitializeComponent();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			updateDataSource();
		}

		private void updateDataSource()
		{
			(_customers, _totalItems) = _customerBUS.findCustomerBySearch(_currentPage, _rowsPerPage, _currentKey);
			customersListView.ItemsSource = _customers;

			infoTextBlock.Text = $"Đang hiển thị {_customers.Count} trên tổng số {_totalItems} khách hàng";
			updatePagingInfo();
		}

		private void updatePagingInfo()
		{
			_totalPages = _totalItems / _rowsPerPage + (_totalItems % _rowsPerPage == 0 ? 0 : 1);
			if (_totalPages == 0) _totalPages = 1;

			pageInfoTextBlock.Text = $"{_currentPage}/{_totalPages}";
		}


		private void AddCustomer_Click(object sender, RoutedEventArgs e)
		{
			// Chưa thêm cái pagination vào
			_pageNavigation.NavigationService.Navigate(new AddCustomer(_pageNavigation));
		}

		private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			int i = customersListView.SelectedIndex;
			var customer = _customers![i];

			if (customer != null)
			{
				//_pageNavigation.NavigationService.Navigate(new OrderDetail(order, _pageNavigation));
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

		private void SearchTermTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				_currentKey = SearchTermTextBox.Text.Trim();
				_currentPage = 1;
				updateDataSource();
			}
		}
	}
}
