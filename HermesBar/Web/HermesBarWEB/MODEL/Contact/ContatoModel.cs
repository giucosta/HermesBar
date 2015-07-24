using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Contact
{
    public class ContatoModel
    {
        public int Id { get; set; }
        [DisplayName("Nome")]
        public string Nome { get; set; }
        [DisplayName("Telefone")]
        public string Telefone { get; set; }
        [DisplayName("Celular")]
        public string Celular { get; set; }
        [DisplayName("E-mail")]
        public string Email { get; set; }
    }
}
