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
    /// Interaction logic for TipoProduto.xaml
    /// </summary>
    public partial class TipoProduto : ModernWindow
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
        public TipoProduto()
        {
            InitializeComponent();
            gridPesquisa.ItemsSource = TipoProdutoBLL.RetornaTipos();
            cbTipo.ItemsSource = TipoProdutoBLL.RetornaTipos();
        }
        private void Pesquisar(object sender, RoutedEventArgs e)
        {
            var items = TipoProdutoBLL.Pesquisa(new TipoProdutoModel() { Id = (int)cbTipo.SelectedValue });
            if (items != null)
                gridPesquisa.ItemsSource = items;
            else
                Mensagens.GeraMensagens("Erro ao pesquisar", MENSAGEM.ERRO_PESQUISAR, TIPOS_MENSAGENS.ERRO);
        }
        private void Novo(object sender, RoutedEventArgs e)
        {
            new TipoProdutoCadastro().Show();
            this.Close();
        }
        private void Editar(object sender, MouseButtonEventArgs e)
        {
            DataGrid data = (DataGrid)sender;
            new TipoProdutoCadastro((TipoProdutoModel)data.SelectedItems[0]).Show();
            this.Close();
        }
    }
}
