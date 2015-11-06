using BLL.User;
using HELPER;
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
        private LoginBLL LoginBLL;
        public Login()
        {
            this.LoginBLL = Singleton<LoginBLL>.Instance();
        }
        public UsuarioModel EfetuarLogin(UsuarioModel user)
        {
            return LoginBLL.EfetuarLogin(user);
        }

        public void GenerateBackup()
        {
            LoginBLL.CreateBackup();
        }
    }
}