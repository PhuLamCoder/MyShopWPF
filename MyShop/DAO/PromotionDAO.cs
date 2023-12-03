using Microsoft.Data.SqlClient;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
	}
}
