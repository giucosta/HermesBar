using MODEL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.Cliente;
using MODEL.Produto;

namespace MODEL.Caixa
{
    public class CartaoModel : IModel
    {
        public string NumeroCartao { get; set; }
        public ClienteModel Cliente { get; set; }
        public DateTime Data { get; set; }
        public double ValorTotal { get; set; }
        public string FormaPagamento { get; set; }
        public DateTime HoraEntrada { get; set; }
        public DateTime HoraSaida { get; set; }
    }
}
