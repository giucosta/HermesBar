using BLL.Comum;
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
using HMAViews.Utils;
using UTIL;

namespace HMAViews.View.Funcionario
{
    /// <summary>
    /// Interaction logic for UsuarioCadastro.xaml
    /// </summary>
    public partial class FuncionarioCadastro : Window
    {
        #region Access Method
        private TipoFuncionarioBLL _tipoFuncionarioBLL = null;
        public TipoFuncionarioBLL TipoFuncionarioBLL
        {
            get
            {
                if (_tipoFuncionarioBLL == null)
                    _tipoFuncionarioBLL = new TipoFuncionarioBLL();
                return _tipoFuncionarioBLL;
            }
        }
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
        private ContatoBLL _contatoBLL = null;
        public ContatoBLL ContatoBLL
        {
            get
            {
                if (_contatoBLL == null)
                    _contatoBLL = new ContatoBLL();
                return _contatoBLL;
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
        #endregion
        private static int _idFuncionario = 0;
        private static int _idEndereco = 0;
        private static int _idContato = 0;
        public FuncionarioCadastro()
        {
            InitializeComponent();
            CarregaCombos();
            btExcluir.Visibility = System.Windows.Visibility.Hidden;
        }
        public FuncionarioCadastro(FuncionarioModel funcionario)
        {
            InitializeComponent();
            CarregaFuncionario(funcionario);
            btExcluir.Visibility = System.Windows.Visibility.Visible;
        }
        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (_idFuncionario == 0)
            {
                var camposObrigatorios = VerificaCamposObrigatorios();
                if (camposObrigatorios.Count == 0)
                {
                    var funcionario = SalvarFuncionario();
                    funcionario.Endereco = SalvarEndereco();
                    funcionario.Contato = SalvarContato();

                    if (FuncionarioBLL.Salvar(funcionario))
                    {
                        new Funcionario().Show();
                        this.Close();
                    }
                }
            }
            else
                EditarFuncionario();
        }
        private ContatoModel SalvarContato()
        {
            var contato = new ContatoModel();
            contato.Nome = tbNome.Text;
            contato.Site = "";
            contato.Telefone = tbTelefone.Text;
            contato.Celular = tbCelular.Text;
            contato.Email = tbEmail.Text;

            return contato;
        }
        private EnderecoModel SalvarEndereco()
        {
            var endereco = new EnderecoModel();
            endereco.Rua = tbRua.Text;
            endereco.Numero = tbNumero.Text;
            endereco.Bairro = tbBairro.Text;
            endereco.Cep = tbCep.Text;
            endereco.Cidade = tbCidade.Text;
            endereco.Complemento = tbComplemento.Text;
            endereco.Estado = cbEstado.SelectionBoxItem.ToString();
            endereco.Tipo = new TipoEnderecoModel() { Tipo = Constantes.ATipoEndereco.PESSOAL };

            return endereco;
        }
        private FuncionarioModel SalvarFuncionario()
        {
            var funcionario = new FuncionarioModel();
            if (_idFuncionario != 0)
                funcionario.Id = _idFuncionario;
            funcionario.Nome = tbNome.Text;
            funcionario.Rg = tbRg.Text;
            funcionario.Serie = tbSerie.Text;
            funcionario.Cpf = tbCpf.Text;
            funcionario.DataAdmissao = tbDataAdmissao.SelectedDate.Value;
            funcionario.DataNascimento = tbDataNascimento.SelectedDate.Value;
            funcionario.CarteiraTrabalho = tbCartTrabalho.Text;
            funcionario.Serie = tbSerie.Text;
            funcionario.Tipo = TipoFuncionarioBLL.RetornaTipo(new TipoFuncionarioModel() { TipoFuncionario = cbTipo.SelectionBoxItem.ToString() });

            return funcionario;
        }
        private void CarregaCombos()
        {
            cbTipo.ItemsSource = TipoFuncionarioBLL.RetornaTipos();
            cbTipo.SelectedIndex = 0;
            cbEstado.ItemsSource = EnderecoBLL.CarregaEstados();
            cbEstado.SelectedIndex = 0;
        }
        private List<String> VerificaCamposObrigatorios()
        {
            var campos = new List<String>();

            if (string.IsNullOrWhiteSpace(tbNome.Text))
                campos.Add("Nome");
            if (string.IsNullOrWhiteSpace(tbCpf.Text))
                campos.Add("Cpf");
            if (string.IsNullOrWhiteSpace(tbRg.Text))
                campos.Add("Rg");
            if (string.IsNullOrWhiteSpace(tbDataNascimento.Text))
                campos.Add("Data de Nascimento");
            if (string.IsNullOrWhiteSpace(tbDataAdmissao.Text))
                campos.Add("Data de Admissao");

            //Endereço
            if (string.IsNullOrWhiteSpace(tbRua.Text))
                campos.Add("Rua");
            if (string.IsNullOrWhiteSpace(tbNumero.Text))
                campos.Add("Número");

            //Contato
            if (string.IsNullOrWhiteSpace(tbCelular.Text))
                campos.Add("Celular");

            return campos;
        }
        private void CarregaFuncionario(FuncionarioModel func)
        {
            CarregaCamposEdicao(FuncionarioBLL.PesquisaFuncionarioId(func));
        }
        private void CarregaCamposEdicao(FuncionarioModel func)
        {
            _idFuncionario = func.Id;
            _idEndereco = func.Endereco.Id;
            _idContato = func.Contato.Id;
            tbBairro.Text = func.Endereco.Bairro;
            tbCartTrabalho.Text = func.CarteiraTrabalho;
            tbCelular.Text = func.Contato.Celular;
            tbCep.Text = func.Endereco.Cep;
            tbCidade.Text = func.Endereco.Cidade;
            tbComplemento.Text = func.Endereco.Complemento;
            tbCpf.Text = func.Cpf;
            tbDataAdmissao.Text = func.DataAdmissao.ToShortDateString();
            tbDataNascimento.Text = func.DataNascimento.ToShortDateString();
            tbEmail.Text = func.Contato.Email;
            tbNome.Text = func.Nome;
            tbNumero.Text = func.Endereco.Numero;
            tbRg.Text = func.Rg;
            tbRua.Text = func.Endereco.Rua;
            tbSerie.Text = func.Serie;
            tbTelefone.Text = func.Contato.Telefone;
        }
        private void ExcluirFuncionario(object sender, RoutedEventArgs e)
        {

            if (FuncionarioBLL.Excluir(new FuncionarioModel() { Id = _idFuncionario, Endereco = new EnderecoModel() { Id = _idEndereco }, Contato = new ContatoModel() { Id = _idContato } }))
            {
                new Funcionario().Show();
                this.Close();
            }
            return;
        }
        private void EditarFuncionario()
        {
            var func = SalvarFuncionario();
            func.Endereco = SalvarEndereco();
            func.Contato = SalvarContato();

            if (FuncionarioBLL.Editar(func))
            {
                new Funcionario().Show();
                this.Close();
            }
        }
        private void LimparCamposEndereco()
        {
            tbRua.Clear();
            tbNumero.Clear();
            tbBairro.Clear();
            tbCep.Clear();
            tbCidade.Clear();
            tbComplemento.Clear();
            cbEstado.SelectedIndex = 0;
        }
        #region Masked
        private void CpfMasked(Object sender, KeyEventArgs e)
        {
            Mascaras.CpfMasked(tbCpf, e);
        }
        private void PhoneMasked(Object sender, KeyEventArgs e)
        {
            Mascaras.PhoneMasked(tbTelefone, e);
        }
        private void CelularMasked(Object sender, KeyEventArgs e)
        {
            Mascaras.PhoneMasked(tbCelular, e);
        }
        private void SomenteNumeros(Object sender, KeyEventArgs e)
        {
            Mascaras.SomenteNumeros(tbRg, e);
        }
        private void ConsultarCep(Object sender, RoutedEventArgs e)
        {
            if (!ConsultaCeps.ConsultarCep(tbRua, tbCidade, tbBairro, cbEstado, tbCep))
            {
                lbCepNaoExistente.Visibility = System.Windows.Visibility.Visible;
                LimparCamposEndereco();
            }
        }
        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new Funcionario().Show();
        }
    }
}
