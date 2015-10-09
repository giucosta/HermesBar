using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MODEL.User
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string PerfilSigla { get; set; }
        public string PerfilDescricao { get; set; }
        public int PerfilId { get; set; }
        public List<SelectListItem> Perfil { get; set; }
        public string PerfilSelected { get; set; }
        public string Senha { get; set; }
        public string StatusSelected { get; set; }
        public List<SelectListItem> Status { get; set; }
    }
}
