using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class UsuarioEntity : Reg
    {
        public string NOME { get; set; }
        public string EMAIL { get; set; }
        public string SENHA { get; set; }
        public int PRI_ACS { get; set; }
    }
}
