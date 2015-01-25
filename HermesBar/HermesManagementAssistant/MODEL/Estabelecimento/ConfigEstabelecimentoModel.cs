using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Estabelecimento
{
    public class ConfigEstabelecimentoModel
    {
        public bool AgruparItensQuantidade { get; set; }
        public String TipoSistema { get; set; }
        public int QuantidadeMesas { get; set; }
        public int TaxaServico { get; set; }
        public EstabelecimentoModel Estabelecimento { get; set; }
    }
}
