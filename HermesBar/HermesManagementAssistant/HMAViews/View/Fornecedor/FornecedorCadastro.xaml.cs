using BLL.Comum;
using BLL.Fornecedor;
using FirstFloor.ModernUI.Windows.Controls;
using HMAViews.Utils;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using HMAViews;
using UTIL;

namespace HMAViews.View.Fornecedor
{
    /// <summary>
    /// Interaction logic for FornecedorCadastro.xaml
    /// </summary>
    public partial class FornecedorCadastro : ModernWindow
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
        private FornecedorModel _fornecedorEdicao = null;
        public FornecedorCadastro()
        {
            InitializeComponent();
            CarregaTela();
        }
        public FornecedorCadastro(FornecedorModel fornecedor)
        {
            InitializeComponent();
            CarregaTela();
            PreencheTelaFornecedor(fornecedor);
            btExcluir.Visibility = System.Windows.Visibility.Visible;
            _fornecedorEdicao = fornecedor;
        }

        private void Salvar(object sender, RoutedEventArgs e)
        {
            if (_fornecedorEdicao == null)
            {
                var camposObrigatorios = VerificarCamposObrigatorios();
                if (camposObrigatorios.Count == 0)
                {
                    if (FornecedorBLL.Salvar(CarregaFornecedor()))
                    {
                        Mensagens.GeraMensagens("Salvo com sucesso!", MENSAGEM.FORNECEDOR_CADASTRO_SUCESSO, null, TIPOS_MENSAGENS.SUCESSO);
                        new Fornecedor().Show();
                        this.Close();
                    }
                    else
                        Mensagens.GeraMensagens("Falha ao cadastrar!", MENSAGEM.FORNECEDOR_CADASTRO_ERRO, null, TIPOS_MENSAGENS.ERRO);
                }
                else
                    Mensagens.GeraMensagens("Campos Obrigatórios", MENSAGEM.CAMPOS_OBRIGATORIOS, camposObrigatorios, TIPOS_MENSAGENS.ALERTA);
            }
            else
                Editar();
        }
        private void Editar()
        {
            var camposObrigatorios = VerificarCamposObrigatorios();
            if (camposObrigatorios.Count == 0)
            {
                if (FornecedorBLL.Editar(CarregaFornecedor()))
                {
                    Mensagens.GeraMensagens("Editado com sucesso", MENSAGEM.FORNECEDOR_EDITAR_SUCESSO, null, TIPOS_MENSAGENS.SUCESSO);
                    new Fornecedor().Show();
                    this.Close();
                }
                else
                    Mensagens.GeraMensagens("Erro ao editar", MENSAGEM.FORNECEDOR_EDITAR_ERRO, null, TIPOS_MENSAGENS.ERRO);
            }else
                Mensagens.GeraMensagens("Campos Obrigatórios", MENSAGEM.CAMPOS_OBRIGATORIOS, camposObrigatorios, TIPOS_MENSAGENS.ALERTA);
        }
        private void Excluir(object sender, RoutedEventArgs e)
        {
            if (FornecedorBLL.Excluir(_fornecedorEdicao))
            {
                Mensagens.GeraMensagens("Excluído com sucesso", MENSAGEM.FORNECEDOR_EXCLUIR_SUCESSO, null, TIPOS_MENSAGENS.SUCESSO);
                new Fornecedor().Show();
                this.Close();
            }    
            else
                Mensagens.GeraMensagens("Erro ao excluir", MENSAGEM.FORNECEDOR_EXCLUIR_ERRO, null, TIPOS_MENSAGENS.ERRO);
        }
        private List<string> VerificarCamposObrigatorios()
        {
            var camposObrigatorios = new List<string>();
            if (string.IsNullOrWhiteSpace(tbRazaoSocial.Text))
                camposObrigatorios.Add("Razão Social");
            if (string.IsNullOrWhiteSpace(tbCnpj.Text))
                camposObrigatorios.Add("CNPJ");
            if (string.IsNullOrWhiteSpace(tbCep.Text))
                camposObrigatorios.Add("CEP");
            if (string.IsNullOrWhiteSpace(tbNome.Text))
                camposObrigatorios.Add("Nome");

            return camposObrigatorios;
        }
        private FornecedorModel CarregaFornecedor()
        {
            var fornecedor = new FornecedorModel();
            if (_fornecedorEdicao != null)
                fornecedor.Id = _fornecedorEdicao.Id;
            
            fornecedor.RazaoSocial = tbRazaoSocial.Text;
            fornecedor.Cpj = tbCnpj.Text;
            if (string.IsNullOrWhiteSpace(tbInscEstadual.Text))
                fornecedor.InscricaoEstadual = Constantes.AInscricaoEstadual.ISENTO;
            fornecedor.InscricaoEstadual = tbInscEstadual.Text;
            fornecedor.Contato = CarregaContatoFornecedor();
            fornecedor.Endereco = CarregaEnderecoFornecedor();

            return fornecedor;
        }
        private ContatoModel CarregaContatoFornecedor()
        {
            var contato = new ContatoModel();
            if (_fornecedorEdicao != null)
                contato.Id = _fornecedorEdicao.Contato.Id;
            contato.Nome = tbNome.Text;
            contato.Telefone = tbTelefone.Text;
            contato.Celular = tbCelular.Text;
            contato.Email = tbEmail.Text;
            contato.Site = tbSite.Text;

            return contato;
        }
        private EnderecoModel CarregaEnderecoFornecedor()
        {
            var endereco = new EnderecoModel();
            if (_fornecedorEdicao != null)
                endereco.Id = _fornecedorEdicao.Endereco.Id;
            endereco.Tipo = new TipoEnderecoModel() { Tipo = Constantes.ATipoEndereco.MATRIZ };
            endereco.Rua = tbRua.Text;
            endereco.Cep = tbCep.Text;
            endereco.Bairro = tbBairro.Text;
            endereco.Cidade = tbCidade.Text;
            endereco.Estado = cbEstado.SelectionBoxItem.ToString();
            endereco.Numero = tbNumero.Text;
            
            return endereco;
        }
        private void PreencheTelaFornecedor(FornecedorModel forn)
        {
            tbCnpj.Text = forn.Cpj;
            tbInscEstadual.Text = forn.InscricaoEstadual;
            tbRazaoSocial.Text = forn.RazaoSocial;
            tbCep.Text = forn.Endereco.Cep;
            tbRua.Text = forn.Endereco.Rua;
            tbCidade.Text = forn.Endereco.Cidade;
            tbBairro.Text = forn.Endereco.Bairro;
            tbCep.Text = forn.Endereco.Cep;
            var i = 0;
            foreach (var x in cbEstado.ItemsSource)
            {
                if (forn.Endereco.Estado == x.ToString())
                    cbEstado.SelectedIndex = i;
                else
                    i++;
            }
            tbNumero.Text = forn.Endereco.Numero;
            tbComplemento.Text = forn.Endereco.Complemento;
            tbNome.Text = forn.Contato.Nome;
            tbTelefone.Text = forn.Contato.Telefone;
            tbCelular.Text = forn.Contato.Celular;
            tbEmail.Text = forn.Contato.Email;
            tbSite.Text = forn.Contato.Site;
        }
        private void ConsultarCep(object sender, RoutedEventArgs e)
        {
            if (tbCep.Text.Length == 8)
            {
                if (!ConsultaCeps.ConsultarCep(tbRua, tbCidade, tbBairro, cbEstado, tbCep))
                    tbCep.Text = "Cep não encontrado";
                else
                    tbNumero.Focus();
            }
            else
            {
                Mensagens.GeraMensagens("Cep Inválido", MENSAGEM.CONSULTACEP_TAMANHO_ERRO, null, TIPOS_MENSAGENS.ALERTA);
                tbCep.Clear();
            }    
        }
        private void GerenciaCamposEndereco()
        {
            if (ConexaoWeb.IsConnected())
            {
                tbRua.IsReadOnly = true;
                tbBairro.IsReadOnly = true;
                tbCidade.IsReadOnly = true;
                cbEstado.IsReadOnly = true;
            }
            else
                btConsultar.Visibility = System.Windows.Visibility.Hidden;
        }
        private void MascaraCnpj(object sender, KeyEventArgs e)
        {
            Mascaras.CnpjMasked(tbCnpj, e);
        }
        private void MascaraInscEstadual(object sender, KeyEventArgs e)
        {
            Mascaras.SomenteNumeros(tbInscEstadual, e);
        }
        private void MascaraCep(object sender, KeyEventArgs e)
        {
            Mascaras.SomenteNumeros(tbCep, e);
        }
        private void MascaraTelefone(object sender, KeyEventArgs e)
        {
            Mascaras.PhoneMasked(tbTelefone, e);
        }
        private void MascaraCelular(object sender, KeyEventArgs e)
        {
            Mascaras.PhoneMasked(tbCelular, e);
        }
        private void CarregaTela()
        {
            GerenciaCamposEndereco();
            cbEstado.ItemsSource = EnderecoBLL.CarregaEstados();
            tbRazaoSocial.Focus();
            btExcluir.Visibility = System.Windows.Visibility.Hidden;
        }

        private void ModernWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Mensagens.GeraMensagens("Deseja fechar?", MENSAGEM.FECHAR_TELA_CONFIRMA, null, TIPOS_MENSAGENS.QUESTAO))
                e.Cancel = false;
            else
                e.Cancel = true;
        }
    }
}
