using MyShop.BUS;
using MyShop.DTO;
using System.Windows;
using System.Windows.Controls;

namespace MyShop.Views.MainView.Pages
{
    /// <summary>
    /// Interaction logic for AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Page
    {
		private Frame _pageNavigation;

		public AddCustomer(Frame pageNavigation)
        {
            InitializeComponent();
			_pageNavigation = pageNavigation;
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{

        }

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			_pageNavigation.NavigationService.GoBack();
		}

		private void SaveCustomer_Click(object sender, RoutedEventArgs e)
		{
			if (NameTermTextBox.Text.Trim() == "" || TelTermTextBox.Text.Trim() == "" || 
				DobPicker.SelectedDate == null || AddressTermTextBox.Text == "")
			{
				MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			var customerDTO = new CustomerDTO();

			customerDTO.CusName = NameTermTextBox.Text;
			customerDTO.Tel = TelTermTextBox.Text;
			customerDTO.DOB = (DateTime)DobPicker.SelectedDate;
			customerDTO.Address = AddressTermTextBox.Text;
			customerDTO.Block = 0;
			if (GenderCombobox.SelectedIndex == 0 )
			{
				customerDTO.Gender = "Nam";
			} 
			else if (GenderCombobox.SelectedIndex == 1 )
			{
				customerDTO.Gender = "Nữ";
			} 
			else
			{
				customerDTO.Gender = "Khác";
			}

			CustomerBUS customerBUS = new CustomerBUS();
			int id = customerBUS.saveCustomer(customerDTO);

			MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
			_pageNavigation.NavigationService.GoBack();
		}
	}
}
