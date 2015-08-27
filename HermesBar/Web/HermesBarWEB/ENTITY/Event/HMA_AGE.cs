using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.Event
{
    public class HMA_AGE : RegControl
    {
        public int _ID_CLI { get; set; }
        public int QUANT_RESR { get; set; }
        public string OBS { get; set; }
        public DateTime DATA_RESER { get; set; }
        public string CLI_NOM { get; set; }
    }
}
