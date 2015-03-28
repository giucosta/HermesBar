using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UTIL
{
    public class ConexaoWeb
    {
        //Método da API
        [DllImport("wininet.dll")]
        private extern static Boolean InternetGetConnectedState(out int Description, int ReservedValue);

        public static Boolean IsConnected()
        {
            int Description;
            return InternetGetConnectedState(out Description, 0);
        }
    }
}
