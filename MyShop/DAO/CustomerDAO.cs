using Microsoft.Data.SqlClient;
using MyShop.DTO;
using System.Collections.ObjectModel;
using System.Data;

namespace MyShop.DAO
{
    class CustomerDAO
    {
		DatabaseUtilitites db = DatabaseUtilitites.getInstance();

		public ObservableCollection<CustomerDTO> getAll()
		{
			ObservableCollection<CustomerDTO> list = new ObservableCollection<CustomerDTO>();

			string sql = "SELECT * FROM customer WHERE Block = 0";
			var command = new SqlCommand(sql, db.connection);

			var reader = command.ExecuteReader();

			while (reader.Read())
			{
				CustomerDTO customer = new CustomerDTO();
				customer.CusID = (int)reader["CusID"];
				customer.CusName = (string)reader["CusName"];
				customer.Gender = (string)reader["Gender"];
				customer.DOB = (DateTime)reader["DOB"];
				customer.Address = (string)reader["Address"];
				customer.Tel = (string)reader["Tel"];
				customer.Block = (int)reader["Block"];
				list.Add(customer);
			}
			reader.Close();

			return list;
		}

		public CustomerDTO getCustomerById(int cusID)
		{
			List<CustomerDTO> list = new List<CustomerDTO>();
			CustomerDTO result = new();

			string sql = $"SELECT * FROM customer WHERE CusID = @id";

			var command = new SqlCommand(sql, db.connection);
			command.Parameters.Add("@id", SqlDbType.Int).Value = cusID;

			var reader = command.ExecuteReader();
			while (reader.Read())
			{
				CustomerDTO customer = new CustomerDTO();
				customer.CusID = (int)reader["CusID"];
				customer.CusName = (string)reader["CusName"];
				customer.Gender = (string)reader["Gender"];
				customer.DOB = (DateTime)reader["DOB"];
				customer.Address = (string)reader["Address"];
				customer.Tel = (string)reader["Tel"];
				customer.Block = (int)reader["Block"];
				list.Add(customer);
			}

			reader.Close();
			result = list[0];

			return result;
		}

		public int insertCustomer(CustomerDTO customerDTO)
		{
			string query = """
				INSERT INTO customer(CusName, Gender, DOB, Address, Tel, Block)
				VALUES(@CusName, @Gender, @DOB, @Address, @Tel, @Block);
				SELECT ident_current('customer')
				""";
			var command = new SqlCommand(query, db.connection);

			command.Parameters.Add("@CusName", SqlDbType.NVarChar).Value = customerDTO.CusName;
			command.Parameters.Add("@Gender", SqlDbType.NVarChar).Value = customerDTO.Gender;
			command.Parameters.Add("@DOB", SqlDbType.Date).Value = customerDTO.DOB;
			command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = customerDTO.Address;
			command.Parameters.Add("@Tel", SqlDbType.NVarChar).Value = customerDTO.Tel;
			command.Parameters.Add("@Block", SqlDbType.Int).Value = customerDTO.Block;

			int id = (int)((decimal)command.ExecuteScalar());

			return id;
		}

		// Soft delete
		public void deleteCustomerById(int CusId)
		{
			string sql = $"""
                UPDATE customer 
                SET Block = {1}
                WHERE CusID = {CusId}
                """;

			var command = new SqlCommand(sql, db.connection);
			command.ExecuteNonQuery();
		}

		public void updateCustomer(CustomerDTO customer)
		{
			string query = """
				UPDATE customer
				SET CusName = @CusName, 
					Gender = @Gender,
					DOB = @DOB,
					Address = @Address,
					Tel = @Tel
				WHERE CusID = @CusID
				""";
			var command = new SqlCommand(query, db.connection);

			command.Parameters.Add("@CusID", SqlDbType.Int).Value = customer.CusID;
			command.Parameters.Add("@CusName", SqlDbType.NVarChar).Value = customer.CusName;
			command.Parameters.Add("@Gender", SqlDbType.NVarChar).Value = customer.Gender;
			command.Parameters.Add("@DOB", SqlDbType.Date).Value = customer.DOB;
			command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = customer.Address;
			command.Parameters.Add("@Tel", SqlDbType.NVarChar).Value = customer.Tel;

			command.ExecuteNonQuery();
		}
	}
}
