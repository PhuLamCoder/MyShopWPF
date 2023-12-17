using MyShop.DAO;
using MyShop.DTO;
using System.Collections.ObjectModel;

namespace MyShop.BUS
{
    class CustomerBUS
    {
		private CustomerDAO _customerDAO;

		public CustomerBUS()
		{
			_customerDAO = new CustomerDAO();
		}

		public ObservableCollection<CustomerDTO> getAll(string? sortBy = null) 
		{
			var result = _customerDAO.getAll();
			if (sortBy != null)
			{
				if (sortBy.ToLower() == "name")
				{
					result = new ObservableCollection<CustomerDTO>(result.OrderBy(cus => cus.CusName).ToList());
				}
			}
			return result;
		}

		public CustomerDTO findCustomerById(int cusID)
		{
			return _customerDAO.getCustomerById(cusID);
		}

		public Tuple<ObservableCollection<CustomerDTO>, int> findCustomerBySearch(int currentPage = 1, int rowsPerPage = 8,
			 string? keyword = null)
		{
			var origin = _customerDAO.getAll();

			var list = origin.Where((item) => {
				bool check = true;
				if (keyword != null)
				{
					check = item.CusName.ToLower().Contains(keyword.ToLower()) || item.Tel.ToLower().Contains(keyword.ToLower());
				}
				return check;
			});

			var items = list.Skip((currentPage - 1) * rowsPerPage).Take(rowsPerPage);
			var result = new Tuple<ObservableCollection<CustomerDTO>, int>(new ObservableCollection<CustomerDTO>(items), list.Count());

			return result;
		}

		public int saveCustomer(CustomerDTO customer)
		{
			int id = _customerDAO.insertCustomer(customer);
			return id;
		}

		public void delCustomerById(int id)
		{
			_customerDAO.deleteCustomerById(id);
		}

		public void updateCustomer(CustomerDTO customer)
		{
			_customerDAO.updateCustomer(customer);
		}
	}
}
