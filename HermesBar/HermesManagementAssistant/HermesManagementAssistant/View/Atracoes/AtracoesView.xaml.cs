﻿using BLL.Atracoes;
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
            dgPesquisa.ItemsSource = AtracoesBLL.ListarAtracoes();
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

            atracoes.Nome = tbNome.Text != null ? tbNome.Text : null;
            atracoes.Estilo = cbTipo.SelectionBoxItem.ToString();
            
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
        }
    }
}
