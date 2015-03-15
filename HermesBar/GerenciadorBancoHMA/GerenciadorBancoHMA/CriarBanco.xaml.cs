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
using System.Windows.Shapes;

namespace GerenciadorBancoHMA
{
    /// <summary>
    /// Interaction logic for CriarBanco.xaml
    /// </summary>
    public partial class CriarBanco : Window
    {
        public CriarBanco()
        {
            InitializeComponent();
        }
        public void CriarBancoGeral(object sender, RoutedEventArgs e)
        {
            Connection conn = new Connection();
            MessageBox.Show(conn.CriarBanco(tbNomeServidor.Text));
            this.Close();
        }
    }
}
