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

		public ObservableCollection<CategoryDTO> getAll()
		{
			return _categoryDAO.getAll();
		}

		public int addCategory(CategoryDTO category)
		{
			return _categoryDAO.insertCategory(category);
		}

		public Tuple<Boolean, string> delCategoryById(int catID)
		{
			return _categoryDAO.delCategoryById(catID);
		}

		public void updateCategory(CategoryDTO category)
		{
			_categoryDAO.updateCategory(category);
		}
	}
}
