using FirstFloor.ModernUI.Windows.Controls;
using HermesManagementAssistant.View.Fornecedor;
using HermesManagementAssistant.View.Funcionario;
using HermesManagementAssistant.View.Produtos;
using HMAViews.View;
using HMAViews.View.Atracoes;
using HMAViews.View.Estabelecimento;
using HMAViews.View.Fornecedor;
using HMAViews.View.Produtos;
using Microsoft.Win32;
using MODEL;
using MODEL.Fornecedor;
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
using Utils.Mensagens;
using UTILS;

namespace HMAViews
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
        }
        public void Atracoes(object sender, RoutedEventArgs e)
        {
            new Atracoes().Show();
        }
        public void Estabelecimento(object sender, RoutedEventArgs e)
        {
            new Estabelecimento().Show();
        }
        public void Fornecedor(object sender, RoutedEventArgs e)
        {
            new HMAViews.View.Fornecedor.Fornecedor().Show();
        }
        public void Funcionarios(object sender, RoutedEventArgs e)
        {
            new Funcionario().Show();
        }
        public void Produtos(object sender, RoutedEventArgs e)
        {
            new Produtos().Show();
        }
        public void TipoProduto(object sender, RoutedEventArgs e)
        {
            new TipoProduto().Show();
        }
        public void ImportarNfe(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = false;
            open.Filter = "XML|*.xml";

            if ((bool)open.ShowDialog())
            {
                XmlReader xmlReader = new XmlReader();
                var fornecedor = xmlReader.ImportXml(open.FileName, open.OpenFile());
                if (fornecedor != null)
                {
                    var forn = new FornecedorModel();
                    forn.RazaoSocial = fornecedor.RazaoSocial;
                    forn.Cpj = fornecedor.Cpj;
                    forn.InscricaoEstadual = fornecedor.InscricaoEstadual;
                    forn.Endereco = new EnderecoModel() { 
                        Rua = fornecedor.Endereco.Rua, 
                        Bairro = fornecedor.Endereco.Bairro,
                        Cep = fornecedor.Endereco.Cep,
                        Cidade = fornecedor.Endereco.Cidade
                    };
                    new HermesManagementAssistant.View.Fornecedor.FornecedorCadastro(forn).Show();
                }
                else
                    Mensagens.GeraMensagens("Importar XML", MENSAGEM.ARQUIVO_JA_EXPORTADO, null, TIPOS_MENSAGENS.ALERTA);
            }
        }
    }
}
