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
	/// Interaction logic for UpdateProduct.xaml
	/// </summary>
	public partial class UpdateProduct : Page
	{
		private bool _isImageChanged = false;
		private FileInfo? _selectedImage = null;
		private Frame _pageNavegation;

		private CategoryBUS _categoryBUS;
		private ProductBUS _productBUS;
		private PromotionBUS _promotionBUS;

		private ProductDTO _productDTO;
		private CategoryDTO _categoryDTO;

		public UpdateProduct(ProductDTO product, CategoryDTO categoryDTO, Frame pageNavigation)
		{
			InitializeComponent();

			// categoryDTO parameter của hàm này đang giữ vùng nhớ của page Detail
			_categoryDTO = categoryDTO;
			// dòng trên để lưu lại vùng nhớ

			// trời ơi chúa tể vùng nhớ đây rùi hahaa :)))
			_pageNavegation = pageNavigation;
			_categoryBUS = new CategoryBUS();
			_productBUS = new ProductBUS();
			_promotionBUS = new PromotionBUS();

			var categories = _categoryBUS.getAll();

			CategoryCombobox.ItemsSource = categories;

			foreach (var category in categories)
			{
				if (category.CatID == categoryDTO.CatID)
				{
					categoryDTO = category;
					break;
				}
			}

			CategoryCombobox.SelectedValue = categoryDTO;
			_productDTO = product;

			DataContext = product;
		}

		private void AddImageButton_Click(object sender, RoutedEventArgs e)
		{
			var screen = new OpenFileDialog();
			screen.Filter = "Files|*.png; *.jpg; *.jpeg;";
			if (screen.ShowDialog() == true)
			{
				_isImageChanged = true;
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
			var categoryDTO = (CategoryDTO)CategoryCombobox.SelectedValue;

			// cập nhật giá khuyến mãi
			int discountPercent = 0;
			if (_productDTO.PromoID != null)
			{
				discountPercent = _promotionBUS.getPromotionById((int)_productDTO.PromoID).DiscountPercent;
			}
			double percent = 1 - discountPercent * 1.0 / 100;

			try
			{
				_productDTO.ProName = NameTermTextBox.Text;
				_productDTO.Ram = Double.Parse(RamTermTextBox.Text);
				_productDTO.Rom = int.Parse(RomTermTextBox.Text);
				_productDTO.ScreenSize = Double.Parse(ScreenSizeTermTextBox.Text);
				_productDTO.Description = DesTermTextBox.Text;
				_productDTO.Price = Decimal.Parse(PriceTermTextBox.Text);
				_productDTO.PromotionPrice = (decimal)((double)_productDTO.Price * percent);
				_productDTO.Trademark = TradeMarkTermTextBox.Text;
				_productDTO.BatteryCapacity = int.Parse(PinTermTextBox.Text);
				_productDTO.CatID = categoryDTO.CatID;

				// ĐỂ CẬP NHẬT GIAO DIỆN CHỖ CATEGORY KHI QUAY VỀ DETAIL
				var categoryTemp = _categoryBUS.getCategoryById(_productDTO.CatID);
				_categoryDTO.CatID = categoryTemp.CatID;
				_categoryDTO.CatName = categoryTemp.CatName;

				_productDTO.Quantity = int.Parse(QuantityTermTextBox.Text);
				_productDTO.Block = 0;

				_productBUS.updateProduct(_productDTO);

				string key = Guid.NewGuid().ToString();

				if (_isImageChanged)
					_productDTO.ImagePath = _productBUS.uploadImage(_selectedImage, _productDTO.ProId, key);

				MessageBox.Show("Sản phẩm đã chỉnh sửa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
				_pageNavegation.NavigationService.GoBack();
			} catch
			{
				MessageBox.Show("Định dạng dữ liệu không hợp lệ!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			_pageNavegation.NavigationService.GoBack();
		}
	}
}
