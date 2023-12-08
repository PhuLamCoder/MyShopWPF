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
	}
}
