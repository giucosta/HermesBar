using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.Establishment
{
    public class HMA_EST : RegControl
    {
        public string FAN { get; set; }
        public string RAZ { get; set; }
        public string CNPJ { get; set; }
        public string INSEST { get; set; }
        public string INSMUN { get; set; }
        public int QUANT_MESA { get; set; }
        public int QUANT_CLI { get; set; }
    }
}
