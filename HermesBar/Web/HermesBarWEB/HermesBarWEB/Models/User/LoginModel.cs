using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HermesBarWEB.Models.User
{
    public class LoginModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string PerfilSigla { get; set; }
        public int PerfilId { get; set; }
        public string Senha { get; set; }
    }
}