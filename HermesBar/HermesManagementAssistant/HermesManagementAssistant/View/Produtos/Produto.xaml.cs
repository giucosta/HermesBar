using BLL.Produtos;
using MODEL.Produto;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Produto.xaml
    /// </summary>
    public partial class Produto : Window
    {
        private ProdutoBLL _produtoBLL = null;
        public ProdutoBLL ProdutoBLL
        {
            get
            {
                if (_produtoBLL == null)
                    _produtoBLL = new ProdutoBLL();
                return _produtoBLL;
            }
        }
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
        public Produto()
        {
            InitializeComponent();
            gridPesquisa.ItemsSource = ProdutoBLL.Pesquisa(new ProdutoModel());
            cbTipoProduto.ItemsSource = TipoProdutoBLL.RetornaTipos();
        }
        private void Editar(object sender, MouseButtonEventArgs e)
        {

        }
        private void Pesquisar(object sender, RoutedEventArgs e)
        {
            if(cbTipoProduto.SelectedValue == null){
                gridPesquisa.ItemsSource = ProdutoBLL.Pesquisa(
                    new ProdutoModel()
                    {
                        Nome = tbNome.Text,
                        CodigoOriginal = tbCodigo.Text,
                        Tipo = new TipoProdutoModel()
                    });
            }
            else
            {
                gridPesquisa.ItemsSource = ProdutoBLL.Pesquisa(
                    new ProdutoModel()
                    {
                        Nome = tbNome.Text,
                        CodigoOriginal = tbCodigo.Text,
                        Tipo = new TipoProdutoModel() { Id = (int)cbTipoProduto.SelectedValue }
                    });
            }
        }
        public void NovoProduto(object sender, RoutedEventArgs e)
        {
            new ProdutoCadastro().Show();
            this.Close();
        }
    }
}
