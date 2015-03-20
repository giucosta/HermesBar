using BLL.Atracoes;
using MODEL;
using MODEL.Atracoes;
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
            CarregaDataGrid(new AtracoesModel() { Nome = "", Estilo = new EstiloAtracoesModel() });
            CarregaComboBox();
        }
        private void CarregaDataGrid(AtracoesModel atracoes)
        {
            dgPesquisa.ItemsSource = AtracoesBLL.Pesquisa(atracoes);    
        }
        private void CarregaComboBox()
        {
            cbTipo.ItemsSource = AtracoesBLL.RecuperaEstilos();
        }
        private void btPesquisar_Click(object sender, RoutedEventArgs e)
        {
            var atracoes = new AtracoesModel();

            if (cbTipo.SelectedValue == null)
                atracoes.Estilo = new EstiloAtracoesModel();
            else
                atracoes.Estilo = new EstiloAtracoesModel() { Id = (int)cbTipo.SelectedValue };

            atracoes.Nome = tbNome.Text;
            
            CarregaDataGrid(atracoes);
        }
        private void btNovo_Click(object sender, RoutedEventArgs e)
        {
            new CadastroAtracoesView().Show();
        }
        private void Editar(Object sender, MouseButtonEventArgs e)
        {
            DataGrid data = (DataGrid)sender;
            new CadastroAtracoesView((AtracoesModel)data.SelectedItems[0]).Show();
            this.Close();
        }
    }
}
