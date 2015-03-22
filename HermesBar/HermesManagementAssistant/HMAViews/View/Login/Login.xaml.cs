using BLL.Login;
using BLL.Session;
using FirstFloor.ModernUI.Windows.Controls;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utils.Mensagens;

namespace HMAViews.View.Login
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : ModernWindow
    {
        public Login()
        {
            SplashScreen splash = new SplashScreen("images/LOGO_GENERICA 04.png");
            splash.Show(true);
            Thread.Sleep(3000);
            splash.Close(new TimeSpan(0, 0, 5));

            InitializeComponent();
            tbLogin.Focus();
        }
        private void Entrar(object sender, RoutedEventArgs e)
        {
            var login = new LoginModel()
            {
                Login = tbLogin.Text,
                Senha = tbSenha.Password
            };
            if (new LoginBLL().EfetuaLogin(login))
            {
                new SessionBLL().CarregaSession(login);
                new MainWindow().Show();
                this.Close();
            }
            else
            {
                Mensagens.GeraMensagens(MENSAGEM.LOGIN_INVALIDO, MENSAGEM.LOGIN_INVALIDO, null, TIPOS_MENSAGENS.ERRO);
                limparCampos();
            }
        }
        private void limparCampos()
        {
            tbSenha.Clear();
            tbSenha.Clear();
        }
        private void EsqueceuSenha(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbLogin.Text))
            {
                if (new LoginBLL().EsqueceuSenha(new LoginModel() { Login = tbLogin.Text }))
                    Mensagens.GeraMensagens("Email enviado", MENSAGEM.NOVA_SENHA_EMAIL, null, TIPOS_MENSAGENS.SUCESSO);
                else
                    Mensagens.GeraMensagens("Erro", MENSAGEM.ERRO_ENVIA_EMAIL, null, TIPOS_MENSAGENS.ERRO);
            }
            else
            {
                Mensagens.GeraMensagens("Preencha os campos", MENSAGEM.PREENCHE_CAMPO_LOGIN, null, TIPOS_MENSAGENS.ALERTA);
                tbLogin.Focus();
            }
        }
    }
}
