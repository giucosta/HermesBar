﻿using BLL.Comum;
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
                        new Funcionario().Show();
                        this.Close();
                    }
                    else
                        Mensagens.GeraMensagens("Erro ao cadastrar!", MENSAGEM.FUNCIONARIO_CADASTRO_ERRO, TIPOS_MENSAGENS.ERRO);
                }
                else
                    Mensagens.GeraMensagens("Campos Obrigatórios", MENSAGEM.CAMPOS_OBRIGATORIOS, null, TIPOS_MENSAGENS.ALERTA);
            }
        }
        private List<string> VerificaCamposObrigatorios()
        {
            var camposObrigatorios = new List<string>();
            if (string.IsNullOrEmpty(tbNome.Text))
                camposObrigatorios.Add("NOME");
            if (string.IsNullOrEmpty(tbDataNascimento.Text))
                camposObrigatorios.Add("DATA NASCIMENTO");
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
            contato.Celular = tbCelular.Text;
            contato.Email = tbEmail.Text;
            contato.Nome = tbNome.Text;
            contato.Telefone = tbTelefone.Text;

            return contato;
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
