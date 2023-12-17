using MyShop.BUS;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyShop.Views.MainView.Pages
{
    /// <summary>
    /// Interaction logic for UpdateCustomer.xaml
    /// </summary>
    public partial class UpdateCustomer : Page
    {
		private Frame _pageNavigation;
		private CustomerDTO _customer;
		public UpdateCustomer(CustomerDTO customer, Frame pagePagination)
        {
            InitializeComponent();
			_pageNavigation = pagePagination;
			_customer = customer;
			DataContext = _customer;
			if (_customer.Gender.Trim() == "Nam")
			{
				GenderCombobox.SelectedIndex = 0;
			}
			else if (_customer.Gender.Trim() == "Nữ")
			{
				GenderCombobox.SelectedIndex = 1;
			}
			else
			{
				GenderCombobox.SelectedIndex = 2;
			}
        }

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{

		}

		private void SaveCustomer_Click(object sender, RoutedEventArgs e)
		{
			if (NameTermTextBox.Text.Trim() == "" || TelTermTextBox.Text.Trim() == "" ||
				DobPicker.SelectedDate == null || AddressTermTextBox.Text.Trim() == "")
			{
				MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			if (GenderCombobox.SelectedIndex == 0)
			{
				_customer.Gender = "Nam";
			}
			else if (GenderCombobox.SelectedIndex == 1)
			{
				_customer.Gender = "Nữ";
			}
			else
			{
				_customer.Gender = "Khác";
			}

			CustomerBUS customerBUS = new CustomerBUS();
			customerBUS.updateCustomer(_customer);

			MessageBox.Show("Chỉnh sửa khách hàng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
			_pageNavigation.NavigationService.GoBack();
		}

		private void DelCustomer_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult choice = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông Báo",
				MessageBoxButton.OKCancel, MessageBoxImage.Question);

			if (choice == MessageBoxResult.OK)
			{
				var customerBUS = new CustomerBUS();
				customerBUS.delCustomerById(_customer.CusID);
				_pageNavigation.NavigationService.GoBack();
			}
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			_pageNavigation.NavigationService.GoBack();
		}
	}
}
