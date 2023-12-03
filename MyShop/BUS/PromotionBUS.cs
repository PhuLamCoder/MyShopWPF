using MyShop.DAO;
using MyShop.DTO;

namespace MyShop.BUS
{
	public class PromotionBUS
	{
		private PromotionDAO _PromotionDAO;

		public PromotionBUS()
		{
			_PromotionDAO = new PromotionDAO();
		}

		public PromotionDTO getPromotionById(int id)
		{
			return _PromotionDAO.getPromoById(id);
		}
	}
}
