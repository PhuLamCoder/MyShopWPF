using Microsoft.Data.SqlClient;
using MyShop.DTO;
using System.Collections.ObjectModel;

namespace MyShop.DAO
{
	public class OrderDAO
	{
		DatabaseUtilitites db = DatabaseUtilitites.getInstance();

		public async Task<ObservableCollection<ShopOrderDTO>> getAll()
		{
			ObservableCollection<ShopOrderDTO> list = new ObservableCollection<ShopOrderDTO>();

			await Task.Run(() =>
			{
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
			});
			return list;
		}
	}
}
