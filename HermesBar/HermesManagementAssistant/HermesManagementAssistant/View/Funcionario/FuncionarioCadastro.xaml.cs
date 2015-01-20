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
        #endregion
        public FuncionarioCadastro()
        {
            InitializeComponent();
            CarregaCombos();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            var funcionario = new FuncionarioModel();
            funcionario.Endereco = SalvarEndereco();
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

            return new EnderecoBLL().Salvar(endereco);
        }

        private void CarregaCombos()
        {
            cbTipo.ItemsSource = TipoFuncionarioBLL.RetornaTipos();
            cbTipo.SelectedIndex = 0;
        }
    }
}
