using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.PDV
{
    public class HMA_PDV_CAI : RegControl
    {
        public DateTime DT_ABER { get; set; }
        public DateTime? DT_FEC { get; set; }
        public decimal VLR_INI { get; set; }
        public decimal? VLR_FIN { get; set; }
        public decimal? VLR_REF { get; set; }
    }
}
