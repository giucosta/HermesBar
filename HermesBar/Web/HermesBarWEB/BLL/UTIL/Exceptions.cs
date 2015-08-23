using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UTIL
{
    class Exceptions : Exception
    {
        public Exceptions(string message)
            : base(message)
        {
        }
    }
}
