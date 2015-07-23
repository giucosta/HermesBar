using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.User
{
    public class HMA_USR : RegControl
    {
        public string NOM { get; set; }
        public string EMA { get; set; }
        public string PAS { get; set; }
        public int PRI_ACS { get; set; }
        public string SIG { get; set; }
        public string DSC { get; set; }

        public int PER_ID { get; set; }
    }
}
