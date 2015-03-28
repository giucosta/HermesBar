using HMAViews.View.Funcionario;
using HMAViews.View.Usuario;
using HMAViews.View.Produtos;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using Microsoft.Win32;
using System.IO;
using BLL.Fornecedor;
using UTIL;


namespace HMAViews
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            if (Session.Usuario.Perfil.IdPerfil == Constantes.APerfil.ADMINISTRADOR)
                InitializeComponent();
        }

        private void PesquisaUsuario(object sender, RoutedEventArgs e)
        {
            new Usuario().Show();
        }
        private void PesquisaAtracoes(object sender, RoutedEventArgs e)
        {
            
        }
        private void PesquisaFuncionarios(object sender, RoutedEventArgs e)
        {
            new Funcionario().Show();
        }
        private void PesquisaEstabelecimento(object sender, RoutedEventArgs e)
        {
            
        }
        private void PesquisaProdutos(object sender, RoutedEventArgs e)
        {
            new Produto().Show();
        }
        private void PesquisaTipoProduto(object sender, RoutedEventArgs e)
        {
            new TipoProduto().Show();
        }
        private void PesquisaFornecedor(object sender, RoutedEventArgs e)
        {
            
        }
        private void ImportarNFe(Object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = false;
            open.Filter = "XML|*.xml";

            if ((bool)open.ShowDialog())
            {
                XmlReader xmlReader = new XmlReader();
                var fornecedor = xmlReader.ImportXml(open.FileName,open.OpenFile());
            }
        }
    }
}
