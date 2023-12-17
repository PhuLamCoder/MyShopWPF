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

			string sql = "SELECT * FROM customer";
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
				list.Add(customer);
			}

			reader.Close();
			result = list[0];

			return result;
		}
	}
}
