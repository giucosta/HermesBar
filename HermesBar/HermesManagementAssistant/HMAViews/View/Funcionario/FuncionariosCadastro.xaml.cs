using BLL.Comum;
using BLL.Funcionario;
using FirstFloor.ModernUI.Windows.Controls;
using HMAViews.Mascara;
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

namespace HMAViews.View.Funcionario
{
    /// <summary>
    /// Interaction logic for FuncionariosCadastro.xaml
    /// </summary>
    public partial class FuncionariosCadastro : ModernWindow
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
        private EnderecoBLL _enderecoBLL = null;
        public EnderecoBLL EnderecoBLL
        {
            get
            {
                if (_enderecoBLL == null)
                    _enderecoBLL = new EnderecoBLL();
                return _enderecoBLL;
            }
        }
        private TipoFuncionarioBLL _tipoFuncionarioBLL = null;
        public TipoFuncionarioBLL TipoFuncionarioBLL
        {
            get
            {
                if (_tipoFuncionarioBLL == null)
                    _tipoFuncionarioBLL = new TipoFuncionarioBLL();
                return _tipoFuncionarioBLL;
            }
        }
        public FuncionariosCadastro()
        {
            InitializeComponent();
            CarregaTela();
        }

        private void CarregaTela()
        {
            cbEstado.ItemsSource = EnderecoBLL.CarregaEstados();
            cbTipo.ItemsSource = TipoFuncionarioBLL.RetornaTipos();
        }
        private void MascaraCpf(object sender, KeyEventArgs e)
        {
            Mascaras.CpfMasked(tbCpf, e);
        }
        private void MascaraCartTrabalho(object sender, KeyEventArgs e)
        {
            Mascaras.SomenteNumeros(tbCartTrabalho, e);
        }
        private void MascaraSerie(object sender, KeyEventArgs e)
        {
            Mascaras.SomenteNumeros(tbSerie, e);
        }
    }
}
