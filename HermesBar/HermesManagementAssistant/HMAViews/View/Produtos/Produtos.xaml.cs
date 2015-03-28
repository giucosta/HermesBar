using FirstFloor.ModernUI.Windows.Controls;
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
using HermesManagementAssistant.Utils;

namespace HMAViews.View.Produtos
{
    /// <summary>
    /// Interaction logic for Produtos.xaml
    /// </summary>
    public partial class Produtos : ModernWindow
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
        public Produtos()
        {
            InitializeComponent();
            gridPesquisa.ItemsSource = ProdutoBLL.Pesquisa(new ProdutoModel());
            cbTipoProduto.ItemsSource = TipoProdutoBLL.RetornaTipos();
        }
        private void MascaraCodigo(object sender, KeyEventArgs e)
        {
            Mascaras.SomenteNumeros(tbCodigo, e);
        }
        private void Pesquisar(object sender, RoutedEventArgs e)
        {
            if (cbTipoProduto.SelectedValue == null)
            {
                gridPesquisa.ItemsSource = ProdutoBLL.Pesquisa(
                    new ProdutoModel()
                    {
                        Nome = tbNome.Text,
                        CodigoOriginal = tbCodigo.Text
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
    }
}
