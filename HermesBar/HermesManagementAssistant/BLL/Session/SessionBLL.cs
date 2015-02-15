using DAO.Usuario;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Session
{
    public class SessionBLL
    {
        public void CarregaSession(LoginModel login)
        {
            DAO.Session.Usuario = new UsuarioDAO().RetornaUsuario(login);
        }
    }
}
