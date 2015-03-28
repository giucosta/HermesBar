using BLL.Fornecedor;
using DAO.Produtos;
using MODEL.Fornecedor;
using MODEL.Produto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTIL;

namespace BLL.Produtos
{
    public class ProdutoBLL
    {
        private ProdutoDAO _produtoDAO = null;
        public ProdutoDAO ProdutoDAO
        {
            get
            {
                if (_produtoDAO == null)
                    _produtoDAO = new ProdutoDAO();
                return _produtoDAO;
            }
        }
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
        private MarcaProdutoBLL _marcaProduto = null;
        public MarcaProdutoBLL MarcaProdutoBLL
        {
            get
            {
                if (_marcaProduto == null)
                    _marcaProduto = new MarcaProdutoBLL();
                return _marcaProduto;
            }
        }
        public bool Salvar(ProdutoModel produto)
        {
            if (PesquisaProdutoCodigo(produto).Count == 0)
            {
                if (produto.ValorCusto < produto.ValorVenda)
                    return ProdutoDAO.Salvar(produto);
                else
                    UTIL.Session.MensagemErro = "O valor da Venda não pode ser menor que o valor do custo!";
            }
            else
                UTIL.Session.MensagemErro = "Código já cadastrado!";
            return false;
        }
        public bool Editar(ProdutoModel produto)
        {
            try
            {
                return ProdutoDAO.Editar(produto);
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return false;
            }
        }
        public List<ProdutoGridModel> Pesquisa(ProdutoModel produto)
        {
            if(produto.Tipo != null)
                produto.Tipo = (TipoProdutoModel)TipoProdutoBLL.Pesquisa(produto.Tipo).First();

            return PreencheProdutoGrid(ProdutoDAO.PesquisaGrid(produto));
        }
        public List<string> RetornaUnidadeProduto()
        {
            var unidades = new List<string>();
            unidades.Add(Constantes.AUnidadeProduto.GRAMA);
            unidades.Add(Constantes.AUnidadeProduto.KILO);
            unidades.Add(Constantes.AUnidadeProduto.LATA);
            unidades.Add(Constantes.AUnidadeProduto.LITRO);
            unidades.Add(Constantes.AUnidadeProduto.ML);
            unidades.Add(Constantes.AUnidadeProduto.PORCAO);
            unidades.Add(Constantes.AUnidadeProduto.FARDO);
            unidades.Add(Constantes.AUnidadeProduto.FRASCO);
            unidades.Add(Constantes.AUnidadeProduto.GARRAFA);
            unidades.Add(Constantes.AUnidadeProduto.UNIDADE);

            return unidades;
        }
        public List<ProdutoModel> PesquisaProdutoCodigo(ProdutoModel produto)
        {
            try
            {
                return ProdutoDAO.PesquisaProdutoCodigo(produto).DataTableToList<ProdutoModel>();
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return null;
            }
        }
        public int SugereProximoCodigo()
        {
            try
            {
                return int.Parse(ProdutoDAO.SugereProximoCodigo().DataTableToString()) + 1;
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return 0;
            }
        }
        private List<ProdutoGridModel> PreencheProdutoGrid(DataTable data)
        {
            var list = new List<ProdutoGridModel>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                list.Add(new ProdutoGridModel()
                {
                    CodigoOriginal = data.Rows[i]["CodigoOriginal"].ToString(),
                    Marca = data.Rows[i]["Marca"].ToString(),
                    Nome = data.Rows[i]["Nome"].ToString(),
                    QuantidadeEstoque = data.Rows[i]["QuantidadeEstoque"].ToString(),
                    Tipo = data.Rows[i]["Tipo"].ToString(),
                    Unidade = data.Rows[i]["Unidade"].ToString(),
                    ValorVenda = data.Rows[i]["ValorVenda"].ToString()
                });
            }
            return list;
        }
        public int RecuperaIdFornecedorProduto(ProdutoModel produto)
        {
            try
            {
                return ProdutoDAO.RecuperaIdFornecedorProduto(produto);
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return 0;
            }
        }
        public int RecuperaIdMarcaProduto(ProdutoModel produto)
        {
            try
            {
                return ProdutoDAO.RecuperaIdMarcaProduto(produto);
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return 0;
            }
        }
        public ProdutoModel RetornaProdutoEdicao(ProdutoGridModel grid)
        {
            try
            {
                var produto = ProdutoDAO.RecuperaProdutoEdicao(grid.CodigoOriginal).DataTableToSimpleObject<ProdutoModel>();
                produto.Fornecedor = FornecedorBLL.PesquisaFornecedorPorId(ProdutoDAO.RecuperaIdFornecedorProduto(produto));
                produto.Marca = MarcaProdutoBLL.RecuperaMarcaid(ProdutoDAO.RecuperaIdMarcaProduto(produto));
                produto.Tipo = TipoProdutoBLL.RecuperaTipoId(ProdutoDAO.RecuperaIdTipoProduto(produto));

                return produto;
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return null;
            }
        }
    }
}
