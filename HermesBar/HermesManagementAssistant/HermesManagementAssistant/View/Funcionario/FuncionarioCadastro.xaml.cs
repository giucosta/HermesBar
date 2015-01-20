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
using Utils;
using UTILS;

namespace HermesManagementAssistant.View.Funcionario
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
        public FuncionarioCadastro()
        {
            InitializeComponent();
            CarregaCombos();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            var camposObrigatorios = VerificaCamposObrigatorios();
            if (camposObrigatorios.Count == 0)
            {
                var funcionario = new FuncionarioModel();
                funcionario.Endereco = SalvarEndereco();
                funcionario.Contato = SalvarContato();
            }
            else
                Mensagens.GeraMensagens("Campos Obrigatórios",MENSAGEM.CAMPOS_OBRIGATORIOS,camposObrigatorios,TIPOS_MENSAGENS.ALERTA);
        }

        private ContatoModel SalvarContato()
        {
            var contato = new ContatoModel();
            contato.Nome = tbNome.Text;
            contato.Site = "";
            contato.Telefone = tbTelefone.Text;
            contato.Celular = tbCelular.Text;
            contato.Email = tbEmail.Text;

            return ContatoBLL.Salvar(contato);
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
            endereco.Estado = tbEstado.Text;
            endereco.Tipo = new TipoEnderecoModel() { Tipo = Constantes.ATipoEndereco.PESSOAL };

            return EnderecoBLL.Salvar(endereco);
        }
        private void CarregaCombos()
        {
            cbTipo.ItemsSource = TipoFuncionarioBLL.RetornaTipos();
            cbTipo.SelectedIndex = 0;
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

            if (string.IsNullOrWhiteSpace(tbRua.Text))
                campos.Add("Rua");
            if (string.IsNullOrWhiteSpace(tbNumero.Text))
                campos.Add("Número");

            if (string.IsNullOrWhiteSpace(tbCelular.Text))
                campos.Add("Celular");

            return campos;
        }
    }
}
