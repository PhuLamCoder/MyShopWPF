using Microsoft.Win32;
using MyShop.BUS;
using MyShop.DTO;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
namespace MyShop.Views.MainView.Pages
{
	/// <summary>
	/// Interaction logic for AddProduct.xaml
	/// </summary>
	public partial class AddProduct : Page
	{
		private FileInfo? _selectedImage = null;
		private ProductBUS _productBUS;
		private CategoryBUS _categoryBUS;
		private Frame _pageNavigation;

		public AddProduct(Frame pageNavigation)
		{
			InitializeComponent();

			_productBUS = new ProductBUS();
			_categoryBUS = new CategoryBUS();

			var categories = _categoryBUS.getAll();


			CategoryCombobox.ItemsSource = categories;
			CategoryCombobox.SelectedIndex = 0;

			_pageNavigation = pageNavigation;
		}

		private void AddImageButton_Click(object sender, RoutedEventArgs e)
		{
			var screen = new OpenFileDialog();
			screen.Filter = "Files|*.png; *.jpg; *.jpeg;";
			if (screen.ShowDialog() == true)
			{
				_selectedImage = new FileInfo(screen.FileName);

				var bitmap = new BitmapImage();
				bitmap.BeginInit();
				bitmap.UriSource = new Uri(screen.FileName, UriKind.Absolute);
				bitmap.EndInit();

				AddedImage.Source = bitmap;
			}
		}

		private void SaveProduct_Click(object sender, RoutedEventArgs e)
		{
			if (_selectedImage == null)
			{
				MessageBox.Show("Vui lòng Chọn ảnh sản phẩm!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			var categoryDTO = (CategoryDTO)CategoryCombobox.SelectedValue;

			var productDTO = new ProductDTO();

			productDTO.ProName = NameTermTextBox.Text;
			productDTO.Ram = Double.Parse(RamTermTextBox.Text);
			productDTO.Rom = int.Parse(RomTermTextBox.Text);
			productDTO.ScreenSize = Double.Parse(ScreenSizeTermTextBox.Text);
			productDTO.Description = DesTermTextBox.Text;
			productDTO.Price = Decimal.Parse(PriceTermTextBox.Text);
			productDTO.Trademark = TradeMarkTermTextBox.Text;
			productDTO.BatteryCapacity = int.Parse(PinTermTextBox.Text);
			productDTO.CatID = categoryDTO.CatID;
			productDTO.Quantity = int.Parse(QuantityTermTextBox.Text);
			productDTO.Block = 0;

			int id = _productBUS.saveProduct(productDTO);
			string key = Guid.NewGuid().ToString();
			_productBUS.uploadImage(_selectedImage, id, key);

			MessageBox.Show("Sản phẩm đã thêm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
			_pageNavigation.NavigationService.GoBack();
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			_pageNavigation.NavigationService.GoBack();
		}
	}
}
