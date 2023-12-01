using Microsoft.Data.SqlClient;
using MyShop.DTO;

namespace MyShop.DAO
{
	public class UserDAO
	{
		DatabaseUtilitites db = DatabaseUtilitites.getInstance();

		public UserDTO? GetOne(string username, string password)
		{
			var query = "SELECT * FROM [user] WHERE Username = @Username AND Password = @Password";

			var command = new SqlCommand(query, db.connection);
			AesHelper aesHelper = new AesHelper();
			string encryptedText = aesHelper.Encrypt(password);
			command.Parameters.AddWithValue("@Username", username);
			command.Parameters.AddWithValue("@Password", encryptedText);

			var reader = command.ExecuteReader();

			UserDTO? user = null;
			if (reader.Read())
			{
				user = new UserDTO
				{
					UserID = (int)reader["UserID"],
					Username = (string)reader["Username"],
					Password = (string)reader["Password"],
					Fullname = (string)reader["Fullname"],
					Gender = (string)reader["Gender"],
					Address = (string)reader["Address"],
					Tel = (string)reader["Tel"],
				};
			}
			reader.Close();
			return user;
		}

		public bool CreateUser(UserDTO user)
		{
			var query = """
				INSERT INTO [user] (Username, Password, Fullname, Gender, Address, Tel) 
				VALUES (@Username, @Password, @Fullname, @Gender, @Address, @Tel)
				""";

			var command = new SqlCommand(query, db.connection);
			command.Parameters.AddWithValue("@Username", user.Username);
			command.Parameters.AddWithValue("@Password", user.Password);
			command.Parameters.AddWithValue("@Fullname", user.Fullname);
			command.Parameters.AddWithValue("@Gender", user.Gender);
			command.Parameters.AddWithValue("@Address", user.Address);
			command.Parameters.AddWithValue("@Tel", user.Tel);

			var rowsAffected = command.ExecuteNonQuery();

			return rowsAffected > 0;
		}
	}
}
