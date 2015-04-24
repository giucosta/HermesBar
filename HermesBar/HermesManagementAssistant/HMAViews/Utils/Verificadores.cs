using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace HMAViews.Utils
{
    public class Verificadores
    {
        public static bool VerificaNumero(string text)
        {
            char[] caracter = text.ToCharArray();
            if (caracter.Length > 0)
            {
                foreach (char x in caracter)
                {
                    if (!Char.IsDigit(x))
                        return false;
                }
            }
            return true;
        }
    }
}
