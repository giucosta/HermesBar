using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.PDV
{
    public class HMA_PDV_CLI : RegControl
    {
        public int? _ID_CLI { get; set; }
        public DateTime HOR_ENT { get; set; }
        public DateTime? HOR_SAI { get; set; }
        public decimal? CONS_TOT { get; set; }
        public int _ID_CAI { get; set; }
    }
}
