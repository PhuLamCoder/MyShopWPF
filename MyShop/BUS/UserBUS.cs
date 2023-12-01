using MyShop.DAO;
using MyShop.DTO;

namespace MyShop.BUS
{
	public class UserBUS
	{
		private UserDAO _userDAO;

		public UserBUS()
		{
			_userDAO = new UserDAO();
		}

		public UserDTO? getOne(string username, string password)
		{
			return _userDAO.GetOne(username, password);
		}

		public bool createUser(UserDTO user)
		{
			return _userDAO.CreateUser(user);
		}
	}
}
