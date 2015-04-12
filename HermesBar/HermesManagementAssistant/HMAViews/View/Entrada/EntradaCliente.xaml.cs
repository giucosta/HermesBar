using BLL.Caixa;
using FirstFloor.ModernUI.Windows.Controls;
using HMAViews.Utils;
using MODEL.Caixa;
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

namespace HMAViews.View.Entrada
{
    /// <summary>
    /// Interaction logic for EntradaCliente.xaml
    /// </summary>
    public partial class EntradaCliente : ModernWindow
    {
        private CartaoBLL _cartaoBLL = null;
        public CartaoBLL CartaoBLL
        {
            get
            {
                if (_cartaoBLL == null)
                    _cartaoBLL = new CartaoBLL();
                return _cartaoBLL;
            }
        }
        private int _numeroCartao = 001;
        public EntradaCliente()
        {
            InitializeComponent();
            if (UTIL.Session.CaixaAberto)
                _numeroCartao = CartaoBLL.RecuperaUltimoCartao();
            tbNumeroCartao.Text = _numeroCartao.ToString("D4");
        }
        private void RegistrarEntrada(object sender, RoutedEventArgs e)
        {
            var camposObrigatorios = VerificaCamposObrigatorios();
            if (camposObrigatorios.Count == 0)
            {
                var model = new CartaoModel();
                model.NumeroCartao = tbNumeroCartao.Text;
                model.Cliente = new MODEL.Cliente.ClienteModel() { Nome = tbNome.Text, RG = tbRG.Text, Telefone = tbTelefone.Text };

                if (!CartaoBLL.Salvar(model))
                    Mensagens.GeraMensagens("Erro ao inserir cliente", MENSAGEM.ENTRADACLIENTE_ERRO, TIPOS_MENSAGENS.ERRO);
                else
                {
                    RegistraProximoNumero();
                    UTIL.Session.CaixaAberto = true;
                }
            }
            else
                Mensagens.GeraMensagens("Campos obrigatórios", MENSAGEM.CAMPOS_OBRIGATORIOS, camposObrigatorios, TIPOS_MENSAGENS.ALERTA);
        }
        private void RegistraProximoNumero()
        {
            tbNome.Clear();
            tbRG.Clear();
            tbTelefone.Clear();

            _numeroCartao++;
            tbNumeroCartao.Text = _numeroCartao.ToString("D4");
        }
        private List<string> VerificaCamposObrigatorios()
        {
            var camposObrigatorios = new List<string>();
            if (string.IsNullOrEmpty(tbNome.Text))
                camposObrigatorios.Add("NOME");
            if (string.IsNullOrEmpty(tbNumeroCartao.Text))
                camposObrigatorios.Add("NÚMERO CARTÃO");
            if (string.IsNullOrEmpty(tbRG.Text))
                camposObrigatorios.Add("RG");
            if (string.IsNullOrEmpty(tbTelefone.Text))
                camposObrigatorios.Add("TELEFONE");

            return camposObrigatorios;
        }
        private void MascaraCartao(object sender, KeyEventArgs e)
        {
            Mascaras.SomenteNumeros(tbNumeroCartao, e);
        }
        private void MascaraTelefone(object sender, KeyEventArgs e)
        {
            Mascaras.PhoneMasked(tbTelefone, e);
        }
        private void MascaraRg(object sender, KeyEventArgs e)
        {
            Mascaras.SomenteNumeros(tbRG, e);
        }
    }
}
