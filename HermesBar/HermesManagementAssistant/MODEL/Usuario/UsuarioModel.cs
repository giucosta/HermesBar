using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class UsuarioModel
    {
        public int IdUsuario { get; set; }
        public LoginModel Login { get; set; }
        public PerfilModel Perfil { get; set; }
        public string Nome { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
    }
}
