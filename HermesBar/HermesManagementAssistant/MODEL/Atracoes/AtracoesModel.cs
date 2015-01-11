using MODEL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class AtracoesModel : IModel
    {
        public string Nome { get; set; }
        public string EstiloPredominante { get; set; }

    }
}
