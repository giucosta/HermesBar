using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MODEL.PDV.Caixa
{
    public class CaixaModel
    {
        public int Id { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime DataFechamento { get; set; }
        public Decimal ValorAbertura { get; set; }
        public Decimal ValorFechamento { get; set; }
        public string StatusSelected { get; set; }
        public List<SelectListItem> Status { get; set; }
    }
}
