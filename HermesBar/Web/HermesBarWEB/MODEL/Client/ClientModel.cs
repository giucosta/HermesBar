using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.Contact;
using System.Web.Mvc;
namespace MODEL.Client
{
    public class ClientModel
    {
        public int Id { get; set; }
        public ContatoModel Contato { get; set; }
        public DateTime DataNascimento { get; set; }
        public string StatusSelected { get; set; }
        public List<SelectListItem> Status { get; set; }
    }
}
