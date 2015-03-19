using BLL.Atracoes;
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

namespace HermesManagementAssistant.View.Atracoes
{
    public partial class AtracoesView : Window
    {
        private AtracoesBLL _atracoesBLL = null;
        public AtracoesBLL AtracoesBLL
        {
            get
            {
                if (_atracoesBLL == null)
                    _atracoesBLL = new AtracoesBLL();
                return _atracoesBLL;
            }
        }
        public AtracoesView()
        {
            InitializeComponent();
            CarregaDataGrid(new AtracoesModel() { Nome = "", Estilo = "" });
            CarregaComboBox();
        }
        private void CarregaDataGrid(AtracoesModel atracoes)
        {
            if (string.IsNullOrEmpty(atracoes.Nome) && string.IsNullOrEmpty(atracoes.Estilo))
                dgPesquisa.ItemsSource = AtracoesBLL.Pesquisa();
            else
                dgPesquisa.ItemsSource = AtracoesBLL.Pesquisa(atracoes);
        }
        private void CarregaComboBox()
        {
            cbTipo.ItemsSource = AtracoesBLL.RecuperaEstilos();
            cbTipo.SelectedIndex = 0;
        }
        private void btPesquisar_Click(object sender, RoutedEventArgs e)
        {
            var atracoes = new AtracoesModel();

            if (string.IsNullOrWhiteSpace(cbTipo.SelectionBoxItem.ToString()))
                atracoes.Estilo = "";
            else
                atracoes.Estilo = cbTipo.SelectionBoxItem.ToString();

            atracoes.Nome = tbNome.Text;
            
            CarregaDataGrid(atracoes);
        }
        private void btNovo_Click(object sender, RoutedEventArgs e)
        {
            new CadastroAtracoesView().Show();
        }
        private void Editar(Object sender, SelectionChangedEventArgs e)
        {
            DataGrid data = (DataGrid)sender;
            new CadastroAtracoesView((AtracoesModel)data.SelectedItems[0]).Show();
            this.Close();
        }
    }
}
