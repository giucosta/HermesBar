using BLL.Caixa;
using FirstFloor.ModernUI.Windows.Controls;
using HMAViews.Mascara;
using MODEL.Caixa;
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

namespace HMAViews.View.Caixa
{
    /// <summary>
    /// Interaction logic for EntradaCliente.xaml
    /// </summary>
    public partial class AberturaCaixa : ModernWindow
    {
        private CaixaAbertoBLL _caixaAbertoBLL = null;
        public CaixaAbertoBLL CaixaAbertlBLL
        {
            get
            {
                if (_caixaAbertoBLL == null)
                    _caixaAbertoBLL = new CaixaAbertoBLL();
                return _caixaAbertoBLL;
            }
        }
        public AberturaCaixa()
        {
            InitializeComponent();
        }
        private void MascaraSaldoInicial(object sender, KeyEventArgs e)
        {
            Mascaras.SomenteNumeros(tbSaldoInicial, e);
        }
        private void AbrirCaixa(object sender, RoutedEventArgs e)
        {
            var caixa = new CaixaModel();
            caixa.ValorEntrada = Convert.ToDouble(tbSaldoInicial.Text);
            caixa.ObservacaoAbertura = tbObservacao.Text;
            CaixaAbertlBLL.AbrirCaixa(caixa);

            new CaixaAberto().Show();
        }
    }
}
