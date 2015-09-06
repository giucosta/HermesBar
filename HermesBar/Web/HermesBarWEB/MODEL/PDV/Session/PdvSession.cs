using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.PDV.Session
{
    public class PdvSession
    {
        public int Id { get; set; }
        public decimal ValorInicial { get; set; }
        public decimal ValorFinal { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime DataFechamento { get; set; }
        public int Usuario { get; set; }
    }
}
