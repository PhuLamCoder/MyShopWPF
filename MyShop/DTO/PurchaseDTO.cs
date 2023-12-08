using System.ComponentModel;

namespace MyShop.DTO
{
	public class PurchaseDTO : INotifyPropertyChanged
	{
		public int PurchaseID { get; set; }
		public int OrderID { get; set; }
		public int ProID { get; set; }
		public int Quantity { get; set; }

		public Decimal TotalPrice { get; set; }

		public event PropertyChangedEventHandler? PropertyChanged;
	}
}
