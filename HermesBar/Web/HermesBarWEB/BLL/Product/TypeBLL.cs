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
using HELPER;

namespace BLL.Product
{
    public class TypeBLL
    {
        #region Singleton
        private TypeDAO TypeDAO = Singleton<TypeDAO>.Instance();
        #endregion

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
            catch (Exception)
            {
                throw;
            }
        }
        public TipoModel GetId(TipoModel tipo, UsuarioModel user)
        {
            try
            {
                return ConvertEntityToModel(TypeDAO.GetId(ConvertModelToEntity(tipo, user)).DataTableToList<HMA_TIP>().FirstOrDefault());
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Insert(TipoModel tipo, UsuarioModel user)
        {
            try
            {
                if (string.IsNullOrEmpty(tipo.Descricao))
                    tipo.Descricao = "";
                return TypeDAO.Insert(ConvertModelToEntity(tipo, user)).GetResults();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Update(TipoModel tipo, UsuarioModel user)
        {
            try
            {
                return TypeDAO.Update(ConvertModelToEntity(tipo, user)).GetResults();
            }
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
            }
        }
        internal TipoModel ConvertEntityToModel(HMA_TIP tipo)
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
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
