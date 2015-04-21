using BLL.Comum;
using BLL.Estabelecimento;
using FirstFloor.ModernUI.Windows.Controls;
using MODEL;
using MODEL.Estabelecimento;
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
using HMAViews.Mascara;
using HMAViews.Utils;

namespace HMAViews.View.Estabelecimento
{
    /// <summary>
    /// Interaction logic for Estabelecimento.xaml
    /// </summary>
    public partial class Estabelecimento : ModernWindow
    {
        private EstabelecimentoBLL _estabelecimentoBLL = null;
        public EstabelecimentoBLL EstabelecimentoBLL
        {
            get
            {
                if (_estabelecimentoBLL == null)
                    _estabelecimentoBLL = new EstabelecimentoBLL();
                return _estabelecimentoBLL;
            }
        }
        public Estabelecimento()
        {
            InitializeComponent();
            cbEstado.ItemsSource = new EnderecoBLL().CarregaEstados();
            GerenciaCamposEstabelecimentoSemConexao();
        }
        public void ConsultarCep(object sender, RoutedEventArgs e)
        {
            lbCepNaoExistente.Visibility = System.Windows.Visibility.Hidden;
            if (!ConsultaCeps.ConsultarCep(tbRua, tbCidade, tbBairro, cbEstado, tbCep))
            {
                lbCepNaoExistente.Visibility = System.Windows.Visibility.Visible;
                LimparCamposEndereco();
            }
            tbNumero.Focus();
        }
        private void LimparCamposEndereco()
        {
            tbRua.Text = "";
            tbCidade.Text = "";
            tbBairro.Text = "";
            cbEstado.SelectedIndex = 0;
        }
        private void GerenciaCamposEstabelecimentoSemConexao()
        {
            if (!ConexaoWeb.IsConnected())
            {
                tbRua.IsReadOnly = false;
                tbRua.Background = tbNumero.Background;
                tbBairro.IsReadOnly = false;
                tbBairro.Background = tbNumero.Background;
                tbCidade.IsReadOnly = false;
                tbCidade.Background = tbNumero.Background;
                btConsultaCep.Visibility = System.Windows.Visibility.Hidden;
            }
        }
        private void MascaraCnpj(object sender, KeyEventArgs e)
        {
            Mascaras.CnpjMasked(tbCnpj, e);
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
        private void MascaraQuantidadeMesa(object sender, KeyEventArgs e)
        {
            Mascaras.SomenteNumeros(tbQuantMesa, e);
        }
        private void MascaraTaxaServico(object sender, KeyEventArgs e)
        {
            Mascaras.SomenteNumeros(tbTaxaServico, e);
        }
        private void Gravar(object sender, RoutedEventArgs e)
        {
            if (EstabelecimentoBLL.Salvar(CarregaModelEstabelecimento()))
                Mensagens.GeraMensagens("Salvo com sucesso", MENSAGEM.ESTABELECIMENTO_CADASTRO_SUCESSO, null, TIPOS_MENSAGENS.SUCESSO);
            else
                Mensagens.GeraMensagens("Erro ao salvar", MENSAGEM.ESTABELECIMENTO_CADASTRO_ERRO, null, TIPOS_MENSAGENS.ERRO);
        }
        private EstabelecimentoModel CarregaModelEstabelecimento()
        {
            var estabelecimento = new EstabelecimentoModel();
            estabelecimento.RazaoSocial = tbRazaoSocial.Text;
            estabelecimento.NomeFantasia = tbNomeFantasia.Text;
            estabelecimento.Cnpj = tbCnpj.Text;
            estabelecimento.InscricaoEstadual = tbInscEstadual.Text;
            estabelecimento.Endereco = CarregaEnderecoEstabelecimento();
            estabelecimento.Contato = CarregaContatoEstabelecimento();
            estabelecimento.ConfigEstabelecimento = CarregaConfiguracaoEstabelecimento();

            return estabelecimento;
        }
        private EnderecoModel CarregaEnderecoEstabelecimento()
        {
            var endereco = new EnderecoModel();
            endereco.Rua = tbRua.Text;
            endereco.Numero = tbNumero.Text;
            endereco.Bairro = tbBairro.Text;
            endereco.Estado = cbEstado.SelectionBoxItem.ToString();
            endereco.Cep = tbCep.Text;
            endereco.Tipo = new TipoEnderecoModel() { Tipo = Constantes.ATipoEndereco.MATRIZ };

            return endereco;
        }
        private ContatoModel CarregaContatoEstabelecimento()
        {
            var contato = new ContatoModel();
            contato.Celular = tbCelular.Text;
            contato.Email = tbEmail.Text;
            contato.Nome = tbNome.Text;
            contato.Site = tbSite.Text;
            contato.Telefone = tbTelefone.Text;
            return contato;
        }
        private ConfigEstabelecimentoModel CarregaConfiguracaoEstabelecimento()
        {
            var configEstabelecimento = new ConfigEstabelecimentoModel();
            configEstabelecimento.AgruparItensQuantidade = cbItensIguais.IsChecked.Value;
            configEstabelecimento.QuantidadeMesas = int.Parse(tbQuantMesa.Text);
            configEstabelecimento.TaxaServico = int.Parse(tbTaxaServico.Text);
            if (rbComanda.IsChecked == true)
                configEstabelecimento.TipoSistema = Constantes.ATipoSistema.COMANDA;
            else
                configEstabelecimento.TipoSistema = Constantes.ATipoSistema.MESA;

            return configEstabelecimento;
        }
    }
}
