using BLL.Produtos;
using MODEL.Produto;
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
using Utils.Mensagens;

namespace HermesManagementAssistant.View.Produtos
{
    /// <summary>
    /// Interaction logic for TipoProdutoCadastro.xaml
    /// </summary>
    public partial class TipoProdutoCadastro : Window
    {
        private TipoProdutoBLL _tipoProdutoBLL = null;
        public TipoProdutoBLL TipoProdutoBLL
        {
            get
            {
                if (_tipoProdutoBLL == null)
                    _tipoProdutoBLL = new TipoProdutoBLL();
                return _tipoProdutoBLL;
            }
        }
        public TipoProdutoCadastro()
        {
            InitializeComponent();
        }
        private void Salvar(object sender, RoutedEventArgs e)
        {
            var camposObrigatorios = VerificaCamposObrigatorios();
            if (camposObrigatorios.Count == 0)
            {
                if (TipoProdutoBLL.Salvar(new TipoProdutoModel() { Tipo = tbTipo.Text, Descricao = tbDescricao.Text })){
                    Mensagens.GeraMensagens("Cadastrado com sucesso!", MENSAGEM.TIPOPRODUTO_CADASTRO_SUCESSO, null, TIPOS_MENSAGENS.SUCESSO);
                    new TipoProduto().Show();
                    this.Close();
                }
                else
                    Mensagens.GeraMensagens("Erro no cadastro!", MENSAGEM.TIPOPRODUTO_CADASTRO_ERRO, null, TIPOS_MENSAGENS.ERRO);
            }
            else
                Mensagens.GeraMensagens("Campos Obrigatórios", MENSAGEM.CAMPOS_OBRIGATORIOS, camposObrigatorios, TIPOS_MENSAGENS.ALERTA);
        }
        private List<string> VerificaCamposObrigatorios()
        {
            var list = new List<string>();
            if (string.IsNullOrEmpty(tbTipo.Text))
                list.Add("Tipo");
            return list;
        }
    }
}
