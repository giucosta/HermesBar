using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Banco
{
    public class ContasPagarGridModel
    {
        public int Id { get; set; }
        public int DiasAtraso { get; set; }
        public string DataVencimento { get; set; }
        public string Fornecedor { get; set; }
        public string Referente { get; set; }
        public string FormaPagamento { get; set; }
        public string Parcelas { get; set; }
        public string Valor { get; set; }
        public string Status { get; set; }
    }
}
