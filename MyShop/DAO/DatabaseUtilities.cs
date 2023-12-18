using System.Windows;
using Microsoft.Data.SqlClient;

namespace MyShop.DAO
{
	class DatabaseUtilitites
	{
		private string _server;
		private string _databaseName;
		private string _user;
		private string _password;
		public static bool isSelectedDatabase = false;

		private static DatabaseUtilitites? _instance = null;
		SqlConnection? _connection;
		public static DatabaseUtilitites getInstance()
		{
			if (_instance == null)
			{
				_instance = new DatabaseUtilitites();
			}
			return _instance;
		}

		public DatabaseUtilitites()
		{
			_server = "sqlexpress";
			_databaseName = "MyShop";
			_user = "admin";
			_password = "admin";
			_connection = null;
		}

		public DatabaseUtilitites(string server, string databaseName, string user, string password)
		{
			_server = server;
			_databaseName = databaseName;
			_user = user;
			_password = password;

			string connectionString = $""""
				Server = {_server}; 
				User ID = {_user}; 
				Password={_password}; 
				Database = {_databaseName}; 
				TrustServerCertificate=True;
				Connect Timeout=5
				"""";

			_connection = new SqlConnection(connectionString);

			try
			{
				_connection.Open();
				isSelectedDatabase = true;

			}
			catch (Exception ex)
			{
				MessageBox.Show($"Failed to connect to database! Reason: {ex.Message}", 
					"Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}

			_instance = this;
		}

		public SqlConnection? connection { 
			get { 
				return _connection; 
			} 
		}
	}
}
