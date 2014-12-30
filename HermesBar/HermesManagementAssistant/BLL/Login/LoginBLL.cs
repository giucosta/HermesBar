using MODEL.Login;
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
            if (login.Usuario.Nome != null && login.Usuario.Senha != null)
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
                    #region Dados de email
                    var enderecoEmailHermes = "hermesmanagementassistant@gmail.com";
                    var identificacaoEmailHermes = "Hermes Management Assistant";
                    var passwordEmailHermes = "hermesBarSistema";
                    var envioDe = new MailAddress(enderecoEmailHermes, identificacaoEmailHermes);
                    #endregion

                    #region Definicoes do usuario

                    if (RecuperaEmailUsuario(login) == null)
                        return false;
                    var envioPara = new MailAddress(RecuperaEmailUsuario(login));
                    var passwordEmailUsuario = GeraNovaSenha(login);
                    #endregion

                    #region Configuracoes de email
                    var tituloEmail = "Hermes Management Assistant";
                    var corpoEmailInicio1 = "Olá, segue sua nova senha de acesso ao sistema Hermes Management Assistant";
                    var corpoEmailInicio2 = "Acreditamos que seja interessante efetuar a troca da senha, acessando o módulo Gestão/Configurações/Senhas";
                    var corpoEmailFinal = "Email enviando automaticamente, caso necessite entrar em contato envie email para: giulianocosta@outlook.com";
                    var hostEmail = "smtp.gmail.com";
                    var portaEnvioEmail = "587";
                    #endregion

                    #region Smtp
                    smtp.Host = hostEmail;
                    smtp.Port = int.Parse(portaEnvioEmail);
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(envioDe.Address, passwordEmailHermes);
                    #endregion

                    #region Formatacao do Email e envio
                    using (var message = new MailMessage(envioDe, envioPara)
                    {
                        Subject = tituloEmail,
                        IsBodyHtml = true,
                        Body = "<html>" +
                                    "<head>" +
                                    "</head>" +
                                    "<body>" +
                                        corpoEmailInicio1 +
                                        "<br/>" +
                                        "<br/>" +
                                        corpoEmailInicio2 +
                                        "<br/>"+
                                        "<b>" + passwordEmailUsuario + "</b>" +
                                        "<br/>" +
                                        "<br/>" +
                                        corpoEmailFinal +
                                    "</body>" +
                                "</html>"
                    })
                    {
                        smtp.Send(message);
                    }
                    #endregion
                    return true;
                }
                catch (Exception ex)
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

            login.Usuario.Senha = novaSenha;
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
    }
}
