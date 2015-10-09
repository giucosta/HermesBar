using DAO.User;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.UTIL;
using ENTITY.User;

namespace BLL.User
{
    public class UserBLL
    {
        #region Singleton
        private UserDAO _usuarioDAO = null;
        private UserDAO UsuarioDAO
        {
            get
            {
                if (_usuarioDAO == null)
                    _usuarioDAO = new UserDAO();
                return _usuarioDAO;
            }
        }
        #endregion
        public List<UsuarioModel> Get(UsuarioModel user)
        {
            try
            {
                var list = new List<UsuarioModel>();

                foreach (var item in UsuarioDAO.Get(ConvertModelToEntity(user)).DataTableToList<HMA_USR>())
                    list.Add(ConvertEntityToModel(item));

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Insert(UsuarioModel user)
        {
            try
            {
                return UsuarioDAO.Insert(ConvertModelToEntity(user)).GetResults();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Update(UsuarioModel user)
        {
            try
            {
                return UsuarioDAO.Update(ConvertModelToEntity(user)).GetResults();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Inactive(UsuarioModel user)
        {
            try
            {
                return UsuarioDAO.Inactive(ConvertModelToEntity(user)).GetResults();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Active(UsuarioModel user)
        {
            try
            {
                return UsuarioDAO.Active(ConvertModelToEntity(user)).GetResults();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Private Methods
        private HMA_USR ConvertModelToEntity(UsuarioModel user)
        {
            try
            {
                var ent = new HMA_USR();
                ent._ID = user.Id;
                ent.NOM = user.Nome;
                ent.PAS = user.Senha;
                ent._ATV = Convert.ToInt32(user.StatusSelected);
                if(!string.IsNullOrEmpty(user.PerfilSelected))
                    ent.PER_ID = Convert.ToInt32(user.PerfilSelected);
                ent.EMA = user.Email;

                return ent;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private UsuarioModel ConvertEntityToModel(HMA_USR user)
        {
            try
            {
                var model = new UsuarioModel();
                model.Id = user._ID;
                model.Email = user.EMA;
                model.PerfilSigla = user.SIG;
                model.PerfilDescricao = user.DSC;
                model.Nome = user.NOM;
                model.StatusSelected = user._ATV.ToString();

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
