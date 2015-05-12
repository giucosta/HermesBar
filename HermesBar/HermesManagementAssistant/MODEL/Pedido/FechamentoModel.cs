using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Pedido
{
    public class FechamentoModel
    {
        public List<PedidoModel> Pedido { get; set; }
        public Double ValorTotal { get; set; }
        public Double ValorEntrada { get; set; }
    }
}
