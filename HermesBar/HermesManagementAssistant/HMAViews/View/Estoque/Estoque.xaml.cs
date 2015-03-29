using BLL.Produtos;
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

namespace HMAViews.View.Estoque
{
    /// <summary>
    /// Interaction logic for Estoque.xaml
    /// </summary>
    public partial class Estoque : ModernWindow
    {
        private TipoProdutoBLL _tipoProdutoBLL = null;
        public TipoProdutoBLL TipoProdutoBLL
        {
            get
            {
                if (_tipoProdutoBLL == null)
                    _tipoProdutoBLL = new TipoProdutoBLL();
                return _tipoProdutoBLL;
            }
        }
        public Estoque()
        {
            InitializeComponent();
        }
        private void CarregaCombos()
        {
            cbTipoProduto.ItemsSource = TipoProdutoBLL.RetornaTipos();
        }
    }
}
