using MyShop.DAO;
using MyShop.DTO;
using System.Collections.ObjectModel;

namespace MyShop.BUS
{
	public class ProductBUS
	{
		private ProductDAO _productDAO;

		public ProductBUS()
		{
			_productDAO = new ProductDAO();
		}


		public async Task<int> countTotalProduct()
		{
			return await _productDAO.countTotalProduct();
		}


		public async Task<ObservableCollection<ProductDTO>> getTop5Product()
		{
			return await _productDAO.getTop5Product();
		}


		public async Task<Tuple<List<ProductDTO>, int>> findProductBySearch(int currentPage = 1, int productsPerPage = 9,
				string keyword = "", Decimal? startPrice = null, Decimal? endPrice = null, string? orderBy = null, bool asc = true)
		{
			var origin = await _productDAO.getAll();
			var sortedList = origin.ToList<ProductDTO>();

			if (orderBy != null)
			{
                if (orderBy.ToLower() == "price")
                {
                    if (asc)
					{
						sortedList = origin.OrderBy(product => product.PromotionPrice).ToList<ProductDTO>();
					} else
					{
						sortedList = origin.OrderByDescending(product => product.PromotionPrice).ToList<ProductDTO>();
					}
                } else if (orderBy.ToLower() == "name")
				{
					if (asc)
					{
						sortedList = origin.OrderBy(product => product.ProName).ToList<ProductDTO>();
					}
					else
					{
						sortedList = origin.OrderByDescending(product => product.ProName).ToList<ProductDTO>();
					}
				}
			}

			// TODO: nên handle việc ProName bị null ở đây .
			// 
			var list = sortedList.Where((item) => {
				bool checkName = item.ProName.ToLower().Contains(keyword.ToLower());

				if (startPrice == null || endPrice == null) return checkName;

				bool checkPrice = item.Price >= startPrice && item.Price <= endPrice;

				return checkName && checkPrice;
			});

			var items = list.Skip((currentPage - 1) * productsPerPage).Take(productsPerPage);
			var result = new Tuple<List<ProductDTO>, int>(items.ToList(), list.Count());
			return result;
		}

		public void delProduct(int id)
		{
			_productDAO.deleteProductById(id);
		}

		public void updateProduct(ProductDTO product)
		{
			_productDAO.updateProduct(product);
		}
	}
}
