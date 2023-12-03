﻿using Microsoft.Data.SqlClient;
using MyShop.DTO;
using System.Collections.ObjectModel;
using System.Data;

namespace MyShop.DAO
{
	public class ProductDAO
	{
		DatabaseUtilitites db = DatabaseUtilitites.getInstance();
		//private ObservableCollection<PromotionDTO> _promotion = (new PromotionDAO()).getAll();

		public async Task<ObservableCollection<ProductDTO>> getAll()
		{
			ObservableCollection<ProductDTO> list = new ObservableCollection<ProductDTO>();

			string sql = "SELECT * FROM product WHERE Block = 0";
			var command = new SqlCommand(sql, db.connection);
			var reader = command.ExecuteReader();

			while (reader.Read())
			{
				ProductDTO product = new ProductDTO();
				product.ProId = (int)reader["ProID"];
				product.ProName = reader["ProName"] == DBNull.Value ? "Lỗi tên sản phẩm" : (string?)reader["ProName"];
				product.Ram = (double)reader["Ram"];
				product.Rom = (int)reader["Rom"];
				product.ScreenSize = (double)reader["ScreenSize"];
				product.TinyDes = reader["TinyDes"] == DBNull.Value ? null : (string?)reader["TinyDes"];
				product.FullDes = reader["FullDes"] == DBNull.Value ? null : (string?)reader["FullDes"];
				product.Price = (decimal)reader["Price"];
				product.ImagePath = reader["ImagePath"] == DBNull.Value ? "Assets/Images/sp/404.png" : (string?)reader["ImagePath"];
				product.Trademark = reader["Trademark"] == DBNull.Value ? null : (string?)reader["Trademark"];
				product.BatteryCapacity = (int)reader["BatteryCapacity"];
				product.CatID = (int)reader["CatID"];
				product.Quantity = (int)reader["Quantity"];
				product.PromotionPrice = reader["PromotionPrice"] == DBNull.Value ? product.Price : (decimal?)reader["PromotionPrice"];
				product.PromoID = reader["PromoID"] == DBNull.Value ? null : (int?)reader["PromoID"];
				product.Block = (int)reader["Block"];
				list.Add(product);
			}
			reader.Close();
			
			return list;
		}


		public async Task<int> countTotalProduct()
		{
			int total = 0;
			await Task.Run(() => {
				string sql = $"SELECT COUNT(*) AS total FROM product WHERE Block = 0";
				var command = new SqlCommand(sql, db.connection);
				var reader = command.ExecuteReader();

				while (reader.Read()) {
					total = (int)reader["total"];
				}
				reader.Close();
			});
			return total;
		}

		public async Task<ObservableCollection<ProductDTO>> getTop5Product()
		{
			ObservableCollection<ProductDTO> list = new ObservableCollection<ProductDTO>();
			await Task.Run(() =>
			{
				string sql = "SELECT TOP 5 * FROM product WHERE Quantity <= 5 ORDER BY Quantity";
				var command = new SqlCommand(sql, db.connection);

				var reader = command.ExecuteReader();

				while (reader.Read())
				{
					ProductDTO product = new ProductDTO();
					product.ProId = (int)reader["ProID"];
					product.ProName = reader["ProName"] == DBNull.Value ? "Lỗi tên sản phẩm" : (string?)reader["ProName"];
					product.Ram = (double)reader["Ram"];
					product.Rom = (int)reader["Rom"];
					product.ScreenSize = (double)reader["ScreenSize"];
					product.TinyDes = reader["TinyDes"] == DBNull.Value ? null : (string?)reader["TinyDes"];
					product.FullDes = reader["FullDes"] == DBNull.Value ? null : (string?)reader["FullDes"];
					product.Price = (decimal)reader["Price"];
					product.ImagePath = reader["ImagePath"] == DBNull.Value ? null : (string?)reader["ImagePath"];
					product.Trademark = reader["Trademark"] == DBNull.Value ? null : (string?)reader["Trademark"];
					product.BatteryCapacity = (int)reader["BatteryCapacity"];
					product.CatID = (int)reader["CatID"];
					product.Quantity = (int)reader["Quantity"];
					product.PromoID = reader["PromoID"] == DBNull.Value ? null : (int?)reader["PromoID"];
					product.PromotionPrice = reader["PromotionPrice"] == DBNull.Value ? (decimal)reader["Price"] : (decimal?)reader["PromotionPrice"];
					product.Block = (int)reader["Block"];

					list.Add(product);
				}
				reader.Close();
			});

			return list;
		}

		// Soft delete
		public void deleteProductById(int ProId)
		{
			string sql = $"""
                UPDATE product 
                SET Block = {1}
                WHERE ProID = {ProId}
                """;

			var command = new SqlCommand(sql, db.connection);
			command.ExecuteNonQuery();
		}

		public void updateProduct(ProductDTO productDTO)
		{
			string sql = "update product " +
				"set ProName =  @ProName, Ram = @Ram, Rom = @Rom, ScreenSize = @ScreenSize, TinyDes = @TinyDes," +
				"Price = @Price, Trademark = @Trademark, BatteryCapacity = @BatteryCapacity," +
				"CatID = @CatID, Quantity = @Quantity, PromoID = @PromoID, PromotionPrice = @PromotionPrice, Block = @Block " +
				"where ProID = @ProID";

			string a = """
				UPDATE product
				SET ProName =  @ProName, 
					Ram = @Ram, 
					Rom = @Rom, 
					ScreenSize = @ScreenSize, 
					TinyDes = @TinyDes,
					Price = @Price, 
					Trademark = @Trademark, 
					BatteryCapacity = @BatteryCapacity,
					CatID = @CatID, 
					Quantity = @Quantity, 
					PromoID = @PromoID, 
					PromotionPrice = @PromotionPrice, 
					Block = @Block
				WHERE ProID = @ProID
				""";

			var command = new SqlCommand(sql, db.connection);

			command.Parameters.Add("@ProID", SqlDbType.Int).Value = productDTO.ProId;
			command.Parameters.Add("@ProName", SqlDbType.NVarChar).Value = productDTO.ProName;
			command.Parameters.Add("@Ram", SqlDbType.Float).Value = productDTO.Ram;
			command.Parameters.Add("@Rom", SqlDbType.Int).Value = productDTO.Rom;
			command.Parameters.Add("@ScreenSize", SqlDbType.Float).Value = productDTO.ScreenSize;
			command.Parameters.Add("@TinyDes", SqlDbType.NVarChar).Value = productDTO.TinyDes;
			command.Parameters.Add("@Price", SqlDbType.Money).Value = productDTO.Price;
			command.Parameters.Add("@Trademark", SqlDbType.Text).Value = productDTO.Trademark;
			command.Parameters.Add("@BatteryCapacity", SqlDbType.Int).Value = productDTO.BatteryCapacity;
			command.Parameters.Add("@CatID", SqlDbType.Int).Value = productDTO.CatID;
			command.Parameters.Add("@Quantity", SqlDbType.Int).Value = productDTO.Quantity;
			command.Parameters.Add("@PromotionPrice", SqlDbType.Money).Value = productDTO.PromotionPrice == null ? productDTO.Price : productDTO.PromotionPrice;
			command.Parameters.Add("@PromoID", SqlDbType.Int).Value = productDTO.PromoID == null ? DBNull.Value : productDTO.PromoID;
			command.Parameters.Add("@Block", SqlDbType.Int).Value = productDTO.Block;

			command.ExecuteNonQuery();
		}
	}
}
