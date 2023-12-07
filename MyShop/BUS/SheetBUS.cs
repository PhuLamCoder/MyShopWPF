using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MyShop.DTO;
using System.Text;
using System.Windows;

namespace MyShop.BUS
{
	class SheetBUS
	{
		enum Product
		{
			ProName,
			Ram,
			Rom,
			ScreenSize,
			Desc,
			Price,
			Trademark,
			BatteryCapacity,
			CatID,
			Quantity
		}

		public List<ProductDTO>? ReadExcelFile(string filePath)
		{
			List<ProductDTO> result = new List<ProductDTO>();
			try
			{
				using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(filePath, false))
				{
					WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart!;
					Sheet sheet = workbookPart.Workbook.Sheets!.GetFirstChild<Sheet>()!;

					WorksheetPart worksheetPart = (WorksheetPart)(workbookPart.GetPartById(sheet.Id!));

					SharedStringTablePart sharedStringTablePart;
					if (workbookPart.GetPartsOfType<SharedStringTablePart>().Count() > 0)
					{
						sharedStringTablePart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
					}
					else
					{
						return null;
					}

					IEnumerable<Row> rows = worksheetPart.Worksheet.Descendants<Row>();

					foreach (Row row in rows)
					{
						int columnIndex = 0;
						ProductDTO productDTO = new ProductDTO();
						foreach (Cell cell in row.Descendants<Cell>())
						{
							string cellValue = cell.InnerText;

							if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
							{
								int sharedStringIndex = int.Parse(cellValue);
								cellValue = sharedStringTablePart.SharedStringTable.ElementAt(sharedStringIndex).InnerText;
							}
							else if (cell.DataType != null && cell.DataType.Value == CellValues.String && cell.CellValue != null)
							{
								byte[] bytes = Encoding.UTF8.GetBytes(cell.CellValue.Text);
								cellValue = Encoding.UTF8.GetString(bytes);
							}

							if (columnIndex == (int)Product.ProName)
							{
								productDTO.ProName = cellValue;
							}
							else if (columnIndex == (int)Product.Ram)
							{
								productDTO.Ram = int.Parse(cellValue);
							}
							else if (columnIndex == (int)Product.Rom)
							{
								productDTO.Rom = int.Parse(cellValue);
							}
							else if (columnIndex == (int)Product.CatID)
							{
								productDTO.CatID = int.Parse(cellValue);
							}
							else if (columnIndex == (int)Product.BatteryCapacity)
							{
								productDTO.BatteryCapacity = int.Parse(cellValue);
							}
							else if (columnIndex == (int)Product.Quantity)
							{
								productDTO.Quantity = int.Parse(cellValue);
							}
							else if (columnIndex == (int)Product.Trademark)
							{
								productDTO.Trademark = cellValue;
							}
							else if (columnIndex == (int)Product.ScreenSize)
							{
								productDTO.ScreenSize = double.Parse(cellValue);
							}
							else if (columnIndex == (int)Product.Desc)
							{
								productDTO.Description = cellValue;
							}
							else if (columnIndex == (int)Product.Price)
							{
								productDTO.Price = Decimal.Parse(cellValue);
							}
							columnIndex++;
						}
						result.Add(productDTO);
					}
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Lỗi xải ra trong quá trình import. Nếu bạn đang mở file excel, vui lòng tắt nó đi!", 
					"Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
			}

			return result;
		}
	}
}
