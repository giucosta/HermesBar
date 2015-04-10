using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.Produto;

namespace MODEL.Caixa
{
    public class ConsumoCartaoModel
    {
        public CartaoModel Cartao { get; set; }
        public ProdutoModel Produto { get; set; }
        public DateTime Data { get; set; }
    }
}
