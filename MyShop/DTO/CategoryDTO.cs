using System.ComponentModel;

namespace MyShop.DTO
{
	public class CategoryDTO : INotifyPropertyChanged
	{
		public int CatID { get; set; }
		public string CatName { get; set; }
		public string CatDescription { get; set; }

		public event PropertyChangedEventHandler? PropertyChanged;
	}
}
