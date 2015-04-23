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
        }
        private void Salvar(object sender, RoutedEventArgs e)
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

            if (ContasPagarBLL.Salvar(contasPagar))
                MessageBox.Show("deu");
            else
                MessageBox.Show("Ops");
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
    }
}