using BLL.Fornecedor;
using BLL.Produtos;
using FirstFloor.ModernUI.Windows.Controls;
using HMAViews.Utils;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using HMAViews;

namespace HMAViews.View.Produtos
{
    /// <summary>
    /// Interaction logic for ProdutosCadastro.xaml
    /// </summary>
    public partial class ProdutosCadastro : ModernWindow
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
        private ProdutoModel _produto = null;
        public ProdutosCadastro()
        {
            InitializeComponent();
            CarregaCamposTela();
            btExcluir.Visibility = System.Windows.Visibility.Hidden;
        }
        public ProdutosCadastro(ProdutoGridModel produto)
        {
            InitializeComponent();
            CarregaCamposTela();
            CarregaTelaEdicao(produto);
            tbCodigo.IsReadOnly = true;
        }
        private void MascaraCodigo(object sender, KeyEventArgs e)
        {
            Mascaras.SomenteNumeros(tbCodigo, e);
        }
        private void CarregaCamposTela()
        {
            cbFornecedor.ItemsSource = FornecedorBLL.Pesquisar(new FornecedorModel());
            cbTipo.ItemsSource = TipoProdutoBLL.RetornaTipos();
            cbUnidade.ItemsSource = ProdutoBLL.RetornaUnidadeProduto();
            cbMarca.ItemsSource = MarcaProdutoBLL.RetonaMarca();
            tbCodigo.Text = ProdutoBLL.SugereProximoCodigo().ToString();
        }
        private void Editar()
        {
            var camposObrigatorios = VerificaCamposObrigatorios();
            if (camposObrigatorios.Count == 0)
            {
                if (ProdutoBLL.Editar(CarregaProduto()))
                    Mensagens.GeraMensagens("Editado com sucesso!", MENSAGEM.PRODUTO_EDITAR_SUCESSO, null, TIPOS_MENSAGENS.SUCESSO);
                else
                    Mensagens.GeraMensagens("Erro ao editar", MENSAGEM.PRODUTO_EDITAR_ERRO, TIPOS_MENSAGENS.ERRO);
            }
            else
                Mensagens.GeraMensagens("Campos Obrigatórios", MENSAGEM.CAMPOS_OBRIGATORIOS, null, TIPOS_MENSAGENS.ALERTA);
        }
        private void Salvar(object sender, RoutedEventArgs e)
        {
            if (_produto == null)
            {
                var camposObrigatorios = VerificaCamposObrigatorios();
                if (camposObrigatorios.Count == 0)
                {
                    if (ProdutoBLL.Salvar(CarregaProduto()))
                    {
                        Mensagens.GeraMensagens("Produto cadastrado!", MENSAGEM.PRODUTO_CADASTRO_SUCESSO, null, TIPOS_MENSAGENS.SUCESSO);
                        new Produtos().Show();
                        this.Close();
                    }
                    else
                        Mensagens.GeraMensagens("Erro ao cadastrar!", MENSAGEM.PRODUTO_CADASTRO_ERRO, TIPOS_MENSAGENS.ERRO);
                }
                else
                    Mensagens.GeraMensagens("Campos Obrigatórios!", MENSAGEM.CAMPOS_OBRIGATORIOS, camposObrigatorios, TIPOS_MENSAGENS.ALERTA);
            }
            else
                Editar();
        }
        private void Excluir(object sender, RoutedEventArgs e)
        {
            if (Mensagens.GeraMensagens("Confirma exclusão?", MENSAGEM.PRODUTO_EXCLUIR_CONFIRMA, null, TIPOS_MENSAGENS.QUESTAO))
            {
                if (ProdutoBLL.Excluir(_produto)){
                    Mensagens.GeraMensagens("Excluído com sucesso!", MENSAGEM.PRODUTO_EXCLUIR_SUCESSO, null, TIPOS_MENSAGENS.SUCESSO);
                    new Produtos().Show();
                    this.Close();
                }
                else
                    Mensagens.GeraMensagens("Erro ao excluir", MENSAGEM.PRODUTO_EXCLUIR_ERRO, TIPOS_MENSAGENS.ERRO);
            }
        }
        private List<string> VerificaCamposObrigatorios()
        {
            var camposObrigatorios = new List<string>();

            if (string.IsNullOrEmpty(tbNome.Text))
                camposObrigatorios.Add("Nome");
            if (string.IsNullOrEmpty(tbNomeReduzido.Text))
                camposObrigatorios.Add("Nome Reduzido");
            if (string.IsNullOrEmpty(tbCodigo.Text))
                camposObrigatorios.Add("Código");
            if (string.IsNullOrEmpty(cbTipo.SelectionBoxItem.ToString()))
                camposObrigatorios.Add("Tipo");
            if(string.IsNullOrEmpty(cbUnidade.SelectionBoxItem.ToString()))
                camposObrigatorios.Add("Unidade");
            if (string.IsNullOrEmpty(cbFornecedor.SelectionBoxItem.ToString()))
                camposObrigatorios.Add("Fornecedor");
            if (string.IsNullOrEmpty(tbValorCusto.Text))
                camposObrigatorios.Add("Valor Custo");
            if (string.IsNullOrEmpty(tbValorVenda.Text))
                camposObrigatorios.Add("Valor Venda");

            return camposObrigatorios;
        }
        private ProdutoModel CarregaProduto()
        {
            var produto = new ProdutoModel();
            if (_produto != null)
                produto.Id = _produto.Id;
            produto.CodigoOriginal = tbCodigo.Text;
            produto.CodigoBarras = "";
            produto.Fornecedor = new FornecedorModel() { Id = (int)cbFornecedor.SelectedValue };
            produto.Marca = new MarcaModel() { Id = (int)cbMarca.SelectedValue };
            produto.Nome = tbNome.Text;
            produto.NomeReduzido = tbNomeReduzido.Text;
            produto.Observacao = tbObservacao.Text;
            produto.Tipo = new TipoProdutoModel() { Id = (int)cbTipo.SelectedValue };
            produto.Unidade = cbUnidade.SelectionBoxItem.ToString();
            produto.ValorCusto = Convert.ToDouble(tbValorCusto.Text);
            produto.ValorVenda = Convert.ToDouble(tbValorVenda.Text);

            return produto;
        }
        private void CarregaTelaEdicao(ProdutoGridModel produto)
        {
             _produto = ProdutoBLL.RetornaProdutoEdicao(produto);

             tbCodigo.Text = _produto.CodigoOriginal;
             tbNome.Text = _produto.Nome;
             tbNomeReduzido.Text = _produto.NomeReduzido;
             tbObservacao.Text = _produto.Observacao;
             tbQuantEstoque.Text = _produto.QuantidadeEstoque.ToString();
             tbValorCusto.Text = _produto.ValorCusto.ToString();
             tbValorVenda.Text = _produto.ValorVenda.ToString();
           
            var i = 0;
            foreach (var item in cbFornecedor.ItemsSource)
            {
                FornecedorModel forn = (FornecedorModel)item;
                if (forn.RazaoSocial.Equals(_produto.Fornecedor.RazaoSocial))
                {
                    cbFornecedor.SelectedIndex = i;
                    break;
                }
                else
                    i++;
            }
            i = 0;
            foreach (var item in cbMarca.ItemsSource)
            {
                MarcaModel marca = (MarcaModel)item;
                if (marca.Marca.Equals(_produto.Marca.Marca))
                {
                    cbMarca.SelectedIndex = i;
                    break;
                }
                else
                    i++;
            }
            i = 0;
            foreach (var item in cbTipo.ItemsSource)
            {
                TipoProdutoModel tipo = (TipoProdutoModel)item;
                if (tipo.Tipo.Equals(_produto.Tipo.Tipo))
                {
                    cbTipo.SelectedIndex = i;
                    break;
                }
                else
                    i++;
            }
            i = 0;
            foreach (var item in cbUnidade.ItemsSource)
            {
                if (_produto.Unidade.Equals(item.ToString()))
                {
                    cbUnidade.SelectedIndex = i;
                    break;
                }
                else
                    i++;
            }
        }
        private void Fechar(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Mensagens.GeraMensagens("Deseja fechar?", MENSAGEM.FECHAR_TELA_CONFIRMA, null, TIPOS_MENSAGENS.QUESTAO))
            {
                e.Cancel = false;
                new Produtos().Show();
            }
            else
                e.Cancel = true;
        }
    }
}
