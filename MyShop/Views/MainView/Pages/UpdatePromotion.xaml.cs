using MyShop.BUS;
using MyShop.DTO;
using System.Windows;
using System.Windows.Controls;

namespace MyShop.Views.MainView.Pages
{
	/// <summary>
	/// Interaction logic for UpdatePromotion.xaml
	/// </summary>
	public partial class UpdatePromotion : Page
	{
		Frame _pageNavigation;
		PromotionDTO _promotion;
		PromotionBUS _promotionBUS;
		public UpdatePromotion(Frame pageNavigation, PromotionDTO promotion)
		{
			_pageNavigation = pageNavigation;
			_promotion = promotion;
			InitializeComponent();
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			_pageNavigation.NavigationService.GoBack();
		}

		private void SavePromotion_Click(object sender, RoutedEventArgs e)
		{
			if (NameCodeTextBox.Text.Trim() != "")
			{
				try
				{
					_promotion.PromoCode = NameCodeTextBox.Text;
					_promotion.DiscountPercent = int.Parse(NameDiscountTextBox.Text);
					if (_promotion.DiscountPercent <= 0 || _promotion.DiscountPercent >= 100)
					{
						MessageBox.Show("Giá trị giảm giá phải lớn hơn 0 và nhỏ hơn 100!", "Thông báo",
							MessageBoxButton.OK, MessageBoxImage.Warning);
					}
					else
					{
						_promotionBUS.updatePromotion(_promotion);

						MessageBox.Show("Cập nhật mã giảm giá thành công!", "Thông báo", 
							MessageBoxButton.OK, MessageBoxImage.Information);
						_pageNavigation.NavigationService.GoBack();
					}
				}
				catch (Exception)
				{
					MessageBox.Show("Giá trị giảm giá phải là số nguyên!", "Thông báo",
						MessageBoxButton.OK, MessageBoxImage.Warning);
				}
			}
			else
			{
				MessageBox.Show("Vui lòng nhập tên mã!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			DataContext = _promotion;
			_promotionBUS = new PromotionBUS();
		}
	}
}
