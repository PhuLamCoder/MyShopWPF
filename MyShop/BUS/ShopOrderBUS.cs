using MyShop.DAO;
using MyShop.DTO;

namespace MyShop.BUS
{
	public class ShopOrderBUS
	{
		private ShopOrderDAO _orderDAO;

		public ShopOrderBUS()
		{
			_orderDAO = new ShopOrderDAO();
		}

		public int countTotalOrderbyLastWeek()
		{
			var orders = _orderDAO.getAll();

			DateTime today = DateTime.Today;
			DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek);
			DateTime endOfWeek = startOfWeek.AddDays(6);

			var ordersByLastWeek = orders.Where(
				order => order.CreateAt >= startOfWeek.Date && order.CreateAt <= endOfWeek.Date)
				.ToList();

			return ordersByLastWeek.Count;
		}

		public Tuple<List<ShopOrderDTO>, int> findOrderBySearch(int currentPage = 1, int rowsPerPage = 6,
			 DateTime? startDate = null, DateTime? endDate = null)
		{
			var origin = _orderDAO.getAll().ToList();

			var list = origin.Where((item) => {
				bool check = true;
				if (startDate != null && endDate != null)
				{
					check = item.CreateAt >= startDate && item.CreateAt <= endDate;
				}
				else if (startDate != null && endDate == null)
				{
					check = item.CreateAt >= startDate;
				}
				else if (startDate == null && endDate != null)
				{
					check = item.CreateAt <= endDate;
				}

				return check;
			});

			var items = list.Skip((currentPage - 1) * rowsPerPage).Take(rowsPerPage);
			var result = new Tuple<List<ShopOrderDTO>, int>(items.ToList(), list.Count());

			return result;
		}

		public int addShopOrder(ShopOrderDTO shopOrderDTO)
		{
			return _orderDAO.insertShopOrder(shopOrderDTO);
		}

		public decimal calOrderProfit(decimal finalTotal)
		{
			float profit = 0.07f;
			decimal result = finalTotal * (decimal)profit;
			return result;
		}

		public void addPurchase(PurchaseDTO purchaseDTO)
		{
			_orderDAO.insertPurchase(purchaseDTO);
		}
	}
}
