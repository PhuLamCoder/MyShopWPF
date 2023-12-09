using Microsoft.Data.SqlClient;
using MyShop.BUS;
using MyShop.DTO;
using System.Collections.ObjectModel;
using System.Data;

namespace MyShop.DAO
{
	public class ShopOrderDAO
	{
		DatabaseUtilitites db = DatabaseUtilitites.getInstance();

		public ObservableCollection<ShopOrderDTO> getAll()
		{
			ObservableCollection<ShopOrderDTO> list = new ObservableCollection<ShopOrderDTO>();

			string sql = "SELECT * FROM shop_order WHERE FinalTotal > 0";
			var command = new SqlCommand(sql, db.connection);
			var reader = command.ExecuteReader();

			while (reader.Read())
			{
				ShopOrderDTO shopOrder = new ShopOrderDTO
				{
					OrderID = (int)reader["OrderID"],
					CusID = (int)reader["CusID"],
					CreateAt = (DateTime)reader["CreateAt"],
					FinalTotal = reader["FinalTotal"] == DBNull.Value ? null : (decimal?)reader["FinalTotal"],
					ProfitTotal = reader["ProfitTotal"] == DBNull.Value ? null : (decimal?)reader["ProfitTotal"]
				};
				list.Add(shopOrder);
			}
			reader.Close();

			return list;
		}

		public int insertShopOrder(ShopOrderDTO shopOrderDTO)
		{
			// insert SQL
			string query = """
				INSERT INTO shop_order(CusID, CreateAt, FinalTotal, ProfitTotal)
				VALUES (@CusID, @CreateAt, @FinalTotal, @ProfitTotal);
				SELECT ident_current('shop_order')
				""";
			var command = new SqlCommand(query, db.connection);
			command.Parameters.Add("@CusID", SqlDbType.Int).Value = shopOrderDTO.CusID;
			command.Parameters.Add("@CreateAt", SqlDbType.Date).Value = shopOrderDTO.CreateAt;
			command.Parameters.Add("@FinalTotal", SqlDbType.Money).Value = shopOrderDTO.FinalTotal;
			command.Parameters.Add("@ProfitTotal", SqlDbType.Money).Value = shopOrderDTO.ProfitTotal;

			int id = (int)((decimal)command.ExecuteScalar());

			return id;
		}

		public void insertPurchase(PurchaseDTO purchaseDTO)
		{
			string query = """
				INSERT INTO purchase(OrderID, ProID, Quantity, TotalPrice)
				VALUES (@OrderID, @ProID, @Quantity, @TotalPrice)
				""";
			var command = new SqlCommand(query, db.connection);

			command.Parameters.Add("@OrderID", SqlDbType.Int).Value = purchaseDTO.OrderID;
			command.Parameters.Add("@ProID", SqlDbType.Int).Value = purchaseDTO.ProID;
			command.Parameters.Add("@Quantity", SqlDbType.Int).Value = purchaseDTO.Quantity;
			command.Parameters.Add("@TotalPrice", SqlDbType.Money).Value = purchaseDTO.TotalPrice;

			command.ExecuteNonQuery();

			// Cập nhật Quantity ở sản phẩm ProID
			ProductBUS productBUS = new ProductBUS();
			var product = productBUS.findProductById(purchaseDTO.ProID);

			product.Quantity = product.Quantity - purchaseDTO.Quantity;

			productBUS.updateProduct(product);
		}

		public List<PurchaseDTO> getPurchases(int orderId)
		{
			List<PurchaseDTO> list = new List<PurchaseDTO>();
			string query = "SELECT * FROM purchase WHERE OrderID = @id";

			var command = new SqlCommand(query, db.connection);
			command.Parameters.Add("@id", SqlDbType.Int).Value = orderId;

			var reader = command.ExecuteReader();
			while (reader.Read())
			{
				PurchaseDTO purchase = new PurchaseDTO();
				purchase.PurchaseID = (int)reader["PurchaseID"];
				purchase.OrderID = (int)reader["OrderID"];
				purchase.ProID = (int)reader["ProID"];
				purchase.Quantity = (int)reader["Quantity"];
				purchase.TotalPrice = (decimal)reader["TotalPrice"];

				list.Add(purchase);
			}

			reader.Close();
			return list;
		}

		public void updateShopOrder(ShopOrderDTO shopOrder)
		{
			string query = """
				UPDATE shop_order
				SET CusID =  @CusID, 
					CreateAt = @CreateAt, 
					FinalTotal = @FinalTotal, 
					ProfitTotal = @ProfitTotal
				WHERE OrderID = @OrderID
				""";
			var command = new SqlCommand(query, db.connection);

			command.Parameters.Add("@OrderID", SqlDbType.Int).Value = shopOrder.OrderID;
			command.Parameters.Add("@CusID", SqlDbType.Int).Value = shopOrder.CusID;
			command.Parameters.Add("@CreateAt", SqlDbType.Date).Value = shopOrder.CreateAt;
			command.Parameters.Add("@FinalTotal", SqlDbType.Money).Value = shopOrder.FinalTotal;
			command.Parameters.Add("@ProfitTotal", SqlDbType.Money).Value = shopOrder.ProfitTotal;

			command.ExecuteNonQuery();
		}

		public void deleteOrderPurchase(int orderID)
		{
			List<PurchaseDTO> purchases = getPurchases(orderID);

			// Cập nhật Quantity ở sản phẩm ProID
			foreach (var purchaseDTO in purchases)
			{
				ProductBUS productBUS = new ProductBUS();
				var product = productBUS.findProductById(purchaseDTO.ProID);
				product.Quantity = product.Quantity + purchaseDTO.Quantity;

				productBUS.updateProduct(product);
			}

			// Xóa những purchase của orderID
			string query = """
				DELETE FROM purchase
				WHERE OrderID = @OrderID
				""";
			var command = new SqlCommand(query, db.connection);
			command.Parameters.Add("@OrderID", SqlDbType.Int).Value = orderID;
			command.ExecuteNonQuery();
		}

		public void deleteOrderById(int orderID)
		{
			// Xóa ở bảng purchase và cập nhật lại số lượng product
			deleteOrderPurchase(orderID);

			// Xóa ở bảng shop_order
			string sql = $"""
                DELETE FROM shop_order 
                WHERE OrderID = @OrderID
                """;
			var command = new SqlCommand(sql, db.connection);
			command.Parameters.Add("@OrderID", SqlDbType.Int).Value = orderID;
			command.ExecuteNonQuery();
		}
	}
}
