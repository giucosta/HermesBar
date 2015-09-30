using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.PDV.Client
{
    public class PdvFechamentoClientModel
    {
        public decimal TOTAL { get; set; }
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public string Nome { get; set; }
    }
}
