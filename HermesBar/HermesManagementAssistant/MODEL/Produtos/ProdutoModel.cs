using MODEL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.Fornecedor;

namespace MODEL.Produto
{
    public class ProdutoModel : IModel
    {
        public string CodigoOriginal { get; set; }
        public string CodigoBarras { get; set; }
        public string Nome { get; set; }
        public string NomeReduzido { get; set; }
        public TipoProdutoModel Tipo { get; set; }
        public MarcaProdutoModel Marca { get; set; }
        public string Unidade { get; set; }
        public FornecedorModel Fornecedor { get; set; }
        public int QuantidadeEstoque { get; set; }
        public int EstoqueMinimo { get; set; }
        public int EstoqueIdeal { get; set; }
        public double ValorCusto { get; set; }
        public double ValorVenda { get; set; }
        public string Observacao { get; set; }
    }
}
