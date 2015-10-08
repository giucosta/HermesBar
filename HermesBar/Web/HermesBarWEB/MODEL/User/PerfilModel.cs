using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MODEL.User
{
    public class PerfilModel 
    {
        public int Id { get; set; }
        public string Perfil { get; set; }
        public string Descricao { get; set; }
        public string Sigla { get; set; }
        public string StatusSelected { get; set; }
        public List<SelectListItem> Status { get; set; }
    }
}
