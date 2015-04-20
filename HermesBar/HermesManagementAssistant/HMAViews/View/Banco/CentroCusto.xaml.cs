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
    /// Interaction logic for CentroCusto.xaml
    /// </summary>
    public partial class CentroCusto : ModernWindow
    {
        private CentroCustoBLL _centroCustoBLL = null;
        public CentroCustoBLL CentroCustoBLL
        {
            get
            {
                if (_centroCustoBLL == null)
                    _centroCustoBLL = new CentroCustoBLL();
                return _centroCustoBLL;
            }
        }
        public CentroCusto()
        {
            InitializeComponent();
            gridPesquisa.ItemsSource = CentroCustoBLL.GetAllCentroCusto(new CentroCustoModel());
        }
        private void NovoCentroCusto(object sender, RoutedEventArgs e)
        {
            new CadastroCentroCusto().Show();
            this.Close();
        }
        private void Pesquisar(object sender, RoutedEventArgs e)
        {
            var centroCusto = new CentroCustoModel();
            centroCusto.Nome = tbNome.Text;
            centroCusto.Codigo = tbCodigo.Text;
            gridPesquisa.ItemsSource = CentroCustoBLL.GetAllCentroCusto(centroCusto);
        }
        private void Editar(object sender, MouseButtonEventArgs e)
        {
            DataGrid data = (DataGrid)sender;
            new CadastroCentroCusto((CentroCustoModel)data.SelectedItems[0]).Show();
            this.Close();
        }

    }
}
