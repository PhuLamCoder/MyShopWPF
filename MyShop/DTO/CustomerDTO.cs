using System.ComponentModel;

namespace MyShop.DTO
{
	public class CustomerDTO : INotifyPropertyChanged
	{
		public int CusID { get; set; }
		public string CusName { get; set; }

		public event PropertyChangedEventHandler? PropertyChanged;
	}
}
