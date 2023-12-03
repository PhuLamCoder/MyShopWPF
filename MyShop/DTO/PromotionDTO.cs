using System.ComponentModel;

namespace MyShop.DTO
{
	public class PromotionDTO : INotifyPropertyChanged
	{
		public int? IdPromo { get; set; }
		public string? PromoCode { get; set; }
		public int DiscountPercent { get; set; }


		public event PropertyChangedEventHandler? PropertyChanged;

		public void copy(PromotionDTO other)
		{
			this.PromoCode = other.PromoCode;
			this.IdPromo = other.IdPromo;
			this.DiscountPercent = other.DiscountPercent;
		}
	}
}
