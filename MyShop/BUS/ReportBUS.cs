using MyShop.DAO;
using MyShop.DTO;
using System.ComponentModel;

namespace MyShop.BUS
{
	public class ReportBUS
	{
		private List<ShopOrderDTO> _orders;
		private List<ProductDTO> _products;

		private ShopOrderDAO _orderDAO;
		private ProductDAO _productDAO;

		private int topN = 5;

		public ReportBUS()
		{
			_orderDAO = new ShopOrderDAO();
			_productDAO = new ProductDAO();
			_orders = getAllShopOrders();
			_products = getAllProducts();
		}

		private List<ShopOrderDTO> getAllShopOrders()
		{
			var orders = _orderDAO.getAll();
			return orders.ToList();
		}

		private List<ProductDTO> getAllProducts()
		{
			var products = _productDAO.getAll();
			return products.ToList();
		}

		public class ProductSales
		{
			public ProductDTO Product { get; set; }
			public int Quantity { get; set; }
		}

		// START: phương thức thống kê doanh thu, lợi nhuận
		public Tuple<List<double>, List<double>> groupRevenueAndProfitByYear()
		{
			List<double> renevues = new List<double>();
			List<double> profits = new List<double>();
			int currentYear = DateTime.Now.Year;

			for (int year = currentYear - 2; year <= currentYear; year++)
			{
				double totalRevenue = 0;
				double totalProfit = 0;
				foreach (var order in _orders)
				{
					if (order.CreateAt.Year == year)
					{
						totalRevenue += (double)order.FinalTotal!;
						totalProfit += (double)order.ProfitTotal!;
					}
				}
				renevues.Add(totalRevenue);
				profits.Add(totalProfit);
			}
			return new Tuple<List<double>, List<double>>(renevues, profits);
		}

		public Tuple<List<double>, List<double>> groupRevenueAndProfitByMonth(int year)
		{
			List<double> renevues = new List<double>();
			List<double> profits = new List<double>();

			for (int month = 1; month <= 12; month++)
			{
				double totalRevenue = 0;
				double totalProfit = 0;
				foreach (var order in _orders)
				{
					if (order.CreateAt.Month == month && order.CreateAt.Year == year)
					{
						totalRevenue += (double)order.FinalTotal!;
						totalProfit += (double)order.ProfitTotal!;
					}
				}
				renevues.Add(totalRevenue);
				profits.Add(totalProfit);
			}
			return new Tuple<List<double>, List<double>>(renevues, profits);
		}

		public Tuple<List<double>, List<double>> groupRevenueAndProfitByWeek(int month, int year)
		{
			List<double> renevues = new List<double>();
			List<double> profits = new List<double>();

			DateTime firstDayOfMonth = new DateTime(year, month, 1);
			DateTime lastDayOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));

			for (int week = 1; week <= 5; week++)
			{
				double totalRevenue = 0;
				double totalProfit = 0;

				DateTime startDate = firstDayOfMonth.AddDays((week - 1) * 7);
				DateTime endDate = startDate.AddDays(6);
				if (week == 5) endDate = lastDayOfMonth;

				foreach (var order in _orders)
				{
					if (order.CreateAt.Date >= startDate.Date && order.CreateAt.Date <= endDate.Date)
					{
						totalRevenue += (double)order.FinalTotal!;
						totalProfit += (double)order.ProfitTotal!;
					}
				}
				renevues.Add(totalRevenue);
				profits.Add(totalProfit);
			}

			return new Tuple<List<double>, List<double>>(renevues, profits);
		}

		public Tuple<List<double>, List<double>> groupRevenueAndProfitByDate(DateTime startDate, DateTime endDate)
		{
			List<double> renevues = new List<double>();
			List<double> profits = new List<double>();

			for (DateTime day = startDate.Date; day <= endDate.Date; day = day.AddDays(1))
			{
				double totalRevenue = 0;
				double totalProfit = 0;

				foreach (var order in _orders)
				{
					if (order.CreateAt.Date == day)
					{
						totalRevenue += (double)order.FinalTotal!;
						totalProfit += (double)order.ProfitTotal!;
					}
				}
				renevues.Add(totalRevenue);
				profits.Add(totalProfit);
			}

			return new Tuple<List<double>, List<double>>(renevues, profits);
		}
		// END: phương thức thống kê doanh thu, lợi nhuận

		// START: phương thức thống kê số lượng sản phẩm đã bán
		public List<int> groupQuantityOfProductByYear(ProductDTO product)
		{
			List<int> result = new List<int>();

			int currentYear = DateTime.Now.Year;

			for (int year = currentYear - 2; year <= currentYear; year++)
			{
				int quantity = 0;
				foreach (var order in _orders)
				{
					if (order.CreateAt.Year == year)
					{
						var purchases = _orderDAO.getPurchases(order.OrderID);
						foreach (var purchase in purchases)
						{
							if (purchase.ProID == product.ProId)
							{
								quantity += purchase.Quantity;
							}
						}
					}
				}
				result.Add(quantity);
			}
			return result;
		}

		public List<int> groupQuantityOfProductByMonth(ProductDTO product, int year)
		{
			List<int> result = new List<int>();

			for (int month = 1; month <= 12; month++)
			{
				int quantity = 0;
				foreach (var order in _orders)
				{
					if (order.CreateAt.Year == year && order.CreateAt.Month == month)
					{
						var purchases = _orderDAO.getPurchases(order.OrderID);
						foreach (var purchase in purchases)
						{
							if (purchase.ProID == product.ProId)
							{
								quantity += purchase.Quantity;
							}
						}
					}
				}
				result.Add(quantity);
			}
			return result;
		}

		public List<int> groupQuantityOfProductByWeek(ProductDTO product, int year, int month)
		{
			List<int> result = new List<int>();

			DateTime firstDayOfMonth = new DateTime(year, month, 1);
			DateTime lastDayOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));

			for (int week = 1; week <= 5; week++)
			{
				int quantity = 0;
				DateTime startDate = firstDayOfMonth.AddDays((week - 1) * 7);
				DateTime endDate = startDate.AddDays(6);
				if (week == 5) endDate = lastDayOfMonth;

				foreach (var order in _orders)
				{
					if (order.CreateAt.Date >= startDate.Date && order.CreateAt.Date <= endDate.Date)
					{
						var purchases = _orderDAO.getPurchases(order.OrderID);
						foreach (var purchase in purchases)
						{
							if (purchase.ProID == product.ProId)
							{
								quantity += purchase.Quantity;
							}
						}
					}
				}
				result.Add(quantity);
			}
			return result;
		}

		public List<int> groupQuantityOfProductByDate(ProductDTO product, DateTime startDate, DateTime endDate)
		{
			List<int> result = new List<int>();

			for (DateTime day = startDate.Date; day <= endDate.Date; day = day.AddDays(1))
			{
				int quantity = 0;
				foreach (var order in _orders)
				{
					if (order.CreateAt.Date == day)
					{
						var purchases = _orderDAO.getPurchases(order.OrderID);
						foreach (var purchase in purchases)
						{
							if (purchase.ProID == product.ProId)
							{
								quantity += purchase.Quantity;
							}
						}
					}
				}
				result.Add(quantity);
			}
			return result;
		}
		// END: phương thức thống kê số lượng sản phẩm đã bán

		// START: phương thức thống kê sản phẩm bán chạy
		public List<ProductSales> groupTopSalesByYear(int year)
		{
			List<ProductSales> result = new List<ProductSales>();

			foreach (var product in _products)
			{
				result.Add(new ProductSales { Product = product, Quantity = 0 });
			}


			foreach (var order in _orders)
			{
				if (order.CreateAt.Year == year)
				{
					var purchases = _orderDAO.getPurchases(order.OrderID);
					foreach (var purchase in purchases)
					{
						result.Find(item => item.Product.ProId == purchase.ProID)!.Quantity += purchase.Quantity;
					}
				}
			}
			return result.OrderByDescending(item => item.Quantity).Take(topN).ToList();
		}

		public List<ProductSales> groupTopSalesByMonth(int year, int month)
		{
			List<ProductSales> result = new List<ProductSales>();

			foreach (var product in _products)
			{
				result.Add(new ProductSales { Product = product, Quantity = 0 });
			}

			foreach (var order in _orders)
			{
				if (order.CreateAt.Year == year && order.CreateAt.Month == month)
				{
					var purchases = _orderDAO.getPurchases(order.OrderID);
					foreach (var purchase in purchases)
					{
						result.Find(item => item.Product.ProId == purchase.ProID)!.Quantity += purchase.Quantity;
					}
				}
			}
			return result.OrderByDescending(item => item.Quantity).Take(topN).ToList();
		}

		public List<ProductSales> groupTopSalesByCurrentWeek()
		{
			List<ProductSales> result = new List<ProductSales>();

			foreach (var product in _products)
			{
				result.Add(new ProductSales { Product = product, Quantity = 0 });
			}

			DateTime today = DateTime.Today;
			int daysUntilMonday = ((int)today.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;

			DateTime startOfWeek = today.AddDays(-daysUntilMonday);
			DateTime endOfWeek = startOfWeek.AddDays(6); // 6 ngày sau để đến Chủ nhật

			foreach (var order in _orders)
			{
				if (order.CreateAt.Date >= startOfWeek && order.CreateAt.Date <= endOfWeek)
				{
					var purchases = _orderDAO.getPurchases(order.OrderID);
					foreach (var purchase in purchases)
					{
						result.Find(item => item.Product.ProId == purchase.ProID)!.Quantity += purchase.Quantity;
					}
				}
			}
			return result.OrderByDescending(item => item.Quantity).Take(topN).ToList();
		}
		// END: phương thức thống kê sản phẩm bán chạy


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
