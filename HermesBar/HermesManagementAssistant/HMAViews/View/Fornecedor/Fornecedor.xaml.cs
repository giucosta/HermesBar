using BLL.Fornecedor;
using FirstFloor.ModernUI.Windows.Controls;
using MODEL.Fornecedor;
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

namespace HMAViews.View.Fornecedor
{
    /// <summary>
    /// Interaction logic for Fornecedor.xaml
    /// </summary>
    public partial class Fornecedor : ModernWindow
    {
        private FornecedorBLL _fornecedorBLL = null;
        public FornecedorBLL FornecedorBLL
        {
            get
            {
                if (_fornecedorBLL == null)
                    _fornecedorBLL = new FornecedorBLL();
                return _fornecedorBLL;
            }
        }
        public Fornecedor()
        {
            InitializeComponent();
            gridPesquisa.ItemsSource = FornecedorBLL.Pesquisar(new FornecedorModel());
        }
        private void Pesquisa(object sender, RoutedEventArgs e)
        {
            var fornecedor = new FornecedorModel();
            if (!string.IsNullOrWhiteSpace(tbCodigo.Text))
                fornecedor.Id = int.Parse(tbCodigo.Text);
            fornecedor.RazaoSocial = tbRazaoSocial.Text;
            gridPesquisa.ItemsSource = FornecedorBLL.Pesquisar(fornecedor);
        }
        private void Novo(object sender, RoutedEventArgs e)
        {
            new FornecedorCadastro().Show();
        }
    }
}
