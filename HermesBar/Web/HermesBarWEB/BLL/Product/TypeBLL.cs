using DAO.Product;
using ENTITY.Product;
using MODEL.Product;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.UTIL;

namespace BLL.Product
{
    public class TypeBLL
    {
        private TypeDAO _typeDAO = null;
        private TypeDAO TypeDAO
        {
            get
            {
                if (_typeDAO == null)
                    _typeDAO = new TypeDAO();
                return _typeDAO;
            }
        }

        public List<TipoModel> Get()
        {
            try
            {
                var result = TypeDAO.Get().DataTableToList<HMA_TIP>();
                if(result.Count > 0)
                {
                    var list = new List<TipoModel>();
                    foreach (var item in result)
                        list.Add(ConvertEntityToModel(item));

                    return list;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TipoModel GetId(TipoModel tipo, UsuarioModel user)
        {
            try
            {
                return ConvertEntityToModel(TypeDAO.GetId(ConvertModelToEntity(tipo, user)).DataTableToList<HMA_TIP>().FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Insert(TipoModel tipo, UsuarioModel user)
        {
            try
            {
                if (string.IsNullOrEmpty(tipo.Descricao))
                    tipo.Descricao = "";
                return Convert.ToInt32(TypeDAO.Insert(ConvertModelToEntity(tipo, user)).Rows[0]["SUCCESS"]) == 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Update(TipoModel tipo, UsuarioModel user)
        {
            try
            {
                return Convert.ToInt32(TypeDAO.Update(ConvertModelToEntity(tipo, user)).Rows[0]["SUCCESS"]) == 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Private Methods
        private HMA_TIP ConvertModelToEntity(TipoModel tipo, UsuarioModel user)
        {
            try
            {
                var entity = new HMA_TIP();
                entity._ATV = Convert.ToInt32(tipo.StatusSelected);
                entity._USR = user.Id;
                entity.DSC = tipo.Descricao;
                entity.NOM = tipo.Nome;
                entity._ID = tipo.Id;

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private TipoModel ConvertEntityToModel(HMA_TIP tipo)
        {
            try
            {
                var model = new TipoModel();
                model.Nome = tipo.NOM;
                model.StatusSelected = tipo._ATV.ToString();
                model.Descricao = tipo.DSC;
                model.Id = tipo._ID;

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
