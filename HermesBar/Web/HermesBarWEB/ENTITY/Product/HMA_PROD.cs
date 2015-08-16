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
        public decimal QUANT_ATL { get; set; }

        #region External Attributes
        public string MED_NOM { get; set; }
        public string TIP_NOM { get; set; }
        public int MED_ID { get; set; }
        public int TIP_ID { get; set; }
        #endregion
    }
}
