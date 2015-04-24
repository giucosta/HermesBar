using MODEL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Log
{
    public class LogsModel : IModel
    {
        public string Metodo { get; set; }
        public string Classe { get; set; }
        public DateTime Data { get; set; }
        public string Usuario { get; set; }
        public string Erro { get; set; }
        public string Tipo { get; set; }
    }
}
