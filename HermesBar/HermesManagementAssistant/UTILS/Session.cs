using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTIL
{
    public static class Session
    {
        public static UsuarioModel Usuario { get; set; }
        public static string MensagemErro { get; set; }
    }
}
