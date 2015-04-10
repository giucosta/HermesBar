using BLL.Caixa;
using FirstFloor.ModernUI.Windows.Controls;
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

namespace HMAViews.View.Entrada
{
    /// <summary>
    /// Interaction logic for EntradaCliente.xaml
    /// </summary>
    public partial class EntradaCliente : ModernWindow
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
        public EntradaCliente()
        {
            InitializeComponent();
        }
        private void RegistrarEntrada(object sender, RoutedEventArgs e)
        {
            var model = new CartaoModel();
            model.HoraEntrada = DateTime.Now;
            model.NumeroCartao = tbNumeroCartao.Text;
            model.Cliente = new MODEL.Cliente.ClienteModel() { Nome = tbNome.Text, DataNascimento = DateTime.Parse(tbDataNascimento.Text), RG = tbRG.Text, Contato = new MODEL.ContatoModel() { Nome = tbNome.Text, Telefone = tbTelefone.Text } };

            CartaoBLL.Salvar(model);
        }
    }
}
