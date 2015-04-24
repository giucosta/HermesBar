using BLL.Banco;
using BLL.Fornecedor;
using FirstFloor.ModernUI.Windows.Controls;
using HMAViews.Mascara;
using HMAViews.Utils;
using MODEL.Banco;
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

namespace HMAViews.View.Banco
{
    public partial class ContasPagarCadastro : ModernWindow
    {
        private ContasPagarBLL _contasPagarBLL = null;
        public ContasPagarBLL ContasPagarBLL
        {
            get
            {
                if (_contasPagarBLL == null)
                    _contasPagarBLL = new ContasPagarBLL();
                return _contasPagarBLL;
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
        private FormaPagamentoBLL _formaPagamentoBLL = null;
        public FormaPagamentoBLL FormaPagamentoBLL
        {
            get
            {
                if (_formaPagamentoBLL == null)
                    _formaPagamentoBLL = new FormaPagamentoBLL();
                return _formaPagamentoBLL;
            }
        }
        private CentroCustoBLL _centroCustoBLL = null;
        public CentroCustoBLL CentroCustoBLL
        {
            get
            {
                if (_centroCustoBLL == null)
                    _centroCustoBLL = new CentroCustoBLL();
                return _centroCustoBLL;
            }
        }
        public ContasPagarCadastro()
        {
            InitializeComponent();
            CarregaCampos();
        }
        public ContasPagarCadastro(ContasPagarModel contasPagar)
        {
            InitializeComponent();
            btCancelar.Visibility = System.Windows.Visibility.Visible;
        }
        private void CarregaCampos()
        {
            tbFornecedor.ItemsSource = FornecedorBLL.Pesquisar(new FornecedorModel());
            cbFormaPagamento.ItemsSource = FormaPagamentoBLL.RetornaFormas();
            cbCentroCusto.ItemsSource = CentroCustoBLL.GetAllCentroCusto(new CentroCustoModel());
            dpDataEmissao.DisplayDate = DateTime.Now;
            dpDataVenc.DisplayDate = DateTime.Now.AddDays(30);
        }
        private void Salvar(object sender, RoutedEventArgs e)
        {
            var camposObrigatorios = VerificaCamposObrigatorios();
            if (camposObrigatorios.Count == 0)
            {
                var contasPagar = new ContasPagarModel();
                contasPagar.DataCadastro = DateTime.Now;
                contasPagar.DataEmissao = dpDataEmissao.DisplayDate;
                contasPagar.DataVencimento = dpDataVenc.DisplayDate;
                contasPagar.FormaPagamento = cbFormaPagamento.SelectionBoxItem.ToString();
                contasPagar.Fornecedor = (FornecedorModel)tbFornecedor.SelectedItem;
                contasPagar.NumeroNota = tbNumeroNota.Text;
                contasPagar.Observacao = tbObservacao.Text;
                contasPagar.Parcelas = tbParcelas.Text;
                contasPagar.Referente = tbReferente.Text;
                contasPagar.Valor = tbValor.Text;
                contasPagar.ValorPago = "0";
                contasPagar.Status = "S";
                contasPagar.CentroCusto = (CentroCustoModel)cbCentroCusto.SelectedItem;

                if (ContasPagarBLL.Salvar(contasPagar))
                    Mensagens.GeraMensagens("Conta cadastrada!", MENSAGEM.CONTASPAGAR_CADASTRO_SUCESSO, null, TIPOS_MENSAGENS.SUCESSO);
                else
                    Mensagens.GeraMensagens("Erro ao inserir!", MENSAGEM.CONTASPAGAR_CADASTRO_ERRO, TIPOS_MENSAGENS.ERRO);
            }
            else
                Mensagens.GeraMensagens("Campos Obrigatórios", MENSAGEM.CAMPOS_OBRIGATORIOS, camposObrigatorios, TIPOS_MENSAGENS.ALERTA);
            
        }
        private void AlterarFormaPagamento(object sender, SelectionChangedEventArgs e)
        {
            if (cbFormaPagamento.SelectedItem.ToString().Equals("Crédito") || cbFormaPagamento.SelectedItem.ToString().Equals("Boleto"))
                tbParcelas.IsReadOnly = false;
            else
            {
                tbParcelas.Clear();
                tbParcelas.IsReadOnly = true;
            }
        }
        private void MascaraParcelas(object sender, KeyEventArgs e)
        {
            Mascaras.SomenteNumeros(tbParcelas, e);
        }
        private void MascaraValor(object sender, RoutedEventArgs e)
        {
            var text = tbValor.Text;
            if (Verificadores.VerificaNumero(text))
                tbValor.Text = text.Replace(".", "");
        }
        private List<string> VerificaCamposObrigatorios()
        {
            var list = new List<string>();
            if (string.IsNullOrEmpty(dpDataEmissao.Text))
                list.Add("Data Emissão");
            if (string.IsNullOrEmpty(dpDataVenc.Text))
                list.Add("Data Vencimento");
            if (string.IsNullOrEmpty(cbFormaPagamento.SelectionBoxItem.ToString()))
                list.Add("Forma de Pagamento");
            if (string.IsNullOrEmpty(tbFornecedor.SelectionBoxItem.ToString()))
                list.Add("Fornecedor");
            if (string.IsNullOrEmpty(tbValor.Text))
                list.Add("Valor");

            return list;
        }
    }
}