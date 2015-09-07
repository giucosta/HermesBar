using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.PDV.Client
{
    public class PdvClientModel
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdCaixa { get; set; }
        public DateTime Entrada { get; set; }
        public DateTime Saida { get; set; }
        public decimal ConsumoTotal { get; set; }
    }
}
