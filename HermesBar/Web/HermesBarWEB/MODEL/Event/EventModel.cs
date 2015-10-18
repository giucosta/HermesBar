using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.Client;
using System.Web.Mvc;

namespace MODEL.Event
{
    public class EventModel
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int QuantidadePessoas { get; set; }
        public string Observacao { get; set; }
        public string StatusSelected { get; set; }
        public List<SelectListItem> Status { get; set; }
        public string ClienteSelected { get; set; }
        public List<SelectListItem> Cliente { get; set; }
        public string ClienteNome { get; set; }
        public string MatrizSelected { get; set; }
        public List<SelectListItem> Matriz { get; set; }
    }
}
