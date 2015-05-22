using BLL.Caixa;
using BLL.Pedido;
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

namespace HMAViews.View.Saida
{
    /// <summary>
    /// Interaction logic for SaidaCliente.xaml
    /// </summary>
    public partial class SaidaCliente : ModernWindow
    {
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
        private PedidoBLL _pedidoBLL = null;
        public PedidoBLL PedidoBLL
        {
            get
            {
                if (_pedidoBLL == null)
                    _pedidoBLL = new PedidoBLL();
                return _pedidoBLL;
            }
        }
        private FechamentoModel _fechamento = null;
        public SaidaCliente()
        {
            InitializeComponent();
        }
        private void PesquisarCartao(object sender, RoutedEventArgs e)
        {
            _fechamento = PedidoBLL.PesquisaFechamento(new PedidoModel() { NumeroCartao = new CartaoModel() { NumeroCartao = tbNumeroCartao.Text } });
            if (_fechamento.Pedido.Count > 0)
            {
                
                gridPesquisa.ItemsSource = _fechamento.Pedido;
                _fechamento.ValorTotal = 0;
                foreach (var item in _fechamento.Pedido)
                    _fechamento.ValorTotal += (Convert.ToDouble(item.Quantidade) * item.CodigoProduto.ValorVenda);

                tbTotal.Text = _fechamento.ValorTotal.ToString("C");
            }    
            else
                MessageBox.Show("Este cliente não possui pedidos!");
        }
        private void FecharComanda(object sender, RoutedEventArgs e)
        {
            new FecharComanda(_fechamento).Show();
            this.Close();
        }
    }
}
