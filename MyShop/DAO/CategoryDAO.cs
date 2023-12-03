using Microsoft.Data.SqlClient;
using MyShop.DTO;
using System.Data;

namespace MyShop.DAO
{
	public class CategoryDAO
	{
		DatabaseUtilitites db = DatabaseUtilitites.getInstance();

		public CategoryDTO getCategoryById(int id)
		{
			List<CategoryDTO> list = new List<CategoryDTO>();

			string sql = $"SELECT * FROM category WHERE CatID = @id";

			var command = new SqlCommand(sql, db.connection);
			command.Parameters.Add("@id", SqlDbType.Int).Value = id;

			var reader = command.ExecuteReader();
			while (reader.Read())
			{
				CategoryDTO category = new CategoryDTO();

				category.CatID = (int)reader["CatID"];
				category.CatName = reader["CatName"] == DBNull.Value ? "Lỗi tên thể loại" : (string?)reader["CatName"];
				category.CatIcon = (string)reader["CatIcon"];
				category.CatDescription = (string)reader["CatDescription"];

				list.Add(category);
			}

			reader.Close();
			return list[0];
		}
	}
}
