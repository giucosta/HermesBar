using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.Employee
{
    public class HMA_FUNC : RegControl
    {
        public string RG { get; set; }
        public string CPF { get; set; }
        public string CTPS { get; set; }
        public DateTime DT_ADM { get; set; }
        public DateTime DT_DEM { get; set; }
        public int TIP { get; set; }
        public int _ID_CAR { get; set; }
        public string FUN { get; set; }
        public string SEX { get; set; }
    }
}
