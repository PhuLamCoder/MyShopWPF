using System.ComponentModel;

namespace MyShop.DTO
{
	public class CustomerDTO : INotifyPropertyChanged
	{
		public int CusID { get; set; }
		public string CusName { get; set; }
		public string Gender { get; set; }
		public DateTime DOB { get; set; }
		public string Address { get; set; }
		public string Tel { get; set; }
		public int Block { get; set; }

		public event PropertyChangedEventHandler? PropertyChanged;
	}
}
