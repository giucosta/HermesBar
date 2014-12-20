using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Usuarios;

namespace Model.Login
{
    public class Login
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public string LoginUser { get; set; }
        public string Senha { get; set; }
    }
}
