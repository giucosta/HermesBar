using BLL.Funcionario;
using MODEL;
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

namespace HermesManagementAssistant.View.Funcionario
{
    /// <summary>
    /// Interaction logic for Funcionario.xaml
    /// </summary>
    public partial class Funcionario : Window
    {
        private FuncionarioBLL _funcionarioBLL = null;
        public FuncionarioBLL FuncionarioBLL
        {
            get
            {
                if (_funcionarioBLL == null)
                    _funcionarioBLL = new FuncionarioBLL();
                return _funcionarioBLL;
            }
        }
        public Funcionario()
        {
            InitializeComponent();
            gridPesquisa.ItemsSource = FuncionarioBLL.Pesquisa(new FuncionarioModel());
        }
        private void PesquisarFuncionario(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbCodigo.Text))
                gridPesquisa.ItemsSource = FuncionarioBLL.Pesquisa(new FuncionarioModel() { Id = int.Parse(tbCodigo.Text), Nome = tbNome.Text });
            else
                gridPesquisa.ItemsSource = FuncionarioBLL.Pesquisa(new FuncionarioModel() { Nome = tbNome.Text });
        }
        private void NovoFuncionario(object sender, RoutedEventArgs e)
        {
            new FuncionarioCadastro().Show();
            this.Close();
        }
        private void Editar(object sender, SelectionChangedEventArgs e)
        {
            DataGrid data  = (DataGrid)sender;
            new FuncionarioCadastro((FuncionarioModel)data.SelectedItems[0]).Show();
        }
    }
}
