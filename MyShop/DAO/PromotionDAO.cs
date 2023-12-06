using Microsoft.Data.SqlClient;
using MyShop.DTO;
using System.Collections.ObjectModel;
using System.Data;

namespace MyShop.DAO
{
	public class PromotionDAO
	{
		DatabaseUtilitites db = DatabaseUtilitites.getInstance();

		public PromotionDTO getPromoById(int id)
		{
			List<PromotionDTO> list = new List<PromotionDTO>();

			string sql = $"SELECT * FROM promotion WHERE IdPromo = @id";

			var command = new SqlCommand(sql, db.connection);
			command.Parameters.Add("@id", SqlDbType.Int).Value = id;

			var reader = command.ExecuteReader();
			while (reader.Read())
			{
				PromotionDTO category = new PromotionDTO();
				category.IdPromo = (int)reader["IdPromo"];
				category.PromoCode = reader["PromoCode"] == DBNull.Value ? null : (string?)reader["PromoCode"];
				category.DiscountPercent = (int)reader["DiscountPercent"];
				list.Add(category);
			}
			reader.Close();
			return list[0];
		}

		public ObservableCollection<PromotionDTO> getAll()
		{
			ObservableCollection<PromotionDTO> list = new ObservableCollection<PromotionDTO>();

			string sql = "SELECT * FROM promotion";
			var command = new SqlCommand(sql, db.connection);
			var reader = command.ExecuteReader();

			while (reader.Read())
			{
				PromotionDTO category = new PromotionDTO();
				category.IdPromo = (int)reader["IdPromo"];
				category.PromoCode = reader["PromoCode"] == DBNull.Value ? null : (string?)reader["PromoCode"];
				category.DiscountPercent = (int)reader["DiscountPercent"];

				list.Add(category);
			}
			reader.Close();

			return list;
		}

		public int insertPromo(PromotionDTO category)
		{
			string query = """
				INSERT INTO promotion(PromoCode, DiscountPercent)
				VALUES (@PromoCode, @DiscountPercent);
				SELECT ident_current('promotion')
				""";
			var command = new SqlCommand(query, db.connection);
			command.Parameters.Add("@PromoCode", SqlDbType.NVarChar).Value = category.PromoCode;
			command.Parameters.Add("@DiscountPercent", SqlDbType.Int).Value = category.DiscountPercent;

			int id = (int)((decimal)command.ExecuteScalar());
			return id;
		}

		public Tuple<Boolean, string> delPromoById(int idPromo)
		{
			string message = "";
			bool isSuccess = true;

			string sql = $"""
                DELETE promotion 
                WHERE IdPromo={idPromo}
                """;
			var command = new SqlCommand(sql, db.connection);
			try
			{
				command.ExecuteNonQuery();
			}
			catch (SqlException ex)
			{
				message = ex.Message;
				isSuccess = false;
			}

			return new Tuple<bool, string>(isSuccess, message);
		}
	}
}
