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
        public UsuarioDAO UsuarioDAO
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
            return UsuarioDAO.Pesquisar(usuario);
        }
        public bool RetornaUsuarioExistente(string usuario)
        {
            return UsuarioDAO.PesquisaUsuarioExistente(usuario);
        }

        public bool GravarUsuario(UsuarioModel usuario)
        {
            return UsuarioDAO.Salvar(usuario);
        }
    }
}
