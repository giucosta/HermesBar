using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.PDV
{
    public class HMA_PDV_CLI_MOV : RegControl
    {
        public int _ID_CLI_PDV { get; set; }
        public int _ID_PED_PDV { get; set; }
        public int _ID_MOV { get; set; }
        public string _DESCR { get; set; }
    }
}
