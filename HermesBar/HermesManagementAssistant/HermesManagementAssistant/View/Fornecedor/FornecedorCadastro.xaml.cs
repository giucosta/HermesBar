using BLL.Comum;
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
        private EnderecoBLL _enderecoBLL = null;
        public EnderecoBLL EnderecoBLL
        {
            get
            {
                if (_enderecoBLL == null)
                    _enderecoBLL = new EnderecoBLL();
                return _enderecoBLL;
            }
        }
        public FornecedorCadastro()
        {
            InitializeComponent();
            cbEstado.ItemsSource = EnderecoBLL.CarregaEstados();
        }
        public FornecedorCadastro(FornecedorModel forn)
        {
            InitializeComponent();
            cbEstado.ItemsSource = EnderecoBLL.CarregaEstados();
            PreencheTelaFornecedor(forn);
        }
        public void Salvar(object sender, RoutedEventArgs e)
        {
            if (FornecedorBLL.Salvar(CarregaFornecedor()))
                MessageBox.Show("Gravou");
        }
        
        #region Preenche Model
        private FornecedorModel CarregaFornecedor()
        {
            var fornecedor = new FornecedorModel();
            fornecedor.RazaoSocial = tbRazaoSocial.Text;
            fornecedor.Cpj = tbCpfCnpj.Text;
            fornecedor.InscricaoEstadual = tbInscricaoEstadual.Text;
            fornecedor.Contato = CarregaContatoFornecedor();
            fornecedor.Endereco = CarregaEnderecoFornecedor();

            return fornecedor;
        }
        private ContatoModel CarregaContatoFornecedor()
        {
            var contato = new ContatoModel();
            contato.Nome = tbNome.Text;
            contato.Telefone = tbTelefone.Text;
            contato.Celular = tbCelular.Text;
            contato.Email = tbEmail.Text;

            return contato;
        }
        private EnderecoModel CarregaEnderecoFornecedor()
        {
            var endereco = new EnderecoModel();
            endereco.Rua = tbRua.Text;
            endereco.Cep = tbCep.Text;
            endereco.Bairro = tbBairro.Text; 
            endereco.Cidade = tbCidade.Text;
            endereco.Estado = cbEstado.SelectionBoxItem.ToString();
            endereco.Numero = tbNumero.Text;
            endereco.Tipo = new TipoEnderecoModel(){
                Tipo = Constantes.ATipoEndereco.MATRIZ
            };
            return endereco;
        }
        private void PreencheTelaFornecedor(FornecedorModel forn)
        {
            tbBairro.Text = forn.Endereco.Bairro;
            tbCep.Text = forn.Endereco.Cep;
            tbCidade.Text = forn.Endereco.Cidade;
            tbCpfCnpj.Text = forn.Cpj;
            tbInscricaoEstadual.Text = forn.InscricaoEstadual;
            tbRazaoSocial.Text = forn.RazaoSocial;
            tbRua.Text = forn.Endereco.Rua;
        }
        #endregion

        #region Masked
        private void CpfCnpjMasked(object sender, KeyEventArgs e)
        {
            Mascaras.CnpjCpfMasked(tbCpfCnpj, e);
            tbCpfCnpj.SelectionStart = tbCpfCnpj.Text.Length + 1;
        }
        private void TelefoneMasked(object sender, KeyEventArgs e)
        {
            Mascaras.PhoneMasked(tbTelefone, e);
        }
        private void CelularMasked(object sender, KeyEventArgs e){
            Mascaras.PhoneMasked(tbCelular, e);
        }
        #endregion
    }
}
