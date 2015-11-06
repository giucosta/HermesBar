using DAO.User;
using ENTITY.User;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.UTIL;
using HELPER;

namespace BLL.User
{
    public class LoginBLL
    {
        #region Singleton
        private LoginDAO LoginDAO = Singleton<LoginDAO>.Instance();
        #endregion

        public UsuarioModel EfetuarLogin(UsuarioModel usuario)
        {
            try
            {
                var result = LoginDAO.Login(ConvertModelToEntity(usuario)).DataTableToList<HMA_USR>().FirstOrDefault();
                if (result != null)
                    return ConvertEntityToModel(result);
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao efetuar login! " + ex.Message);
            }
        }

        public void CreateBackup()
        {
            LoginDAO.GenerateBackup();
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
                entity.MTR = Convert.ToInt32(usuario.MatrizSelected);

                return entity;
            }
            catch (Exception)
            {
                throw;
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
                model.MatrizSelected = entity.MTR.ToString();

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
