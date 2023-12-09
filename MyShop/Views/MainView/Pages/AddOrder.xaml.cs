using MyShop.BUS;
using MyShop.DTO;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyShop.Views.MainView.Pages
{
    /// <summary>
    /// Interaction logic for AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Page
    {
		Frame _pageNavigation;
		private ProductBUS _productBUS;
		private CustomerBUS _customerBUS;
		private ShopOrderBUS _orderBUS;

		private ObservableCollection<Data> _data;
		private ObservableCollection<CustomerDTO> _customers;
		private ObservableCollection<ProductDTO> _products;

		private ProductDTO _currentProduct;

		private Decimal _currentTotalPrice = 0;
		private List<PurchaseDTO> _purchaseBuffer;

		public class Data
		{
			public string ProName { get; set; }
			public decimal Price { get; set; }
			public int Quantity { get; set; }
			public decimal TotalPrice { get; set; }
		}

		public AddOrder(Frame pageNavigation)
		{
			_pageNavigation = pageNavigation;

			_productBUS = new ProductBUS();
			_customerBUS = new CustomerBUS();
			_orderBUS = new ShopOrderBUS();

			_currentProduct = new ProductDTO();
			_data = new ObservableCollection<Data>();
			_purchaseBuffer = new List<PurchaseDTO>();

			InitializeComponent();
		}

		private async void Page_Loaded(object sender, RoutedEventArgs e)
		{
			_products = await _productBUS.getAll(sortBy: "name");
			ProductCombobox.ItemsSource = _products;
			_currentProduct .copy(_products[0]);
			ProductCombobox.SelectedIndex = 0;

			_customers = _customerBUS.getAll(sortBy: "name");
			CustomerCombobox.ItemsSource = _customers;
			CustomerCombobox.SelectedIndex = 0;

			FinalPrice.Text = string.Format("{0:N0} đ", _currentTotalPrice);

			ordersListView.ItemsSource = _data;
			this.DataContext = _currentProduct;
		}

		private void SaveOrder_Click(object sender, RoutedEventArgs e)
		{
			if (_purchaseBuffer.Count > 0)
			{
				var customerDTO = (CustomerDTO) CustomerCombobox.SelectedValue;
				var shopOrderDTO = new ShopOrderDTO();

				shopOrderDTO.CusID = customerDTO.CusID;
				shopOrderDTO.CreateAt = DateTime.Now.Date;
				shopOrderDTO.FinalTotal = _currentTotalPrice;
				shopOrderDTO.ProfitTotal = _orderBUS.calOrderProfit(_currentTotalPrice);

				int orderID = _orderBUS.addShopOrder(shopOrderDTO);

				// lúc này mới lưu vào database 
				foreach (var purchase in _purchaseBuffer)
				{
					// lúc mà thêm purchase thì ở DAO đã xóa số lượng bên product luôn rồi !
					purchase.OrderID = orderID;
					_orderBUS.addPurchase(purchase);
				}

				// Thêm vào cho danh sách order trang manage
				MessageBox.Show("Đã lưu đơn hàng thành công!", "Thông Báo",
					MessageBoxButton.OK, MessageBoxImage.Information);
				_pageNavigation.NavigationService.GoBack();
			}
			else
			{
				MessageBox.Show("Bạn chưa chọn sản phẩm nào!", "Thông Báo",
					MessageBoxButton.OK, MessageBoxImage.Warning);
			}
		}

		private void AddProduct_Click(object sender, RoutedEventArgs e)
		{
			int quantity = 0;
			try
			{
				quantity = int.Parse(QuantityTermTextBox.Text);
			} catch (Exception)
			{
				MessageBox.Show("Số lượng phải là số!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			var purchareDTO = new PurchaseDTO();
			var productDTO = (ProductDTO) ProductCombobox.SelectedValue;

			if (quantity <= 0 || quantity > _currentProduct.Quantity)
			{
				MessageBox.Show("Số lượng không hợp lệ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			// Tạo ra 1 chi tiết trong đơn hàng
			decimal priceOfProduct = _currentProduct.PromotionPrice;
			purchareDTO.ProID = productDTO.ProId;
			purchareDTO.Quantity = quantity;
			purchareDTO.TotalPrice = priceOfProduct * quantity;
			var existedPurchase = _purchaseBuffer.Find(purchase => purchase.ProID == purchareDTO.ProID);
			if (existedPurchase != null)
			{
				MessageBoxResult result = MessageBox.Show("Xóa và thêm lại để cập nhật số lượng!", "Thông Báo",
				MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			_purchaseBuffer.Add(purchareDTO);

			// Hiển thị thông tin lên giao diện
			var data = new Data
			{
				Quantity = quantity,
				Price = priceOfProduct,
				ProName = productDTO.ProName!,
				TotalPrice = priceOfProduct * quantity
			};

			// Cập nhật lại số lượng trên giao diện và số lượng trong danh sách
			_currentProduct.Quantity -= quantity;
			_products.First(product => product.ProId == _currentProduct.ProId).Quantity -= quantity;

			_currentTotalPrice += data.TotalPrice;
			_data.Add(data);

			FinalPrice.Text = string.Format("{0:N0} đ", _currentTotalPrice);
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result = MessageBox.Show("Đơn hàng chưa được lưu. Bạn có muốn tiếp tục không?", "Thông Báo", 
				MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.No)
			{
				_pageNavigation.NavigationService.GoBack();
			}
		}

		private void SaveCustomer_Click(object sender, RoutedEventArgs e)
		{
			/*
			var customerDTO = new CustomerDTO();

			int CusID;


			customerDTO.CusName = CustomerTermTextBox.Text;
			CusID = _customerBUS.addCustomer(customerDTO);
			customerDTO.CusID = CusID;

			_customers.Add(customerDTO);

			MessageBox.Show("Khách hàng đã thêm thành công", "Thông báo", MessageBoxButton.OK);
			*/
		}

		private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var result = MessageBox.Show("Bạn có muốn xóa không?", "Thông Báo", 
				MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.Yes)
			{
				int index = ordersListView.SelectedIndex; if (index == -1) return;
				_currentTotalPrice -= _data[index].TotalPrice;
				FinalPrice.Text = string.Format("{0:N0} đ", _currentTotalPrice);

				// Tăng lại số lượng sản phẩn khi xóa khỏi danh sách mua
				var deleteProduct = _products.First(product => product.ProId == _purchaseBuffer[index].ProID);
				deleteProduct.Quantity += _purchaseBuffer[index].Quantity;
				// Cập nhật lại số lượng lên giao diện
				if (deleteProduct.ProId  == _currentProduct.ProId)
				{
					_currentProduct.copy(deleteProduct);
				}

				_data.RemoveAt(index);
				_purchaseBuffer.RemoveAt(index);
			}
		}

		private void ProductCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int index = ProductCombobox.SelectedIndex;

			if (index != -1)
				_currentProduct.copy(_products[index]);
		}
	}
}
