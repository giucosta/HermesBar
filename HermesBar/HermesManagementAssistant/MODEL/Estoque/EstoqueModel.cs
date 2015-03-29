using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.Produto;

namespace MODEL.Estoque
{
    public class EstoqueModel
    {
        public ProdutoModel Produto { get; set; }
        public double QuantidadeEstoque { get; set; }
        public double EstoqueMinimo { get; set; }
        public double EstoqueIdeal { get; set; }
        public DateTime UltimaSaida { get; set; }
        public DateTime UltimaEntrada { get; set; }
    }
}
