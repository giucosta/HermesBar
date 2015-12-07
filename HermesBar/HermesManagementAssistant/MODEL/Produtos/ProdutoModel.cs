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
        public MarcaModel Marca { get; set; }
        public string Unidade { get; set; }
        public FornecedorModel Fornecedor { get; set; }
        public double ValorCusto { get; set; }
        public double ValorVenda { get; set; }
        public string Observacao { get; set; }
    }
}
