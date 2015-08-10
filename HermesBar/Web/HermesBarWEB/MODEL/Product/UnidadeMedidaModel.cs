using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MODEL.Product
{
    public class UnidadeMedidaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string StatusSelected { get; set; }
        public List<SelectListItem> Status { get; set; }
    }
}
