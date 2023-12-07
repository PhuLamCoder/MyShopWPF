using System.ComponentModel;

namespace MyShop.DTO
{
	public class ProductDTO : INotifyPropertyChanged, ICloneable
	{
		public ProductDTO()
		{
			Block = 0;
		}

		public int ProId { get; set; }
		public string? ProName { get; set; }
		public double Ram { get; set; }
		public int Rom { get; set; }
		public double ScreenSize { get; set; }
		public string? Description { get; set; }
		public decimal Price { get; set; }
		public decimal? PromotionPrice { get; set; }
		public string? ImagePath { get; set; }
		public string? Trademark { get; set; }
		public int BatteryCapacity { get; set; }
		public int CatID { get; set; }
		public int Quantity { get; set; }
		public int? PromoID { get; set; }
		public int? Block { get; set; }

		public event PropertyChangedEventHandler? PropertyChanged;

		public object Clone()
		{
			return MemberwiseClone();
		}

		public void copy(ProductDTO other)
		{
			this.ProId = other.ProId;
			this.ProName = other.ProName;
			this.Ram = other.Ram;
			this.Rom = other.Rom;
			this.ScreenSize = other.ScreenSize;
			this.Description = other.Description;
			this.Price = other.Price;
			this.PromotionPrice = other.PromotionPrice;
			this.ImagePath = other.ImagePath;
			this.Trademark = other.Trademark;
			this.BatteryCapacity = other.BatteryCapacity;
			this.CatID = other.CatID;
			this.Quantity = other.Quantity;
			this.PromoID = other.PromoID;
			this.Block = other.Block;
		}
	}
}
