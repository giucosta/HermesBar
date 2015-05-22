using BLL.Caixa;
using BLL.Cliente;
using FirstFloor.ModernUI.Windows.Controls;
using MODEL.Caixa;
using MODEL.Pedido;
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
using UTIL;

namespace HMAViews.View.Saida
{
    /// <summary>
    /// Interaction logic for FecharComanda.xaml
    /// </summary>
    public partial class FecharComanda : ModernWindow
    {
        private ClienteBLL _clienteBLL = null;
        public ClienteBLL ClienteBLL
        {
            get
            {
                if (_clienteBLL == null)
                    _clienteBLL = new ClienteBLL();
                return _clienteBLL;
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
        public static List<string> bandeiras = Constantes.ABandeiraCartao.BandeirasCartao;
        private double totalPagar;
        private FechamentoModel fech;
        private double valorEntrada;
        private CartaoModel _cartaoModel;
        public FecharComanda(FechamentoModel fechamento)
        {
            InitializeComponent();
            fech = fechamento;

            tbBandeira.ItemsSource = bandeiras;
            tbBandeiraDebito.ItemsSource = bandeiras;
            lbTotalPedido.Content = lbTotalPedido.Content + " " + fechamento.ValorTotal.ToString("C");

            var cliente = fechamento.Pedido.First();
            if (cliente.NumeroCartao.Cliente.Sexo == "M")
                valorEntrada = 20.00;
            else
                valorEntrada = 15.00;

            lbEntrada.Content = lbEntrada.Content + valorEntrada.ToString("C");
            totalPagar = valorEntrada + fechamento.ValorTotal;

            lbTotalPagar.Content = lbTotalPagar.Content + totalPagar.ToString("C");

            _cartaoModel = cliente.NumeroCartao;
            _cartaoModel.ValorTotal = totalPagar;
        }
        private void CalculaDesconto(object sender, RoutedEventArgs e)
        {
            CalculaValor(tbDesconto);
        }
        private void CalculaPagamentoDinheiro(object sender, RoutedEventArgs e)
        {
            CalculaValor(tbDinheiro);
            _cartaoModel.FormaPagamento = Constantes.ATipoPagamento.Dinheiro;
        }
        private void CalculaPagamentoCredito(object sender, RoutedEventArgs e)
        {
            CalculaValor(tbValorCredito);
            _cartaoModel.FormaPagamento = Constantes.ATipoPagamento.CartaoCredito;
        }
        private void CalculaPagamentoDebito(object sender, RoutedEventArgs e)
        {
            CalculaValor(tbValorDebito);
            _cartaoModel.FormaPagamento = Constantes.ATipoPagamento.CartaoDebito;
        }
        private void CalculaPagamentoCheque(object sender, RoutedEventArgs e)
        {
            CalculaValor(tbCheque);
            _cartaoModel.FormaPagamento = Constantes.ATipoPagamento.Cheque;
        }
        private void FecharComandas(object sender, RoutedEventArgs e)
        {
            if (CartaoBLL.FecharComanda(_cartaoModel)){
                MessageBox.Show("Comanda fechada com sucesso!");
                new SaidaCliente().Show();
                this.Close();
            }
            else
                MessageBox.Show("Deu erro nas parada tudo");
        }
        private void CalculaValor(TextBox textBox)
        {
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                double pagamentoDinheiro = Convert.ToDouble(textBox.Text);
                totalPagar = totalPagar - pagamentoDinheiro;
                if (totalPagar > 0)
                    lbTotalPagar.Content = "FALTANDO: " + totalPagar.ToString("C");
                if (totalPagar < 0)
                    lbTotalPagar.Content = "TROCO: " + totalPagar.ToString("C");
                if (totalPagar == 0)
                    lbTotalPagar.Content = totalPagar.ToString("C");
            }
            else
            {
                totalPagar = fech.ValorTotal + valorEntrada;
                lbTotalPagar.Content = "TOTAL A PAGAR:" + totalPagar.ToString("C");
            }   
        }
    }
}
