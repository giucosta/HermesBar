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

namespace HMAPedidos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }
        private void CarregaCliente(object sender, RoutedEventArgs e)
        {
            lbNumeroCartao.Content = tbNumeroCartao.Text;
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
    }
}
