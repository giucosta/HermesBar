using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public abstract class Reg
    {
        public int _ID { get; set; }
        public DateTime _UPD { get; set; }
        public DateTime _INS { get; set; }
        public int _ATV { get; set; }
        public string _USR { get; set; }
    }
}
