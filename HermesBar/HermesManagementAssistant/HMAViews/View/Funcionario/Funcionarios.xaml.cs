using BLL.Funcionario;
using FirstFloor.ModernUI.Windows.Controls;
using HMAViews.Mascara;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HMAViews.View.Funcionario
{
    /// <summary>
    /// Interaction logic for Funcionarios.xaml
    /// </summary>
    public partial class Funcionarios : ModernWindow
    {
        private FuncionarioBLL _funcionarioBLL = null;
        public FuncionarioBLL FuncionarioBLL
        {
            get
            {
                if (_funcionarioBLL == null)
                    _funcionarioBLL = new FuncionarioBLL();
                return _funcionarioBLL;
            }
        }

        public Funcionarios()
        {
            InitializeComponent();
            gridPesquisa.ItemsSource = FuncionarioBLL.Pesquisa(new FuncionarioModel());
        }
        private void Novo(object sender, RoutedEventArgs e)
        {
            new FuncionariosCadastro().Show();
        }
        private void Pesquisar(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbCodigo.Text))
                gridPesquisa.ItemsSource = FuncionarioBLL.Pesquisa(new FuncionarioModel() { Id = int.Parse(tbCodigo.Text), Nome = tbNome.Text });
            else
                gridPesquisa.ItemsSource = FuncionarioBLL.Pesquisa(new FuncionarioModel() { Nome = tbNome.Text });
        }
        private void MascaraCodigo(object sender, KeyEventArgs e)
        {
            Mascaras.SomenteNumeros(tbCodigo, e);
        }
    }
}
