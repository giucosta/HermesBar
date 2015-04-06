using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Produto
{
    public class ProdutoGridModel
    {
        public string CodigoOriginal { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public string Unidade { get; set; }
        public string QuantidadeEstoque { get; set; }
        public string ValorVenda { get; set; }
        public string Tipo { get; set; }
    }
}
