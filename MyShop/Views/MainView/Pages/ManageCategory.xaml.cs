using MyShop.BUS;
using MyShop.DTO;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyShop.Views.MainView.Pages
{
	/// <summary>
	/// Interaction logic for ManageCategory.xaml
	/// </summary>
	public partial class ManageCategory : Page
	{
		private CategoryBUS _categoryBUS;
		private Frame _pageNavigation;
		private ObservableCollection<CategoryDTO> _categories;

		public ManageCategory(Frame pageNavigation)
		{
			_pageNavigation = pageNavigation;
			InitializeComponent();
		}

		private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			int i = categoriesListView.SelectedIndex;

			var category = _categories[i];
			if (category != null)
			{
				_pageNavigation.NavigationService.Navigate(new UpdateCategory(_pageNavigation, category));
			}
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			_categoryBUS = new CategoryBUS();
			_categories = _categoryBUS.getAll();

			categoriesListView.ItemsSource = _categories;
		}

		private void SaveCategory_Click(object sender, RoutedEventArgs e)
		{
			var category = new CategoryDTO();

			if (NameTermTextBox.Text.Trim() == "" || DesTermTextBox.Text.Trim() == "")
			{
				MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
			} else
			{
				category.CatName = NameTermTextBox.Text;
				category.CatDescription = DesTermTextBox.Text;
				int id = _categoryBUS.addCategory(category);

				category.CatID = id;
				_categories.Add(category);
				NameTermTextBox.Text = "";
				DesTermTextBox.Text = "";
				MessageBox.Show("Thêm loại thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		private void DelCategory_Click(object sender, RoutedEventArgs e)
		{
			int i = categoriesListView.SelectedIndex;

			if (i == -1)
			{
				MessageBox.Show("Vui lòng chọn thể loại trước khi xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			else
			{
				MessageBoxResult choice = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông Báo", 
					MessageBoxButton.OKCancel, MessageBoxImage.Question);

				if (choice == MessageBoxResult.OK)
				{
					var CatID = _categories[i].CatID;
					bool isSuccess; string message;

					(isSuccess, message) = _categoryBUS.delCategoryById(CatID);
					if (!isSuccess)
					{
						MessageBox.Show(message, "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
					}
					else
					{
						_categories.RemoveAt(i);
					}
				}
			}
		}
	}
}
