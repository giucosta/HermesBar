using HermesManagementAssistant.View.Atracoes;
using HermesManagementAssistant.View.Estabelecimento;
using HermesManagementAssistant.View.Funcionario;
using HermesManagementAssistant.View.Login;
using HermesManagementAssistant.View.Usuario;
using HermesManagementAssistant.View.Produtos;
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
using Utils.Mensagens;
using HermesManagementAssistant.View.Fornecedor;
using Microsoft.Win32;
using System.IO;
using BLL.Fornecedor;
using UTILS;


namespace HermesManagementAssistant
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
            else
                Mensagens.GeraMensagens("Permissões",MENSAGEM.SEM_PERMISSAO,null, TIPOS_MENSAGENS.ALERTA);
        }

        private void PesquisaUsuario(object sender, RoutedEventArgs e)
        {
            new Usuario().Show();
        }

        private void PesquisaAtracoes(object sender, RoutedEventArgs e)
        {
            new AtracoesView().Show();
        }

        private void PesquisaFuncionarios(object sender, RoutedEventArgs e)
        {
            new Funcionario().Show();
        }
        private void PesquisaEstabelecimento(object sender, RoutedEventArgs e)
        {
            new Estabelecimento().Show();
        }

        private void PesquisaProdutos(object sender, RoutedEventArgs e)
        {
            new Produto().Show();
        }
        private void PesquisaFornecedor(object sender, RoutedEventArgs e)
        {
            new Fornecedor().Show();
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
                if (fornecedor != null)
                {
                    FornecedorCadastro view = new FornecedorCadastro();
                    view.tbRazaoSocial.Text = fornecedor.RazaoSocial;
                    view.tbCpfCnpj.Text = fornecedor.Cnpj;
                    view.tbInscricaoEstadual.Text = fornecedor.InscricaoEstadual;
                    view.tbRua.Text = fornecedor.Endereco.Rua;
                    view.tbNumero.Text = fornecedor.Endereco.Numero;
                    view.tbBairro.Text = fornecedor.Endereco.Bairro;
                    view.tbCidade.Text = fornecedor.Endereco.Cidade;
                    view.tbCep.Text = fornecedor.Endereco.Cep;
                    view.Show();
                }
                else
                    Mensagens.GeraMensagens("Importar XML",MENSAGEM.ARQUIVO_JA_EXPORTADO,null,TIPOS_MENSAGENS.ALERTA);
            }
        }
    }
}
