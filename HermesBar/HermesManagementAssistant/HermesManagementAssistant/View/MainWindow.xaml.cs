using HermesManagementAssistant.View.Usuario;
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
using UTILS;

namespace HermesManagementAssistant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            if (Session.Usuario.Perfil.Perfil == Constantes.APerfil.ADMINISTRADOR)
                InitializeComponent();
            else
                MessageBox.Show("Não tem permissão");
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            new Usuario().Show();
        }
    }
}
