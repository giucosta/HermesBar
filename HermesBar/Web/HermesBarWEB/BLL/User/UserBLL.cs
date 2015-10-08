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
                model.PerfilSigla = user.DSC;
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
