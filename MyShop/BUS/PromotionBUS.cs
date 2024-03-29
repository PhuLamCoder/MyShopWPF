﻿using MyShop.DAO;
using MyShop.DTO;
using System.Collections.ObjectModel;

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

		public ObservableCollection<PromotionDTO> getAll()
		{
			return _PromotionDAO.getAll();
		}

		public int addPromotion(PromotionDTO promotion)
		{
			return _PromotionDAO.insertPromo(promotion);
		}

		public Tuple<Boolean, string> delPromotionById(int idPromo)
		{
			return _PromotionDAO.delPromoById(idPromo);
		}

		public void updatePromotion(PromotionDTO promotion)
		{
			_PromotionDAO.updatePromo(promotion);
		}
	}
}
