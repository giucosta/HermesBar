using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Atracoes
{
    public class EstiloAtracoesModel
    {
        public int Id { get; set; }
        public string Estilo { get; set; }

        public override string ToString()
        {
            return Estilo;
        }
    }
}
