using BLL.Produtos;
using MODEL.Produto;
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

namespace HMAViews.View.Produtos
{
    /// <summary>
    /// Interaction logic for TipoProduto.xaml
    /// </summary>
    public partial class TipoProduto : Window
    {
        private TipoProdutoBLL _tipoProdutoBLL = null;
        public TipoProdutoBLL TipoProdutoBLL
        {
            get
            {
                if (_tipoProdutoBLL == null)
                    _tipoProdutoBLL = new TipoProdutoBLL();
                return _tipoProdutoBLL;
            }
        }
        public TipoProduto()
        {
            InitializeComponent();
            gridPesquisa.ItemsSource = TipoProdutoBLL.Pesquisa(new TipoProdutoModel());
        }
        private void EditarTipoProduto(object sender, RoutedEventArgs e)
        {

        }
        private void NovoTipoProduto(object sender, RoutedEventArgs e)
        {
            new TipoProdutoCadastro().Show();
        }
        private void PesquisaTipoProduto(object sender, RoutedEventArgs e)
        {
            gridPesquisa.ItemsSource = TipoProdutoBLL.Pesquisa(new TipoProdutoModel() { Tipo = tbTipo.Text });
        }
        private void Editar(object sender, RoutedEventArgs e)
        {

        }
        private void EditarTipoProduto(object sender, MouseButtonEventArgs e)
        {
            DataGrid data = (DataGrid)sender;
            new TipoProdutoCadastro((TipoProdutoModel)data.SelectedItems[0]).Show();
        }
    }
}
