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
using System.Windows.Shapes;
using System.Data.SqlClient;
using BLL.Login;
using BLL.Session;
using MODEL;

namespace HermesManagementAssistant.View.Login
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            SplashScreen splash = new SplashScreen("images/LOGO_GENERICA 04.png");
            splash.Show(true);
            Thread.Sleep(3000);
            splash.Close(new TimeSpan(0, 0, 5));

            InitializeComponent();
        }

        private void btEntrar_Click(object sender, RoutedEventArgs e)
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
            }    
            else
            {
                MessageBox.Show("Login e/ou senha inválido", "Login e/ou senha inválido !", MessageBoxButton.OK, MessageBoxImage.Error);
                limparCampos();
            }    
        }

        private void limparCampos()
        {
            tbSenha.Clear();
            tbSenha.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbLogin.Text))
            {
                if (new LoginBLL().EsqueceuSenha(new LoginModel() { Login = tbLogin.Text }))
                    MessageBox.Show("Sua nova senha foi enviada para o email cadastrado!", "Email enviado", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Ocorreu um erro ao enviar email, favor consultar o administrador do sistema", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Favor preencher o campo LOGIN", "Preencha os campos", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbLogin.Focus();
            }
        }
    }
}
