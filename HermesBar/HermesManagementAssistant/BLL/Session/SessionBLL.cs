using DAO.Usuario;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILS;

namespace BLL.Session
{
    public class SessionBLL
    {
        public void CarregaSession(LoginModel login)
        {
            UTILS.Session.Usuario = new UsuarioDAO().RetornaUsuario(login);
        }
    }
}
