using LiveCharts;
using LiveCharts.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MyShop.BUS;

namespace MyShop.Views.MainView.Pages
{
	/// <summary>
	/// Interaction logic for RevenueReport.xaml
	/// </summary>
	public partial class RevenueReport : Page
	{
		private ReportBUS _reportBUS;
		private CartesianChart _chart;
		private int _currentYear;
		private Frame _pageNavigation;

		public RevenueReport(Frame pageNavigation)
		{
			_reportBUS = new ReportBUS();
			_pageNavigation = pageNavigation;
			InitializeComponent();

			_chart = chart;

			_chart.AxisY.Add(new Axis
			{
				Foreground = Brushes.Black,
				Title = "VNĐ",
				MinValue = 0,
				LabelFormatter = value => value.ToString("N0")
			});
			Title.Text = "Đang hiển thị chế độ xem theo năm";
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			displayYearMode();
			int currentYear = DateTime.Now.Year;
			for (int year = currentYear - 2; year <= currentYear; year++)
			{
				var item = new ComboBoxItem();
				item.Content = $"Năm {year.ToString()}";
				YearCombobox.Items.Add(item);
			}
		}

		private void displayYearMode()
		{
			(var revenueByYears, var profitByYears) = _reportBUS.groupRevenueAndProfitByYear();

			var valuesColChart = new ChartValues<double>(revenueByYears);
			var valuesLineChart = new ChartValues<double>(profitByYears);

			_chart.Series = new SeriesCollection();
			_chart.AxisX = new AxesCollection();

			_chart.Series.Add(new ColumnSeries()
			{
				Fill = Brushes.Chocolate,
				Title = "Doanh thu theo năm",
				Values = valuesColChart
			});

			_chart.Series.Add(new LineSeries()
			{
				Stroke = Brushes.DeepSkyBlue,
				Title = "Lợi nhuận theo năm",
				Values = valuesLineChart
			});

			var currentYear = DateTime.Now.Year;
			chart.AxisX.Add(new Axis()
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
		}

		private void displayMonthMode(int year)
		{
			(var revenueByMonths, var profitByMonths) = _reportBUS.groupRevenueAndProfitByMonth(year);

			var valuesColChart = new ChartValues<double>(revenueByMonths);
			var valuesLineChart = new ChartValues<double>(profitByMonths);

			_chart.Series = new SeriesCollection();
			_chart.AxisX = new AxesCollection();

			_chart.Series.Add(new ColumnSeries()
			{
				Fill = Brushes.Chocolate,
				Title = "Doanh thu theo tháng",
				Values = valuesColChart
			});

			_chart.Series.Add(new LineSeries()
			{
				Stroke = Brushes.DeepSkyBlue,
				Title = "Lợi nhuận theo tháng",
				Values = valuesLineChart
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
		}

		private void displayWeekMode(int month, int year)
		{
			(var revenueByWeeks, var profitByWeeks) = _reportBUS.groupRevenueAndProfitByWeek(month, year);

			var valuesColChart = new ChartValues<double>(revenueByWeeks);
			var valuesLineChart = new ChartValues<double>(profitByWeeks);

			_chart.Series = new SeriesCollection();
			_chart.AxisX = new AxesCollection();

			_chart.Series.Add(new ColumnSeries()
			{
				Fill = Brushes.Chocolate,
				Title = "Doanh thu theo tuần",
				Values = valuesColChart
			});

			_chart.Series.Add(new LineSeries()
			{
				Stroke = Brushes.DeepSkyBlue,
				Title = "Lợi nhuận theo tuần",
				Values = valuesLineChart
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
		}

		private void displayDateMode(DateTime startDate, DateTime endDate)
		{
			(var revenueByDates, var profitByDates) = _reportBUS.groupRevenueAndProfitByDate(startDate, endDate);

			var valuesColChart = new ChartValues<double>(revenueByDates);
			var valuesLineChart = new ChartValues<double>(profitByDates);

			_chart.Series = new SeriesCollection();
			_chart.AxisX = new AxesCollection();

			_chart.Series.Add(new ColumnSeries()
			{
				Fill = Brushes.Chocolate,
				Title = "Doanh thu theo ngày",
				Values = valuesColChart
			});

			_chart.Series.Add(new LineSeries()
			{
				Stroke = Brushes.DeepSkyBlue,
				Title = "Lợi nhuận theo ngày",
				Values = valuesLineChart
			});

			chart.AxisX.Add(
				new Axis()
				{
					Foreground = Brushes.Black,
					Title = "Ngày",
					Labels = _reportBUS.EachDayConverter(startDate, endDate)
				});
			Title.Text = "Đang hiển thị chế độ xem theo ngày";
		}

		private void Search_Click(object sender, RoutedEventArgs e)
		{
			var startDate = StartDate.SelectedDate;
			var endDate = EndDate.SelectedDate;

			if (startDate == null || endDate == null)
			{
				MessageBox.Show("Vui lòng chọn đủ ngày bắt đầu và kết thúc!", "Thông báo",
					MessageBoxButton.OK, MessageBoxImage.Warning);
			} else
			{
				displayDateMode((DateTime)startDate, (DateTime)endDate);
			}
		}

		private void NextProductReport_Click(object sender, RoutedEventArgs e)
		{
			_pageNavigation.NavigationService.Navigate(new ProductReport(_pageNavigation));
		}

		private void MonthCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int index = MonthCombobox.SelectedIndex;
			if (index > 0)
			{
				displayWeekMode(index, _currentYear);
			}
		}

		private void YearCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int index = YearCombobox.SelectedIndex;
			var currentYear = DateTime.Now.Year;
			if (index == 1)
			{
				displayMonthMode(currentYear - 2);
				_currentYear = currentYear - 2;
			}
			else if (index == 2)
			{
				displayMonthMode(currentYear - 1);
				_currentYear = currentYear - 1;
			}
			else if (index == 3)
			{
				displayMonthMode(currentYear);
				_currentYear = currentYear;
			}
		}
	}
}
