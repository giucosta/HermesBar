using BLL.Comum;
using BLL.Funcionario;
using FirstFloor.ModernUI.Windows.Controls;
using HMAViews.Utils;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UTIL;

namespace HMAViews.View.Funcionario
{
    /// <summary>
    /// Interaction logic for FuncionariosCadastro.xaml
    /// </summary>
    public partial class FuncionariosCadastro : ModernWindow
    {
        private FuncionarioModel _funcionarioModel = null;
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
        public FuncionariosCadastro()
        {
            InitializeComponent();
            CarregaTela();
            btExcluir.Visibility = System.Windows.Visibility.Hidden;
        }
        public FuncionariosCadastro(FuncionarioModel funcionario)
        {
            InitializeComponent();
            _funcionarioModel = funcionario;
            CarregaTela();
            CarregarEdicao(_funcionarioModel);
        }

        private void CarregaTela()
        {
            cbEstado.ItemsSource = EnderecoBLL.CarregaEstados();
            cbTipo.ItemsSource = TipoFuncionarioBLL.RetornaTipos();
        }
        private void Salvar(object sender, RoutedEventArgs e)
        {
            if (_funcionarioModel == null)
            {
                var camposObrigatorios = VerificaCamposObrigatorios();
                if (camposObrigatorios.Count == 0)
                {
                    if (FuncionarioBLL.Salvar(CarregaFuncionario()))
                    {
                        Mensagens.GeraMensagens("Funcionário Cadastrado!", MENSAGEM.FUNCIONARIO_CADASTRO_SUCESSO, null, TIPOS_MENSAGENS.SUCESSO);
                        new Funcionarios().Show();
                        this.Close();
                    }
                    else
                        Mensagens.GeraMensagens("Erro ao cadastrar!", MENSAGEM.FUNCIONARIO_CADASTRO_ERRO, TIPOS_MENSAGENS.ERRO);
                }
                else
                    Mensagens.GeraMensagens("Campos Obrigatórios", MENSAGEM.CAMPOS_OBRIGATORIOS, camposObrigatorios, TIPOS_MENSAGENS.ALERTA);
            }
            else
                Editar();
        }
        private void Editar()
        {
            var camposObrigatorios = VerificaCamposObrigatorios();
            if (camposObrigatorios.Count == 0)
            {
                if (FuncionarioBLL.Editar(CarregaFuncionario()))
                {
                    Mensagens.GeraMensagens("Funcionário editado!", MENSAGEM.FUNCIONARIO_EDITAR_SUCESSO, null, TIPOS_MENSAGENS.SUCESSO);
                    new Funcionarios().Show();
                    this.Close();
                }
                else
                    Mensagens.GeraMensagens("Erro ao editar!", MENSAGEM.FUNCIONARIO_EDITAR_ERRO, TIPOS_MENSAGENS.ERRO);
            }
            else
                Mensagens.GeraMensagens("Campos Obrigatórios", MENSAGEM.CAMPOS_OBRIGATORIOS, camposObrigatorios, TIPOS_MENSAGENS.ALERTA);
        }
        private void Excluir(object sender, RoutedEventArgs e)
        {
            if (FuncionarioBLL.Excluir(CarregaFuncionario()))
            {
                Mensagens.GeraMensagens("Excluído com sucesso", MENSAGEM.FUNCIONARIO_EXCLUIR_SUCESSO, null, TIPOS_MENSAGENS.SUCESSO);
                new Funcionarios().Show();
                this.Close();
            }
            else
                Mensagens.GeraMensagens("Erro ao excluir", MENSAGEM.FUNCIONARIO_EXCLUIR_ERRO, TIPOS_MENSAGENS.SUCESSO);
        }
        private List<string> VerificaCamposObrigatorios()
        {
            var camposObrigatorios = new List<string>();
            if (string.IsNullOrEmpty(tbNome.Text))
                camposObrigatorios.Add("NOME");
            if (string.IsNullOrEmpty(tbDataNascimento.Text))
                camposObrigatorios.Add("DATA NASCIMENTO");
            if (string.IsNullOrEmpty(tbDataAdmissao.Text))
                camposObrigatorios.Add("DATA ADMISSÃO");
            if (string.IsNullOrEmpty(tbCpf.Text))
                camposObrigatorios.Add("CPF");
            if (string.IsNullOrEmpty(cbTipo.SelectionBoxItem.ToString()))
                camposObrigatorios.Add("TIPO");
            if (string.IsNullOrEmpty(tbCep.Text))
                camposObrigatorios.Add("CEP");
            if (string.IsNullOrEmpty(tbTelefone.Text))
                camposObrigatorios.Add("TELEFONE");

            return camposObrigatorios;
        }
        private FuncionarioModel CarregaFuncionario()
        {
            var funcionario = new FuncionarioModel();
            if (_funcionarioModel != null)
                funcionario.Id = _funcionarioModel.Id;
            funcionario.CarteiraTrabalho = tbCartTrabalho.Text;
            funcionario.Endereco = CarregaEndereco();
            funcionario.Contato = CarregaContato();
            funcionario.Cpf = tbCpf.Text;
            funcionario.DataAdmissao = DateTime.Parse(tbDataAdmissao.Text);
            funcionario.DataNascimento = DateTime.Parse(tbDataNascimento.Text);
            funcionario.Nome = tbNome.Text;
            funcionario.Rg = tbRg.Text;
            funcionario.Serie = tbSerie.Text;
            funcionario.Tipo = (TipoFuncionarioModel)cbTipo.SelectedItem;

            return funcionario;
        }
        private EnderecoModel CarregaEndereco()
        {
            var endereco = new EnderecoModel();
            if (_funcionarioModel != null)
                endereco.Id = _funcionarioModel.Endereco.Id;
            endereco.Bairro = tbBairro.Text;
            endereco.Cep = tbCep.Text;
            endereco.Cidade = tbCidade.Text;
            endereco.Complemento = tbComplemento.Text;
            endereco.Estado = cbEstado.SelectionBoxItem.ToString();
            endereco.Numero = tbNumero.Text;
            endereco.Rua = tbRua.Text;
            endereco.Tipo = new TipoEnderecoModel() { Tipo = Constantes.ATipoEndereco.PESSOAL };

            return endereco;
        }
        private ContatoModel CarregaContato()
        {
            var contato = new ContatoModel();
            if (_funcionarioModel != null)
                contato.Id = _funcionarioModel.Contato.Id;
            contato.Celular = tbCelular.Text;
            contato.Email = tbEmail.Text;
            contato.Nome = tbNome.Text;
            contato.Telefone = tbTelefone.Text;

            return contato;
        }
        private void CarregarEdicao(FuncionarioModel funcionario)
        {
            tbNome.Text = funcionario.Nome;
            tbCpf.Text = funcionario.Cpf;
            tbRg.Text = funcionario.Rg;
            tbDataAdmissao.Text = funcionario.DataAdmissao.ToShortDateString();
            tbDataNascimento.Text = funcionario.DataNascimento.ToShortDateString();
            tbCartTrabalho.Text = funcionario.CarteiraTrabalho;
            tbSerie.Text = funcionario.Serie;
            int i = 0;
            foreach (var item in cbTipo.ItemsSource)
            {
                TipoFuncionarioModel tipo = (TipoFuncionarioModel)item;
                
                if(tipo.Tipo.Equals(funcionario.Tipo.Tipo)){
                    cbTipo.SelectedIndex = i;
                    break;
                }
                else
                    i++;
            }
            tbCep.Text = funcionario.Endereco.Cep;
            tbRua.Text = funcionario.Endereco.Rua;
            tbNumero.Text = funcionario.Endereco.Numero;
            tbComplemento.Text = funcionario.Endereco.Complemento;
            tbBairro.Text = funcionario.Endereco.Bairro;
            tbCidade.Text = funcionario.Endereco.Cidade;
            i = 0;
            foreach (var item in cbEstado.ItemsSource)
            {
                if (item.ToString().Equals(funcionario.Endereco.Estado))
                {
                    cbEstado.SelectedIndex = i;
                    break;
                }
                else
                    i++;
            }
            tbTelefone.Text = funcionario.Contato.Telefone;
            tbCelular.Text = funcionario.Contato.Celular;
            tbEmail.Text = funcionario.Contato.Email;
        }
        #region mascaras

        private void MascaraCpf(object sender, KeyEventArgs e)
        {
            Mascaras.CpfMasked(tbCpf, e);
        }
        private void MascaraCartTrabalho(object sender, KeyEventArgs e)
        {
            Mascaras.SomenteNumeros(tbCartTrabalho, e);
        }
        private void MascaraSerie(object sender, KeyEventArgs e)
        {
            Mascaras.SomenteNumeros(tbSerie, e);
        }
        private void MascaraTelefone(object sender, KeyEventArgs e)
        {
            Mascaras.PhoneMasked(tbTelefone, e);
        }
        private void MascaraCelular(object sender, KeyEventArgs e)
        {
            Mascaras.PhoneMasked(tbCelular, e);
        }
        #endregion
    }
}
