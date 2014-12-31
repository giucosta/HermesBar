using BLL.Login;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HermesManagementAssistant.Controller
{
    public class LoginController : View.Login.Login
    {
        private LoginBLL _loginBLL = null;
        public LoginBLL LoginBLL
        {
            get
            {
                if (_loginBLL == null)
                    _loginBLL = new LoginBLL();
                return _loginBLL;
            }
        }
        public void SplashScreen()
        {
            SplashScreen splash = new SplashScreen("images/LOGO_GENERICA 04.png");
            splash.Show(true);
            Thread.Sleep(3000);
            splash.Close(new TimeSpan(0, 0, 5));
        }
        public bool EfetuaLogin()
        {
            if (VerificaCampos())
            {
                var BLL = new LoginBLL();
                var login = new LoginModel();
                login.Login = tbLogin.Text;
                login.Senha = tbSenha.Password;

                if (LoginBLL.EfetuaLogin(login))
                    return true;
            }
            return false;
        }
        public bool VerificaCampos()
        {
            if (string.IsNullOrWhiteSpace(tbLogin.Text) && string.IsNullOrWhiteSpace(tbSenha.Password))
                return true;
            return false;
        }
        public bool EsqueceuSenha()
        {
            if (!string.IsNullOrWhiteSpace(tbLogin.Text))
            {
                var login = new LoginModel();
                login.Login = tbLogin.Text;

                if(LoginBLL.EsqueceuSenha(login))
                    return LoginBLL.EsqueceuSenha(login);       
            }
            return false;
        }
    }
}
