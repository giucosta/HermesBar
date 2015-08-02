using DAO.Product;
using MODEL.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.UTIL;
using ENTITY.Product;

namespace BLL.Product
{
    public class ProductBLL
    {
        private ProductDAO _productDAO = null;
        private ProductDAO ProductDAO
        {
            get
            {
                if (_productDAO == null)
                    _productDAO = new ProductDAO();
                return _productDAO;
            }
        }

        public List<ProdutoModel> Get()
        {
            try
            {
                var listModel = new List<ProdutoModel>();
                foreach (var item in ProductDAO.Get().DataTableToList<HMA_PROD>())
                    listModel.Add(ConvertEntityToModel(item));

                return listModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Private Methods
        private ProdutoModel ConvertEntityToModel(HMA_PROD entity)
        {
            try
            {
                var model = new ProdutoModel();
                model.CodigoVenda = entity.COD_VEN;
                model.Descricao = entity.DSC;
                model.Nome = entity.NOM;
                model.Id = entity._ID;
                model.ValorCompra = Convert.ToDouble(entity.VLR_COM);
                model.ValorVenda = Convert.ToDouble(entity.VLR_VEN);

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
