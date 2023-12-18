using MyShop.BUS;
using MyShop.DAO;
using MyShop.DTO;
using MyShop.Views.LoginView;
using System.Windows;

namespace MyShop.Views.SignupView
{
	/// <summary>
	/// Interaction logic for SignupWindow.xaml
	/// </summary>
	public partial class SignupWindow : Window
	{
		public SignupWindow()
		{
			InitializeComponent();
		}

		class Resoures
		{
			public string Logo { get; set; }
		}


		private void Button_Signup_Click(object sender, RoutedEventArgs e)
		{
			string inputUsername = txtUsername.Text;
			string inputPassword = txtPassword.Password;
			string inputFullname = txtFullname.Text;
			string inputAdress = txtAdress.Text;
			string inputNumberphone = txtNumberphone.Text;

			if (string.IsNullOrEmpty(inputUsername) || string.IsNullOrEmpty(inputPassword) || 
				string.IsNullOrEmpty(inputFullname) || string.IsNullOrEmpty(inputAdress) || 
				string.IsNullOrEmpty(inputNumberphone))
			{
				txtFailSignUp.Text = "Please enter all information";
			}
			else
			{
				AesHelper aesHelper = new AesHelper();
				string encryptedText = aesHelper.Encrypt(inputPassword);
				string decryptedText = aesHelper.Decrypt(encryptedText);

				UserDTO userDTO = new UserDTO
				{
					Username = inputUsername,
					Password = encryptedText,
					Fullname = inputFullname,
					Gender = "Nam",
					Address = inputAdress,
					Tel = inputNumberphone,
				};


				UserBUS userBUS = new UserBUS();

				bool signupSucess = userBUS.createUser(userDTO);
				if (signupSucess)
				{
					LoginWindow loginWindow = new LoginWindow();
					loginWindow.Show();
					Close();
				}
				else
				{
					txtFailSignUp.Text = "Failed to create new account";
				}
			}

		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			DataContext = new Resoures()
			{
				Logo = "Assets/Images/logo.png",
			};
		}

		private void Button_Exit_Click(object sender, RoutedEventArgs e)
		{
			LoginWindow loginWindow = new LoginWindow();
			loginWindow.Show();
			Close();
		}
	}
}
