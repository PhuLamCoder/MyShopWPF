using System.ComponentModel;

namespace MyShop.DTO
{
	public class ShopOrderDTO : INotifyPropertyChanged
	{
		public int OrderID { get; set; }
		public int CusID { get; set; }
		public DateTime CreateAt { get; set; }
		// Final total này bao gồm cả lợi nhuận
		public Decimal? FinalTotal { get; set; }
		// Lợi nhuận được tính theo phần trăm của tổng hóa đơn
		public Decimal? ProfitTotal { get; set; }

		public event PropertyChangedEventHandler? PropertyChanged;
	}
}
