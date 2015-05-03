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
using BLL.Caixa;
using MODEL.Caixa;
using MODEL.Pedido;
using BLL.Pedido;
using BLL.Estoque;
using MODEL.Estoque;

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
        private CartaoBLL _cartaoBLL = null;
        public CartaoBLL CartaoBLL
        {
            get
            {
                if (_cartaoBLL == null)
                    _cartaoBLL = new CartaoBLL();
                return _cartaoBLL;
            }
        }
        private PedidoBLL _pedidoBLL = null;
        public PedidoBLL PedidoBLL
        {
            get
            {
                if (_pedidoBLL == null)
                    _pedidoBLL = new PedidoBLL();
                return _pedidoBLL;
            }
        }
        private EstoqueBLL _estoqueBLL = null;
        public EstoqueBLL EstoqueBLL
        {
            get
            {
                if (_estoqueBLL == null)
                    _estoqueBLL = new EstoqueBLL();
                return _estoqueBLL;
            }
        }


        private List<FuncionarioModel> _funcionarios = null;
        private List<ProdutoGridModel> _produtos = null;
        private List<CartaoModel> _cartao = null;

        private FuncionarioModel _funcionarioModel = null;
        private ProdutoGridModel _produtoModel = null;
        private CartaoModel _cartaoModel = null;
        
        public MainWindow()
        {
            InitializeComponent();
            _funcionarios = FuncionarioBLL.Pesquisa(new FuncionarioModel());
            _produtos = ProdutoBLL.Pesquisa(new ProdutoModel());
            _cartao = CartaoBLL.Pesquisar(new CartaoModel());

            tbNumeroCartao.Focus();
        }
        private void AdicionarPedido(object sender, RoutedEventArgs e)
        {
            var camposObrigatorios = CamposObrigatorios();
            if (camposObrigatorios.Count == 0)
            {
                var pedido = new PedidoModel();
                pedido.CodigoFuncionario = _funcionarioModel;
                pedido.NumeroCartao = _cartaoModel;
                pedido.CodigoFuncionario = _funcionarioModel;
                pedido.CodigoProduto = new ProdutoModel();
                pedido.CodigoProduto.CodigoOriginal = _produtoModel.CodigoOriginal;
                pedido.Observacao = tbObservacao.Text;
                pedido.Quantidade = tbQuantidade.Text;

                if (!PedidoBLL.Salvar(pedido))
                    Mensagens.GeraMensagens("Erro ao inserir!", MENSAGEM.PEDIDOS_INSERIR_ERRO, TIPOS_MENSAGENS.ERRO);
                else
                    _produtos = ProdutoBLL.Pesquisa(new ProdutoModel());

                LimparCampos();
            }
            else
                Mensagens.GeraMensagens("Campos obrigatórios", MENSAGEM.CAMPOS_OBRIGATORIOS, camposObrigatorios, TIPOS_MENSAGENS.ALERTA);
        }
        private void CarregaCliente(object sender, RoutedEventArgs e)
        {
            foreach (var item in _cartao)
            {
                if (item.NumeroCartao == tbNumeroCartao.Text)
                {
                    lbResultNumeroCartao.Content = item.Cliente.Nome;
                    _cartaoModel = item;
                    dgPedidos.ItemsSource = PedidoBLL.Pesquisar(new PedidoModel() { NumeroCartao = new CartaoModel() { NumeroCartao = item.NumeroCartao } });
                    break;
                }
            }
        }
        private void CarregaFuncionario(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbCodigFuncionario.Text))
            {
                foreach (var item in _funcionarios)
                {
                    if (item.Id == Convert.ToInt16(tbCodigFuncionario.Text))
                    {
                        lbResultCodigoFuncionario.Content = item.Nome;
                        _funcionarioModel = item;
                        break;
                    }
                }
            }
        }
        private void CarregaProduto(object sender, RoutedEventArgs e)
        {
            foreach (var item in _produtos)
            {
                if (item.CodigoOriginal == tbCodigoProduto.Text)
                {
                    if (Double.Parse(item.QuantidadeEstoque) > 0)
                    {
                        lbResultNomeProduto.Content = item.Nome;
                        _produtoModel = item;
                    }
                    else 
                    {
                        Mensagens.GeraMensagens("Estoque!", MENSAGEM.PEDIDOS_FALTA_ESTOQUE, null, TIPOS_MENSAGENS.ALERTA);
                        LimparCampos();
                    }
                        
                    break;
                }
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
        private List<string> CamposObrigatorios()
        {
            var list = new List<string>();
            if (string.IsNullOrEmpty(tbCodigFuncionario.Text))
                list.Add("CÓDIGO FUNCIONÁRIO");
            if (string.IsNullOrEmpty(tbCodigoProduto.Text))
                list.Add("CÓDIGO PRODUTO");
            if (string.IsNullOrEmpty(tbQuantidade.Text))
                list.Add("QUANTIDADE");
            if(string.IsNullOrEmpty(tbNumeroCartao.Text))
                list.Add("CARTÃO");

            return list;
        }
        private void LimparCampos()
        {
            tbQuantidade.Clear();
            tbObservacao.Clear();
            tbNumeroCartao.Clear();
            tbCodigoProduto.Clear();
            tbCodigFuncionario.Clear();

            lbResultCodigoFuncionario.Content = "";
            lbResultNomeProduto.Content = "";
            lbResultNumeroCartao.Content = "";

            dgPedidos.ItemsSource = null;

            tbNumeroCartao.Focus();
        }
    }
}
