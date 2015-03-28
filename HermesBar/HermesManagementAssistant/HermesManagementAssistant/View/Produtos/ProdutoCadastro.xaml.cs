using BLL.Fornecedor;
using BLL.Produtos;
using MODEL.Fornecedor;
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
    /// Interaction logic for ProdutoCadastro.xaml
    /// </summary>
    public partial class ProdutoCadastro : Window
    {
        #region AccessMethod
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
        private MarcaProdutoBLL _marcaProdutoBLL = null;
        public MarcaProdutoBLL MarcaProdutoBLL
        {
            get
            {
                if (_marcaProdutoBLL == null)
                    _marcaProdutoBLL = new MarcaProdutoBLL();
                return _marcaProdutoBLL;
            }
        }
        #endregion
        public ProdutoCadastro()
        {
            InitializeComponent();
            CarregaCombos();
            tbCodigo.Text = ProdutoBLL.SugereProximoCodigo().ToString();
        }
        public ProdutoCadastro(ProdutoModel produto)
        {
            InitializeComponent();
            CarregaCombos();
        }
        public void CarregaCombos()
        {
            cbFornecedor.ItemsSource = FornecedorBLL.Pesquisar(new FornecedorModel());
            cbTipo.ItemsSource = TipoProdutoBLL.RetornaTipos();
            cbUnidade.ItemsSource = ProdutoBLL.RetornaUnidadeProduto();
            cbMarca.ItemsSource = MarcaProdutoBLL.RetonaMarca();
        }
        public ProdutoModel CarregaProdutos()
        {
            var produto = new ProdutoModel();
            produto.CodigoOriginal = tbCodigo.Text;
            produto.CodigoBarras = "";
            produto.Fornecedor = new FornecedorModel() { Id = (int)cbFornecedor.SelectedValue };
            produto.Nome = tbNome.Text;
            produto.NomeReduzido = tbNomeReduzido.Text;
            produto.Observacao = tbObservacao.Text;
            produto.Tipo = new TipoProdutoModel() { Id = (int)cbTipo.SelectedValue };
            produto.Unidade = cbUnidade.SelectionBoxItem.ToString();
            produto.ValorCusto = double.Parse(tbValorCusto.Text);
            produto.ValorVenda = double.Parse(tbValorVenda.Text);
            produto.Marca = new MarcaModel() { Id = (int)cbMarca.SelectedValue };

            return produto;
        }
        public List<string> VerificaCamposObrigatorios()
        {
            var obrigatorios = new List<string>();
            if (string.IsNullOrEmpty(tbNome.Text))
                obrigatorios.Add("Nome");
            if (string.IsNullOrEmpty(tbNomeReduzido.Text))
                obrigatorios.Add("Nome Reduzido");
            if (string.IsNullOrEmpty(tbCodigo.Text))
                obrigatorios.Add("Código");
            if (cbFornecedor.SelectedValue == null)
                obrigatorios.Add("Fornecedor");
            if (string.IsNullOrEmpty(cbUnidade.SelectionBoxItem.ToString()))
                obrigatorios.Add("Unidade");
            if (cbMarca.SelectedValue == null)
                obrigatorios.Add("Marca");
            if (cbTipo.SelectedValue == null)
                obrigatorios.Add("Tipo");

            return obrigatorios;
        }
        private void Salvar(object sender, RoutedEventArgs e)
        {
            
        }

        #region Masked
        private void NumberMasked(object sender, KeyEventArgs e)
        {
            
        }
        #endregion
    }
}
