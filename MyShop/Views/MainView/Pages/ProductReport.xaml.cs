using LiveCharts.Wpf;
using MyShop.BUS;
using MyShop.DTO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.ObjectModel;
using LiveCharts;

namespace MyShop.Views.MainView.Pages
{
	/// <summary>
	/// Interaction logic for ProductReport.xaml
	/// </summary>
	public partial class ProductReport : Page
	{
		private enum Mode
		{
			Year,
			Month,
			Week,
			Date
		}

		private ReportBUS _reportBUS;
		private ProductBUS _productBUS;

		private int _currentYear;
		private ProductDTO _currentProduct;

		private Frame _pageNavigation;
		Mode currentMode = Mode.Year;

		public ProductReport(Frame pageNavigation)
		{
			_reportBUS = new ReportBUS();
			_productBUS = new ProductBUS();
			_pageNavigation = pageNavigation;
			InitializeComponent();
		}

		private async void Page_Loaded(object sender, RoutedEventArgs e)
		{
			int currentYear = DateTime.Now.Year;
			for (int year = currentYear - 2; year <= currentYear; year++)
			{
				var item = new ComboBoxItem();
				item.Content = $"Năm {year.ToString()}";
				YearCombobox.Items.Add(item);
			}

			chart.AxisY.Add(new Axis
			{
				Foreground = Brushes.Black,
				Title = "Số lượng đã bán",
				MinValue = 0
			});
			Title.Text = "Đang hiển thị chế độ xem theo năm";

			ObservableCollection<ProductDTO> products = await _productBUS.getAll();

			ProductsCombobox.ItemsSource = products;
			ProductsCombobox.SelectedIndex = 0;

			_currentProduct = (ProductDTO)ProductsCombobox.SelectedValue;
			displayYearMode(_currentProduct);
		}

		private void displayYearMode(ProductDTO product)
		{
			var quantities = _reportBUS.groupQuantityOfProductByYear(product);

			var valuesColChart = new ChartValues<int>(quantities);

			chart.Series = new SeriesCollection();
			chart.AxisX = new AxesCollection();

			chart.Series.Add(new ColumnSeries()
			{
				Fill = Brushes.MediumBlue,
				Title = "Số lượng đã bán theo năm",
				Values = valuesColChart
			});

			var currentYear = DateTime.Now.Year;
			chart.AxisX.Add(
				new Axis()
				{
					Foreground = Brushes.Black,
					Title = "Năm",
					Labels = new string[] {
						$"{currentYear - 2}",
						$"{currentYear - 1}",
						$"{currentYear}",
					}
				});
			Title.Text = "Đang hiển thị chế độ xem theo năm";
			currentMode = Mode.Year;
		}

		private void displayMonthMode(ProductDTO product, int year)
		{
			var quantities = _reportBUS.groupQuantityOfProductByMonth(product, year);

			var valuesColChart = new ChartValues<int>(quantities);

			chart.Series = new SeriesCollection();
			chart.AxisX = new AxesCollection();

			chart.Series.Add(new ColumnSeries()
			{
				Fill = Brushes.MediumBlue,
				Title = "Số lượng đã bán theo tháng",
				Values = valuesColChart
			});

			chart.AxisX.Add(
				new Axis()
				{
					Foreground = Brushes.Black,
					Title = "Tháng",
					Labels = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12",
					}
				});
			Title.Text = "Đang hiển thị chế độ xem theo tháng";
			MonthCombobox.IsEnabled = true;
			MonthCombobox.SelectedIndex = 0;
			currentMode = Mode.Month;
		}

		private void displayWeekMode(ProductDTO product, int month, int year)
		{
			var quantities = _reportBUS.groupQuantityOfProductByWeek(product, year, month);

			var valuesColChart = new ChartValues<int>(quantities);

			chart.Series = new SeriesCollection();
			chart.AxisX = new AxesCollection();

			chart.Series.Add(new ColumnSeries()
			{
				Fill = Brushes.MediumBlue,
				Title = "Số lượng đã bán theo tuần",
				Values = valuesColChart
			});

			chart.AxisX.Add(
				new Axis()
				{
					Foreground = Brushes.Black,
					Title = "Tuần",
					Labels = new string[] { "1", "2", "3", "4", "5",
					}
				});
			Title.Text = "Đang hiển thị chế độ xem theo tuần";
			currentMode = Mode.Week;
		}

		private void displayDateMode(ProductDTO product, DateTime startDate, DateTime endDate)
		{
			var pricesByDate = _reportBUS.groupQuantityOfProductByDate(product, startDate, endDate);

			var valuesColChart = new ChartValues<int>(pricesByDate);

			chart.Series = new SeriesCollection();
			chart.AxisX = new AxesCollection();

			chart.Series.Add(new ColumnSeries()
			{
				Fill = Brushes.MediumBlue,
				Title = "Số lượng đã bán theo ngày",
				Values = valuesColChart
			});

			chart.AxisX.Add(
				new Axis()
				{
					Foreground = Brushes.Black,
					Title = "Date",
					Labels = _reportBUS.EachDayConverter(startDate, endDate)
				});
			Title.Text = "Đang hiển thị chế độ xem theo ngày";
			currentMode = Mode.Date;
		}

		private void YearCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int index = YearCombobox.SelectedIndex;
			var currentYear = DateTime.Now.Year;
			if (index == 1)
			{
				displayMonthMode(_currentProduct, currentYear - 2);
				_currentYear = currentYear - 2;
			}
			else if (index == 2)
			{
				displayMonthMode(_currentProduct, currentYear - 1);
				_currentYear = currentYear - 1;
			}
			else if (index == 3)
			{
				displayMonthMode(_currentProduct, currentYear);
				_currentYear = currentYear;
			}
		}

		private void MonthCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int index = MonthCombobox.SelectedIndex;
			if (index > 0)
			{
				displayWeekMode(_currentProduct, index, _currentYear);
			}
		}

		private void ProductsCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			_currentProduct = (ProductDTO) ProductsCombobox.SelectedValue;
			if (currentMode == Mode.Year)
			{
				displayYearMode(_currentProduct);
			}
			else if (currentMode == Mode.Month)
			{
				int index = YearCombobox.SelectedIndex;
				var currentYear = DateTime.Now.Year;
				if (index == 1)
				{
					displayMonthMode(_currentProduct, currentYear - 2);
					_currentYear = currentYear - 2;
				}
				else if (index == 2)
				{
					displayMonthMode(_currentProduct, currentYear - 1);
					_currentYear = currentYear - 1;
				}
				else if (index == 3)
				{
					displayMonthMode(_currentProduct, currentYear);
					_currentYear = currentYear;
				}
			}
			else if (currentMode == Mode.Week)
			{
				int index = MonthCombobox.SelectedIndex;
				if (index > 0)
				{
					displayWeekMode(_currentProduct, index, _currentYear);
				}
			}
			else if (currentMode == Mode.Date)
			{
				var startDate = StartDate.SelectedDate;
				var endDate = EndDate.SelectedDate;

				if (startDate == null || endDate == null)
				{
					MessageBox.Show("Vui lòng chọn đủ ngày bắt đầu và kết thúc!", "Thông báo",
						MessageBoxButton.OK, MessageBoxImage.Warning);
				}
				else
				{
					YearCombobox.SelectedIndex = 0;
					MonthCombobox.SelectedIndex = 0;
					displayDateMode(_currentProduct, (DateTime)startDate, (DateTime)endDate);
				}
			}
		}

		private void Search_Click(object sender, RoutedEventArgs e)
		{
			var startDate = StartDate.SelectedDate;
			var endDate = EndDate.SelectedDate;

			if (startDate == null || endDate == null)
			{
				MessageBox.Show("Vui lòng chọn đủ ngày bắt đầu và kết thúc!", "Thông báo",
					MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			else
			{
				YearCombobox.SelectedIndex = 0;
				MonthCombobox.SelectedIndex = 0;
				displayDateMode(_currentProduct, (DateTime)startDate, (DateTime)endDate);
			}
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			_pageNavigation.NavigationService.Navigate(new RevenueReport(_pageNavigation));
			//_pageNavigation.NavigationService.GoBack();
		}
	}
}
