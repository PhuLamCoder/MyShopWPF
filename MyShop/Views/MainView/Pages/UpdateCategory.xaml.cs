using MyShop.BUS;
using MyShop.DTO;
using System.Windows;
using System.Windows.Controls;

namespace MyShop.Views.MainView.Pages
{
	/// <summary>
	/// Interaction logic for UpdateCategory.xaml
	/// </summary>
	public partial class UpdateCategory : Page
	{
		Frame _pageNavigation;
		CategoryDTO _category;
		CategoryBUS _categoryBUS;

		public UpdateCategory(Frame pageNavigation, CategoryDTO category)
		{
			_pageNavigation = pageNavigation;
			_category = category;
			InitializeComponent();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			DataContext = _category;
			_categoryBUS = new CategoryBUS();

		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			_pageNavigation.NavigationService.GoBack();
		}

		private void SaveCategory_Click(object sender, RoutedEventArgs e)
		{
			if (NameTermTextBox.Text.Trim() != "" && DesTermTextBox.Text.Trim() != "")
			{
				_category.CatName = NameTermTextBox.Text;
				_category.CatDescription = DesTermTextBox.Text;

				_categoryBUS.updateCategory(_category);

				MessageBox.Show("Chỉnh sửa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
				_pageNavigation.NavigationService.GoBack();
			}
			else
			{
				MessageBox.Show("Vui lòng không bỏ trống thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
		}
	}
}
