using DAO.User;
using ENTITY.User;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.UTIL;

namespace BLL.User
{
    public class LoginBLL
    {
        private LoginDAO _loginDAO = null;
        private LoginDAO LoginDAO
        {
            get
            {
                if (_loginDAO == null)
                    _loginDAO = new LoginDAO();
                return _loginDAO;
            }
        }

        public UsuarioModel EfetuarLogin(UsuarioModel usuario)
        {
            try
            {
                return ConvertEntityToModel(LoginDAO.Login(ConvertModelToEntity(usuario)).DataTableToList<HMA_USR>().FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao efetuar login! " + ex.Message);
            }
        }

        #region Private Methods
        private HMA_USR ConvertModelToEntity(UsuarioModel usuario)
        {
            try
            {
                var entity = new HMA_USR();
                entity._ID = usuario.Id;
                entity.EMA = usuario.Email;
                entity.NOM = usuario.Nome;
                entity.PAS = usuario.Senha;

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private UsuarioModel ConvertEntityToModel(HMA_USR entity)
        {
            try
            {
                var model = new UsuarioModel();
                model.Email = entity.EMA;
                model.Id = entity._ID;
                model.Nome = entity.NOM;
                model.PerfilSigla = entity.SIG;
                model.PerfilId = entity.PER_ID;

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
