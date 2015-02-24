﻿using HermesManagementAssistant.Utils;
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

namespace HermesManagementAssistant.View.Fornecedor
{
    /// <summary>
    /// Interaction logic for FornecedorCadastro.xaml
    /// </summary>
    public partial class FornecedorCadastro : Window
    {
        public FornecedorCadastro()
        {
            InitializeComponent();
        }
        public void Salvar(object sender, RoutedEventArgs e)
        {

        }
        private void CpfCnpjMasked(object sender, KeyEventArgs e)
        {
            Mascaras.CnpjCpfMasked(tbCpfCnpj, e);
            tbCpfCnpj.SelectionStart = tbCpfCnpj.Text.Length + 1;
        }
    }
}
