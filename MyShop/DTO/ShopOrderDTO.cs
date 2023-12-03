using System.ComponentModel;

namespace MyShop.DTO
{
	public class ShopOrderDTO : INotifyPropertyChanged
	{
		public int OrderID { get; set; }
		public int CusID { get; set; }
		public DateTime CreateAt { get; set; }
		public Decimal? FinalTotal { get; set; }
		public Decimal? ProfitTotal { get; set; }

		public event PropertyChangedEventHandler? PropertyChanged;
	}
}
