using BLL.Banco;
using FirstFloor.ModernUI.Windows.Controls;
using MODEL.Banco;
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
    /// Interaction logic for ContasPagar.xaml
    /// </summary>
    public partial class ContasPagar : ModernWindow
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
        public ContasPagar()
        {
            InitializeComponent();
        }
        private void NovoContasPagar(object sender, RoutedEventArgs e)
        {
            new ContasPagarCadastro().Show();
            this.Close();
        }
        private void Pesquisar(object sender, RoutedEventArgs e)
        {
            var model = new ContasPagarModel();
            if ((bool)cbAtivoSim.IsChecked)
                model.Status = "S";
            else
                model.Status = "N";

            model.DataEmissao = tbDataDe.DisplayDate;
            var data = tbDataAte.DisplayDate;

            gridPesquisa.ItemsSource = ContasPagarBLL.Pesquisar(model,data);
        }
    }
}
