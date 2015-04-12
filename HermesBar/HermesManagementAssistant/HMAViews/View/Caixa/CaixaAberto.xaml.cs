using BLL.Caixa;
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

namespace HMAViews.View.Caixa
{
    /// <summary>
    /// Interaction logic for CaixaAberto.xaml
    /// </summary>
    public partial class CaixaAberto : ModernWindow
    {
        private CaixaAbertoBLL _caixaAbertoBLL = null;
        public CaixaAbertoBLL CaixaAbertoBLL
        {
            get
            {
                if (_caixaAbertoBLL == null)
                    _caixaAbertoBLL = new CaixaAbertoBLL();
                return _caixaAbertoBLL;
            }
        }
        public CaixaAberto()
        {
            InitializeComponent();
            movimentacoes.ItemsSource = CaixaAbertoBLL.RetornaMovimentacoes();
        }
    }
}
