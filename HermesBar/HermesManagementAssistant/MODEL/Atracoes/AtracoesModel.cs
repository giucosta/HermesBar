using MODEL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.Atracoes;

namespace MODEL
{
    public class AtracoesModel : IModel
    {
        public string Nome { get; set; }
        public EstiloAtracoesModel Estilo { get; set; }
        public ContatoModel Contato { get; set; }
        public Double Ultimo_Valor_Cobrado { get; set; }
        public string Tempo_Show { get; set; }
    }
}
