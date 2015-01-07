using DAO.Usuario;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Usuario
{
    public class UsuarioBLL
    {
        private UsuarioDAO _usuarioDao;
        public UsuarioDAO UsuarioDao
        {
            get
            {
                if(_usuarioDao == null)
                    _usuarioDao = new UsuarioDAO();
                return _usuarioDao;
            }
        }
        public DataTable PesquisaUsuario(UsuarioModel usuario)
        {
            return UsuarioDao.PesquisaUsuario(usuario);
        }

        public bool RetornaUsuarioExistente(string usuario)
        {
            return UsuarioDao.PesquisaUsuarioExistente(usuario);
        }
    }
}
