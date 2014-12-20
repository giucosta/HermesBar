using Model.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Usuarios;

namespace BLL.Usuarios
{
    class UsuarioBLL
    {
        private UsuariosDAO _Dao;

        public UsuarioBLL()
        {
            _Dao = new UsuariosDAO();
        }


        /// <summary>
        /// Método responsável pelas regras de negócio do cadastro de usuario
        /// Metodo chamado no controller (HermesManagementAssistant - UsuarioController)
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool CadastrarUsuario(Usuario usuario)
        {
            return _Dao.CadastrarUsuario(usuario);
        }
    }
}
