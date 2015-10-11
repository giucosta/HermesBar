using DAO.ShoppingList;
using ENTITY.ShoppingList;
using HELPER;
using MODEL.ListaCompras;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.UTIL;

namespace BLL.ShoppingList
{
    public class ShoppingListBLL
    {
        #region Singleton
        private ShoppingListDAO ShoppingListDAO = Singleton<ShoppingListDAO>.Instance();
        #endregion

        public bool Insert(List<ListaComprasModel> shoppingListModel, UsuarioModel user)
        {
            try
            {
                var nextId = 0;
                var nextIdBase = ShoppingListDAO.GetNextId().Rows[0]["ID"];

                if (nextIdBase != DBNull.Value)
                    nextId = Convert.ToInt32(nextIdBase);

                bool verificador = false;

                foreach (var item in shoppingListModel)
                {
                    verificador = ShoppingListDAO.Insert(ConvertModelToEntity(item, user, nextId)).GetResults();
                    if (verificador)
                        continue;
                    else
                        return verificador;
                }
                return verificador;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ListaComprasModel> Get()
        {
            try
            {
                var result = ShoppingListDAO.Get().DataTableToList<HMA_LIS_COM>();
                var listModel = new List<ListaComprasModel>();
                int id = -1;
                foreach (var item in result)
                {
                    if (id != item._ID_LIS)
                    {
                        listModel.Add(ConvertEntityToModel(item));
                        id = item._ID_LIS;
                    }
                }
                return listModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Private Methods
        private HMA_LIS_COM ConvertModelToEntity(ListaComprasModel model, UsuarioModel user, int idList)
        {
            try
            {
                var entity = new HMA_LIS_COM();
                entity._ID_PROD = Convert.ToInt32(model.id);
                entity.QUANT = Convert.ToInt32(model.quantidade);
                entity._USR = user.Id;
                entity._ID_LIS = idList;

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private ListaComprasModel ConvertEntityToModel(HMA_LIS_COM entity)
        {
            try
            {
                var model = new ListaComprasModel();
                model.IdLista = entity._ID_LIS;
                model.DataCriacao = entity._INS;

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
