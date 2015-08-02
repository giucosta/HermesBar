using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.Product
{
    public class HMA_PROD : RegControl
    {
        public string NOM { get; set; }
        public string DSC { get; set; }
        public decimal VLR_COM { get; set; }
        public decimal VLR_VEN { get; set; }
        public string COD_VEN { get; set; }
        public int QUANT_MIN { get; set; }
    }
}
