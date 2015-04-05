using BLL.Estoque;
using BLL.Produtos;
using FirstFloor.ModernUI.Windows.Controls;
using HMAViews.Utils;
using MODEL.Estoque;
using HMAViews.View.Produtos;
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

namespace HMAViews.View.Estoque
{
    /// <summary>
    /// Interaction logic for Estoque.xaml
    /// </summary>
    public partial class Estoque : ModernWindow
    {
        private EstoqueBLL _estoqueBLL = null;
        public EstoqueBLL EstoqueBLL
        {
            get
            {
                if (_estoqueBLL == null)
                    _estoqueBLL = new EstoqueBLL();
                return _estoqueBLL;
            }
        }
        public Estoque()
        {
            InitializeComponent();
        }
        public Estoque(ProdutoGridModel produto)
        {
            InitializeComponent();
            lbCodProduto.Content = produto.CodigoOriginal;
            lbQuantEstoque.Content = produto.QuantidadeEstoque;
        }
        private void Salvar(object sender, RoutedEventArgs e)
        {
            try
            {
                EstoqueModel estoque = new EstoqueModel();
                estoque.Produto = new ProdutoModel() { CodigoOriginal = (string)lbCodProduto.Content };
                var valorAdicionar = Double.Parse(tbQuantidade.Text);
                if (valorAdicionar > 0)
                {
                    if (rbNovosItens.IsChecked == true)
                        estoque.QuantidadeEstoque = Double.Parse(lbQuantEstoque.Content.ToString()) + valorAdicionar;
                    else
                        estoque.QuantidadeEstoque = Double.Parse(lbQuantEstoque.Content.ToString()) - Double.Parse(tbQuantidade.Text);    
                }
                else
                {
                    Mensagens.GeraMensagens("Valor precisa ser superior a 0", "O valor a adicionar precisa ser superior a 0(ZERO)", null, TIPOS_MENSAGENS.ALERTA);
                    tbQuantidade.Focus();
                    return;
                }

                if (EstoqueBLL.Editar(estoque))
                {
                    Mensagens.GeraMensagens("Atualizado!", MENSAGEM.ESTOQUE_INSERIR_SUCESSO, null, TIPOS_MENSAGENS.SUCESSO);
                    new Produtos.Produtos().Show();
                    this.Close();
                }
                else
                    Mensagens.GeraMensagens("Erro ao atualizar!", MENSAGEM.ESTOQUE_INSERIR_ERRO, TIPOS_MENSAGENS.ERRO);
            }
            catch (FormatException)
            {
                Mensagens.GeraMensagens("Campo obrigatório", "É obrigatório o preenchimento do campo quantidade", null, TIPOS_MENSAGENS.ALERTA);
            }
        }
    }
}
