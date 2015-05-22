using BLL.Cliente;
using FirstFloor.ModernUI.Windows.Controls;
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
        public static List<string> bandeiras = Constantes.ABandeiraCartao.BandeirasCartao;
        private double totalPagar;
        private FechamentoModel fech;
        private double valorEntrada;
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
        }
        private void CalculaDesconto(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbDesconto.Text))
            {
                var valorDesconto = Convert.ToDouble(tbDesconto.Text);
                if (valorDesconto > 0)
                {
                    totalPagar = totalPagar - valorDesconto;
                    if (totalPagar > 0)
                        lbTotalPagar.Content = "TOTAL A PAGAR: " + totalPagar.ToString("C");
                    else
                        lbTotalPagar.Content = "TROCO: " + totalPagar.ToString("C");
                }
            }
            else
            {
                totalPagar = fech.ValorTotal + valorEntrada;
                lbTotalPagar.Content = "TOTAL A PAGAR:" + totalPagar.ToString("C");
            }    
        }
    }
}
