using BLL.User;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HermesBarWCF.DataContracts
{
    [DataContract]
    public class Login
    {
        private LoginBLL _loginBLL = null;
        private LoginBLL LoginBLL
        {
            get
            {
                if (_loginBLL == null)
                    _loginBLL = new LoginBLL();
                return _loginBLL;
            }
        }
        public UsuarioModel EfetuarLogin(UsuarioModel usuario)
        {
            return LoginBLL.EfetuarLogin(usuario);
        }
    }
}