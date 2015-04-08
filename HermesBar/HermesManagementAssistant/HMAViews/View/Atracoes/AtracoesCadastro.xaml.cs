using BLL.Atracoes;
using BLL.Comum;
using FirstFloor.ModernUI.Windows.Controls;
using HMAViews.Utils;
using MODEL;
using MODEL.Atracoes;
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


namespace HMAViews.View.Atracoes
{
    /// <summary>
    /// Interaction logic for AtracoesCadastro.xaml
    /// </summary>
    public partial class AtracoesCadastro : ModernWindow
    {
        private AtracoesBLL _atracoesBLL = null;
        public AtracoesBLL AtracoesBLL
        {
            get
            {
                if (_atracoesBLL == null)
                    _atracoesBLL = new AtracoesBLL();
                return _atracoesBLL;
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
        private AtracoesModel _atracao = null;
        private int _idContato = 0;
        public AtracoesCadastro()
        {
            InitializeComponent();
            btExcluir.Visibility = System.Windows.Visibility.Hidden;
            CarregaComboEstilos();
        }
        public AtracoesCadastro(AtracoesModel atracao)
        {
            InitializeComponent();
            btExcluir.Visibility = System.Windows.Visibility.Visible;
            CarregaComboEstilos();
            CarregaAtracaoEdicao(atracao);
        }
        private void Salvar(object sender, RoutedEventArgs e)
        {
            if (_atracao != null)
            {
                var obrigatorios = ValidarCampos();
                if (obrigatorios.Count == 0)
                {
                    var contato = CarregaContato();
                    var atracoes = CarregaAtracoes();
                    atracoes.Contato = contato;

                    if (AtracoesBLL.Salvar(atracoes))
                    {
                        Mensagens.GeraMensagens("Salvo com sucesso", MENSAGEM.ATRACOES_CADASTRO_SUCESSO, null, TIPOS_MENSAGENS.SUCESSO);
                        new Atracoes().Show();
                        this.Close();
                    }
                    else
                        Mensagens.GeraMensagens("Erro ao salvar", MENSAGEM.ATRACOES_CADASTRO_ERRO, TIPOS_MENSAGENS.ERRO);
                }
                else
                    Mensagens.GeraMensagens("Campos Obrigatórios", MENSAGEM.CAMPOS_OBRIGATORIOS, obrigatorios, TIPOS_MENSAGENS.ALERTA);
            }
            else
                Editar();
        }
        private void Excluir(Object sender, RoutedEventArgs e)
        {
            if (Mensagens.GeraMensagens("Deseja Excluir?", MENSAGEM.ATRACOES_EXCLUIR_CERTEZA, null, TIPOS_MENSAGENS.QUESTAO))
            {
                if (AtracoesBLL.Excluir(new AtracoesModel() { Id = _idAtracao, Contato = new ContatoModel() { Id = _idContato } }))
                {
                    Mensagens.GeraMensagens("Atração excluída", MENSAGEM.ATRACOES_EXCLUIR_SUCESSO, null, TIPOS_MENSAGENS.SUCESSO);
                    new Atracoes().Show();
                    this.Close();
                }
                else
                    Mensagens.GeraMensagens("Erro!", MENSAGEM.ATRACOES_EXCLUIR_ERRO, null, TIPOS_MENSAGENS.ERRO);
            }
            else
                return;
        }
        private void Editar()
        {
            var atracao = CarregaAtracoes();
            atracao.Contato = CarregaContato();

            if (AtracoesBLL.Editar(atracao))
            {
                Mensagens.GeraMensagens("Edição Ok!", MENSAGEM.ATRACOES_EDITAR_SUCESSO, null, TIPOS_MENSAGENS.SUCESSO);
                new Atracoes().Show();
                this.Close();
            }
            else
                Mensagens.GeraMensagens("Erro ao editar!", MENSAGEM.ATRACOES_EDITAR_ERRO, TIPOS_MENSAGENS.ERRO);
        }
        private List<String> ValidarCampos()
        {
            var camposObrigatorios = new List<String>();
            if (string.IsNullOrWhiteSpace(tbAtracao.Text))
                camposObrigatorios.Add("ATRACAO");
            if (string.IsNullOrWhiteSpace(tbValor.Text))
                camposObrigatorios.Add("VALOR");
            if (string.IsNullOrWhiteSpace(tbTempo.Text))
                camposObrigatorios.Add("TEMPO");
            if (string.IsNullOrWhiteSpace(tbNome.Text))
                camposObrigatorios.Add("NOME");
            if (string.IsNullOrWhiteSpace(tbTelefone.Text))
                camposObrigatorios.Add("TELEFONE");

            return camposObrigatorios;
        }
        private void CarregaComboEstilos()
        {
            cbEstilo.ItemsSource = AtracoesBLL.RecuperaEstilos();
            cbEstilo.SelectedIndex = 0;
        }
        private void CarregaAtracaoEdicao(AtracoesModel atracao)
        {
            _atracao = atracao;
            tbAtracao.Text = atracao.Nome;
            tbTempo.Text = atracao.Tempo_Show;
            tbValor.Text = atracao.Ultimo_Valor_Cobrado.ToString();
            cbEstilo.SelectedValue = atracao.Estilo.Id;

            var contato = ContatoBLL.RecuperaContatoId(AtracoesBLL.RecuperaIdContato(atracao));
            _idContato = contato.Id;
            tbCelular.Text = contato.Celular;
            tbEmail.Text = contato.Email;
            tbNome.Text = contato.Nome;
            tbSite.Text = contato.Site;
            tbTelefone.Text = contato.Telefone;
        }
        private ContatoModel CarregaContato()
        {
            var contato = new ContatoModel();
            if (_atracao != null)
                contato.Id = _idContato;
            contato.Nome = tbNome.Text;
            contato.Telefone = tbTelefone.Text;
            contato.Celular = tbCelular.Text;
            contato.Email = tbEmail.Text;
            contato.Site = tbSite.Text;

            return contato;
        }
        private AtracoesModel CarregaAtracoes()
        {
            var atracoes = new AtracoesModel();
            if (_idAtracao != 0)
                atracoes.Id = _idAtracao;
            atracoes.Nome = tbAtracao.Text;
            atracoes.Tempo_Show = tbTempo.Text;
            atracoes.Ultimo_Valor_Cobrado = Double.Parse(tbValor.Text);
            atracoes.Estilo = new EstiloAtracoesModel() { Id = (int)cbEstilo.SelectedValue };

            return atracoes;
        }
        //private void SomenteNumeros(Object sender, KeyEventArgs e)
        //{
        //    Mascaras.SomenteNumeros(tbValor, e);
        //}
        private void PhoneMasked(Object sender, KeyEventArgs e)
        {
            Mascaras.PhoneMasked(tbTelefone, e);
        }
        private void CelularMasked(Object sender, KeyEventArgs e)
        {
            Mascaras.PhoneMasked(tbCelular, e);
        }
    }
}
