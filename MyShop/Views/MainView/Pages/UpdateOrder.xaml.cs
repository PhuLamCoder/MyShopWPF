using MyShop.BUS;
using MyShop.DTO;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyShop.Views.MainView.Pages
{
	/// <summary>
	/// Interaction logic for UpdateOrder.xaml
	/// </summary>
	public partial class UpdateOrder : Page
	{
		private Frame _pageNavigation;
		private OrderDetail.OrderInfo? _orderInfo;
		private ObservableCollection<OrderDetail.Data>? _dataList;

		private ProductBUS _productBUS;
		private CustomerBUS _customerBUS;
		private ShopOrderBUS _orderBUS;

		private ProductDTO _currentProduct;
		private ObservableCollection<CustomerDTO> _customers;
		private ObservableCollection<ProductDTO> _products;
		private Decimal _currentTotalPrice = 0;


		public UpdateOrder(Frame pageNavigation, OrderDetail.OrderInfo orderInfo, ObservableCollection<OrderDetail.Data> dataList)
		{
			InitializeComponent();
			_pageNavigation = pageNavigation;
			_orderInfo = orderInfo;
			_dataList = dataList;
			_currentProduct = new ProductDTO();

			_productBUS = new ProductBUS();
			_customerBUS = new CustomerBUS();
			_orderBUS = new ShopOrderBUS();

			// Load dữ liệu
			_products = _productBUS.getAll(sortBy: "name");
			_currentProduct.copy(_products[0]);
			_customers = _customerBUS.getAll(sortBy: "name");
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			// Hiển thị danh sách sản phẩm
			ProductCombobox.ItemsSource = _products;
			ProductCombobox.SelectedIndex = 0;
			DataContext = _currentProduct;

			// Hiển thị danh sách và chọn tên khách hàng
			CustomerCombobox.ItemsSource = _customers;
			for (int i = 0; i < _customers.Count; i++)
			{
				if (_customers[i].CusID == _orderInfo!.Customer.CusID)
				{
					CustomerCombobox.SelectedIndex = i;
					break;
				}
			}

			_currentTotalPrice = (decimal)_orderInfo!.Order.FinalTotal!;
			FinalPrice.Text = string.Format("{0:N0} đ", _currentTotalPrice);

			ordersListView.ItemsSource = _dataList;
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result = MessageBox.Show("Đơn hàng chưa được lưu. Bạn có muốn tiếp tục không?", "Thông Báo",
				MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.No)
			{
				// Load lại dữ liệu cũ khi quay về màn hình detail
				_dataList!.Clear();
				List<PurchaseDTO> _purchases = _orderBUS.findPurchases(_orderInfo!.Order.OrderID);

				foreach (var purchase in _purchases)
				{
					var product = _productBUS.findProductById(purchase.ProID);
					OrderDetail.Data item = new OrderDetail.Data();
					item.Product = product;
					item.Purchase = purchase;
					_dataList.Add(item);
				}

				CustomerDTO _customer = _customerBUS.findCustomerById(_orderInfo!.Order.CusID);
				_orderInfo.Customer = _customer;
				_pageNavigation.NavigationService.GoBack();
			}
		}

		private void ProductCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int index = ProductCombobox.SelectedIndex;

			if (index != -1)
				_currentProduct.copy(_products[index]);
		}

		private void AddProduct_Click(object sender, RoutedEventArgs e)
		{
			int quantity = 0;
			try
			{
				quantity = int.Parse(QuantityTermTextBox.Text);
			}
			catch (Exception)
			{
				MessageBox.Show("Số lượng phải là số!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			if (quantity <= 0 || quantity > _currentProduct.Quantity)
			{
				MessageBox.Show("Số lượng không hợp lệ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			var productDTO = new ProductDTO();
			productDTO.copy((ProductDTO)ProductCombobox.SelectedValue);

			var purchareDTO = new PurchaseDTO();
			decimal priceOfProduct = _currentProduct.PromotionPrice;
			purchareDTO.ProID = productDTO.ProId;
			purchareDTO.Quantity = quantity;
			purchareDTO.TotalPrice = priceOfProduct * quantity;

			var existedPurchase = _dataList.FirstOrDefault(item => item.Product.ProId == productDTO.ProId);
			if (existedPurchase != null)
			{
				MessageBox.Show("Xóa và thêm lại để cập nhật số lượng!", "Thông Báo", 
					MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			_dataList.Add(new OrderDetail.Data()
			{
				Product = productDTO,
				Purchase = purchareDTO,
			});

			// Cập nhật lại số lượng trên giao diện và số lượng trong danh sách
			_currentProduct.Quantity -= quantity;
			_products.First(product => product.ProId == _currentProduct.ProId).Quantity -= quantity;

			_currentTotalPrice += purchareDTO.TotalPrice;

			FinalPrice.Text = string.Format("{0:N0} đ", _currentTotalPrice);
		}

		private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var result = MessageBox.Show("Bạn có muốn xóa không?", "Thông Báo",
				MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.Yes)
			{
				int index = ordersListView.SelectedIndex; if (index == -1) return;
				_currentTotalPrice -= _dataList![index].Purchase.TotalPrice;

				// Tăng lại số lượng sản phẩn khi xóa khỏi danh sách mua
				var deleteProduct = _products.First(product => product.ProId == _dataList![index].Purchase.ProID);
				deleteProduct.Quantity += _dataList![index].Purchase.Quantity;
				// Cập nhật lại số lượng lên giao diện
				if (deleteProduct.ProId == _currentProduct.ProId)
				{
					_currentProduct.copy(deleteProduct);
				}

				FinalPrice.Text = string.Format("{0:N0} đ", _currentTotalPrice);
				_dataList.RemoveAt(index);
			}
		}

		private void SaveOrder_Click(object sender, RoutedEventArgs e)
		{
			if (_dataList!.Count > 0)
			{
				var customerDTO = (CustomerDTO)CustomerCombobox.SelectedValue;
				var shopOrderDTO = _orderInfo!.Order;

				shopOrderDTO.CusID = customerDTO.CusID;
				shopOrderDTO.FinalTotal = _currentTotalPrice;
				shopOrderDTO.ProfitTotal = _orderBUS.calOrderProfit(_currentTotalPrice);

				_orderBUS.updateShopOrder(shopOrderDTO);

				int orderID = shopOrderDTO.OrderID;

				// Xóa các purchase cũ đồng thời cập nhật lại quantity của product
				_orderBUS.deleteOrderPurchase(orderID);

				// thay các purchase mới vào
				foreach (var item in _dataList)
				{
					// lúc mà thêm purchase thì ở DAO đã xóa số lượng bên product luôn rồi !
					item.Purchase.OrderID = orderID;
					_orderBUS.addPurchase(item.Purchase);
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
	}
}
