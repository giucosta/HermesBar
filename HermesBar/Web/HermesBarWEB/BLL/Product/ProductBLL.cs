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
using HELPER;

namespace BLL.Product
{
    public class ProductBLL
    {
        #region Singleton
        private ProductDAO ProductDAO = Singleton<ProductDAO>.Instance();
        private TypeBLL TypeBLL = Singleton<TypeBLL>.Instance();
        #endregion

        public List<ProdutoModel> Get()
        {
            try
            {
                var listModel = new List<ProdutoModel>();
                foreach (var item in ProductDAO.Get().DataTableToList<HMA_PROD>())
                    listModel.Add(ConvertEntityToModel(item));

                return listModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ProdutoModel GetId(int id, int ativo)
        {
            try
            {
                var entity = new HMA_PROD() { _ID = id, _ATV = ativo };
                var result = ProductDAO.GetId(entity).DataTableToList<HMA_PROD>().FirstOrDefault();

                if (result != null)
                    return ConvertEntityToModel(result);
                return new ProdutoModel();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Insert(ProdutoModel produto, UsuarioModel user)
        {
            try
            {
                HMA_PROD product;
                HMA_TIP productType;
                HMA_UNI_MED unity;

                LoadModels(produto, user, out product, out productType, out unity);

                if (ProductDAO.VerifyExistingProduct(product).Rows.Count == 0)
                    return ProductDAO.Insert(product, productType, unity).GetResults();

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
                HMA_PROD product;
                HMA_TIP productType;
                HMA_UNI_MED unity;

                LoadModels(produto, user, out product, out productType, out unity);

                return ProductDAO.Update(product, productType, unity).GetResults();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Active(ProdutoModel produto, UsuarioModel user)
        {
            return ProductDAO.Active(ConvertModelToEntity(produto, user)).GetResults();
        }
        public bool Inactive(ProdutoModel produto, UsuarioModel user)
        {
            return ProductDAO.Inactive(ConvertModelToEntity(produto, user)).GetResults();
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
        public List<ProdutoModel> GetLow()
        {
            try
            {
                var result = ProductDAO.LowProducts().DataTableToList<HMA_PROD>();
                if (result.Count() > 0)
                {
                    var list = new List<ProdutoModel>();
                    foreach (var item in result)
                        list.Add(ConvertEntityToModel(item));
                    return list;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Private Methods
        private void LoadModels(ProdutoModel produto, UsuarioModel user, out HMA_PROD prod, out HMA_TIP tipo, out HMA_UNI_MED unidade)
        {
            prod = ConvertModelToEntity(produto, user);
            tipo = new HMA_TIP() { _ID = Convert.ToInt32(produto.TipoSelected) };
            unidade = new HMA_UNI_MED() { _ID = Convert.ToInt32(produto.UnidadeMedidaSelected) };
        }
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
