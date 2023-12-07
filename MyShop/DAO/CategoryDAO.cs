using Microsoft.Data.SqlClient;
using MyShop.DTO;
using System.Collections.ObjectModel;
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
				category.CatName = reader["CatName"] == DBNull.Value ? "Lỗi tên thể loại" : (string)reader["CatName"];
				category.CatDescription = (string)reader["CatDescription"];

				list.Add(category);
			}

			reader.Close();
			return list[0];
		}

		public ObservableCollection<CategoryDTO> getAll()
		{
			ObservableCollection<CategoryDTO> list = new ObservableCollection<CategoryDTO>();

			string sql = "SELECT * FROM category";
			var command = new SqlCommand(sql, db.connection);
			var reader = command.ExecuteReader();

			while (reader.Read())
			{
				CategoryDTO category = new CategoryDTO();
				category.CatID = (int)reader["CatID"];
				category.CatName = reader["CatName"] == DBNull.Value ? "Lỗi tên thể loại" : (string)reader["CatName"];
				category.CatDescription = (string)reader["CatDescription"];
				list.Add(category);
			}
			reader.Close();

			return list;
		}

		public int insertCategory(CategoryDTO category)
		{
			string query = """
				INSERT INTO category(CatName, CatDescription)
				VALUES (@CatName, @CatDescription);
				SELECT ident_current('category')
				""";
			var command = new SqlCommand(query, db.connection);

			command.Parameters.Add("@CatName", SqlDbType.NVarChar).Value = category.CatName;
			command.Parameters.Add("@CatDescription", SqlDbType.NVarChar).Value = category.CatDescription;

			int id = (int)((decimal)command.ExecuteScalar());

			return id;
		}

		public Tuple<Boolean, string> delCategoryById(int catID)
		{
			string message = "";
			bool isSuccess = true;

			string query = $"""
                DELETE category 
                WHERE CatID = {catID}
                """;

			var command = new SqlCommand(query, db.connection);
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

		public void updateCategory(CategoryDTO category)
		{
			string query = """
				UPDATE category
				SET CatName =  @CatName, 
					CatDescription = @CatDescription
				WHERE CatID = @CatID
				""";
			var command = new SqlCommand(query, db.connection);

			command.Parameters.Add("@CatID", SqlDbType.Int).Value = category.CatID;
			command.Parameters.Add("@CatName", SqlDbType.NVarChar).Value = category.CatName;
			command.Parameters.Add("@CatDescription", SqlDbType.NVarChar).Value = category.CatDescription;

			command.ExecuteNonQuery();
		}
	}
}
