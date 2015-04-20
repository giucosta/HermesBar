using MODEL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Banco
{
    public class CentroCustoModel : IModel
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Status { get; set; }
        public string PermiteLancamento { get; set; }
    }
}
