using FirstFloor.ModernUI.Windows.Controls;
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
using HMAViews.Utils;
using HMAViews.Mascara;
using BLL.Funcionario;
using MODEL;
using BLL.Produtos;
using MODEL.Produto;

namespace HMAPedidos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
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
        private List<FuncionarioModel> _funcionarios = null;
        private List<ProdutoGridModel> _produtos = null;
        public MainWindow()
        {
            InitializeComponent();
            _funcionarios = FuncionarioBLL.Pesquisa(new FuncionarioModel());
            _produtos = ProdutoBLL.Pesquisa(new ProdutoModel());
        }
        private void CarregaCliente(object sender, RoutedEventArgs e)
        {
            lbResultNumeroCartao.Content = tbNumeroCartao.Text;
        }
        private void CarregaFuncionario(object sender, RoutedEventArgs e)
        {
            foreach (var item in _funcionarios)
            {
                if(item.Id == Convert.ToInt16(tbCodigFuncionario.Text))
                    lbResultCodigoFuncionario.Content = item.Nome;
                break;
            }
        }
        private void CarregaProduto(object sender, RoutedEventArgs e)
        {
            foreach (var item in _produtos)
            {
                if (item.CodigoOriginal == tbCodigoProduto.Text)
                    lbResultNomeProduto.Content = item.Nome;
                break;
            }
        }
        private void MascaraCartao(object sender, KeyEventArgs e)
        {
            Mascaras.SomenteNumeros(tbNumeroCartao, e);
        }
        private void MascaraFuncionario(object sender, KeyEventArgs e)
        {
            Mascaras.SomenteNumeros(tbCodigFuncionario, e);
        }
        private void MascaraProduto(object sender, KeyEventArgs e)
        {
            Mascaras.SomenteNumeros(tbCodigoProduto, e);
        }
        private void MascaraQuantidade(object sender, KeyEventArgs e)
        {
            Mascaras.SomenteNumeros(tbQuantidade, e);
        }
        private void RemoveTextoPesquisa(object sender, RoutedEventArgs e)
        {
            tbPesquisar.Clear();
        }
        private void RetornaTextoPesquisa(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbPesquisar.Text))
                tbPesquisar.Text = "Pesquisar";
        }
    }
}
