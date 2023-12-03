using MyShop.DAO;
using MyShop.DTO;
using System.Collections.ObjectModel;

namespace MyShop.BUS
{
	public class CategoryBUS
	{
		private CategoryDAO _categoryDAO;

		public CategoryBUS()
		{
			_categoryDAO = new CategoryDAO();
		}

		public CategoryDTO getCategoryById(int id)
		{
			return _categoryDAO.getCategoryById(id);
		}
	}
}
