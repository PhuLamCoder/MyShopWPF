using MyShop.BUS;
using MyShop.DTO;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace MyShop.Views.MainView.Pages
{
	/// <summary>
	/// Interaction logic for Home.xaml
	/// </summary>
	public partial class Home : Page
	{
		class Resources
		{
			public string FirstIcon { get; set; }
			public string LastIcon { get; set; }
			public string NextIcon { get; set; }
			public string PrevIcon { get; set; }
			public int ProductListWidth { get; set; }
		}

		// Mục đích là đổ dữ liệu của Class này lên UI
		class Data
		{
			public string? ProName { get; set; }
			public string? ProImage { get; set; }
			public string? CatIcon { get; set; }
			public string? CatName { get; set; }
			public decimal? PromotionPrice { get; set; }

			//public int ProductItemWitdh { get; set; }
			//public string ScaleValue { get; set; }
			public int DiscountPercent { get; set; }

			public Data(ProductDTO productDTO, CategoryDTO categoryDTO, int discountPercent)
			{
				ProName = productDTO.ProName;
				ProImage = productDTO.ImagePath;
				PromotionPrice = productDTO.PromotionPrice;
				CatIcon = categoryDTO.CatIcon;
				CatName = categoryDTO.CatName;
				//ProductItemWitdh = 400;
				//ScaleValue = "1";
				DiscountPercent = discountPercent;
			}
		}


		private List<ProductDTO>? _products = null;
		private string _currentKey = "";
		private int _currentPage = 1;
		private int _rowsPerPage = 9;
		private int _totalItems = 0;
		private int _totalPages = 0;
		private string? _currentSort = null;
		private bool _currentOrder = true;
		private Decimal? _currentStartPrice = null;
		private Decimal? _currentEndPrice = null;
		private Frame _pageNavigation;
		private FileInfo _selectedFile;

		//public Tuple<string, int, int, Decimal?, Decimal?> getCurrentState()
		//{
		//	return new Tuple<string, int, Decimal?, Decimal?>(
		//		_currentKey,
		//		_currentPage,
		//		_currentStartPrice,
		//		_currentEndPrice
		//	);
		//}

		public Home(Frame pageNavigation, int page = 1, string keyword = "", 
			Decimal? currentStartPrice = null, Decimal? currentEndPrice = null)
		{
			_pageNavigation = pageNavigation;
			InitializeComponent();
	
			_currentPage = page;
			_currentKey = keyword;
			_currentStartPrice = currentStartPrice;
			_currentEndPrice = currentEndPrice;
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			updateDataSource(_currentPage, _currentKey, _currentStartPrice, _currentEndPrice);

			this.DataContext = new Resources()
			{
				FirstIcon = "Assets/Images/ic-first.png",
				LastIcon = "Assets/Images/ic-last.png",
				NextIcon = "Assets/Images/ic-next.png",
				PrevIcon = "Assets/Images/ic-prev.png",
			};
		}

		private async void updateDataSource(int page = 1, string keyword = "",
			Decimal? currentStartPrice = null, Decimal? currentEndPrice = null, string? currentSort = null, bool currentOrder = true)
		{
			MessageText.Text = string.Empty;
			List<Data> list = new List<Data>();
			ProductBUS productBUS = new ProductBUS();
			CategoryBUS categoryBUS = new CategoryBUS();
			PromotionBUS promotionBUS = new PromotionBUS();

			(_products, _totalItems) = await productBUS.findProductBySearch(_currentPage, _rowsPerPage, keyword, 
				currentStartPrice, currentEndPrice, currentSort, currentOrder);
			
			foreach (var product in _products)
			{
				int DiscountPercent = 0;
				if (product.PromoID != null)
				{
					DiscountPercent = promotionBUS.getPromotionById((int)product.PromoID).DiscountPercent;
				}
				var category = categoryBUS.getCategoryById(product.CatID);
				list.Add(new Data(product, category, DiscountPercent));
			}

			if (list.Count == 0)
			{
				MessageText.Text = "Opps! Không tìm thấy bất kì sản phẩm nào!";
			}

			dataListView.ItemsSource = list;

			infoTextBlock.Text = $"Đang hiển thị {_products.Count} trên tổng số {_totalItems} sản phẩm";
			updatePagingInfo();
		}

		private void updatePagingInfo()
		{
			_totalPages = _totalItems / _rowsPerPage + (_totalItems % _rowsPerPage == 0 ? 0 : 1);
			_totalPages = _totalPages == 0 ? 1 : _totalPages;
			pageInfoTextBlock.Text = $"{_currentPage}/{_totalPages}";
		}

		private void SearchTermTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				string keyword = SearchTermTextBox.Text.Trim();
				_currentKey = keyword;
				updateDataSource(1, keyword, _currentStartPrice, _currentEndPrice);
			}
		}

		private void PrevButton_Click(object sender, RoutedEventArgs e)
		{
			if (_currentPage > 1)
			{
				_currentPage--;
				updateDataSource(_currentPage, _currentKey, _currentStartPrice, _currentEndPrice);
			}
		}

		private void NextButton_Click(object sender, RoutedEventArgs e)
		{
			if (_currentPage < _totalPages)
			{
				_currentPage++;
				updateDataSource(_currentPage, _currentKey, _currentStartPrice, _currentEndPrice);
			}
		}

		private void FirstButton_Click(object sender, RoutedEventArgs e)
		{
			_currentPage = 1;
			updateDataSource(_currentPage, _currentKey, _currentStartPrice, _currentEndPrice);
		}

		private void LastButton_Click(object sender, RoutedEventArgs e)
		{
			_currentPage = _totalPages;
			updateDataSource(_currentPage, _currentKey, _currentStartPrice, _currentEndPrice);
		}

		private void PriceCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (PriceCombobox.SelectedIndex >= 0)
			{
				_currentPage = 1;
				// Giá dưới 5 triệu
				if (PriceCombobox.SelectedIndex == 1)
				{
					_currentStartPrice = 0;
					_currentEndPrice = 5000000;
					updateDataSource(_currentPage, _currentKey, _currentStartPrice, _currentEndPrice);
					updatePagingInfo();
				}
				// Giá từ 5 triệu đến dưới 10 triệu
				if (PriceCombobox.SelectedIndex == 2)
				{
					_currentStartPrice = 5000000;
					_currentEndPrice = 10000000;
					updateDataSource(_currentPage, _currentKey, _currentStartPrice, _currentEndPrice);
					updatePagingInfo();
				}
				// Giá dưới 10 triệu đến dưới 15 triệu
				if (PriceCombobox.SelectedIndex == 3)
				{
					_currentStartPrice = 10000000;
					_currentEndPrice = 15000000;
					updateDataSource(_currentPage, _currentKey, _currentStartPrice, _currentEndPrice);
					updatePagingInfo();
				}
				// Giá từ 15 trở lên triệu
				if (PriceCombobox.SelectedIndex == 4)
				{
					_currentStartPrice = 15000000;
					_currentEndPrice = Decimal.MaxValue;
					updateDataSource(_currentPage, _currentKey, _currentStartPrice, _currentEndPrice);
					updatePagingInfo();
				}
				// Tất cả giá
				if (PriceCombobox.SelectedIndex == 5)
				{
					_currentStartPrice = null;
					_currentEndPrice = null;
					updateDataSource(_currentPage, _currentKey, null, null);
					updatePagingInfo();
				}
			}
		}

		private void SortCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (SortCombobox.SelectedIndex >= 0)
			{
				// Giá tăng
				if (SortCombobox.SelectedIndex == 1)
				{
					_currentSort = "price";
					_currentOrder = true;
					updateDataSource(1, _currentKey, _currentStartPrice, _currentEndPrice, _currentSort, _currentOrder);
					updatePagingInfo();
				}
				// Giá giảm
				if (SortCombobox.SelectedIndex == 2)
				{
					_currentSort = "price";
					_currentOrder = false;
					updateDataSource(1, _currentKey, _currentStartPrice, _currentEndPrice, _currentSort, _currentOrder);
					updatePagingInfo();
				}
				// Tên tăng
				if (SortCombobox.SelectedIndex == 3)
				{
					_currentSort = "name";
					_currentOrder = true;
					updateDataSource(1, _currentKey, _currentStartPrice, _currentEndPrice, _currentSort, _currentOrder);
					updatePagingInfo();
				}
				// Tên giảm
				if (SortCombobox.SelectedIndex == 4)
				{
					_currentSort = "name";
					_currentOrder = false;
					updateDataSource(1, _currentKey, _currentStartPrice, _currentEndPrice, _currentSort, _currentOrder);
					updatePagingInfo();
				}
				// Không sắp xếp
				if (SortCombobox.SelectedIndex == 5)
				{
					_currentSort = null;
					_currentOrder = true;
					updateDataSource(1, _currentKey, _currentStartPrice, _currentEndPrice, _currentSort, _currentOrder);
					updatePagingInfo();
				}
			}
		}


		// SỰ KIỆN CẦN XỬ LÍ
		private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			int i = dataListView.SelectedIndex;

			var product = _products[i];
			if (product != null)
			{
				//_pageNavigation.NavigationService.Navigate(new ProductDetail(this, product, _pageNavigation));
			}
		}

		private void AddProduct_Click(object sender, RoutedEventArgs e)
		{
			//_pageNavigation.NavigationService.Navigate(new AddProduct(_pageNavigation));
		}

		private void Sheet_Click(object sender, RoutedEventArgs e)
		{
			//string filename = "";


			//var screen = new OpenFileDialog();
			//screen.Filter = "Files|*.xlsx; *.csv;";
			//if (screen.ShowDialog() == true)
			//{
			//	filename = screen.FileName;

			//	var sheetBUS = new SheetBUS();
			//	var productBUS = new ProductBUS();

			//	var products = sheetBUS.ReadExcelFile(filename);

			//	foreach (var product in products)
			//	{
			//		productBUS.saveProduct(product);
			//	}
			//	MessageBox.Show("Đã thêm thành công", "Thông báo");
			//}
			//else
			//{
			//	MessageBox.Show("Đã có lỗi xảy ra!", "Thông báo");
			//}
		}

		bool flag = false;
		private void Option_Click(object sender, RoutedEventArgs e)
		{
			if (flag == false)
			{
				_rowsPerPage = 6;
			}
			if (flag == true)
			{
				_rowsPerPage = 9;
			}

			updateDataSource(1, _currentKey, _currentStartPrice, _currentEndPrice);
			//updatePagingInfo();

			flag = !flag;
		}
	}
}
