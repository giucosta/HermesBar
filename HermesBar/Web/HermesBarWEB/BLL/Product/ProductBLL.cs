using DAO.Product;
using MODEL.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.UTIL;
using ENTITY.Product;
using MODEL.User;

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
        private TypeBLL _typeBLL = null;
        private TypeBLL TypeBLL
        {
            get
            {
                if (_typeBLL == null)
                    _typeBLL = new TypeBLL();
                return _typeBLL;
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
        public ProdutoModel GetId(int id)
        {
            var entity = new HMA_PROD() { _ID = id };
            var result = ProductDAO.GetId(entity).DataTableToList<HMA_PROD>().FirstOrDefault();

            if (result != null)
                return ConvertEntityToModel(result);
            return new ProdutoModel();
        }
        public bool Insert(ProdutoModel produto, UsuarioModel user)
        {
            try
            {
                var prod = ConvertModelToEntity(produto, user);
                if (ProductDAO.VerifyExistingProduct(prod).Rows.Count == 0)
                {
                    var tipo = new HMA_TIP() { _ID = Convert.ToInt32(produto.TipoSelected) };
                    var unidade = new HMA_UNI_MED() { _ID = Convert.ToInt32(produto.UnidadeMedidaSelected) };

                    return Convert.ToInt32(ProductDAO.Insert(prod, tipo, unidade).Rows[0]["SUCCESS"]) == 1;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Update(ProdutoModel produto, UsuarioModel user)
        {
            try
            {
                var prod = ConvertModelToEntity(produto, user);
                var tipo = new HMA_TIP() { _ID = Convert.ToInt32(produto.TipoSelected) };
                var unidade = new HMA_UNI_MED() { _ID = Convert.ToInt32(produto.UnidadeMedidaSelected) };

                return Convert.ToInt32(ProductDAO.Update(prod, tipo, unidade).Rows[0]["SUCCESS"]) == 1;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Active(ProdutoModel produto, UsuarioModel user)
        {
            return Convert.ToInt32(ProductDAO.Active(ConvertModelToEntity(produto, user)).Rows[0]["SUCCESS"]) == 1;
        }
        public bool Inactive(ProdutoModel produto, UsuarioModel user)
        {
            return Convert.ToInt32(ProductDAO.Inactive(ConvertModelToEntity(produto, user)).Rows[0]["SUCCESS"]) == 1;
        }
        public int GetNextCode()
        {
            try
            {
                return Convert.ToInt32(ProductDAO.GetNextCod().Rows[0]["COD_VEN"]);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Private Methods
        private ProdutoModel ConvertEntityToModel(HMA_PROD entity)
        {
            try
            {
                var model = new ProdutoModel();
                model.CodigoVenda = entity.COD_VEN;
                model.StatusSelected = entity._ATV.ToString();
                model.Descricao = entity.DSC;
                model.Nome = entity.NOM;
                model.Id = entity._ID;
                model.ValorCompra = Convert.ToDouble(entity.VLR_COM);
                model.ValorVenda = Convert.ToDouble(entity.VLR_VEN);
                if (string.IsNullOrEmpty(entity.MED_NOM))
                {
                    model.UnidadeMedidaSelected = entity.MED_ID.ToString();
                    model.TipoSelected = entity.TIP_ID.ToString();
                }
                else
                {
                    model.UnidadeMedidaSelected = entity.MED_NOM;
                    model.TipoSelected = entity.TIP_NOM;
                }
                model.QuantidadeAtual = Convert.ToDouble(entity.QUANT_ATL);
                model.QuantidadeMinimaAviso = entity.QUANT_MIN;
                
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private HMA_PROD ConvertModelToEntity(ProdutoModel model, UsuarioModel user)
        {
            try
            {
                var prod = new HMA_PROD();
                prod._ID = model.Id;
                prod._ATV = Convert.ToInt32(model.StatusSelected);
                prod._USR = user.Id;
                prod.COD_VEN = model.CodigoVenda;
                prod.DSC = model.Descricao;
                prod.NOM = model.Nome;
                prod.VLR_COM = Convert.ToDecimal(model.ValorCompra);
                prod.VLR_VEN = Convert.ToDecimal(model.ValorVenda);
                prod.QUANT_MIN = model.QuantidadeMinimaAviso;
                prod.QUANT_ATL = Convert.ToDecimal(model.QuantidadeAtual);
                
                return prod;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
