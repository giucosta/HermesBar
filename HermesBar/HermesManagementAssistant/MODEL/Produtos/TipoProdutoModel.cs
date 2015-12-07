using MODEL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Produto
{
    public class TipoProdutoModel : IModel
    {
        public string Tipo { get; set; }
        public string Descricao { get; set; }
    }
}
