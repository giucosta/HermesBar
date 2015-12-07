using BLL.Atracoes;
using FirstFloor.ModernUI.Windows.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HMAViews.View.Atracoes
{
    /// <summary>
    /// Interaction logic for Atracoes.xaml
    /// </summary>
    public partial class Atracoes : ModernWindow
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
        public Atracoes()
        {
            InitializeComponent();
            CarregaDataGrid(new AtracoesModel() { Nome = "", Estilo = new EstiloAtracoesModel() });
            CarregaComboBox();
            tbNome.Focus();
        }
        private void CarregaDataGrid(AtracoesModel atracoes)
        {
            dgPesquisa.ItemsSource = AtracoesBLL.Pesquisa(atracoes);    
        }
        private void CarregaComboBox()
        {
            cbTipo.ItemsSource = AtracoesBLL.RecuperaEstilos();
        }
        private void Pesquisa(object sender, RoutedEventArgs e)
        {
            var atracoes = new AtracoesModel();

            if (cbTipo.SelectedValue == null)
                atracoes.Estilo = new EstiloAtracoesModel();
            else
                atracoes.Estilo = new EstiloAtracoesModel() { Id = (int)cbTipo.SelectedValue };

            atracoes.Nome = tbNome.Text;
            
            CarregaDataGrid(atracoes);
        }
        private void Novo(object sender, RoutedEventArgs e)
        {
            new AtracoesCadastro().Show();
            this.Close();
        }
        private void Editar(Object sender, MouseButtonEventArgs e)
        {
            DataGrid data = (DataGrid)sender;
            new AtracoesCadastro((AtracoesModel)data.SelectedItems[0]).Show();
            this.Close();
        }
    }
}
