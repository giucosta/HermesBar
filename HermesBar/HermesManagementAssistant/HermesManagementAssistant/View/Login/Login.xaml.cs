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
using MODEL.Login;

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

            var usuario = new MODEL.UsuarioModel()
            {
                Senha = tbSenha.Password,
                Nome = tbLogin.Text
            };

            if (new LoginBLL().efetuaLogin(new LoginModel() { Usuario = usuario }))
                new MainWindow().Show();
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
    }
}
