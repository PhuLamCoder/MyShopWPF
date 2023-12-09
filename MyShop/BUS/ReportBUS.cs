using MyShop.DAO;
using MyShop.DTO;

namespace MyShop.BUS
{
	public class ReportBUS
	{
		private List<ShopOrderDTO> _orders;

		private ShopOrderBUS _orderBUS;
		private ShopOrderDAO _orderDAO;

		public ReportBUS()
		{
			var orderDAO = new ShopOrderDAO();
			var orderBUS = new ShopOrderBUS();
			_orderBUS = orderBUS;
			_orderDAO = orderDAO;
			_orders = getAllShopOrders();
		}

		private List<ShopOrderDTO> getAllShopOrders()
		{
			var orders = _orderDAO.getAll();
			return orders.ToList();
		}

		// Các phương thức thống kê doanh thu, lợi nhuận
		public List<double> groupPriceTotalByYear()
		{
			List<double> result = new List<double>();
			int currentYear = DateTime.Now.Year;

			for (int year = currentYear - 2; year <= currentYear; year++)
			{
				double totalPrice = 0;
				foreach (var order in _orders)
				{
					if (order.CreateAt.Year == year)
					{
						totalPrice += (double)order.FinalTotal!;
					}
				}
				result.Add(totalPrice);
			}
			return result;
		}

		public List<double> groupProfitTotalByYear()
		{
			List<double> result = new List<double>();
			int currentYear = DateTime.Now.Year;

			for (int year = currentYear - 2; year <= currentYear; year++)
			{
				double totalProfit = 0;
				foreach (var order in _orders)
				{
					if (order.CreateAt.Year == year)
					{
						totalProfit += (double)order.ProfitTotal!;
					}
				}
				result.Add(totalProfit);
			}
			return result;
		}

		public List<double> groupPriceTotalByMonth(int year)
		{
			List<double> result = new List<double>();

			for (int month = 1; month <= 12; month++)
			{
				double totalPrice = 0;
				foreach (var order in _orders)
				{
					if (order.CreateAt.Month == month && order.CreateAt.Year == year)
					{
						totalPrice += (double)order.FinalTotal!;
					}
				}
				result.Add(totalPrice);
			}
			return result;
		}

		public List<double> groupProfitTotalByMonth(int year)
		{
			List<double> result = new List<double>();

			for (int month = 1; month <= 12; month++)
			{
				double totalProfit = 0;
				foreach (var order in _orders)
				{
					if (order.CreateAt.Month == month && order.CreateAt.Year == year)
					{
						totalProfit += (double)order.ProfitTotal!;
					}
				}
				result.Add(totalProfit);
			}
			return result;
		}

		public List<double> groupPriceTotalByWeek(int month, int year)
		{
			List<double> result = new List<double>();

			DateTime firstDayOfMonth = new DateTime(year, month, 1);
			DateTime lastDayOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));

			for (int week = 1; week <= 5; week++)
			{
				double totalPrice = 0;
				DateTime startDate = firstDayOfMonth.AddDays((week - 1) * 7);
				DateTime endDate = startDate.AddDays(6);
				if (week == 5) endDate = lastDayOfMonth;

				foreach (var order in _orders)
				{
					if (order.CreateAt.Date >= startDate.Date && order.CreateAt.Date <= endDate.Date)
					{
						totalPrice += (double)order.FinalTotal!;
					}
				}
				result.Add(totalPrice);
			}

			return result;
		}

		public List<double> groupProfitTotalByWeek(int month, int year)
		{
			List<double> result = new List<double>();

			DateTime firstDayOfMonth = new DateTime(year, month, 1);
			DateTime lastDayOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));

			for (int week = 1; week <= 5; week++)
			{
				double totalProfit = 0;
				DateTime startDate = firstDayOfMonth.AddDays((week - 1) * 7);
				DateTime endDate = startDate.AddDays(6);
				if (week == 5) endDate = lastDayOfMonth;

				foreach (var order in _orders)
				{
					if (order.CreateAt.Date >= startDate.Date && order.CreateAt.Date <= endDate.Date)
					{
						totalProfit += (double)order.ProfitTotal!;
					}
				}
				result.Add(totalProfit);
			}

			return result;
		}

		public List<double> groupPriceTotalByDate(DateTime startDate, DateTime endDate)
		{
			List<double> result = new List<double>();

			for (DateTime day = startDate.Date; day <= endDate.Date; day = day.AddDays(1))
			{
				double totalPrice = 0;
				foreach (var order in _orders)
				{
					if (order.CreateAt.Date == day)
					{
						totalPrice += (double)order.FinalTotal!;
					}
				}
				result.Add(totalPrice);
			}

			return result;
		}

		public List<double> groupProfitTotalByDate(DateTime startDate, DateTime endDate)
		{
			List<double> result = new List<double>();

			for (DateTime day = startDate.Date; day <= endDate.Date; day = day.AddDays(1))
			{
				double totalProfit = 0;
				foreach (var order in _orders)
				{
					if (order.CreateAt.Date == day)
					{
						totalProfit += (double)order.ProfitTotal!;
					}
				}
				result.Add(totalProfit);
			}

			return result;
		}
		// end công thức thống kê doanh thu, lợi nhuận


		public List<string> EachDayConverter(DateTime startDate, DateTime endDate)
		{
			List<string> result = new List<string>();

			for (DateTime day = startDate.Date; day <= endDate.Date; day = day.AddDays(1))
			{
				result.Add(day.ToString("MM/dd/yyyy"));
			}
			return result;
		}
	}
}
