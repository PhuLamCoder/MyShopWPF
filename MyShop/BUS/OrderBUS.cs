using MyShop.DAO;

namespace MyShop.BUS
{
	public class OrderBUS
	{
		private OrderDAO _orderDAO;

		public OrderBUS()
		{
			_orderDAO = new OrderDAO();
		}

		public async Task<int> countTotalOrderbyLastWeek()
		{
			var orders = await _orderDAO.getAll();

			DateTime today = DateTime.Today;
			DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek);
			DateTime endOfWeek = startOfWeek.AddDays(6);

			var ordersByLastWeek = orders.Where(
				order => order.CreateAt >= startOfWeek.Date && order.CreateAt <= endOfWeek.Date)
				.ToList();

			return ordersByLastWeek.Count;
		}
	}
}
