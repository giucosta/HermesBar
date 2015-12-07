using BLL.Produtos;
using FirstFloor.ModernUI.Windows.Controls;
using HMAViews.Utils;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HMAViews.View.Produtos
{
    /// <summary>
    /// Interaction logic for TipoProdutoCadastro.xaml
    /// </summary>
    public partial class TipoProdutoCadastro : ModernWindow
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
        private TipoProdutoModel _tipoProdutoModel = null;
        public TipoProdutoCadastro()
        {
            InitializeComponent();
            btExcluir.Visibility = System.Windows.Visibility.Hidden;
        }
        public TipoProdutoCadastro(TipoProdutoModel tipoProduto)
        {
            InitializeComponent();
            CarregaTelaEdicao(tipoProduto);
        }
        private void Salvar(object sender, RoutedEventArgs e)
        {
            if (_tipoProdutoModel == null)
            {
                var camposObrigatorios = VerificaCamposObrigatorios();
                if (camposObrigatorios.Count == 0)
                {
                    if (TipoProdutoBLL.Salvar(new TipoProdutoModel() { Tipo = tbTipo.Text, Descricao = tbDescricao.Text }))
                    {
                        Mensagens.GeraMensagens("Cadastrado com sucesso!", MENSAGEM.TIPOPRODUTO_CADASTRO_SUCESSO, null, TIPOS_MENSAGENS.SUCESSO);
                        new TipoProduto().Show();
                        this.Close();
                    }
                    else
                        Mensagens.GeraMensagens("Erro ao cadastrar!", MENSAGEM.TIPOPRODUTO_CADASTRO_ERRO, TIPOS_MENSAGENS.ERRO);
                }
                else
                    Mensagens.GeraMensagens("Campos obrigatórios", MENSAGEM.CAMPOS_OBRIGATORIOS, camposObrigatorios, TIPOS_MENSAGENS.ALERTA);
            }
            else
                Editar();
                
        }
        private List<string> VerificaCamposObrigatorios()
        {
            var list = new List<string>();
            if (string.IsNullOrEmpty(tbTipo.Text))
                list.Add("Tipo");
            if (string.IsNullOrEmpty(tbDescricao.Text))
                list.Add("Descrição");
            return list;
        }
        private void CarregaTelaEdicao(TipoProdutoModel tipoProduto)
        {
            _tipoProdutoModel = tipoProduto;
            tbDescricao.Text = tipoProduto.Descricao;
            tbTipo.Text = tipoProduto.Tipo;
        }
        private void Editar()
        {
            var camposObrigatorios = VerificaCamposObrigatorios();
            if (camposObrigatorios.Count == 0)
            {
                if (TipoProdutoBLL.Editar(CarregaTipo()))
                {
                    Mensagens.GeraMensagens("Editado com sucesso!", MENSAGEM.TIPOPRODUTO_EDITAR_SUCESSO, null, TIPOS_MENSAGENS.SUCESSO);
                    new TipoProduto().Show();
                    this.Close();
                }
                else
                    Mensagens.GeraMensagens("Erro ao editar", MENSAGEM.TIPOPRODUTO_EDITAR_ERRO, TIPOS_MENSAGENS.ERRO);
            }else
                Mensagens.GeraMensagens("Campos obrigatórios", MENSAGEM.CAMPOS_OBRIGATORIOS, camposObrigatorios, TIPOS_MENSAGENS.ALERTA);
        }
        private void Excluir(object sender, RoutedEventArgs e)
        {
            if (Mensagens.GeraMensagens("Confirma exclusão?", MENSAGEM.TIPOPRODUTO_EXCLUIR_CONFIRMA, null, TIPOS_MENSAGENS.QUESTAO))
            {
                if (TipoProdutoBLL.Excluir(_tipoProdutoModel))
                {
                    Mensagens.GeraMensagens("Excluido com sucesso", MENSAGEM.TIPOPRODUTO_EXCLUIR_SUCESSO, null, TIPOS_MENSAGENS.SUCESSO);
                    new TipoProduto().Show();
                    this.Close();
                }
                else
                    Mensagens.GeraMensagens("Erro ao excluir", MENSAGEM.TIPOPRODUTO_EXCLUIR_ERRO, TIPOS_MENSAGENS.ERRO);
            }
        }
        private TipoProdutoModel CarregaTipo()
        {
            _tipoProdutoModel.Tipo = tbTipo.Text;
            _tipoProdutoModel.Descricao = tbDescricao.Text;
            return _tipoProdutoModel;
        }
    }
}
