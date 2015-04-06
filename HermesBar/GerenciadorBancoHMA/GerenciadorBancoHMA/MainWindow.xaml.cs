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

namespace GerenciadorBancoHMA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void CriarBanco(object sender, RoutedEventArgs e)
        {
            new CriarBanco().Show();
        }
        private void ResetarBanco(object sender, RoutedEventArgs e)
        {
            Connection conn = new Connection();
            MessageBox.Show(conn.ResetarBanco());
        }
        private void CriarBancoTestes(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(new Connection().CriarBancoTeste());
        }
        private void CriarTabelas(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(new Connection().CriarTabelas());
        }
    }
}
