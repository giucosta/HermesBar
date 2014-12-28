using MODEL.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Login;

namespace BLL.Login
{
    public class LoginBLL
    {
        #region AccessMethod
        private LoginDAO _loginDao = null;
        public LoginDAO LoginDAO
        {
            get
            {
                if (_loginDao == null)
                    _loginDao = new LoginDAO();
                return _loginDao;
            }
        }
        #endregion
        public bool efetuaLogin(LoginModel login)
        {
            if (login.Usuario.Nome != null && login.Usuario.Senha != null)
                return LoginDAO.efetuaLogin(login);

            return false;
        }
    }
}
