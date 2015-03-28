using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Login;
using System.Net.Mail;
using System.Net;
using DAO.Usuario;
using DAO.Encript;
using MODEL;
using UTIL;

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

        private UsuarioDAO _usuarioDao = null;
        public UsuarioDAO UsuarioDAO
        {
            get
            {
                if (_usuarioDao == null)
                    _usuarioDao = new UsuarioDAO();
                return _usuarioDao;
            }
        }
        #endregion
        /// <summary>
        /// Verifica o usuario e senha
        /// </summary>
        /// <param name="login">LoginModel login</param>
        /// <returns></returns>
        public bool EfetuaLogin(LoginModel login)
        {
            if(!string.IsNullOrWhiteSpace(login.Login) && !string.IsNullOrWhiteSpace(login.Senha))
                return LoginDAO.EfetuaLogin(login);

            return false;
        }

        /// <summary>
        /// Envia email com a nova senha ao usuario e grava a senha no banco
        /// </summary>
        /// <param name="login">LoginModel login</param>
        /// <returns></returns>
        public bool EsqueceuSenha(LoginModel login)
        {
            using (var smtp = new SmtpClient())
            {
                try
                {
                    var envioDe = new MailAddress(Constantes.ADadosEmail.ENDERECO_EMAIL, Constantes.ADadosEmail.IDENTIFICACAO_EMAIL);
                    
                    #region Definicoes do usuario
                    var emailUsuario = RecuperaEmailUsuario(login);
                    if (string.IsNullOrWhiteSpace(emailUsuario))
                        return false;
                    var envioPara = new MailAddress(emailUsuario);
                    var passwordEmailUsuario = GeraNovaSenha(login);
                    #endregion

                    #region Smtp
                    smtp.Host = Constantes.ADadosEmail.HOST_EMAIL;
                    smtp.Port = Constantes.ADadosEmail.PORTA_EMAIL;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(envioDe.Address, Constantes.ADadosEmail.SENHA_EMAIL);
                    #endregion

                    #region Formatacao do Email e envio
                    using (var message = new MailMessage(envioDe, envioPara)
                    {
                        Subject = Constantes.ADadosEmail.TITULO_EMAIL,
                        IsBodyHtml = true,
                        Body = "<html>" +
                                    "<head>" +
                                    "</head>" +
                                    "<body>" +
                                        Constantes.ADadosEmail.CORPO_INICIO_EMAIL +
                                        "<br/>" +
                                        "<br/>" +
                                        Constantes.ADadosEmail.CORPO_MEIO_EMAIL +
                                        "<br/>"+
                                        "<b>" + passwordEmailUsuario + "</b>" +
                                        "<br/>" +
                                        "<br/>" +
                                        Constantes.ADadosEmail.CORPO_FINAL_EMAIL +
                                    "</body>" +
                                "</html>"
                    })
                    {
                        smtp.Send(message);
                    }
                    #endregion
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Recupera email do usuario
        /// </summary>
        /// <param name="login">LoginModel login</param>
        /// <returns></returns>
        public string RecuperaEmailUsuario(LoginModel login)
        {
            return UsuarioDAO.RecuperaEmailUsuario(login);
        }
        /// <summary>
        /// Gera uma nova senha pro usuario e grava a senha no banco
        /// </summary>
        /// <param name="login">LoginModel login</param>
        /// <returns></returns>
        public string GeraNovaSenha(LoginModel login)
        {
            string guid = Guid.NewGuid().ToString().Replace("-", "");

            Random clsRan = new Random();
            Int32 tamanhoSenha = clsRan.Next(6, 16);

            var novaSenha = "";
            for (Int32 i = 0; i <= tamanhoSenha; i++)
                novaSenha += guid.Substring(clsRan.Next(1, guid.Length), 1);

            login.Senha = novaSenha;
            if (GravaNovaSenha(login))
                return novaSenha;
            return null;
        }
        /// <summary>
        /// Grava a nova senha no banco
        /// </summary>
        /// <param name="login">LoginModel login</param>
        /// <returns></returns>
        public bool GravaNovaSenha(LoginModel login)
        {
            if (UsuarioDAO.GravaNovaSenha(login))
                return true;
            return false;
        }

        public LoginModel RecuperaLogin(LoginModel login)
        {
            return LoginDAO.RecuperaLogin(login);
        }

        public LoginModel GravarLogin(LoginModel login)
        {
            return LoginDAO.Salvar(login);
        }
    }
}
