using MODEL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Caixa
{
    public class CaixaModel : IModel
    {
        public double ValorInicial { get; set; }
        public string ObservacaoAbertura { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime DataMovimentacao { get; set; }
        public string Descricao { get; set; }
        public double ValorEntrada { get; set; }
        public double ValorSaida { get; set; }
        public string FormaPagamento { get; set; }
        public string Observacao { get; set; }
        public DateTime DataFechamento { get; set; }
        public double TotalEntrada { get; set; }
        public double TotalSaida { get; set; }
        public double TotalGeral { get; set; }
    }
}
