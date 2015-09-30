using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.PDV
{
    public class HMA_PDV_PED_CLI : RegControl
    {
        public int _ID_CLI { get; set; }
        public string _ID_PROD { get; set; }
        public int _ID_FUNC { get; set; }
        public int QTD { get; set; }
    }
}
