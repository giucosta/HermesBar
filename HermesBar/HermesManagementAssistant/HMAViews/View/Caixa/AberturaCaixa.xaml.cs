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

namespace HMAViews.View.Caixa
{
    /// <summary>
    /// Interaction logic for EntradaCliente.xaml
    /// </summary>
    public partial class AberturaCaixa : ModernWindow
    {
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
            new CaixaAberto().Show();
        }
    }
}
