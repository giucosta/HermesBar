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
            var login = new LoginModel();
            var usuario = new MODEL.UsuarioModel();
            usuario.Senha = tbSenha.Text;
            usuario.Nome = tbLogin.Text;
            login.Usuario = usuario;

            if (new LoginBLL().efetuaLogin(login))
                MessageBox.Show("Login ok");
            else
                MessageBox.Show("tenso");
        }
    }
}
