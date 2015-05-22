using MODEL.Abstract;
using MODEL.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Banco
{
    public class ContasReceberModel : IModel
    {
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public ClienteModel Cliente { get; set; }
        public CentroCustoModel CentroCusto { get; set; }
        public string Referente { get; set; }
        public string FormaPagamento { get; set; }
        public int Parcelas { get; set; }
        public double Valor { get; set; }
        public double ValorPago { get; set; }
        public string Observacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Status { get; set; }

    }
}
