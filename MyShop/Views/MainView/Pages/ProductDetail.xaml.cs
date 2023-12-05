using MyShop.BUS;
using MyShop.DTO;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace MyShop.Views.MainView.Pages
{
	/// <summary>
	/// Interaction logic for ProductDetail.xaml
	/// </summary>
	public partial class ProductDetail : Page
	{
		private ProductDTO _product;
		private CategoryDTO _category;

		private Frame _pageNavigation;
		private ProductBUS _productBUS;
		private CategoryBUS _categoryBUS;
		private PromotionBUS _promotionBUS;
		protected PromotionDTO? _promotion;
		private Home _home;
		private Data _currentData;


		// Mục đích là đổ dữ liệu của Class này lên UI
		class Data : INotifyPropertyChanged
		{
			public ProductDTO Product { get; set; }
			public CategoryDTO Category { get; set; }
			public PromotionDTO Promotion { get; set; }

			public string CatIcon { get; set; }

			public Data(ProductDTO productDTO, CategoryDTO categoryDTO, PromotionDTO? promotion)
			{
				this.Product = productDTO;
				this.Category = categoryDTO;
				if (promotion != null)
				{
					this.Promotion = promotion;
				}
				else
				{
					this.Promotion = new();
					this.Promotion.DiscountPercent = 0;
				}
			}

			public event PropertyChangedEventHandler? PropertyChanged;
		}

		public ProductDetail(Home home, ProductDTO product, Frame pageNavigation)
		{
			InitializeComponent();
			_home = home;
			_product = product;
			_pageNavigation = pageNavigation;
			_productBUS = new ProductBUS();
			_categoryBUS = new CategoryBUS();
			_promotionBUS = new PromotionBUS();

			CategoryDTO category = _categoryBUS.getCategoryById(_product.CatID);
			PromotionDTO? promotion = null;

			var promotions = _promotionBUS.getAll();
			promotions.Insert(0, new PromotionDTO()
			{
				PromoCode = "Chưa áp dụng"
			});

			PromotionsCombobox.ItemsSource = promotions;

			// Nếu PromoID == null; nghĩa là sản phẩm chưa được áp dụng khuyến mãi
			if (_product.PromoID != null)
			{
				promotion = _promotionBUS.getPromotionById((int)_product.PromoID);
				_promotion = promotion;
				PromotionsCombobox.SelectedValue = (PromotionDTO)promotions.Where(item => item.IdPromo == _product.PromoID).ToList()[0];
			}
			else // Chưa áp dụng khuyến mãi
			{
				PromotionsCombobox.SelectedIndex = 0;
				_promotion = null;
			}

			_category = category;
			Data data = new Data(_product, category, _promotion);
			_currentData = data;
			DataContext = data;
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			// Lưu lại trạng thái trước đó
			_pageNavigation.NavigationService.GoBack();
		}

		private void DelProduct_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult choice = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông Báo", 
				MessageBoxButton.OKCancel, MessageBoxImage.Question);

			if (choice == MessageBoxResult.OK)
			{
				// lưu lại trạng thái trước đó
				_productBUS.delProduct(_product.ProId);

				_pageNavigation.NavigationService.GoBack();
			}
		}

		private void UpdateButton_Click(object sender, RoutedEventArgs e)
		{
			var clonedProduct = (ProductDTO)_product.Clone();
			_pageNavigation.NavigationService.Navigate(new UpdateProduct(_product, _category, _pageNavigation));
		}

		int flag = 0; // bỏ qua lần đầu
		private void PromotionsCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var promotion = (PromotionDTO)PromotionsCombobox.SelectedValue;

			if (promotion != null && flag != 0)
			{
				_currentData.Promotion.copy(promotion);
				_product.PromoID = _currentData.Promotion.IdPromo;
				double percent = 1 - promotion.DiscountPercent * 1.0 / 100;
				_product.PromotionPrice = (decimal?)((double)_product.Price * percent);
				_productBUS.updateProduct(_product);
				_promotion = promotion;
				MessageBox.Show("Đã áp dụng mã khuyến mãi thành công!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			flag = 1;
		}
	}
}
