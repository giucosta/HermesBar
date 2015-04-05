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
            EstoqueModel estoque = new EstoqueModel();
            estoque.Produto = new ProdutoModel() { CodigoOriginal = (string)lbCodProduto.Content };
            estoque.QuantidadeEstoque = Double.Parse(tbQuantidade.Text);

            if (EstoqueBLL.Editar(estoque))
            {
                Mensagens.GeraMensagens("Atualizado!", MENSAGEM.ESTOQUE_INSERIR_SUCESSO, null, TIPOS_MENSAGENS.SUCESSO);
                new Produtos.Produtos().Show();
                this.Close();
            }
            else
                Mensagens.GeraMensagens("Erro ao atualizar!", MENSAGEM.ESTOQUE_INSERIR_ERRO, TIPOS_MENSAGENS.ERRO);
        }
    }
}
