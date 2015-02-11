﻿using BLL.Funcionario;
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
        public FuncionarioBLL BLL
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
            gridPesquisa.ItemsSource = BLL.Pesquisa(new FuncionarioModel() { Nome = "" });
        }

        private void PesquisarFuncionario(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(tbCodigo.Text))
                gridPesquisa.ItemsSource = BLL.Pesquisa(new FuncionarioModel() { Nome = tbNome.Text , Id = int.Parse(tbCodigo.Text) });
            else
                gridPesquisa.ItemsSource = BLL.Pesquisa(new FuncionarioModel() { Nome = tbNome.Text });
        }

        private void NovoFuncionario(object sender, RoutedEventArgs e)
        {
            new FuncionarioCadastro().Show();
            this.Close();
        }
    }
}
