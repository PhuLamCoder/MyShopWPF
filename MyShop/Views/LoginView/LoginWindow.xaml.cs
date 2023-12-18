using System.Diagnostics;
using System.Windows;
using MyShop.BUS;
using MyShop.DTO;
using MyShop.Views.MainView;
using MyShop.Views.SignupView;

namespace MyShop.Views.LoginView
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		public LoginWindow()
		{
			InitializeComponent();
		}

		class Resoures
		{
			public string Logo { get; set; }
			public string MainBgPath { get; set; }
		}

		private void Button_Login(object sender, RoutedEventArgs e)
		{
			string inputUsername = txtUsername.Text;
			string inputPassword = txtPassword.Password;

			UserBUS productBUS = new UserBUS();
			UserDTO? handleApiUser = productBUS.getOne(inputUsername, inputPassword);

			if (handleApiUser != null)
			{
				bool remember = RememberMeCheckBox.IsChecked == true;
				Properties.Settings.Default.IdUser = handleApiUser.UserID;
				Properties.Settings.Default.Save();
				if (remember)
				{
					Properties.Settings.Default.UsernameRemember = true;
					Properties.Settings.Default.Save();
				}

				Trace.WriteLine("Sucess");
				MainWindow mainPage = new MainWindow();
				mainPage.Show();
				Close();
			}
			else
			{
				txtFailLogin.Text = "Invalid username or password";
				Trace.WriteLine("Invalid Username or password");
			}
		}

		private void Button_Signup(object sender, RoutedEventArgs e)
		{
			SignupWindow signUpWindow = new SignupWindow();
			signUpWindow.Show();
			Close();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (Properties.Settings.Default.UsernameRemember)
			{
				MainWindow mainPage = new MainWindow();
				mainPage.Show();
				Close();
			}

			DataContext = new Resoures()
			{
				Logo = "Assets/Images/logo.png",
			};
		}
	}
}
