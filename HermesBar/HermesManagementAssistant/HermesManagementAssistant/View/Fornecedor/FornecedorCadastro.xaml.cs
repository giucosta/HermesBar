using BLL.Fornecedor;
using HermesManagementAssistant.Utils;
using MODEL;
using MODEL.Fornecedor;
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
using UTILS;

namespace HermesManagementAssistant.View.Fornecedor
{
    /// <summary>
    /// Interaction logic for FornecedorCadastro.xaml
    /// </summary>
    public partial class FornecedorCadastro : Window
    {
        private FornecedorBLL _fornecedorBLL = null;
        public FornecedorBLL FornecedorBLL
        {
            get
            {
                if (_fornecedorBLL == null)
                    _fornecedorBLL = new FornecedorBLL();
                return _fornecedorBLL;
            }
        }
        public FornecedorCadastro()
        {
            InitializeComponent();
        }
        public void Salvar(object sender, RoutedEventArgs e)
        {
            if (FornecedorBLL.Salvar(CarregaFornecedor()))
                MessageBox.Show("Gravou");
        }
        private void CpfCnpjMasked(object sender, KeyEventArgs e)
        {
            Mascaras.CnpjCpfMasked(tbCpfCnpj, e);
            tbCpfCnpj.SelectionStart = tbCpfCnpj.Text.Length + 1;
        }
        private FornecedorModel CarregaFornecedor()
        {
            var fornecedor = new FornecedorModel();
            fornecedor.Cnpj = tbCpfCnpj.Text;
            fornecedor.Contato = new ContatoModel() { Telefone = tbTelefone.Text, Celular = tbCelular.Text, Email = tbEmail.Text };
            fornecedor.Endereco = new EnderecoModel() { Rua = tbRua.Text, Cep = tbCep.Text, Bairro = tbBairro.Text, Cidade = tbCidade.Text, Estado = tbEstado.Text, Numero = tbNumero.Text, Tipo = new TipoEnderecoModel(){Tipo = Constantes.ATipoEndereco.MATRIZ}};
            fornecedor.InscricaoEstadual = tbInscricaoEstadual.Text;
            fornecedor.RazaoSocial = tbRazaoSocial.Text;

            return fornecedor;

        }
    }
}
