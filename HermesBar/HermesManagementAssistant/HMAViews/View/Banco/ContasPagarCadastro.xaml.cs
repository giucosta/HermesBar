using BLL.Banco;
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

namespace HMAViews.View.Banco
{
    /// <summary>
    /// Interaction logic for ContasPagarCadastro.xaml
    /// </summary>
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
        private void CarregaCampos()
        {
            tbFornecedor.ItemsSource = FornecedorBLL.Pesquisar(new FornecedorModel());
            cbFormaPagamento.ItemsSource = FormaPagamentoBLL.RetornaFormas();
        }
        private void Salvar(object sender, RoutedEventArgs e)
        {

        }
    }
}
