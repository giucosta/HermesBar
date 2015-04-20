using BLL.Banco;
using FirstFloor.ModernUI.Windows.Controls;
using HMAViews.Utils;
using MODEL.Banco;
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

namespace HMAViews.View.Banco
{
    /// <summary>
    /// Interaction logic for CadastroCentroCussto.xaml
    /// </summary>
    public partial class CadastroCentroCusto : ModernWindow
    {
        private CentroCustoBLL _centroCustoBLL = null;
        public CentroCustoBLL CentroCustoBLL
        {
            get
            {
                if (_centroCustoBLL == null)
                    _centroCustoBLL = new CentroCustoBLL();
                return _centroCustoBLL;
            }
        }
        private CentroCustoModel _centroCustoModel = null;
        public CadastroCentroCusto()
        {
            InitializeComponent();
        }
        public CadastroCentroCusto(CentroCustoModel centroCusto)
        {
            InitializeComponent();
            _centroCustoModel = centroCusto;
            CarregaTelaEdicao();
        }
        private void CadastrarCentroCusto(object sender, RoutedEventArgs e)
        {
            if (_centroCustoModel == null)
            {
                var camposObrigatorios = CamposObrigatorios();
                if (camposObrigatorios.Count <= 0)
                {
                    var centroCusto = new CentroCustoModel();
                    centroCusto.Nome = tbNome.Text;
                    centroCusto.Codigo = tbCodigo.Text;
                    if ((bool)cbPermiteLancamento.IsChecked)
                        centroCusto.PermiteLancamento = "S";
                    else
                        centroCusto.PermiteLancamento = "N";
                    if ((bool)cbAtivo.IsChecked)
                        centroCusto.Status = "S";
                    else
                        centroCusto.Status = "N";

                    if (CentroCustoBLL.Salvar(centroCusto))
                    {
                        Mensagens.GeraMensagens("Cadastrado!", MENSAGEM.CENTROCUSTO_CADASTRO_SUCESSO, null, TIPOS_MENSAGENS.SUCESSO);
                        new CentroCusto().Show();
                        this.Close();
                    }
                    else
                        Mensagens.GeraMensagens("Erro ao cadastrar!", MENSAGEM.CENTROCUSTO_CADASTRO_ERRO, TIPOS_MENSAGENS.ERRO);
                }
                else
                    Mensagens.GeraMensagens("Campos obrigatórios", MENSAGEM.CAMPOS_OBRIGATORIOS, camposObrigatorios, TIPOS_MENSAGENS.ALERTA);
            }
            else
                Editar();
        }
        private void Editar()
        {
            var camposObrigatorios = CamposObrigatorios();
            if (camposObrigatorios.Count <= 0)
            {
                _centroCustoModel.Nome = tbNome.Text;
                _centroCustoModel.Codigo = tbCodigo.Text;
                if ((bool)cbPermiteLancamento.IsChecked)
                    _centroCustoModel.PermiteLancamento = "S";
                else
                    _centroCustoModel.PermiteLancamento = "N";
                if ((bool)cbAtivo.IsChecked)
                    _centroCustoModel.Status = "S";
                else
                    _centroCustoModel.Status = "N";

                if (CentroCustoBLL.Editar(_centroCustoModel))
                {
                    Mensagens.GeraMensagens("Editar!", MENSAGEM.CENTROCUSTO_EDITAR_SUCESSO, null, TIPOS_MENSAGENS.SUCESSO);
                    new CentroCusto().Show();
                    this.Close();
                }
                else
                    Mensagens.GeraMensagens("Erro ao cadastrar!", MENSAGEM.CENTROCUSTO_EDITAR_ERRO, TIPOS_MENSAGENS.ERRO);
            }else
                Mensagens.GeraMensagens("Campos obrigatórios", MENSAGEM.CAMPOS_OBRIGATORIOS, camposObrigatorios, TIPOS_MENSAGENS.ALERTA);
        }
        private void CarregaTelaEdicao()
        {
            tbNome.Text = _centroCustoModel.Nome;
            tbCodigo.Text = _centroCustoModel.Codigo;
            if (_centroCustoModel.Status.Equals("S"))
                cbAtivo.IsChecked = true;
            else
                cbAtivo.IsChecked = false;
            if (_centroCustoModel.PermiteLancamento.Equals("S"))
                cbPermiteLancamento.IsChecked = true;
            else
                cbPermiteLancamento.IsChecked = false;
        }
        private List<string> CamposObrigatorios()
        {
            var camposObrigatorios = new List<string>();
            if (string.IsNullOrEmpty(tbNome.Text))
                camposObrigatorios.Add("NOME");
            if (string.IsNullOrEmpty(tbCodigo.Text))
                camposObrigatorios.Add("CODIGO");

            return camposObrigatorios;
        }
    }
}
