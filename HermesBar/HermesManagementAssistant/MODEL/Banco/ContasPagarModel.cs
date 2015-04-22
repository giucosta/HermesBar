using MODEL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.Fornecedor;

namespace MODEL.Banco
{
    public class ContasPagarModel : IModel
    {
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public FornecedorModel Fornecedor{ get; set; }
        public string Referente { get; set; }
        public string FormaPagamento { get; set; }
        public string Parcelas { get; set; }
        public string Valor { get; set; }
        public string ValorPago { get; set; }
        public string NumeroNota { get; set; }
        public string Observacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Status { get; set; }
    }
}
