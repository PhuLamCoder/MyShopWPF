using MyShop.DAO;
using MyShop.Views.LoginView;
using System.Windows;

namespace MyShop.Views.ConfigView
{
	/// <summary>
	/// Interaction logic for ConfigWindow.xaml
	/// </summary>
	public partial class ConfigWindow : Window
	{
		public ConfigWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{

		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string servername = ServerTermTextBox.Text;
			string password = PasswordTermTextBox.Text;
			string username = UsernameTermTextBox.Text;
			string databasename = DatabaseTermTextBox.Text;

			new DatabaseUtilitites(
				  servername,
				  databasename,
				  username,
				  password
			);

			if (DatabaseUtilitites.isSelectedDatabase == true)
			{
				LoginWindow loginWindow = new LoginWindow();
				loginWindow.Show();
				this.Close();
			}
		}
	}
}
