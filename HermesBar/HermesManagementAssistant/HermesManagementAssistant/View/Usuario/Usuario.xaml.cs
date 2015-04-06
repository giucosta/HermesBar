using BLL.Usuario;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HMAViews.View.Usuario
{
    /// <summary>
    /// Interaction logic for Usuario.xaml
    /// </summary>
    public partial class Usuario : Window
    {
        public Usuario()
        {
            InitializeComponent();
            DataGridPesquisaUsuario.ItemsSource = new UsuarioBLL().PesquisaUsuario(new UsuarioModel() { Nome = "", Email = "" }).DefaultView;
        }

        private void tbPesquisar_Click(object sender, RoutedEventArgs e)
        {
            DataGridPesquisaUsuario.ItemsSource = new UsuarioBLL().PesquisaUsuario(new UsuarioModel() { Nome = tbNome.Text, Email = tbEmail.Text }).DefaultView;
        }

        private void btNovo_Click(object sender, RoutedEventArgs e)
        {
            new UsuarioCadastro().Show();
        }
    }
}
